using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace POC.Data.Models.Mapping
{
    public class RegionMap : EntityTypeConfiguration<Region>
    {
        #region Constructors and Destructors

        public RegionMap()
        {
            // Primary Key
            HasKey(t => t.RegionID);

            // Properties
            Property(t => t.RegionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.RegionDescription)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Region");
            Property(t => t.RegionID).HasColumnName("RegionID");
            Property(t => t.RegionDescription).HasColumnName("RegionDescription");
        }

        #endregion
    }
}