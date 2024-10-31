using System.Collections.Generic;

namespace WorldTree.TreeDataFormatters
{

	public static class DictionaryFormatterRule
	{
		[TreeDataSpecial(1)]
		private class Serialize<TKey, TValue> : IEnumerableSpecialFormatterRule.SerializeBase<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>
		{
			public override void ForeachWrite(TreeDataByteSequence self, Dictionary<TKey, TValue> obj)
			{
				self.WriteUnmanaged(obj.Count);
				if (obj.Count == 0) return;
				foreach (var item in obj) self.WriteValue(item);
			}
		}

		private class Deserialize<TKey, TValue> : IEnumerableSpecialFormatterRule.DeserializeBase<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>
		{
			public override void ForeachRead(TreeDataByteSequence self, Dictionary<TKey, TValue> obj)
			{
				//���������ݣ����������
				if (obj.Count != 0) obj.Clear();

				self.ReadUnmanaged(out int length);
				//���ݳ���Ϊ0��ֱ�ӷ���
				if (length == 0) return;

				KeyValuePair<TKey, TValue> keyValuePair;
				//��ȡ��������
				for (int j = 0; j < length; j++)
				{
					keyValuePair = self.ReadValue<KeyValuePair<TKey, TValue>>();
					obj.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}
	}
}