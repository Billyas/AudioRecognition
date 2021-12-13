using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.DAL;
using AudioRecognition.Model;
using System.Data;

namespace AudioRecognition.BLL
{
    class FlashService
    {
        public bool AddFRRbyUser(JObject json, string username)
        {
            if (json != null)
            {
                FlashRecognitionResult frr = new FlashRecognitionResult();
                frr.Request_id = json["request_id"].ToString();
                frr.Audio_duration = json["audio_duration"].ToString();
                frr.Message = json["message"].ToString();
                frr.Flash_result = json["flash_result"].ToString();
                frr.Time = DateTime.Now;
                frr.UserName = username;
                DbFRR dbFRR = new DbFRR();
                return dbFRR.AddFRR(frr);
            }
            else
            {
                return false;
            }
        }

        public DataSet GetFRRbyUser(string username)
        {
            DbFRR dbFRR = new DbFRR();
            return dbFRR.GetFRRByUserName(username);
        }

    }
}
