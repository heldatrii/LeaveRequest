using CorseLeave.Base;
using LeaveRequest.Models;
using LeaveRequest.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorseLeave.Repository.Data
{
    public class RequestStatusRepository : GeneralRepository<RequestStatus, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public RequestStatusRepository(Address address, string request = "RequestStatus/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }
            
        public HttpStatusCode LeaveRequestResponse(ResponseVM responseVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(responseVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request, content).Result;
            return result.StatusCode;
        }

        public async Task<RequestStatus> GetByRequestId(int RequestId)
        {
            RequestStatus entities = null;

            using (var response = await httpClient.GetAsync(request + "GetByRequestId/" + RequestId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<RequestStatus>(apiResponse);
            }
            return entities;
        }
    }
}
