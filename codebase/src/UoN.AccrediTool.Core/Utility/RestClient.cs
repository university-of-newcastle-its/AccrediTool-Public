using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UoN.AccrediTool.Core.Utility
{
    /// <summary>
    /// This class provides a Rest Client for consuming any REST service
    /// </summary>
    public class RestClient : IDisposable
    {
        private bool isDisposed;
        private HttpClient _client;
        private Uri _baseUri;
        private Uri _loginPath;
        private string _token;
        private string _tokenType;
        /// <summary>
        /// This method logs in to the service endpoint and fetches the token for future use
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="loginPath"></param>
        /// <param name="loginModel"></param>
        public RestClient(Uri baseUri, Uri loginPath, object loginModel)
        {
            _client = new HttpClient();
            _baseUri = baseUri;
            _client.BaseAddress = _baseUri;
            _loginPath = loginPath;
            login(loginModel);
        }
        /// <summary>
        /// This method assumes basic auth or no auth
        /// </summary>
        /// <param name="baseUri"></param>
        public RestClient(Uri baseUri, string userName, string password)
        {
            _client = new HttpClient();
            _baseUri = baseUri;
            _client.BaseAddress = _baseUri;
            if(!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
                setupBasicAuthToken(userName, password);
        }

        private void setupBasicAuthToken(string userName, string password)
        {
            _token = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            _tokenType = "Basic";
        }
        private void login(object loginModel)
        {
            using (var stringContent = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json"))
            {
                var response = _client.PostAsync(_loginPath, stringContent).Result;
                if(response.IsSuccessStatusCode)
                {
                    _token = JsonSerializer.Deserialize<Token>(response.Content.ReadAsStringAsync().Result).TokenString;
                    _tokenType = "Bearer";
                }
            }
        } 
        public T Get<T>(Uri path)
        {
            T t = default(T);
            if(!string.IsNullOrEmpty(_token))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenType, _token);

            var response = _client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                t = JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
            }
            return t;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // free managed resources
                _client.Dispose();
            }

            isDisposed = true;
        }

        private class Token
        {
            [JsonPropertyName("token")]
            public string TokenString { get; set; }
        }
    }
}
