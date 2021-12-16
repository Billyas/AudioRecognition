using AudioRecognition.UI;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.BLL
{
    class Recorder
    {
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;
        private string fileName = string.Empty;

        public Recorder()
        {

        }

        public Recorder(string fileName)
        {
            this.fileName = fileName;
        }

        //LiveRecoderSender testlive = new LiveRecoderSender();
        //public JObject msg ;
        //LiveForm f = null;

        //public void setform(LiveForm f)
        //{
        //    this.f = f;
        //}

        /// <summary>
        /// 开始录音
        /// </summary>
        public void StartRec()
        {
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(16000, 16, 1); // 16bit,16KHz,Mono的录音格式

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter(fileName, waveSource.WaveFormat);

            waveSource.StartRecording();
            //testlive.go();
            //testlive.setform(f);
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void StopRec()
        {
            waveSource.StopRecording();

            // Close Wave(Not needed under synchronous situation)
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
            //testlive.sendEND();
        }

        /// <summary>
        /// 录音结束后保存的文件路径
        /// </summary>
        /// <param name="fileName">保存wav文件的路径名</param>
        public void SetFileName(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// 开始录音回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                //Console.WriteLine(e.Buffer.Length);
                //Console.WriteLine(e.BytesRecorded);
                //testlive.Data = e.Buffer;
                //testlive.SendLiveVoiceData(e.Buffer.Take(1280).ToArray());

                //testlive.SendLiveVoiceData(e.Buffer.Skip(1280).Take(1280).ToArray());

                //testlive.SendLiveVoiceData(e.Buffer.Skip(2560).ToArray());

                //msg = testlive.getMsg();
                waveFile.Flush();
            }
        }

        /// <summary>
        /// 录音结束回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }

        public void saveLRR(string filename)
        {

        }

    }
}
