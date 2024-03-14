using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormLib
{
    public partial class Users : Form
    {
        public string Location { get; set; }
        public string Language { get; set; }
        string xmlDocument = "C:\\dotnet\\WinForms1\\users.xml";
        public Users()
        {
            InitializeComponent();
            var ds = new DataSet(); ds.ReadXml(xmlDocument);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            var data = (DataTable)dataGridView1.DataSource;
            data.WriteXml(xmlDocument);
        }

        private void bChoose_Click(object sender, EventArgs e)
        {
            var cells = dataGridView1.SelectedRows[0].Cells;
            Language = (string)cells[2].Value;
            Location = (string)cells[3].Value;
        }
    }
}
