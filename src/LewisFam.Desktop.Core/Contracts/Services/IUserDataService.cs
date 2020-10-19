using LewisFam.Desktop.Core.Models;
using System;


namespace LewisFam.Desktop.Core.Contracts.Services
{
    public interface IUserDataService
    {
        event EventHandler<UserViewModel> UserDataUpdated;

        void Initialize();

        UserViewModel GetUser();
    }
}
