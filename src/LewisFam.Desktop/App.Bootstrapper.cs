using System.Windows;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using LewisFam.Desktop.Views;
using Prism.Ioc;
using LewisFam.Desktop.ViewModels;
using System.Configuration;
using Prism.Modularity;

namespace LewisFam.Desktop
{
    public partial class App
    {
        //protected static ILogger Log => LogManager.GetCurrentClassLogger();
        
        void SetupUnhandledExceptionHandling()
        {
            // Catch exceptions from all threads in the AppDomain.
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                ShowUnhandledException(args.ExceptionObject as Exception, "AppDomain.CurrentDomain.UnhandledException", false);

            // Catch exceptions from each AppDomain that uses a task scheduler for async operations.
            TaskScheduler.UnobservedTaskException += (sender, args) =>
                ShowUnhandledException(args.Exception, "TaskScheduler.UnobservedTaskException", false);

            // Catch exceptions from a single specific UI dispatcher thread.
            Dispatcher.UnhandledException += (sender, args) =>
            {
                // If we are debugging, let Visual Studio handle the exception and take us to the code that threw it.
                if (!Debugger.IsAttached)
                {
                    args.Handled = true;
                    ShowUnhandledException(args.Exception, "Dispatcher.UnhandledException", true);
                }
            };

            // Catch exceptions from the main UI dispatcher thread.
            // Typically we only need to catch this OR the Dispatcher.UnhandledException.
            // Handling both can result in the exception getting handled twice.
            //Application.Current.DispatcherUnhandledException += (sender, args) =>
            //{
            //	// If we are debugging, let Visual Studio handle the exception and take us to the code that threw it.
            //	if (!Debugger.IsAttached)
            //	{
            //		args.Handled = true;
            //		ShowUnhandledException(args.Exception, "Application.Current.DispatcherUnhandledException", true);
            //	}
            //};
        }

        void ShowUnhandledException(Exception e, string unhandledExceptionType, bool promptUserForShutdown)
        {
            var messageBoxTitle = $"Unexpected Error Occurred: {unhandledExceptionType}";
            var messageBoxMessage = $"The following exception occurred:\n\n{e}";
            var messageBoxButtons = MessageBoxButton.OK;

            if (promptUserForShutdown)
            {
                messageBoxMessage += "\n\nNormally the app would die now. Should we let it die?";
                messageBoxButtons = MessageBoxButton.YesNo;
            }

            // Let the user decide if the app should die or not (if applicable).
            if (MessageBox.Show(messageBoxMessage, messageBoxTitle, messageBoxButtons) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }


        /// <inheritdoc/>        
        protected override IModuleCatalog CreateModuleCatalog()
        {
            if (ConfigurationManager.AppSettings["useModulePath"] != null && bool.Parse(ConfigurationManager.AppSettings["useModulePath"]) == true)
            {

                //use directory to load modules.
                var modulePath = ConfigurationManager.AppSettings["modulePath"];
                if (string.IsNullOrEmpty(modulePath))
                    modulePath = @".\";

                //Log.Debug($"Creating catalog from directory. {modulePath}");

                return new DirectoryModuleCatalog() { ModulePath = modulePath };
            }

            if (ConfigurationManager.AppSettings["useConfig"] != null && bool.Parse(ConfigurationManager.AppSettings["useConfig"]) == true)
            {
                //Log.Debug("Creating catalog from config.");
                //use app.config to load modules.
                return new ConfigurationModuleCatalog();
            }

            //use default to load modules.
            //Log.Debug("Creating catalog.");
            return base.CreateModuleCatalog();
        }

        /// <inheritdoc/>        
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        /// <summary>
        /// On Exit.
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            //Log.Trace("Exit {0}", e.ApplicationExitCode);    
            //LogManager.Shutdown();
            base.OnExit(e);
        }   


        /// <inheritdoc/>        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {               
            containerRegistry.RegisterForNavigation<ShellWindow, ShellViewModel>();                        
        }                                        
    }
}
