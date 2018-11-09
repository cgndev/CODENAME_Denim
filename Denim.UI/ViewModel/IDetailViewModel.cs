using System.Threading.Tasks;

namespace Denim.UI.ViewModel
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int id);
        bool HasChanges { get; }
        int Id { get; }
    }
}