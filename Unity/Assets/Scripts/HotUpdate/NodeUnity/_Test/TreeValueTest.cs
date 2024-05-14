﻿namespace WorldTree
{
	public class TreeValueTest : Node, ComponentOf<InitialDomain>
		, AsAwake
	{
		public TreeValue<float> valueFloat;
		public TreeValue<int> valueInt;

		public TreeValue<string> valueString;
		public TreeTween<string> treeTween;
	}
}
