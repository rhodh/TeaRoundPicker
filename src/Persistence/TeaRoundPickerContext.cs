using Microsoft.EntityFrameworkCore;
using Persistence.DBModels;

namespace Persistence
{
    public class TeaRoundPickerContext : DbContext
    {
        public TeaRoundPickerContext(DbContextOptions<TeaRoundPickerContext> options) : base(options)
        {
        }

        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<DrinkOrderDbModel> DrinkOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeaRoundPickerContext).Assembly);
        }
    }
}
