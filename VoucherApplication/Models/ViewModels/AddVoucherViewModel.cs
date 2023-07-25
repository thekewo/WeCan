using System.ComponentModel.DataAnnotations;
using VoucherApplication.Models.Enums;

namespace VoucherApplication.Models.ViewModels
{
    public class AddVoucherViewModel
    {
        public int Id { get; set; }
        public VoucherType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
