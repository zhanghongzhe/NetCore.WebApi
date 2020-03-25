using System;
using System.Collections.Generic;
using System.Text;
using NetCore.Data.Domain.Parcels;
using NetCore.Data.Repositories.Parcels;
using System.Linq;

namespace NetCore.Services.Parcels
{
    public class ParcelService
    {
        private readonly ParcelRepository _parcelRepository;

        private readonly NetCore.DataEF.IRepository<NetCore.Core.Domain.Parcels.Parcel> _parcelEFRepository;

        public ParcelService(ParcelRepository parcelRepository, NetCore.DataEF.IRepository<NetCore.Core.Domain.Parcels.Parcel> _parcelRepository)
        {
            this._parcelRepository = parcelRepository;
            this._parcelEFRepository = _parcelRepository;
        }

        public ParcelDo Get(int customerId, string stockOrderNo)
        {
            return this._parcelRepository.Get(customerId, stockOrderNo);
        }

        public IEnumerable<ParcelDo> GetByCustomer(int customerId)
        {
            return this._parcelRepository.GetByCustomer(customerId);
        }

        public NetCore.Core.Domain.Parcels.Parcel GetByEF(int customerId, string stockOrderNo)
        {
            return this._parcelEFRepository.TableNoTracking.Where(o => o.CustomerId == customerId && o.StockOrderNo == stockOrderNo).FirstOrDefault();
        }
    }
}
