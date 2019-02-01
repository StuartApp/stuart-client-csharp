using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using StuartDelivery.Abstract;
using StuartDelivery.Models;
using StuartDelivery.Models.Job.Enums;
using JobRequest = StuartDelivery.Models.Job.Request;
using JobResponse = StuartDelivery.Models.Job.Response;

namespace StuartDelivery.Concrete
{
    class Job : IJob
    {
        private readonly WebClient _webClient;

        public Job(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task CancelDelivery(int deliveryId)
        {
            var result = await _webClient.PostAsync($"/v2/deliveries/{deliveryId}/cancel").ConfigureAwait(false);
            if (!result.IsSuccessStatusCode)
            { 
                var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
                throw new HttpRequestException($"Canceling delivery failed with message: {error.Message}");
            }
        }

        public async Task CancelJob(int jobId)
        {
            var result = await _webClient.PostAsync($"/v2/jobs/{jobId}/cancel").ConfigureAwait(false);
            if (!result.IsSuccessStatusCode)
            {
                var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
                throw new HttpRequestException($"Canceling job failed with message: {error.Message}");
            }
        }

        public async Task<JobResponse.Job> CreateJob(JobRequest.JobRequest job)
        {
            var result = await _webClient.PostAsync($"/v2/jobs", job).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<JobResponse.Job>().ConfigureAwait(false);

            var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
            throw new HttpRequestException($"Creating job failed with message: {error.Message}");
        }

        public async Task<string> GetDriversPhone(int deliveryId)
        {
            var result = await _webClient.GetAsync($"/v2/deliveries/{deliveryId}/phone_number").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<string>().ConfigureAwait(false);
            
            var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
            throw new HttpRequestException($"Getting driver's phone failed with message: {error.Message}");
        }

        public async Task<JobResponse.Job> GetJob(int id)
        {
            var result = await _webClient.GetAsync($"/v2/jobs/{id}").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<JobResponse.Job>().ConfigureAwait(false);

            var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
            throw new HttpRequestException($"Getting job failed with message: {error.Message}");
        }

        public async Task<IEnumerable<JobResponse.Job>> GetJobs(string status = "", int? page = null, int? perPage = null, string clientReference = "")
        {
            var uriParams = new List<string>
            {
                $"{(string.IsNullOrEmpty(status) ? string.Empty : "status=" + status)}",
                $"{(page.HasValue ? "page=" + page.Value : string.Empty)}",
                $"{(perPage.HasValue ? "per_page=" + perPage.Value : string.Empty)}",
                $"{(string.IsNullOrEmpty(clientReference) ? string.Empty : "client_reference=" + clientReference)}"
            };

            var urlParams = string.Join("&", uriParams.Where(x => !string.IsNullOrEmpty(x)));
            var result = await _webClient.GetAsync($"/v2/jobs{(string.IsNullOrEmpty(urlParams) ? string.Empty : "?" + urlParams)}").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<IEnumerable<JobResponse.Job>>().ConfigureAwait(false);

            var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
            throw new HttpRequestException($"Getting jobs failed with message: {error.Message}");
        }

        public async Task<JobResponse.SchedulingSlots> GetSchedulingSlots(string city, ScheduleType type, DateTime date)
        {
            var result = await _webClient.GetAsync($"/v2/jobs/schedules/{WebUtility.UrlEncode(city)}/{Enum.GetName(typeof(ScheduleType), type)}/{date.ToString("yyyy-MM-dd")}").ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<JobResponse.SchedulingSlots>().ConfigureAwait(false);

            var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
            throw new HttpRequestException($"Getting scheduling slots failed with message: {error.Message}");
        }

        public async Task<int> RequestEta(JobRequest.JobRequest job)
        {
            var result = await _webClient.PostAsync($"/v2/jobs/eta", job).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsAsync<ExpandoObject>().ConfigureAwait(false);
                return Convert.ToInt32(response.FirstOrDefault(x => x.Key == "eta").Value);
            }

            var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
            throw new HttpRequestException($"Getting ETA failed with message: {error.Message}");
        }

        public async Task<JobResponse.SimplePricing> RequestJobPricing(JobRequest.JobRequest job)
        {
            var result = await _webClient.PostAsync($"/v2/jobs/pricing", job).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsAsync<JobResponse.SimplePricing>().ConfigureAwait(false);

            var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
            throw new HttpRequestException($"Getting job pricing failed with message: {error.Message}");
        }

        public async Task UpdateJob(int id, JobRequest.UpdateJobRequest updatedJob)
        {
            var result = await _webClient.PatchAsync($"/v2/jobs/{id}", updatedJob).ConfigureAwait(false);
            if (!result.IsSuccessStatusCode)
            {
                var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
                throw new HttpRequestException($"Patching job failed with message: {error.Message}");
            }
        }

        public async Task<bool> ValidateParameters(JobRequest.JobRequest job)
        {
            var result = await _webClient.PostAsync($"/v2/jobs/validate", job).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsAsync<ExpandoObject>().ConfigureAwait(false);
                return (bool)response.FirstOrDefault(x => x.Key == "valid").Value;
            }

            var error = result.Content.ReadAsAsync<ErrorResponse>().Result;
            throw new HttpRequestException($"Validation failed with message: {error.Message}");
        }
    }
}
