using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace NetCore.Data.Repositories
{
    public class TMSConnectionFactory : DbConnectionFactory
    {
        public TMSConnectionFactory(IOptions<DbConnectionConfig> options)
            : base(options)
        {
            this.ConnectionString = config.TMS;
        }
    }

    public class DbConnectionFactory
    {
        protected DbConnectionConfig config { get; set; }
        protected string ConnectionString { get; set; }

        private IDbConnection connection;

        public DbConnectionFactory(IOptions<DbConnectionConfig> options)
        {
            this.config = options.Value;
            this.ConnectionString = config.TMS;
        }

        /// <summary>
        /// 主
        /// </summary>
        public IDbConnection Connection
        {

            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(this.ConnectionString);
                }

                return connection;
            }
        }
    }
}
