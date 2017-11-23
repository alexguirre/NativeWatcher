namespace NativeWatcher
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;

    using Rage;

    internal static unsafe class Plugin
    {
        private const uint StackCapacity = 32768;

        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] [return: MarshalAs(UnmanagedType.I1)] static extern bool IsHooked();
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern void Hook(void* nativeCaseAddress);
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern void Unhook();
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern ulong* GetStackCount();
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern ulong* GetNativesCallsStack();

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)] public static extern void* memcpy(void* dest, void* src, ulong count);
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)] public static extern void* memset(void* dest, int value, ulong byteCount);

        private delegate uint GetHashKeyDelegate(sbyte* text, uint startHash);
        private static GetHashKeyDelegate getHashKey;

        private static IntPtr switchAddress;

        static ulong timesFetched;
        static ulong timesCalled;
        static Dictionary<uint, ScriptNativeCalls> scripts = new Dictionary<uint, ScriptNativeCalls>();

        static ulong stackCount = 0;
        static NativeCallEntry[] callsStackBuffer = new NativeCallEntry[StackCapacity];

        private static void Main()
        {
            while (Game.IsLoading)
                GameFiber.Sleep(1000);

            getHashKey = Marshal.GetDelegateForFunctionPointer<GetHashKeyDelegate>(Game.FindAllOccurrencesOfPattern("45 33 D2 44 8B C2 4C 8B C9 48 85 C9")[1]);

            switchAddress = Game.FindPattern("48 8D 15 ?? ?? ?? ?? 8B 8C 82 ?? ?? ?? ?? 48 03 CA FF E1 48 2B FE");

            StringBuilder stringBuffer = new StringBuilder();
            string drawString = "";
            Game.RawFrameRender += (s, e) => { e.Graphics.DrawText(drawString, "Consolas", 18.0f, new System.Drawing.PointF(8.0f, 8.0f), System.Drawing.Color.Red); };

            int scrIndex = 0;
            ulong highestStackCount = 0;
            while (true)
            {
                GameFiber.Yield();

                if (Game.IsKeyDown(Keys.Y))
                {
                    if (IsHooked())
                    {
                        Unhook();
                    }
                    else
                    {
                        Hook(switchAddress.ToPointer());
                    }
                }

                stringBuffer.Clear();

                ulong c = *GetStackCount();
                if (c > highestStackCount) highestStackCount = c;

                Game.DisplaySubtitle("[Hooked:" + IsHooked() + "] Native Case Executed " + timesCalled + " times. " + c +  "  " + highestStackCount + "~n~" + ((IntPtr)GetStackCount()).ToString("X16") + "~n~" + ((IntPtr)GetNativesCallsStack()).ToString("X16") + "~n~Fetched " + timesFetched + " times.");

                if (Game.IsKeyDown(Keys.Add)) scrIndex = MathHelper.Clamp(scrIndex + 1, 0, scripts.Count - 1);
                if (Game.IsKeyDown(Keys.Subtract)) scrIndex = MathHelper.Clamp(scrIndex - 1, 0, scripts.Count - 1);

                ScriptNativeCalls scr = null;
                int k = 0;
                foreach (KeyValuePair<uint, ScriptNativeCalls> p in scripts)
                {
                    if(scrIndex == k++)
                    {
                        scr = p.Value;
                        break;
                    }
                }

                if(scr != null)
                {
                    stringBuffer.AppendFormat("Script: {1}\r\n", scrIndex, scr.ScriptName);
                    stringBuffer.AppendFormat("  Natives: {0}\r\n", scr.Natives.Length);
                    for (int i = 0; i < scr.Natives.Length; i++)
                    {
                        ScriptNative n = scr.Natives[i];
                        stringBuffer.AppendFormat("    #{0} {1}  x{2} ({3})\r\n", i, n.Hash.ToString("X16"), n.TimesCalled, n.Address.ToString("X16"));

                        if(i > 45)
                        {
                            break;
                        }
                    }
                }

                drawString = stringBuffer.ToString();

                if (*GetStackCount() > (StackCapacity / 2))
                {
                    FetchStack();
                }
            }
        }

        private static void OnUnload(bool isTerminating)
        {
            if (IsHooked())
            {
                Unhook();
            }
        }

        private static void FetchStack()
        {
            stackCount = *GetStackCount();
            fixed (NativeCallEntry* calls = callsStackBuffer)
            {
                memcpy(calls, GetNativesCallsStack(), StackCapacity * 16);
                memset(GetNativesCallsStack(), 0x00, StackCapacity * 16);
            }
            *GetStackCount() = 0;
            
            for (uint i = 0; i < stackCount; i++)
            {
                NativeCallEntry c = callsStackBuffer[i];
                scrProgram* program = (scrProgram*)c.ProgramAddress;
                uint hash = getHashKey(program->Name, 0);

                if (!scripts.TryGetValue(hash, out ScriptNativeCalls scr))
                {
                    scripts[hash] = scr = new ScriptNativeCalls((scrProgram*)c.ProgramAddress);
                }

                uint nativeIndex = c.NativeIndex;
                scr.Natives[nativeIndex].TimesCalled++;

                timesCalled++;
            }

            timesFetched++;
        }

        [StructLayout(LayoutKind.Explicit, Size = 16)]
        private struct NativeCallEntry
        {
            [FieldOffset(0x0)] public ulong ProgramAddress;
            [FieldOffset(0x8)] public uint NativeIndex;
        }
    }
}
