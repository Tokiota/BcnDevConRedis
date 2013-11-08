using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Category_Sales_for_1997Map : EntityTypeConfiguration<Category_Sales_for_1997>
    {
        #region Constructors and Destructors

        public Category_Sales_for_1997Map()
        {
            // Primary Key
            HasKey(t => t.CategoryName);

            // Properties
            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            ToTable("Category Sales for 1997");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
            Property(t => t.CategorySales).HasColumnName("CategorySales");
        }

        #endregion
    }
}