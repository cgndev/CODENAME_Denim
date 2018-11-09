using System.Data.Entity;
using System.Threading.Tasks;
using Denim.DataAccess;
using Denim.Model;

namespace Denim.UI.Data.Repositories
{
    public class ProgrammingLanguageRepository
        : GenericRepository<ProgrammingLanguage, DenimDbContext>,
        IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(DenimDbContext context)
            : base(context)
        {
        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            return await Context.Friends.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);
        }
    }
}
