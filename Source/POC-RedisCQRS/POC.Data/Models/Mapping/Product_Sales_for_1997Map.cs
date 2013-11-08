using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Product_Sales_for_1997Map : EntityTypeConfiguration<Product_Sales_for_1997>
    {
        #region Constructors and Destructors

        public Product_Sales_for_1997Map()
        {
            // Primary Key
            HasKey(t => new
                {
                    t.CategoryName,
                    t.ProductName
                });

            // Properties
            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("Product Sales for 1997");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.ProductSales).HasColumnName("ProductSales");
        }

        #endregion
    }
}