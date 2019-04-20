using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UsersManagement.Models;

namespace UsersManagement.Services
{
    public class LoadData : User
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly UserService _userService;
        public IEnumerable<User> users { get; set; }
        public bool GetUsersError { get; set; }

        public LoadData(IHttpClientFactory clientFactory, UserService userService)
        {
            _clientFactory = clientFactory;
            _userService = userService;
        }

        public async Task OnGet()
        {
            if (_userService.checkEmpty())
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                        "https://jsonplaceholder.typicode.com/users");
                request.Headers.Add("Accept", "application/vnd.github.v3+json");
                request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content
                        .ReadAsAsync<IEnumerable<User>>();
                    await _userService.insertManyUsersAsync(users);
                }
                else
                {
                    GetUsersError = true;
                    users = Array.Empty<User>();
                }
            }
            else
            {
                return;
            }
            
        }

    }
}
