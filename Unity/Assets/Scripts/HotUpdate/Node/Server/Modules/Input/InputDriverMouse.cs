/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2025/1/2 17:04

* ������

*/
using System;

namespace WorldTree.Server
{
	/// <summary>
	/// �������������
	/// </summary>
	public class InputDriverMouse : InputDriver
		, AsAwake<InputDeviceManager>
		, AsUpdate
	{



	}

	/// <summary>
	/// ���������Ϣ��ί��
	/// </summary>
	/// <param name="nCode"></param>
	/// <param name="wParam"></param>
	/// <param name="lParam"></param>
	/// <returns></returns>
	public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

}