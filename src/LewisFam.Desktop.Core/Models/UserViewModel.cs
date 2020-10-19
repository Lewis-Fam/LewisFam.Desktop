using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Mvvm;
using Syncfusion.UI.Xaml.Charts;

namespace LewisFam.Desktop.Core.Models
{ 
    public class UserViewModel : BindableBase
    {
        private string _name;
        private string _userPrincipalName;
        private BitmapImage _photo;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string UserPrincipalName
        {
            get => _userPrincipalName;
            set => SetProperty(ref _userPrincipalName, value);
        }

        public BitmapImage Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }

        public UserViewModel()
        {
        }
    }
}
