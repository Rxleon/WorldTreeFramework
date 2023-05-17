﻿/****************************************

* 作者： 闪电黑客
* 日期： 2023/2/28 10:31

* 描述： 节点法则调用执行

*/

namespace WorldTree
{
    public static class NodeCallRule
    {
        #region Call

        public static bool TryCallRule<R, OutT>(this INode self, R defaultRule, out OutT outT)
        where R : ICallRule<OutT>
        {

            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCall(self, out outT);
            }
            else
            {
                outT = default(OutT);
                return false;
            }

        }
        public static bool TryCallRule<R, T1, OutT>(this INode self, R defaultRule, T1 arg1, out OutT outT)
        where R : ICallRule<T1, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCall(self, arg1, out outT);
            }
            else
            {
                outT = default(OutT);
                return false;
            }
        }
        public static bool TryCallRule<R, T1, T2, OutT>(this INode self, R defaultRule, T1 arg1, T2 arg2, out OutT outT)
        where R : ICallRule<T1, T2, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCall(self, arg1, arg2, out outT);
            }
            else
            {
                outT = default(OutT);
                return false;
            }
        }
        public static bool TryCallRule<R, T1, T2, T3, OutT>(this INode self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, out OutT outT)
        where R : ICallRule<T1, T2, T3, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCall(self, arg1, arg2, arg3, out outT);
            }
            else
            {
                outT = default(OutT);
                return false;
            }
        }
        public static bool TryCallRule<R, T1, T2, T3, T4, OutT>(this INode self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, T4 arg4, out OutT outT)
        where R : ICallRule<T1, T2, T3, T4, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCall(self, arg1, arg2, arg3, arg4, out outT);
            }
            else
            {
                outT = default(OutT);
                return false;
            }
        }
        public static bool TryCallRule<R, T1, T2, T3, T4, T5, OutT>(this INode self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, out OutT outT)
        where R : ICallRule<T1, T2, T3, T4, T5, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCall(self, arg1, arg2, arg3, arg4, arg5, out outT);
            }
            else
            {
                outT = default(OutT);
                return false;
            }
        }



        public static OutT CallRule<N, R, OutT>(this N self, R defaultRule, out OutT outT)
            where N : class, INode, AsRule<R>
            where R : ICallRule<OutT>
        {
            self.TryCallRule(defaultRule, out outT);
            return outT;
        }
        public static OutT CallRule<N, R, T1, OutT>(this N self, R defaultRule, T1 arg1, out OutT outT)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, OutT>
        {
            self.TryCallRule(defaultRule, arg1, out outT);
            return outT;
        }
        public static OutT CallRule<N, R, T1, T2, OutT>(this N self, R defaultRule, T1 arg1, T2 arg2, out OutT outT)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, T2, OutT>
        {
            self.TryCallRule(defaultRule, arg1, arg2, out outT);
            return outT;
        }
        public static OutT CallRule<N, R, T1, T2, T3, OutT>(this N self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, out OutT outT)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, T2, T3, OutT>
        {
            self.TryCallRule(defaultRule, arg1, arg2, arg3, out outT);
            return outT;
        }
        public static OutT CallRule<N, R, T1, T2, T3, T4, OutT>(this N self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, T4 arg4, out OutT outT)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, T2, T3, T4, OutT>
        {
            self.TryCallRule(defaultRule, arg1, arg2, arg3, arg4, out outT);
            return outT;
        }
        public static OutT CallRule<N, R, T1, T2, T3, T4, T5, OutT>(this N self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, out OutT outT)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, T2, T3, T4, T5, OutT>
        {
            self.TryCallRule(defaultRule, arg1, arg2, arg3, arg4, arg5, out outT);
            return outT;
        }
        #endregion


        #region Calls
        public static bool TryCallsRule<R, OutT>(this INode self, R defaultRule, out UnitList<OutT> values)
        where R : ICallRule<OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCalls(self, out values);
            }
            else
            {
                values = null;
                return false;
            }
        }
        public static bool TryCallsRule<R, T1, OutT>(this INode self, R defaultRule, T1 arg1, out UnitList<OutT> values)
        where R : ICallRule<T1, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCalls(self, arg1, out values);
            }
            else
            {
                values = null;
                return false;
            }
        }
        public static bool TryCallsRule<R, T1, T2, OutT>(this INode self, R defaultRule, T1 arg1, T2 arg2, out UnitList<OutT> values)
        where R : ICallRule<T1, T2, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCalls(self, arg1, arg2, out values);
            }
            else
            {
                values = null;
                return false;
            }
        }
        public static bool TryCallsRule<R, T1, T2, T3, OutT>(this INode self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, out UnitList<OutT> values)
        where R : ICallRule<T1, T2, T3, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCalls(self, arg1, arg2, arg3, out values);
            }
            else
            {
                values = null;
                return false;
            }
        }
        public static bool TryCallsRule<R, T1, T2, T3, T4, OutT>(this INode self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, T4 arg4, out UnitList<OutT> values)
        where R : ICallRule<T1, T2, T3, T4, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCalls(self, arg1, arg2, arg3, arg4, out values);
            }
            else
            {
                values = null;
                return false;
            }
        }
        public static bool TryCallsRule<R, T1, T2, T3, T4, T5, OutT>(this INode self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, out UnitList<OutT> values)
        where R : ICallRule<T1, T2, T3, T4, T5, OutT>
        {
            if (self.Core.RuleManager.TryGetRuleGroup(out IRuleGroup<R> group))
            {
                return group.TryCalls(self, arg1, arg2, arg3, arg4, arg5, out values);
            }
            else
            {
                values = null;
                return false;
            }
        }



        public static UnitList<OutT> CallsRule<N, R, OutT>(this N self, R defaultRule, out UnitList<OutT> values)
            where N : class, INode, AsRule<R>
            where R : ICallRule<OutT>
        {
            self.TryCallsRule(defaultRule, out values);
            return values;
        }
        public static UnitList<OutT> CallsRule<N, R, T1, OutT>(this N self, R defaultRule, T1 arg1, out UnitList<OutT> values)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, OutT>
        {
            self.TryCallsRule(defaultRule, arg1, out values);
            return values;
        }
        public static UnitList<OutT> CallsRule<N, R, T1, T2, OutT>(this N self, R defaultRule, T1 arg1, T2 arg2, out UnitList<OutT> values)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, T2, OutT>
        {
            self.TryCallsRule(defaultRule, arg1, arg2, out values);
            return values;
        }
        public static UnitList<OutT> CallsRule<N, R, T1, T2, T3, OutT>(this N self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, out UnitList<OutT> values)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, T2, T3, OutT>
        {
            self.TryCallsRule(defaultRule, arg1, arg2, arg3, out values);
            return values;
        }
        public static UnitList<OutT> CallsRule<N, R, T1, T2, T3, T4, OutT>(this N self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, T4 arg4, out UnitList<OutT> values)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, T2, T3, T4, OutT>
        {
            self.TryCallsRule(defaultRule, arg1, arg2, arg3, arg4, out values);
            return values;
        }
        public static UnitList<OutT> CallsRule<N, R, T1, T2, T3, T4, T5, OutT>(this N self, R defaultRule, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, out UnitList<OutT> values)
            where N : class, INode, AsRule<R>
            where R : ICallRule<T1, T2, T3, T4, T5, OutT>
        {
            self.TryCallsRule(defaultRule, arg1, arg2, arg3, arg4, arg5, out values);
            return values;
        }
        #endregion

    }
}