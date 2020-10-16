using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace XiansInitiatives.Data.Repository
{
    public class InitiativeMeetingRepository : Repository<InitiativeMeeting>, IInitiativeMeetingRepository
    {
        private readonly IdentityDataContext _db;

        public InitiativeMeetingRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> AddInitiativeMeeting(InitiativeMeeting initiativeMeeting)
        {
            await _db.InitiativeMeeting.AddAsync(initiativeMeeting);
            return _db.SaveChanges() > 0;
        }

        public Task<List<InitiativeMeetingDto>> GetInitiativeMeetings(string initiativeId)
        {
            var result = from initiativeMeeting in _db.InitiativeMeeting
                         join user in _db.ApplicationUser
                         on initiativeMeeting.OrganizerId equals user.Id
                         where initiativeMeeting.InitiativeId.ToString() == initiativeId
                         select new InitiativeMeetingDto
                         {
                             Id = initiativeMeeting.Id.ToString(),
                             OrganizerName = user.FullName,
                             Purpose = initiativeMeeting.Purpose,
                             CreatedAt = initiativeMeeting.CreatedAt,
                             ScheduledAt = initiativeMeeting.ScheduledAt,
                             MeetingNotes = initiativeMeeting.MeetingNotes,
                         };

            return result.ToListAsync();
        }
    }
}