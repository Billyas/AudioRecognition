using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.Model
{
    [Table("ShortRecognitionResults")]
    public class ShortRecognitionResult
    {
        public ShortRecognitionResult()
        {

        }
        public ShortRecognitionResult(string requestId, string audioDuration, string result, DateTime time)
        {
            RequestId = requestId;
            AudioDuration = audioDuration;
            Result = result;
            Time = time;
        }

        public ShortRecognitionResult(string requestId, string audioDuration, string result, DateTime time, string userName)
        {
            RequestId = requestId;
            AudioDuration = audioDuration;
            Result = result;
            Time = time;
            UserName = userName;
        }

        [Key]
        public string RequestId { get; set; }
        public string AudioDuration { get; set; }
        public string Result { get; set; }

        public DateTime Time { get; set; }

        public string UserName { get; set; }

        public User User { get; set; }


        public override string ToString()
        {
            return "requestID = "+RequestId+ "; audioDuration" +AudioDuration+ "; result"+Result;
        }
    }
}
