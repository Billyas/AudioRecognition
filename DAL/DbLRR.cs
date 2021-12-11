using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.Model;
namespace AudioRecognition.DAL
{
    class DbLRR
    {
        public string[] Data2string(LiveRecognitionResult lrr)
        {
            string[] res = new string[6];
            res[0] = lrr.Voice_id;
            res[1] = lrr.Message;
            res[2] = lrr.MessageId;
            res[3] = lrr.Result;
            res[4] = lrr.Time.ToString("s");
            res[5] = lrr.UserName;
            return res;
        }
        public List<LiveRecognitionResult> GetLRRByUser(User user)
        {
            SqLiteHelper sqLiteHelper = new SqLiteHelper();
            var res = sqLiteHelper.ExecuteQuery("select * from LiveRecognitionResults where username = '" + user.Username + "'");
            List<LiveRecognitionResult> liveRecognitionResults = new List<LiveRecognitionResult>();
            if (res.HasRows)
            {
                while (res.Read())
                {
                    LiveRecognitionResult tmp =
                        new LiveRecognitionResult(res.GetString(0),res.GetString(1), res.GetString(2),res.GetString(3), res.GetDateTime(4), res.GetString(5));
                    liveRecognitionResults.Add(tmp);
                }
                sqLiteHelper.CloseConnection();
                return liveRecognitionResults;
            }
            else
            {
                sqLiteHelper.CloseConnection();
                return null;
            }
        }

        public bool AddLRR(LiveRecognitionResult liveRecognitionResult)
        {
            SqLiteHelper sq = new SqLiteHelper();
            var res = sq.ExecuteQuery("select * from LiveRecognitionResults where Voice_id = '" + liveRecognitionResult.Voice_id + "'");
            if (res.HasRows)
            {
                sq.CloseConnection();
                return false;
            }
            else
            {
                bool flag;
                flag = sq.InsertItems("LiveRecognitionResults", Data2string(liveRecognitionResult));
                sq.CloseConnection();
                return flag;
            }
        }
    }
}
