using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition
{
    public class NARecorder
    {
        private int deviceNumber = 0;    // 选择的录音设备下标。多个设备时可设置为用户选择
        private WaveIn waveIn;    // waveIn操作类
        private WaveFormat recordingFormat;    // 录音格式
        private WaveFileWriter writer;    // 录音文件操作类
        public event EventHandler StoppedEvent = delegate { };    // 录音结束事件
        public event EventHandler DataAvailableEvent = delegate { }; // 录音过程中接收到数据事件
        public double RecordedTime        // 获取到录音的时长
        {
            get
            {
                if (writer == null)
                    return 0;
                return (double)writer.Length / writer.WaveFormat.AverageBytesPerSecond;
            }
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        /// <param name="filename">保存的文件名</param>
        internal bool StartRecorder(string filename)
        {
            // 设置录音格式
            recordingFormat = new WaveFormat(16000, 16, 1);//new WaveFormat(44100, WaveIn.GetCapabilities(deviceNumber).Channels);
            // 设置麦克风操作对象
            waveIn = new WaveIn();
            waveIn.DeviceNumber = deviceNumber;    // 设置使用的录音设备
            waveIn.DataAvailable += OnDataAviailable;        // 接收到音频数据时，写入文件
            waveIn.RecordingStopped += OnRecordingStopped;   // 录音结束时执行
            waveIn.WaveFormat = recordingFormat;
            // 设置文件操作类
            writer = new WaveFileWriter(filename, recordingFormat);
            // 开始录音
            waveIn.StartRecording();

            return true;
        }

        /// <summary>
        /// 结束录音
        /// </summary>
        /// <returns></returns>
        internal bool StopRecorder()
        {
            waveIn.StopRecording();
            return true;
        }

        /// <summary>
        /// 录音结束回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            writer.Dispose();
            // 通知结束事件
            StoppedEvent(this, EventArgs.Empty);
        }

        /// <summary>
        /// 录音回调函数，写入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataAviailable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;
            WriteToFile(buffer, bytesRecorded);    // 音频数据写入文件
            DataAvailableEvent(this, EventArgs.Empty);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bytesRecorded"></param>
        private void WriteToFile(byte[] buffer, int bytesRecorded)
        {
            long maxFileLength = this.recordingFormat.AverageBytesPerSecond * 60;

            var toWrite = (int)Math.Min(maxFileLength - writer.Length, bytesRecorded);
            if (toWrite > 0)
            {
                writer.Write(buffer, 0, bytesRecorded);
            }
            else
            {
                StopRecorder();
            }
        }
    }
}
