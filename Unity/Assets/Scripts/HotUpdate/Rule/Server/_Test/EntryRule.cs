/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2024/8/27 15:39

* ������

*/
namespace WorldTree
{
	public static class MainWorldRule
	{
		class Add : AddRule<MainWorld>
		{
			protected override void Execute(MainWorld self)
			{
				self.Log("��ڣ�����");
				self.AddComponent(out DotNetInit _);
			}
		}
	}
}