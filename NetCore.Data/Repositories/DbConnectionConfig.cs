using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Data.Repositories
{
    public class DbConnectionConfig
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string TMS { get; set; }

        public string BasicData { get; set; }
    }
}
