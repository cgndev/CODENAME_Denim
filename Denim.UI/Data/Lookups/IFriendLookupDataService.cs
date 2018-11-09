using System.Collections.Generic;
using System.Threading.Tasks;
using Denim.Model;

namespace Denim.UI.Data.Lookups

{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();
    }
}