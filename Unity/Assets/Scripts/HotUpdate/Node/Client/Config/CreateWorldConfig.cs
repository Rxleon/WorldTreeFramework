namespace WorldTree
{
	/// <summary>
	/// 测试数据
	/// </summary>
	public class CreateWorldConfigGroup : ConfigGroup<int, CreateWorldConfig>
	{
	}

	/// <summary>
	/// 测试数据
	/// </summary>
	public class CreateWorldConfig : Config<int>
	{

		/// <summary>
		/// 进程
		/// </summary>
		public int Process;

		/// <summary>
		/// 区
		/// </summary>
		public int Zone;

		/// <summary>
		/// 主世界
		/// </summary>
		public string WorldType;

		/// <summary>
		/// 世界名称
		/// </summary>
		public string WorldName;
	}
}