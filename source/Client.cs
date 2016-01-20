using com.esendex.sdk.authenticators;
using com.esendex.sdk.surveys;

namespace com.esendex.sdk
{
    public class Client
    {
        private const string SURVEYS_BASE_URL = "https://api.surveys.esendex.com";
        
        private readonly IAuthenticator _authenticator;

        public Client(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        private SurveysClient _surveysClient;
        public SurveysClient Surveys
        {
            get { return _surveysClient ?? (_surveysClient = new SurveysClient(SURVEYS_BASE_URL, _authenticator)); }
        }
    }
}