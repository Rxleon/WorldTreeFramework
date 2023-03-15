﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2023/2/9 14:34

* 描述： 世界树异步任务基类

*/

using System;
using System.Runtime.CompilerServices;

namespace WorldTree
{
    /// <summary>
    /// 树异步任务基类
    /// </summary>
    public abstract class TreeTaskBase : Node, ICriticalNotifyCompletion
    {
        /// <summary>
        /// 是否完成
        /// </summary>
        public abstract bool IsCompleted { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 延续
        /// </summary>
        public Action continuation;

        /// <summary>
        /// 设置完成
        /// </summary>
        protected void SetCompleted()
        {
            IsCompleted = true;
            if (IsActive)
            {
                continuation?.Invoke();
                Dispose();
            }
            else
            {
                this.AddComponent(out TreeTaskContinue _);
            }
        }

        /// <summary>
        /// 设置异常
        /// </summary>
        public void SetException(Exception exception)
        {
            this.Exception = exception;
        }

        /// <summary>
        /// 不安全完成时
        /// </summary>
        public void UnsafeOnCompleted(Action continuation)
        {
            this.continuation = continuation;
        }
        /// <summary>
        /// 完成时
        /// </summary>
        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        /// <summary>
        /// 继续
        /// </summary>
        public void Continue()
        {
            if (IsCompleted)
            {
                continuation?.Invoke();
                Dispose();
            }
        }

        public override void OnDispose()
        {
            IsCompleted = false;
            continuation = null;

            base.OnDispose();
        }
    }
}
