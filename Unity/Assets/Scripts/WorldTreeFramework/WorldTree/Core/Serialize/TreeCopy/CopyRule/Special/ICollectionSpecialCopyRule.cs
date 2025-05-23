
using System;
using System.Collections.Generic;

namespace WorldTree.TreeCopys
{
	public static class ICollectionSpecialCopyRule
	{
		/// <summary>
		/// �������������⿽���������
		/// </summary>
		public abstract class CopyRuleBase<T, ItemT> : IEnumerableSpecialCopyRule.CopyRuleBase<T, ItemT>
			where T : class, ICollection<ItemT>, new()
		{
			public override void ForeachCopy(TreeCopyExecutor self, T source, T target)
			{
				foreach (var item in target)
				{
					if (item is IDisposable disposable) disposable.Dispose();
				}
				target.Clear();
				foreach (var item in source) target.Add(self.Copy(item));
			}
		}
	}
}