using StuartDelivery.Abstract;
using StuartDelivery.Concrete;

namespace StuartDelivery
{
    public class StuartApi
    {
        private readonly WebClient _client;
        private readonly Authenticator _authenticator;
        private readonly Environment _environment;

        private IAddress _address;
        private IJob _job;

        public IAddress Address { get { return _address ?? (_address = new Address(_client)); } }
        public IJob Job { get { return _job ?? (_job = new Job(_client)); } }

        public static StuartApi Initialize(Environment environment, string clientId, string clientSecret)
        {
            return new StuartApi(environment, clientId, clientSecret);
        }

        private StuartApi(Environment environment, string clientId, string clientSecret)
        {
            _environment = environment;
            _authenticator = new Authenticator(environment, clientId, clientSecret);
            _client = new WebClient(environment);
            _client.SetAuthorization(_authenticator.GetAccessToken().Result);
        }
    }
}
