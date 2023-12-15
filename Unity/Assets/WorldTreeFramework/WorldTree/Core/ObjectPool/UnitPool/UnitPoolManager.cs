﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2022/11/11 11:05

* 描述： 单位对象池管理器

*/

using System;

namespace WorldTree
{
    /// <summary>
    /// 单位对象池管理器
    /// </summary>
    public class UnitPoolManager : PoolManagerBase<UnitPool>,ComponentOf<WorldTreeCore>
        , AsRule<IAwakeRule>
    {
        /// <summary>
        /// 尝试获取单位
        /// </summary>
        public bool TryGet(long type, out IUnitPoolEventItem unit)
        {
            if (TryGet(type, out object obj))
            {
                unit = obj as IUnitPoolEventItem;
                return true;
            }
            else
            {
                unit = null;
                return false;
            }
        }
    }
}
