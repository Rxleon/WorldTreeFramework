﻿using UnityEngine;

namespace WorldTree
{
	public class SphereManagerTest : Node
		, ComponentOf<InitialDomain>
		, AsIdNodeBranch
		, AsAwake
	{
		public int G = 10;
		public int bottomY = 0;
		public int topY = 10;
		public int spawnCount = 7000;
		public GameObject balls;
	}
}