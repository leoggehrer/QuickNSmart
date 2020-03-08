//@QnSBaseCode
//MdStart
using System.Threading.Tasks;
using QuickNSmart.Contracts.Persistence.Account;

namespace QuickNSmart.Adapters.Modules.Account
{
    public static partial class AccountManager
    {
        static AccountManager()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        /// <summary>
        /// The base url like https://localhost:5001/api
        /// </summary>
        public static string BaseUri = "";
        public static AdapterType Adapter { get; set; } = AdapterType.Controller;

        public static async Task<ILoginSession> LogonAsync(string email, string password)
        {
            var result = default(ILoginSession);

            if (Adapter == AdapterType.Controller)
            {
                result = await Logic.Modules.Account.AccountManager.LogonAsync(email, password).ConfigureAwait(false);
            }
            else if (Adapter == AdapterType.Service)
            {

            }
            return result;
        }
    }
}
//MdEnd