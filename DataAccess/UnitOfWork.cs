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
        private DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ICityRepo CityRepo =>  new CityRepo(_context);

        public IUserRepo UserRepo => new UserRepo(_context);
       

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
