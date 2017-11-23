namespace NativeWatcher
{
    using System;
    using System.Runtime.InteropServices;

    using Rage;
    
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct NativeRegistration
    {
        [FieldOffset(0x0000)] public NativeRegistration* Next;
        [FieldOffset(0x0008)] public fixed ulong HandlersPointers[7];
        [FieldOffset(0x0040)] public uint EntriesCount;
        [FieldOffset(0x0048)] public fixed ulong Hashes[7];



        private static NativeRegistration** registrationTable;
        public static NativeRegistration** GetRegistrationTable()
        {
            if (registrationTable == null)
            {
                IntPtr address = Game.FindPattern("48 8D 0D ?? ?? ?? ?? 4E 8B 1C C7 41 0F B6 C3 48 8B 0C C1");
                address = address + *(int*)(address + 3) + 7;
                registrationTable = (NativeRegistration**)address;
            }

            return registrationTable;
        }
    }
}
