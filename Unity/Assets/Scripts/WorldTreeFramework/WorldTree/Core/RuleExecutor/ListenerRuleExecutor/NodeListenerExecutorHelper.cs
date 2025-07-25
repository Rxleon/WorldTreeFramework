﻿/****************************************

* 作者： 闪电黑客
* 日期： 2023/9/4 20:34

* 描述： 节点监听执行器帮助类

*/

namespace WorldTree
{
	/// <summary>
	/// 节点监听执行器帮助类
	/// </summary>
	public static class NodeListenerExecutorHelper
	{
		/// <summary>
		/// 获取节点监听执行器
		/// </summary>
		/// <remarks>获取监听这个节点的监听器法则执行器</remarks>
		public static IRuleExecutor<R> GetListenerExecutor<R>(INode node)
		   where R : IListenerRule
		{
			if (!node.Core.IsCoreActive) return null;
			if (node.Core.ReferencedPoolManager == null) return null;
			if (!node.Core.ReferencedPoolManager.TryGetPool(node.Type, out ReferencedPool nodePool)) return null;
			if (!node.Core.RuleManager.TryGetTargetRuleGroup(node.TypeToCode<R>(), node.Type, out RuleGroup ruleGroup)) return null;
			if (!nodePool.TryGetComponent(out ListenerRuleExecutorGroup executorGroup)) nodePool.AddComponent(out executorGroup);
			return executorGroup.AddRuleExecutor(ruleGroup) as IRuleExecutor<R>;
		}
	}
}
