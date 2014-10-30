namespace Terminals.Wizard
{
    partial class CommonOptions
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
            if(disposing && (components != null))
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MinimizeCheckbox = new System.Windows.Forms.CheckBox();
            this.SingleCheckbox = new System.Windows.Forms.CheckBox();
            this.WarnCheckbox = new System.Windows.Forms.CheckBox();
            this.autoSwitchOnCapture = new System.Windows.Forms.CheckBox();
            this.LoadDefaultShortcutsCheckbox = new System.Windows.Forms.CheckBox();
            this.ImportRDP = new System.Windows.Forms.CheckBox();
            this.CaptureToClipboard = new System.Windows.Forms.CheckBox();
            this.CaptureToFolder = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Below are a few of the more common options which you should take time now to twea" +
                "k according to your needs.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Welcome to Terminals";
            // 
            // MinimizeCheckbox
            // 
            this.MinimizeCheckbox.AutoSize = true;
            this.MinimizeCheckbox.Checked = true;
            this.MinimizeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MinimizeCheckbox.Location = new System.Drawing.Point(28, 93);
            this.MinimizeCheckbox.Name = "MinimizeCheckbox";
            this.MinimizeCheckbox.Size = new System.Drawing.Size(98, 17);
            this.MinimizeCheckbox.TabIndex = 5;
            this.MinimizeCheckbox.Text = "Minimize to tray";
            this.MinimizeCheckbox.UseVisualStyleBackColor = true;
            // 
            // SingleCheckbox
            // 
            this.SingleCheckbox.AutoSize = true;
            this.SingleCheckbox.Checked = true;
            this.SingleCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SingleCheckbox.Location = new System.Drawing.Point(28, 116);
            this.SingleCheckbox.Name = "SingleCheckbox";
            this.SingleCheckbox.Size = new System.Drawing.Size(217, 17);
            this.SingleCheckbox.TabIndex = 6;
            this.SingleCheckbox.Text = "Allow a single instance of the application";
            this.SingleCheckbox.UseVisualStyleBackColor = true;
            // 
            // WarnCheckbox
            // 
            this.WarnCheckbox.AutoSize = true;
            this.WarnCheckbox.Checked = true;
            this.WarnCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WarnCheckbox.Location = new System.Drawing.Point(28, 139);
            this.WarnCheckbox.Name = "WarnCheckbox";
            this.WarnCheckbox.Size = new System.Drawing.Size(122, 17);
            this.WarnCheckbox.TabIndex = 7;
            this.WarnCheckbox.Text = "Warn on disconnect";
            this.WarnCheckbox.UseVisualStyleBackColor = true;
            // 
            // autoSwitchOnCapture
            // 
            this.autoSwitchOnCapture.AutoSize = true;
            this.autoSwitchOnCapture.Checked = true;
            this.autoSwitchOnCapture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSwitchOnCapture.Location = new System.Drawing.Point(48, 254);
            this.autoSwitchOnCapture.Name = "autoSwitchOnCapture";
            this.autoSwitchOnCapture.Size = new System.Drawing.Size(218, 17);
            this.autoSwitchOnCapture.TabIndex = 8;
            this.autoSwitchOnCapture.Text = "Automatically switch to Capture Manager";
            this.autoSwitchOnCapture.UseVisualStyleBackColor = true;
            // 
            // LoadDefaultShortcutsCheckbox
            // 
            this.LoadDefaultShortcutsCheckbox.AutoSize = true;
            this.LoadDefaultShortcutsCheckbox.Checked = true;
            this.LoadDefaultShortcutsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LoadDefaultShortcutsCheckbox.Location = new System.Drawing.Point(28, 162);
            this.LoadDefaultShortcutsCheckbox.Name = "LoadDefaultShortcutsCheckbox";
            this.LoadDefaultShortcutsCheckbox.Size = new System.Drawing.Size(131, 17);
            this.LoadDefaultShortcutsCheckbox.TabIndex = 9;
            this.LoadDefaultShortcutsCheckbox.Text = "Load default shortcuts";
            this.LoadDefaultShortcutsCheckbox.UseVisualStyleBackColor = true;
            // 
            // ImportRDP
            // 
            this.ImportRDP.AutoSize = true;
            this.ImportRDP.Location = new System.Drawing.Point(28, 185);
            this.ImportRDP.Name = "ImportRDP";
            this.ImportRDP.Size = new System.Drawing.Size(202, 17);
            this.ImportRDP.TabIndex = 10;
            this.ImportRDP.Text = "Import RDP Connections from registry";
            this.ImportRDP.UseVisualStyleBackColor = true;
            // 
            // CaptureToClipboard
            // 
            this.CaptureToClipboard.AutoSize = true;
            this.CaptureToClipboard.Checked = true;
            this.CaptureToClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CaptureToClipboard.Location = new System.Drawing.Point(28, 208);
            this.CaptureToClipboard.Name = "CaptureToClipboard";
            this.CaptureToClipboard.Size = new System.Drawing.Size(191, 17);
            this.CaptureToClipboard.TabIndex = 11;
            this.CaptureToClipboard.Text = "Enable screen capture to clipboard";
            this.CaptureToClipboard.UseVisualStyleBackColor = true;
            // 
            // CaptureToFolder
            // 
            this.CaptureToFolder.AutoSize = true;
            this.CaptureToFolder.Checked = true;
            this.CaptureToFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CaptureToFolder.Location = new System.Drawing.Point(28, 231);
            this.CaptureToFolder.Name = "CaptureToFolder";
            this.CaptureToFolder.Size = new System.Drawing.Size(174, 17);
            this.CaptureToFolder.TabIndex = 12;
            this.CaptureToFolder.Text = "Enable screen capture to folder";
            this.CaptureToFolder.UseVisualStyleBackColor = true;
            this.CaptureToFolder.CheckedChanged += new System.EventHandler(this.CaptureToFolder_CheckedChanged);
            // 
            // CommonOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CaptureToFolder);
            this.Controls.Add(this.CaptureToClipboard);
            this.Controls.Add(this.ImportRDP);
            this.Controls.Add(this.LoadDefaultShortcutsCheckbox);
            this.Controls.Add(this.autoSwitchOnCapture);
            this.Controls.Add(this.WarnCheckbox);
            this.Controls.Add(this.SingleCheckbox);
            this.Controls.Add(this.MinimizeCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CommonOptions";
            this.Size = new System.Drawing.Size(379, 283);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox MinimizeCheckbox;
        private System.Windows.Forms.CheckBox SingleCheckbox;
        private System.Windows.Forms.CheckBox WarnCheckbox;
        private System.Windows.Forms.CheckBox autoSwitchOnCapture;
        private System.Windows.Forms.CheckBox LoadDefaultShortcutsCheckbox;
        private System.Windows.Forms.CheckBox ImportRDP;
        private System.Windows.Forms.CheckBox CaptureToClipboard;
        private System.Windows.Forms.CheckBox CaptureToFolder;
    }
}
