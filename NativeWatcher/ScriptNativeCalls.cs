namespace NativeWatcher
{
    using System;

    internal sealed unsafe class ScriptNativeCalls
    {
        public string ScriptName { get; }
        public ScriptNative[] Natives { get; }

        public ScriptNativeCalls(scrProgram* program)
        {
            ScriptName = new String(program->Name);
            Natives = new ScriptNative[program->NativeFunctionsCount];
            for (int i = 0; i < program->NativeFunctionsCount; i++)
            {
                Natives[i] = new ScriptNative(program->NativeFunctions[i]);
            }
        }
    }

    internal sealed class ScriptNative
    {
        public ulong Address { get; }
        public ulong Hash { get; }
        public string Name { get; }
        public ulong TimesCalled { get; set; }

        public ScriptNative(ulong address)
        {
            Address = address;
            Hash = NativeTranslator.AddressToOriginal(address);
            Name = NativeTranslator.OriginalToName(Hash);
        }
    }
}
