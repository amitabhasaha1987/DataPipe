using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolsticeDataServices.DAL.Context
{
    public class SolsticeDataServicesContext : DbContext
    {
        public SolsticeDataServicesContext()
            : base("SolsticeDataServicesContext")
        {
            // Setting the configuration property of Context file
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        // This method is called when the model for a derived context has been initialized
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DeviceMapper());
            modelBuilder.Configurations.Add(new UsersMapper());
            modelBuilder.Configurations.Add(new MasterDeviceMapper());
            base.OnModelCreating(modelBuilder);
        }

        #region DbSet

        public DbSet<Device> Devices { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<MasterDevice> MstrDevice { get; set; }
        public DbSet<Region> Region { get; set; }

        #endregion
    }
}