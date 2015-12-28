using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SolsticeDataServices.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolsticeDataServices.DAL.Mappers
{
    public class MasterDeviceMapper: EntityTypeConfiguration<MasterDevice>
    {
        public MasterDeviceMapper()
        {
            // assigning the table name for the "MasterDevice" class
            this.ToTable("MasterDevice");

             //setting the primary key
            this.HasKey(d => d.DeviceId);
            // specifies that the column is identity column
            this.Property(d => d.DeviceId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // IsRequired specifies that the column is mandatory
            this.Property(d => d.DeviceId).IsRequired();

            this.Property(d => d.DeviceId).IsRequired();
            // HasMaxLength specifies that the column's length
            this.Property(d => d.DeviceId).HasMaxLength(64);

            this.Property(d => d.DeviceName).IsRequired();
            this.Property(d => d.DeviceName).HasMaxLength(256);
        }
    }
}
