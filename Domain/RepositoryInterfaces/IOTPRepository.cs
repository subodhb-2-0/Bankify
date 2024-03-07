using Domain.Entities.OTPManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IOTPRepository :  IGenericRepository<OTPDetails>
    {
        Task<OTPDetails> Get(string loginId, int moduleId, CancellationToken cancellationToken = default);
    }
}
