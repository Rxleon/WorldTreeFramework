namespace WorldTree
{
	/// <summary>
	/// ���л�����
	/// </summary>
	public interface Serialize : ISendRule, ILifeCycleRule
	{ }

	/// <summary>
	/// �����л�����
	/// </summary>
	public interface Deserialize : ISendRule, ILifeCycleRule
	{ }
}