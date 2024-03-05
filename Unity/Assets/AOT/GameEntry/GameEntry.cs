using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WorldTree;
using YooAsset;

public class GameEntry : MonoBehaviour
{
	public ResourcePackage package;

	// Start is called before the first frame update
	private void Start()
	{
		StartCoroutine(Load());
	}

	private IEnumerator Load()
	{
		Debug.Log($"��������AB�������� ");

#if UNITY_EDITOR
		yield return InitializeYooAsset();
#else
		yield return LoadDefaultPackage();
		yield return LoadAOT();
		yield return LoadHotUpdate();
#endif

		Assembly hotUpdateAssembly = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "WorldTree.CoreUnity");

		Type type = hotUpdateAssembly.GetType("WorldTree.UnityWorldTree");
		Component component = gameObject.AddComponent(type);
		//���������ֶ�
		component.GetType().GetMethod("Start1").Invoke(component, null);
	}

	#region �༭��

	private IEnumerator InitializeYooAsset()
	{
		// ��ʼ����Դϵͳ
		YooAssets.Initialize();
		// ����Ĭ�ϵ���Դ��
		package = YooAssets.CreatePackage("DefaultPackage");
		YooAssets.SetDefaultPackage(package);

		var initParameters = new EditorSimulateModeParameters();
		var simulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline, "DefaultPackage");
		initParameters.SimulateManifestFilePath = simulateManifestFilePath;
		yield return package.InitializeAsync(initParameters);

		AssetHandle assetHandle = package.LoadAssetAsync<GameObject>("MainWindow");
		yield return assetHandle;

		Debug.Log($"�༭���������� : {assetHandle.AssetObject.name}");
	}

	#endregion

	#region ����

	private IEnumerator LoadDefaultPackage()
	{
		// ��ʼ����Դϵͳ
		YooAssets.Initialize();
		// ����Ĭ�ϵ���Դ��
		package = YooAssets.CreatePackage("DefaultPackage");
		// ���ø���Դ��ΪĬ�ϵ���Դ��������ʹ��YooAssets��ؼ��ؽӿڼ��ظ���Դ�����ݡ�
		YooAssets.SetDefaultPackage(package);
		yield return SingleInitializeYooAsset(package);

		AssetHandle assetHandle = package.LoadAssetAsync<GameObject>("MainWindow");
		yield return assetHandle;

		Debug.Log($"�������� : {assetHandle.AssetObject.name}");
	}

	private IEnumerator LoadAOT()
	{
		//AOT
		foreach (string address in GetAddressesByTag("aotDlls"))
		{
			Debug.Log($"AOT:{address}");

			AssetHandle handle = package.LoadAssetAsync<TextAsset>(address);
			yield return handle;
			RuntimeApi.LoadMetadataForAOTAssembly((handle.AssetObject as TextAsset).bytes, HomologousImageMode.SuperSet);
		}
	}

	private IEnumerator LoadHotUpdate()
	{
		Dictionary<string, Assembly> assemblys = new();

		foreach (string address in GetAddressesByTag("hotUpdateDlls"))
		{
			AssetHandle handle = package.LoadAssetAsync<TextAsset>(address);
			yield return handle;
			Debug.Log($"HotUpdate {address}:{(handle.AssetObject as TextAsset).bytes.Length}");

			Assembly assembly = Assembly.Load((handle.AssetObject as TextAsset).bytes);
			assemblys.Add(address, assembly);
		}
	}

	#endregion

	private IEnumerator SingleInitializeYooAsset(ResourcePackage package)
	{
		var initParameters = new OfflinePlayModeParameters();
		yield return package.InitializeAsync(initParameters);
	}

	public string[] GetAddressesByTag(string tag)
	{
		AssetInfo[] assetInfos = YooAssets.GetAssetInfos(tag);
		string[] addresses = new string[assetInfos.Length];
		for (int i = 0; i < assetInfos.Length; i++)
		{
			addresses[i] = assetInfos[i].Address;
		}

		return addresses;
	}
}