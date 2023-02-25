using System.Net;

namespace DigitalVoting.Shared.Exceptions
{
    public class ResponseException : System.Exception
    {
        public ResponseException(string mensagem, HttpStatusCode statusCode) : base(mensagem)
        {
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; private set; }
    }
}