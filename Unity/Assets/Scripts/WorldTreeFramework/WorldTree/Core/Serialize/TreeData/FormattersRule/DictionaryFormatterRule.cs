using System.Collections.Generic;
using System;
using System.Collections;

namespace WorldTree.TreeDataFormatters
{

	public static class DictionaryFormatterRule
	{
		[TreeDataSpecial(1)]
		private class Serialize<TKey, TValue> : TreeDataSerializeRule<Dictionary<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object value, ref int nameCode)
			{
				Dictionary<TKey, TValue> obj = (Dictionary<TKey, TValue>)value;
				if (nameCode == -1)
				{
					self.WriteType(typeof(Dictionary<TKey, TValue>));
					if (obj == null)
					{
						self.WriteUnmanaged((long)ValueMarkCode.NULL_OBJECT);
						return;
					}
					//д���ֶ�����
					self.WriteUnmanaged(1);
				}

				//�����ֵ���һ�������ֶ�
				if (!self.WriteCheckNameCode(1683726967)) self.AddNameCode(1683726967, nameof(self));

				//���л������ֶ�
				self.WriteType(typeof(KeyValuePair<TKey, TValue>[]));
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

		private  class Deserialize<TKey, TValue> : TreeDataDeserializeRule<Dictionary<TKey, TValue>>
		{
			protected override void Execute(TreeDataByteSequence self, ref object value, ref int nameCode)
			{
				if (nameCode != -1)
				{
					SwitchRead(self, ref value, nameCode);
					return;
				}
				var targetType = typeof(Dictionary<TKey, TValue>);
				if (!(self.TryReadType(out Type dataType) && dataType == typeof(Dictionary<TKey, TValue>)))
				{
					//��Ծ����,�����ȡ����
					self.SubTypeReadValue(dataType, targetType, ref value);
					return;
				}
				//��ȡ�ֶ�����
				self.ReadUnmanaged(out int count);
				if (count == ValueMarkCode.NULL_OBJECT)
				{
					value = null;
					return;
				}
				if (count < 0)
				{
					self.ReadBack(4);
					self.SkipData(dataType);
					return;
				}
				if (value is not Dictionary<TKey, TValue>) value = new Dictionary<TKey, TValue>();
				for (int i = 0; i < count; i++)
				{
					self.ReadUnmanaged(out nameCode);
					SwitchRead(self, ref value, nameCode);
				}
			}

			/// <summary>
			/// �ֶζ�ȡ
			/// </summary>
			private void SwitchRead(TreeDataByteSequence self, ref object value, int nameCode)
			{
				if (value is not Dictionary<TKey, TValue> obj) return;
				switch (nameCode)
				{
					case 1683726967:
						{
							//�����л�����
							if (!(self.TryReadType(out Type dataType) && dataType == typeof(KeyValuePair<TKey, TValue>[])))
							{
								//��Ծ����
								self.SkipData(dataType);
								return;
							}
							//��ȡ����ά������,�����null�����ǰ�浲��,���Բ����ж�
							self.ReadUnmanaged(out int _);
							self.ReadUnmanaged(out int length);
							//����������
							if (obj.Count != 0) obj.Clear();
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
						break;
					default: self.SkipData(); break;
				}
			}
		}
	}
}