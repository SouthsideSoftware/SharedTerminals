﻿using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Terminals.Data;

namespace Terminals.Forms.EditFavorite
{
    internal partial class RdpExtendedSettingsControl : UserControl, IValidatedProtocolControl, IProtocolOptionsControl
    {
        public event CancelEventHandler IntegerValidationRequested;

        public RdpExtendedSettingsControl()
        {
            InitializeComponent();

            RegisterIntegerValidations();
        }

        private void RegisterIntegerValidations()
        {
            this.ShutdownTimeoutTextBox.Validating += this.FireIntegerValidationRequested;
            this.OverallTimeoutTextbox.Validating += this.FireIntegerValidationRequested;
            this.SingleTimeOutTextbox.Validating += this.FireIntegerValidationRequested;
            this.IdleTimeoutMinutesTextBox.Validating += this.FireIntegerValidationRequested;
        }

        private void FireIntegerValidationRequested(object sender, CancelEventArgs cancelEventArgs)
        {
            if (this.IntegerValidationRequested != null)
                this.IntegerValidationRequested(sender, cancelEventArgs);
        }

        public void SaveTo(IFavorite favorite)
        {
            var rdpOptions = favorite.ProtocolProperties as RdpOptions;
            if (rdpOptions == null)
                return;

            this.FillFavoriteRdpInterfaceOptions(rdpOptions);
            this.FillFavoriteRdpTimeOutOptions(rdpOptions);
        }

        private void FillFavoriteRdpInterfaceOptions(RdpOptions rdpOptions)
        {
            RdpUserInterfaceOptions userInterface = rdpOptions.UserInterface;
            rdpOptions.GrabFocusOnConnect = this.GrabFocusOnConnectCheckbox.Checked;
            userInterface.DisableWindowsKey = this.DisableWindowsKeyCheckbox.Checked;
            userInterface.DoubleClickDetect = this.DetectDoubleClicksCheckbox.Checked;
            userInterface.DisplayConnectionBar = this.DisplayConnectionBarCheckbox.Checked;
            userInterface.DisableControlAltDelete = this.DisableControlAltDeleteCheckbox.Checked;
            userInterface.AcceleratorPassthrough = this.AcceleratorPassthroughCheckBox.Checked;
            userInterface.EnableCompression = this.EnableCompressionCheckbox.Checked;
            userInterface.BitmapPeristence = this.EnableBitmapPersistenceCheckbox.Checked;
            userInterface.AllowBackgroundInput = this.AllowBackgroundInputCheckBox.Checked;
            userInterface.LoadBalanceInfo = this.txtLoadBalanceInfo.Text;
        }

        private void FillFavoriteRdpTimeOutOptions(RdpOptions rdpOptions)
        {
            rdpOptions.TimeOuts.ShutdownTimeout = ParseInteger(this.ShutdownTimeoutTextBox);
            rdpOptions.TimeOuts.OverallTimeout = ParseInteger(this.OverallTimeoutTextbox);
            rdpOptions.TimeOuts.ConnectionTimeout = ParseInteger(this.SingleTimeOutTextbox);
            rdpOptions.TimeOuts.IdleTimeout = ParseInteger(this.IdleTimeoutMinutesTextBox);
        }

        private static int ParseInteger(TextBox textBox)
        {
            int parsed;
            int.TryParse(textBox.Text, out parsed);
            return parsed;
        }

        public void LoadFrom(IFavorite favorite)
        {
            var rdpOptions = favorite.ProtocolProperties as RdpOptions;
            if (rdpOptions == null)
                return;

            FillRdpTimeOutControls(rdpOptions);
            FillRdpUserInterfaceControls(rdpOptions);
        }

        private void FillRdpTimeOutControls(RdpOptions rdpOptions)
        {
            this.ShutdownTimeoutTextBox.Text = rdpOptions.TimeOuts.ShutdownTimeout.ToString(CultureInfo.InvariantCulture);
            this.OverallTimeoutTextbox.Text = rdpOptions.TimeOuts.OverallTimeout.ToString(CultureInfo.InvariantCulture);
            this.SingleTimeOutTextbox.Text = rdpOptions.TimeOuts.ConnectionTimeout.ToString(CultureInfo.InvariantCulture);
            this.IdleTimeoutMinutesTextBox.Text = rdpOptions.TimeOuts.IdleTimeout.ToString(CultureInfo.InvariantCulture);
        }

        private void FillRdpUserInterfaceControls(RdpOptions rdpOptions)
        {
            var userInterface = rdpOptions.UserInterface;
            this.GrabFocusOnConnectCheckbox.Checked = rdpOptions.GrabFocusOnConnect;
            this.DisableWindowsKeyCheckbox.Checked = userInterface.DisableWindowsKey;
            this.DetectDoubleClicksCheckbox.Checked = userInterface.DoubleClickDetect;
            this.DisplayConnectionBarCheckbox.Checked = userInterface.DisplayConnectionBar;
            this.DisableControlAltDeleteCheckbox.Checked = userInterface.DisableControlAltDelete;
            this.AcceleratorPassthroughCheckBox.Checked = userInterface.AcceleratorPassthrough;
            this.EnableCompressionCheckbox.Checked = userInterface.EnableCompression;
            this.EnableBitmapPersistenceCheckbox.Checked = userInterface.BitmapPeristence;
            this.AllowBackgroundInputCheckBox.Checked = userInterface.AllowBackgroundInput;
            this.txtLoadBalanceInfo.Text = userInterface.LoadBalanceInfo;
        }
    }
}
