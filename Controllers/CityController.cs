using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StudyMash.API.Models;
using StudyMash.API.Interfaces;
using StudyMash.API.DTOS;
using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace StudyMash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]

        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _uow.CityRepo.GetCitiesAsync();

            //using autoMapper

            var citiesDto = _mapper.Map<IEnumerable<CityDTO>>(cities);

            //Using Linq 
            //Manual Mapping

            var citiesto = from c in cities
                         select new CityDTO()
                         {
                             Id = c.Id,
                             Name = c.Name
                          };

            return Ok(citiesDto);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var city = await _uow.CityRepo.FindCity(id);

            var cityDto = _mapper.Map<CityDTO>(city);
            //Manual Mapping
            //var cityDto = new CityDTO()
            //{
            //    Id = city.Id,
            //    Name = city.Name
            //};

            return Ok(cityDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityDTO cityDTO)
        {
            var city = _mapper.Map<City>(cityDTO);
                city.LastUpdatedBy = 1;
                city.LastUpdatedOn = DateTime.Now;
                 
            //Manual Mapping
            //var city = new City()
            //{
            //    Name = cityDTO.Name,
            //    LastUpdatedBy = 1,
            //    LastUpdatedOn = DateTime.Now
            //};

            _uow.CityRepo.AddCity(city);
            await _uow.SaveAsync();

            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id , CityDTO citydto)
        {
            var oldcity = await _uow.CityRepo.FindCity(id);
            oldcity.LastUpdatedBy = 1;
            oldcity.LastUpdatedOn = DateTime.UtcNow;

            _mapper.Map(citydto,oldcity);
            await _uow.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCity(int id)
        {
            _uow.CityRepo.DeleteCity(id);
            await _uow.SaveAsync();
            return Ok();
        }

        
    }
}
