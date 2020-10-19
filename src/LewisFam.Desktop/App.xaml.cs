using Prism.Ioc;
using LewisFam.Desktop.Views;
using System.Windows;
using Prism.Modularity;
using System.Configuration;
using LewisFam.Desktop.ViewModels;

namespace LewisFam.Desktop
{         
    public partial class App  
    {
        protected App()
        {
            SetupUnhandledExceptionHandling();
        }            
    }
}
