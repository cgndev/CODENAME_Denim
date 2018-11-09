using Denim.DataAccess;
using Denim.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denim.UI.Data.Lookups
{
    public class LookupDataService : IFriendLookupDataService,
        IProgrammingLanguageLookupDataService,
        IMeetingLookupDataService,
        IMemberLookupDataService
    {
        private Func<DenimDbContext> _contextCreator;

        public LookupDataService(Func<DenimDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Friends.AsNoTracking()
                    .Select(f =>
                   new LookupItem
                   {
                       Id = f.Id,
                       DisplayMember = f.LastName + " " + f.FirstName
                   })
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProgrammingLanguages.AsNoTracking()
                    .Select(f =>
                   new LookupItem
                   {
                       Id = f.Id,
                       DisplayMember = f.Name
                   })
                    .ToListAsync();
            }
        }

        public async Task<List<LookupItem>> GetMeetingLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var items = await ctx.Meetings.AsNoTracking()
                    .Select(m =>
                    new LookupItem
                    {
                        Id = m.Id,
                        DisplayMember = m.Title
                    })
                    .ToListAsync();
                return items;
            }
        }

        //
        public async Task<IEnumerable<LookupItem>> GetMemberLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Members.AsNoTracking()
                    .Select(m =>
                   new LookupItem
                   {
                       Id = m.Id,
                       DisplayMember = m.LastName + " " + m.FirstName
                   })
                   .OrderBy(m => m.DisplayMember)
                    .ToListAsync();
            }
        }
    }
}
