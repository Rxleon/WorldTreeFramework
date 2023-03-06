﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2022/8/24 17:31

* 描述： 活跃禁用法则
* 
* 同时会在移除节点前触发事件
* 

*/

namespace WorldTree
{
    /// <summary>
    /// 活跃禁用法则接口
    /// </summary>
    public interface IDisableRule : ISendRule { }

    /// <summary>
    /// 活跃禁用法则
    /// </summary>
    public abstract class DisableRule<N> : SendRuleBase<IDisableRule, N>
        where N : Node
    {
        public override void Invoke(Node self)
        {
            if (self.IsActive != self.m_ActiveEventMark)
            {
                if (!self.IsActive)
                {
                    OnEvent(self as N);
                }
                self.m_ActiveEventMark = self.IsActive;
            }
        }
    }
}