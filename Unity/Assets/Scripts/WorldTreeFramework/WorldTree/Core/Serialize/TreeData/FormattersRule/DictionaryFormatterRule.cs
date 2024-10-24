using System.Collections.Generic;
using System;

namespace WorldTree.TreeDataFormatters
{
	public static class DictionaryFormatterRule
	{
		private class Serialize<TKey, TValue> : TreeDataSerializeRule<Dictionary<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object obj)
			{

				Dictionary<TKey, TValue> dataDict = (Dictionary<TKey, TValue>)obj;
				self.WriteType(typeof(Dictionary<TKey, TValue>));
				if (dataDict == null)
				{
					self.WriteUnmanaged((long)ValueMarkCode.NULL_OBJECT);
					return;
				}
				//д������ά������
				self.WriteUnmanaged(~1);
				self.WriteUnmanaged(dataDict.Count);
				if (dataDict.Count == 0) return;

				//д����������
				foreach (var item in dataDict)
				{
					self.WriteValue(item);
				}
			}
		}

		private class Deserialize<TKey, TValue> : TreeDataDeserializeRule<Dictionary<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object obj)
			{
				if (!(self.TryReadType(out Type dataType) && dataType == typeof(Dictionary<TKey, TValue>)))
				{
					//��Ծ����,�����ȡ����
					self.SkipData(dataType);

					return;
				}
				//��ȡ����ά������
				self.ReadUnmanaged(out int count);
				if (count == ValueMarkCode.NULL_OBJECT)
				{
					obj = null;
					return;
				}
				count = ~count;
				if (count != 1)
				{
					//��ȡָ�����
					self.ReadBack(4);
					self.SkipData(dataType);
					return;
				}

				self.ReadUnmanaged(out int length);

				//��������Ϊ�ջ򳤶Ȳ�һ��
				Dictionary<TKey, TValue> dataDict = obj as Dictionary<TKey, TValue>;
				if (dataDict == null)
				{
					obj = dataDict = new();
				}
				else if (dataDict.Count != 0)
				{
					dataDict.Clear();
				}

				//���ݳ���Ϊ0��ֱ�ӷ���
				if (length == 0) return;

				KeyValuePair<TKey, TValue> keyValuePair;
				//��ȡ��������
				for (int i = 0; i < length; i++)
				{
					keyValuePair = self.ReadValue<KeyValuePair<TKey, TValue>>();
					dataDict.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}
	}
}