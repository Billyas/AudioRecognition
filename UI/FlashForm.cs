using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioRecognition.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AudioRecognition.UI
{
    public partial class FlashForm : Form
    {
        private string username;
        public FlashForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string filename = OpenFile();
            textBox1.Text = filename;

            //Console.WriteLine(OpenFile());
        }


        public string OpenFile()
        {
            string strFileName=@"";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "音频文件(*.mp3;*.wav;*.pcm;*.m4a;*.acc)|*.mp3;*.wav;*.pcm;*.m4a;*.acc|所有文件|*.*";
            ofd.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true; //验证路径的有效性
            ofd.CheckPathExists = true;//验证路径的有效性
            if (ofd.ShowDialog() == DialogResult.OK) //用户点击确认按钮，发送确认消息
            {
                strFileName += ofd.FileName;//获取在文件对话框中选定的路径或者字符串

            }
            return strFileName;
        }

        private void selectFile_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FlashSend flash = new FlashSend();
            string result = flash.FlashRRun(textBox1.Text);
            JObject json = (JObject)JsonConvert.DeserializeObject(result);
            if (json["code"].ToString().Equals("0"))
            {
                FlashtextBox.Text = json["flash_result"].First()["text"].ToString();
            }
            FlashService fserver = new FlashService();
            fserver.AddFRRbyUser(json, username);

        }

        private void label4_Click(object sender, EventArgs e)
        {
            FlashService flashService = new FlashService();
            DataSet dataSet = flashService.GetFRRbyUser(username);
            dataGridView1.DataSource = dataSet.Tables[0];
        }
    }
}
