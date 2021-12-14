using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private string filename;
        private string srtfile;
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
                filename = ofd.FileName;

            }
            return strFileName;
        }

        private void selectFile_Click(object sender, EventArgs e)
        {

        }


        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            FlashSend flash = new FlashSend();
            FlashtextBox.Text = "请稍等片刻！";
            if(textBox1.Text.Trim()==null || textBox1.Text.Trim() == "")
            {
                FlashtextBox.Text = "未选择文件！";
                return;
            }
            var jsons = Task.Run(() =>
            {
                string result = flash.FlashRRun(textBox1.Text);
                JObject json1 = (JObject)JsonConvert.DeserializeObject(result);
                return json1;
            });
            JObject json = await jsons;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (IDtextBox.Text.Trim() != null)
            {
                //Json2SRT sRT = new Json2SRT();
                //Console.WriteLine(sRT.milisecond2Time("32400365"));
                //string str = "{\"request_id\":\"61b0a88786698904cdd328b0\",\"code\":0,\"message\":\"\",\"audio_duration\":12320,\"flash_result\":[{\"text\":\"你好你好，天王盖地虎，宝塔镇河妖测试成功。长风破浪会有时，直挂云帆济沧海。\",\"channel_id\":0,\"sentence_list\":[{\"text\":\"你好你好，天王盖地虎，宝塔镇河妖测试成功。\",\"start_time\":300,\"end_time\":6400,\"speaker_id\":0},{\"text\":\"长风破浪会有时，直挂云帆济沧海。\",\"start_time\":6920,\"end_time\":11520,\"speaker_id\":0}]}]}";
                //JObject jo = JObject.Parse(IDtextBox.Text.Trim());
                //FlashtextBox.Text = sRT.convertJon2SRT(jo);
                FlashService flashService = new FlashService();
                string res = flashService.getSRT(IDtextBox.Text.Trim());
                if(res == null)
                {
                    label6.Text = "id有误，未查询到信息";

                }
                else
                {
                    FlashtextBox.Text = res;
                    label6.Text = "转化成功，详细信息查看右边表格";
                }
            }
            else
            {
                label6.Text = "id有误，未查询到信息";

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "srt文件(*.srt)|*.srt";
            if (FlashtextBox.Text.Trim() == "" || FlashtextBox.Text.Trim() == null)
            {
                label6.Text = "解析结果为空，请解析音频文件！";
                return;
            }
            if (file.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = File.CreateText(file.FileName);
                Console.WriteLine(file.FileName);
                sw.Write(FlashtextBox.Text);
                sw.Flush();
                sw.Close();
                srtfile = file.FileName;
                label6.Text = "保存成功";
            }
            //string resfile = filename.Substring(0,filename.Length - 4);
            //Console.WriteLine(resfile);

        }

        private void openfilebutton_Click(object sender, EventArgs e)
        {
            if (srtfile != null)
            {
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = @" /select, "+srtfile;
                p.Start();
                //Process.Start("ExpLore", srtfile);
            }
            else
            {
                label6.Text = "请先保存srt文件！";
            }
        }
    }
}
