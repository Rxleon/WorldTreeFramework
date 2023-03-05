﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2022/11/10 10:12

* 描述： 空异步任务构建器

*/

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace WorldTree.Internal
{
    /// <summary>
    /// 空异步任务构建器
    /// </summary>
    public struct TreeTaskVoidMethodBuilder
    {
        // 1. Static Create method.
        [DebuggerHidden]
        public static TreeTaskVoidMethodBuilder Create()
        {
            TreeTaskVoidMethodBuilder builder = new TreeTaskVoidMethodBuilder();
            return builder;
        }

        // 2. TaskLike Task property(void)
        [DebuggerHidden]
        public TreeTaskVoid Task => default;

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception e)
        {
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult()
        {
            // do nothing
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.UnsafeOnCompleted(stateMachine.MoveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }


    }
}