using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Alphabetical_list_of_productMap : EntityTypeConfiguration<Alphabetical_list_of_product>
    {
        #region Constructors and Destructors

        public Alphabetical_list_of_productMap()
        {
            // Primary Key
            HasKey(t => new
                {
                    t.ProductID,
                    t.ProductName,
                    t.Discontinued,
                    t.CategoryName
                });

            // Properties
            Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.QuantityPerUnit)
                .HasMaxLength(20);

            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            ToTable("Alphabetical list of products");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.SupplierID).HasColumnName("SupplierID");
            Property(t => t.CategoryID).HasColumnName("CategoryID");
            Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
            Property(t => t.UnitsOnOrder).HasColumnName("UnitsOnOrder");
            Property(t => t.ReorderLevel).HasColumnName("ReorderLevel");
            Property(t => t.Discontinued).HasColumnName("Discontinued");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
        }

        #endregion
    }
}