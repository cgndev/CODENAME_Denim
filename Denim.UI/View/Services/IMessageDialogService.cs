using System.Threading.Tasks;

namespace Denim.UI.View.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string text, string title);
        void ShowInfoDialog (string text);
    }
}