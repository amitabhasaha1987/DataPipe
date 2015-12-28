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
    class UsersMapper : EntityTypeConfiguration<Users>
    {
        public UsersMapper()
        {
            // assigning the table name for the "Users" class
            this.ToTable("Users");


            // specifies that it is primary key
            this.HasKey(u => u.UserId);
            // specifies that it is mandatory field
            this.Property(u => u.UserId).IsRequired();
            // specifies that it is identity column
            this.Property(u => u.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.Property(u => u.FirstName).IsRequired();
            this.Property(u => u.FirstName).HasMaxLength(256);

            this.Property(u => u.LastName).IsRequired();
            this.Property(u => u.LastName).HasMaxLength(256);

            this.Property(u => u.Company).IsRequired();
            this.Property(u => u.Company).HasMaxLength(256);

            this.Property(u => u.Contact).IsOptional();
            this.Property(u => u.Contact).HasMaxLength(50);

            this.Property(u => u.EmailId).IsRequired();
            this.Property(u => u.EmailId).HasMaxLength(256);

            this.Property(u => u.Password).IsOptional();
            this.Property(u => u.Password).HasMaxLength(64);
    
            this.Property(u => u.IsBlocked).IsOptional();

            this.Property(u => u.IsSuperAdmin).IsOptional();

            this.Property(u => u.IsVerified).IsOptional();

            this.Property(u => u.AddTime).IsOptional();
        }
    }
}
