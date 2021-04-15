using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.DBModels;

namespace Persistence.Configuration
{
    internal class DrinkRunConfiguration : IEntityTypeConfiguration<DrinkRunDbModel>
    {
        public void Configure(EntityTypeBuilder<DrinkRunDbModel> builder)
        {
            builder.ToTable("DrinkRun");

            builder.HasKey(o => o.DrinkMakerId);

            builder
                .HasOne(o => o.DrinkMaker)
                .WithOne()
                .HasForeignKey<DrinkRunDbModel>(dR => dR.DrinkMakerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
                .HasMany(d => d.DrinkOrders)
                .WithMany(o => o.DrinkRuns);
        }
    }
}
