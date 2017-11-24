namespace NativeWatcher
{
    using System;
    using System.Runtime.InteropServices;

    internal static unsafe class WinFunctions
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)] public static extern void* memcpy(void* dest, void* src, ulong count);
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)] public static extern void* memset(void* dest, int value, ulong byteCount);

        [DllImport("user32.dll")] public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// https://msdn.microsoft.com/es-es/library/windows/desktop/bb760637(v=vs.85).aspx
        /// </summary>
        public const uint TCM_SETMINTABWIDTH = 0x1300 + 49;
    }
}
