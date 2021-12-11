using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.Model;

namespace AudioRecognition.DAL
{
    class DbSecret
    {
        public string[] Data2string(Secret sec)
        {
            string[] res = new string[4];
            res[0] = sec.APPID;
            res[1] = sec.SECRET_ID;
            res[2] = sec.SECRET_KEY;
            res[3] = sec.UserName;
            return res;
        }

        public string[] GetColName()
        {
            string[] colNames = new string[4];
            colNames[0] = "APPID";
            colNames[1] = "SECRET_ID";
            colNames[2] = "SECRET_KEY";
            colNames[3] = "UserName";
            return colNames;
        }


        public Secret GetSecByUser(User user)
        {
            SqLiteHelper sqLiteHelper = new SqLiteHelper();
            var res = sqLiteHelper.ExecuteQuery("select * from Secrets where username = '" + user.Username + "'");
            
            if (res.HasRows)
            {
                res.Read();
                Secret tmp =
                        new Secret(res.GetString(1), res.GetString(2), res.GetString(3), res.GetString(4));
                
                sqLiteHelper.CloseConnection();
                return tmp;
            }
            else
            {
                sqLiteHelper.CloseConnection();
                return null;
            }
        }

        public bool AddSec(Secret secret)
        {
            SqLiteHelper sq = new SqLiteHelper();
            var res = sq.ExecuteQuery("select * from Secrets  where UserName = '" + secret.UserName + "'");
            if (res.HasRows)
            {
                sq.CloseConnection();
                return false;
            }
            else
            {
                bool flag;
                flag = sq.InsertItems2("Secrets", GetColName(), Data2string(secret));
                sq.CloseConnection();
                return flag;
            }
        }

        public bool UpdateSec(Secret secret)
        {
            SqLiteHelper sq = new SqLiteHelper();
            var res = sq.ExecuteQuery("select * from Secrets  where UserName = '" + secret.UserName + "'");
            if (res.HasRows)
            {
                bool flag;
                flag=sq.UpdateValues2("Secrets", GetColName(), Data2string(secret), "UserName", secret.UserName);
                sq.CloseConnection();
                return flag;
            }
            else
            {
                sq.CloseConnection();
                return false;
            }
        }

    }
}
