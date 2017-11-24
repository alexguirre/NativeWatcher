namespace NativeWatcher.Forms
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    internal sealed class FormsManager : IDisposable
    {
        public Thread Thread { get; private set; }
        public MainForm MainForm { get; private set; }
        public bool IsMainFormVisible
        {
            get => MainForm != null && MainForm.Visible;
            set
            {
                if (MainForm != null)
                {
                    if (value)
                    {
                        if (!MainForm.Visible)
                        {
                            MainForm.Show();
                            bool topMost = MainForm.TopMost;
                            MainForm.TopMost = true;
                            MainForm.TopMost = topMost;
                        }
                    }
                    else
                    {
                        if (MainForm.Visible)
                        {
                            MainForm.Hide();
                        }
                    }
                }
            }
        }

        public FormsManager()
        {
            Thread = new Thread(() =>
            {
                try
                {
                    MainForm = new MainForm();
                    Application.EnableVisualStyles();
                    MainForm.Show();
                    Application.Run();
                }
                catch (ThreadAbortException)
                {
                    MainForm?.Close();
                    MainForm?.Dispose();
                }
            });
            Thread.SetApartmentState(ApartmentState.STA);
            Thread.IsBackground = true;
            Thread.Start();
        }

        public void Dispose()
        {
            if (MainForm != null)
            {
                MainForm.Close();
                MainForm.Dispose();
            }
            MainForm = null;
            Thread?.Abort();
            Thread = null;
        }
    }
}
