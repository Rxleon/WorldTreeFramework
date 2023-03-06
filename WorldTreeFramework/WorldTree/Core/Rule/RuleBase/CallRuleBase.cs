﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2022/11/10 10:12

* 描述： 调用法则基类
* 
* 可以理解为Entity的有返回值扩展方法
* 
* 
* ICallRule 继承 IRule
* 主要作用：统一 调用方法  OutT Invoke(Node self,T1 ar1, ...);
* 
* 
* CallRuleBase 则继承 RuleBase 
* 同时还继承了 ICallRule 可以转换为 ICallRule 进行统一调用。
* 
* 主要作用：确定Entity的类型并转换，并统一 Invoke 中转调用 OnEvent 的过程。
* 其中 Invoke 设定为虚方法方便子类写特殊的中转调用。
* 
*/

namespace WorldTree
{
    /// <summary>
    /// 调用法则接口
    /// </summary>
    public interface ICallRule<OutT> : IRule
    {
        OutT Invoke(Node self);
    }
    /// <summary>
    /// 调用法则接口
    /// </summary>
    public interface ICallRule<T1, OutT> : IRule
    {
        OutT Invoke(Node self, T1 arg1);
    }
    /// <summary>
    /// 调用法则接口
    /// </summary>
    public interface ICallRule<T1, T2, OutT> : IRule
    {
        OutT Invoke(Node self, T1 arg1, T2 arg2);
    }
    /// <summary>
    /// 调用法则接口
    /// </summary>
    public interface ICallRule<T1, T2, T3, OutT> : IRule
    {
        OutT Invoke(Node self, T1 arg1, T2 arg2, T3 arg3);
    }
    /// <summary>
    /// 调用法则接口
    /// </summary>
    public interface ICallRule<T1, T2, T3, T4, OutT> : IRule
    {
        OutT Invoke(Node self, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }
    /// <summary>
    /// 调用法则接口
    /// </summary>
    public interface ICallRule<T1, T2, T3, T4, T5, OutT> : IRule
    {
        OutT Invoke(Node self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }


    /// <summary>
    /// 调用法则抽象基类
    /// </summary>
    public abstract class CallRuleBase<S, E, OutT> : RuleBase<E, S>, ICallRule<OutT>
    where E : Node
    where S : ICallRule<OutT>
    {
        public virtual OutT Invoke(Node self) => OnEvent(self as E);
        public abstract OutT OnEvent(E self);
    }
    /// <summary>
    /// 调用法则抽象基类
    /// </summary>
    public abstract class CallRuleBase<S, E, T1, OutT> : RuleBase<E, S>, ICallRule<T1, OutT>
    where E : Node
    where S : ICallRule<T1, OutT>
    {
        public virtual OutT Invoke(Node self, T1 arg1) => OnEvent(self as E, arg1);
        public abstract OutT OnEvent(E self, T1 arg1);
    }
    /// <summary>
    /// 调用法则抽象基类
    /// </summary>
    public abstract class CallRuleBase<S, E, T1, T2, OutT> : RuleBase<E, S>, ICallRule<T1, T2, OutT>
    where E : Node
    where S : ICallRule<T1, T2, OutT>
    {
        public virtual OutT Invoke(Node self, T1 arg1, T2 arg2) => OnEvent(self as E, arg1, arg2);
        public abstract OutT OnEvent(E self, T1 arg1, T2 arg2);
    }
    /// <summary>
    /// 调用法则抽象基类
    /// </summary>
    public abstract class CallRuleBase<S, E, T1, T2, T3, OutT> : RuleBase<E, S>, ICallRule<T1, T2, T3, OutT>
    where E : Node
    where S : ICallRule<T1, T2, T3, OutT>
    {
        public virtual OutT Invoke(Node self, T1 arg1, T2 arg2, T3 arg3) => OnEvent(self as E, arg1, arg2, arg3);
        public abstract OutT OnEvent(E self, T1 arg1, T2 arg2, T3 arg3);
    }
    /// <summary>
    /// 调用法则抽象基类
    /// </summary>
    public abstract class CallRuleBase<S, E, T1, T2, T3, T4, OutT> : RuleBase<E, S>, ICallRule<T1, T2, T3, T4, OutT>
    where E : Node
    where S : ICallRule<T1, T2, T3, T4, OutT>
    {
        public virtual OutT Invoke(Node self, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => OnEvent(self as E, arg1, arg2, arg3, arg4);
        public abstract OutT OnEvent(E self, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }
    /// <summary>
    /// 调用法则抽象基类
    /// </summary>
    public abstract class CallRuleBase<S, E, T1, T2, T3, T4, T5, OutT> : RuleBase<E, S>, ICallRule<T1, T2, T3, T4, T5, OutT>
    where E : Node
    where S : ICallRule<T1, T2, T3, T4, T5, OutT>
    {
        public virtual OutT Invoke(Node self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => OnEvent(self as E, arg1, arg2, arg3, arg4, arg5);
        public abstract OutT OnEvent(E self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}