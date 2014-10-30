using System;
using System.Windows.Forms;
using Terminals.Data;
using Terminals.Data.Validation;

namespace Terminals
{
    internal partial class NewGroupForm : Form
    {
        private NewGroupForm()
        {
            InitializeComponent();
        }

        private void TxtGroupName_TextChanged(object sender, EventArgs e)
        {
            this.btnOk.Enabled = !string.IsNullOrEmpty(txtGroupName.Text);
        }

        /// <summary>
        /// Shows this dialog to the user asking for group name.
        /// If user confirms, the entered value is returned; otherwiser returns empty string.
        /// </summary>
        internal static string AskFroGroupName(IPersistence persistence)
        {
            using (var frmNewGroup = new NewGroupForm())
            {
                if (frmNewGroup.ShowDialog() == DialogResult.OK)
                    return ValidateNewGroupName(persistence, frmNewGroup.txtGroupName.Text);

                return string.Empty;
            }
        }

        private static string ValidateNewGroupName(IPersistence persistence, string newGroupName)
        {
            string message = new GroupNameValidator(persistence).ValidateNew(newGroupName);

            if (string.IsNullOrEmpty(message))
                return newGroupName;

            MessageBox.Show(message, "New Group name is not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return string.Empty;
        }
    }
}