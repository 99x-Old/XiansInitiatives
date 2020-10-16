using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Data.Repository
{
    public class InitiativeYearRepository : Repository<InitiativeYear>, IInitiativeYearRepository
    {
        private readonly IdentityDataContext _db;

        public InitiativeYearRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> CreateInitiativeYear(InitiativeYear initiativeYear)
        {
            await _db.InitiativeYear.AddAsync(initiativeYear);
            return _db.SaveChanges() > 0;
        }       

        public async Task<InitiativeYear> GetInitiativeYearByYear(int year)
        {
            return await _db.InitiativeYear.FirstOrDefaultAsync(i => i.Year == year);
        }

        public async Task<IEnumerable<InitiativeYear>> GetInitiativeYears()
        {
            return await _db.InitiativeYear.ToListAsync();
        }
    }
}