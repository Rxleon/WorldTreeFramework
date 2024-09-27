
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;

namespace WorldTree
{
	/// <summary>
	/// data
	/// </summary>
	public partial class AData
	{
		/// <summary>
		/// 测试int
		/// </summary>
		public int AInt = 10;

		/// <summary>
		/// 测试int数组
		/// </summary>
		public int[] Ints;
	}

	//以下代码需要由工具生成
	public partial class AData
	{
		public static class KeyValuePairFormatterRule
		{
			class Serialize : TreeDataSerializeRule<TreeDataByteSequence, AData>
			{
				protected override void Execute(TreeDataByteSequence self, ref object value)
				{
					//============ Data <=> Byte <=> Object ======


					AData data = (AData)value;

					//类型名称
					self.WriteType(typeof(AData));
					if (data == null)//空对象
					{
						self.WriteUnmanaged((long)ValueMarkCode.NULL_OBJECT);
						return;
					}

					//写入字段数量
					self.WriteUnmanaged(~2);

					//AData的字段名称1
					self.WriteUnmanaged(101);
					if (!self.ContainsNameCode(101)) self.AddNameCode(101, nameof(data.AInt));

					//AData的字段名称1
					self.WriteUnmanaged(102);
					if (!self.ContainsNameCode(102)) self.AddNameCode(102, nameof(data.Ints));

					//value类型
					//类型名称
					self.WriteType(data.AInt.GetType());

					//字段值
					self.WriteUnmanaged(data.AInt);

					//self.WriteValue(data.AInt);


					self.WriteValue(data.Ints);
				}
			}

			class Deserialize : TreeDataDeserializeRule<TreeDataByteSequence, AData>
			{
				protected override void Execute(TreeDataByteSequence self, ref object value)
				{
					//通过类型码获取类型
					self.TryReadType(out Type type);
					//读取字段数量
					self.ReadUnmanaged(out int count);
					//空对象判断
					if (count == ValueMarkCode.NULL_OBJECT)
					{
						value = null;
						return;
					}

					//是本身类型，正常读取流程
					if (typeof(AData) == type)
					{
						//判断是否为负数，负数为数组
						if (count < 0) count = ~count;

						//类型新建和转换
						if (!(value is AData obj))
						{
							obj = new AData();
							value = obj;
						}
						//读取字段
						for (int i = 0; i < count; i++)
						{
							//读取字段名称
							self.ReadUnmanaged(out int nameCode);
							switch (nameCode)
							{
								case 101: self.ReadValue(ref obj.AInt); break;

								case 102: self.ReadValue(ref obj.Ints); break;

								default://不存在该字段，跳跃数据
									self.SkipData();
									break;
							}
						}
					}
					else
					{
						self.SubTypeReadValue(type, typeof(AData), ref value);
					}
				}
			}
		}

	}



	/// <summary>
	/// 序列化测试
	/// </summary>
	public class TreeDataTest : Node
		, ComponentOf<INode>
		, AsAwake
	{ }
}

