using System.Collections.Generic;
using System.Threading.Tasks;
using Denim.Model;

namespace Denim.UI.Data.Lookups
{
    public interface IProgrammingLanguageLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync();
    }
}