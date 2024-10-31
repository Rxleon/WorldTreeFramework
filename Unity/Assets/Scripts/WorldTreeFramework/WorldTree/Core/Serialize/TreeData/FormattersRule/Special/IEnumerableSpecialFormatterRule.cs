


using System.Collections.Generic;
using System;

namespace WorldTree.TreeDataFormatters
{

	/// <summary>
	/// ö�����������ʽ����
	/// </summary>
	public static class IEnumerableSpecialFormatterRule
	{
		/// <summary>
		/// ö�������������л��������
		/// </summary>
		public abstract class SerializeBase<T, ItemT> : TreeDataSerializeRule<T>
			where T : class, IEnumerable<ItemT>, new()
		{
			/// <summary>
			/// ����д�뷽��
			/// </summary>
			public abstract void ForeachWrite(TreeDataByteSequence self, T obj);

			protected override void Execute(TreeDataByteSequence self, ref object value, ref int nameCode)
			{
				T obj = (T)value;
				if (nameCode == -1)
				{
					self.WriteType(typeof(T));
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
				self.WriteType(typeof(ItemT[]));
				//д������ά������
				self.WriteUnmanaged(~1);
				ForeachWrite(self, obj);
			}
		}

		/// <summary>
		/// ö���������ⷴ���л��������
		/// </summary>
		public abstract class DeserializeBase<T, ItemT> : TreeDataDeserializeRule<T>
			where T : class, IEnumerable<ItemT>, new()
		{
			/// <summary>
			/// ������ȡ����
			/// </summary>
			public abstract void ForeachRead(TreeDataByteSequence self, T obj);

			protected override void Execute(TreeDataByteSequence self, ref object value, ref int nameCode)
			{
				if (nameCode != -1)
				{
					SwitchRead(self, ref value, nameCode);
					return;
				}
				var targetType = typeof(T);
				if (!(self.TryReadType(out Type dataType) && dataType == typeof(T)))
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
				if (value is not T) value = new T();
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
				if (value is not T obj) return;
				switch (nameCode)
				{
					case 1683726967:
						{
							//�����л�����
							if (!(self.TryReadType(out Type dataType) && dataType == typeof(ItemT[])))
							{
								//��Ծ����
								self.SkipData(dataType);
								return;
							}
							//��ȡ����ά������,�����null�����ǰ�浲��,���Բ����ж�
							self.ReadUnmanaged(out int _);
							ForeachRead(self, obj);
						}
						break;
					default: self.SkipData(); break;
				}
			}
		}
	}
}