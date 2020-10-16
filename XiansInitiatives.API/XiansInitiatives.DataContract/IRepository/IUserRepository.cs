using System.Threading.Tasks;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContracts.IRepository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUser(string Id);

        Task<bool> SetProfileImageUrl(string userId, string url);
    }
}