using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.NBitcoin;
using System.Configuration;

namespace AudioRecognition
{
    class Testliv
    {
        //官网文档：https://cloud.tencent.com/document/product/1093/48982
        static string File = @"D:\-\VisualStudio\WinFormApp\AudioToSRT\AudioToSRT\test_wav\16k.wav";//模拟实时音频的文件路劲
        static ClientWebSocket client = new ClientWebSocket();//实例化客户端对象
        static int cutlegth = 1280;//建议每40ms发送40ms时长（即1:1实时率）的数据包，对应8k采样率为640字节，16k采样率为1280字节
        private static string Msg = "";
        public string msg = "";
        //public static string Msg { get => Msg; set => Msg = value; }

        //static private byte[] data;

        //public byte[] Data { get => data; set => data = value; }

        static Form1 f = null;

        public void setform(Form1 ff)
        {
            f = ff;
        }

        public void setmsg()
        {
            this.msg = Msg;
        }

        public  void go()
        {
            // 密钥参数
            string SECRET_ID = ConfigurationManager.AppSettings["SECRET_ID"];
            string SECRET_KEY = ConfigurationManager.AppSettings["SECRET_KEY"]; 
            string APPID = ConfigurationManager.AppSettings["APPID"]; 

            //实时语音参数
            ///参数设置
            string Host = "wss://asr.cloud.tencent.com/asr/v2/";
            var engine_model_type = "16k_zh";
            var voice_format = "1"; // 语音编码方式，可选，默认值为4。1：pcm；4：speex(sp)；6：silk；8：mp3；12：wav；14：m4a（每个分片须是一个完整的 m4a 音频）；16：aac。
            var needvad = "0";   //如果音频流总时长超过60秒，用户需开启 vad。0：关闭 vad，1：开启 vad。
            var hotword_id = string.Empty;//热词ID
            var vad_silence_time = 500; //语音断句检测阈值，静音时长超过该阈值会被认为断句（多用在智能客服场景，需配合 needvad=1 使用），取值范围150-2000，单位 ms，目前仅支持 8k_zh 引擎模型
            var filter_dirty = "0";
            var filter_modal = "0";
            var filter_punc = "0";
            var word_info = "0";
            var convert_num_mode = "1";//是否进行阿拉伯数字智能转换

            Random r = new Random();
            SortedDictionary<string, string> param = new SortedDictionary<string, string>();
            param.Add("secretid", SECRET_ID);
            param.Add("timestamp", GetTimeStamp());
            param.Add("expired", GetTimeStampExpired());
            param.Add("nonce", r.Next(1, 100000).ToString());
            param.Add("engine_model_type", engine_model_type);
            param.Add("voice_id", getRandomString(18));
            param.Add("voice_format", voice_format);
            param.Add("needvad", needvad);
            if (!string.IsNullOrEmpty(hotword_id))
            {
                param.Add("hotword_id", hotword_id);
            }
            //自学习id
            param.Add("filter_dirty", filter_dirty);
            param.Add("filter_modal", filter_modal);
            param.Add("filter_punc", filter_punc);
            param.Add("convert_num_mode", convert_num_mode);//是否进行阿拉伯数字智能转换
            param.Add("word_info", word_info);
            //查询是否设置语音断句检测阈值 需配合 needvad=1 使用，取值范围150-2000，目前仅支持8k_zh
            if (vad_silence_time >= 150 && vad_silence_time <= 2000 && param["needvad"] == "1" && param["engine_model_type"] == "8k_zh")
            {
                param.Add("vad_silence_time", vad_silence_time.ToString());
            }

            //1.拼接生成的签名原文 
            string SignText = MakeSignPlainText(param, APPID);

            //2.计算签名+urlencode
            string signatureStr = CreateSign(SECRET_KEY, SignText);

            //3.拼接请求URL
            var url = MakeUrl(param, APPID, Host, signatureStr);

            //4.握手并模拟音频请求wss接口
            MakeHandAndSendData(url);

            //Console.ReadKey();
        }

        /// <summary>
        /// 1.拼接生成签名原文 
        /// </summary>
        /// <param name="requestParams">请求参数</param>
        /// <param name="APPID">腾讯云APPID</param>
        /// <param name="service">asr</param>
        /// <returns></returns>
        public static string MakeSignPlainText(SortedDictionary<string, string> requestParams, string APPID)
        {
            string retStr = "";
            retStr += "asr.cloud.tencent.com/asr/v2/" + APPID;
            retStr += "?";
            string v = string.Empty;
            foreach (string key in requestParams.Keys)
            {
                v += string.Format("{0}={1}&", key, requestParams[key]);
            }
            retStr += v.TrimEnd('&');
            return retStr;
        }

        /// <summary>
        /// 2.对签名原文使用 SecretKey 进行 HmacSha1 加密，之后再进行 base64 编码  得到 signature 签名值
        /// </summary>
        /// <param name="signKey">SecretKey</param>
        /// <param name="retStr">签名原文</param>
        /// <returns></returns>
        public static string CreateSign(string signKey, string retStr)
        {
            string signRet = string.Empty;
            using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(signKey)))
            {
                byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(retStr));
                var strS = Convert.ToBase64String(hash);
                signRet = HttpUtility.UrlEncode(strS);
            }
            return signRet;
        }

        /// <summary>
        /// 3.拼接生成URL
        /// </summary>
        /// <param name="requestParams"></param>
        /// <param name="APPID"></param>
        /// <param name="apiUrl">http://asr.cloud.tencent.com</param>
        /// <param name="service">asr</param>
        /// <returns></returns>
        public static string MakeUrl(SortedDictionary<string, string> requestParams, string APPID, string requestHost, string signature)
        {
            string Url = "";
            Url += requestHost + APPID;
            Url += "?";
            string v = "";
            foreach (string key in requestParams.Keys)
            {
                v += string.Format("{0}={1}&", key, requestParams[key]);
            }
            Url += v + "signature=" + signature;
            return Url;
        }

        /// <summary>
        /// 4.使用签名之后拼接的URL进行握手和模拟语音识别操作
        /// </summary>
        /// <param name="_apiUrl"></param>
        public static void MakeHandAndSendData(string _apiUrl)
        {
            try
            {
                var ServerAddress = _apiUrl;
                //ServerAddress = "wss://127.0.0.1:6789";
                //如果已经连上了服务端，想要再次进行连接，需要进行判断，关闭当前连接后才能进行
                if (client.State == WebSocketState.Open)
                {
                    Console.WriteLine("当前client对象连接状态为open，若要重新连接，请先关闭当前连接");
                    client.Abort();
                    return;
                }

                try
                {
                    client.ConnectAsync(new Uri(ServerAddress), CancellationToken.None).Wait();
                    //①不停的接受当前链接的客户端发送的消息
                    GetMessages(client);

                    //②模拟发起实时音频识别
                    //SendLiveVoiceData(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("连接出现问题，请检查网络是否通畅，地址是否正确，服务端是否开启");
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #region 模拟实时语音分片发送
        /// <summary>
        /// 模拟实时语音发送到服务端识别
        /// </summary>
        public static void SendVoiceData()
        {
            ///获取音频信息转为二进制流数据长度
            var voice_data = getFileContent(File);
            //计算数据包可分片次数
            var voicelen = voice_data.Length;
            var whilenum = Math.Ceiling(Convert.ToDecimal((voicelen / cutlegth)));//发送数据包的次数
            var ls = getFileContentData(File, cutlegth);
            int i = 0;
            while (whilenum >= i)
            {
                var array = new ArraySegment<byte>(ls[i]);
                if (client.State == WebSocketState.Open)
                {
                    client.SendAsync(array, WebSocketMessageType.Binary, true, CancellationToken.None).Wait();
                }
                i++;
            }

            var arraylast = new ArraySegment<byte>(Encoding.UTF8.GetBytes("{\"type\": \"end\"}"));
            try
            {
                client.SendAsync(arraylast, WebSocketMessageType.Text, true, CancellationToken.None).Wait();
            }
            catch
            {
                Console.WriteLine("最后一个数据分片--发送错误！");
            }
        }


        public  void SendLiveVoiceData(byte[] data)
        {
            ///获取音频信息转为二进制流数据长度
            //var voice_data = getFileContent(File);
            //计算数据包可分片次数
            var voicelen = data.Length;
            var whilenum = Math.Ceiling(Convert.ToDecimal((voicelen / cutlegth)));//发送数据包的次数
            
            //var ls = getContentData(data);  //这里是把data 3200 划分成为1280
            //int i = 0;
            //while (whilenum >= i)
            //{
                var array = new ArraySegment<byte>(data);
                if (client.State == WebSocketState.Open)
                {
                    client.SendAsync(array, WebSocketMessageType.Binary, true, CancellationToken.None).Wait();
                    //Console.WriteLine("one success");
                }
            //else
            //{
                //Console.WriteLine("false");

            //}
            //i++;
            //}


        }

        public  void sendEND()
        {
            var arraylast = new ArraySegment<byte>(Encoding.UTF8.GetBytes("{\"type\": \"end\"}"));
            try
            {
                client.SendAsync(arraylast, WebSocketMessageType.Text, true, CancellationToken.None).Wait();
            }
            catch
            {
                Console.WriteLine("最后一个数据分片--发送错误！");
            }
        }



        public static byte[] getFileContent(string File)
        {
            //根据url获取音频流
            FileStream fs = new FileStream(File, FileMode.Open, FileAccess.Read);
            byte[] bt = new byte[fs.Length];
            fs.Read(bt, 0, bt.Length);
            fs.Close();
            return bt;
        }

        public static List<byte[]> getFileContentData(string File, int maxLength)
        {
            List<byte[]> ls = new List<byte[]>();
            FileStream fs = new FileStream(File, FileMode.Open, FileAccess.Read);
            long leftLength = fs.Length;//还没有读取的文件内容长度
            int num = 0;  //每次实际返回的字节数长度
            int fileStart = 0;//文件开始读取的位置

            while (leftLength > 0)
            {
                //接收文件内容的字节数组
                byte[] btData = new byte[maxLength];

                //设置文件流的读取位置
                fs.Position = fileStart;
                if (leftLength < maxLength)
                {
                    num = fs.Read(btData, 0, Convert.ToInt32(leftLength));
                }
                else
                {
                    num = fs.Read(btData, 0, maxLength);
                }
                if (num == 0)
                {
                    break;
                }
                fileStart += num;
                leftLength -= num;
                ls.Add(btData);
            }

            fs.Close();
            return ls;
        }
        #endregion


        public static List<byte[]> getContentData(byte[] data)
        {
            List<byte[]> ls = new List<byte[]>();
            long leftLength = data.Length;//还没有读取的文件内容长度
            Console.WriteLine(leftLength);

            ls[0] = data.Take(1280).ToArray();
            ls[1] = data.Skip(1280).Take(1280).ToArray();
            ls[2] = data.Skip(2560).ToArray();

            return ls;
        }

        /// <summary>
        /// 握手连接之后，一直监听接受服务端的消息
        /// </summary>
        /// <param name="clientWebSocket"></param>
        public async static void GetMessages(Object clientWebSocket)
        {
            var proxSocket = clientWebSocket as ClientWebSocket;
            ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[19200]);
            WebSocketReceiveResult result = null;

            while (true)
            {
                using (var ms = new MemoryStream())
                {
                    try
                    {
                        do
                        {
                            result = await proxSocket.ReceiveAsync(buffer, CancellationToken.None);
                            ms.Write(buffer.Array, buffer.Offset, result.Count);
                        }
                        while (!result.EndOfMessage);
                        ms.Seek(0, SeekOrigin.Begin);
                        if (ms.Length <= 0)
                        {
                            break;
                        }

                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            using (var reader = new StreamReader(ms, Encoding.UTF8))
                            {
                                string strResult = reader.ReadToEnd();
                                JObject jo = (JObject)JsonConvert.DeserializeObject(strResult);
                                if (jo["result"]!=null)
                                {
                                    Console.WriteLine(jo["result"]["voice_text_str"].ToString());
                                    Msg = jo["result"]["voice_text_str"].ToString();
                                    if (f != null)
                                    {
                                        //f.richTextBox1.Text = jo["result"]["voice_text_str"].ToString();
                                    }
                                }
                                Console.WriteLine(strResult);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
            }
        }

        #region 时间戳
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString().Substring(0, 10);
        }

        /// <summary>
        /// 失效时间
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStampExpired()
        {
            TimeSpan ts = DateTime.UtcNow.AddDays(1) - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString().Substring(0, 10); ;
        }
        #endregion

        #region 生成voice_id
        /// <summary>
        /// 生成voice_id
        /// </summary>
        /// <param name="len">voice_id 长度</param>
        /// <returns></returns>
        public static string getRandomString(int len)
        {
            var voiceid = string.Empty;
            string[] strs = new string[] {  "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
            "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v",
            "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G",
            "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R",
            "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2",
            "3", "4", "5", "6", "7", "8", "9","0", "1", "2",
            "3", "4", "5", "6", "7", "8", "9","0", "1", "2",
            "3", "4", "5", "6", "7", "8", "9"};

            Random r = new Random();
            for (int i = 0; i < len; i++)
            {
                voiceid += strs[r.Next(0, strs.Length)];
            }
            return voiceid;
        }
        #endregion


    }
}
