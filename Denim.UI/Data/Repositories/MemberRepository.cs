using System.Data.Entity;
using System.Threading.Tasks;
using Denim.DataAccess;
using Denim.Model;

namespace Denim.UI.Data.Repositories
{
    public class MemberRepository : GenericRepository<Member, DenimDbContext>,
                                    IMemberRepository
    {
        public MemberRepository(DenimDbContext context)
            :base(context)
        {

        }

        // 지울 OfficeId 로 가져오는 방법으로 구현적용 후 삭제 할것..
        public async Task<Member> GetByIpNumberAsync(int ipNumber)
        {
            return await Context.Members
                .SingleAsync(m => m.IpNumber == ipNumber);
        }

        public async Task<Member> GetByOfficeIdAsync(string officeId)
        {
            return await Context.Members
                .SingleAsync(m => m.OfficeId == officeId);
        }

    }
}
