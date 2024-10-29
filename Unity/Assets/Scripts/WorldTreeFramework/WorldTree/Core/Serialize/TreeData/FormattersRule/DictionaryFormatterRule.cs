using System.Collections.Generic;
using System;
using System.Collections;

namespace WorldTree.TreeDataFormatters
{
	public static class DictionaryFormatterRule
	{
		private class Serialize<TKey, TValue> : TreeDataSerializeRule<Dictionary<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object value)
			{
				Dictionary<TKey, TValue> obj = (Dictionary<TKey, TValue>)value;

				self.WriteType(typeof(Dictionary<TKey, TValue>));
				if (obj == null)
				{
					self.WriteUnmanaged((long)ValueMarkCode.NULL_OBJECT);
					return;
				}
				//д������ά������
				self.WriteUnmanaged(~1);
				self.WriteUnmanaged(obj.Count);
				if (obj.Count == 0) return;

				//д����������
				foreach (var item in obj)
				{
					self.WriteValue(item);
				}
			}
		}

		private class Deserialize<TKey, TValue> : TreeDataDeserializeRule<Dictionary<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object value)
			{
				var targetType = typeof(Dictionary<TKey, TValue>);
				if (!(self.TryReadType(out Type dataType) && dataType == typeof(Dictionary<TKey, TValue>)))
				{
					//��Ծ����,�����ȡ����
					self.SubTypeReadValue(dataType, targetType, ref value);
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
					self.SkipData(dataType);
					return;
				}

				self.ReadUnmanaged(out int length);

				//��������Ϊ�ջ򳤶Ȳ�һ��
				Dictionary<TKey, TValue> obj = value as Dictionary<TKey, TValue>;
				if (obj == null)
				{
					value = obj = new();
				}
				else if (obj.Count != 0)
				{
					obj.Clear();
				}

				//���ݳ���Ϊ0��ֱ�ӷ���
				if (length == 0) return;

				KeyValuePair<TKey, TValue> keyValuePair;
				//��ȡ��������
				for (int i = 0; i < length; i++)
				{
					keyValuePair = self.ReadValue<KeyValuePair<TKey, TValue>>();
					obj.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}
	}
}