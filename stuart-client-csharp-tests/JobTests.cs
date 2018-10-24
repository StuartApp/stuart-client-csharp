using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StuartDelivery.Models.Job.Enums;
using JobRequest = StuartDelivery.Models.Job.Request;

namespace StuartDeliveryTests
{
    [TestClass]
    public class JobTests : BaseTests
    {
        private Random _random;

        [TestInitialize]
        public override void TestInit()
        {
            _random = new Random();
            base.TestInit();
        }

        [TestMethod]
        //[Ignore("PickUpAt and DropOffAt error not handled")]
        public async Task CreateJob_Should_CreateAndReturnJobCorrectly()
        {
            //Arrange
            var jobRequest = CreateJob();

            //Act
            var result = await StuartApi.Job.CreateJob(jobRequest).ConfigureAwait(false);

            //Assert
            result.Id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task CreateJob_Should_ThrowException()
        {
            //Arrange
            var jobRequest = CreateJob();
            jobRequest.Job.TransportType = TransportType.car;

            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.CreateJob(jobRequest)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task RequestJobPricing_Should_ReturnPricesCorrectly()
        {
            //Arrange
            var jobRequest = CreateJob();
            jobRequest.Job.DropOffs.First().ClientReference = "Order_ID#1234_3";
            jobRequest.Job.DropOffs.Last().ClientReference = "Order_ID#1234_4";

            //Act
            var result = await StuartApi.Job.RequestJobPricing(jobRequest).ConfigureAwait(false);

            //Assert
            result.Amount.Should().Be(19.4);
            result.Currency.Should().Be("EUR");
        }

        [TestMethod]
        public async Task RequestJobPricing_Should_ThrowException()
        {
            //Arrange
            var jobRequest = CreateJob();
            jobRequest.Job.TransportType = TransportType.car;

            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.RequestJobPricing(jobRequest)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task ValidateParameters_Should_ValidateDataCorrectly()
        {
            //Arrange
            var jobRequest = CreateJob();

            //Act
            var result = await StuartApi.Job.ValidateParameters(jobRequest).ConfigureAwait(false);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task ValidateParameters_Should_ThrowException()
        {
            //Arrange
            var jobRequest = CreateJob();
            jobRequest.Job.TransportType = TransportType.car;

            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.ValidateParameters(jobRequest)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task RequestEta_Should_ReturnCorrectData()
        {
            //Arrange
            var jobRequest = CreateJob();

            //Act
            var result = await StuartApi.Job.RequestEta(jobRequest).ConfigureAwait(false);

            //Assert
            result.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task RequestEta_Should_ThrowException()
        {
            //Arrange
            var jobRequest = CreateJob();
            jobRequest.Job.TransportType = TransportType.car;

            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.RequestEta(jobRequest)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetJob_Should_ReturnJobCorrectly()
        {
            //Arrange
            var jobRequest = CreateJob();
            var job = await StuartApi.Job.CreateJob(jobRequest).ConfigureAwait(false);

            //Act
            var result = await StuartApi.Job.GetJob(job.Id).ConfigureAwait(false);

            //Assert
            result.Deliveries.Should().NotBeEmpty();
            result.Distance.Should().Be(2.395);
            result.PackageType.Should().Be(PackageSizeType.large);
        }

        [TestMethod]
        public async Task GetJob_Should_ThrowException()
        {
            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.GetJob(123)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetJobs_Should_ReturnJobsCorrectly()
        {
            //Arrange
            var job = CreateJob();

            await StuartApi.Job.CreateJob(job).ConfigureAwait(false);

            //Act
            var result = await StuartApi.Job.GetJobs().ConfigureAwait(false);

            //Assert
            result.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task GetJobs_Should_ReturnJobsCorrectlyWithSpecifiedStatus()
        {
            //Act
            var result = await StuartApi.Job.GetJobs("finished").ConfigureAwait(false);

            //Assert
            result.Where(x => x.Status != "finished").Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetJobs_Should_ReturnJobsCorrectlyWithPages()
        {
            //Arrange
            var job1 = CreateJob();
            var job2 = CreateJob();

            await StuartApi.Job.CreateJob(job1).ConfigureAwait(false);
            await StuartApi.Job.CreateJob(job2).ConfigureAwait(false);

            //Act
            var resultFromPage1 = await StuartApi.Job.GetJobs(page: 1, perPage: 1).ConfigureAwait(false);
            var resultFromPage2 = await StuartApi.Job.GetJobs(page: 2, perPage: 1).ConfigureAwait(false);

            //Assert
            resultFromPage1.Count().Should().Be(1);
            resultFromPage2.Count().Should().Be(1);
            resultFromPage1.First().Id.Should().NotBe(resultFromPage2.First().Id);
        }

        [TestMethod]
        public async Task GetSchedulingSlots_Should_ReturnSchedulingSlotsCorrectly()
        {
            //Arrange
            var city = "Paris";
            var type = ScheduleType.pickup;
            var date = DateTime.Now.AddMinutes(30);

            //Act
            var result = await StuartApi.Job.GetSchedulingSlots(city, type, date).ConfigureAwait(false);

            //Assert
            result.Slots.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task GetSchedulingSlots_Should_ThrowException()
        {
            //Arrange
            var city = "Warsaw";
            var type = ScheduleType.pickup;
            var date = DateTime.Now.AddMinutes(30);

            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.GetSchedulingSlots(city, type, date)).ConfigureAwait(false);
        }

        [TestMethod]
        [Ignore("Access token considered as invalid")]
        public async Task GetDriversPhone_Should_ReturnDriverPhoneCorrectly()
        {
            //Arrange
            var job = await StuartApi.Job.GetJobs().ConfigureAwait(false);

            //Act
            var result = await StuartApi.Job.GetDriversPhone(job.Last().Deliveries.First().Id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task GetDriversPhone_Should_ThrowException()
        {
            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.GetDriversPhone(0)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task UpdateJob_Should_UpdateJobCorrectly()
        {
            //Arrange
            var jobRequest = CreateJob();
            var job = await StuartApi.Job.CreateJob(jobRequest).ConfigureAwait(false);
            var updatedClientReference = "UPDATED_ID";
            var updatedJob = new JobRequest.UpdateJobRequest
            {
                Job = new JobRequest.UpdatedJob
                {
                    Deliveries = new List<JobRequest.Delivery>
                    {
                        new JobRequest.Delivery
                        {
                            Id = job.Deliveries.First().Id,
                            ClientReference = updatedClientReference
                        }
                    }
                }
            };

            //Act
            await StuartApi.Job.UpdateJob(job.Id, updatedJob).ConfigureAwait(false);
            var updateResult = await StuartApi.Job.GetJob(job.Id).ConfigureAwait(false);

            //Assert
            updateResult.Deliveries.First().ClientReference.Should().Be(updatedClientReference);
        }

        [TestMethod]
        public async Task UpdateJob_Should_ThrowException()
        {
            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.UpdateJob(0, null)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task CancelJob_Should_CancelJobCorrectly()
        {
            //Arrange
            var jobRequest = CreateJob();
            var job = await StuartApi.Job.CreateJob(jobRequest).ConfigureAwait(false);

            //Act
            await StuartApi.Job.CancelJob(job.Id);
            var canceledJob = await StuartApi.Job.GetJob(job.Id).ConfigureAwait(false);

            //Assert
            canceledJob.Status.Should().Be("canceled");
        }

        [TestMethod]
        public async Task CancelJob_Should_ThrowException()
        {
            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.CancelJob(0)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task CancelDelivery_Should_CancelDeliveryCorrectly()
        {
            //Arrange
            var jobRequest = CreateJob();
            var job = await StuartApi.Job.CreateJob(jobRequest).ConfigureAwait(false);

            //Act
            await StuartApi.Job.CancelDelivery(job.Deliveries.First().Id);
            var canceledDeliveryJob = await StuartApi.Job.GetJob(job.Id).ConfigureAwait(false);

            //Assert
            canceledDeliveryJob.Status.Should().NotBe("canceled");
            canceledDeliveryJob.Deliveries.First().Status.Should().Be("cancelled");
            canceledDeliveryJob.Deliveries.Last().Status.Should().NotBe("cancelled");
        }

        [TestMethod]
        public async Task CancelDelivery_Should_ThrowException()
        {
            //Act & assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => StuartApi.Job.CancelDelivery(0)).ConfigureAwait(false);
        }

        private JobRequest.JobRequest CreateJob()
        {
            var job = new JobRequest.JobRequest
            {
                Job = new JobRequest.Job
                {
                    AssigmentCode = "ACC861MM",
                    PickUps = new List<JobRequest.PickUp>
                    {
                        new JobRequest.PickUp
                        {
                            Address = "12 rue rivoli, 75001 Paris",
                            Comment = "Ask Bobby",
                            Contact = new JobRequest.Contact
                            {
                                FirstName = "Bobby",
                                LastName = "Brown",
                                Phone = "+33610101010",
                                Email = "bobby.brown@pizzahut.com",
                                Company = "Pizza Hut"
                            }
                        }
                    },
                    DropOffs = new List<JobRequest.DropOff>
                    {
                        new JobRequest.DropOff
                        {
                            PackageType = PackageSizeType.small,
                            PackageDescription = "The blue one.",
                            ClientReference = $"Order_ID#{_random.Next(10000)}",
                            Address = "42 rue rivoli, 75001 Paris",
                            Comment = "2nd floor on the left",
                            Contact = new JobRequest.Contact
                            {
                                FirstName = "Dany",
                                LastName = "Dan",
                                Phone = "+33611112222",
                                Email = "client1@email.com",
                                Company = "Sample Company Inc."
                            }
                        },
                        new JobRequest.DropOff
                        {
                            PackageType = PackageSizeType.large,
                            PackageDescription = "The red one.",
                            ClientReference = $"Order_ID#{_random.Next(10000)}",
                            Address = "6 Place des Vosges, 75004 Paris",
                            Comment = "2nd floor on the left",
                            Contact = new JobRequest.Contact
                            {
                                FirstName = "John",
                                LastName = "Doe",
                                Phone = "+33611111111",
                                Email = "client2@email.com",
                                Company = "Sample Company Inc."
                            }
                        }
                    }
                }
            };

            return job;
        }
    }
}
