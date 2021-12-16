using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.BLL
{
    class Video2Audio
    {
        public string getAudio(string videofilename)
        {
            string resfile=videofilename+ DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss") + "_out.mp3";
            string arg = "-i " + videofilename+ " -acodec libmp3lame -ar 16000 -ac 1 "+resfile;
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = @".\ffmpeg\bin\ffmpeg.exe";//要调用外部程序的绝对路径
            p.StartInfo.Arguments = arg;//参数(这里就是FFMPEG的参数了)
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError =
            true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用
            //StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来
            //捕获的)
            p.StartInfo.CreateNoWindow = true;//不创建进程窗口
            p.ErrorDataReceived += new DataReceivedEventHandler(Output);//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
            return resfile;
        }
        private void Output(object sendProcess, DataReceivedEventArgs output)
        {
            if (!string.IsNullOrEmpty(output.Data))
            {

                Console.WriteLine(output.Data);
                //处理方法... 
            }
        }
    }
}
