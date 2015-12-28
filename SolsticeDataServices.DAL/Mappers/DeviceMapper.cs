using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolsticeDataServices.DAL.Mappers
{
    class DeviceMapper : EntityTypeConfiguration<Device>
    {
        // the mapper class of the entity class "Device.cs"
        public DeviceMapper()
        {
            // The name of the table
            this.ToTable("UserDevice");
            

            // make this column primary key
            this.HasKey(c => c.Id);
            // make this column as identity column
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // make this column as mandatory
            this.Property(c => c.Id).IsRequired();


            this.Property(c => c.ConfigureResponseID).IsRequired();
            // setting the length of the column
            this.Property(c => c.ConfigureResponseID).HasMaxLength(255);

            this.Property(c => c.DeviceId).IsRequired();
            //this.Property(c => c.DeviceDefinitionId).HasMaxLength(255);

            this.Property(c => c.DeviceSerialNumber).IsRequired();
            this.Property(c => c.DeviceSerialNumber).HasMaxLength(255);

            this.Property(c => c.LogicalDeviceId).IsRequired();
            this.Property(c => c.LogicalDeviceId).HasMaxLength(255);

            this.Property(c => c.RegionId).IsRequired();
            this.Property(c => c.RegionId).HasMaxLength(64);

            this.Property(c => c.IsLocked).IsRequired();
            //this.Property(c => c.EmailId).HasMaxLength(128);
        }
    }
}
