using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AudioRecognition.DAL;
using AudioRecognition.Model;

namespace AudioRecognition.BLL
{
    class UserService
    {
        private string pattern = @"^[0-9a-zA-Z_]{1,10}$";
        public bool VerifyUser(string username, string passsword)
        {
            if(Regex.IsMatch(username, pattern))
            {
                User user = new User(username, passsword);
                DbUser dbUser = new DbUser();
                return dbUser.VerifyUser(user);
            }
            else
            {
                return false;
            }
        }

        public bool AddUser(string username, string password)
        {
            if(Regex.IsMatch(username, password))
            {
                User user = new User(username, password);
                DbUser dbUser = new DbUser();
                return dbUser.AddUser(user);
            }
            else
            {
                return false;
            }
        }
    }
}
