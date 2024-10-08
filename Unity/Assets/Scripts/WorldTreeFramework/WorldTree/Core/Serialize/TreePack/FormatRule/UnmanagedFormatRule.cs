﻿using System.Runtime.CompilerServices;

namespace WorldTree.TreePackFormats
{
	public static class UnmanagedFormatRule
	{
		class Serialize<T> : TreePackSerializeUnmanagedRule<TreePackByteSequence, T>
		{
			protected override void Execute(TreePackByteSequence self, ref T value)
			{
				Unsafe.WriteUnaligned(ref self.GetWriteRefByte(Unsafe.SizeOf<T>()), value);
			}
		}
		class Deserialize<T> : TreePackDeserializeUnmanagedRule<TreePackByteSequence, T>
		{
			protected override void Execute(TreePackByteSequence self, ref T value)
			{
				value = Unsafe.ReadUnaligned<T>(ref self.GetReadRefByte(Unsafe.SizeOf<T>()));
			}
		}
	}
}
