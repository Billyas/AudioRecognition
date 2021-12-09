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
        public ShortRecognitionResult(string requestId, string audioDuration, string result)
        {
            RequestId = requestId;
            AudioDuration = audioDuration;
            Result = result;
        }

        [Key]
        public string RequestId { get; set; }
        public string AudioDuration { get; set; }
        public string Result { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }
        [Required]
        public User User { get; set; }

        public override string ToString()
        {
            return "requestID = "+RequestId+ "; audioDuration" +AudioDuration+ "; result"+Result;
        }
    }
}
