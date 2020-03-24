using Dapper;
using NetCore.Data.Domain.Parcels;
using System.Collections.Generic;

namespace NetCore.Data.Repositories.Parcels
{
    public class ParcelRepository : Repository<Parcel>
    {
        public ParcelRepository(TMSConnectionFactory dbConnectionFactory) 
            : base(dbConnectionFactory)
        {
        }

        public Parcel Get(int customerId, string stockOrderNo)
        {
            return FirstOrDefault("[CustomerId] = @customerId AND StockOrderNo = @stockOrderNo", new { customerId, stockOrderNo });
        }

        public Parcel Get(int customerId, string stockOrderNo, string status)
        {
            return FirstOrDefault("[CustomerId] = @customerId AND StockOrderNo = @stockOrderNo AND [Status] = @status", new { customerId, stockOrderNo, status });
        }

        public IEnumerable<Parcel> GetByCustomer(int customerId)
        {
            return Connection.Query<Parcel>($"SELECT TOP 10 * FROM {TableName} WHERE CustomerId = @customerId", new { customerId });
        }
    }
}
