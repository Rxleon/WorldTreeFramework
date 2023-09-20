﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2023/9/11 20:46

* 描述： 

*/

using System;

namespace WorldTree
{
    public static partial class WorldTreeCoreRule
    {
        /// <summary>
        /// 获取数组对象
        /// </summary>
        public static T[] GetArray<T>(this WorldTreeCore self, out T[] outT, int Length)
        {
            Type type = typeof(T);
            if (self.ArrayPoolManager != null)
            {
                outT = self.ArrayPoolManager.Get(type, Length) as T[];
            }
            else
            {
                outT = Array.CreateInstance(type, Length) as T[];
            }
            return outT;
        }

        /// <summary>
        /// 获取数组对象
        /// </summary>
        public static T[] GetArray<T>(this WorldTreeCore self, int Length)
        {
            Type type = typeof(T);
            if (self.ArrayPoolManager != null)
            {
                return self.ArrayPoolManager.Get(type, Length) as T[];
            }
            return Array.CreateInstance(type, Length) as T[];
        }

        /// <summary>
        /// 回收数组
        /// </summary>
        public static void Recycle(this WorldTreeCore self, Array obj)
        {
            if (self.ArrayPoolManager != null)
            {
                self.ArrayPoolManager.Recycle(obj);
            }
            else
            {
                Array.Clear(obj, 0, obj.Length);
            }
        }

    }
}
