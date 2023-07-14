﻿using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using WorldTree;





public class TreeNode : Node
    , ComponentOf<INode>
    , AsRule<IFixedUpdateRule>
    , AsRule<ILateUpdateRule>
{

    public static bool bit = true;
    public async TreeTask Test()
    {
        await this.AsyncDelay(2);

        World.Log("1！");
        await this.TreeTaskCompleted();
        World.Log("2！");

        await this.TreeTaskCompleted();
        World.Log("3！");

        await this.TreeTaskCompleted();
        World.Log("4！");

        await this.TreeTaskCompleted();
        World.Log("5！");

        await this.TreeTaskCompleted();
        await T2();
    }

    public async TreeTask T2()
    {
        await this.TreeTaskCompleted();
        World.Log("T2 1！");
        await this.TreeTaskCompleted();
    }
    public async TreeTask T3()
    {
        World.Log("T3 1！");

        await this.AsyncDelay(3);

        World.Log("T3 2！");

        await this.TreeTaskCompleted();

        World.Log(await T4());

        await T5();

    }

    public async TreeTask<int> T4()
    {
        World.Log("T4 1！");

        await this.AsyncDelay(3);
        World.Log("T4 2！");

        await this.TreeTaskCompleted();

        return 10021;
    }

    public async TreeTask T5()
    {
        await this.TreeTaskCompleted();
    }


    //}
    //class NodeNewSystem : NewRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self)
    //    {
    //        Debug.Log("NewSystem!");
    //    }
    //}
    //class NodeGetSystem : GetRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self)
    //    {
    //        Debug.Log("GetSystem!");
    //    }
    //}
    //class NodeAddSystem : AddRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self)
    //    {
    //        Debug.Log("AddSystem!");
    //    }
    //}
    //class NodeEnableSystem : EnableRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self)
    //    {
    //        Debug.Log("EnableSystem!");
    //    }
    //}

    //class NodeDisableSystem : DisableRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self)
    //    {
    //        Debug.Log("DisableSystem!");
    //    }
    //}

    class NodeUpdateSystem : UpdateRule<TreeNode>
    {
        public override void OnEvent(TreeNode self, float deltaTime)
        {
            //Debug.Log("UpdateSystem!");
        }
    }


    //class NodeLateUpdateSystem : LateUpdateRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self, float deltaTime)
    //    {
    //        Debug.Log("LateUpdateSystem!");
    //    }
    //}
    //class NodeFixedUpdateSystem : FixedUpdateRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self, float deltaTime)
    //    {
    //        Debug.Log("FixedUpdateSystem!");
    //    }
    //}

    //class NodeRemoveSystem : RemoveRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self)
    //    {
    //        Debug.Log("RemoveSystem!");
    //    }
    //}
    //class NodeRecycleSystem : RecycleRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self)
    //    {
    //        Debug.Log("RecycleSystem!");
    //    }
    //}
    //class NodeDestroySystem : DestroyRule<TreeNode>
    //{
    //    public override void OnEvent(TreeNode self)
    //    {
    //        Debug.Log("DestroySystem!");
    //    }
    //}


}