﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2022/11/23 10:31

* 描述： 字典扩展方法静态类

*/

using System.Collections.Generic;

namespace WorldTree
{
    public static class DictionaryExtension
    {
        /// <summary>
        /// 获取或新建值（未通过对象池）
        /// </summary>
        public static V GetOrNewValue<K, V>(this Dictionary<K, V> self, K key)
            where V : class, new()
        {

            if (!self.TryGetValue(key, out V value))
            {
                value = new V();
                self.Add(key, value);
            }
            return value;
        }
    }
}
