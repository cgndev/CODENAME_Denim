using Denim.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Denim.UI.Data.Lookups
{
    public interface IMemberLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetMemberLookupAsync();
    }
}
