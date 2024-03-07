using Contracts.Common;
using Contracts.DummyData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.Utility;
using Services.Abstractions;

namespace PublicAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class DummyController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IPAddressHelper _ipAddressHelper;
        public DummyController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _ipAddressHelper = new IPAddressHelper();
            
        }
        /// <summary>
        /// Get List of Dummy Data
        /// </summary>

        /// <returns></returns>
        [HttpGet("GetDummyData")]
        public async Task<ActionResult> GetDummyData(int pageNo, int pageSize, bool? errorRequired)
        {
            ResponseModelDto responseModelDto = null;

            if (errorRequired.HasValue && errorRequired.Value == true)
            {
                responseModelDto = new ResponseModelDto()
                {
                    Response = "Error",

                    ResponseCode = "-1",
                    Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                Error = "Contact to admin",
                ErrorCode = "1001"
                } }
                };
            }
            else if (errorRequired.HasValue && errorRequired.Value == true)
            {
                responseModelDto = new ResponseModelDto()
                {
                    Response = "This Name already exits.",
                    ResponseCode = "1",
                    Errors = new List<ErrorModelDto>() { new ErrorModelDto() {
                Error = "Comission Sharing Model already exist",
                ErrorCode = "5008"
                } }
                };
            }
            else
            {
                responseModelDto = new ResponseModelDto()
                {
                    Response = "Success",

                    ResponseCode = "0",
                    Data = new List<DummyModel>() {
                        new DummyModel()
                        {
                            key1 = 1,
                            key2 = "Testing Dummy Data",
                            key3 = "2023-04-25 17:53:08.837",
                            key4 = DateTime.Now,
                            key5 = "Testing Dummy Data",
                            key6 = "Testing Dummy Data",
                            key7 = "Testing Dummy Data",
                            key8 = "Testing Dummy Data",
                            key9 = 100.05
                        },
                        new DummyModel()
                        {
                            key1 = 2,
                            key2 = "Testing Dummy Data Two",
                            key3 = "2023-05-25 17:53:08.837",
                            key4 = DateTime.Now,
                            key5 = "Testing Dummy Data",
                            key6 = "Testing Dummy Data",
                            key7 = "Testing Dummy Data",
                            key8 = "Testing Dummy Data",
                            key9 = 100.05
                        },
                        new DummyModel()
                        {
                            key1 = 3,
                            key2 = "Testing Dummy Data 3",
                            key3 = "2023-04-25 17:53:08.837",
                            key4 = DateTime.Now,
                            key5 = "Testing Dummy Data",
                            key6 = "Testing Dummy Data",
                            key7 = "Testing Dummy Data",
                            key8 = "Testing Dummy Data",
                            key9 = 100.05
                        },
                        new DummyModel()
                        {
                            key1 = 4,
                            key2 = "Testing Dummy Data 4",
                            key3 = "2023-05-25 17:53:08.837",
                            key4 = DateTime.Now,
                            key5 = "Testing Dummy Data",
                            key6 = "Testing Dummy Data",
                            key7 = "Testing Dummy Data",
                            key8 = "Testing Dummy Data",
                            key9 = 100.05
                        },
                        new DummyModel()
                        {
                            key1 = 5,
                            key2 = "Testing Dummy Data 5",
                            key3 = "2023-04-25 17:53:08.837",
                            key4 = DateTime.Now,
                            key5 = "Testing Dummy Data",
                            key6 = "Testing Dummy Data",
                            key7 = "Testing Dummy Data",
                            key8 = "Testing Dummy Data",
                            key9 = 100.05
                        },
                        new DummyModel()
                        {
                            key1 = 6,
                            key2 = "Testing Dummy Data 6",
                            key3 = "2023-05-25 17:53:08.837",
                            key4 = DateTime.Now,
                            key5 = "Testing Dummy Data",
                            key6 = "Testing Dummy Data",
                            key7 = "Testing Dummy Data",
                            key8 = "Testing Dummy Data",
                            key9 = 100.05
                        },
                        new DummyModel()
                        {
                            key1 = 7,
                            key2 = "Testing Dummy Data 7",
                            key3 = "2023-04-25 17:53:08.837",
                            key4 = DateTime.Now,
                            key5 = "Testing Dummy Data",
                            key6 = "Testing Dummy Data",
                            key7 = "Testing Dummy Data",
                            key8 = "Testing Dummy Data",
                            key9 = 100.05
                        },
                        new DummyModel()
                        {
                            key1 = 8,
                            key2 = "Testing Dummy Data 8",
                            key3 = "2023-05-25 17:53:08.837",
                            key4 = DateTime.Now,
                            key5 = "Testing Dummy Data",
                            key6 = "Testing Dummy Data",
                            key7 = "Testing Dummy Data",
                            key8 = "Testing Dummy Data",
                            key9 = 100.05
                        }
                   }
                };
            }


            return Ok(responseModelDto);
        }
    }
}
