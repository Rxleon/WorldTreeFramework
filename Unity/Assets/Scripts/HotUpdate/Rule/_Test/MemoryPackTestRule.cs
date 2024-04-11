﻿using System.Collections.Generic;
using MemoryPack;

namespace WorldTree
{
	public static class MemoryPackTestRule
	{
		class AddRule : AddRule<MemoryPackTest>
		{
			protected override void Execute(MemoryPackTest self)
			{
				self.data = new MemoryPackDataTest<string> { t = "ASDF", Age = 60, Name = 654321L, ints = new List<int>() { 7, 8, 9 } };

				List<int> ints = self.data.ints;
				MemoryPackDataTest<string> v = new MemoryPackDataTest<string> { t = "ASDF", Age = 40, Name = 123456L, ints = new List<int>() { 1, 3, 4 } };
				byte[] bin = MemoryPackSerializer.Serialize(v);
				MemoryPackSerializer.Deserialize(bin, ref self.data);
				self.Log($"{self.data.t} : {self.data.Age} : {self.data.Name}:{ints[1]} :byte {bin.Length}");
			}
		}

		class UpdateRule : UpdateRule<MemoryPackTest>
		{
			protected override void Execute(MemoryPackTest self)
			{
			}
		}
	}
}