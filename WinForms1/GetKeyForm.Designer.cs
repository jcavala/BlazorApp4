namespace WinForms1
{
    partial class GetKeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetKeyForm));
            bGetAesKey = new Button();
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            bDocuments = new Button();
            bUsers = new Button();
            SuspendLayout();
            // 
            // bGetAesKey
            // 
            resources.ApplyResources(bGetAesKey, "bGetAesKey");
            bGetAesKey.Name = "bGetAesKey";
            bGetAesKey.UseVisualStyleBackColor = true;
            bGetAesKey.Click += GetAesKey_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            label1.Click += GetAesKey_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += GetRsaKeyClick;
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
            // bDocuments
            // 
            resources.ApplyResources(bDocuments, "bDocuments");
            bDocuments.Name = "bDocuments";
            bDocuments.UseVisualStyleBackColor = true;
            bDocuments.Click += bDocuments_Click;
            // 
            // bUsers
            // 
            resources.ApplyResources(bUsers, "bUsers");
            bUsers.Name = "bUsers";
            bUsers.UseVisualStyleBackColor = true;
            bUsers.Click += bUsers_Click;
            // 
            // GetKeyForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(bUsers);
            Controls.Add(bDocuments);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(bGetAesKey);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "GetKeyForm";
            Load += GetKeyForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bGetAesKey;
        private Label label1;
        private Button button1;
        private Label label2;
        private Label label3;
        private Button bDocuments;
        private Button bUsers;
    }
}