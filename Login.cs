using AudioRecognition.BBL;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void usernameInput_TextChanged(object sender, EventArgs e)
        {

        }


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string user = usernameInput.Text;
            string pwd = passwordInput.Text;
            UserService userService = new UserService();
            if (userService.VerifyUser(user, pwd))
            {
                Form1 mainform = new Form1();
                this.Hide();
                mainform.ShowDialog();
                Application.ExitThread();
            }
            else
            {
                MessageBox.Show("登录失败", "用户名或密码错误！");
                usernameInput.Text = "";
                passwordInput.Text = "";
                usernameInput.Focus();
            }

            //DbUser dbUser = new DbUser();
            //if (dbUser.Veryfiyuser(user, pwd))
            //{
            //    //实现页面跳转
            //    Form1 mainform = new Form1();
            //    this.Hide();     //隐藏当前窗体   
            //    mainform.ShowDialog();
            //    Application.ExitThread();
            //}
            //else
            //{
            //    MessageBox.Show("用户名或密码错误", "登陆失败");

            //}

        }



        private void buttonRegist_Click(object sender, EventArgs e)
        {
            string user = usernameInput.Text;
            string pwd = passwordInput.Text;
            UserService userService = new UserService();
            if(userService.AddUser(user, pwd))
            {
                MessageBox.Show("注册成功", "请登录");
            }
            else
            {
                MessageBox.Show("注册失败", "用户名已存在！");
            }

        }
    }
}
