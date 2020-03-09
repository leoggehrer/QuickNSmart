﻿//@QnSBaseCode
//MdStart
using CommonBase.Extensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace QuickNSmart.Adapters.Service
{
    abstract partial class ServiceAdapterObject : IDisposable
    {
        static ServiceAdapterObject()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public ServiceAdapterObject(string baseUri)
        {
            Constructing();
            BaseUri = baseUri;
            Constructed();
        }
        public ServiceAdapterObject(string baseUri, string sessionToken)
        {
            Constructing();
            BaseUri = baseUri;
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ServiceAdapterObject()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
//MdEnd