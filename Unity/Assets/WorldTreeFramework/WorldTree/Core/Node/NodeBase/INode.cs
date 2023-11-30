﻿/****************************************

* 作者： 闪电黑客
* 日期： 2023/3/11 16:39

* 描述： 树节点最底层接口
* 
* 抽出这个接口是为了用于扩展原生类型

*/

namespace WorldTree
{
	/// <summary>
	/// 节点：可用法则限制
	/// </summary>
	/// <typeparam name="R">法则类型</typeparam>
	/// <remarks>节点拥有的法则，和Where约束搭配形成法则调用限制</remarks>
	public interface AsRule<in R> where R : IRule { }


	/// <summary>
	/// 核心节点标记
	/// </summary>
	/// <remarks>将节点标记为核心组件，避免核心启动时处理自己出现死循环</remarks>
	public interface ICoreNode { }


	/// <summary>
	/// 世界树节点接口
	/// </summary>
	/// <remarks>
	/// <para>世界树节点最底层接口</para> 
	/// </remarks>
	public partial interface INode : IUnitPoolItem

		, AsRule<INewRule>
		, AsRule<IGetRule>
		, AsRule<IRecycleRule>
		, AsRule<IDestroyRule>

		, AsRule<IEnableRule>
		, AsRule<IDisableRule>

		, AsRule<IGraftRule>
		, AsRule<ICutRule>

		, AsRule<IAddRule>
		, AsRule<IUpdateRule>
		, AsRule<IUpdateTimeRule>
		, AsRule<IBeforeRemoveRule>
		, AsRule<IRemoveRule>

		, AsRule<IDeReferencedChildRule>
		, AsRule<IDeReferencedParentRule>

		, AsRule<IReferencedChildRemoveRule>
		, AsRule<IReferencedParentRemoveRule>

	{

		/// <summary>
		/// 节点ID
		/// </summary>
		/// <remarks>在框架内唯一</remarks>
		public long Id { get; set; }

		/// <summary>
		/// 树根节点
		/// </summary>
		/// <remarks>挂载核心启动后的管理器组件</remarks>
		public WorldTreeRoot Root { get; set; }

		/// <summary>
		/// 树枝节点
		/// </summary>
		/// <remarks>用于划分作用域</remarks>
		public INode Domain { get; set; }

		/// <summary>
		/// 父节点
		/// </summary>
		public INode Parent { get; set; }


		#region Active
		/// <summary>
		/// 活跃开关
		/// </summary>
		public bool ActiveToggle { get; set; }

		/// <summary>
		/// 活跃状态(设定为只读，禁止修改)
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// 活跃事件标记
		/// </summary>
		public bool m_ActiveEventMark { get; set; }

		#endregion

		#region Rattan



		#endregion

		#region Branch

		/// <summary>
		/// 挂载的分支类型
		/// </summary>
		public long BranchType { get; set; }

		/// <summary>
		/// 树分支
		/// </summary>
		public UnitDictionary<long, IBranch> m_Branchs { get; set; }


		/// <summary>
		/// 树分支
		/// </summary>
		public UnitDictionary<long, IBranch> Branchs { get; }


		#endregion


		#region 分支处理

		#region 添加

		/// <summary>
		/// 添加分支
		/// </summary>
		public B AddBranch<B>() where B : class, IBranch;

		/// <summary>
		/// 添加分支
		/// </summary>
		public IBranch AddBranch(long Type);

		#endregion

		#region 移除

		/// <summary>
		/// 移除分支中的节点
		/// </summary>
		public void RemoveBranchNode<B>(INode node) where B : class, IBranch;

		/// <summary>
		/// 移除分支中的节点
		/// </summary>
		public void RemoveBranchNode(long branchType, INode node);

		#endregion

		#region 获取

		/// <summary>
		/// 尝试获取分支
		/// </summary>
		public bool TryGetBranch<B>(out B branch) where B : class, IBranch;

		/// <summary>
		/// 尝试获取分支
		/// </summary>
		public bool TryGetBranch(long branchType, out IBranch branch);

		/// <summary>
		/// 获取分支
		/// </summary>
		public B GetBranch<B>() where B : class, IBranch;

		/// <summary>
		/// 获取分支
		/// </summary>
		public IBranch GetBranch(long branchType);

		#endregion

		#endregion

		#region 节点处理

		#region 添加

		/// <summary>
		/// 树结构添加节点
		/// </summary>
		public N TreeAddNode<B, K, N>(K key, N node) where B : class, IBranch<K> where N : class, INode;
		/// <summary>
		/// 树结构添加节点
		/// </summary>
		public N TreeAddNode<B, K, N, T1>(K key, N node, T1 arg1) where B : class, IBranch<K> where N : class, INode;
		/// <summary>
		/// 树结构添加节点
		/// </summary>
		public N TreeAddNode<B, K, N, T1, T2>(K key, N node, T1 arg1, T2 arg2) where B : class, IBranch<K> where N : class, INode;
		/// <summary>
		/// 树结构添加节点
		/// </summary>
		public N TreeAddNode<B, K, N, T1, T2, T3>(K key, N node, T1 arg1, T2 arg2, T3 arg3) where B : class, IBranch<K> where N : class, INode;
		/// <summary>
		/// 树结构添加节点
		/// </summary>
		public N TreeAddNode<B, K, N, T1, T2, T3, T4>(K key, N node, T1 arg1, T2 arg2, T3 arg3, T4 arg4) where B : class, IBranch<K> where N : class, INode;
		/// <summary>
		/// 树结构添加节点
		/// </summary>
		public N TreeAddNode<B, K, N, T1, T2, T3, T4, T5>(K key, N node, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) where B : class, IBranch<K> where N : class, INode;

		/// <summary>
		/// 树结构添加自己时的处理
		/// </summary>
		/// <remarks>由框架内部调用</remarks>
		public void OnTreeAddSelf();

		#endregion

		#region 嫁接

		/// <summary>
		/// 树结构尝试嫁接节点
		/// </summary>
		public bool TreeGraftNode<B, K>(K key, INode node) where B : class, IBranch<K>;

		/// <summary>
		/// 树结构嫁接自己时的处理
		/// </summary>
		/// <remarks>由框架内部调用</remarks>
		public void OnTreeGraftSelf();

		#endregion

		#region 裁剪

		/// <summary>
		/// 树结构尝试裁剪节点
		/// </summary>
		public bool TryCutNodeById<B>(long id, out INode node) where B : class, IBranch;
		/// <summary>
		/// 树结构尝试裁剪节点
		/// </summary>
		public bool TryCutNode<B, K>(K key, out INode node) where B : class, IBranch<K>;

		/// <summary>
		/// 树结构裁剪节点
		/// </summary>
		public INode CutNodeById<B>(long id) where B : class, IBranch;
		/// <summary>
		/// 树结构裁剪节点
		/// </summary>
		public INode CutNode<B, K>(K key) where B : class, IBranch<K>;


		/// <summary>
		/// 从树上将自己裁剪下来
		/// </summary>
		public INode TreeCutSelf();

		/// <summary>
		/// 从树上将自己裁剪下来时的处理
		/// </summary>
		/// <remarks>由框架内部调用</remarks>
		public void OnTreeCutSelf();

		#endregion

		#region 释放

		/// <summary>
		/// 释放所有分支的所有节点
		/// </summary>
		public void RemoveAllNode();

		/// <summary>
		/// 释放分支的所有节点
		/// </summary>
		public void RemoveAllNode<B>() where B : class, IBranch;

		/// <summary>
		/// 释放分支的所有节点
		/// </summary>
		public void RemoveAllNode(long branchType);

		/// <summary>
		/// 根据键值释放分支的节点
		/// </summary>
		public void RemoveNode<B, K>(K key) where B : class, IBranch<K>;

		/// <summary>
		/// 根据id释放分支的节点
		/// </summary>
		public void RemoveNodeById<B>(long id) where B : class, IBranch;

		/// <summary>
		/// 释放前的处理
		/// </summary>
		/// <remarks>由框架内部调用</remarks>
		public void OnBeforeDispose();

		#endregion

		#region 获取

		/// <summary>
		/// 节点Id包含判断
		/// </summary>
		public bool ContainsId<B>(long id) where B : class, IBranch;
		/// <summary>
		/// 节点键值包含判断
		/// </summary>
		public bool Contains<B, K>(K key) where B : class, IBranch<K>;

		/// <summary>
		/// 树结构尝试获取节点
		/// </summary>
		public bool TryGetNodeById<B>(long id, out INode node) where B : class, IBranch;

		/// <summary>
		/// 树结构尝试获取节点
		/// </summary>
		public bool TryGetNode<B, K>(K key, out INode node) where B : class, IBranch<K>;

		/// <summary>
		/// 树结构获取节点
		/// </summary>
		public INode GetNodeById<B>(long id) where B : class, IBranch;
		/// <summary>
		/// 树结构获取节点
		/// </summary>
		public INode GetNode<B, K>(K key) where B : class, IBranch<K>;

		#endregion

		#endregion

	}
}