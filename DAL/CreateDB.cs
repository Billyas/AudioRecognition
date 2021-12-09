using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecognition.DAL
{
    class CreateDB
    {
        public CreateDB()
        {
            if (!File.Exists("Data.db"))
            {
                Console.WriteLine("不存在数据库，正在重构数据库！");
                using (var context = new ASRContext())
                {
                    var res = context.Database.EnsureCreated();
                    Console.WriteLine("数据库创建"+res);
                }
            }
            else
            {
                Console.WriteLine("数据库已存在！");
            }
        }
    }
}
