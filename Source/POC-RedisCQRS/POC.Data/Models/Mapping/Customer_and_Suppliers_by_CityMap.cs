using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class Customer_and_Suppliers_by_CityMap : EntityTypeConfiguration<Customer_and_Suppliers_by_City>
    {
        #region Constructors and Destructors

        public Customer_and_Suppliers_by_CityMap()
        {
            // Primary Key
            HasKey(t => new
                {
                    t.CompanyName,
                    t.Relationship
                });

            // Properties
            Property(t => t.City)
                .HasMaxLength(15);

            Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.ContactName)
                .HasMaxLength(30);

            Property(t => t.Relationship)
                .IsRequired()
                .HasMaxLength(9);

            // Table & Column Mappings
            ToTable("Customer and Suppliers by City");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.CompanyName).HasColumnName("CompanyName");
            Property(t => t.ContactName).HasColumnName("ContactName");
            Property(t => t.Relationship).HasColumnName("Relationship");
        }

        #endregion
    }
}