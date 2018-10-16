using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StuartDelivery.Models.Job.Enums;
using JobRequest = StuartDelivery.Models.Job.Request;
using JobResponse = StuartDelivery.Models.Job.Response;

namespace StuartDelivery.Abstract
{
    public interface IJob
    {
        Task<JobResponse.Job> CreateJob(JobRequest.JobRequest job);
        Task<JobResponse.SimplePricing> RequestJobPricing(JobRequest.JobRequest job);
        Task<bool> ValidateParameters(JobRequest.JobRequest job);
        Task<int> RequestEta(JobRequest.JobRequest job);

        Task<JobResponse.Job> GetJob(int id);
        Task<IEnumerable<JobResponse.Job>> GetJobs(string status = "", int? page = null, int? perPage = null, string clientReference = "");
        Task<JobResponse.SchedulingSlots> GetSchedulingSlots(string city, ScheduleType type, DateTime date);
        Task<string> GetDriversPhone(int deliveryId);

        Task UpdateJob(int id, JobRequest.UpdateJobRequest updatedJob);

        Task CancelJob(int jobId);
        Task CancelDelivery(int deliveryId);
    }
}
