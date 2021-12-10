using AudioRecognition.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            using (var context = new ASRContext())
            {
                var users = context.Users.ToList();
                return users;
            }
        }

        public bool AddUser(User user)
        {

            string sql = "select * from Users where username = '" + user.Username + "'";
            string sql2 = "INSERT INTO Users (username, password) VALUES('" + user.Username + "','" + user.Password + "');";

            SqLiteHelper sq = new SqLiteHelper();
            var has = sq.ExecuteQuery(sql);

            if (!has.HasRows)
            {
                sq.ExecuteQuery(sql2);
                return true;
            }
            else
            {
                return false;
            }
            //string sql = "INSERT INTO Users (username, password) VALUES('" + user.Username + "','" + user.Password + "');";


/*            using (var context = new ASRContext())
            {
                try
                {
                    var hasusers = context.Users
                    .Count(u => u.Equals(user));
                    if (hasusers != 0)
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
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

            }*/
        }

        /*        public bool AddUser2(User user) //采用原生插入比较快
                {
                    string sql = "INSERT INTO Users (username, password) VALUES('" + user.Username + "','" + user.Password + "');";
                    dbHelper.ExecuteQuery(sql);
                    return true;
                }*/

        public bool VerifyUser(User user)
        {
            //using (var context = new ASRContext())
            //{
            //    var has = context.Users.Count(u => u.Equals(user));
            //    if (has != 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            bool flag = false;
            string sql = "select * from Users where username= '" + user.Username + "'and password='" + user.Password + "'";
            SqLiteHelper sqLiteHelper = new SqLiteHelper();
            var res = sqLiteHelper.ExecuteQuery(sql);
            if (res.HasRows)
            {
                flag = true;
            }
            sqLiteHelper.CloseConnection();
            return flag;
        }

        //public User GetUser(string username)
        //{
        //    //using (var context = new ASRContext())
        //    //{
        //    //    try
        //    //    {
        //    //        var has = context.Users.Count(u => u.Username.Equals(username));
        //    //        if (has != 0)
        //    //        {
        //    //            return context.Users.Single(u => u.Username.Equals(username));
        //    //        }
        //    //        else
        //    //        {
        //    //            return null;
        //    //        }
        //    //    }
        //    //    catch (Exception e)
        //    //    {
        //    //        Console.WriteLine(e);
        //    //        return null;
        //    //    }
        //    //}

        //}


    }
}
