namespace WorldTree
{
	/// <summary>
	/// ����֮�ģ�WinForm�߳�
	/// </summary>
	public class WinFormWorldHeart : WorldHeartBase
		, AsComponentBranch
		, CoreManagerOf<WorldLine>
		, AsAwake<int>
	{
		/// <summary>
		/// ��һ������ʱ��
		/// </summary>
		public DateTime afterTime;

		#region ��������
		/// <summary>
		/// �������� UpdateTime
		/// </summary>
		public WorldPulse<UpdateTime> worldUpdate;

		#endregion

		#region ȫ���¼�����

		/// <summary>
		/// ȫ���¼����� Enable
		/// </summary>
		public IRuleExecutor<Enable> enable;
		/// <summary>
		/// ȫ���¼����� Disable
		/// </summary>
		public IRuleExecutor<Disable> disable;
		/// <summary>
		/// ȫ���¼����� Update
		/// </summary>
		public IRuleExecutor<Update> update;
		/// <summary>
		/// ȫ���¼����� UpdateTime
		/// </summary>
		public IRuleExecutor<UpdateTime> updateTime;

		#endregion

		/// <summary>
		/// ����
		/// </summary>
		public override void Run()
		{
			isRun = true;
		}

		/// <summary>
		/// ��ͣ
		/// </summary>
		public override void Pause()
		{
			isRun = false;
		}

		/// <summary>
		/// ��֡����
		/// </summary>
		public override void OneFrame()
		{
			isRun = false;
			worldUpdate?.Update((DateTime.Now - afterTime));
		}

	}
}