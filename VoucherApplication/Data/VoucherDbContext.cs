using Microsoft.EntityFrameworkCore;

namespace VoucherApplication.Data
{
    public class VoucherDbContext : DbContext
    {
        public VoucherDbContext(DbContextOptions<VoucherDbContext> options) : base(options)
        {
        }

        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Voucher>();
        }
    }
}
