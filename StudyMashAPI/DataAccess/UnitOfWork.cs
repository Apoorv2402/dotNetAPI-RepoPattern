using StudyMash.API.Interfaces;
using StudyMash.API.DataAccess.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyMash.API.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private CityContext _context;
        public UnitOfWork(CityContext context)
        {
            _context = context;
        }

        public ICityRepo CityRepo =>  new CityRepo(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
