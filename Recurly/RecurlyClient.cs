using Recurly.Configuration;
using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace Recurly
{
    public partial class RecurlyClient
    {
        private readonly string BaseUrl = "https://{0}.recurly.com/v2/";
        private readonly RestClient _client;

        public RecurlyClient()
        {
            BaseUrl = string.Format(BaseUrl, Settings.Instance.Subdomain);
            _client = new RestClient(BaseUrl);
            _client.Authenticator = new HttpBasicAuthenticator(Settings.Instance.ApiKey, null);
            _client.AddHandler("application/xml", new RecurlyXmlDeserializer());
        }

        private Task<IRestResponse<T>> GetResourceAsync<T>(string url) where T : new()
        {
            var restClient = new RestClient(url);
            restClient.Authenticator = new HttpBasicAuthenticator(Settings.Instance.ApiKey, null);
            var request = new RestRequest();

            return Task<IRestResponse<T>>.Factory.StartNew(() => Execute<T>(request, restClient));
        }

        /// <summary>
        /// A wrapper around RestSharp's Execute which checks and handles errors
        /// </summary>
        private IRestResponse<T> Execute<T>(IRestRequest request, RestClient client = null) where T : new()
        {
          //  request.XmlSerializer = new DotNetXmlSerializer("www.contonso.com");
            var response = client == null ? _client.Execute<T>(request) : client.Execute<T>(request);
            CheckForError(response);

            return response;
        }

        /// <summary>
        /// A wrapper around RestSharp's Execute which checks and handles errors
        /// </summary>
        private void Execute(IRestRequest request)
        {
          //  request.XmlSerializer = new DotNetXmlSerializer("www.contonso.com");
            var response = _client.Execute(request);
            CheckForError(response);
        }

        /// <summary>
        /// A wrapper around RestSharp's Execute which checks and handles errors
        /// </summary>
        private Task<T> ExecuteAsync<T>(IRestRequest request) where T : new()
        {
            return Task.Factory.StartNew(() => Execute<T>(request).Data);
        }

        /// <summary>
        /// A wrapper around RestSharp's Execute which checks and handles errors
        /// </summary>
        private Task ExecuteAsync(IRestRequest request)
        {
            return Task.Factory.StartNew(() => Execute(request));
        }

        private void CheckForError(IRestResponse response)
        {
            Error[] errors;
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.Created:
                case HttpStatusCode.NoContent:
                    return;

                case HttpStatusCode.NotFound:
                    errors = Error.ReadResponseAndParseErrors(response);
                    if (errors.Length > 0) { throw new RecurlyNotFoundException(errors[0].Message, errors); }

                    throw new RecurlyNotFoundException("The requested object was not found.", errors);

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    errors = Error.ReadResponseAndParseErrors(response);
                    throw new RecurlyInvalidCredentialsException(errors);

                case HttpStatusCode.PreconditionFailed:
                case HttpStatusCode.BadRequest:
                    errors = Error.ReadResponseAndParseErrors(response);
                    throw new RecurlyValidationException(errors);

                case HttpStatusCode.ServiceUnavailable:
                    throw new TemporarilyUnavailableException();

                case HttpStatusCode.InternalServerError:
                    errors = Error.ReadResponseAndParseErrors(response);
                    throw new ServerException(errors);
            }

            if ((int)response.StatusCode == RecurlyValidationException.HttpStatusCode)
            {
                errors = Error.ReadResponseAndParseErrors(response);
                throw new RecurlyValidationException(errors);
            }

            if (response.ErrorException != null)
                throw response.ErrorException;
        }
    }
}
