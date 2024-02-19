using UnityEngine;
using YooAsset;

public class GameEntry : MonoBehaviour
{
	public ResourcePackage package;

	// Start is called before the first frame update
	void Start()
    {
		

	}

	public void Load()
	{

		// ��ʼ����Դϵͳ
		YooAssets.Initialize();

		// ����Ĭ�ϵ���Դ��
		package = YooAssets.CreatePackage("DefaultPackage");

		// ���ø���Դ��ΪĬ�ϵ���Դ��������ʹ��YooAssets��ؼ��ؽӿڼ��ظ���Դ�����ݡ�
		YooAssets.SetDefaultPackage(package);


		var initParameters = new OfflinePlayModeParameters();

		package.InitializeAsync(initParameters).Completed += (a) =>
		{
			package.LoadAssetAsync<GameObject>("MainWindow").Completed += GameEntry_Completed;
		};
	}



	private void GameEntry_Completed(AssetHandle obj)
	{
		GameObject gameObject = obj.AssetObject as GameObject;
		Debug.Log($"YooAsset ??? : {gameObject.name}");


	}

}
