
namespace AudioRecognition
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.usernameInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordInput = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonRegist = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("思源宋体 CN Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(192, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // usernameInput
            // 
            this.usernameInput.Font = new System.Drawing.Font("思源宋体 CN Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernameInput.Location = new System.Drawing.Point(290, 83);
            this.usernameInput.Name = "usernameInput";
            this.usernameInput.Size = new System.Drawing.Size(301, 35);
            this.usernameInput.TabIndex = 1;
            this.usernameInput.TextChanged += new System.EventHandler(this.usernameInput_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("思源宋体 CN Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(192, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "密    码：";
            // 
            // passwordInput
            // 
            this.passwordInput.Font = new System.Drawing.Font("思源宋体 CN Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordInput.Location = new System.Drawing.Point(290, 149);
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.Size = new System.Drawing.Size(301, 35);
            this.passwordInput.TabIndex = 3;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Font = new System.Drawing.Font("思源宋体 CN Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLogin.Location = new System.Drawing.Point(197, 270);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(119, 44);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "登录";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonRegist
            // 
            this.buttonRegist.Font = new System.Drawing.Font("思源宋体 CN Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRegist.Location = new System.Drawing.Point(472, 270);
            this.buttonRegist.Name = "buttonRegist";
            this.buttonRegist.Size = new System.Drawing.Size(119, 44);
            this.buttonRegist.TabIndex = 5;
            this.buttonRegist.Text = "注册";
            this.buttonRegist.UseVisualStyleBackColor = true;
            this.buttonRegist.Click += new System.EventHandler(this.buttonRegist_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRegist);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.passwordInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usernameInput);
            this.Controls.Add(this.label1);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox usernameInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordInput;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonRegist;
    }
}