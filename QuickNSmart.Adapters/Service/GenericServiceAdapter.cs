//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommonBase.Extensions;
using QuickNSmart.Adapters.Exceptions;

namespace QuickNSmart.Adapters.Service
{
    partial class GenericServiceAdapter<TContract, TEntity> : Contracts.Client.IAdapterAccess<TContract>
        where TContract : Contracts.IIdentifiable
        where TEntity : TContract, Contracts.ICopyable<TContract>, new()
    {
        static GenericServiceAdapter()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        private static string Separator => ";";

        public string BaseUri
        {
            get;
        }
        public virtual string ExtUri
        {
            get;
        }

        public GenericServiceAdapter(string baseUri, string extUri)
        {
            Constructing();
            BaseUri = baseUri;
            ExtUri = extUri;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        private JsonSerializerOptions DeserializerOptions => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public int MaxPageSize
        {
            get
            {
                return Task.Run(async () =>
                {
                    using var client = GetClient(BaseUri);
                    HttpResponseMessage response = await client.GetAsync(ExtUri + "/MaxPage");

                    if (response.IsSuccessStatusCode)
                    {
                        string stringData = await response.Content.ReadAsStringAsync();

                        return Convert.ToInt32(stringData);
                    }
                    else
                    {
                        string stringData = await response.Content.ReadAsStringAsync();
                        string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                        System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                        throw new AdapterException((int)response.StatusCode, errorMessage);
                    }
                }).Result;
            }
        }

        public async Task<int> CountAsync()
        {
            using var client = GetClient(BaseUri);
            HttpResponseMessage response = await client.GetAsync(ExtUri + "/Count");

            if (response.IsSuccessStatusCode)
            {
                string stringData = await response.Content.ReadAsStringAsync();

                return Convert.ToInt32(stringData);
            }
            else
            {
                string stringData = await response.Content.ReadAsStringAsync();
                string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                throw new AdapterException((int)response.StatusCode, errorMessage);
            }
        }

        public async Task<IEnumerable<TContract>> GetAllAsync()
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync(ExtUri + "/Get");

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<TEntity[]>(contentData, DeserializerOptions) as IEnumerable<TContract>;
                }
                else
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }
        public async Task<IEnumerable<TContract>> GetPageListAsync(int pageIndex, int pageSize)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"{ExtUri}/Get/{pageIndex}/{pageSize}");

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<TEntity[]>(contentData, DeserializerOptions) as IEnumerable<TContract>;
                }
                else
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }
        public async Task<IEnumerable<TContract>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"{ExtUri}/Get/{predicate}/{pageIndex}/{pageSize}");

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<TEntity[]>(contentData, DeserializerOptions) as IEnumerable<TContract>;
                }
                else
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }

        public async Task<TContract> GetByIdAsync(int id)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"{ExtUri}/Get/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<TEntity>(contentData, DeserializerOptions);
                }
                else
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }

        public async Task<TContract> CreateAsync()
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"{ExtUri}/Create").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<TEntity>(contentData, DeserializerOptions);
                }
                else
                {
                    string errorMessage = $"{response.ReasonPhrase}: {await response.Content.ReadAsStringAsync().ConfigureAwait(false)}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }

        public async Task<TContract> InsertAsync(TContract entity)
        {
            entity.CheckArgument(nameof(entity));

            using (var client = GetClient(BaseUri))
            {
                string jsonData = JsonSerializer.Serialize<TContract>(entity);
                StringContent contentData = new StringContent(jsonData, Encoding.UTF8, MediaType);
                HttpResponseMessage response = await client.PostAsync(ExtUri, contentData);

                if (response.IsSuccessStatusCode)
                {
                    var resultData = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    return await JsonSerializer.DeserializeAsync<TEntity>(resultData, DeserializerOptions).ConfigureAwait(false);
                }
                else
                {
                    string errorMessage = $"{response.ReasonPhrase}: {await response.Content.ReadAsStringAsync()}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }

        public async Task<TContract> UpdateAsync(TContract entity)
        {
            entity.CheckArgument(nameof(entity));

            using (var client = GetClient(BaseUri))
            {
                string jsonData = JsonSerializer.Serialize<TContract>(entity);
                StringContent contentData = new StringContent(jsonData, Encoding.UTF8, MediaType);
                HttpResponseMessage response = await client.PutAsync(ExtUri, contentData).ConfigureAwait(false);

                if (response.IsSuccessStatusCode == false)
                {
                    string errorMessage = $"{response.ReasonPhrase}: {await response.Content.ReadAsStringAsync().ConfigureAwait(false)}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
            return await GetByIdAsync(entity.Id);
        }

        public async Task DeleteAsync(int id)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.DeleteAsync($"{ExtUri}/{id}");
                if (response.IsSuccessStatusCode == false)
                {
                    string errorMessage = $"{response.ReasonPhrase}: {await response.Content.ReadAsStringAsync()}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }
        public void Dispose()
        {
        }

        #region Helpers
        protected static string MediaType => "application/json";
        protected HttpClient CreateClient(string baseAddress)
        {
            HttpClient client = new HttpClient();

            if (baseAddress.EndsWith(@"/") == false
                || baseAddress.EndsWith(@"\") == false)
            {
                baseAddress = baseAddress + "/";
            }

            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaType));

            return client;
        }
        protected HttpClient GetClient(string baseAddress)
        {
            return CreateClient(baseAddress);
        }
        #endregion Helpers
    }
}
//MdEnd