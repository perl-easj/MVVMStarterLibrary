using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;

namespace WebAPI
{
    public class WebAPISource<T> : IPersistentSource<T> where T : IStorable
    {
        private string _serverURL;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;

        public WebAPISource(string serverURL, string apiID)
        {
            _serverURL = serverURL;
            _apiID = apiID;
            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.UseDefaultCredentials = true;
            _httpClient = new HttpClient(_httpClientHandler);
        }

        public async Task<List<T>> Load()
        {
            InitHTTPClient(_httpClient);
            Task<HttpResponseMessage> responseTask = _httpClient.GetAsync($"api/{_apiID}");
            await responseTask;

            if (responseTask.Result.IsSuccessStatusCode)
            {
                Task<IEnumerable<T>> readTask = responseTask.Result.Content.ReadAsAsync<IEnumerable<T>>();
                await readTask;

                return readTask.Result.ToList();
            }

            return null;
        }

        public Task Save(List<T> objects)
        {
            throw new NotSupportedException("Save not supported for WebAPI Persistency (Use Create/Delete)");
        }

        public async Task Create(T obj)
        {
            InitHTTPClient(_httpClient);
            await _httpClient.PostAsJsonAsync($"api/{_apiID}", obj);
        }

        public async Task<T> Read(int key)
        {
            InitHTTPClient(_httpClient);
            Task<HttpResponseMessage> responseTask = _httpClient.GetAsync($"api/{_apiID}/{key}");
            await responseTask;

            if (responseTask.Result.IsSuccessStatusCode)
            {
                Task<T> readTask = responseTask.Result.Content.ReadAsAsync<T>();
                await readTask;

                return readTask.Result;
            }

            return default(T);
        }

        public async Task Update(int key, T obj)
        {
            InitHTTPClient(_httpClient);
            await _httpClient.PutAsJsonAsync($"api/{_apiID}/{key}", obj);
        }

        public async Task Delete(int key)
        {
            InitHTTPClient(_httpClient);
            await _httpClient.DeleteAsync($"api/{_apiID}/{key}");
        }

        private void InitHTTPClient(HttpClient client)
        {
            client.BaseAddress = new Uri(_serverURL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}