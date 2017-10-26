using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace WebAPI
{
    /// <summary>
    /// Implementation of the IPersistentSource interface,
    /// using a RESTful Web Service. Original data objects
    /// are transformed before being provided to the
    /// HTTPClient methods.
    /// </summary>
    /// <typeparam name="T">
    /// Type of domain data objects
    /// </typeparam>
    /// <typeparam name="TDO">Type of transformed data objects</typeparam>
    public class WebAPISource<T, TDO> : IPersistentSource<T> 
        where T : IStorable
        where TDO : ITransformedData
    {
        private enum APIMethod { Load, Create, Read, Update, Delete }

        #region Instance fields
        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        private ITransformedDataFactory<T> _tdoFactory;
        #endregion

        #region Constructor
        public WebAPISource(ITransformedDataFactory<T> tdoFactory, string serverURL, string apiID, string apiPrefix = "api")
        {
            _tdoFactory = tdoFactory;
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
        public async Task<List<T>> Load()
        {
            // Retrieve transformed data object from Web Service
            string requestURI = BuildRequestURI(APIMethod.Load);
            List<TDO> dboList = await InvokeHTTPClientMethodWithReturnValueAsync<List<TDO>>(() => _httpClient.GetAsync(requestURI));

            // Transform the retrieved objects to domain objects
            List<T> objList = new List<T>();
            foreach (TDO dbObj in dboList)
            {
                objList.Add(_tdoFactory.CreateDomainObject(dbObj));
            }

            return objList;
        }

        /// <summary>
        /// Save operation is not supported by a Web Service
        /// </summary>
        public Task Save(List<T> objects)
        {
            throw new NotSupportedException("Save not supported for WebAPI Persistency (Use Create/Delete)");
        }

        /// <summary>
        /// Implementation of Create method
        /// </summary>
        /// <param name="obj">Object to create</param>
        public async Task Create(T obj)
        {
            await InvokeHTTPClientMethodNoReturnValueAsync(() => _httpClient.PostAsJsonAsync(BuildRequestURI(APIMethod.Create), _tdoFactory.CreateTransformedObject(obj)));
        }

        /// <summary>
        /// Implementation of Read method
        /// </summary>
        /// <param name="key">Key for object to read</param>
        /// <returns>Domain object matching key</returns>
        public async Task<T> Read(int key)
        {
            TDO dbObj = await InvokeHTTPClientMethodWithReturnValueAsync<TDO>(() => _httpClient.GetAsync(BuildRequestURI(APIMethod.Read, key)));
            return _tdoFactory.CreateDomainObject(dbObj);
        }

        /// <summary>
        /// Implementation of Update method
        /// </summary>
        /// <param name="key">Key for object to update</param>
        /// <param name="obj">Object to update</param>
        public async Task Update(int key, T obj)
        {
            await InvokeHTTPClientMethodNoReturnValueAsync(() => _httpClient.PutAsJsonAsync(BuildRequestURI(APIMethod.Update, key), _tdoFactory.CreateTransformedObject(obj)));
        }

        /// <summary>
        /// Implementation of Delete method
        /// </summary>
        /// <param name="key">Key for object to delete</param>
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