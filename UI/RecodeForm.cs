using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioRecognition.BLL;
namespace AudioRecognition.UI
{
    public partial class RecodeForm : Form
    {
        private string filename;
        private bool flag = false;
        public RecodeForm()
        {
            InitializeComponent();
        }
        Recorder recorder = new Recorder();

        private void LiveRecButton_Click(object sender, EventArgs e)
        {
            if (!flag) {
                string resfile = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "_rec.mp3";
                Console.WriteLine(resfile);
                recorder.SetFileName(resfile);
                filename = resfile;
                recorder.StartRec();
                label3.Text = "正在录音！";

                flag = true;
            }
            else
            {
                recorder.StopRec();
                label3.Text = "停止录音！";

                flag = false;
            }
            
        }

        private void showDatabutton_Click(object sender, EventArgs e)
        {
            if (filename != null)
            {
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = @" /select, " + filename;
                p.Start();
                //Process.Start("ExpLore", srtfile);
            }
            else
            {
                label3.Text = "请先生成录音文件！";
            }
        }
    }
}
