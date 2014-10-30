﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Terminals.Data;

namespace Terminals.Forms.EditFavorite
{
    internal partial class RdpLocalResourcesControl : UserControl,
        IRdpLocalResourcesControl, IProtocolObserver, IProtocolOptionsControl
    {
        public List<string> RedirectedDrives { get; set; }

        public bool RedirectDevices { get; set; }

        private string serverName;

        public RdpLocalResourcesControl()
        {
            InitializeComponent();

            this.RedirectedDrives = new List<String>();
            this.cmbSounds.SelectedIndex = 2;
        }

        private void BtnBrowseShare_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Desktop Share:";
                dialog.ShowNewFolderButton = false;
                dialog.SelectedPath = @"\\" + this.serverName;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.txtDesktopShare.Text = dialog.SelectedPath;
            }
        }

        private void BtnDrives_Click(object sender, EventArgs e)
        {
            using(var drivesForm = new DiskDrivesForm(this))
                drivesForm.ShowDialog(this);
        }

        public void SaveTo(IFavorite favorite)
        {
            var rdpOptions = favorite.ProtocolProperties as RdpOptions;
            if (rdpOptions == null)
                return;

            favorite.DesktopShare = this.txtDesktopShare.Text;
            rdpOptions.Redirect.Drives = this.RedirectedDrives;
            rdpOptions.Redirect.Ports = this.chkSerialPorts.Checked;
            rdpOptions.Redirect.Printers = this.chkPrinters.Checked;
            rdpOptions.Redirect.Clipboard = this.chkRedirectClipboard.Checked;
            rdpOptions.Redirect.Devices = this.RedirectDevices;
            rdpOptions.Redirect.SmartCards = this.chkRedirectSmartcards.Checked;

            // because of changing protocol the value of the combox doesnt have to be selected
            if (this.cmbSounds.SelectedIndex >= 0)
                rdpOptions.Redirect.Sounds = (RemoteSounds)this.cmbSounds.SelectedIndex;
        }

        public void LoadFrom(IFavorite favorite)
        {
            var rdpOptions = favorite.ProtocolProperties as RdpOptions;
            if (rdpOptions == null)
                return;

            this.txtDesktopShare.Text = favorite.DesktopShare;
            this.RedirectedDrives = rdpOptions.Redirect.Drives;
            this.chkSerialPorts.Checked = rdpOptions.Redirect.Ports;
            this.chkPrinters.Checked = rdpOptions.Redirect.Printers;
            this.chkRedirectClipboard.Checked = rdpOptions.Redirect.Clipboard;
            this.RedirectDevices = rdpOptions.Redirect.Devices;
            this.chkRedirectSmartcards.Checked = rdpOptions.Redirect.SmartCards;
            this.cmbSounds.SelectedIndex = (Int32)rdpOptions.Redirect.Sounds;
        }

        public void OnServerNameChanged(string newServerName)
        {
            this.serverName = newServerName;
        }
    }
}
