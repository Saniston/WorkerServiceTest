namespace WorkerServiceForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnInstall = new Button();
            btnUninstall = new Button();
            btnStart = new Button();
            SuspendLayout();
            // 
            // btnInstall
            // 
            btnInstall.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnInstall.Location = new Point(12, 106);
            btnInstall.Name = "btnInstall";
            btnInstall.Size = new Size(94, 29);
            btnInstall.TabIndex = 0;
            btnInstall.Text = "Instalar";
            btnInstall.UseVisualStyleBackColor = true;
            btnInstall.Click += btnInstall_Click;
            // 
            // btnUninstall
            // 
            btnUninstall.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnUninstall.Location = new Point(112, 106);
            btnUninstall.Name = "btnUninstall";
            btnUninstall.Size = new Size(94, 29);
            btnUninstall.TabIndex = 1;
            btnUninstall.Text = "Desinstalar";
            btnUninstall.UseVisualStyleBackColor = true;
            btnUninstall.Click += btnUninstall_Click;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStart.Location = new Point(212, 106);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 2;
            btnStart.Text = "Iniciar";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(317, 147);
            Controls.Add(btnStart);
            Controls.Add(btnUninstall);
            Controls.Add(btnInstall);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Instalador";
            ResumeLayout(false);
        }

        #endregion

        private Button btnInstall;
        private Button btnUninstall;
        private Button btnStart;
    }
}