namespace WinForms1
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            tbPassword = new TextBox();
            tbName = new TextBox();
            cbRegister = new CheckBox();
            bRDG = new Button();
            pbLoginIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbLoginIcon).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tbPassword
            // 
            resources.ApplyResources(tbPassword, "tbPassword");
            tbPassword.Name = "tbPassword";
            // 
            // tbName
            // 
            resources.ApplyResources(tbName, "tbName");
            tbName.Name = "tbName";
            tbName.TextChanged += tbName_TextChanged;
            // 
            // cbRegister
            // 
            resources.ApplyResources(cbRegister, "cbRegister");
            cbRegister.Name = "cbRegister";
            cbRegister.UseVisualStyleBackColor = true;
            // 
            // bRDG
            // 
            resources.ApplyResources(bRDG, "bRDG");
            bRDG.Name = "bRDG";
            bRDG.UseVisualStyleBackColor = true;
            bRDG.Click += bRDG_Click;
            // 
            // pbLoginIcon
            // 
            resources.ApplyResources(pbLoginIcon, "pbLoginIcon");
            pbLoginIcon.Name = "pbLoginIcon";
            pbLoginIcon.TabStop = false;
            // 
            // LoginForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbLoginIcon);
            Controls.Add(bRDG);
            Controls.Add(cbRegister);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(tbPassword);
            Controls.Add(tbName);
            Name = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)pbLoginIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label label2;
        private Label label1;
        private Button button1;
        private TextBox tbPassword;
        private TextBox tbName;
        private CheckBox cbRegister;
        private Button bRDG;
        private PictureBox pbLoginIcon;
    }
}