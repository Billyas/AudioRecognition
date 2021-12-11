using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.Model
{
    class Secret
    {
        public Secret()
        {

        }
        public Secret(string aPPID, string sECRET_ID, string sECRET_KEY, string userName)
        {
            APPID = aPPID;
            SECRET_ID = sECRET_ID;
            SECRET_KEY = sECRET_KEY;
            UserName = userName;
        }

        public string APPID { get; set; }
        public string SECRET_ID { get; set; }
        public string SECRET_KEY { get; set; }
        public string UserName { get; set; }
    }
}
