using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.BLL
{
    class Json2SRT
    {
        public static String formatLongToTimeStr(long l)
        {
            String str = "";
            int hour = 0;
            int minute = 0;
            int second = 0;
            second = (int)(l / 1000);
            int milisecond = (int)(l % 1000);
            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }


            return (hour.ToString() + "小时" + minute.ToString() + "分钟"
                + second.ToString() + "秒");
        }

        public  string milisecond2Time(string time)
        {
            double milisecond;
            if (!string.IsNullOrEmpty(time))
            {
                milisecond = double.Parse(time);
                DateTime dateTime = DateTime.Parse(DateTime.Now.ToString(format: "1970-01-01       00:00:00")).AddMilliseconds(milisecond);
                return string.Format("{0:HH:mm:ss,fff}", dateTime);
            }
            else
            {
                return null;
            }
        }
        public string convertJon2SRT(JObject json)
        {
            string res="";
            if (json["code"].ToString().Equals("0")) //成功获取到信息
            {
                int length = json["flash_result"].First()["sentence_list"].Count();
                JToken s = json["flash_result"].First()["sentence_list"];
                for (int i=0; i<length; i++)
                {
                    res += i.ToString() + Environment.NewLine;
                    string time_start = milisecond2Time(s.ElementAt(i)["start_time"].ToString());
                    string end_time = milisecond2Time(s.ElementAt(i)["end_time"].ToString());
                    res += time_start + " --> " + end_time + Environment.NewLine;
                    res += s.ElementAt(i)["text"].ToString() + Environment.NewLine + Environment.NewLine;
                }
                return res;
            }
            else
            {
                return null;
            }
        }

        public string String2SRT(string str)
        {
            string res = "";
            JObject json = JObject.Parse(str);
            if (json["sentence_list"]!=null) //成功获取到信息
            {
                int length = json["sentence_list"].Count();
                JToken s = json["sentence_list"];
                for (int i = 0; i < length; i++)
                {
                    res += i.ToString() + Environment.NewLine;
                    string time_start = milisecond2Time(s.ElementAt(i)["start_time"].ToString());
                    string end_time = milisecond2Time(s.ElementAt(i)["end_time"].ToString());
                    res += time_start + " --> " + end_time + Environment.NewLine;
                    res += s.ElementAt(i)["text"].ToString() + Environment.NewLine + Environment.NewLine;
                }
                return res;
            }
            else
            {
                return null;
            }
        }
    }
}
