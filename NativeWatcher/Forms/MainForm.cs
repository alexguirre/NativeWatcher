namespace NativeWatcher.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    internal partial class MainForm : Form
    {
        private List<ScriptTabPageContents> scriptTabs = new List<ScriptTabPageContents>();

        public MainForm()
        {
            InitializeComponent();

            addButtonTabPage.HandleCreated += OnAddButtonTabPageHandleCreated;
            tabControl.MouseDown += OnTabControlMouseDown;
            tabControl.Selecting += OnTabControlSelecting;
            
            activeCheckBox.Checked = Plugin.Fetcher.IsActive;
        }

        public void UpdateCurrentScriptTab()
        {
            TabPage t = tabControl.SelectedTab;
            ScriptTabPageContents p = scriptTabs.FirstOrDefault(x => x.Tab == t);
            p?.UpdateListView(true);
        }

        private void AddNewTab()
        {
            int lastIndex = tabControl.TabCount - 1;
            TabPage newTab = new TabPage("Tab");
            scriptTabs.Add(new ScriptTabPageContents(newTab));
            tabControl.TabPages.Insert(lastIndex, newTab);
            tabControl.SelectedIndex = lastIndex;
        }

        private void OnTabControlMouseDown(object sender, MouseEventArgs e)
        {
            int lastIndex = tabControl.TabCount - 1;
            if (tabControl.GetTabRect(lastIndex).Contains(e.Location))
            {
                AddNewTab();
            }
        }

        private void OnTabControlSelecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == tabControl.TabCount - 1)
            {
                e.Cancel = true;
            }
        }

        private void OnAddButtonTabPageHandleCreated(object sender, EventArgs e)
        {
            WinFunctions.SendMessage(tabControl.Handle, WinFunctions.TCM_SETMINTABWIDTH, IntPtr.Zero, (IntPtr)20);
        }

        private void OnActiveCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            Plugin.Fetcher.IsActive = activeCheckBox.Checked;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
