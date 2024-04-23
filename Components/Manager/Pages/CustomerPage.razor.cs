﻿using Microsoft.AspNetCore.Components;
using ZiniTechERPSystem.Data;
using ZiniTechERPSystem.Components.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ZiniTechERPSystem.Components.Manager.Pages
{
    public partial class CustomerPage
    {
        public List<Customer> customers = new List<Customer>();

        [Inject]
        private CustomerService CustomerService { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            GetCustomners();
        }

        public async void GetCustomners()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    customers = CustomerService.GetCustomersByManagerId(userId).ToList();
                }
            }
        }
    }
}
