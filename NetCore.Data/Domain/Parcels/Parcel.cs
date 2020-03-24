using Dapper.Contrib.Extensions;
using System;

namespace NetCore.Data.Domain.Parcels
{
    [Table("TParcel")]
    public class Parcel : Entity
    {
        [ExplicitKey]
        public int ParcelId { get; set; }
        public int CustomerId { get; set; }
        public int AreaId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string DistributeCenterId { get; set; }
        public string Address { get; set; }
        public string OriginalAddress { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Precise { get; set; }
        public int Confidence { get; set; }
        public string SalesOrderNo { get; set; }
        public string StockOrderNo { get; set; }
        public int PackageCount { get; set; }
        public string Source { get; set; }
        public string Mobile { get; set; }
        public string TelePhone { get; set; }
        public string Company { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CustomerNote { get; set; }
        public int Urgent { get; set; }
        public DateTime OriginalDeliveryTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string DeliveryNote { get; set; }
        public string ParcelType { get; set; }
        public string RealPayType { get; set; }
        public string AppointPayType { get; set; }
        public decimal GoodsAmount { get; set; }
        public decimal CodAmount { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Status { get; set; }
        public string OrderCustomerID { get; set; }

    }
}
