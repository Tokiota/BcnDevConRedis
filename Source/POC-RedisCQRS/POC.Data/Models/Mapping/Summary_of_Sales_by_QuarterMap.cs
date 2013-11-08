using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Summary_of_Sales_by_QuarterMap : EntityTypeConfiguration<Summary_of_Sales_by_Quarter>
    {
        #region Constructors and Destructors

        public Summary_of_Sales_by_QuarterMap()
        {
            // Primary Key
            HasKey(t => t.OrderID);

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("Summary of Sales by Quarter");
            Property(t => t.ShippedDate).HasColumnName("ShippedDate");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.Subtotal).HasColumnName("Subtotal");
        }

        #endregion
    }
}