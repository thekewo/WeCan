using VoucherApplication.Data;
using VoucherApplication.Models.Enums;
using VoucherApplication.Models.Results;

namespace VoucherApplication.Services.Interfaces
{
    public interface IVoucherService
    {
        Task<Result> AddVoucherAsync(Voucher voucher);
        Task<Result> RedeemVoucherAsync(int id, ServiceType serviceType);
    }
}
