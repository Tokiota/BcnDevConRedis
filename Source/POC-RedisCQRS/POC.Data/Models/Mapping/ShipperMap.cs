using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class ShipperMap : EntityTypeConfiguration<Shipper>
    {
        #region Constructors and Destructors

        public ShipperMap()
        {
            // Primary Key
            HasKey(t => t.ShipperID);

            // Properties
            Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.Phone)
                .HasMaxLength(24);

            // Table & Column Mappings
            ToTable("Shippers");
            Property(t => t.ShipperID).HasColumnName("ShipperID");
            Property(t => t.CompanyName).HasColumnName("CompanyName");
            Property(t => t.Phone).HasColumnName("Phone");
        }

        #endregion
    }
}