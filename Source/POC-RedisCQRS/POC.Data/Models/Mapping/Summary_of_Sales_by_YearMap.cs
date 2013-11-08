using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Summary_of_Sales_by_YearMap : EntityTypeConfiguration<Summary_of_Sales_by_Year>
    {
        #region Constructors and Destructors

        public Summary_of_Sales_by_YearMap()
        {
            // Primary Key
            HasKey(t => t.OrderID);

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("Summary of Sales by Year");
            Property(t => t.ShippedDate).HasColumnName("ShippedDate");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.Subtotal).HasColumnName("Subtotal");
        }

        #endregion
    }
}