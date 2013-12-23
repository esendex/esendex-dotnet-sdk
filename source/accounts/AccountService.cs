using com.esendex.sdk.accounts;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.accounts
{
    public class AccountService : ServiceBase, IAccountService
    {
        /// <summary>
        /// Initialises a new instance of the AccountService
        /// </summary>
        /// <param name="username">Your Esendex username.</param>
        /// <param name="password">Your Esendex password.</param>
        public AccountService(string username, string password) : this(new EsendexCredentials(username, password))
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.accounts.AccountService
        /// </summary>
        /// <param name="credentials">A com.esendex.sdk.EsendexCredentials instance that contains access credentials.</param>
        public AccountService(EsendexCredentials credentials) : base(credentials){ }

        internal AccountService(IRestClient restClient, ISerialiser serialiser) : base(restClient, serialiser) { }

        public AccountCollection GetAccounts()
        {
            RestResource resource = new AccountsResource();

            return MakeRequest<AccountCollection>(HttpMethod.GET, resource);
        }
    }
}