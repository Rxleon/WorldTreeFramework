namespace WorldTree
{
	public static class EntryRule
	{
		class Add : AddRule<Entry>
		{
			protected override void Execute(Entry self)
			{
				self.Log("��ڣ�����");
				self.AddComponent(out DotNetInit _);
			}
		}
	}
}