namespace NativeWatcher.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    internal delegate void ScriptChangedEventHandler(ScriptTabPageContents sender, ScriptNativeCalls script);

    internal partial class ScriptTabPageContents : UserControl
    {
        private ScriptNativeCalls script;

        public event ScriptChangedEventHandler CurrentScriptChanged;

        public ScriptNativeCalls CurrentScript
        {
            get => script;
            private set
            {
                if(value != script)
                {
                    script = value;
                    OnCurrentScriptChanged(script);
                }
            }
        }
        public TabPage Tab { get; }

        public ScriptTabPageContents(TabPage tab)
        {
            Tab = tab;

            InitializeComponent();

            UpdateScriptsComboBox(scriptsComboBox);

            Dock = DockStyle.Fill;
            Tab.Text = "No script";
            Tab.Controls.Add(this);

            nativesCountLabel.Visible = false;
        }

        protected virtual void OnCurrentScriptChanged(ScriptNativeCalls script)
        {
            UpdateListView(false);
            if (script != null)
            {
                nativesCountLabel.Text = $"Number of natives: {script.Natives.Length}";
                nativesCountLabel.Visible = true;
            }
            else
            {
                nativesCountLabel.Visible = false;
            }
            CurrentScriptChanged?.Invoke(this, script);
        }

        private void OnScriptsComboBoxDropDown(object sender, EventArgs e)
        {
            UpdateScriptsComboBox(scriptsComboBox);
        }

        private void OnScriptsComboBoxSelectedValueChanged(object sender, EventArgs e)
        {
            int index = scriptsComboBox.SelectedIndex;
            if(index < 0)
            {
                CurrentScript = null;
                Tab.Text = "No script";
                scriptsComboBox.Text = "Please, select script...";
            }
            else
            {
                string scriptName = scriptsComboBox.SelectedItem.ToString();
                Rage.Game.LogTrivial("scriptName: " + scriptName);
                uint hash = Rage.Game.GetHashKey(scriptName);
                Rage.Game.LogTrivial("hash: " + hash);
                if (Plugin.Fetcher.Scripts.TryGetValue(hash, out ScriptNativeCalls scr))
                {
                    Rage.Game.LogTrivial("found");
                    CurrentScript = scr;
                    Tab.Text = "Script: " + scr.ScriptName;
                }
                else
                {
                    Rage.Game.LogTrivial("not found");
                    CurrentScript = null;
                    Tab.Text = "No script";
                }
            }
        }

        public void UpdateListView(bool onlyCount)
        {
            if (script == null)
            {
                listView.Items.Clear();
            }
            else
            {
                if (onlyCount)
                {
                    for (int i = 0; i < script.Natives.Length; i++)
                    {
                        ScriptNative n = script.Natives[i];
                        listView.Items[i].SubItems[0].Text = n.TimesCalled.ToString();
                    }
                }
                else
                {
                    for (int i = 0; i < script.Natives.Length; i++)
                    {
                        ScriptNative n = script.Natives[i];
                        string[] row = new[] { n.TimesCalled.ToString(), n.Name, "0x" + n.Hash.ToString("X16"), n.Address.ToString("X16") };
                        ListViewItem item = new ListViewItem(row);
                        listView.Items.Add(item);
                    }
                }
            }
        }



        static void UpdateScriptsComboBox(ComboBox comboBox)
        {
            IReadOnlyDictionary<uint, ScriptNativeCalls> scripts = Plugin.Fetcher.Scripts;
            comboBox.Items.Clear();
            foreach (KeyValuePair<uint, ScriptNativeCalls> p in scripts)
            {
                comboBox.Items.Add(p.Value.ScriptName);
            }

            if (comboBox.SelectedIndex < 0)
            {
                comboBox.Text = "Please, select script...";
            }
        }
    }
}
