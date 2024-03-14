using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WinForms1
{
    public partial class EditUserDataForm : Form
    {
        
        string xmlDocument = Program.xmlFile; 
        
        
        public EditUserDataForm()
        {
            InitializeComponent();
            var ds = new DataSet(); 
            ds.ReadXml(xmlDocument);
            dataGridView1.DataSource = ds.Tables[0];
        }

      

        private void EditUserDataForm_Load(object sender, EventArgs e)
        {
          
        }

      


        

       

      

        private void bSave_Click(object sender, EventArgs e)
        {
            var data = (DataTable)dataGridView1.DataSource;
            data.WriteXml(xmlDocument);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Program.Location = (string)dataGridView1.SelectedRows[0].Cells[2].Value;
            Program.Language = (string)dataGridView1.SelectedRows[0].Cells[1].Value;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Program.Language);
        }
    }
}
