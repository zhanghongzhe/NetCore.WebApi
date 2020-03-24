using System;
using System.Collections.Generic;
using System.Text;
using NetCore.Data.Domain.Parcels;
using NetCore.Data.Repositories.Parcels;

namespace NetCore.Services.Parcels
{
    public class ParcelService
    {
        private readonly ParcelRepository _outerParcelRepository;
        public ParcelService(ParcelRepository outerParcelRepository)
        {
            this._outerParcelRepository = outerParcelRepository;
        }

        public Parcel Get(int customerId, string stockOrderNo)
        {
            return this._outerParcelRepository.Get(customerId, stockOrderNo);
        }

        public IEnumerable<Parcel> GetByCustomer(int customerId)
        {
            return this._outerParcelRepository.GetByCustomer(customerId);
        }
    }
}
