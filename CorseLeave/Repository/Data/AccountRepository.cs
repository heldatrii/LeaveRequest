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
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public AccountRepository(Address address, string request = "Accounts/") : base(address, request)
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

        public async Task<List<ApplyDetailIDVM>> ApplyListID(string NIK)
        {
            List<ApplyDetailIDVM> entities = new List<ApplyDetailIDVM>();

            using (var response = await httpClient.GetAsync(request+ "ApplyListID/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ApplyDetailIDVM>>(apiResponse);
            }
            return entities;
        }
        
        public async Task<List<ApplyDetailVM>> ApplyListManager(string NIK)
        {
            List<ApplyDetailVM> entities = new List<ApplyDetailVM>();

            using (var response = await httpClient.GetAsync(request+"ApplyListManager/"+NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ApplyDetailVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<ApplyDetailVM> ApplyDetail(string IdRequest)
        {
            ApplyDetailVM entities = null;

            using (var response = await httpClient.GetAsync(request + "ApplyDetail/" + IdRequest))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<ApplyDetailVM>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode CheckPassword(CheckPasswordVM checkPasswordVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(checkPasswordVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "CheckPassword/", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Apply(ApplyVM applyVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(applyVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "Apply", content).Result;
            return result.StatusCode;
        }
    }
}
