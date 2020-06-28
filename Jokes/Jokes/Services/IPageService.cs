using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jokes.Services
{
    public interface IPageService
    {
        /// <summary>
        /// Pushes a new Page to the Navigation Stack
        /// </summary>
        /// <param name="page">New Page</param>
        /// <returns>Page</returns>
        Task PushAsync(Page page);

        /// <summary>
        /// Pops the current Page from Navigation Stack
        /// </summary>
        /// <returns>Page</returns>
        Task<Page> PopAsync();

        /// <summary>
        /// Displays Confirmation Popup
        /// </summary>
        /// <param name="title">Confirmation Title</param>
        /// <param name="message">Confirmation Message</param>
        /// <param name="ok">OK-Button Text</param>
        /// <param name="cancel">Cancel-Button Text</param>
        /// <returns></returns>
        Task<bool> DisplayConfirm(string title, string message, string ok, string cancel);

        /// <summary>
        /// Displays Alert Popup
        /// </summary>
        /// <param name="title">Alert Title</param>
        /// <param name="message">Alert Message</param>
        /// <param name="ok">OK-Button Text</param>
        /// <returns></returns>
        Task DisplayAlert(string title, string message, string ok);
    }
}
