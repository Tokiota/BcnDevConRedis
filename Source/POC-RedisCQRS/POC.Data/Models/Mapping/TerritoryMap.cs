using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class TerritoryMap : EntityTypeConfiguration<Territory>
    {
        #region Constructors and Destructors

        public TerritoryMap()
        {
            // Primary Key
            HasKey(t => t.TerritoryID);

            // Properties
            Property(t => t.TerritoryID)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.TerritoryDescription)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Territories");
            Property(t => t.TerritoryID).HasColumnName("TerritoryID");
            Property(t => t.TerritoryDescription).HasColumnName("TerritoryDescription");
            Property(t => t.RegionID).HasColumnName("RegionID");

            // Relationships
            HasRequired(t => t.Region)
                .WithMany(t => t.Territories)
                .HasForeignKey(d => d.RegionID);
        }

        #endregion
    }
}