using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioRecognition.DAL;
using AudioRecognition.Model;
using AudioRecognition.UI;

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

        private void button1_Click_2(object sender, EventArgs e)
        {
            CreateDB createDB = new CreateDB();

        }

        private void 退出登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User user = new User("2", "2");
            DbSRR dbSRR = new DbSRR();
            ShortRecognitionResult shortRecognition = 
                new ShortRecognitionResult("aa","a","a", DateTime.Now);
            dbSRR.AddSRR(shortRecognition, user);
        }
    }
}
