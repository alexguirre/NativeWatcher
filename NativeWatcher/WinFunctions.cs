namespace NativeWatcher
{
    using System.Runtime.InteropServices;

    internal static unsafe class WinFunctions
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)] public static extern void* memcpy(void* dest, void* src, ulong count);
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)] public static extern void* memset(void* dest, int value, ulong byteCount);
    }
}
