using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.Model;

namespace AudioRecognition.DAL
{
    class DbFRR
    {
        public string[] Data2string(FlashRecognitionResult frr)
        {
            string[] res = new string[6];
            res[0] = frr.Request_id;
            res[1] = frr.Audio_duration;
            res[2] = frr.Message;
            res[3] = frr.Flash_result;
            res[4] = frr.Time.ToString("s");
            res[5] = frr.UserName;
            return res;
        }

        public List<FlashRecognitionResult> GetFRRByUser(User user)
        {
            SqLiteHelper sqLiteHelper = new SqLiteHelper();
            var res = sqLiteHelper.ExecuteQuery("select * from FlashRecognitionResults where username = '" + user.Username + "'");
            List<FlashRecognitionResult> flashRecognitionResults = new List<FlashRecognitionResult>();
            if (res.HasRows)
            {
                while (res.Read())
                {
                    FlashRecognitionResult tmp =
                        new FlashRecognitionResult(res.GetString(0), res.GetString(1), res.GetString(2), res.GetString(3), res.GetDateTime(4), res.GetString(5));
                    flashRecognitionResults.Add(tmp);
                }
                sqLiteHelper.CloseConnection();
                return flashRecognitionResults;
            }
            else
            {
                sqLiteHelper.CloseConnection();
                return null;
            }
        }

        public bool AddFRR(FlashRecognitionResult frr)
        {
            SqLiteHelper sq = new SqLiteHelper();
            var res = sq.ExecuteQuery("select * from FlashRecognitionResults where Request_id = '" + frr.Request_id + "'");
            if (res.HasRows)
            {
                sq.CloseConnection();
                return false;
            }
            else
            {
                bool flag;
                flag = sq.InsertItems("FlashRecognitionResults", Data2string(frr));
                sq.CloseConnection();
                return flag;
            }
        }

        public bool DeleteFRR(string id)
        {
            SqLiteHelper sq = new SqLiteHelper();
            bool res = sq.DeleteValues("FlashRecognitionResults", "Request_id", id);
            sq.CloseConnection();
            return res;
        }

    }
}
