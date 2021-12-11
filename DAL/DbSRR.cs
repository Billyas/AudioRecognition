using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.Model;

namespace AudioRecognition.DAL
{
    class DbSRR
    {
        public string[] DataToSQL(ShortRecognitionResult srr)
        {
            string[] res = new string[5];
            res[0] = srr.RequestId;
            res[1] = srr.AudioDuration;
            res[2] = srr.Result;
            res[3] = srr.Time.ToString("s");
            res[4] = srr.UserName;
            return res;
        }

        public List<ShortRecognitionResult> GetSRRByUser(User user)
        {
            SqLiteHelper sqLiteHelper = new SqLiteHelper();
            var res = sqLiteHelper.ExecuteQuery("select * from ShortRecognitionResults where username = '" + user.Username+"'");
            List<ShortRecognitionResult> shortRecognitionResults = new List<ShortRecognitionResult>();
            if (res.HasRows)
            {
                while (res.Read())
                {
                    ShortRecognitionResult tmp =
                        new ShortRecognitionResult(res.GetString(0), res.GetString(1), res.GetString(2), res.GetDateTime(3));


                    shortRecognitionResults.Add(tmp);
                }
                sqLiteHelper.CloseConnection();
                return shortRecognitionResults;
            }
            else
            {
                sqLiteHelper.CloseConnection();
                return null;
            }
        }

        public bool AddSRR(ShortRecognitionResult srr)
        {
            SqLiteHelper sq = new SqLiteHelper();
            var res = sq.ExecuteQuery("select * from ShortRecognitionResults where RequestId = '" + srr.RequestId + "'");
            if (res.HasRows)
            {
                sq.CloseConnection();
                return false;
            }
            else
            {
                bool flag = false;
                flag = sq.InsertItems("ShortRecognitionResults", DataToSQL(srr));
                sq.CloseConnection();
                return flag;
            }
            /*using (var context = new ASRContext())
            {
                List<ShortRecognitionResult> shortRecognitionResults = new List<ShortRecognitionResult>();
                shortRecognitionResults.Add(srr);
                try
                {
                    var has = context.Users.Count(e => e.Equals(user));
                    if (has != 0)
                    {
                        var getuser = context.Users.SingleOrDefault(e => e.Equals(user));
                        if (getuser.ShortRecognitionResults == null)
                        {
                            List<ShortRecognitionResult> shorts = new List<ShortRecognitionResult>();
                            shorts.Add(srr);
                            getuser.ShortRecognitionResults = shorts;
                        }
                        else
                        {
                            getuser.ShortRecognitionResults.Add(srr);
                        }
                        if (context.SaveChanges() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

            }*/
        }

        public bool DeleteSRR(string RequestId)
        {
            SqLiteHelper sqLiteHelper = new SqLiteHelper();
            bool f= sqLiteHelper.DeleteValues("ShortRecognitionResult", "RequestId", RequestId);
            sqLiteHelper.CloseConnection();
            return f;
        }
    }
}
