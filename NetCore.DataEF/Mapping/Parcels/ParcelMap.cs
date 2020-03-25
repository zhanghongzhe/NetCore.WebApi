using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCore.Core.Domain.Parcels;

namespace NetCore.DataEF.Mapping.Parcels
{
    public partial class ParcelMap : EntityTypeConfiguration<Parcel>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.ToTable("TParcel");
            builder.HasKey(o => o.ParcelId);

            base.Configure(builder);
        }

        #endregion
    }
}
