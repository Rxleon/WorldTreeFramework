using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderPassFeature : ScriptableRendererFeature
{
	TestRenderPass testRenderPass;

	CustomRenderPassCreateMesh m_ScriptablePassCreateMesh;
	public Material material;
	public Shader shaderMesh;

	public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;


	public override void Create()
	{
		testRenderPass = new TestRenderPass(RenderPassEvent.BeforeRenderingPostProcessing);


		m_ScriptablePassCreateMesh = new CustomRenderPassCreateMesh();
		m_ScriptablePassCreateMesh._Material = new Material(shaderMesh);
		m_ScriptablePassCreateMesh.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
	}

	//���������������Ⱦ����ע��һ��������Ⱦͨ����
	//��Ϊÿ�����������һ����Ⱦ��ʱ�������ô˷�����
	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		if (material == null) return;

		testRenderPass.material = this.material;
		testRenderPass.Setup(renderer);
		renderer.EnqueuePass(testRenderPass);
		renderer.EnqueuePass(m_ScriptablePassCreateMesh);
	}
}

public class TestRenderPass : ScriptableRenderPass
{
	private ScriptableRenderer currentTarget;
	private TestVolume volume;
	public Material material;
	public TestRenderPass(RenderPassEvent evt)
	{
		renderPassEvent = evt;
	}
	public void Setup(ScriptableRenderer currentTarget)
	{
		this.currentTarget = currentTarget;
	}
	public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
	{
		// �����ǰ���û�����ú���Ч������ֱ�ӷ���
		if (!renderingData.cameraData.postProcessEnabled) return;


		// ��ȡ��ǰ�� Volume ��ջ
		var stack = VolumeManager.instance.stack;

		// �Ӷ�ջ�л�ȡ TestVolume ���
		volume = stack.GetComponent<TestVolume>();
		// ���û���ҵ� TestVolume ������������δ�����ֱ�ӷ���
		if (volume == null) return;
		if (!volume.IsActive()) return;

		// ���ò��ʵĲ���
		material.SetFloat("_Offs", volume.offset.value);

		// ����һ��������������ڴ洢��Ⱦ����
		CommandBuffer cmd = CommandBufferPool.Get("TestRenderPass");

		// ��ȡ��ǰ�������ɫ��ȾĿ��
		var source = currentTarget.cameraColorTarget;
		// ����һ����ʱ����� ID
		int temTextureID = Shader.PropertyToID("_TestTex");

		// ��ȡ��ǰ�������ȾĿ��������
		RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
		// ����һ����ʱ��ȾĿ�꣬ʹ��ǰ���ȡ��������
		cmd.GetTemporaryRT(temTextureID, descriptor);

		// ���ò��ʵ� _Offs ����Ϊ 0.5

		// �� source ��ȾĿ������ݿ����� temTextureID����Ӧ�ò��ʵĵ�һ�� pass������Ϊ 0��
		cmd.Blit(source, temTextureID, material, 0);
		// �� temTextureID �����ݿ����� source ��ȾĿ��
		cmd.Blit(temTextureID, source);

		// ִ����������е���������
		context.ExecuteCommandBuffer(cmd);
		// �ͷ��������
		CommandBufferPool.Release(cmd);
	}
}

public class TestVolume : VolumeComponent, IPostProcessComponent
{

	public FloatParameter offset = new FloatParameter(0.1f);

	public bool IsActive()
	{
		//return material.value != null;
		return true;
	}

	public bool IsTileCompatible()
	{
		return false;
	}

}

/// <summary>
/// ����Ч��
/// </summary>
class CustomRenderShaderPostPass : ScriptableRenderPass
{
	//������Ⱦ���ʣ�ͨ��Create������ֵ
	public Material _Material;


	public void SetMaterial(Material material)
	{
		_Material = material;
	}

	//��ִ����Ⱦͨ��֮ǰ���ô˷�����
	//��������������ȾĿ�꼰�����״̬�����⣬�����Դ�����ʱ��ȾĿ������
	//���Ϊ�գ������Ⱦͨ������Ⱦ������������ȾĿ�ꡣ
	//������� Command Buffer.Set Render Target�����Ϊ���� <c>Configure Target</c> �� <c>Configure Clear</c>��
	//��Ⱦ�ܵ���ȷ���Ը����ܷ�ʽ����Ŀ�����ú������
	public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
	{
	}


	// �����������ʵ����Ⱦ�߼���
	// ʹ�� <c>Scriptable Render Context</c> �������������ִ���������
	// https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
	// �����ص��� Scriptable Render Context.submit����Ⱦ���߽��ڹ����е��ض����������

	public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
	{
		//����һ��CommandBuffer
		CommandBuffer cmd = CommandBufferPool.Get("TestRenderShader");

		//������ȾĿ��Ϊ�������ɫ������
		cmd.Blit(colorAttachment, RenderTargetHandle.CameraTarget.Identifier(), _Material);
		//ִ��CommandBuffer
		context.ExecuteCommandBuffer(cmd);
		////����CommandBuffer
		//CommandBufferPool.Release(cmd);
		cmd.Clear();
	}

	// ������ִ�д���Ⱦͨ���ڼ䴴�����κ��ѷ�����Դ��
	public override void OnCameraCleanup(CommandBuffer cmd)
	{
	}
}


//��������
class CustomRenderPassCreateMesh : ScriptableRenderPass
{
	public Material _Material;
	public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
	{
		CommandBuffer cmd = CommandBufferPool.Get("CreateMesh");
		cmd.DrawMesh(CreateMesh(), Matrix4x4.identity, _Material);
		//����ͺ���һ���Ĳ���
		context.ExecuteCommandBuffer(cmd);
		CommandBufferPool.Release(cmd);
	}
	//��������
	Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[4] { new Vector3(1, 1, 1), new Vector3(-1, 1, 1), new Vector3(-1, 1, -1), new Vector3(1, 1, -1) };
		int[] indices = new int[8] { 0, 1, 1, 2, 2, 3, 3, 0 };
		//�����򵥵�������
		mesh.SetIndices(indices, MeshTopology.Lines, 0);
		return mesh;
	}
}
