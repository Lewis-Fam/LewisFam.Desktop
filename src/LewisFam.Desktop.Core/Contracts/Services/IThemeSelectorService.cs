using LewisFam.Desktop.Core.Models;

namespace LewisFam.Desktop.Core.Contracts.Services
{
    public interface IThemeSelectorService
    {
        bool SetTheme(AppTheme? theme = null);

        AppTheme GetCurrentTheme();
    }
}
