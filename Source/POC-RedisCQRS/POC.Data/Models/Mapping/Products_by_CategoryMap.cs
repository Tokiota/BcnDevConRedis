using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Products_by_CategoryMap : EntityTypeConfiguration<Products_by_Category>
    {
        #region Constructors and Destructors

        public Products_by_CategoryMap()
        {
            // Primary Key
            HasKey(t => new
                {
                    t.CategoryName,
                    t.ProductName,
                    t.Discontinued
                });

            // Properties
            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.QuantityPerUnit)
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("Products by Category");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
            Property(t => t.Discontinued).HasColumnName("Discontinued");
        }

        #endregion
    }
}