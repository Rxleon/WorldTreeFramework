using System.Collections.Generic;
using System;

namespace WorldTree.TreeDataFormatters
{
	public static class DictionaryFormatterRule
	{
		private class Serialize<TKey, TValue> : TreeDataSerializeRule<TreeDataByteSequence, Dictionary<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object value)
			{
				Dictionary<TKey, TValue> dataDict = (Dictionary<TKey, TValue>)value;
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

		private class Deserialize<TKey, TValue> : TreeDataDeserializeRule<TreeDataByteSequence, Dictionary<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object value)
			{
				if (!(self.TryReadType(out Type type) && type == typeof(Dictionary<TKey, TValue>)))
				{
					//��Ծ����
					self.SkipData(type);
					return;
				}
				//��ȡ����ά������
				self.ReadUnmanaged(out int count);
				if (count == ValueMarkCode.NULL_OBJECT)
				{
					value = null;
					return;
				}
				count = ~count;
				if (count != 1)
				{
					//��ȡָ�����
					self.ReadBack(4);
					self.SkipData(type);
					return;
				}

				self.ReadUnmanaged(out int length);

				//��������Ϊ�ջ򳤶Ȳ�һ��
				Dictionary<TKey, TValue> dataDict = value as Dictionary<TKey, TValue>;
				if (dataDict == null)
				{
					//����Type���䴴��
					value.GetType();

					value = dataDict = new Dictionary<TKey, TValue>(length);
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