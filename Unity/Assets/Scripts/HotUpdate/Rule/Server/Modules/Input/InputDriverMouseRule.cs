/****************************************

* ���ߣ�����ڿ�
* ���ڣ�2025/1/2 17:06

* ������

*/
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WorldTree.Server
{
	public static class InpuDriverMouseRule
	{
		private class Awake : AwakeRule<InputDriverMouse, InputDeviceManager>
		{
			protected override void Execute(InputDriverMouse self, InputDeviceManager manager)
			{
				self.mouseProc = self.HookMouseCallback;
				self.mouseHookID = self.SetHook(self.mouseProc);

				self.inputManager = manager;
				self.Core.PoolGetUnit(out self.InputInfosList);

				self.IsExists = new bool[256];

				self.RegisterDevice<MouseKey>(1);

				self.SetInputType(MouseKey.Mouse, InputType.Axis2);
				self.SetInputType(MouseKey.MouseLeft, InputType.Press);
				self.SetInputType(MouseKey.MouseRight, InputType.Press);
				self.SetInputType(MouseKey.MouseMiddle, InputType.Press);
				self.SetInputType(MouseKey.MouseWheel, InputType.Delta2);
				self.SetInputType(MouseKey.Mouse0, InputType.Press);
				self.SetInputType(MouseKey.Mouse1, InputType.Press);
				self.SetInputType(MouseKey.Mouse2, InputType.Press);
				self.SetInputType(MouseKey.Mouse3, InputType.Press);
				self.SetInputType(MouseKey.Mouse4, InputType.Press);
				self.SetInputType(MouseKey.Mouse5, InputType.Press);
				self.SetInputType(MouseKey.Mouse6, InputType.Press);
			}
		}
		class Remove : RemoveRule<InputDriverMouse>
		{
			protected override void Execute(InputDriverMouse self)
			{
				UnhookWindowsHookEx(self.mouseHookID);
				self.mouseProc = null;
				self.mouseHookID = default;
			}
		}


		/// <summary>
		/// ��깳�ӵĻص�����
		/// </summary>
		/// <param name="nCode"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		private static IntPtr HookMouseCallback(this InputDriverMouse self, int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode < 0) return CallNextHookEx(self.mouseHookID, nCode, wParam, lParam);
			switch (wParam)
			{
				case (IntPtr)WindowMouseKey.LeftButtonDown:
					self.Log("111");
					//self.InputData(0, (byte)MouseKey.MouseLeft, GetPress(true));
					break;
				case (IntPtr)WindowMouseKey.LeftButtonUp:
					//self.InputData(0, (byte)MouseKey.MouseLeft, GetPress(false));
					break;
				case (IntPtr)WindowMouseKey.RightButtonDown:
					//self.InputData(0, (byte)MouseKey.MouseRight, GetPress(true));
					break;
				case (IntPtr)WindowMouseKey.RightButtonUp:
					//self.InputData(0, (byte)MouseKey.MouseRight, GetPress(false));
					break;
				case (IntPtr)WindowMouseKey.MiddleButtonDown:
					//self.InputData(0, (byte)MouseKey.MouseMiddle, GetPress(true));
					break;
				case (IntPtr)WindowMouseKey.MiddleButtonUp:
					//self.InputData(0, (byte)MouseKey.MouseMiddle, GetPress(false));
					break;
			}

			// ������һ������
			return CallNextHookEx(self.mouseHookID, nCode, wParam, lParam);
		}

		/// <summary>
		/// ��ȡ��갴��
		/// </summary>
		private static InputDriverInfo GetPress(bool isPress) => isPress ? new(true, 1) : default;

		/// <summary>
		/// ���ù���
		/// </summary>
		private static IntPtr SetHook(this InputDriverMouse self, LowLevelMouseProc proc)
		{
			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule)
			{
				// ����ȫ����깳��
				return SetWindowsHookEx(self.WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
			}
		}


		#region �ⲿ����

		/// <summary>
		/// �����ⲿ���� SetWindowsHookEx
		/// </summary>
		/// <param name="idHook"></param>
		/// <param name="lpfn"></param>
		/// <param name="hMod"></param>
		/// <param name="dwThreadId"></param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

		/// <summary>
		/// �����ⲿ���� UnhookWindowsHookEx
		/// </summary>
		/// <param name="hhk"></param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		/// <summary>
		/// �����ⲿ���� CallNextHookEx
		/// </summary>
		/// <param name="hhk"></param>
		/// <param name="nCode"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// �����ⲿ���� GetModuleHandle
		/// </summary>
		/// <param name="lpModuleName"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		/// <summary>
		/// �����ⲿ���� GetCursorPos
		/// </summary>
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(out POINT lpPoint);

		#endregion
	}

}