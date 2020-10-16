using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XiansInitiatives.Data.Repository;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Data.DataContracts.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly IdentityDataContext _db;

        public UserRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ApplicationUser> GetUser(string Id)
        {
            var user = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == Id);
            return user;
        }

        public async Task<bool> SetProfileImageUrl(string userId, string url)
        {
            var user = await _db.ApplicationUser.FirstOrDefaultAsync(i => i.Id == userId);

            user.ProfileImageUrl = url;

            return _db.SaveChanges() > 0;
        }
    }
}