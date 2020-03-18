//@QnSBaseCode
//MdStart
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using QuickNSmart.Adapters.Exceptions;
using QuickNSmart.Transfer.Persistence.Account;

namespace QuickNSmart.Adapters.Service
{
    partial class InvokeServiceAdapter : ServiceAdapterObject
    {
        static InvokeServiceAdapter()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public InvokeServiceAdapter(string baseUri)
            : base(baseUri)
        {
            Constructing();
            Constructed();
        }
        public InvokeServiceAdapter(string baseUri, string sessionToken)
            : base(baseUri, sessionToken)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        public async Task<LoginSession> LogonAsync(string jsonWebToken)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"Account/Logon/{jsonWebToken}");

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<LoginSession>(contentData, DeserializerOptions);
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
        public async Task<LoginSession> LogonAsync(string email, string password)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"Account/Logon/{email}/{password}");

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<LoginSession>(contentData, DeserializerOptions);
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
        public async Task LogoutAsync(string sessionToken)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"Account/Logout/{sessionToken}");

                if (response.IsSuccessStatusCode == false)
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }
        public async Task ChangePasswordAsync(string sessionToken, string oldPwd, string newPwd)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"Account/ChangePassword/{sessionToken}/{oldPwd}/{newPwd}");

                if (response.IsSuccessStatusCode == false)
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }
        public async Task ChangePasswordForAsync(string sessionToken, string email, string newPwd)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"Account/ChangePasswordFor/{sessionToken}/{email}/{newPwd}");

                if (response.IsSuccessStatusCode == false)
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }
        public async Task ResetForAsync(string sessionToken, string email)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"Account/ResetFor/{sessionToken}/{email}");

                if (response.IsSuccessStatusCode == false)
                {
                    string stringData = await response.Content.ReadAsStringAsync();
                    string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                    System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                    throw new AdapterException((int)response.StatusCode, errorMessage);
                }
            }
        }
        public async Task<bool> HasRoleAsync(string sessionToken, string role)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"Account/HasRole/{sessionToken}/{role}");

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<bool>(contentData, DeserializerOptions);
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

        public async Task<LoginSession> QueryLoginAsync(string sessionToken)
        {
            using (var client = GetClient(BaseUri))
            {
                HttpResponseMessage response = await client.GetAsync($"Account/QueryLogin/{sessionToken}");

                if (response.IsSuccessStatusCode)
                {
                    var contentData = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<LoginSession>(contentData, DeserializerOptions);
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
    }
}
//MdEnd