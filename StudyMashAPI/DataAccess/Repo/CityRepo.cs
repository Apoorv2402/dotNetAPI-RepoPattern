using Microsoft.EntityFrameworkCore;
using StudyMash.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudyMash.API.Interfaces;

namespace StudyMash.API.DataAccess.Repo
{
    public class CityRepo : ICityRepo
    {
        private CityContext _context;
        public CityRepo(CityContext context)
        {
            _context = context;
        }

         async Task<IEnumerable<City>> ICityRepo.GetCitiesAsync()
        {
           return await _context.cities.ToListAsync();
        }

        async void ICityRepo.AddCity(City city)
        {
            await _context.cities.AddAsync(city);
        }

        async void ICityRepo.DeleteCity(int id)
        {
            var city = await _context.cities.FindAsync(id);
            _context.cities.Remove(city);
        }

        
    }
}
