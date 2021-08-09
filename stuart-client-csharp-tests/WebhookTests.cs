using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

            var infoResult = await StuartApi.Webhook.Get(createResult.Data.Id);

            infoResult.Data.Id.Should().Equals(createResult.Data.Id);
            infoResult.Data.Url.Should().Equals(spec.Url);
            infoResult.Data.Enabled.Should().Equals(spec.Enabled);
            infoResult.Data.AuthenticationHeader.Should().Equals(spec.AuthenticationHeader);
            infoResult.Data.AuthenticationKey.Should().Equals(spec.AuthenticationKey);

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

        private Models.Webhook.Request.Webhook CreateWebhookSpec()
        {
            return new Models.Webhook.Request.Webhook
            {
                Url = "https://example.org/" + Guid.NewGuid().ToString("N"),
                Enabled = true,
                Topics = new string[]
                {
                    "job/create",
                    "job/update",
                    "delivery/create",
                    "delivery/update",
                    "driver/update",
                    "driver/online",
                    "driver/offline"
                },
                AuthenticationHeader = "X-My-Header",
                AuthenticationKey = "abc123"
            };
        }
    }
}
