namespace NativeWatcher
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit, Size = 128)]
    internal unsafe struct scrProgram
    {
        [FieldOffset(0x10)] public byte** CodeBlocks;

        [FieldOffset(0x20)] public uint EntryPointArgCount;
        [FieldOffset(0x24)] public uint StaticVariablesCount;

        [FieldOffset(0x2C)] public uint NativeFunctionsCount;

        [FieldOffset(0x40)] public ulong* NativeFunctions;

        [FieldOffset(0x60)] public sbyte* Name;
    }
}
