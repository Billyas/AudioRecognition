using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.Model
{
    [Table("LiveRecognitionResults")]
    public class LiveRecognitionResult
    {
        public LiveRecognitionResult()
        {

        }
        public LiveRecognitionResult(string voice_id, string message, string messageId, string result, DateTime time)
        {
            Voice_id = voice_id;
            Message = message;
            MessageId = messageId;
            Result = result;
            Time = time;
        }

        public LiveRecognitionResult(string voice_id, string message, string messageId, string result, DateTime time, string userName)
        {
            Voice_id = voice_id;
            Message = message;
            MessageId = messageId;
            Result = result;
            Time = time;
            UserName = userName;
        }

        [Key]
        public string Voice_id { get; set; }
        public string Message { get; set; }
        public string MessageId { get; set; }
        public string Result { get; set; }

        public DateTime Time { get; set; }
        
        public string UserName { get; set; }

    }
}
