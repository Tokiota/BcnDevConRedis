using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Sales_Totals_by_AmountMap : EntityTypeConfiguration<Sales_Totals_by_Amount>
    {
        #region Constructors and Destructors

        public Sales_Totals_by_AmountMap()
        {
            // Primary Key
            HasKey(t => new
                {
                    t.OrderID,
                    t.CompanyName
                });

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("Sales Totals by Amount");
            Property(t => t.SaleAmount).HasColumnName("SaleAmount");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.CompanyName).HasColumnName("CompanyName");
            Property(t => t.ShippedDate).HasColumnName("ShippedDate");
        }

        #endregion
    }
}