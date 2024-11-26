﻿/****************************************

* 作者：闪电黑客
* 日期：2024/11/20 20:18

* 描述：数据库管理器

*/
using LiteDB;

namespace WorldTree
{
	/// <summary>
	/// 数据库管理器
	/// </summary>
	public class LiteDBManager : Node, IDataBase
		, AsComponentBranch
		, ComponentOf<DataBaseProxy>
		, AsAwake<string>
	{
		/// <summary>
		/// 数据库
		/// </summary>
		public LiteDatabase database;

		public IDataCollection<T> GetCollection<T>()
			where T : class, INodeData
		{
			if (!this.TryGetComponent(out LiteDBCollection<T> node))
			{
				// 键值规则为 a-Z$_，把非字母符号全换成下划线
				string typeName = typeof(T).ToString();
				char[] collectionNameChars = typeName.ToCharArray();
				for (int i = 0; i < collectionNameChars.Length; i++)
				{
					if (!char.IsLetter(collectionNameChars[i]))
					{
						collectionNameChars[i] = '_';
					}
				}
				string collectionName = new string(collectionNameChars);

				ILiteCollection<BsonDocument> collection = database.GetCollection(collectionName);

				if (collection != null)
				{
					node = this.AddComponent(out LiteDBCollection<T> _, collection);
				}
			}
			return node;
		}

		public bool TryGetCollection<T>(out IDataCollection<T> collection)
			where T : class, INodeData
		{
			if (!this.TryGetComponent(out collection))
			{
				collection = GetCollection<T>();
			}
			return collection != null;
		}
	}
}