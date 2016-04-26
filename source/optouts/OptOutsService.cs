using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using com.esendex.sdk.core;
using com.esendex.sdk.exceptions;
using com.esendex.sdk.optouts.models;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.optouts
{
    public class OptOutsService
    {
        private const string OPTOUTS_BASE_URL = "https://api.esendex.com";

        private readonly Uri _baseUrl;
        private readonly EsendexCredentials _credentials;

        internal OptOutsService(string url, EsendexCredentials esendexCredentials)
        {
            _baseUrl = new Uri(url);
            _credentials = esendexCredentials;
        }

        public OptOutsService(EsendexCredentials credentials) : this(OPTOUTS_BASE_URL, credentials)
        {
        }

        public OptOutsService(string username, string password) : this(OPTOUTS_BASE_URL, new EsendexCredentials(username, password))
        {
        }

        public OptOut GetById(Guid optOutId)
        {
            var requestUrl = new Uri(_baseUrl, string.Format("v1.0/optouts/{0}", optOutId));
            var request = Request.Create("GET", requestUrl)
                                 .WithHeader("Authorization", "Basic " + _credentials.EncodedValue())
                                 .WithAcceptHeader(Constants.XML_MEDIA_TYPE)
                                 .If(_credentials.UseProxy, r => r.WithProxy(_credentials.WebProxy));

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string responseText = reader.ReadToEnd();
                    return new XmlSerialiser().Deserialise<OptOut>(responseText);
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response.StatusCode != HttpStatusCode.BadRequest)
                    throw;

                throw new BadRequestException(ex, null);
            }
        }

        public SubscriptionCollection GetByPhoneNumber(string phoneNumber, string accountReference, int pageNumber, int pageSize)
        {
            var requestUrl = string.Concat(_baseUrl, "v1.0/optouts");
            var uriBuilder = new UriBuilder(requestUrl);
            var query = HttpUtility.ParseQueryString(string.Empty);
            
            if (accountReference != null)
                query["accountreference"] = accountReference;
            if (phoneNumber != null)
                query["from"] = phoneNumber;
            query["startIndex"] = GetStartIndex(pageNumber, pageSize).ToString();
            query["count"] = pageSize.ToString();

            uriBuilder.Query = query.ToString();

            var request = Request.Create("GET", uriBuilder.Uri)
                                 .WithHeader("Authorization", "Basic " + _credentials.EncodedValue())
                                 .WithAcceptHeader(Constants.XML_MEDIA_TYPE)
                                 .If(_credentials.UseProxy, r => r.WithProxy(_credentials.WebProxy));

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string responseText = reader.ReadToEnd();
                    return new XmlSerialiser().Deserialise<OptOutCollection>(responseText);
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response.StatusCode != HttpStatusCode.BadRequest)
                    throw;

                throw new BadRequestException(ex, null);
            }
        }

        public SubscriptionCollection GetByFromAddress(string from, int pageNumber, int pageSize)
        {
            return GetByPhoneNumber(from, null, pageNumber, pageSize);
        }

        public SubscriptionCollection GetByAccountReference(string accountReference, int pageNumber, int pageSize)
        {
            return GetByPhoneNumber(null, accountReference, pageNumber, pageSize);
        }

        public SubscriptionCollection GetAll(int pageNumber, int pageSize)
        {
            return GetByPhoneNumber(null, null, pageNumber, pageSize);
        }

        private int GetStartIndex(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than zero.", "pageNumber");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than zero.", "pageSize");

            var startIndex = ((--pageNumber)*pageSize);
            return startIndex;
        }

        public OptOut Add(string accountReference, string phoneNumber)
        {
            var requestData = new OptOutCreateRequest
            {
                AccountReference = accountReference,
                From = new FromAddress
                {
                    PhoneNumber = phoneNumber
                }
            };

            var request = Request.Create("POST", new Uri(string.Concat(_baseUrl, "v1.0/optouts")))
                                 .WithHeader("Authorization", "Basic " + _credentials.EncodedValue())
                                 .WithAcceptHeader(Constants.XML_MEDIA_TYPE)
                                 .If(_credentials.UseProxy, r => r.WithProxy(_credentials.WebProxy))
                                 .WriteBody(Constants.XML_MEDIA_TYPE, streamWriter => streamWriter.Write(new XmlSerialiser().Serialise(requestData)));

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string responseText = reader.ReadToEnd();
                    return new XmlSerialiser().Deserialise<OptOutCreateRootResponse>(responseText).OptOut;
                    
                }
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}