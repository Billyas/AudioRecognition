
namespace AudioRecognition.UI
{
    partial class LiveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveForm));
            this.LiveRecButton = new System.Windows.Forms.PictureBox();
            this.LRR_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LiveDataView = new System.Windows.Forms.DataGridView();
            this.showDatabutton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LiveRecButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LiveDataView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // LRR_textbox
            // 
            this.LRR_textbox.Location = new System.Drawing.Point(26, 201);
            this.LRR_textbox.Multiline = true;
            this.LRR_textbox.Name = "LRR_textbox";
            this.LRR_textbox.Size = new System.Drawing.Size(225, 105);
            this.LRR_textbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("思源宋体 CN Medium", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(71, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "点击开始实时录音识别";
            // 
            // LiveDataView
            // 
            this.LiveDataView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.LiveDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LiveDataView.Location = new System.Drawing.Point(12, 138);
            this.LiveDataView.Name = "LiveDataView";
            this.LiveDataView.RowTemplate.Height = 23;
            this.LiveDataView.Size = new System.Drawing.Size(409, 251);
            this.LiveDataView.TabIndex = 3;
            this.LiveDataView.Visible = false;
            // 
            // showDatabutton
            // 
            this.showDatabutton.Location = new System.Drawing.Point(95, 323);
            this.showDatabutton.Name = "showDatabutton";
            this.showDatabutton.Size = new System.Drawing.Size(96, 23);
            this.showDatabutton.TabIndex = 4;
            this.showDatabutton.Text = "显示历史记录";
            this.showDatabutton.UseVisualStyleBackColor = true;
            this.showDatabutton.Click += new System.EventHandler(this.showDatabutton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LiveRecButton);
            this.panel1.Controls.Add(this.showDatabutton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.LRR_textbox);
            this.panel1.Location = new System.Drawing.Point(280, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 364);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(136, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "实时语音历史记录";
            this.label2.Visible = false;
            // 
            // LiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LiveDataView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "LiveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实时语音识别";
            ((System.ComponentModel.ISupportInitialize)(this.LiveRecButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LiveDataView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LiveRecButton;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox LRR_textbox;
        private System.Windows.Forms.DataGridView LiveDataView;
        private System.Windows.Forms.Button showDatabutton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}