/****************************************

* 作者：闪电黑客
* 日期：2024/10/23 18:27

* 描述：

*/


using System.Collections.Generic;

namespace WorldTree.TreeDataFormatters
{
	public static class KeyValuePairFormatterRule
	{
		class Serialize<TKey, TValue> : TreeDataSerializeRule<KeyValuePair<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object value, ref SerializedTypeMode typeMode)
			{
				if (self.TryWriteDataHead(value, typeMode, 2, out KeyValuePair<TKey, TValue> obj)) return;
				self.WriteUnmanaged(-853882612);
				self.WriteValue(obj.Key);
				self.WriteUnmanaged(-783812246);
				self.WriteValue(obj.Value);
			}
		}

		class Deserialize<TKey, TValue> : TreeDataDeserializeRule<KeyValuePair<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object value, ref int fieldNameCode)
			{
				if (self.TryReadClassHead(typeof(KeyValuePair<TKey, TValue>), ref value, out int count)) return;

				TKey key = default;
				TValue val = default;
				for (int i = 0; i < count; i++)
				{
					self.ReadUnmanaged(out fieldNameCode);

					switch (fieldNameCode)
					{
						case -853882612: key = self.ReadValue<TKey>(); break;
						case -783812246: val = self.ReadValue<TValue>(); break;
						default: self.SkipData(); break;
					}
				}
				value = new KeyValuePair<TKey, TValue>(key, val);
			}
		}
	}
}