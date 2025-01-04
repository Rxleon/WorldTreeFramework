/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2024/10/31 18:06

* ������

*/

using System;
using System.Collections.Generic;

namespace WorldTree.TreeDataFormatters
{
	public static class ListFormatterRule
	{
		private class Serialize<T> : ICollectionSpecialFormatterRule.SerializeBase<List<T>, T> { }

		private class Deserialize<T> : ICollectionSpecialFormatterRule.DeserializeBase<List<T>, T> { }
	}
}