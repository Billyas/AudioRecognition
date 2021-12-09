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
        public LiveRecognitionResult(string voice_id, string message, string messageId, string result)
        {
            Voice_id = voice_id;
            Message = message;
            MessageId = messageId;
            Result = result;
        }

        [Key]
        public string Voice_id { get; set; }
        public string Message { get; set; }
        public string MessageId { get; set; }
        public string Result { get; set; }
        
        [Required]
        public User User { get; set; }

    }
}
