using HybridCLR;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using YooAsset;

public class GameEntry : MonoBehaviour
{
	public ResourcePackage package;
	private OfflinePlayModeParameters initParameters = new OfflinePlayModeParameters();

	// Start is called before the first frame update
	private void Start()
	{
		StartCoroutine(Load());
	}

	private IEnumerator Load()
	{
		Debug.Log($"��������AB�������� ");

		yield return LoadDefaultPackage();
		yield return LoadAOT();
		yield return LoadHotUpdate();
	}

	private IEnumerator LoadDefaultPackage()
	{
		// ��ʼ����Դϵͳ
		YooAssets.Initialize();
		// ����Ĭ�ϵ���Դ��
		var package = YooAssets.CreatePackage("DefaultPackage");
		// ���ø���Դ��ΪĬ�ϵ���Դ��������ʹ��YooAssets��ؼ��ؽӿڼ��ظ���Դ�����ݡ�
		YooAssets.SetDefaultPackage(package);

		InitializationOperation operation = package.InitializeAsync(initParameters);

		yield return operation;

		if (operation.Status == EOperationStatus.Succeed)
		{
			Debug.Log($"����Ĭ�ϰ��İ汾: {operation.PackageVersion}");
		}

		AssetHandle assetHandle = package.LoadAssetAsync<GameObject>("MainWindow");
		yield return assetHandle;

		Debug.Log($"�������� : {assetHandle.AssetObject.name}");
	}

	private IEnumerator LoadAOT()
	{
		Debug.Log($"AOT��ʼ ������");

		//AOT
		HomologousImageMode mode = HomologousImageMode.SuperSet;
		ResourcePackage package = YooAssets.CreatePackage("AotDlls");
		InitializationOperation operation = package.InitializeAsync(initParameters);
		yield return operation;
		if (operation.Status == EOperationStatus.Succeed)
		{
			Debug.Log($"����Ĭ�ϰ��İ汾: {operation.PackageVersion}");
			AllAssetsHandle handle = package.LoadAllAssetsAsync<TextAsset>("YooAsset.dll");
			yield return handle;

			foreach (TextAsset assetObject in handle.AllAssetObjects)
			{
				RuntimeApi.LoadMetadataForAOTAssembly(assetObject.bytes, mode);
			}

			Debug.Log($"AOT��� ������");
		}
	}

	private IEnumerator LoadHotUpdate()
	{
		Debug.Log($"HotUpdate��ʼ ������");

		ResourcePackage package = YooAssets.CreatePackage("HotUpdateDlls");
		InitializationOperation operation = package.InitializeAsync(initParameters);
		if (operation.Status == EOperationStatus.Succeed)
		{
			Debug.Log($"����Ĭ�ϰ��İ汾: {operation.PackageVersion}");

			AllAssetsHandle handle = package.LoadAllAssetsAsync<TextAsset>("WorldTree.Node.dll");
			yield return handle;
			foreach (TextAsset assetObject in handle.AllAssetObjects)
			{
				Assembly assembly = Assembly.Load(assetObject.bytes);
				if (assetObject.name == "WorldTree.CoreUnity")
				{
					Type type = assembly.GetType("UnityWorldTree");
					this.gameObject.AddComponent(type);
				}
			}
			Debug.Log($"HotUpdate��� ������");
		}
	}
}