using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.DBModels;

namespace Persistence.Configuration
{
    public class UsersConfiguration : IEntityTypeConfiguration<UserDbModel>
    {
        public void Configure(EntityTypeBuilder<UserDbModel> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.FirstName)
                .IsRequired(true);

            builder.Property(o => o.LastName)
                .IsRequired(true);
        }
    }
}
