using Denim.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Denim.UI.Data.Lookups
{
    public interface IMeetingLookupDataService
    {
        Task<List<LookupItem>> GetMeetingLookupAsync();
    }
}