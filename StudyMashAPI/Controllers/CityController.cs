using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StudyMash.API.Models;
using StudyMash.API.Interfaces;

namespace StudyMash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public CityController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _uow.CityRepo.GetCitiesAsync();
            return Ok(cities);
        }

            [HttpPost]
          

          public async Task<IActionResult> AddCity(City city)
            {

            _uow.CityRepo.AddCity(city);
            await _uow.SaveAsync();

                    return StatusCode(201);
            }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCity(int id)
        {
            _uow.CityRepo.DeleteCity(id);
            await _uow.SaveAsync();
            return StatusCode(204);
        }

        
    }
}
