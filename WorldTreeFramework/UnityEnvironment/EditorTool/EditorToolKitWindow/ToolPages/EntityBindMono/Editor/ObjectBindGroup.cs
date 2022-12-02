﻿
/****************************************

* 作者： 闪电黑客
* 日期： 2022/11/29 14:15

* 描述： 对象绑定组

*/

using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WorldTree
{
    [Serializable]
    public class ObjectBindGroup
    {

        [HideInInspector]
        public EntityBindMonoTool monoBindEntityTool = null;

        [HideInInspector]
        public bool IsShow = true;

        [HideLabel, HorizontalGroup("A", width: 200)]
        public string groupName;

        [GUIColor(0, 1, 1)]
        [LabelText("拖拽添加对象")]
        [HorizontalGroup("A")]
        [ListDrawerSettings(Expanded = false, HideAddButton = true, ShowItemCount = false)]
        public List<MonoObject> addMonoObjects = new List<MonoObject>();

        [GUIColor(0, 1, 0)]
        [ShowIf("@objects.Count > 0 &&!IsShow")]
        [HorizontalGroup("A", width: 100)]
        [Button("对象列表", ButtonSizes.Medium)]
        public void FoldShow()
        {
            IsShow = true;
        }
        [GUIColor(1, 0, 0)]
        [ShowIf("@objects.Count > 0&&IsShow")]
        [HorizontalGroup("A", width: 100)]
        [Button("对象列表", ButtonSizes.Medium)]
        public void FoldHide()
        {
            IsShow = false;
        }


        [ShowIf("@objects.Count > 0&&IsShow")]
        [LabelText("对象列表")]
        [Searchable]
        [ListDrawerSettings(Expanded = true, HideAddButton = true)]
        public List<ObjectBindItem> objects = new List<ObjectBindItem>();



        public void UpdateRefresh()
        {
            DeleteNull();
            AddList();
        }

        //检测删除Null组件，同时提供Update刷新
        private void DeleteNull()
        {
            for (int i = 0; i < objects.Count;)
            {
                if (objects[i].monoObject != null)
                {
                    objects[i].UpdateRefresh();
                    i++;
                }
                else
                {
                    objects.RemoveAt(i);
                }
            }
        }

        //检测添加组件
        private void AddList()
        {
            if (addMonoObjects.Count > 0)
            {
                foreach (var monoObject in addMonoObjects)
                {
                    if (!monoBindEntityTool.groups.Any(item => item.objects.Any((item) => item.monoObject == monoObject || item.monoObject.name == monoObject.name)))
                    {
                        objects.Add(new ObjectBindItem() { monoObject = monoObject,objectBindGroup =this });
                    }
                    else
                    {
                        Debug.Log($"{monoObject.name} 已存在");
                    }
                }
                addMonoObjects.Clear();
            }
        }

    }



}
