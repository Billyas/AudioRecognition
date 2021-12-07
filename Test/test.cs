using System;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Asr.V20190614;
using TencentCloud.Asr.V20190614.Models;
using System.IO;
using System.Configuration;
/// <summary>
// 腾讯云语音识别一句话识别测试
// 接口请求域名： asr.tencentcloudapi.com 。
//本接口用于对60秒之内的短音频文件进行识别。
//• 支持中文普通话、英语、粤语、日语、上海话方言。
//• 支持本地语音文件上传和语音URL上传两种请求方式，音频时长不能超过60s，音频文件大小不能超过3MB。
//• 音频格式支持wav、mp3；采样率支持8000Hz或者16000Hz；采样精度支持16bits；声道支持单声道。
//• 请求方法为 HTTP POST , Content-Type为"application/json; charset=utf-8"
//• 签名方法参考 公共参数 中签名方法v3。
//默认接口请求频率限制：25次/秒。
/// </summary>
namespace AudioRecognition
{
    class SentenceRecognition
    {
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

        public void test()
        {
            try
            {
                Credential cred = new Credential
                {
                    SecretId = ConfigurationManager.AppSettings["SECRET_ID"],
                    SecretKey = ConfigurationManager.AppSettings["SECRET_KEY"]
                };

                ClientProfile clientProfile = new ClientProfile();
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.Endpoint = ("asr.tencentcloudapi.com");
                clientProfile.HttpProfile = httpProfile;

                AsrClient client = new AsrClient(cred, "", clientProfile);
                SentenceRecognitionRequest req = new SentenceRecognitionRequest();
                string audio = "D:\\-\\VisualStudio\\WinFormApp\\AudioToSRT\\AudioToSRT\\test_wav\\16k.wav";
                string testAudio = "http://d.billyme.top/bt/16k.wav";

                byte[] rawdata = FileToByte(audio);
                string data = Convert.ToBase64String(rawdata);
                int rawdataLen = rawdata.Length;

                req.ProjectId = 0;
                req.SubServiceType = 2;
                req.EngSerViceType = "16k_zh";
                req.SourceType = 1;
                req.VoiceFormat = "wav";
                req.UsrAudioKey = "key";
                req.Data = data;
                //req.Url = testAudio;
                req.DataLen = rawdataLen;
                
                SentenceRecognitionResponse resp = client.SentenceRecognitionSync(req);
                Console.WriteLine(AbstractModel.ToJsonString(resp));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /*public static void Main(string[] args)
        {
            try
            {
                Credential cred = new Credential
                {
                    SecretId = "AKID3vYuzoNYT7VcS2KM6ZlgLiqhHacOu97o",
                    SecretKey = "ScV8UZNvCCvMYT7TZOxFH2VhcGdBSTDg"
                };

                ClientProfile clientProfile = new ClientProfile();
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.Endpoint = ("asr.tencentcloudapi.com");
                clientProfile.HttpProfile = httpProfile;

                AsrClient client = new AsrClient(cred, "", clientProfile);
                SentenceRecognitionRequest req = new SentenceRecognitionRequest();
                req.ProjectId = 0;
                req.SubServiceType = 2;
                req.EngSerViceType = "16k_zh";
                req.SourceType = 1;
                req.VoiceFormat = "wav";
                req.UsrAudioKey = "key";
                SentenceRecognitionResponse resp = client.SentenceRecognitionSync(req);
                Console.WriteLine(AbstractModel.ToJsonString(resp));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.Read();
        }*/
    }
}
