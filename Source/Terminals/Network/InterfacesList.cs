using System;
using System.Windows.Forms;

namespace Terminals.Network
{
    internal partial class InterfacesList : UserControl
    {
        public InterfacesList()
        {
            InitializeComponent();
        }
        
        private void InterfacesList_Load(object sender, EventArgs e)
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            Metro.NetworkInterfaceList nil = new Metro.NetworkInterfaceList();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = nil.Interfaces;
            this.dataGridView2.DataSource = null;
            this.dataGridView2.DataSource = Terminals.Network.DNS.AdapterInfo.GetAdapters();

        }
    }
}
