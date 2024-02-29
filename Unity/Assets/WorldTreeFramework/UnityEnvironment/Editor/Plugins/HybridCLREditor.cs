using HybridCLR.Editor.Commands;
using HybridCLR.Editor.Settings;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class HybridCLREditor
{
	[MenuItem("HybridCLR/CopyHotUpdateDlls")]
	public static void CopyHotUpdateDlls()
	{
		string externalHotUpdatePath = HybridCLRSettings.Instance.externalHotUpdateAssembliyDirs[0];
		string toDir = "Assets/Bundles/HotUpdateDlls";
		if (Directory.Exists(toDir)) Directory.Delete(toDir, true);
		Directory.CreateDirectory(toDir);
		AssetDatabase.Refresh();

		Debug.Log("�� " + externalHotUpdatePath + "���Ƶ�" + toDir);

		foreach (var hotUpdateAssemblie in HybridCLRSettings.Instance.hotUpdateAssemblies)
		{
			File.Copy(Path.Combine(externalHotUpdatePath, $"{hotUpdateAssemblie}.dll"), Path.Combine(toDir, $"{hotUpdateAssemblie}.dll.bytes"), true);
		}
		Debug.Log("CopyHotUpdateDlls ��� !!!");

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	[MenuItem("HybridCLR/CopyAotDlls")]
	public static void CopyAotDll()
	{
		BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
		string fromDir = Path.Combine(HybridCLRSettings.Instance.strippedAOTDllOutputRootDir, target.ToString());
		string toDir = "Assets/Bundles/AotDlls";
		if (Directory.Exists(toDir)) Directory.Delete(toDir, true);
		Directory.CreateDirectory(toDir);
		AssetDatabase.Refresh();

		Debug.Log("�� " + fromDir + "���Ƶ�" + toDir);

		foreach (string aotDll in AOTGenericReferences.PatchedAOTAssemblyList)
		{
			File.Copy(Path.Combine(fromDir, aotDll), Path.Combine(toDir, $"{aotDll}.bytes"), true);
		}
		//foreach (string aotDll in HybridCLRSettings.Instance.patchAOTAssemblies)
		//{
		//	File.Copy(Path.Combine(fromDir, aotDll), Path.Combine(toDir, $"{aotDll}.bytes"), true);
		//}

		Debug.Log("CopyAotDlls ��� !!!");

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	[MenuItem("HybridCLR/MyGenAll")]
	public static void GenAll()
	{
		BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
		CompileDllCommand.CompileDll(target);
		Il2CppDefGeneratorCommand.GenerateIl2CppDef();

		// �⼸����������HotUpdateDlls
		LinkGeneratorCommand.GenerateLinkXml(target);

		//��һ��ʹ�������Լ���
		// ���ɲü����aot dll
		StripAOTDllCommand.GenerateStripedAOTDlls();
		//GenerateStripedAOTDlls(target, EditorUserBuildSettings.selectedBuildTargetGroup);

		// �ŽӺ�������������AOT dll�����뱣֤�Ѿ�build��������AOT dll
		MethodBridgeGeneratorCommand.GenerateMethodBridge(target);
		ReversePInvokeWrapperGeneratorCommand.GenerateReversePInvokeWrapper(target);
		AOTReferenceGeneratorCommand.GenerateAOTGenericReference(target);
	}
}