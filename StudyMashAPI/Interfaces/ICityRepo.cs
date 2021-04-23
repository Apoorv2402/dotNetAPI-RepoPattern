using StudyMash.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyMash.API.Interfaces
{
    public interface ICityRepo
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        void AddCity(City city);

        void DeleteCity(int id);

    }
}
