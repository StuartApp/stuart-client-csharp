using Newtonsoft.Json.Converters;

namespace StuartDelivery.Converters
{
    internal class StuartDateTimeConverter : IsoDateTimeConverter
    {
        public StuartDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}