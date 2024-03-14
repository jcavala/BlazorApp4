namespace WinForms1
{
    partial class DocumentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentForm));
            bSave = new Button();
            bDelete = new Button();
            bCheckSignature = new Button();
            bDownloadSignature = new Button();
            bDownloadDocument = new Button();
            bOpen = new Button();
            documentTable = new DataGridView();
            bDocList = new Button();
            bRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)documentTable).BeginInit();
            SuspendLayout();
            // 
            // bSave
            // 
            resources.ApplyResources(bSave, "bSave");
            bSave.Name = "bSave";
            bSave.UseVisualStyleBackColor = true;
            bSave.Click += bSave_Click;
            // 
            // bDelete
            // 
            resources.ApplyResources(bDelete, "bDelete");
            bDelete.Name = "bDelete";
            bDelete.UseVisualStyleBackColor = true;
            bDelete.Click += bDelete_Click;
            // 
            // bCheckSignature
            // 
            resources.ApplyResources(bCheckSignature, "bCheckSignature");
            bCheckSignature.Name = "bCheckSignature";
            bCheckSignature.UseVisualStyleBackColor = true;
            bCheckSignature.Click += bCheckSignature_Click_1;
            // 
            // bDownloadSignature
            // 
            resources.ApplyResources(bDownloadSignature, "bDownloadSignature");
            bDownloadSignature.Name = "bDownloadSignature";
            bDownloadSignature.UseVisualStyleBackColor = true;
            bDownloadSignature.Click += bDownloadSignature_Click_1;
            // 
            // bDownloadDocument
            // 
            resources.ApplyResources(bDownloadDocument, "bDownloadDocument");
            bDownloadDocument.Name = "bDownloadDocument";
            bDownloadDocument.UseVisualStyleBackColor = true;
            bDownloadDocument.Click += bDownloadDocument_Click_1;
            // 
            // bOpen
            // 
            resources.ApplyResources(bOpen, "bOpen");
            bOpen.Name = "bOpen";
            bOpen.UseVisualStyleBackColor = true;
            bOpen.Click += bOpen_Click;
            // 
            // documentTable
            // 
            documentTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(documentTable, "documentTable");
            documentTable.Name = "documentTable";
            // 
            // bDocList
            // 
            resources.ApplyResources(bDocList, "bDocList");
            bDocList.Name = "bDocList";
            bDocList.UseVisualStyleBackColor = true;
            bDocList.Click += bDocListDownload_Click;
            // 
            // bRefresh
            // 
            resources.ApplyResources(bRefresh, "bRefresh");
            bRefresh.Name = "bRefresh";
            bRefresh.UseVisualStyleBackColor = true;
            bRefresh.Click += bRefresh_Click;
            // 
            // DocumentForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(bRefresh);
            Controls.Add(bDocList);
            Controls.Add(bSave);
            Controls.Add(bDelete);
            Controls.Add(bCheckSignature);
            Controls.Add(bDownloadSignature);
            Controls.Add(bDownloadDocument);
            Controls.Add(bOpen);
            Controls.Add(documentTable);
            Name = "DocumentForm";
            ((System.ComponentModel.ISupportInitialize)documentTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button bSave;
        private Button bDelete;
        private Button bCheckSignature;
        private Button bDownloadSignature;
        private Button bDownloadDocument;
        private Button bOpen;
        private DataGridView documentTable;
        private Button bDocList;
        private Button bRefresh;
    }
}