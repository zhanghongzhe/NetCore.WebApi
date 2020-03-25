using System;
using Dapper;
using System.Collections.Generic;
using NetCore.Data.Domain.Parcels;
using NetCore.Data.Repositories.Parcels;
using System.Linq;

namespace NetCore.Services.Parcels
{
    public class ParcelService
    {
        //每个类定义一个仓储模型
        private readonly ParcelRepository _parcelRepository;

        //采用泛型模型
        private readonly Data.Repositories.Repository<ParcelDo> _parcelDapperRepository;

        //采用泛型模型
        private readonly NetCore.DataEF.IRepository<NetCore.Core.Domain.Parcels.Parcel> _parcelEFRepository;

        public ParcelService(ParcelRepository parcelRepository,
            Data.Repositories.Repository<ParcelDo> parcelDapperRepository,
            NetCore.DataEF.IRepository<NetCore.Core.Domain.Parcels.Parcel> parcelEFRepository)
        {
            this._parcelRepository = parcelRepository;
            this._parcelDapperRepository = parcelDapperRepository;
            this._parcelEFRepository = parcelEFRepository;
        }

        public ParcelDo Get(int customerId, string stockOrderNo)
        {
            return this._parcelRepository.Get(customerId, stockOrderNo);
        }

        public IEnumerable<ParcelDo> GetByCustomer(int customerId)
        {
            return this._parcelDapperRepository.Connection.Query<ParcelDo>($"SELECT TOP 10 * FROM TParcel WHERE CustomerId = @customerId", new { customerId });
        }

        public NetCore.Core.Domain.Parcels.Parcel GetByEF(int customerId, string stockOrderNo)
        {
            return this._parcelEFRepository.TableNoTracking.Where(o => o.CustomerId == customerId && o.StockOrderNo == stockOrderNo).FirstOrDefault();
        }
    }
}
