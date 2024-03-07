using Domain.RepositoryInterfaces;
using System.Data;
using Dapper;
using Domain.Entities.Onboarding;

namespace Persistence.Onboarding
{
    public class OrgCPDetailRepository : IGenericRepository<OrgCpDetails>
    {
        private readonly DapperContext _context;
        public OrgCPDetailRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<OrgCpDetails> AddAsync(OrgCpDetails entity, CancellationToken cancellationToken = default)
        {
            string query = @"INSERT INTO onboarding.tbl_org_cpdetails(
	cpdetailsid, orgid, dob, perm_house_no, perm_road, perm_dist, perm_sub_dist, perm_pincode, perm_landmark, perm_addr_proof, busi_house_no, busi_road, busi_district, busi_sub_district, busi_pincode, busi_landmark, busi_addr_proof, productid, status, creator, creationdate, modifier, modificationdate, gender, ishandicapped, occupationtype, device, bctype)
	VALUES (@cpdetailsid, @orgid, @dob, @perm_house_no, @perm_road, @perm_dist, @perm_sub_dist, @perm_pincode, @perm_landmark, @perm_addr_proof, @busi_house_no, @busi_road, @busi_district, @busi_sub_district, @busi_pincode, @busi_landmark, @busi_addr_proof, @productid, @status, @creator, @creationdate, @modifier, @modificationdate, @gender, @ishandicapped, @occupationtype, @device, @bctype)";
            var paramas = new { cpdetailsid = entity.orgCpDetailsId, orgid=entity.orgId, dob=entity.dob, perm_house_no=entity.perm_house_no, perm_road=entity.perm_road, perm_dist=entity.perm_dist, perm_sub_dist=entity.perm_sub_dist, perm_pincode=entity.perm_pincode, perm_landmark = entity.perm_landmark, perm_addr_proof = entity.perm_addr_proof, busi_house_no=entity.busi_house_no, busi_road=entity.busi_road, busi_district=entity.busi_district, busi_sub_district=entity.busi_sub_district, busi_pincode=entity.busi_pincode, busi_landmark=entity.busi_landmark, busi_addr_proof=entity.busi_addr_proof, productid=entity.productId, status=entity.status, creator=entity.CreatedBy, creationdate=entity.CreatedOn, modifier=entity.UpdatedBy, modificationdate=entity.UpdatedOn, gender=entity.gender, ishandicapped=entity.isHandiCapped, occupationtype=entity.occupationType, device=entity.device, bctype=entity.bcType };

            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                dbConnection.Open();
                var result = await dbConnection.ExecuteAsync(query, paramas);
                return entity;
            }
        }

        public Task<OrgCpDetails> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrgCpDetails>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OrgCpDetails> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OrgCpDetails> UpdateAsync(OrgCpDetails entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
