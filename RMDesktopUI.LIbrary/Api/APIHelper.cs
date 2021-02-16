using RMDesktopUI.LIbrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.LIbrary.Api
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient _apiClient { get; set; }
        private readonly ILoggedInUserModel _loggedInUser;
        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            _loggedInUser = loggedInUser;
            Initialize_apiClient();
        }
        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }


        private void Initialize_apiClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<AuthenticateUser> Authenticate(string userName, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type","password"),
            new KeyValuePair<string, string>("username", userName),
            new KeyValuePair<string, string>("password", password)
            });
            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticateUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");
            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/User/"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.FirstName = result.FirstName;
                    _loggedInUser.LastName = result.LastName;
                    _loggedInUser.EmailAddress = result.EmailAddress;
                    _loggedInUser.CreateDate = result.CreateDate;
                    _loggedInUser.Token = token;


                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            }
    }
}
