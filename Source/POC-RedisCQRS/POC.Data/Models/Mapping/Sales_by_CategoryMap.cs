using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Sales_by_CategoryMap : EntityTypeConfiguration<Sales_by_Category>
    {
        #region Constructors and Destructors

        public Sales_by_CategoryMap()
        {
            // Primary Key
            HasKey(t => new
                {
                    t.CategoryID,
                    t.CategoryName,
                    t.ProductName
                });

            // Properties
            Property(t => t.CategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("Sales by Category");
            Property(t => t.CategoryID).HasColumnName("CategoryID");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.ProductSales).HasColumnName("ProductSales");
        }

        #endregion
    }
}