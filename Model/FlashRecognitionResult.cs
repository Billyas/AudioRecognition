using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.Model
{ 
    [Table("FlashRecognitionResults")]
    public class FlashRecognitionResult
    {
        public FlashRecognitionResult(string request_id, string audio_duration, string message, string flash_result)
        {
            Request_id = request_id;
            Audio_duration = audio_duration;
            Message = message;
            Flash_result = flash_result;
        }

        [Key]
        public string Request_id { get; set; }
        public string Audio_duration { get; set; }
        public string Message { get; set; }
        public string Flash_result { get; set; }
        //[ForeignKey("FK_user_FRR")]
        [Required]
        public  User User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }



        public override string ToString()
        {
            return "request_id=" + Request_id 
                + "; audio_duration=" +Audio_duration
                + "; message=" + Message
                + "; flash_result=" + Flash_result;
        }
    }
}
