using System.Collections.Generic;

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
				if (self.TryWriteDataHead(value, nameCode, 1, out T obj)) return;

				//�����ֵ���һ�������ֶ�
				self.WriteUnmanaged(1683726967);
				//���л������ֶ�
				self.WriteUnmanaged(0L);
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
				if (self.TryReadDataHead(typeof(T), ref value, out int count)) return;
				if (self.CheckClassCount(count)) return;

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
							if (self.TryReadDataHead(typeof(ItemT[]), ref value, out int count)) return;
							count = ~count;
							if (count != 1)
							{
								//��ȡָ�����
								self.ReadBack(4);
								self.SkipData(null);
								return;
							}
							ForeachRead(self, obj);
						}
						break;
					default: self.SkipData(); break;
				}
			}
		}
	}
}