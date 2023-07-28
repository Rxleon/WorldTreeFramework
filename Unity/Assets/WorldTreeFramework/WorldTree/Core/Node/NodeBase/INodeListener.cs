﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2023/5/15 15:57

* 描述： 节点监听器最底层接口
* 
* 

*/

using System;

namespace WorldTree
{
    /// <summary>
    /// 监听器状态
    /// </summary>
    public enum ListenerState
    {
        /// <summary>
        /// 不是监听器
        /// </summary>
        Not,
        /// <summary>
        /// 监听目标是节点
        /// </summary>
        Node,
        /// <summary>
        /// 监听目标是法则
        /// </summary>
        Rule
    }

    /// <summary>
    /// 动态监听器接口
    /// </summary>
    public interface INodeListener : INode
      , AsRule<IListenerAddRule>
      , AsRule<IListenerRemoveRule>
    {
        #region Listener
        /// <summary>
        /// 动态监听器状态
        /// </summary>
        public ListenerState listenerState { get; set; }

        /// <summary>
        /// 动态监听目标类型
        /// </summary>
        public Type listenerTarget { get; set; }
        #endregion
    }
}