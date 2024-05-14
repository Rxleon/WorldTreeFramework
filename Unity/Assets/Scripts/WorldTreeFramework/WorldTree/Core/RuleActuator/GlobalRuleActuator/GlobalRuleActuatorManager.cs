﻿/****************************************

* 作者： 闪电黑客
* 日期： 2023/1/31 10:08

* 描述： 全局法则执行器集合

*/

namespace WorldTree
{

	public static partial class NodeRule
	{
		/// <summary>
		/// 强制获取全局节点法则执行器
		/// </summary>
		public static GlobalRuleActuator<R> GetOrNewGlobalRuleActuator<R>(this INode self, out GlobalRuleActuator<R> globalRuleActuator)
		where R : IRule
		{
			return self.Core.AddComponent(out GlobalRuleActuatorManager _, isPool: false).AddComponent(out globalRuleActuator, isPool: false);
		}
	}

	/// <summary>
	/// 全局法则执行器管理器
	/// </summary>
	public class GlobalRuleActuatorManager : Node, ComponentOf<WorldTreeCore>
		, AsAwake
	{ }
}
