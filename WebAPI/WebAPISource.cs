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
        private ITransformedDataFactory<T> _dboFactory;
        #endregion

        #region Constructor
        public WebAPISource(ITransformedDataFactory<T> dboFactory, string serverURL, string apiID, string apiPrefix = "api")
        {
            _dboFactory = dboFactory;
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
        public async Task<List<T>> Load()
        {
            string requestURI = BuildRequestURI(APIMethod.Load);
            List<TDO> dboList = await InvokeAPIWithReturnValueAsync<List<TDO>>(() => _httpClient.GetAsync(requestURI));
            List<T> objList = new List<T>();
            foreach (TDO dbObj in dboList)
            {
                objList.Add(_dboFactory.CreateDomainObject(dbObj));
            }
            return objList;
        }

        public Task Save(List<T> objects)
        {
            throw new NotSupportedException("Save not supported for WebAPI Persistency (Use Create/Delete)");
        }

        public async Task Create(T obj)
        {
            await InvokeAPINoReturnValueAsync(() => _httpClient.PostAsJsonAsync(BuildRequestURI(APIMethod.Create), _dboFactory.CreateTransformedObject(obj)));
        }

        public async Task<T> Read(int key)
        {
            TDO dbObj = await InvokeAPIWithReturnValueAsync<TDO>(() => _httpClient.GetAsync(BuildRequestURI(APIMethod.Read, key)));
            return _dboFactory.CreateDomainObject(dbObj);
        }

        public async Task Update(int key, T obj)
        {
            await InvokeAPINoReturnValueAsync(() => _httpClient.PutAsJsonAsync(BuildRequestURI(APIMethod.Update, key), _dboFactory.CreateTransformedObject(obj)));
        }

        public async Task Delete(int key)
        {
            await InvokeAPINoReturnValueAsync(() => _httpClient.DeleteAsync(BuildRequestURI(APIMethod.Update, key)));
        } 
        #endregion

        #region Private methods for API method invocation
        private async Task<U> InvokeAPIWithReturnValueAsync<U>(Func<Task<HttpResponseMessage>> apiMethod)
        {
            return await InvokeAPIAsync(apiMethod).Result.Content.ReadAsAsync<U>();
        }

        private async Task InvokeAPINoReturnValueAsync(Func<Task<HttpResponseMessage>> apiMethod)
        {
            await InvokeAPIAsync(apiMethod);
        }

        private async Task<HttpResponseMessage> InvokeAPIAsync(Func<Task<HttpResponseMessage>> apiMethod)
        {
            // Prepare HTTP client for method invocation
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Invoke the method - the method will at some point 
            // return an HttpResponseMessage 
            HttpResponseMessage response = await apiMethod().ConfigureAwait(false);

            // Throw exception if the invocation was unsuccessful
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{(int)response.StatusCode} - {response.ReasonPhrase}");
            }

            // Return the HttpResponseMessage, which we now know 
            // is a response to a successful method invocation
            return response;
        }

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