using System.Collections.Generic;
using System.Threading.Tasks;
using Denim.Model;

namespace Denim.UI.Data.Repositories
{
    public interface IFriendRepository : IGenericRepository<Friend>
    {
        void RemovePhoneNumber(FriendPhoneNumber model);
        Task<bool> HasMeetingAsync(int friendId);
    }
}