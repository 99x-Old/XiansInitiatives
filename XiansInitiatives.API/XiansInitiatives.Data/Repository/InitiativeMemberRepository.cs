using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Data.Repository
{
    public class InitiativeMemberRepository : Repository<InitiativeMember>, IInitiativeMemberRepository
    {
        private readonly IdentityDataContext _db;

        public InitiativeMemberRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<InitiativeMember> GetInitiativeMember(string MemberId, string InitiativeId)
        {
            var query = _db.InitiativeMember.AsQueryable();

            var result = await query.Where(im => im.MemberId == MemberId && im.InitiativeId.ToString() == InitiativeId).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> AddInitiativeMember(InitiativeMember initiativeMember)
        {
            await _db.InitiativeMember.AddAsync(initiativeMember);
            return _db.SaveChanges() > 0;
        }

        public async Task<bool> RemoveInitiativeMember(InitiativeMember initiativeMember)
        {
            _db.InitiativeMember.Remove(initiativeMember);
            return _db.SaveChanges() > 0;
        }

        public Task<List<ApplicationUser>> GetInitiativeMembers(string initiativeId)
        {
            var result = from user in _db.ApplicationUser
                         join initiativeMember in _db.InitiativeMember
                         on user.Id equals initiativeMember.MemberId
                         where initiativeMember.InitiativeId.ToString() == initiativeId
                         select new ApplicationUser
                         {
                             Id = user.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Designation = user.Designation,
                             Email = user.Email
                         };

            return result.ToListAsync();
        }

        public Task<List<JoinedInitiativeForReturnDto>> GetJoinedInitiatives(string memberId)
        {
            var result = from user in _db.ApplicationUser
                         join initiativeMember in _db.InitiativeMember
                         on user.Id equals initiativeMember.MemberId
                         join initiative in _db.Initiative
                         on initiativeMember.InitiativeId equals initiative.Id
                         where user.Id == memberId
                         select new JoinedInitiativeForReturnDto
                         {
                             Id = initiative.Id.ToString(),
                             Name = initiative.Name,
                             Department = initiative.Department,
                             Description = initiative.Description,
                             Rating = initiativeMember.Rating,
                             Lead = initiative.Lead.FullName,
                             CoLead = initiative.CoLead.FullName,
                             Mentor = initiative.Mentor.FullName
                         };

            return result.ToListAsync();
        }

        public int GetNumberOfMembers(string initiativeId)
        {
            var count = _db.InitiativeMember.Count(im => im.InitiativeId.ToString() == initiativeId);
            return count;
        }

        public async Task<bool> RateMember(InitiativeMemberDto initiativeMemberForRate)
        {
            var query = _db.InitiativeMember.AsQueryable();

            var initiativeMember = await query.Where(im => im.MemberId == initiativeMemberForRate.MemberId && im.InitiativeId.ToString() == initiativeMemberForRate.InitiativeId)
                .FirstOrDefaultAsync();

            if (initiativeMember != null)
            {
                initiativeMember.Rating = initiativeMemberForRate.Rating;
                return _db.SaveChanges() > 0;
            }
            return false;
        }

        public Task<List<UserForProfileDto>> GetTopContributors(string initiativeId)
        {
            var result = from user in _db.ApplicationUser
                         join initiativeMember in _db.InitiativeMember
                         on user.Id equals initiativeMember.MemberId
                         where initiativeMember.InitiativeId.ToString() == initiativeId
                         orderby initiativeMember.Rating descending
                         select new UserForProfileDto
                         {
                             Id = user.Id,
                             FullName = user.FullName,
                             Rating = initiativeMember.Rating,
                             ProfileImageUrl = user.ProfileImageUrl
                         };

            return result.Take(5).ToListAsync();
        }
    }
}