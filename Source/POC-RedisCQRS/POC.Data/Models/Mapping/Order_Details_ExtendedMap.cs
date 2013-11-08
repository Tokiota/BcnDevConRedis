using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Order_Details_ExtendedMap : EntityTypeConfiguration<Order_Details_Extended>
    {
        #region Constructors and Destructors

        public Order_Details_ExtendedMap()
        {
            // Primary Key
            HasKey(t => new
                {
                    t.OrderID,
                    t.ProductID,
                    t.ProductName,
                    t.UnitPrice,
                    t.Quantity,
                    t.Discount
                });

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.UnitPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Quantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("Order Details Extended");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            Property(t => t.Quantity).HasColumnName("Quantity");
            Property(t => t.Discount).HasColumnName("Discount");
            Property(t => t.ExtendedPrice).HasColumnName("ExtendedPrice");
        }

        #endregion
    }
}