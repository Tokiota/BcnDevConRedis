using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Current_Product_ListMap : EntityTypeConfiguration<Current_Product_List>
    {
        #region Constructors and Destructors

        public Current_Product_ListMap()
        {
            // Primary Key
            HasKey(t => new
                {
                    t.ProductID,
                    t.ProductName
                });

            // Properties
            Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("Current Product List");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.ProductName).HasColumnName("ProductName");
        }

        #endregion
    }
}