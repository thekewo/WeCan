using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VoucherApplication.Data.Interfaces;
using Microsoft.Extensions.Logging;
using VoucherApplication.Data;
using VoucherApplication.Models.Enums;

namespace VoucherApplication.Services.Tests
{
    [TestClass()]
    public class VoucherServiceTests
    {
        private Mock<IVoucherRepository> _repository = new Mock<IVoucherRepository>();
        private Mock<ILogger<VoucherService>> _logger = new Mock<ILogger<VoucherService>>();
        private VoucherService _service;
        private Voucher _voucher;

        [TestInitialize]
        public void Initialize()
        {
            _service = new VoucherService(_repository.Object, _logger.Object);
        }

        [TestMethod()]
        public async Task AddVoucherAsyncTestWhenVoucherIsInTheDb()
        {
            // Arrange
            _repository.Setup(r => r.IsVoucherInDbAsync(It.IsAny<int>())).ReturnsAsync(true);
            var voucher = new Voucher()
            {
                Id = 1,
                ExpirationDate = DateTime.Now,
                Quantity = 1,
                ServiceType = ServiceType.Service1,
                Type = VoucherType.SingleRedemption
            };

            // Act
            var result = await _service.AddVoucherAsync(voucher);

            // Assert
            Assert.AreEqual(result.success, false);
            Assert.AreEqual(result.mesage, "Voucher with this id already exists.");
        }

        [TestMethod()]
        public async Task AddVoucherAsyncTestWhenVoucherIsNotInTheDb()
        {
            // Arrange
            _repository.Setup(r => r.IsVoucherInDbAsync(It.IsAny<int>())).ReturnsAsync(false);
            var voucher = new Voucher()
            {
                Id = 1,
                ExpirationDate = DateTime.Now,
                Quantity = 1,
                ServiceType = ServiceType.Service1,
                Type = VoucherType.SingleRedemption
            };

            // Act
            var result = await _service.AddVoucherAsync(voucher);

            // Assert
            Assert.AreEqual(result.success, true);
            Assert.AreEqual(result.mesage, "Voucher added.");
        }

        [TestMethod()]
        public async Task RedeemVoucherAsyncTestWhenVoucherWithIdIsNotInDbWithCorrectQualities()
        {
            // Arrange
            _repository.Setup(r => r.GetVoucherByIdAsync(It.IsAny<int>())).ReturnsAsync(
                new Voucher()
                {
                    Id = 1,
                    ExpirationDate = DateTime.Now,
                    Quantity = 1,
                    ServiceType = ServiceType.Service1,
                    Type = VoucherType.SingleRedemption
                });

            // Act
            var result = await _service.RedeemVoucherAsync(1, ServiceType.Service1);

            // Assert
            Assert.AreEqual(result.success, true);
            Assert.AreEqual(result.mesage, "Voucher reedemed.");
        }

        [TestMethod()]
        public async Task RedeemVoucherAsyncTestWhenVoucherWithIdIsNotInDb()
        {
            // Arrange

            // Act
            var result = await _service.RedeemVoucherAsync(2, ServiceType.Service2);

            // Assert
            Assert.AreEqual(result.success, false);
            Assert.AreEqual(result.mesage, "There is no valid voucher with the given id.");
        }
    }
}