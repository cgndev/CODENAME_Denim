﻿using Denim.DataAccess;
using Denim.Model;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Denim.UI.Data.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, DenimDbContext>,
        IMeetingRepository
    {
        public MeetingRepository(DenimDbContext context) : base(context)
        {
        }

        public async override Task<Meeting> GetByIdAsync(int id)
        {
            return await Context.Meetings
               .Include(m => m.Friends)
               .SingleAsync(m => m.Id == id);
        }

        public async Task<List<Friend>> GetAllFriendsAsync()
        {
            return await Context.Set<Friend>().ToListAsync();
        }

        public async Task ReloadFriendAsync(int friendId)
        {
            var dbEntityEntry = Context.ChangeTracker.Entries<Friend>()
                .SingleOrDefault(db => db.Entity.Id == friendId);
            if (dbEntityEntry != null )
            {
                await dbEntityEntry.ReloadAsync();
            }
        }
    }
}
