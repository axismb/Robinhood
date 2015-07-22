using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Robinhood
{
    public static class Api
    {
        private static HttpClient _Client { get; set; }

        private const string API_BASE_URL = "https://api.robinhood.com/";   // Api Base URL
        private static string Auth_Token { get; set; }

        static Api()
        {
            _Client = new HttpClient() { BaseAddress = new Uri(API_BASE_URL) };
        }

        /// <summary>
        /// Login to Robinhood.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns></returns>
        async public static Task Login(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("username");
            }

            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            FormUrlEncodedContent content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

            try
            {
                var response = await _Client.PostAsync("api-token-auth", content);
                Authentication auth = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(Authentication)) as Authentication;
                if (String.IsNullOrEmpty(auth.Token))
                {
                    throw new Exception(auth.Errors.ToString());
                }
                else
                {
                    Auth_Token = auth.Token;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
