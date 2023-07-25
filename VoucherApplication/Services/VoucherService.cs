using Microsoft.AspNetCore.Components.Routing;
using VoucherApplication.Data;
using VoucherApplication.Data.Interfaces;
using VoucherApplication.Models.Enums;
using VoucherApplication.Models.Results;
using VoucherApplication.Services.Interfaces;

namespace VoucherApplication.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly ILogger<VoucherService> _logger;

        public VoucherService(IVoucherRepository voucherRepository, ILogger<VoucherService> logger)
        {
            _voucherRepository = voucherRepository;
            _logger = logger;
        }

        public async Task<Result> AddVoucherAsync(Voucher voucher)
        {
            try
            {
                if (voucher.Type == VoucherType.FixedTermRedemption && voucher.ExpirationDate == null)
                {
                    return new Result(false, "Expiration date for this type of voucher cannot be empty.");
                }

                if (voucher.Quantity <= 0) return new Result(false, "Quantity must be greater than zero.");
                if (voucher.Type != VoucherType.CountedRedemption && voucher.Quantity > 1) return new Result(false, "Quantity cannot be greater than zero for this type of voucher.");

                var isVoucherInDb = await _voucherRepository.IsVoucherInDbAsync(voucher.Id);
                if (isVoucherInDb)
                {
                    return new Result(false, "Voucher with this id already exists.");
                }
                else
                {
                    await _voucherRepository.AddVoucherAsync(voucher);

                    return new Result(true, "Voucher added.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, e.StackTrace);
                return new Result(false, "Unexpected error occured.");
            }
        }

        public async Task<Result> RedeemVoucherAsync(int id, ServiceType serviceType)
        {
            try
            {
                var voucher = await _voucherRepository.GetVoucherByIdAsync(id);

                if (voucher == null || voucher.Quantity == 0)
                {
                    return new Result(false, "There is no valid voucher with the given id.");
                }

                if (voucher.Type == VoucherType.CountedRedemption && voucher.ServiceType != serviceType)
                {
                    await _voucherRepository.RedeemVoucherAsync(id);
                    return new Result(true, "Voucher is not valid for this type of service.");
                }

                if (voucher.Type == VoucherType.FixedTermRedemption && voucher.ExpirationDate >= DateTime.Now)
                {
                    await _voucherRepository.RedeemVoucherAsync(id);
                    return new Result(true, "Voucher reedemed.");
                }

                if (voucher.Type == VoucherType.SingleRedemption && voucher.ServiceType == serviceType)
                {
                    await _voucherRepository.RedeemVoucherAsync(id);
                    return new Result(true, "Voucher reedemed.");
                }

                await _voucherRepository.RedeemVoucherAsync(id);
                return new Result(true, "Voucher reedemed.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, e.StackTrace);
                return new Result(false, "Unexpected error occured.");
            }
        }
    }
}
