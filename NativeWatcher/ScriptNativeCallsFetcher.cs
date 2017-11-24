namespace NativeWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using Rage;

    internal sealed unsafe class ScriptNativeCallsFetcher : IDisposable
    {
        private const uint StackCapacity = 65536;

        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] [return: MarshalAs(UnmanagedType.I1)] static extern bool IsInitialized();
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern void Initialize(IntPtr switchAddress);
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] [return: MarshalAs(UnmanagedType.I1)] static extern bool IsHooked();
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern void Hook();
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern void Unhook();
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern ulong* GetStackCount();
        [DllImport("NativeWatcher.Cpp.dll", ExactSpelling = true)] static extern NativeCallEntry* GetNativesCallsStack();


        private Dictionary<uint, ScriptNativeCalls> scripts = new Dictionary<uint, ScriptNativeCalls>();

        private ulong* stackCount;
        private NativeCallEntry* callsStack;
        //private ulong highestStackCount;
        //private ulong timesFetched;
        private ulong callsStackBufferCount;
        private NativeCallEntry[] callsStackBuffer = new NativeCallEntry[StackCapacity];

        //public ulong TimesFetched => timesFetched;
        public bool IsActive
        {
            get => IsHooked();
            set
            {
                if (value)
                {
                    Hook();
                }
                else
                {
                    Unhook();
                }
            }
        }
        public bool HasJustFetched { get; private set; }

        public IReadOnlyDictionary<uint, ScriptNativeCalls> Scripts => scripts;

        public ScriptNativeCallsFetcher()
        {
            if (!IsInitialized())
            {
                Initialize(Game.FindPattern("48 8D 15 ?? ?? ?? ?? 8B 8C 82 ?? ?? ?? ?? 48 03 CA FF E1 48 2B FE"));
            }

            stackCount = GetStackCount();
            callsStack = GetNativesCallsStack();
        }

        public void Dispose()
        {
            scripts = null;
            callsStackBuffer = null;
            IsActive = false;
        }

        public void Tick()
        {
            HasJustFetched = false;
            ulong c = *stackCount;
            //if (c > highestStackCount) highestStackCount = c;

            //Game.DisplaySubtitle("[Hooked:" + IsHooked() + "] " + c + "  " + highestStackCount + "~n~" + ((IntPtr)stackCount).ToString("X16") + "~n~" + ((IntPtr)callsStack).ToString("X16") + "~n~Fetched " + timesFetched + " times.");


            if (c > (StackCapacity / 4))
            {
                FetchStack();
            }
        }

        private void FetchStack()
        {
            callsStackBufferCount = *stackCount;
            fixed (NativeCallEntry* calls = callsStackBuffer)
            {
                WinFunctions.memcpy(calls, callsStack, callsStackBufferCount * 16);
            }
            *stackCount = 0;

            for (uint i = 0; i < callsStackBufferCount; i++)
            {
                NativeCallEntry e = callsStackBuffer[i];
                scrProgram* program = (scrProgram*)e.ProgramAddress;
                uint hash = GameFunctions.GetHashKey(program->Name, 0);

                if (!scripts.TryGetValue(hash, out ScriptNativeCalls scr))
                {
                    scripts[hash] = scr = new ScriptNativeCalls(program);
                }

                uint nativeIndex = e.NativeIndex;
                scr.Natives[nativeIndex].TimesCalled++;
            }

            //timesFetched++;
            HasJustFetched = true;
        }


        [StructLayout(LayoutKind.Explicit, Size = 16)]
        private struct NativeCallEntry
        {
            [FieldOffset(0x0)] public ulong ProgramAddress;
            [FieldOffset(0x8)] public uint NativeIndex;
        }
    }
}
