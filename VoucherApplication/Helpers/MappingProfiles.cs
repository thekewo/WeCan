using AutoMapper;
using System.Net;
using VoucherApplication.Data;
using VoucherApplication.Models.ViewModels;

namespace VoucherApplication.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddVoucherViewModel, Voucher>();
            CreateMap<RedeemVoucherViewModel, Voucher>();
        }
    }
}
