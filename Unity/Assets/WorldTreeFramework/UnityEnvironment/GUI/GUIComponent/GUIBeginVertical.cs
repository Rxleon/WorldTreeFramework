﻿using UnityEngine;

namespace WorldTree
{
    public class GUIBeginVertical : GUIBase
    {
        public void Draw()
        {
            GUILayout.BeginVertical(Style, options);
        }
    }

    class GUIBeginVerticalAddSystem : AddRule<GUIBeginVertical>
    {
        public override void OnEvent(GUIBeginVertical self)
        {
            self.Texture = self.GetColorTexture(0.1f);

            self.Padding = new RectOffset(5, 5, 5, 5);
        }
    }

    class GUIBeginVerticalRecycleSystem : RecycleRule<GUIBeginVertical>
    {
        public override void OnEvent(GUIBeginVertical self)
        {
            //self.PoolRecycle(self.style);
            self.style = null;
        }
    }
}