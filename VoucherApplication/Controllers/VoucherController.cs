using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VoucherApplication.Data;
using VoucherApplication.Models.Results;
using VoucherApplication.Models.ViewModels;
using VoucherApplication.Services.Interfaces;

namespace VoucherApplication.Controllers
{
    public class VoucherController : Controller
    {
        private readonly ILogger<VoucherController> _logger;
        private readonly IMapper _mapper;
        private readonly IVoucherService _voucherService;

        public VoucherController(
            ILogger<VoucherController> logger,
            IMapper mapper,
            IVoucherService voucherService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _voucherService = voucherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Success = TempData["success"];
            ViewBag.Message = TempData["mesage"];

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVoucherViewModel voucher)
        {
            var result = await _voucherService.AddVoucherAsync(_mapper.Map<Voucher>(voucher));
            TempData["success"] = result.success;
            TempData["mesage"] = result.mesage;

            return RedirectToAction("Add");
        }

        [HttpGet]
        public IActionResult Redeem()
        {
            ViewBag.Success = TempData["success"];
            ViewBag.Message = TempData["mesage"];

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Redeem(RedeemVoucherViewModel voucher)
        {
            var result = await _voucherService.RedeemVoucherAsync(voucher.Id, voucher.ServiceType);
            TempData["success"] = result.success;
            TempData["mesage"] = result.mesage;

            return RedirectToAction("Redeem");
        }
    }
}
