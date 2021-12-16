using AudioRecognition.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace AudioRecognition.UI
{
    public partial class VideoForm : Form
    {
        private string username;
        private string filename; //视频文件名称
        private string audiofile;//音频文件名称
        private string srtname;  // srt字幕文件
        private string audioid;
        private string resultVideofile; //添加字幕后的视频
        public VideoForm(string username)
        {
            this.username = username;
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        public string OpenFile()
        {
            string strFileName = @"";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "视频文件(*.mp4;*.mkv;*.m4v;*.mov;*.avi;*.flv)|*.mp4;*.mkv;*.m4v;*.mov;*.avi;*.flv|所有文件|*.*";
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

        public string OpenAudioFile()
        {
            string strFileName = @"";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "音频文件(*.mp3;*.wav;*.pcm;*.m4a;*.acc)|*.mp3;*.wav;*.pcm;*.m4a;*.acc|所有文件|*.*";
            ofd.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true; //验证路径的有效性
            ofd.CheckPathExists = true;//验证路径的有效性
            if (ofd.ShowDialog() == DialogResult.OK) //用户点击确认按钮，发送确认消息
            {
                strFileName += ofd.FileName;//获取在文件对话框中选定的路径或者字符串
                audiofile = ofd.FileName;

            }
            return strFileName;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox2.Text = OpenFile();
            textBox2.Visible = true;
            Console.WriteLine(filename);
        }

        private void labelfile_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (filename != null && filename != "" && filename.Length!=0)
            {
                Console.WriteLine(filename);
                Video2Audio v2a = new Video2Audio();
                audiofile = v2a.getAudio(filename);
                Console.WriteLine(audiofile);
                textBox3.Text = "生成音频"+ audiofile;
                textBox3.Visible = true;

            }
            else
            {
                textBox3.Text = OpenAudioFile();
                textBox3.Visible = true;
                Console.WriteLine(audiofile);
            }

        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            if (audiofile != "" || audiofile != null)
            {
                FlashSend flash = new FlashSend();
                textBox1.Text = "请稍等片刻！";

                var jsons = Task.Run(() =>
                {
                    string result = flash.FlashRRun(audiofile);
                    JObject json1 = (JObject)JsonConvert.DeserializeObject(result);
                    return json1;
                });
                JObject json = await jsons;
                if (json["code"].ToString().Equals("0"))
                {
                    textBox1.Text = json["flash_result"].First()["text"].ToString();
                    audioid = json["request_id"].ToString();
                    Console.WriteLine(audioid);
                }
                FlashService fserver = new FlashService();
                fserver.AddFRRbyUser(json, username);
            }
            else
            {
                textBox1.Text = "未选择文件！";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (audioid != null && audioid != "") //获取对应识别id并且解析字幕
            {
                FlashService flashService = new FlashService();
                string res = flashService.getSRT(audioid);
                textBox1.Text = res;
                srtlabel.Text = "字幕解析成功！";
            }else if (textbox_audioid.Text != "" && textbox_audioid.Text.Trim() != null && textbox_audioid.Text.Trim()!="")
            {
                FlashService flashService = new FlashService();
                string res = flashService.getSRT(textbox_audioid.Text);
                textBox1.Text = res;
                srtlabel.Text = "字幕解析成功！";
            }
            else
            {
                srtlabel.Text = "未获取到识别id，请先进行有效识别！或指定ID";
            }
        }

        private void button3_Click(object sender, EventArgs e)//save srt
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "srt文件(*.srt)|*.srt";
            if (textBox1.Text.Trim() == "" || textBox1.Text.Trim() == null)
            {
                srtlabel.Text = "解析结果为空，请解析音频文件！";
                return;
            }
            if (file.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = File.CreateText(file.FileName);
                Console.WriteLine(file.FileName);
                sw.Write(textBox1.Text);
                sw.Flush();
                sw.Close();
                srtname = file.FileName;
                srtlabel.Text = "保存成功";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (audioid != null && audioid != "") //获取对应识别id并且解析字幕
            {
                FlashService flashService = new FlashService();
                string res = flashService.getSRT(audioid);
                textBox1.Text = res;
                srtlabel.Text = "字幕解析成功！";
            }
            else
            {
                srtlabel.Text = "未获取到识别id，请先进行有效识别！";
            }
        }

        private void VideoForm_Activated(object sender, EventArgs e)
        {

        }

        private void VideoForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(filename!=null && srtname!=null && srtname!=""&& filename != null)
            {
                //合成带字幕的mkv视频，mkv对字幕支持效果好
                VideoAddSRT videoAddSRT = new VideoAddSRT();
                resultVideofile = videoAddSRT.addSrt2Video(filename, srtname);
                srtlabel.Text = "字幕已经添加到视频: " + resultVideofile;
            }
            else
            {
                srtlabel.Text = "请先选择视频和音频！";

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (resultVideofile != null)
            {
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = @" /select, " + resultVideofile;
                p.Start();
                //Process.Start("ExpLore", srtfile);
            }
            else
            {
                srtlabel.Text = "请先生成视频文件！";
            }
        }
    }
}
