﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldTree;

public class Node : Entity
{

}

class NodeUpdateSystem : UpdateSystem<Node>
{
    public override void Update(Node self, float deltaTime)
    {
        Debug.Log("Update");
    }
}




//public interface IAddSendSystem :ISendSystem<float>{ }
public interface INodeAddSendSystem : ISendSystem<int> { }

public abstract class NodeAddSendSystem1<T> : SystemBase<T, INodeAddSendSystem>, INodeAddSendSystem
    where T : Entity
{
    public void Invoke(Entity self, int arg1) => Event1(self as T, arg1);
    public abstract void Event1(T self, int arg1);
}


public class NodeAddSendSystem2 : NodeAddSendSystem1<Node>
{
    public override void Event1(Node self, int arg1)
    {
        Debug.Log("!!!!!!!!:"+ arg1);
    }
}

public class NodeAddSendSystem3 : SendSystem<Node,int>
{
    public override void Event(Node self, int arg1)
    {
        Debug.Log("?????????:" + arg1);
    }
}
