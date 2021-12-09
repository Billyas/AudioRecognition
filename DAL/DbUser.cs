using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.Model;

namespace AudioRecognition.DAL
{
    public class DbUser
    {
/*        private SqLiteHelper dbHelper;
        public DbUser()
        {
            dbHelper = new SqLiteHelper();
        }*/

        public List<User> getAllUsers()
        {
            using(var context = new ASRContext())
            {
                var users = context.Users.ToList();
                return users;
            }
        }

        public bool AddUser(User user)
        {
            using (var context = new ASRContext())
            {
                var hasusers = context.Users
                    .Count(u => u.Equals(user));
                if (hasusers!=0)
                {
                    return false;
                }
                else
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;
                }
            }
        }

/*        public bool AddUser2(User user) //采用原生插入比较快
        {
            string sql = "INSERT INTO Users (username, password) VALUES('" + user.Username + "','" + user.Password + "');";
            dbHelper.ExecuteQuery(sql);
            return true;
        }*/

        public bool VerifyUser(User user)
        {
            using (var context = new ASRContext())
            {
                var has = context.Users.Count(u => u.Equals(user));
                if (has != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }


    }
}
