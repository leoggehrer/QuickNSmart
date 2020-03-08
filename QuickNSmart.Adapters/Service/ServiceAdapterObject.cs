//@QnSBaseCode
//MdStart
using CommonBase.Extensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace QuickNSmart.Adapters.Service
{
    abstract partial class ServiceAdapterObject
    {
        static ServiceAdapterObject()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public ServiceAdapterObject(string baseUri, string extUri)
        {
            Constructing();
            BaseUri = baseUri;
            ExtUri = extUri;
            Constructed();
        }
        public ServiceAdapterObject(string baseUri, string extUri, string sessionToken)
        {
            Constructing();
            BaseUri = baseUri;
            ExtUri = extUri;
            SessionToken = sessionToken;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        protected JsonSerializerOptions DeserializerOptions => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        /// <summary>
        /// The base url like https://localhost:5001/api
        /// </summary>
        public string BaseUri
        {
            get;
        }
        public string ExtUri
        {
            get;
        }
        public string SessionToken { private get; set; }

        #region Helpers
        protected static string MediaType => "application/json";
        protected static HttpClient CreateClient(string baseAddress, string sessionToken)
        {
            HttpClient client = new HttpClient();

            if (baseAddress.HasContent())
            {
                if (baseAddress.EndsWith(@"/") == false
                    || baseAddress.EndsWith(@"\") == false)
                {
                    baseAddress = baseAddress + "/";
                }

                client.BaseAddress = new Uri(baseAddress);
            }
            client.DefaultRequestHeaders.Accept.Clear();

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaType));

            if (sessionToken.HasContent())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"[{sessionToken}]")));
            }
            return client;
        }
        protected HttpClient GetClient(string baseAddress)
        {
            return CreateClient(baseAddress, SessionToken);
        }
        protected HttpClient GetClient(string baseAddress, string sessionToken)
        {
            return CreateClient(baseAddress, sessionToken);
        }
        #endregion Helpers
    }
}
//MdEnd