using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

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
                var response = await _Client.PostAsync("api-token-auth/", content);
                Authentication auth = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(Authentication)) as Authentication;
                if (String.IsNullOrEmpty(auth.Token))
                {
                    throw new Exception(String.Join("\n", auth.Errors));
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

        /// <summary>
        /// Gets current user's profile.
        /// </summary>
        /// <returns>User</returns>
        async public static Task<User> GetUser()
        {
            if (String.IsNullOrEmpty(Auth_Token))
            {
                throw new Exception("End-point requires user authentication.");
            }

            try
            {
                var resp = await _Client.GetAsync("user/");
                var user = JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync(), typeof(User)) as User;

                if (String.IsNullOrEmpty(user.Error))
                {
                    return user;
                } else
                {
                    throw new Exception(user.Error);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets list of user dividends.
        /// </summary>
        /// <returns>List of Dividends.</returns>
        async public static Task<List<Dividend>> GetDividends()
        {
            try
            {
                var resp = await _Client.GetAsync("dividends/");
                var collection = JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync(), typeof(ObjectCollection<Dividend>)) as ObjectCollection<Dividend>;
                if (String.IsNullOrEmpty(collection.Error))
                {
                    return collection.Results;
                } else
                {
                    throw new Exception(collection.Error);
                }
            } catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets list of requested quotes.
        /// </summary>
        /// <param name="symbols">Symbols of entities to get quotes for</param>
        /// <returns>List of Quotes.</returns>
        async public static Task<List<Quote>> GetQuotes(params string[] symbols)
        {
            if (symbols.Length == 0)
            {
                throw new Exception("At least one symbol required.");
            }

            try
            {
                string queryParams = String.Join(",", symbols);
                var resp = await _Client.GetAsync("quotes/?symbols=" + queryParams);
                Debug.WriteLine(await resp.Content.ReadAsStringAsync());
                var collection = JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync(), typeof(ObjectCollection<Quote>)) as ObjectCollection<Quote>;
                if (String.IsNullOrEmpty(collection.Error))
                {
                    return collection.Results;
                } else
                {
                    throw new Exception(collection.Error);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets list of companies.
        /// </summary>
        /// <returns>Instruments List</returns>
        async public static Task<List<Instrument>> GetInstruments()
        {
            try
            {
                var resp = await _Client.GetAsync("instruments/");
                var collection = JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync(), typeof(ObjectCollection<Instrument>)) as ObjectCollection<Instrument>;
                if (String.IsNullOrEmpty(collection.Error))
                {
                    return collection.Results;
                } else
                {
                    throw new Exception(collection.Error);
                }
            } catch (Exception e)
            {
                throw e;
            }
        }
    }
}
