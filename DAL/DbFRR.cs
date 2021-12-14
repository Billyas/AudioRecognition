using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioRecognition.Model;

namespace AudioRecognition.DAL
{
    class DbFRR
    {
        public string[] Data2string(FlashRecognitionResult frr)
        {
            string[] res = new string[6];
            res[0] = frr.Request_id;
            res[1] = frr.Audio_duration;
            res[2] = frr.Message;
            res[3] = frr.Flash_result;
            res[4] = frr.Time.ToString("s");
            res[5] = frr.UserName;
            return res;
        }

        public List<FlashRecognitionResult> GetFRRByUser(string user)
        {
            SqLiteHelper sqLiteHelper = new SqLiteHelper();
            var res = sqLiteHelper.ExecuteQuery("select * from FlashRecognitionResults where username = '" + user + "'");
            List<FlashRecognitionResult> flashRecognitionResults = new List<FlashRecognitionResult>();
            if (res.HasRows)
            {
                while (res.Read())
                {
                    FlashRecognitionResult tmp =
                        new FlashRecognitionResult(res.GetString(0), res.GetString(1), res.GetString(2), res.GetString(3), res.GetDateTime(4), res.GetString(5));
                    flashRecognitionResults.Add(tmp);
                }
                sqLiteHelper.CloseConnection();
                return flashRecognitionResults;
            }
            else
            {
                sqLiteHelper.CloseConnection();
                return null;
            }
        }

        public FlashRecognitionResult GetFrrByID(string id)
        {
            SqLiteHelper sqLiteHelper = new SqLiteHelper();
            var res = sqLiteHelper.ExecuteQuery("select * from FlashRecognitionResults where Request_id = '" + id + "'");
            FlashRecognitionResult flashRecognitionResults;
            if (res.HasRows)
            {
                res.Read();
                flashRecognitionResults =
                        new FlashRecognitionResult(res.GetString(0), res.GetString(1), res.GetString(2), res.GetString(3), res.GetDateTime(4), res.GetString(5));
                    //flashRecognitionResults.Add(tmp);
                sqLiteHelper.CloseConnection();
                return flashRecognitionResults;
            }
            else
            {
                sqLiteHelper.CloseConnection();
                return null;
            }
        }

        public DataSet GetFRRByUserName(string username)
        {
            string connStr = "Data Source = Data.db";
            SQLiteConnection conn = null;
            try
            {
                conn = new SQLiteConnection(connStr);
                //打开数据库
                conn.Open();
                string sql = "select Request_id,Flash_result,Time from FlashRecognitionResults where Username='" + username + "'";
                //创建SqlDataAdapter类的对象
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                //创建DataSet类的对象
                DataSet ds = new DataSet();
                //使用SqlDataAdapter对象sda将查新结果填充到DataSet对象ds中
                sda.Fill(ds);
                //设置表格控件的DataSource属性
                //dataGridView1.DataSource = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("出现错误！" + ex.Message);
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    //关闭数据库连接
                    conn.Close();
                }
            }
        }

        public bool AddFRR(FlashRecognitionResult frr)
        {
            SqLiteHelper sq = new SqLiteHelper();
            var res = sq.ExecuteQuery("select * from FlashRecognitionResults where Request_id = '" + frr.Request_id + "'");
            if (res.HasRows)
            {
                sq.CloseConnection();
                return false;
            }
            else
            {
                bool flag;
                flag = sq.InsertItems("FlashRecognitionResults", Data2string(frr));
                sq.CloseConnection();
                return flag;
            }
        }

        public bool DeleteFRR(string id)
        {
            SqLiteHelper sq = new SqLiteHelper();
            bool res = sq.DeleteValues("FlashRecognitionResults", "Request_id", id);
            sq.CloseConnection();
            return res;
        }

    }
}
