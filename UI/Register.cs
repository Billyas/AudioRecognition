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

namespace AudioRecognition.UI
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void buttonRegist_Click(object sender, EventArgs e)
        {
            string user = usernameInput.Text;
            string pwd = passwordInput.Text;
            UserService userService = new UserService();
            if (userService.AddUser(user, pwd))
            {
                MessageBox.Show("请登录", "注册成功");
            }
            else
            {
                MessageBox.Show("用户名已存在！", "注册失败");
            }
        }
    }
}
