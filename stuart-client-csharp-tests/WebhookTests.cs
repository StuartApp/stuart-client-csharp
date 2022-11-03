using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StuartDelivery.Models.Webhook.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StuartDelivery.Tests
{
    [TestClass]
    public class WebhookTests : BaseTests
    {
        private Random _random;

        [TestInitialize]
        public override void TestInit()
        {
            _random = new Random();
            base.TestInit();
        }

        [TestMethod]
        public async Task CreateWebhook_Should_CreateAndDeleteWebhook()
        {
            var spec = CreateWebhookSpec();
            var createResult = await StuartApi.Webhook.Create(spec);

            createResult.Data.Should().NotBeNull();
            createResult.Data.Id.Should().BeGreaterThan(0);
            createResult.Data.Url.Should().Be(spec.Url);

            var infoResult = await StuartApi.Webhook.Get(createResult.Data.Id);

            infoResult.Data.Should().NotBeNull();
            infoResult.Data.Id.Should().Be(createResult.Data.Id);
            infoResult.Data.Url.Should().Be(spec.Url);
            infoResult.Data.Enabled.Should().Be(spec.Enabled);
            infoResult.Data.AuthenticationHeader.Should().Be(spec.AuthenticationHeader);
            infoResult.Data.AuthenticationKey.Should().Be(spec.AuthenticationKey);

            var deleteResult = await StuartApi.Webhook.Delete(createResult.Data.Id);

            deleteResult.Data.Should().BeTrue();
        }

        [TestMethod]
        public async Task UpdateWebhook_Should_ContainError()
        {
            var result = await StuartApi.Webhook.Update(_random.Next(100, 1000), CreateWebhookSpec()).ConfigureAwait(false);
            result.Error.Should().NotBeNull();
            result.Error.Error.Should().BeEquivalentTo("NOT_FOUND");
        }

        [TestMethod]
        public async Task DeleteWebhook_Should_ContainError()
        {
            var result = await StuartApi.Webhook.Delete(_random.Next(100, 1000)).ConfigureAwait(false);
            result.Error.Should().NotBeNull();
            result.Error.Error.Should().BeEquivalentTo("NOT_FOUND");
        }

        [TestMethod]
        public async Task CreateWebhook_Should_RespectWebhookTopics()
        {
            var spec = CreateWebhookSpec();
            spec.Topics = new WebhookTopic[]
            {
                WebhookTopic.JobCreate
            };
            var createResult = await StuartApi.Webhook.Create(spec);

            createResult.Data.Should().NotBeNull();
            createResult.Data.Id.Should().BeGreaterThan(0);
            createResult.Data.Topics.Length.Should().Be(1);
            createResult.Data.Topics[0].Should().Be(WebhookTopic.JobCreate);

            var infoResult = await StuartApi.Webhook.Get(createResult.Data.Id);

            infoResult.Data.Id.Should().Be(createResult.Data.Id);
            infoResult.Data.Topics.Length.Should().Be(1);
            infoResult.Data.Topics[0].Should().Be(WebhookTopic.JobCreate);

            var listResult = await StuartApi.Webhook.GetList();
            listResult.Data.Should().NotBeNull();
            listResult.Data.Length.Should().BeGreaterOrEqualTo(1);
            var webhookFromList = listResult.Data.SingleOrDefault(h => h.Id == createResult.Data.Id);
            webhookFromList.Should().NotBeNull();
            webhookFromList.Topics.Length.Should().Be(1);
            webhookFromList.Topics[0].Should().Be(WebhookTopic.JobCreate);

            var deleteResult = await StuartApi.Webhook.Delete(createResult.Data.Id);

            deleteResult.Data.Should().BeTrue();
        }

        private Models.Webhook.Request.Webhook CreateWebhookSpec()
        {
            return new Models.Webhook.Request.Webhook
            {
                Url = "https://example.org/" + Guid.NewGuid().ToString("N"),
                Enabled = true,
                Topics = new WebhookTopic[] {
                    WebhookTopic.JobCreate,
                    WebhookTopic.JobUpdate,
                    WebhookTopic.DeliveryCreate,
                    WebhookTopic.DeliveryUpdate,
                    WebhookTopic.DriverUpdate,
                    WebhookTopic.DriverOnline,
                    WebhookTopic.DriverOffline
                },
                AuthenticationHeader = "X-My-Header",
                AuthenticationKey = "abc123" + _random.Next()
            };
        }
    }
}
