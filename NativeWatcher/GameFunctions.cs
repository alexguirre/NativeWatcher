namespace NativeWatcher
{
    using System.Runtime.InteropServices;

    using Rage;

    internal static unsafe class GameFunctions
    {
        public delegate uint GetHashKeyDelegate(sbyte* text, uint startHash);

        public static GetHashKeyDelegate GetHashKey { get; }

        static GameFunctions()
        {
            GetHashKey = Marshal.GetDelegateForFunctionPointer<GetHashKeyDelegate>(Game.FindAllOccurrencesOfPattern("45 33 D2 44 8B C2 4C 8B C9 48 85 C9")[1]);
        }
    }
}
