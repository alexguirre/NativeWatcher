namespace NativeWatcher
{
    using System.Windows.Forms;

    using Rage;

    internal static unsafe class Plugin
    {
        public static ScriptNativeCallsFetcher Fetcher { get; private set; }

        private static void Main()
        {
            while (Game.IsLoading)
                GameFiber.Sleep(1000);

            Fetcher = new ScriptNativeCallsFetcher();
            
            while (true)
            {
                GameFiber.Yield();

                if (Game.IsKeyDown(Keys.Y))
                {
                    Fetcher.IsActive = !Fetcher.IsActive;
                }

                Fetcher.Tick();
            }
        }

        private static void OnUnload(bool isTerminating)
        {
            Fetcher.Dispose();
        }
    }
}
