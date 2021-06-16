﻿using CorseLeave.Base;
using LeaveRequest.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CorseLeave.Repository.Data
{
    public class RoleAccountRepository : GeneralRepository<RoleAccount, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public RoleAccountRepository(Address address, string request = "RoleAccounts/") : base(address, request)
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
    }
}
