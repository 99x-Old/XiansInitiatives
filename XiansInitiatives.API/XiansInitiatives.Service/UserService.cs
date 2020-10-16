using AutoMapper;
using System.Threading.Tasks;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserForProfileDto> GetUserAsync(string id)
        {
            var user = await _unitOfWork.User.GetUser(id);

            if (user != null)
            {
                var userToReturn = _mapper.Map<UserForProfileDto>(user);
                return userToReturn;
            }
            return null;
        }

        public async Task<bool> SetProfileImageUrl(string userId, string url)
        {
            return await _unitOfWork.User.SetProfileImageUrl(userId, url);
        }
    }
}