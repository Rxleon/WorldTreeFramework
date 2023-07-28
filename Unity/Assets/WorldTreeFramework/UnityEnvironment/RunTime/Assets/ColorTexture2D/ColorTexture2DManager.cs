﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2022/8/29 13:04

* 描述： 颜色图片管理器
* 
* 通过颜色创建一个对应的1像素大小的Texture2D贴图

*/

using UnityEngine;

namespace WorldTree
{

    public static partial class NodeRule
    {
        /// <summary>
        /// 获取颜色图片
        /// </summary>
        public static Texture2D GetColorTexture(this INode self, Color color)
        {
            return self.Root.AddComponent(out ColorTexture2DManager _).Get(color);
        }

        /// <summary>
        /// 获取黑白图片
        /// </summary>
        public static Texture2D GetColorTexture(this INode self, float color, float alpha = 1)
        {
            return self.Root.AddComponent(out ColorTexture2DManager _).Get(new Color(color, color, color, alpha));
        }
    }

    /// <summary>
    /// 颜色图片管理器
    /// </summary>
    public class ColorTexture2DManager : Node, ComponentOf<WorldTreeRoot>
        , AsRule<IAwakeRule>
    {
        UnitDictionary<Color, Texture2D> colors = new UnitDictionary<Color, Texture2D>();
        public Texture2D Get(Color color)
        {
            Texture2D texture = null;
            if (!colors.TryGetValue(color, out texture))
            {
                texture = new Texture2D(1, 1, Texture2D.grayTexture.format, true);
                texture.SetPixel(0, 0, color);

                colors.Add(color, texture);
                texture.Apply();
            }
            return texture;
        }
    }
}