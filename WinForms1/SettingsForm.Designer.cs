namespace WinForms1
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            bUpdate = new Button();
            bXml = new Button();
            lXml = new Label();
            lJson = new Label();
            lDocument = new Label();
            bJson = new Button();
            bDocument = new Button();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // bUpdate
            // 
            resources.ApplyResources(bUpdate, "bUpdate");
            bUpdate.Name = "bUpdate";
            bUpdate.UseVisualStyleBackColor = true;
            bUpdate.Click += bUpdate_Click;
            // 
            // bXml
            // 
            resources.ApplyResources(bXml, "bXml");
            bXml.Name = "bXml";
            bXml.UseVisualStyleBackColor = true;
            bXml.Click += bXml_Click;
            // 
            // lXml
            // 
            resources.ApplyResources(lXml, "lXml");
            lXml.Name = "lXml";
            // 
            // lJson
            // 
            resources.ApplyResources(lJson, "lJson");
            lJson.Name = "lJson";
            // 
            // lDocument
            // 
            resources.ApplyResources(lDocument, "lDocument");
            lDocument.Name = "lDocument";
            // 
            // bJson
            // 
            resources.ApplyResources(bJson, "bJson");
            bJson.Name = "bJson";
            bJson.UseVisualStyleBackColor = true;
            bJson.Click += bJson_Click;
            // 
            // bDocument
            // 
            resources.ApplyResources(bDocument, "bDocument");
            bDocument.Name = "bDocument";
            bDocument.UseVisualStyleBackColor = true;
            bDocument.Click += bDocument_Click;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(bUpdate);
            Controls.Add(bXml);
            Controls.Add(lXml);
            Controls.Add(lJson);
            Controls.Add(lDocument);
            Controls.Add(bJson);
            Controls.Add(bDocument);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Name = "SettingsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bUpdate;
        private Button bXml;
        private Label lXml;
        private Label lJson;
        private Label lDocument;
        private Button bJson;
        private Button bDocument;
        private Label label6;
        private Label label5;
        private Label label4;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}