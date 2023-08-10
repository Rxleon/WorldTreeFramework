﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2023/8/10 10:33

* 描述： 单位排序字典，这个字典可由对象池管理生成和回收
* 
* 其余和普通的排序字典一样使用

*/

using System.Collections.Generic;

namespace WorldTree
{
    /// <summary>
    /// 单位排序字典：可由对象池管理回收
    /// </summary>
    public class UnitSortedDictionary<TKey, TValue> : SortedDictionary<TKey, TValue>, IUnitPoolEventItem
    {
        public WorldTreeCore Core { get; set; }
        public bool IsFromPool { get; set; }
        public bool IsRecycle { get; set; }
        public bool IsDisposed { get; set; }

        public void OnDispose()
        {
        }

        public void OnGet()
        {
        }

        public void OnNew()
        {
        }

        public void OnRecycle()
        {
            Clear();
        }
        public void Dispose()
        {
            Core?.Recycle(this);
        }
    }
}
