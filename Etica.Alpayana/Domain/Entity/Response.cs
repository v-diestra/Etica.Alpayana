using System.Net;

namespace Etica.Alpayana.Domain.Entity
{
    public class Response
    {
        public HttpStatusCode code { get; set; }
        public string? message { get; set; }

    }
}
