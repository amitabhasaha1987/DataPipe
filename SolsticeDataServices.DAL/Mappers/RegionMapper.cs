﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolsticeDataServices.DAL.Mappers
{
    class RegionMapper : EntityTypeConfiguration<Region>
    {
        // the mapper class of the entity class "Region.cs"
        public RegionMapper()
        {
            this.ToTable("Region");


            // HasKey specifies that the column is Key column
            this.HasKey(r => r.RegionId);
            // specifies that the column is autogenerated one
            this.Property(r => r.RegionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // IsRequired specifies that the column has mandatory entry
            this.Property(r => r.RegionId).IsRequired();
            // HasMaxLength specifies that the column's maximum length
            this.Property(r => r.RegionId).HasMaxLength(64);


            this.Property(r => r.RegionName).IsRequired();
            this.Property(r => r.RegionName).HasMaxLength(128);

        }
    }
}
