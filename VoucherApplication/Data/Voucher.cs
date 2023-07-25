using System.ComponentModel.DataAnnotations;
using VoucherApplication.Models.Enums;

namespace VoucherApplication.Data
{
    public class Voucher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public VoucherType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public ServiceType ServiceType { get; set; }
    }
}
