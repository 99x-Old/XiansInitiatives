using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Data.Repository
{
    public class InitiativeRepository : Repository<Initiative>, IInitiativeRepository
    {
        private readonly IdentityDataContext _db;

        public InitiativeRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> CreateInitiative(Initiative initiative)
        {
            await _db.Initiative.AddAsync(initiative);
            return _db.SaveChanges() > 0;
        }

        public Task<List<Initiative>> GetInitiatives(string yearId)
        {
            var result = from initiative in _db.Initiative
                         join initiativeYear in _db.InitiativeYear
                         on initiative.InitiativeYearId equals initiativeYear.Id
                         where initiativeYear.Id.ToString() == yearId
                         select new Initiative
                         {
                             Id = initiative.Id,
                             Name = initiative.Name,
                             Department = initiative.Department,
                             Description = initiative.Description,
                             CreatedAt = initiative.CreatedAt,
                             Lead = initiative.Lead,
                             CoLead = initiative.CoLead,
                             Mentor = initiative.Mentor,
                             InitiativeYear = initiativeYear
                         };

            return result.ToListAsync();
        }

        public Task<List<Initiative>> GetInitiativesByYear(int year)
        {
            var result = from initiative in _db.Initiative
                         join initiativeYear in _db.InitiativeYear
                         on initiative.InitiativeYearId equals initiativeYear.Id
                         where initiativeYear.Year == year
                         select new Initiative
                         {
                             Id = initiative.Id,
                             Name = initiative.Name,
                             Department = initiative.Department,
                             Description = initiative.Description,
                             CreatedAt = initiative.CreatedAt,
                             Lead = initiative.Lead,
                             CoLead = initiative.CoLead,
                             Mentor = initiative.Mentor,
                             InitiativeYear = initiativeYear
                         };

            return result.ToListAsync();
        }

        public async Task<InitiativeYear> GetInitiativeYearByYear(int Year)
        {
            return await _db.InitiativeYear.FirstOrDefaultAsync(i => i.Year == Year);
        }

        public async Task<Initiative> GetInitiative(string id)
        {
            return await _db.Initiative.FirstOrDefaultAsync(i => i.Id.ToString() == id);
        }

        public async Task<bool> DeleteInitiative(Initiative initiative)
        {
            _db.Initiative.Remove(initiative);
            return _db.SaveChanges() > 0;
        }

        public async Task<bool> UpdateInitiative(InitiativeForUpdateDto initiativeForUpdateDto)
        {
            var existingInitiative = await _db.Initiative.FirstOrDefaultAsync(i => i.Id.ToString() == initiativeForUpdateDto.Id);

            if (existingInitiative != null)
            {
                existingInitiative.Name = initiativeForUpdateDto.Name;
                existingInitiative.Description = initiativeForUpdateDto.Description;
                existingInitiative.Department = initiativeForUpdateDto.Department;
                existingInitiative.LeadId = initiativeForUpdateDto.LeadId;
                existingInitiative.CoLeadId = initiativeForUpdateDto.CoLeadId;
                existingInitiative.MentorId = initiativeForUpdateDto.MentorId;

                return _db.SaveChanges() > 0;
            }

            return false;
        }

        public Task<Initiative> GetLeadInitiative(string memberId)
        {
            return _db.Initiative.FirstOrDefaultAsync(i => i.LeadId == memberId || i.CoLeadId == memberId);
        }
    }
}