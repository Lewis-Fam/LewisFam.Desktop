using System;
using System.Windows;

using ControlzEx.Theming;

using LewisFam.Desktop.Core.Contracts.Services;
using LewisFam.Desktop.Core.Models;

using Microsoft.Win32;

namespace LewisFam.Desktop.Core.Services
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private bool IsHighContrastActive
                        => SystemParameters.HighContrast;

        public ThemeSelectorService()
        {
            SystemEvents.UserPreferenceChanging += OnUserPreferenceChanging;
        }

        public bool SetTheme(AppTheme? theme = null)
        {
            if (IsHighContrastActive)
            {
                // TODO WTS: Set high contrast theme
                // You can add custom themes following the docs on https://mahapps.com/docs/themes/thememanager
            }
            else if (theme == null)
            {
                if (Application.Current.Properties.Contains("Theme"))
                {
                    // Read saved theme from properties
                    var themeName = Application.Current.Properties["Theme"].ToString();
                    theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
                }
                else
                {
                    // Set default theme
                    theme = AppTheme.Light;
                }
            }

            var currentTheme = ThemeManager.Current.DetectTheme(Application.Current);
            if (currentTheme == null || currentTheme.Name != theme.ToString())
            {
                ThemeManager.Current.ChangeTheme(Application.Current, $"{theme}.Blue");
                Application.Current.Properties["Theme"] = theme.ToString();
                return true;
            }

            return false;
        }

        public AppTheme GetCurrentTheme()
        {
            var themeName = Application.Current.Properties["Theme"]?.ToString();
            Enum.TryParse(themeName, out AppTheme theme);
            return theme;
        }

        private void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Color ||
                e.Category == UserPreferenceCategory.VisualStyle)
            {
                SetTheme();
            }
        }
    }
}
