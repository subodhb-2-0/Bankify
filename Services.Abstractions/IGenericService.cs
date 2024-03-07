using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IGenericService<T> 
    {
        Task<ResponseModelDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResponseModelDto> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<ResponseModelDto> DeleteAsync(int id, CancellationToken cancellationToken = default);
        ResponseModelDto Validate(T entity); 
    }
}
