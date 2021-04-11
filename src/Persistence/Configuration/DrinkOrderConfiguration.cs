using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Configuration
{
    internal class DrinkOrderConfiguration : IEntityTypeConfiguration<DrinkOrderDbModel>
    {
        public void Configure(EntityTypeBuilder<DrinkOrderDbModel> builder)
        {
            builder.ToTable("DrinkOrder");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired(true);

           builder.Property(o => o.Type)
                .IsRequired(true);

            builder
                .HasOne(o => o.User)
                .WithMany(u => u.DrinkOrders).IsRequired();

            // TODO remove. We should just use the json column on the postgress db but since we currently have sqlite for integrations test this is required. 
            builder.Property(e => e.AdditionalSpecification)
                .IsRequired()
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, 
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<Dictionary<string,string>>(v, 
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
