using Contracts;
using Contracts.Constants;
using Contracts.CpOnBoard;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Abstractions;
using Services.HttpRequestHandler;

namespace Services
{
    public class StoreService : IStoreService
    {
        private readonly IHttpRequestHandlerService _httpRequestHandlerService;

        public StoreService(IHttpRequestHandlerService httpRequestHandlerService)
        {
            _httpRequestHandlerService = httpRequestHandlerService;
        }

        public async Task<string> GetMerchandiseAsync(CancellationToken cancellationToken)
        {
            string response = string.Empty;
            try
            {
                IConfiguration configurationBuilder = new ConfigurationBuilder().AddJsonFile(Enviroment.AppSettingJson).Build();
                string baseURL = configurationBuilder.GetSection(Enviroment.APIURL)[Enviroment.TransAPIURL];
                string strToken = await GetTransToken(configurationBuilder, baseURL);

                if (!strToken.Contains(nameof(ResponseCode.Error)))
                {
                    CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                    strToken = cpOnBoardToken.data.token;
                    if (!string.IsNullOrEmpty(strToken))
                    {
                        response = await _httpRequestHandlerService.MakeRequest(baseURL + Enviroment.GetMerchandiseURL, nameof(HttpMethod.Get), string.Empty, strToken);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<string> GetOrderAddressAsync(int orgId, CancellationToken cancellationToken)
        {
            string response = string.Empty;
            try
            {
                IConfiguration configurationBuilder = new ConfigurationBuilder().AddJsonFile(Enviroment.AppSettingJson).Build();
                string baseURL = configurationBuilder.GetSection(Enviroment.APIURL)[Enviroment.TransAPIURL];
                string strToken = await GetTransToken(configurationBuilder, baseURL);

                if (!strToken.Contains(nameof(ResponseCode.Error)))
                {
                    CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                    strToken = cpOnBoardToken.data.token;
                    if (!string.IsNullOrEmpty(strToken))
                    {
                        string url = string.Format($"{baseURL}{Enviroment.GetOrderAddressURL}?orgid={orgId}");
                        response = await _httpRequestHandlerService.MakeRequest(url, nameof(HttpMethod.Get), string.Empty, strToken);
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            return response; 
        }

        public async Task<string> PlaceOrderAsync(PlaceOrderDto request, CancellationToken cancellationToken)
        {
            string response = string.Empty;
            try
            {
                IConfiguration configurationBuilder = new ConfigurationBuilder().AddJsonFile(Enviroment.AppSettingJson).Build();
                string baseURL = configurationBuilder.GetSection(Enviroment.APIURL)[Enviroment.TransAPIURL];
                string strToken = await GetTransToken(configurationBuilder, baseURL);

                string requestStr = JsonConvert.SerializeObject(request);

                if (!strToken.Contains(nameof(ResponseCode.Error)))
                {
                    CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                    strToken = cpOnBoardToken.data.token;
                    if (!string.IsNullOrEmpty(strToken))
                    {
                        string url = string.Format($"{baseURL}{Enviroment.PlaceOrderURL}");
                        response = await _httpRequestHandlerService.MakeRequest(url, nameof(HttpMethod.Post), requestStr, strToken);
                    }
                }
            }
            catch(Exception) 
            { 
                throw;
            }
            return response;
        }

        public async Task<string> GetOrderHistoryListAsync(OrderHistoryListDto request, CancellationToken cancellationToken)
        {
            string response = string.Empty;
            try
            {
                // TODO: found a better way to get enviroment value.    
                IConfiguration configurationBuilder = new ConfigurationBuilder().AddJsonFile(Enviroment.AppSettingJson).Build();
                string baseURL = configurationBuilder.GetSection(Enviroment.APIURL)[Enviroment.TransAPIURL];
                string strToken = await GetTransToken(configurationBuilder, baseURL);

                string requestStr = JsonConvert.SerializeObject(request);

                if (!strToken.Contains(nameof(ResponseCode.Error)))
                {
                    CpOnBoardToken cpOnBoardToken = JsonConvert.DeserializeObject<CpOnBoardToken>(strToken);
                    strToken = cpOnBoardToken.data.token;
                    if (!string.IsNullOrEmpty(strToken))
                    {
                        string url = string.Format($"{baseURL}{Enviroment.GetOrderHistoryListURL}");
                        response = await _httpRequestHandlerService.MakeRequest(url, nameof(HttpMethod.Post), requestStr, strToken);
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            return response;
        }

        //TODO: check and move it to httprequesthandler service.
        private async Task<string> GetTransToken(IConfiguration configurationBuilder, string baseURL)
        {
            string strToken = string.Empty;
            try
            {
                // TODO: found a better way to get enviroment value.    
                string userid = configurationBuilder.GetSection(Enviroment.TransToken)[Enviroment.UserId];
                string pass = configurationBuilder.GetSection(Enviroment.TransToken)[Enviroment.Pass];

                strToken = await _httpRequestHandlerService
                           .MakeRequest(baseURL + Enviroment.GetTokenURL, nameof(HttpMethod.Post), "{\"userName\":\"" + userid + "\",\"password\":\"" + pass + "\"}", "");
            }
            catch(Exception)
            {
                throw;
            }
            return strToken;
        }




    }
}
