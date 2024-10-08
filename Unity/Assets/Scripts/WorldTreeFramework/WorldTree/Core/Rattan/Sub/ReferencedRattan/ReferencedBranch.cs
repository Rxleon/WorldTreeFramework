﻿/****************************************

* 作者： 闪电黑客
* 日期： 2023/12/14 03:27:57

* 描述： 引用关系藤分支
* 
* 起初是为了做到双向绑定，当节点解除绑定时，绑定者会收到解除通知
* 但双方都需要存俩个字典，一个需要记录我引用了谁，一个需要记录谁引用了我。
* 这样内存消耗太大。
* 
* 后来写了 NodeRef<T> 这个功能就没用了，但是还是留着吧，以后可能会用到
* 

*/

using System.Collections;
using System.Collections.Generic;
using WorldTree.Internal;

namespace WorldTree
{
	/// <summary>
	/// 引用子级藤
	/// </summary>
	public class ReferencedChildRattan : ReferencedParentRattan { }

	/// <summary>
	/// 引用父级藤
	/// </summary>
	public class ReferencedParentRattan : Unit, IRattan<long>
	{

		public int Count => nodeDict.Count;

		/// <summary>
		/// 节点字典
		/// </summary>
		protected UnitDictionary<long, INode> nodeDict;

		public override void OnCreate()
		{
			Core.PoolGetUnit(out nodeDict);
		}


		public bool Contains(long key) => nodeDict.ContainsKey(key);

		public bool ContainsId(long id) => nodeDict.ContainsKey(id);


		public bool TryAddNode<N>(long key, N node) where N : class, INode => nodeDict.TryAdd(key, node);

		public bool TryGetNodeKey(long nodeId, out long key) { key = nodeId; return true; }

		public bool TryGetNode(long key, out INode node) => this.nodeDict.TryGetValue(key, out node);
		public bool TryGetNodeById(long id, out INode node) => this.nodeDict.TryGetValue(id, out node);

		public INode GetNode(long key) => this.nodeDict.TryGetValue(key, out INode node) ? node : null;
		public INode GetNodeById(long id) => this.nodeDict.TryGetValue(id, out INode node) ? node : null;


		public void RemoveNode(long nodeId) => nodeDict.Remove(nodeId);

		public void Clear()
		{
			nodeDict.Clear();
		}

		public IEnumerator<INode> GetEnumerator() => nodeDict.Values.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => nodeDict.Values.GetEnumerator();

		public override void OnDispose()
		{
			this.nodeDict.Dispose();
			this.nodeDict = null;
		}
	}

	/// <summary>
	/// 节点引用关系藤帮助类
	/// </summary>
	public static class NodeReferencedRattanHelper
	{
		/// <summary>
		/// 建立引用关系
		/// </summary>
		public static void Referenced(INode self, INode node)
		{
			NodeRattanHelper.AddRattan<ReferencedChildRattan>(self).TryAddNode(node.Id, node);
			NodeRattanHelper.AddRattan<ReferencedParentRattan>(node).TryAddNode(self.Id, self);
		}

		/// <summary>
		/// 解除引用关系
		/// </summary>
		public static void DeReferenced(INode self, INode node)
		{
			if (self.TryGetRattan(out ReferencedChildRattan _))
			{
				NodeRattanHelper.RemoveRattanNode<ReferencedChildRattan>(self, node);
				NodeRuleHelper.TrySendRule(self, default(DeReferencedChild), node);
			}

			if (node.TryGetRattan(out ReferencedParentRattan _))
			{
				NodeRattanHelper.RemoveRattanNode<ReferencedParentRattan>(node, self);
				NodeRuleHelper.TrySendRule(node, default(DeReferencedParent), self);
			}
		}

		/// <summary>
		/// 解除所有引用关系
		/// </summary>
		public static void DeReferencedAll(INode self)
		{
			//移除父级
			if (self.TryGetRattan(out ReferencedParentRattan parentRattan))
			{
				using (self.Core.PoolGetUnit(out UnitQueue<INode> nodes))
				{
					foreach (var item in parentRattan) nodes.Enqueue(item);
					while (nodes.Count != 0) DeReferenced(self, nodes.Dequeue());
				}
			}

			//移除子级
			if (self.TryGetRattan(out ReferencedChildRattan childRattan))
			{
				using (self.Core.PoolGetUnit(out UnitQueue<INode> nodes))
				{
					foreach (var item in childRattan) nodes.Enqueue(item);
					while (nodes.Count != 0) DeReferenced(self, nodes.Dequeue());
				}
			}
		}


		/// <summary>
		/// 解除所有引用关系, 通知自己的移除生命周期事件
		/// </summary>
		public static void SendAllReferencedNodeRemove(INode self)
		{
			//移除父级
			if (self.TryGetRattan(out ReferencedParentRattan parentRattan))
			{
				using (self.Core.PoolGetUnit(out UnitQueue<INode> nodes))
				{
					foreach (var item in parentRattan) nodes.Enqueue(item);
					while (nodes.Count != 0)
					{
						var node = nodes.Dequeue();
						NodeRattanHelper.RemoveRattanNode<ReferencedChildRattan>(node, self);
						NodeRuleHelper.TrySendRule(node, default(ReferencedChildRemove), self);
					}
				}
			}

			//移除子级
			if (self.TryGetRattan(out ReferencedChildRattan childRattan))
			{
				using (self.Core.PoolGetUnit(out UnitQueue<INode> nodes))
				{
					foreach (var item in childRattan) nodes.Enqueue(item);
					while (nodes.Count != 0)
					{
						var node = nodes.Dequeue();
						NodeRattanHelper.RemoveRattanNode<ReferencedParentRattan>(node, self);
						NodeRuleHelper.TrySendRule(node, default(ReferencedParentRemove), self);
					}
				}
			}
		}

	}
}
