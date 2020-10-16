using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.ServiceContract
{
    public interface IUserService
    {
        Task<UserForProfileDto> GetUserAsync(string id);

        Task<bool> SetProfileImageUrl(string userId, string url);
    }
}