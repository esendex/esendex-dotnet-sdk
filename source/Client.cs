using com.esendex.sdk.authenticators;
using com.esendex.sdk.surveys;

namespace com.esendex.sdk
{
    public class Client
    {
        private readonly IAuthenticator _authenticator;

        public Client(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        private SurveysClient _surveysClient;
        public SurveysClient Surveys
        {
            get { return _surveysClient ?? (_surveysClient = new SurveysClient("https://api.surveys.esendex.com", _authenticator)); }
        }
    }
}