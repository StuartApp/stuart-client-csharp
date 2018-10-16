using Microsoft.VisualStudio.TestTools.UnitTesting;
using StuartDelivery;

namespace StuartDeliveryUnitTests
{
    [TestClass]
    public class BaseTests
    {
        public StuartApi StuartApi { get; set; }

        [TestInitialize]
        public virtual void TestInit()
        {
            var clientId = "<your_client_id>";
            var clientSecret = "<your_client_secret>";

            //Please always use Sandbox environment for tests purposes
            var environment = Environment.Sandbox;

            StuartApi = StuartApi.Initialize(environment, clientId, clientSecret);
        }
    }
}
