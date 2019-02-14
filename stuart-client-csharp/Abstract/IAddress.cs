using System;
using System.Threading.Tasks;
using StuartDelivery.Models;
using StuartDelivery.Models.Address;

namespace StuartDelivery.Abstract
{
    public interface IAddress
    {
        /// <summary>
        /// Before you CreateJob you can ensure your pickup and drop-offs are eligible for a Stuart delivery.
        /// </summary>
        /// <param name="address">Full-text address you want to validate.</param>
        /// <param name="receivingType"></param>
        /// <param name="phone">Phone number is required when the address is not precise enough.</param>
        /// <seealso cref="IJob.CreateJob(Models.Job.Request.JobRequest)"/>
        /// <returns></returns>
        Task<Result<bool>> Validate(string address, ReceivingType receivingType, string phone = "");

        /// <summary>
        /// This endpoint allows you to determine for each city the Stuart picking/delivering coverage area.
        /// </summary>
        /// <param name="city">Allowed Values: paris, lyon, london, barcelona, madrid</param>
        /// <param name="receivingType"></param>
        /// <returns></returns>
        Task<Result<ZoneResponse>> GetZoneCoverage(string city, ReceivingType receivingType = ReceivingType.picking);

        /// <summary>
        /// The endpoint allows you to get all the available time slots for a driver to come at your customer's location for a return trip to a parcel shop location.
        /// </summary>
        /// <param name="address">Full-text address you want be picking from.</param>
        /// <param name="date">Date in the format: yyyy-mm-dd.</param>
        /// <returns></returns>
        Task<Result<ParcelShopsResponse>> GetParcelShops(string address, DateTime date);
    }
}
