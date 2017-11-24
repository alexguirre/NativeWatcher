namespace NativeWatcher.Forms
{
    partial class ScriptTabPageContents
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.scriptsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView = new NativeWatcher.Forms.ListViewFixed();
            this.countColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hashColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addressColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nativesCountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scriptsComboBox
            // 
            this.scriptsComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptsComboBox.FormattingEnabled = true;
            this.scriptsComboBox.Location = new System.Drawing.Point(57, 3);
            this.scriptsComboBox.Name = "scriptsComboBox";
            this.scriptsComboBox.Size = new System.Drawing.Size(200, 24);
            this.scriptsComboBox.TabIndex = 0;
            this.scriptsComboBox.Text = "Please, select script...";
            this.scriptsComboBox.DropDown += new System.EventHandler(this.OnScriptsComboBoxDropDown);
            this.scriptsComboBox.SelectedValueChanged += new System.EventHandler(this.OnScriptsComboBoxSelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Script:";
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.countColumnHeader,
            this.nameColumnHeader,
            this.hashColumnHeader,
            this.addressColumnHeader});
            this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.Location = new System.Drawing.Point(6, 33);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(1223, 554);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.GridLines = true;
            // 
            // countColumnHeader
            // 
            this.countColumnHeader.Text = "Count";
            this.countColumnHeader.Width = 50;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 350;
            // 
            // hashColumnHeader
            // 
            this.hashColumnHeader.Text = "Hash";
            this.hashColumnHeader.Width = 350;
            // 
            // addressColumnHeader
            // 
            this.addressColumnHeader.Text = "Address";
            this.addressColumnHeader.Width = 350;
            // 
            // nativesCountLabel
            // 
            this.nativesCountLabel.AutoSize = true;
            this.nativesCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nativesCountLabel.Location = new System.Drawing.Point(309, 8);
            this.nativesCountLabel.Name = "nativesCountLabel";
            this.nativesCountLabel.Size = new System.Drawing.Size(139, 17);
            this.nativesCountLabel.TabIndex = 3;
            this.nativesCountLabel.Text = "Number of natives: ?";
            // 
            // ScriptTabPageContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nativesCountLabel);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.scriptsComboBox);
            this.Name = "ScriptTabPageContents";
            this.Size = new System.Drawing.Size(1232, 590);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox scriptsComboBox;
        private System.Windows.Forms.Label label1;
        private NativeWatcher.Forms.ListViewFixed listView;
        private System.Windows.Forms.ColumnHeader countColumnHeader;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader hashColumnHeader;
        private System.Windows.Forms.ColumnHeader addressColumnHeader;
        private System.Windows.Forms.Label nativesCountLabel;
    }
}
