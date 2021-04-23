using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyMash.API.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepo CityRepo { get; }

        Task<bool> SaveAsync();
    }
}
