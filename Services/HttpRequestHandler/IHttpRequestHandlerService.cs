

namespace Services.HttpRequestHandler
{
    public interface IHttpRequestHandlerService
    {
        Task<string> MakeRequest(string url, string method, string data, string token);
    }
}
