using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StuartDelivery.Models.Address;

namespace StuartDeliveryUnitTests
{
    [TestClass]
    public class AddressTests : BaseTests
    {
        [TestMethod]
        public async Task AddressValidate_Should_ReturnTrue()
        {
            //Arrange
            var address = "29 rue de Rivoli 75004 Paris";
            var recivingType = RecivingType.delivering;

            //Act
            var result = await StuartApi.Address.Validate(address, recivingType).ConfigureAwait(false);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task AddressValidate_Should_ReturnTrue_WhenAddressIsAproxAndPhoneIsProvided()
        {
            //Arrange
            var address = "rue de Rivoli 75004 Paris";
            var recivingType = RecivingType.delivering;
            var phone = "123456789";

            //Act
            var result = await StuartApi.Address.Validate(address, recivingType, phone).ConfigureAwait(false);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task AddressValidate_Should_ThrowException()
        {
            //Arrange
            var address = "rue de Rivoli 75004 Paris";
            var recivingType = RecivingType.delivering;

            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Address.Validate(address, recivingType)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetZoneCoverage_Should_ReturnCorrectData()
        {
            // Arrange
            var city = "Paris";
            var recivingType = RecivingType.delivering;

            // Act
            var result = await StuartApi.Address.GetZoneCoverage(city, recivingType).ConfigureAwait(false);

            //Assert
            result.Features.Should().NotBeEmpty();
            result.Type.Should().Be("FeatureCollection");
        }

        [TestMethod]
        public async Task GetZoneCoverage_Should_ThrowException()
        {
            //Arrange
            var city = string.Empty;

            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Address.GetZoneCoverage(city)).ConfigureAwait(false);
        }

        [TestMethod]
        [Ignore("Need to setup data on Stuart's server to perform this test correctly")]
        public async Task GetParcelShops_Should_ReturnCorrectData()
        {
            //Arrange
            var address = "156 rue de Charonne 75011 PARIS";
            var date = DateTime.UtcNow;

            //Act
            var result = await StuartApi.Address.GetParcelShops(address, date).ConfigureAwait(false);

            //Assert
            result.Schedule.Should().NotBeEmpty();
            result.Schedule.First().ParcelShop.Contact.Company.Should().Be("T29 Alimentation generale");
        }

        [TestMethod]
        public async Task GetParcelShops_Should_ThrowEnxception()
        {
            //Arrange
            var address = string.Empty;
            var date = DateTime.UtcNow;

            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Address.GetParcelShops(address, date)).ConfigureAwait(false);
        }
    }
}
