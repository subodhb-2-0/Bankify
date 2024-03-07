using Contracts.Constants;
using System.Net;
using System.Text;

namespace Services.HttpRequestHandler
{
    public class HttpRequestHandlerService : IHttpRequestHandlerService
    {
        public async Task<string> MakeRequest(string url, string method, string data, string token)
        {
            string response = string.Empty;
            try
            {
                HttpWebRequest request = PrepareRequest(url, method, data, token);

                if (!string.IsNullOrEmpty(data))
                {
                    WriteRequestData(request, data);
                }

                response =  await GetResponse(request);
            }
            catch(Exception) 
            {
                throw;
            }
            return response;
        }
        
        private HttpWebRequest PrepareRequest(string url, string method, string data, string token)
        {
            try
            {
                // TODO: Remove HardCode value.
                // REFACTOR: use http Client.
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.Headers.Add("accept", "*/*");
                request.Headers.Add("Content-Type", "application/json");
                request.UserAgent = Enviroment.UserAgent;
                request.Headers.Add(nameof(Cookie), Enviroment.CookieStr);

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Add(nameof(Authorization), "Bearer " + token);
                    request.Headers.Add(nameof(Enviroment.PartnerId), Enviroment.PartnerId);
                }
                return request;
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void WriteRequestData(HttpWebRequest request, string data)
        {
            try
            {
                byte[] dataStream = Encoding.UTF8.GetBytes(data);
                request.ContentLength = dataStream.Length;
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(dataStream, 0, dataStream.Length);
                }
            }
            catch(Exception)
            {
                throw;
            }
           
        }

        private async Task<string> GetResponse(HttpWebRequest request)
        {
            string data = string.Empty;
            try
            {
                using (WebResponse response =  request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    data = reader.ReadToEnd();
                }
            }
            catch(Exception)
            {
                throw;
            }
            return data;
        }
    }
}
