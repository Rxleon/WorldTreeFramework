﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2022/8/1 9:47

* 描述： 移除法则
* 
* 从节点移除时触发

*/

namespace WorldTree
{
    /// <summary>
    /// 移除法则接口
    /// </summary>
    public interface IRemoveRule : ISendRule { }

    /// <summary>
    /// 移除法则
    /// </summary>
    public abstract class RemoveRule<N> : SendRuleBase<IRemoveRule, N> where N : Node { }
}