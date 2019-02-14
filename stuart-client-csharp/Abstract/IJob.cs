using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StuartDelivery.Models;
using StuartDelivery.Models.Job.Enums;
using JobRequest = StuartDelivery.Models.Job.Request;
using JobResponse = StuartDelivery.Models.Job.Response;

namespace StuartDelivery.Abstract
{
    public interface IJob
    {
        /// <summary>
        /// With the Stuart API you can request a driver to handle up to 8 deliveries.
        /// You don't have to bother with itinerary, you only need to indicate
        /// the origin and all the destinations: we will find the best route for you.
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        Task<Result<JobResponse.Job>> CreateJob(JobRequest.JobRequest job);

        /// <summary>
        /// This endpoint allows you to get a pricing for a Job (VAT excluded) by using exactly the same request body as CreateJob endpoint.
        /// </summary>
        /// <param name="job"></param>
        /// <seealso cref="CreateJob(JobRequest.JobRequest)"/>
        /// <returns></returns>
        Task<Result<JobResponse.SimplePricing>> RequestJobPricing(JobRequest.JobRequest job);

        /// <summary>
        /// Allows you to validate a Job before creating it by passing all the parameters required to create a Job.
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        Task<Result<bool>> ValidateParameters(JobRequest.JobRequest job);

        /// <summary>
        /// This endpoint allows you to get an estimated time of arrival at the origin address for a given Job by using exactly the same request body as CreateJob endpoint.
        /// </summary>
        /// <param name="job"></param>
        /// <seealso cref="CreateJob(JobRequest.JobRequest)"/>
        /// <returns></returns>
        Task<Result<int>> RequestEta(JobRequest.JobRequest job);

        /// <summary>
        /// Once you successfully created a Job using CreateJob endpoint, you will be able to get the Job details at any time by using the job ID.
        /// </summary>
        /// <param name="id"></param>
        /// <seealso cref="CreateJob(JobRequest.JobRequest)"/>
        /// <returns></returns>
        Task<Result<JobResponse.Job>> GetJob(int id);

        /// <summary>
        /// You can retreive all the jobs you created using Stuart API or any other client ( or mobile applications).
        /// </summary>
        /// <param name="status">Get jobs with selected status</param>
        /// <param name="page">Get page (used for pagination)</param>
        /// <param name="perPage">Get number elements per page (used for pagintaion)</param>
        /// <param name="clientReference">Get jobs with selected client reference</param>
        /// <returns>List of Jobs</returns>
        Task<Result<IEnumerable<JobResponse.Job>>> GetJobs(string status = "", int? page = null, int? perPage = null, string clientReference = "");

        /// <summary>
        /// You can request this endpoint in order to present your end customers all the schedule slots the Stuart API offers.
        /// Please note the schedules may differ from one city to another.
        /// </summary>
        /// <param name="city">The zone you want to know the scheduling slots. Allowed Values: paris, lyon, london, madrid, barcelona</param>
        /// <param name="type"></param>
        /// <param name="date">The specific day you want to know the scheduling slots.</param>
        /// <returns></returns>
        Task<Result<JobResponse.SchedulingSlots>> GetSchedulingSlots(string city, ScheduleType type, DateTime date);

        /// <summary>
        /// As soon as a driver has accepted your job it is possible to request his phone number with this endpoint.
        /// Please note that the driver's phone number is anonymised. That's why you can only request the driver's phone number during the delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        Task<Result<string>> GetDriversPhone(int deliveryId);

        /// <summary>
        /// Once you successfully created a Job using CreateJob endpoint, you will be able to update some of its informations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedJob"></param>
        /// <seealso cref="CreateJob(JobRequest.JobRequest)"/>
        /// <returns></returns>
        Task<Result> UpdateJob(int id, JobRequest.UpdateJobRequest updatedJob);

        /// <summary>
        /// Using this endpoint you can cancel all deliveries of a Job.
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<Result> CancelJob(int jobId);

        /// <summary>
        /// Using this endpoint you can cancel a specific Delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        Task<Result> CancelDelivery(int deliveryId);
    }
}
