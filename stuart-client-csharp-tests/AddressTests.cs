using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StuartDelivery.Models.Address;

namespace StuartDelivery.Tests
{
    [TestClass]
    public class AddressTests : BaseTests
    {
        [TestMethod]
        public async Task AddressValidate_Should_ReturnTrue()
        {
            //Arrange
            const string address = "29 rue de Rivoli 75004 Paris";
            const ReceivingType receivingType = ReceivingType.delivering;

            //Act
            var result = await StuartApi.Address.Validate(address, receivingType).ConfigureAwait(false);

            //Assert
            result.Data.Should().BeTrue();
        }

        [TestMethod]
        public async Task AddressValidate_Should_ReturnTrue_WhenAddressIsApproxAndPhoneIsProvided()
        {
            //Arrange
            const string address = "rue de Rivoli 75004 Paris";
            const ReceivingType receivingType = ReceivingType.delivering;
            const string phone = "123456789";

            //Act
            var result = await StuartApi.Address.Validate(address, receivingType, phone).ConfigureAwait(false);

            //Assert
            result.Data.Should().BeTrue();
        }

        [TestMethod]
        public async Task AddressValidate_Should_ThrowException()
        {
            //Arrange
            const string address = "rue de Rivoli 75004 Paris";
            const ReceivingType receivingType = ReceivingType.delivering;

            //Act
            var result = await StuartApi.Address.Validate(address, receivingType).ConfigureAwait(false);

            //Assert
            result.Error.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetZoneCoverage_Should_ReturnCorrectData()
        {
            // Arrange
            const string city = "Paris";
            const ReceivingType receivingType = ReceivingType.delivering;

            // Act
            var result = await StuartApi.Address.GetZoneCoverage(city, receivingType).ConfigureAwait(false);

            //Assert
            result.Data.Features.Should().NotBeEmpty();
            result.Data.Type.Should().Be("FeatureCollection");
        }

        [TestMethod]
        public async Task GetZoneCoverage_Should_ThrowException()
        {
            //Arrange
            var city = string.Empty;

            //Act
            var result = await StuartApi.Address.GetZoneCoverage(city).ConfigureAwait(false);

            //Assert
            result.Error.Should().NotBeNull();
        }

        [TestMethod]
        [Ignore("Need to setup data on Stuart's server to perform this test correctly")]
        public async Task GetParcelShops_Should_ReturnCorrectData()
        {
            //Arrange
            const string address = "156 rue de Charonne 75011 PARIS";
            var date = DateTime.UtcNow;

            //Act
            var result = await StuartApi.Address.GetParcelShops(address, date).ConfigureAwait(false);

            //Assert
            result.Data.Schedule.Should().NotBeEmpty();
            result.Data.Schedule.First().ParcelShop.Contact.Company.Should().Be("T29 Alimentation generale");
        }

        [TestMethod]
        public async Task GetParcelShops_Should_ThrowException()
        {
            //Arrange
            var address = string.Empty;
            var date = DateTime.UtcNow;

            //Act
            var result = await StuartApi.Address.GetParcelShops(address, date).ConfigureAwait(false);

            //Assert
            result.Error.Should().NotBeNull();
        }
    }
}
