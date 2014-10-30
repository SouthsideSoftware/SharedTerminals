﻿namespace Terminals {
    partial class PopupTerminal {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupTerminal));
            this.tabControl1 = new TabControl.TabControl();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.AttachToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CaptureToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFullScreen = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.ShowTabs = false;
            this.tabControl1.ShowToolTipOnTitle = false;
            this.tabControl1.Size = new System.Drawing.Size(445, 241);
            this.tabControl1.TabIndex = 0;
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttachToolStripButton,
            this.CaptureToolStripButton,
            this.toolStripButtonFullScreen});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(445, 25);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // AttachToolStripButton
            // 
            this.AttachToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AttachToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("AttachToolStripButton.Image")));
            this.AttachToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AttachToolStripButton.Name = "AttachToolStripButton";
            this.AttachToolStripButton.Size = new System.Drawing.Size(135, 22);
            this.AttachToolStripButton.Text = "Attach to main window";
            this.AttachToolStripButton.Click += new System.EventHandler(this.AttachToTerminalsToolStripMenuItem_Click);
            // 
            // CaptureToolStripButton
            // 
            this.CaptureToolStripButton.Image = global::Terminals.Properties.Resources.camera;
            this.CaptureToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CaptureToolStripButton.Name = "CaptureToolStripButton";
            this.CaptureToolStripButton.Size = new System.Drawing.Size(106, 22);
            this.CaptureToolStripButton.Text = "Capture screen";
            this.CaptureToolStripButton.ToolTipText = "Capture Terminal Screen. This feature has to be enabled in application options fi" +
    "rst. (Ctrl+F12)";
            this.CaptureToolStripButton.Click += new System.EventHandler(this.CaptureToolStripButton_Click);
            // 
            // toolStripButtonFullScreen
            // 
            this.toolStripButtonFullScreen.Image = global::Terminals.Properties.Resources.arrow_out;
            this.toolStripButtonFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFullScreen.Name = "toolStripButtonFullScreen";
            this.toolStripButtonFullScreen.Size = new System.Drawing.Size(83, 22);
            this.toolStripButtonFullScreen.Text = "Full screen";
            this.toolStripButtonFullScreen.Click += new System.EventHandler(this.ToolStripButtonFullScreen_Click);
            // 
            // PopupTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 266);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mainToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PopupTerminal";
            this.Text = "Terminal Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PopupTerminal_FormClosing);
            this.Load += new System.EventHandler(this.PopupTerminal_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PopupTerminal_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl.TabControl tabControl1;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton AttachToolStripButton;
        private System.Windows.Forms.ToolStripButton CaptureToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButtonFullScreen;
    }
}