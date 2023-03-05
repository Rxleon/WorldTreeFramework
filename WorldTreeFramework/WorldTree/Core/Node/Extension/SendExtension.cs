﻿/****************************************

* 作者： 闪电黑客
* 日期： 2023/2/28 10:45

* 描述： 节点法则通知执行

*/

namespace WorldTree
{
    public static class SendExtension
    {
        public static bool TrySend(this Node self) => self.TrySendRule<ISendRule>();
        public static bool TrySend<T1>(this Node self, T1 arg1) => self.TrySendRule<ISendRule<T1>, T1>(arg1);
        public static bool TrySend<T1, T2>(this Node self, T1 arg1, T2 arg2) => self.TrySendRule<ISendRule<T1, T2>, T1, T2>(arg1, arg2);
        public static bool TrySend<T1, T2, T3>(this Node self, T1 arg1, T2 arg2, T3 arg3) => self.TrySendRule<ISendRule<T1, T2, T3>, T1, T2, T3>(arg1, arg2, arg3);
        public static bool TrySend<T1, T2, T3, T4>(this Node self, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => self.TrySendRule<ISendRule<T1, T2, T3, T4>, T1, T2, T3, T4>(arg1, arg2, arg3, arg4);
        public static bool TrySend<T1, T2, T3, T4, T5>(this Node self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => self.TrySendRule<ISendRule<T1, T2, T3, T4, T5>, T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5);

        public static void Send(this Node self) => self.TrySend();
        public static void Send<T1>(this Node self, T1 arg1) => self.TrySend(arg1);
        public static void Send<T1,T2>(this Node self, T1 arg1, T2 arg2) => self.TrySend(arg1, arg2);
        public static void Send<T1, T2, T3>(this Node self, T1 arg1, T2 arg2, T3 arg3) => self.TrySend(arg1, arg2, arg3);
        public static void Send<T1, T2, T3, T4>(this Node self, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => self.TrySend(arg1, arg2, arg3, arg4);
        public static void Send<T1, T2, T3, T4, T5>(this Node self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => self.TrySend(arg1, arg2, arg3, arg4, arg5);

    }
}
