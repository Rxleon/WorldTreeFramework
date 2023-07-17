﻿/****************************************

* 作者： 闪电黑客
* 日期： 2023/7/17 11:41

* 描述： 

*/

using System;

namespace WorldTree
{
    public static partial class TreeValueRule
    {
        class GenericsValueChangeRule<T> : ValueChangeRule<T>
            where T : IEquatable<T>
        {
            public override void OnEvent(TreeValueBase<T> self, T arg1)
            {
                self.Value = arg1;
            }
        }
    }
}
