namespace NativeWatcher
{
    using System.Windows.Forms;

    using Rage;

    using NativeWatcher.Forms;

    internal static unsafe class Plugin
    {
        public static ScriptNativeCallsFetcher Fetcher { get; private set; }
        public static FormsManager Forms { get; private set; }

        private static void Main()
        {
            while (Game.IsLoading)
                GameFiber.Sleep(1000);

            Fetcher = new ScriptNativeCallsFetcher() { IsActive = true };
            Forms = new FormsManager();

            while (true)
            {
                GameFiber.Yield();

                Fetcher.Tick();

                if (Fetcher.HasJustFetched && Forms.IsMainFormVisible)
                {
                    Forms.MainForm.Invoke((System.Action)(() => { Forms.MainForm.UpdateCurrentScriptTab(); }));
                }

                if (Game.IsKeyDown(Keys.F11))
                {
                    Forms.MainForm.Invoke((System.Action)(() => { Forms.IsMainFormVisible = !Forms.IsMainFormVisible; }));
                }
            }
        }

        private static void OnUnload(bool isTerminating)
        {
            Forms?.Dispose();
            Fetcher?.Dispose();
        }
    }
}
