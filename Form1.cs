using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioRecognition
{
    public partial class Form1 : Form
    {
        public static Form1 f = null;
        public Form1()
        {
            InitializeComponent();
            f = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        NAudioRecorder recorder = new NAudioRecorder();

        private void button1_Click(object sender, EventArgs e)
        {
            recorder.setform(f);

            //开始录音
            recorder.SetFileName("record.wav");
            recorder.StartRec();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            recorder.StopRec();
        }
    }
}
