namespace WinForms1
{
    partial class RequestDocumentGeneration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestDocumentGeneration));
            bRequest = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            tbCategory = new TextBox();
            tbLocation = new TextBox();
            dtpDate = new DateTimePicker();
            tbDocumentName = new TextBox();
            label4 = new Label();
            bUpdate = new Button();
            lRequestShow = new Label();
            label5 = new Label();
            tbStoreId = new TextBox();
            cbIgnoreDate = new CheckBox();
            SuspendLayout();
            // 
            // bRequest
            // 
            resources.ApplyResources(bRequest, "bRequest");
            bRequest.Name = "bRequest";
            bRequest.UseVisualStyleBackColor = true;
            bRequest.Click += bRequest_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // tbCategory
            // 
            resources.ApplyResources(tbCategory, "tbCategory");
            tbCategory.Name = "tbCategory";
            // 
            // tbLocation
            // 
            resources.ApplyResources(tbLocation, "tbLocation");
            tbLocation.Name = "tbLocation";
            // 
            // dtpDate
            // 
            resources.ApplyResources(dtpDate, "dtpDate");
            dtpDate.Name = "dtpDate";
            // 
            // tbDocumentName
            // 
            resources.ApplyResources(tbDocumentName, "tbDocumentName");
            tbDocumentName.Name = "tbDocumentName";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // bUpdate
            // 
            resources.ApplyResources(bUpdate, "bUpdate");
            bUpdate.Name = "bUpdate";
            bUpdate.UseVisualStyleBackColor = true;
            bUpdate.Click += bUpdate_Click;
            // 
            // lRequestShow
            // 
            resources.ApplyResources(lRequestShow, "lRequestShow");
            lRequestShow.Name = "lRequestShow";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // tbStoreId
            // 
            resources.ApplyResources(tbStoreId, "tbStoreId");
            tbStoreId.Name = "tbStoreId";
            // 
            // cbIgnoreDate
            // 
            resources.ApplyResources(cbIgnoreDate, "cbIgnoreDate");
            cbIgnoreDate.Name = "cbIgnoreDate";
            cbIgnoreDate.UseVisualStyleBackColor = true;
            // 
            // RequestDocumentGeneration
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cbIgnoreDate);
            Controls.Add(tbStoreId);
            Controls.Add(label5);
            Controls.Add(lRequestShow);
            Controls.Add(bUpdate);
            Controls.Add(label4);
            Controls.Add(tbDocumentName);
            Controls.Add(dtpDate);
            Controls.Add(tbLocation);
            Controls.Add(tbCategory);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(bRequest);
            Name = "RequestDocumentGeneration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bRequest;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox tbCategory;
        private TextBox tbLocation;
        private DateTimePicker dtpDate;
        private TextBox tbDocumentName;
        private Label label4;
        private Button bUpdate;
        private Label lRequestShow;
        private Label label5;
        private TextBox tbStoreId;
        private CheckBox cbIgnoreDate;
    }
}