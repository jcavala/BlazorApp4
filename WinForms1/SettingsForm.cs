using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms1.Data;

namespace WinForms1
{
    public partial class SettingsForm : Form
    {
        
        IniFile ini = new IniFile();
        public SettingsForm()
        {
            InitializeComponent();
            var x = ini.Read("Document Folder");
            lDocument.Text = x;
            x = ini.Read("Json Folder");
            lJson.Text = x;    
            x = ini.Read("Xml Folder");
            lXml.Text = x;
        }

        private void bDocument_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var x = folderBrowserDialog1.SelectedPath;
                lDocument.Text = x;
                Program.documentFolder = x;
            }

        }

        private void bJson_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var x = folderBrowserDialog1.SelectedPath;
                lJson.Text = x;
                Program.jsonFolder = x;
                
            }
        }

        private void bXml_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var x = folderBrowserDialog1.SelectedPath;
                lXml.Text = x;
                Program.xmlFolder = x;
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            ini.Write("Document Folder", Program.documentFolder);
            ini.Write("Json Folder", Program.jsonFolder);
            ini.Write("Xml Folder", Program.xmlFolder);
            
        }
    }
}
