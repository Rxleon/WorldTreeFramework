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
				self.data = new MemoryPackDataTest<string> { Test = "ASDF", Age = 60, Name = 654321L, IntList = new List<int>() { 7, 8, 9 } };

				List<int> intList = self.data.IntList;
				MemoryPackDataTest<string> v = new MemoryPackDataTest<string> { Test = "ASDF", Age = 40, Name = 123456L, IntList = new List<int>() { 1, 3, 4 } };
				byte[] bins = MemoryPackSerializer.Serialize(v);
				MemoryPackSerializer.Deserialize(bins, ref self.data);
				self.Log($"{self.data.Test} : {self.data.Age} : {self.data.Name}:{intList[1]} :byte {bins.Length}");
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
