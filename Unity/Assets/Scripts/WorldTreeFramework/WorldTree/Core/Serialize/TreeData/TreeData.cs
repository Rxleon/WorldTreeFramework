﻿/****************************************

* 作者：闪电黑客
* 日期：2024/9/3 12:08

* 描述：

*/
namespace WorldTree
{

	/// <summary>
	/// 序列化帮助类
	/// </summary>
	public static class TreeDataHelper
	{
		/// <summary>
		/// 序列化数据节点类型
		/// </summary>
		public static byte[] SerializeNode(INode self)
		{
			if (self.IsDisposed) return null;
			NodeBranchTraversalHelper.TraversalPostorder(self, current => current.Core.SerializeRuleGroup.Send(current));

			self.AddTemp(out TreeDataByteSequence sequence);
			if (self?.Parent == null) return null;
			sequence.Serialize(self);
			byte[] bytes = sequence.ToBytes();
			sequence.Dispose();
			return bytes;
		}

		/// <summary>
		/// 反序列化数据节点类型
		/// </summary>
		public static N DeseralizeNode<N>(INode self, byte[] bytes)
			where N : class, INode
		{
			self.AddTemp(out TreeDataByteSequence sequence).SetBytes(bytes);
			INode treeSpade = null;
			sequence.Deserialize(ref treeSpade);
			sequence.Dispose();
			return treeSpade as N;
		}


		/// <summary>
		/// 序列化树数据
		/// </summary>
		public static byte[] SerializeTreeData(TreeData treeData)
		{
			treeData.Parent.AddTemp(out TreeDataByteSequence sequence);
			sequence.SerializeTreeData(treeData);
			byte[] bytes = sequence.ToBytes();
			sequence.Dispose();
			return bytes;
		}

		/// <summary>
		/// 反序列化为通用数据结构
		/// </summary>
		public static TreeData DeserializeTreeData(INode self, byte[] bytes)
		{
			self.AddTemp(out TreeDataByteSequence sequence).SetBytes(bytes);
			TreeData treeData = sequence.DeserializeTreeData();
			sequence.Dispose();
			return treeData;
		}
	}

	/// <summary>
	/// 树数据节点
	/// </summary>
	public class TreeData : Node
		, NumberNodeOf<TreeData>
		, ListNodeOf<TreeData>
		, ChildOf<TreeData>
		, TempOf<INode>
		, AsNumberNodeBranch
		, AsChildBranch
		, AsListNodeBranch
		, AsAwake
	{
		/// <summary>
		/// 类型名称
		/// </summary>
		public string TypeName;

		/// <summary>
		/// 是否默认值
		/// </summary>
		public bool IsDefault = false;



		public override string ToString()
		{
			return $"[TreeData:{this.TypeName}] {(IsDefault ? ": Null" : "")}";
		}
	}

	/// <summary>
	/// 树数据类型节点
	/// </summary>
	public class TreeDataClass : TreeData
	{
		/// <summary>
		/// 节点ID列表
		/// </summary>
		public TreeList<long> nodeNumList;

	}

	/// <summary>
	/// 树数据数组节点
	/// </summary>
	public class TreeDataArray : TreeDataClass
	{
		/// <summary>
		/// 维度长度列表
		/// </summary>
		public TreeList<int> LengthList;



		public override string ToString()
		{
			return $"[TreeArray:{this.TypeName}]:[{string.Join(",", LengthList)}]";
		}


	}

	/// <summary>
	/// 树数据数值
	/// </summary>
	public class TreeDataValue : TreeData
	{
		/// <summary>
		/// 数值
		/// </summary>
		public object Value;

		public override string ToString()
		{
			return $"[TreeValue:{this.TypeName}] : {Value}";
		}
	}

	public static class TreeDataRule
	{
		class Remove : RemoveRule<TreeData>
		{
			protected override void Execute(TreeData self)
			{
				self.TypeName = null;
				self.IsDefault = false;
			}
		}

		class Add : AddRule<TreeDataArray>
		{
			protected override void Execute(TreeDataArray self)
			{
				self.AddTemp(out self.LengthList);
			}
		}
	}

}
