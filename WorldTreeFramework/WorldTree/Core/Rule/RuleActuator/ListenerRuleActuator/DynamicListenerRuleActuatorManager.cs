﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2023/3/6 9:42

* 描述： 动态监听法则执行器管理器

*/

using System;
using System.Collections.Generic;

namespace WorldTree
{
    /// <summary>
    /// 动态监听法则执行器管理器
    /// </summary>
    public class DynamicListenerRuleActuatorManager : Node
    {
        /// <summary>
        /// 目标类型 法则执行器字典
        /// </summary>
        /// <remarks>目标类型《系统，法则执行器》</remarks>
        public Dictionary<Type, ListenerRuleActuatorGroup> ListenerActuatorGroupDictionary = new Dictionary<Type, ListenerRuleActuatorGroup>();

        /// <summary>
        /// 释放后
        /// </summary>
        public override void OnDispose()
        {
            IsRecycle = true;
            IsDisposed = true;
            ListenerActuatorGroupDictionary.Clear();
        }

        #region 判断监听器



        #endregion


        #region 获取执行器

        /// <summary>
        /// 尝试添加动态执行器
        /// </summary>
        public bool TryAddRuleActuator(Type Target, Type RuleType, out RuleActuator actuator)
        {
            //判断是否有组
            if (TryGetGroup(Target, out var group))
            {
                //判断是否有执行器
                if (group.TryGetRuleActuator(RuleType, out actuator)) { return true; }

                //没有执行器 则判断这个系统类型是否有动态类型监听法则集合
                else if (Root.RuleManager.TryGetTargetRuleGroup(RuleType, typeof(Node), out var ruleGroup))
                {
                    //新建执行器
                    actuator = group.GetRuleActuator(RuleType);
                    actuator.ruleGroup = ruleGroup;
                    RuleActuatorAddListener(actuator, Target);
                    return true;
                }
            }
            else if (Root.RuleManager.TryGetTargetRuleGroup(RuleType, typeof(Node), out var ruleGroup))
            {
                //新建组和执行器
                actuator = GetGroup(Target).GetRuleActuator(RuleType);
                actuator.ruleGroup = ruleGroup;
                RuleActuatorAddListener(actuator, Target);
                return true;
            }
            actuator = null;
            return false;
        }

        /// <summary>
        /// 执行器填装监听器
        /// </summary>
        private void RuleActuatorAddListener(RuleActuator actuator, Type Target)
        {
            foreach (var listenerType in actuator.ruleGroup)//遍历监听类型
            {
                //获取监听器对象池
                if (Root.NodePoolManager.pools.TryGetValue(listenerType.Key, out NodePool listenerPool))
                {
                    //遍历已存在的监听器
                    foreach (var listener in listenerPool.Nodes)
                    {
                        //判断目标是否被该监听器监听
                        if (listener.Value.listenerTarget != null)
                        {
                            if (listener.Value.listenerState == ListenerState.Node)
                            {
                                //判断是否全局监听 或 是指定的目标类型
                                if (listener.Value.listenerTarget == typeof(Node) || listener.Value.listenerTarget == Target)
                                {
                                    actuator.Enqueue(listener.Value);
                                }
                            }
                            else if (listener.Value.listenerState == ListenerState.Rule)
                            {
                                //判断的实体类型是否拥有目标系统
                                if (Root.RuleManager.TryGetRuleList(Target, listener.Value.listenerTarget, out _))
                                {
                                    actuator.Enqueue(listener.Value);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 尝试获取动态执行器
        /// </summary>
        public bool TryGetRuleActuator(Type Target, Type RuleType, out RuleActuator actuator)
        {
            if (ListenerActuatorGroupDictionary.TryGetValue(Target, out var group))
            {
                return group.TryGetRuleActuator(RuleType, out actuator);
            }
            else
            {
                actuator = null;
                return false;
            }
        }
        #endregion

        #region 获取执行器组

        /// <summary>
        /// 获取执行器集合
        /// </summary>
        public ListenerRuleActuatorGroup GetGroup(Type Target)
        {
            if (!ListenerActuatorGroupDictionary.TryGetValue(Target, out var group))
            {
                group = new ListenerRuleActuatorGroup();
                group.Target = Target;
                group.id = Root.IdManager.GetId();
                group.Root = Root;
                ListenerActuatorGroupDictionary.Add(Target, group);
                this.AddChildren(group);
            }
            return group;
        }

        /// <summary>
        /// 尝试获取执行器集合
        /// </summary>
        public bool TryGetGroup(Type target, out ListenerRuleActuatorGroup group)
        {
            return ListenerActuatorGroupDictionary.TryGetValue(target, out group);
        }

        #endregion
    }
}