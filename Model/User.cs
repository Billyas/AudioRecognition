using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.Model
{
    [Table("Users")]
    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public List<FlashRecognitionResult> FlashRecognitionResults { get; set; }
        public List<LiveRecognitionResult> LiveRecognitionResults { get; set; }
        public List<ShortRecognitionResult> ShortRecognitionResults { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Username == user.Username &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, Password);
        }

        public override string ToString()
        {
            return "username=" + Username + "; password=" + Password;
        }
    }
}
