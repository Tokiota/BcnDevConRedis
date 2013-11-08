using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Order_SubtotalMap : EntityTypeConfiguration<Order_Subtotal>
    {
        #region Constructors and Destructors

        public Order_SubtotalMap()
        {
            // Primary Key
            HasKey(t => t.OrderID);

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("Order Subtotals");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.Subtotal).HasColumnName("Subtotal");
        }

        #endregion
    }
}