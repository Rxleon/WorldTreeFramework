namespace WorldTree.Server
{

	public static partial class DeepCopyTestRule
	{
		[NodeRule(nameof(AddRule<DeepCopyTest>))]
		private static void OnAdd(this DeepCopyTest self)
		{
			CopyTest copySource = new CopyTest();
			copySource.CopyA = new CopyTestA();
			copySource.CopyARef = copySource.CopyA;
			copySource.CopyA.CopyTestB = new CopyTestB();
			copySource.CopyA.CopyTestB.ValueString = "�����ַ���";


			CopyTestDict1 testDict = new CopyTestDict1();



			copySource.ValueDict = testDict;

			testDict.Value1 = 123987;
			testDict.Value11 = "�ֵ�����";
			testDict.Add(1, 100);
			testDict.Add(2, 200);

			self.AddTemp(out TreeCopier treeCopy);
			CopyTest copyTarget = null;
			treeCopy.CopyTo(copySource, ref copyTarget);


			self.Log($"�Ա��ֶ�A {copySource.CopyA == copyTarget.CopyA}");
			self.Log($"�Ա��ֶ�ARef {copySource.CopyARef == copyTarget.CopyARef}");

			self.Log($"ԭ�������ñȽ� {copySource.CopyA == copySource.CopyARef}");
			self.Log($"Ŀ�����ͻ�ԭ���� {copyTarget.CopyA == copyTarget.CopyARef}");
		}
	}
}