using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VoucherApplication.Data.Interfaces;
using VoucherApplication.Models.Enums;

namespace VoucherApplication.Data.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly VoucherDbContext _dbContext;

        public VoucherRepository(VoucherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddVoucherAsync(Voucher voucher)
        {
            await _dbContext.Vouchers.AddAsync(voucher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Voucher> GetVoucherByIdAsync(int id) => await _dbContext.Vouchers.FindAsync(id);
        public async Task<bool> IsVoucherInDbAsync(int id) => await _dbContext.Vouchers.AnyAsync(v => v.Id == id);
        public async Task RedeemVoucherAsync(int id)
        {
            var voucer = await _dbContext.Vouchers.FindAsync(id);
            voucer.Quantity--;
            await _dbContext.SaveChangesAsync();
        }
    }
}
