using System.Threading.Tasks;
using System.Windows;

using LewisFam.Desktop.Core.Helpers;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace LewisFam.Desktop.Core.Helpers
{
    public static class AuthenticationHelper
    {
        public static async Task ShowLoginErrorAsync(LoginResultType loginResult)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            switch (loginResult)
            {
                //case LoginResultType.NoNetworkAvailable:
                //    await metroWindow.ShowMessageAsync(Resources.DialogNoNetworkAvailableContent, Resources.DialogAuthenticationTitle);
                //    break;
                //case LoginResultType.UnknownError:
                //    await metroWindow.ShowMessageAsync(Resources.DialogAuthenticationTitle, Resources.DialogStatusUnknownErrorContent);
                //    break;
            }
        }
    }
}
