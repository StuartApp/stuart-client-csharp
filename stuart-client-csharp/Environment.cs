namespace StuartDelivery
{
    public sealed class Environment
    {
        private const string sandboxUrl = "https://api.sandbox.stuart.com";
        private const string productionUrl = "https://api.stuart.com";

        public Environment(string url) => BaseUrl = url;

        public static Environment Sandbox => new Environment(sandboxUrl);
        public static Environment Production => new Environment(productionUrl);

        public string BaseUrl { get; }
    }
}
