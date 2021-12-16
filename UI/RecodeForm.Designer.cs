
namespace AudioRecognition.UI
{
    partial class RecodeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecodeForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LiveRecButton = new System.Windows.Forms.PictureBox();
            this.showDatabutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LiveRecButton)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.LiveRecButton);
            this.panel1.Controls.Add(this.showDatabutton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(265, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 364);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("思源宋体 CN Medium", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(71, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "再次点击停止录制音频";
            // 
            // LiveRecButton
            // 
            this.LiveRecButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LiveRecButton.Image = ((System.Drawing.Image)(resources.GetObject("LiveRecButton.Image")));
            this.LiveRecButton.Location = new System.Drawing.Point(74, 30);
            this.LiveRecButton.Name = "LiveRecButton";
            this.LiveRecButton.Size = new System.Drawing.Size(117, 111);
            this.LiveRecButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LiveRecButton.TabIndex = 0;
            this.LiveRecButton.TabStop = false;
            this.LiveRecButton.Click += new System.EventHandler(this.LiveRecButton_Click);
            // 
            // showDatabutton
            // 
            this.showDatabutton.Location = new System.Drawing.Point(74, 281);
            this.showDatabutton.Name = "showDatabutton";
            this.showDatabutton.Size = new System.Drawing.Size(119, 23);
            this.showDatabutton.TabIndex = 4;
            this.showDatabutton.Text = "打开文件路径";
            this.showDatabutton.UseVisualStyleBackColor = true;
            this.showDatabutton.Click += new System.EventHandler(this.showDatabutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("思源宋体 CN Medium", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(77, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "点击开始录制音频";
            // 
            // RecodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "RecodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录制音频";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LiveRecButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox LiveRecButton;
        private System.Windows.Forms.Button showDatabutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}