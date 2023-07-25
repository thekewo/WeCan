namespace VoucherApplication.Data.Interfaces
{
    public interface IVoucherRepository
    {
        Task AddVoucherAsync(Voucher voucher);
        Task<bool> IsVoucherInDbAsync(int id);
        Task<Voucher> GetVoucherByIdAsync(int id);
        Task RedeemVoucherAsync(int id);
    }
}
