using System;
using System.Threading.Tasks;
using StuartDelivery.Models.Address;

namespace StuartDelivery.Abstract
{
    public interface IAddress
    {
        Task<bool> Validate(string address, RecivingType recivingType, string phone = "");
        Task<ZoneResponse> GetZoneCoverage(string city, RecivingType recivingType = RecivingType.picking);
        Task<ParcelShopsResponse> GetParcelShops(string address, DateTime date);
    }
}
