﻿namespace WorldTree
{

    /// <summary>
    /// OnGUI法则接口
    /// </summary>
    public interface IGuiUpdateRule : ISendRuleBase<float> { }

    /// <summary>
    /// OnGUI法则
    /// </summary>
    public abstract class GuiUpdateRule<T> : SendRuleBase<T, IGuiUpdateRule, float> where T : class, INode, AsRule<IGuiUpdateRule> { }
}
