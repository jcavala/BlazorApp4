namespace WinForms1
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            bLogin = new Button();
            bDoc = new Button();
            bSettings = new Button();
            button1 = new Button();
            bDocuments = new Button();
            bGetRsa = new Button();
            bGetAesKey = new Button();
            SuspendLayout();
            // 
            // bLogin
            // 
            resources.ApplyResources(bLogin, "bLogin");
            bLogin.Name = "bLogin";
            bLogin.UseVisualStyleBackColor = true;
            bLogin.Click += bLogin_Click;
            // 
            // bDoc
            // 
            resources.ApplyResources(bDoc, "bDoc");
            bDoc.Name = "bDoc";
            bDoc.UseVisualStyleBackColor = true;
            bDoc.Click += bDoc_Click;
            // 
            // bSettings
            // 
            resources.ApplyResources(bSettings, "bSettings");
            bSettings.Name = "bSettings";
            bSettings.UseVisualStyleBackColor = true;
            bSettings.Click += bSettings_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += bUserSettingsClick;
            // 
            // bDocuments
            // 
            resources.ApplyResources(bDocuments, "bDocuments");
            bDocuments.Name = "bDocuments";
            bDocuments.UseVisualStyleBackColor = true;
            bDocuments.Click += bDocuments_Click;
            // 
            // bGetRsa
            // 
            resources.ApplyResources(bGetRsa, "bGetRsa");
            bGetRsa.Name = "bGetRsa";
            bGetRsa.UseVisualStyleBackColor = true;
            bGetRsa.Click += bGetRsa_Click;
            // 
            // bGetAesKey
            // 
            resources.ApplyResources(bGetAesKey, "bGetAesKey");
            bGetAesKey.Name = "bGetAesKey";
            bGetAesKey.UseVisualStyleBackColor = true;
            bGetAesKey.Click += bGetAesKey_Click;
            // 
            // StartForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(bGetAesKey);
            Controls.Add(bGetRsa);
            Controls.Add(bDocuments);
            Controls.Add(button1);
            Controls.Add(bSettings);
            Controls.Add(bDoc);
            Controls.Add(bLogin);
            Name = "StartForm";
            ResumeLayout(false);
        }

        #endregion

        private Button bLogin;
        private Button bDoc;
        private Button bSettings;
        private Button button1;
        private Button bDocuments;
        private Button bGetRsa;
        private Button bGetAesKey;
    }
}