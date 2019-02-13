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
    public class Job : IJob
    {
        private readonly WebClient _webClient;

        public Job(WebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<Result> CancelDelivery(int deliveryId)
        {
            try
            {
                var response = await _webClient.PostAsync($"/v2/deliveries/{deliveryId}/cancel").ConfigureAwait(false);
                var result = new Result();

                if (response.IsSuccessStatusCode)
                    return result;

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;

                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(CancelDelivery)} failed", e);
            }
        }

        public async Task<Result> CancelJob(int jobId)
        {
            try
            {
                var response = await _webClient.PostAsync($"/v2/jobs/{jobId}/cancel").ConfigureAwait(false);
                var result = new Result();

                if (response.IsSuccessStatusCode)
                    return result;

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;

                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(CancelJob)} failed", e);
            }
        }

        public async Task<Result<JobResponse.Job>> CreateJob(JobRequest.JobRequest job)
        {
            try
            {
                var response = await _webClient.PostAsync($"/v2/jobs", job).ConfigureAwait(false);
                var result = new Result<JobResponse.Job>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<JobResponse.Job>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;

                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(CreateJob)} failed", e);
            }
        }

        public async Task<Result<string>> GetDriversPhone(int deliveryId)
        {
            try
            {
                var response = await _webClient.GetAsync($"/v2/deliveries/{deliveryId}/phone_number").ConfigureAwait(false);
                var result = new Result<string>();
                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<string>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(GetDriversPhone)} failed", e);
            }
        }

        public async Task<Result<JobResponse.Job>> GetJob(int id)
        {
            try
            {
                var response = await _webClient.GetAsync($"/v2/jobs/{id}").ConfigureAwait(false);
                var result = new Result<JobResponse.Job>();
                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<JobResponse.Job>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(GetJob)} failed", e);
            }
        }

        public async Task<Result<IEnumerable<JobResponse.Job>>> GetJobs(string status = "", int? page = null, int? perPage = null, string clientReference = "")
        {
            var uriParams = new List<string>
            {
                $"{(string.IsNullOrEmpty(status) ? string.Empty : "status=" + status)}",
                $"{(page.HasValue ? "page=" + page.Value : string.Empty)}",
                $"{(perPage.HasValue ? "per_page=" + perPage.Value : string.Empty)}",
                $"{(string.IsNullOrEmpty(clientReference) ? string.Empty : "client_reference=" + clientReference)}"
            };

            try
            {
                var urlParams = string.Join("&", uriParams.Where(x => !string.IsNullOrEmpty(x)));
                var response = await _webClient.GetAsync($"/v2/jobs{(string.IsNullOrEmpty(urlParams) ? string.Empty : "?" + urlParams)}").ConfigureAwait(false);
                var result = new Result<IEnumerable<JobResponse.Job>>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<IEnumerable<JobResponse.Job>>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(GetJobs)} failed", e);
            }
        }

        public async Task<Result<JobResponse.SchedulingSlots>> GetSchedulingSlots(string city, ScheduleType type, DateTime date)
        {
            try
            {
                var response = await _webClient.GetAsync($"/v2/jobs/schedules/{WebUtility.UrlEncode(city)}/{Enum.GetName(typeof(ScheduleType), type)}/{date:yyyy-MM-dd}").ConfigureAwait(false);
                var result = new Result<JobResponse.SchedulingSlots>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<JobResponse.SchedulingSlots>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(GetSchedulingSlots)} failed", e);
            }
        }

        public async Task<Result<int>> RequestEta(JobRequest.JobRequest job)
        {
            try
            {
                var response = await _webClient.PostAsync($"/v2/jobs/eta", job).ConfigureAwait(false);
                var result = new Result<int>();

                if (response.IsSuccessStatusCode)
                {
                    var obj = await response.Content.ReadAsAsync<ExpandoObject>().ConfigureAwait(false);
                    result.Data = Convert.ToInt32(obj.FirstOrDefault(x => x.Key == "eta").Value);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(RequestEta)} failed", e);
            }
        }

        public async Task<Result<JobResponse.SimplePricing>> RequestJobPricing(JobRequest.JobRequest job)
        {
            try
            {
                var response = await _webClient.PostAsync($"/v2/jobs/pricing", job).ConfigureAwait(false);
                var result = new Result<JobResponse.SimplePricing>();

                if (response.IsSuccessStatusCode)
                {
                    result.Data = await response.Content.ReadAsAsync<JobResponse.SimplePricing>().ConfigureAwait(false);
                    return result;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(RequestJobPricing)} failed", e);
            }
        }

        public async Task<Result> UpdateJob(int id, JobRequest.UpdateJobRequest updatedJob)
        {
            try
            {
                var response = await _webClient.PatchAsync($"/v2/jobs/{id}", updatedJob).ConfigureAwait(false);
                var result = new Result();

                if (response.IsSuccessStatusCode)
                    return result;

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(UpdateJob)} failed", e);
            }
        }

        public async Task<Result<bool>> ValidateParameters(JobRequest.JobRequest job)
        {
            try
            {
                var response = await _webClient.PostAsync($"/v2/jobs/validate", job).ConfigureAwait(false);
                var result = new Result<bool>();

                if (response.IsSuccessStatusCode)
                {
                    var obj = await response.Content.ReadAsAsync<ExpandoObject>().ConfigureAwait(false);
                    result.Data = (bool)obj.FirstOrDefault(x => x.Key == "valid").Value;
                }

                result.Error = response.Content.ReadAsAsync<ErrorResponse>().Result;
                return result;
            }
            catch (Exception e)
            {
                throw new HttpRequestException($"{nameof(ValidateParameters)} failed", e);
            }
        }
    }
}
