using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Products_Above_Average_PriceMap : EntityTypeConfiguration<Products_Above_Average_Price>
    {
        #region Constructors and Destructors

        public Products_Above_Average_PriceMap()
        {
            // Primary Key
            HasKey(t => t.ProductName);

            // Properties
            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("Products Above Average Price");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.UnitPrice).HasColumnName("UnitPrice");
        }

        #endregion
    }
}