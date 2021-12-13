using System;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Asr.V20190614;
using TencentCloud.Asr.V20190614.Models;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Configuration;
/// <summary>
//1.接口描述
//本接口支持使用者通过 HTTPS POST 方式上传一段音频并在极短时间内同步返回识别结果，可满足音视频字幕、准实时质检等场景下对语音文件识别时效性的要求。

//在使用该接口前，需要在语音识别控制台开通服务，并进入 API 密钥管理页面 新建密钥，生成 AppID、SecretID 和 SecretKey，用于 API 调用时生成签名，签名将用来进行接口鉴权。

//2. 接口要求
//集成录音识别极速版 API 时，需按照以下要求。

//内容	说明
//支持语言	中文普通话
//音频格式	wav、pcm、ogg-opus、speex、silk、mp3、m4a、aac
//使用限制	支持100MB以内音频文件的识别
//请求协议	HTTPS
//请求地址	https://asr.cloud.tencent.com/asr/flash/v1/<appid>?{请求参数}
//接口鉴权 签名鉴权机制，详见 签名生成
//响应格式	统一采用 JSON 格式
//并发限制	默认单账号限制并发数为3路，如您有提高并发限制的需求，可通过 售后支持 咨询。
/// </summary>
/// 

//AppId 是	Integer	用户在腾讯云注册账号的 AppId，可以进入 API 密钥管理页面 获取。
//secretid	是	String	用户在腾讯云注册账号 AppId 对应的 SecretId，可以进入 API 密钥管理页面 获取。
//engine_type	是	String	引擎模型类型。
//8k_zh：8k 中文普通话通用；
//16k_zh：16k 中文普通话通用；
//16k_en：16k 英语；
//16k_zh_video：16k 音视频领域。
//voice_format	是	String	音频格式。支持 wav、pcm、ogg-opus、speex、silk、mp3、m4a、aac。
//timestamp	是	Integer	当前 UNIX 时间戳，如果与当前时间相差超过3分钟，会报签名失败错误。
//speaker_diarization	否	Integer	是否开启说话人分离（目前支持中文普通话引擎），默认为0，0：不开启，1：开启。
//filter_dirty	否	Integer	是否过滤脏词（目前支持中文普通话引擎），默认为0。0：不过滤脏词；1：过滤脏词；2：将脏词替换为 *。
//filter_modal	否	Integer	是否过滤语气词（目前支持中文普通话引擎），默认为0。0：不过滤语气词；1：部分过滤；2：严格过滤。
//filter_punc	否	Integer	是否过滤标点符号（目前支持中文普通话引擎），默认为0。0：不过滤，1：过滤句末标点，2：过滤所有标点。
//convert_num_mode	否	Integer	是否进行阿拉伯数字智能转换，默认为1。0：全部转为中文数字；1：根据场景智能转换为阿拉伯数字。
//word_info	否	Integer	是否显示词级别时间戳，默认为0。0：不显示；1：显示，不包含标点时间戳，2：显示，包含标点时间戳。
//first_channel_only	否	Integer	是否只识别首个声道，默认为1。0：识别所有声道；1：识别首个声道。
namespace AudioRecognition
{
    class FlashSend
    {
        int appid = int.Parse(ConfigurationManager.AppSettings["APPID"]);
        string SecretId = ConfigurationManager.AppSettings["SECRET_ID"];
        string SecretKey = ConfigurationManager.AppSettings["SECRET_KEY"];

        const  string urlhead = "https://asr.cloud.tencent.com/asr/flash/v1/";
        //head
        const string Host = "asr.cloud.tencent.com";
        string Authorization;
        const string Content_Type = "application/octet-stream";
        int Content_Length = 0; //请求长度，此处对应语音数据字节数，单位：字节

        // 参数
        string engine_type = "16k_zh";
        string voice_format = "wav";
        long timestap = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
       
        //int speaker_diarization = 0;

        //string audio = "D:\\-\\VisualStudio\\WinFormApp\\AudioToSRT\\AudioToSRT\\test_wav\\a.mp3";

        //初始化url
        string initurl()
        {
            string url = urlhead + appid + "?convert_num_mode=" + "1"
                             + "&engine_type=" + engine_type
                             + "&filter_dirty=0"
                             + "&filter_modal=0"
                             + "&filter_punc=0"
                             + "&first_channel_only=1"
                             + "&hotword_id="
                             + "&secretid=" + SecretId
                             + "&speaker_diarization=0"
                             + "&timestamp=" + timestap
                             + "&voice_format=" + voice_format
                             + "&word_info=0";
            //string url = urlhead + appid +"?convert_num_mode="+1
            //    +"&engine_type="+engine_type
            //    +"&secretid="+SecretId
            //    +"&timestamp="+timestap
            //    +"&voice_format="+voice_format
            //    +"&word_info=0";
            Console.WriteLine(url);
            return url;

        }


        //url签名加密
        string signedurl()
        {
            string signstr = "POSTasr.cloud.tencent.com/asr/flash/v1/";
            string url = signstr + appid + "?convert_num_mode=" + "1"
                                         + "&engine_type=" + engine_type
                                         + "&filter_dirty=0"
                                         + "&filter_modal=0"
                                         + "&filter_punc=0"
                                         + "&first_channel_only=1"
                                         + "&hotword_id="
                                         + "&secretid=" + SecretId
                                         + "&speaker_diarization=0"
                                         + "&timestamp=" + timestap
                                         + "&voice_format=" + voice_format
                                         + "&word_info=0";
            Console.WriteLine(url);
            HMACSHA1 hMACSHA1 = new HMACSHA1(System.Text.Encoding.UTF8.GetBytes(SecretKey));
            //hMACSHA1.Key = ;
            byte[] res = hMACSHA1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(url));
            string a = Convert.ToBase64String(res);
            Console.WriteLine(a);
            
            return a;
        }

        public static byte[] FileToByte(string fileUrl)
        {
            try
            {
                using (FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read))
                {
                    byte[] byteArray = new byte[fs.Length];
                    fs.Read(byteArray, 0, byteArray.Length);
                    return byteArray;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        string uploadREC(string filepath)
        {
            int DataRead = 0;
            string Authorization = signedurl();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(initurl());
            try
            {
                FileStream ReadIn = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                req.Method = "POST";
                req.ContentType = Content_Type;
                req.Host = Host;
                req.ContentLength = ReadIn.Length;
                req.Headers["Authorization"] = Authorization;
                req.AllowReadStreamBuffering = false;
                //req.Headers.Add("Authorization", Authorization);
                ReadIn.Seek(0, SeekOrigin.Begin); // Move to the start of the file.
            
                Stream tempStream = req.GetRequestStream();

                byte[] FileData = new byte[ReadIn.Length];
                do
                {
                    DataRead = ReadIn.Read(FileData, 0, 2048);
                    if (DataRead > 0) //we have data
                    {
                        tempStream.Write(FileData, 0, DataRead);
                        Array.Clear(FileData, 0, 2048); // Clear the array.
                    }
                } while (DataRead > 0);
                //Stream postStream = req.GetRequestStream();
                //postStream.Write(data, 0, data.Length);
                //postStream.Close();

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                tempStream.Close();
                ReadIn.Close();
                stream.Close();
                Console.WriteLine(result);
                return result;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                
                return null;
            }
        }

        public string FlashRRun(string filename)
        {
            if (filename != "")
            {
                string extension = Path.GetExtension(filename);
                this.voice_format = extension.Split('.')[1];
                return uploadREC(filename);
            }
            else
            {
                return "error file name";
            }
            
        }

        public void test()
        {
            string audio = "D:\\-\\Visual Studio 2019\\AudioToSRT\\AudioToSRT\\bin\\Debug\\record.wav";
            //string testAudio = "http://d.billyme.top/bt/16k.wav";
            //byte[] rawdata = FileToByte(audio);
            uploadREC(audio);
        }

    }
}
