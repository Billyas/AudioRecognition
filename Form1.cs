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
        public static User user;
        public Form1()
        {
            InitializeComponent();
            f = this;
            user = new User("2", "2");
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
                new ShortRecognitionResult("aaaa","a","a", DateTime.Now);
            shortRecognition.UserName = user.Username;
            //dbSRR.AddSRR(shortRecognition, user);
            //dbSRR.GetSRRByUser(user);
            
            Console.WriteLine(dbSRR.DeleteSRR("aaa"));
        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            User user = new User("2", "2");
            DbSRR dbSRR = new DbSRR();
            ShortRecognitionResult shortRecognition =
                new ShortRecognitionResult("aaaaaaaa", "a", "a", DateTime.Now, user.Username);
            dbSRR.AddSRR(shortRecognition);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            User user = new User("2", "2");
            DbLRR dbLRR = new DbLRR();
            LiveRecognitionResult lrr = new LiveRecognitionResult("32d","fi","fdsa","fid",DateTime.Now,user.Username);
            Console.WriteLine(dbLRR.AddLRR(lrr));
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DbLRR dbLRR = new DbLRR();
            User user = new User("2", "2");

            dbLRR.GetLRRByUser(user);
            //Console.WriteLine(dbLRR.AddLRR(lrr));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DbLRR dbLRR = new DbLRR();
            dbLRR.DeleteLRR("32d");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            User user = new User("2", "2");
            DbFRR dbFRR = new DbFRR();
            FlashRecognitionResult frr = new FlashRecognitionResult("adsf","dsf","dfs","dsfa",DateTime.Now,user.Username);
            dbFRR.AddFRR(frr);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            User user = new User("2", "2");
            DbFRR dbFRR = new DbFRR();
            //dbFRR.GetFRRByUser(user);
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DbFRR dbFRR = new DbFRR();
            dbFRR.DeleteFRR("adsf");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            User user = new User("2", "2");

            DbSecret dbSecret = new DbSecret();
            dbSecret.GetSecByUser(user);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DbSecret dbSecret = new DbSecret();
            Secret secret = new Secret("fds","fdsg","fid","2");
            dbSecret.AddSec(secret);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DbSecret dbSecret = new DbSecret();
            Secret secret = new Secret("fds", "fdsgsd", "fid", "2");
            dbSecret.UpdateSec(secret);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_MouseHover(object sender, EventArgs e)
        {
            //Image image = Image.FromFile(@".\Resource\d.png");
            //flowLayoutPanel1.BackgroundImage = image;
            label_live.ForeColor = Color.FromArgb(39, 208, 216);
        }

        private void flowLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            //Image image = Image.FromFile(@".\Resource\c.png");
            //flowLayoutPanel1.BackgroundImage = image;
            label_live.ForeColor = Color.Black;
        }

        private void label_live_Click(object sender, EventArgs e)
        {
            LiveForm liveForm = new LiveForm(user);
            liveForm.ShowDialog();
            //this.Hide();
        }

        private void flowLayoutPanel2_Click(object sender, EventArgs e)
        {
            FlashForm flashForm = new FlashForm(user.Username);
            flashForm.ShowDialog();
        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel3_Click(object sender, EventArgs e)
        {
            VideoForm videoForm = new VideoForm(user.Username);
            videoForm.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            RecodeForm recodeForm = new RecodeForm();
            recodeForm.ShowDialog();
        }

        private void 退出登录ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Form1 mainform = new Form1();
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            Application.ExitThread();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"本程序基于C#编写，
采用腾讯云ASR语音识别技术
实现实时语音识别、录音文件
识别、视频加字幕、即时录音
等功能。如有问题，请联系作
者: admin@billyme.top", "关于");
        }
    }
}
