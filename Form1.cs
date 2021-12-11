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
    }
}
