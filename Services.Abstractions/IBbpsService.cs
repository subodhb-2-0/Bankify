using Contracts.Bbps;
using Contracts.Common;
using Contracts.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBbpsService
    {
        Task<ResponseModelDto> insertbillerAsync(BbpsBillerListDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetbillerAsync(BbpsBillerSearchOptionsDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAgentAsync(GetAgentRequestDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> InsertAgentAsync(InsertBbpsAgentDto entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetBillerInputParams(int bbpsbillerid, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetBillerInfoAsync(string billercode, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateBBPSBillerStatus(UpdateBBPSBillerModelDto entity, CancellationToken cancellationToken = default);
        
        Task<ResponseModelDto> GetBillerCategoryList(CancellationToken cancellationToken = default);

        Task<ResponseModelDto> GetBbpsServiceResult(int serviceId, CancellationToken cancellationToken = default);

        
    }
}
