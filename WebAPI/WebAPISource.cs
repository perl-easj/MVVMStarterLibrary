using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace WebAPI.Implementation
{
    /// <summary>
    /// Implementation of IDataSource... interfaces, using a 
    /// RESTful Web Service. Original data objects are transformed 
    /// before being provided to the HTTPClient methods.
    /// </summary>
    /// <typeparam name="TDTO">Type of Data Transfer objects</typeparam>
    public class WebAPISource<TDTO> : IDataSourceCRUD<TDTO>, IDataSourceLoad<TDTO> where TDTO : IStorable
    {
        private enum APIMethod { Load, Create, Read, Update, Delete }

        #region Instance fields
        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        #region Constructor
        public WebAPISource(string serverURL, string apiID, string apiPrefix = "api")
        {
            _serverURL = serverURL;
            _apiID = apiID;
            _apiPrefix = apiPrefix;
            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.UseDefaultCredentials = true;
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.BaseAddress = new Uri(_serverURL);
        }
        #endregion

        #region Implementation of IPersistentSource
        /// <summary>
        /// Implementation of Load method
        /// </summary>
        /// <returns>List of domain objects</returns>
        public async Task<List<TDTO>> Load()
        {
            return await InvokeHTTPClientMethodWithReturnValueAsync<List<TDTO>>(() => _httpClient.GetAsync(BuildRequestURI(APIMethod.Load)));
        }

        /// <summary>
        /// Implementation of Create method. Create is a bit atypical,
        /// since we need to return the key value associated with the
        /// newly created object. This is relevant if the data is saved
        /// in a database table, where the table itself chooses the next
        /// key for the object (e.g. an auto-increment setting)
        /// </summary>
        /// <param name="obj">DTO to create</param>
        public async Task<int> Create(TDTO obj)
        {
            HttpResponseMessage response = await InvokeHTTPClientMethodAsync((() => _httpClient.PostAsJsonAsync(BuildRequestURI(APIMethod.Create), obj)));
            TDTO createdObj = await response.Content.ReadAsAsync<TDTO>();
            return createdObj.Key;
        }

        /// <summary>
        /// Implementation of Read method
        /// </summary>
        /// <param name="key">Key for object to read</param>
        /// <returns>DTO matching key</returns>
        public async Task<TDTO> Read(int key)
        {
            return await InvokeHTTPClientMethodWithReturnValueAsync<TDTO>(() => _httpClient.GetAsync(BuildRequestURI(APIMethod.Read, key)));
        }

        /// <summary>
        /// Implementation of Update method
        /// </summary>
        /// <param name="key">Key for object to update</param>
        /// <param name="obj">DTO to update</param>
        public async Task Update(int key, TDTO obj)
        {
            await InvokeHTTPClientMethodNoReturnValueAsync(() => _httpClient.PutAsJsonAsync(BuildRequestURI(APIMethod.Update, key), obj));
        }

        /// <summary>
        /// Implementation of Delete method
        /// </summary>
        /// <param name="key">Key for DTO to delete</param>
        public async Task Delete(int key)
        {
            await InvokeHTTPClientMethodNoReturnValueAsync(() => _httpClient.DeleteAsync(BuildRequestURI(APIMethod.Update, key)));
        }
        #endregion

        #region Private methods for HTTPClient method invocation
        /// <summary>
        /// Invokes a HTTP client method which returns a value.
        /// </summary>
        /// <typeparam name="U">Type of retyrn value</typeparam>
        /// <param name="httpClientMethod">Specific HTTPClient method to invoke</param>
        /// <returns></returns>
        private async Task<U> InvokeHTTPClientMethodWithReturnValueAsync<U>(Func<Task<HttpResponseMessage>> httpClientMethod)
        {
            return await InvokeHTTPClientMethodAsync(httpClientMethod).Result.Content.ReadAsAsync<U>();
        }

        /// <summary>
        /// Invokes a HTTP client method which does not return a value.
        /// </summary>
        /// <param name="httpClientMethod">Specific HTTPClient method to invoke</param>
        private async Task InvokeHTTPClientMethodNoReturnValueAsync(Func<Task<HttpResponseMessage>> httpClientMethod)
        {
            await InvokeHTTPClientMethodAsync(httpClientMethod);
        }

        /// <summary>
        /// Central method for invocation of HTTPClient methods
        /// </summary>
        /// <param name="httpClientMethod">Specific HTTPClient method to invoke</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> InvokeHTTPClientMethodAsync(Func<Task<HttpResponseMessage>> httpClientMethod)
        {
            // Prepare HTTP client for method invocation
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Invoke the method - the method will at some point 
            // return an HttpResponseMessage 
            HttpResponseMessage response = await httpClientMethod().ConfigureAwait(false);

            // Throw exception if the invocation was unsuccessful
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{(int)response.StatusCode} - {response.ReasonPhrase}");
            }

            // Return the HttpResponseMessage, which we now know 
            // is a response to a successful method invocation
            return response;
        }

        /// <summary>
        /// Build a proper request URI, corresponding 
        /// to the API method being invoked.
        /// </summary>
        /// <param name="method">API method to invoke</param>
        /// <param name="key">Object key (if relevant)</param>
        /// <returns>HTTP request URI</returns>
        private string BuildRequestURI(APIMethod method, int key = 0)
        {
            switch (method)
            {
                case APIMethod.Load:
                    return $"{_apiPrefix}/{_apiID}";
                case APIMethod.Create:
                    return $"{_apiPrefix}/{_apiID}";
                case APIMethod.Read:
                    return $"{_apiPrefix}/{_apiID}/{key}";
                case APIMethod.Update:
                    return $"{_apiPrefix}/{_apiID}/{key}";
                case APIMethod.Delete:
                    return $"{_apiPrefix}/{_apiID}/{key}";
                default:
                    throw new ArgumentException("BuildRequestURI");
            }
        }
        #endregion
    }
}