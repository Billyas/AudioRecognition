using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.Model;
using Newtonsoft.Json.Linq;
using AudioRecognition.DAL;
using System.Data;

namespace AudioRecognition.BLL
{
    class LRRservice
    {
        public bool AddLRR(JObject json, string username)
        {
            if (json != null)
            {
                LiveRecognitionResult liveRecognitionResult = new LiveRecognitionResult();
                liveRecognitionResult.Voice_id = json["voice_id"].ToString();
                liveRecognitionResult.Message = json["message"].ToString();
                liveRecognitionResult.MessageId = json["message_id"].ToString();
                liveRecognitionResult.Result = json["result"]["voice_text_str"].ToString();
                liveRecognitionResult.Time = DateTime.Now;
                liveRecognitionResult.UserName = username;
                DbLRR dbLRR = new DbLRR();
                return dbLRR.AddLRR(liveRecognitionResult);
            }
            else
            {
                return false;
            }
        }

        public DataSet GetLRRData(string username)
        {
            DbLRR dbLRR = new DbLRR();
            return dbLRR.GetLRRByUserName(username);
        }

    }
}
