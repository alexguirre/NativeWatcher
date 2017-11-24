namespace NativeWatcher.Forms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.addButtonTabPage = new System.Windows.Forms.TabPage();
            this.activeCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.addButtonTabPage);
            this.tabControl.Location = new System.Drawing.Point(12, 39);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1240, 630);
            this.tabControl.TabIndex = 0;
            // 
            // addButtonTabPage
            // 
            this.addButtonTabPage.Location = new System.Drawing.Point(4, 22);
            this.addButtonTabPage.Name = "addButtonTabPage";
            this.addButtonTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.addButtonTabPage.Size = new System.Drawing.Size(1232, 604);
            this.addButtonTabPage.TabIndex = 1;
            this.addButtonTabPage.Text = "+";
            this.addButtonTabPage.UseVisualStyleBackColor = true;
            // 
            // activeCheckBox
            // 
            this.activeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.activeCheckBox.AutoSize = true;
            this.activeCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activeCheckBox.Location = new System.Drawing.Point(1183, 12);
            this.activeCheckBox.Name = "activeCheckBox";
            this.activeCheckBox.Size = new System.Drawing.Size(65, 21);
            this.activeCheckBox.TabIndex = 1;
            this.activeCheckBox.Text = "Active";
            this.activeCheckBox.UseVisualStyleBackColor = true;
            this.activeCheckBox.CheckedChanged += new System.EventHandler(this.OnActiveCheckBoxCheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.activeCheckBox);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "Native Watcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage addButtonTabPage;
        private System.Windows.Forms.CheckBox activeCheckBox;
    }
}