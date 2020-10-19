using System.Threading.Tasks;

using LewisFam.Desktop.Core.Models;

namespace LewisFam.Desktop.Core.Contracts.Services
{
    public interface IMicrosoftGraphService
    {
        Task<User> GetUserInfoAsync(string accessToken);

        Task<string> GetUserPhoto(string accessToken);
    }
}
