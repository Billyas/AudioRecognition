using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.Model;

namespace AudioRecognition.DAL
{
    class DbSRR
    {
        public List<ShortRecognitionResult> GetSRRByUser(User user)
        {
            using (var context = new ASRContext())
            {
                var shortrecognitionresults = context.Users
                    .Single(e=>e.Equals(user))
                    .ShortRecognitionResults;
                //var res = (from c in context.shortRecognitionResults where c.User.Equals(user) select c).ToList();
                //foreach(var i in res)
                //{
                //    Console.WriteLine(i.ToString());
                //}
                return shortrecognitionresults;
            }
        }

        public bool AddSRR(ShortRecognitionResult srr, User user)
        {
            using (var context = new ASRContext())
            {
                List<ShortRecognitionResult> shortRecognitionResults = new List<ShortRecognitionResult>();
                shortRecognitionResults.Add(srr);
                try
                {
                    var has = context.Users.Count(e => e.Equals(user));
                    if (has != 0)
                    {
                        var getuser=context.Users.SingleOrDefault(e => e.Equals(user));
                        if(getuser.ShortRecognitionResults == null)
                        {
                            List<ShortRecognitionResult> shorts = new List<ShortRecognitionResult>();
                            shorts.Add(srr);
                            getuser.ShortRecognitionResults = shorts;
                        }
                        else
                        {
                            getuser.ShortRecognitionResults.Add(srr);
                        }
                        if (context.SaveChanges() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;

                }catch(Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

            }
        }
    }
}
