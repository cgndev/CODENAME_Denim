using Denim.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denim.UI.Data.Repositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member> GetByIpNumberAsync(int ip);
        Task<Member> GetByOfficeIdAsync(string officeId);
    }
}
