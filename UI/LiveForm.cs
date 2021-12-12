using AudioRecognition.BLL;
using AudioRecognition.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioRecognition.UI
{
    public partial class LiveForm : Form
    {
        bool flag = false;
        LiveNAudioRecorder liveNAudioRecorder = new LiveNAudioRecorder();
        private User user;
        public LiveForm(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void LiveRecButton_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                LiveRecButton.Image = Image.FromFile(@".\Resource\rec1.png");
                liveNAudioRecorder.setform(this);
                liveNAudioRecorder.StartRec();
                flag = true;
            }
            else
            {
                LiveRecButton.Image = Image.FromFile(@".\Resource\rec0.png");
                liveNAudioRecorder.StopRec();
                liveNAudioRecorder.saveLRR(user.Username);
                flag = false;
            }
            
            //开始录音操作
        }

        private void showDatabutton_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            LRRservice lR = new LRRservice();
            dataSet = lR.GetLRRData(user.Username);
            LiveDataView.DataSource = dataSet.Tables[0];
            panel1.Left = panel1.Left + 200;
            LiveDataView.Visible = true;
            label2.Visible = true;
        }
    }
}
