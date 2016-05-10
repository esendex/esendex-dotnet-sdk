using System;
using System.Net;
using com.esendex.sdk.extensions;
using com.esendex.sdk.http;
using com.esendex.sdk.optouts.models.request;
using com.esendex.sdk.optouts.models.response;
using Newtonsoft.Json;

namespace com.esendex.sdk.optouts
{
    /// <summary>
    /// A service to create and retrieve opt outs.
    /// </summary>
    public class OptOutsService
    {
        private const string BASE_URL = "https://api.esendex.com";

        private readonly Uri _baseUrl;
        private readonly EsendexCredentials _credentials;

        internal OptOutsService(string url, EsendexCredentials esendexCredentials)
        {
            _baseUrl = new Uri(url);
            _credentials = esendexCredentials;
        }

        /// <summary>
        /// Initialises a new instance of the InboxService
        /// </summary>
        /// <param name="credentials">A com.esendex.sdk.EsendexCredentials instance containing your username and password.</param>
        public OptOutsService(EsendexCredentials credentials) : this(BASE_URL, credentials)
        {
        }

        /// <summary>
        /// Initialises a new instance of the InboxService
        /// </summary>
        /// <param name="username">A string containing your username.</param>
        /// <param name="password">A string containing your password.</param>
        public OptOutsService(string username, string password) : this(BASE_URL, new EsendexCredentials(username, password))
        {
        }

        /// <summary>
        /// Gets a com.esendex.sdk.optouts.OptOut instance containing an opt out.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an opt out.</param>
        /// <returns>A com.esendex.sdk.optouts.OptOut instance containing an opt out.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public OptOut GetById(Guid optOutId)
        {
            var requestUrl = new Uri(_baseUrl, string.Format("v1.0/optouts/{0}", optOutId));
            var request = Request.Create("GET", requestUrl)
                                 .WithHeader("Authorization", "Basic " + _credentials.EncodedValue())
                                 .WithAcceptHeader(Constants.JSON_MEDIA_TYPE)
                                 .If(_credentials.UseProxy, r => r.WithProxy(_credentials.WebProxy));

            var response = request.GetResponse();
            return response.DeserialiseJson<OptOut>();
        }

        /// <summary>
        /// Gets a com.esendex.sdk.optouts.OptOutCollection instance containing a collection of com.esendex.sdk.optouts.OptOut.
        /// </summary>
        /// <param name="phoneNumber">A string that contains the phone number to search with.</param>
        /// <param name="accountReference">A string that contains the account reference to search with.</param>
        /// <param name="pageNumber">An int that that specifies which page of results to return.</param>
        /// <param name="pageSize">An int that specifies how many results to return in a page.</param>
        /// <returns>A com.esendex.sdk.optouts.OptOutCollection instance containing a collection of com.esendex.sdk.optouts.OptOut.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public OptOutCollection GetByPhoneNumber(string phoneNumber, string accountReference, int pageNumber, int pageSize)
        {
            var uri = BuildGetAllUri(phoneNumber, accountReference, pageNumber, pageSize);

            var request = Request.Create("GET", uri)
                                 .WithHeader("Authorization", "Basic " + _credentials.EncodedValue())
                                 .WithAcceptHeader(Constants.JSON_MEDIA_TYPE)
                                 .If(_credentials.UseProxy, r => r.WithProxy(_credentials.WebProxy));

            var response = request.GetResponse();
            return response.DeserialiseJson<OptOutCollection>();
        }

        private Uri BuildGetAllUri(string phoneNumber, string accountReference, int pageNumber, int pageSize)
        {
            var requestUrl = _baseUrl + "v1.0/optouts";

            var builder = HttpUriBuilder.Create(requestUrl)
                                        .WithParameter("startIndex", GetStartIndex(pageNumber, pageSize).ToString())
                                        .WithParameter("count", pageSize.ToString());

            if (accountReference != null)
            {
                builder.WithParameter("accountreference", accountReference);
            }

            if (phoneNumber != null)
            {
                builder.WithParameter("from", phoneNumber);
            }

            return builder.Build();
        }

        /// <summary>
        /// Gets a com.esendex.sdk.optouts.OptOutCollection instance containing a collection of com.esendex.sdk.optouts.OptOut.
        /// </summary>
        /// <param name="phoneNumber">A string that contains the phone number to search with.</param>
        /// <param name="pageNumber">An int that that specifies which page of results to return.</param>
        /// <param name="pageSize">An int that specifies how many results to return in a page.</param>
        /// <returns>A com.esendex.sdk.optouts.OptOutCollection instance containing a collection of com.esendex.sdk.optouts.OptOut.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public OptOutCollection GetByFromAddress(string from, int pageNumber, int pageSize)
        {
            return GetByPhoneNumber(from, null, pageNumber, pageSize);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.optouts.OptOutCollection instance containing a collection of com.esendex.sdk.optouts.OptOut.
        /// </summary>
        /// <param name="accountReference">A string that contains the account reference to search with.</param>
        /// <param name="pageNumber">An int that that specifies which page of results to return.</param>
        /// <param name="pageSize">An int that specifies how many results to return in a page.</param>
        /// <returns>A com.esendex.sdk.optouts.OptOutCollection instance containing a collection of com.esendex.sdk.optouts.OptOut.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public OptOutCollection GetByAccountReference(string accountReference, int pageNumber, int pageSize)
        {
            return GetByPhoneNumber(null, accountReference, pageNumber, pageSize);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.optouts.OptOutCollection instance containing a collection of com.esendex.sdk.optouts.OptOut.
        /// </summary>
        /// <param name="pageNumber">An int that that specifies which page of results to return.</param>
        /// <param name="pageSize">An int that specifies how many results to return in a page.</param>
        /// <returns>A com.esendex.sdk.optouts.OptOutCollection instance containing a collection of com.esendex.sdk.optouts.OptOut.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public OptOutCollection GetAll(int pageNumber, int pageSize)
        {
            return GetByPhoneNumber(null, null, pageNumber, pageSize);
        }

        private int GetStartIndex(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than zero.", "pageNumber");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than zero.", "pageSize");

            var startIndex = (pageNumber - 1) * pageSize;
            return startIndex;
        }

        /// <summary>
        /// Gets a com.esendex.sdk.optouts.OptOutCreateResult instance containing an opt out.
        /// </summary>
        /// <param name="phoneNumber">A string that contains the phone number to be opted out.</param>
        /// <param name="accountReference">A string that contains the account reference to which the opt out will be applied.</param>
        /// <returns>A com.esendex.sdk.optouts.OptOutCreateResult instance containing the created opt out.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public OptOutCreateResult Add(string accountReference, string phoneNumber)
        {
            var requestData = new OptOutCreateRequest
            {
                AccountReference = accountReference,
                From = new FromAddress
                {
                    PhoneNumber = phoneNumber
                }
            };

            var requestUrl = new Uri(string.Concat(_baseUrl, "v1.0/optouts"));
            var request = Request.Create("POST", requestUrl)
                                 .WithHeader("Authorization", "Basic " + _credentials.EncodedValue())
                                 .WithAcceptHeader(Constants.JSON_MEDIA_TYPE)
                                 .If(_credentials.UseProxy, r => r.WithProxy(_credentials.WebProxy))
                                 .WriteBody(Constants.JSON_MEDIA_TYPE, streamWriter => JsonSerializer.Create().Serialize(streamWriter, requestData));

            HttpWebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                if (response.StatusCode != HttpStatusCode.BadRequest)
                    throw;
            }

            return response.DeserialiseJson<OptOutCreateResult>();
        }
    }
}