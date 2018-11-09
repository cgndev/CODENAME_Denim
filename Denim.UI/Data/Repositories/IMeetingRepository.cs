using System.Collections.Generic;
using System.Threading.Tasks;
using Denim.Model;

namespace Denim.UI.Data.Repositories
{
    public interface IMeetingRepository:IGenericRepository<Meeting>
    {
        Task<List<Friend>> GetAllFriendsAsync();
        Task ReloadFriendAsync(int friendId);
    }
}