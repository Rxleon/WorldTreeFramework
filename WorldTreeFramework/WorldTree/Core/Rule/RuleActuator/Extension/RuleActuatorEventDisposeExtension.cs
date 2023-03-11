﻿/****************************************

* 作者： 闪电黑客
* 日期： 2022/9/18 16:55

* 描述： 广播发送

*/

namespace WorldTree
{
    public partial class RuleActuator
    {

        public void SendDispose()
        {
            if (IsActive)
            {
                int length = nodeQueue.Count;
                for (int i = 0; i < length; i++)
                {
                    if (nodeQueue.TryDequeue(out INode node))
                    {
                        ruleGroup.Send(node);
                    }
                }
                Dispose();
            }
        }

        public void SendDispose<T1>(T1 arg1)
        {
            if (IsActive)
            {
                int length = nodeQueue.Count;
                for (int i = 0; i < length; i++)
                {
                    if (nodeQueue.TryDequeue(out INode node))
                    {
                        ruleGroup.Send(node, arg1);
                    }
                }
                Dispose();
            }
        }


        public void SendDispose<T1, T2>(T1 arg1, T2 arg2)
        {
            if (IsActive)
            {
                int length = nodeQueue.Count;
                for (int i = 0; i < length; i++)
                {
                    if (nodeQueue.TryDequeue(out INode node))
                    {
                        ruleGroup.Send(node, arg1, arg2);
                    }
                }
                Dispose();
            }
        }
        public void SendDispose<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            if (IsActive)
            {
                int length = nodeQueue.Count;
                for (int i = 0; i < length; i++)
                {
                    if (nodeQueue.TryDequeue(out INode node))
                    {
                        ruleGroup.Send(node, arg1, arg2, arg3);
                    }
                }
                Dispose();
            }
        }
        public void SendDispose<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (IsActive)
            {
                int length = nodeQueue.Count;
                for (int i = 0; i < length; i++)
                {
                    if (nodeQueue.TryDequeue(out INode node))
                    {
                        ruleGroup.Send(node, arg1, arg2, arg3, arg4);
                    }
                }
                Dispose();
            }
        }
        public void SendDispose<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (IsActive)
            {
                int length = nodeQueue.Count;
                for (int i = 0; i < length; i++)
                {
                    if (nodeQueue.TryDequeue(out INode node))
                    {
                        ruleGroup.Send(node, arg1, arg2, arg3, arg4, arg5);
                    }
                }
                Dispose();
            }
        }


    }
}
