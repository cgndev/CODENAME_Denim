using Denim.Model;
using System.Threading.Tasks;

namespace Denim.UI.Data.Repositories
{
    public interface IProgrammingLanguageRepository
        : IGenericRepository<ProgrammingLanguage>
    {
        Task<bool> IsReferencedByFriendAsync(int programmingLanguageId);
    }
}
