﻿/****************************************

* 作者： 闪电黑客
* 日期： 2023/8/9 15:42

* 描述： 默认类型: 直接使用default会造成反射创建和产生GC，所以存起来使用
* 
*  default(xxx)和(xxx)null 都有转换消耗

*/

namespace WorldTree
{
    /// <summary>
    /// 默认类型缓存：代替default(xxx)和(xxx)null
    /// </summary>
    /// <remarks>直接使用default会造成反射创建和转换消耗，所以存起来使用</remarks>
    public static class DefaultType<T>
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public static readonly T Default = default;
    }



}