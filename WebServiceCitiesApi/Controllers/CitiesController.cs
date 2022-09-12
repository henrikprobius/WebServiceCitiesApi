using Microsoft.AspNetCore.Mvc;
using WebServiceCitiesApi.Models;

namespace WebServiceCitiesApi.Controllers
{
    [ApiController]//not really necessary but improves developer xperience
    [Route("api/cities")]
    public class CitiesController:ControllerBase //or inherit from Controller if using MVC
    {
        
        [HttpGet()]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CityDatastore.Cities);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            //do not send back 200 OK or 500 (Server Errors) if an id is missing
            //Level 100 Information
            //Level 300 Redirection
            //Level 400 Client Mistakes
            // 201 Created
            // 204 No content
            // 400 Bad Request
            // 401 Unauthorized
            // 403 Forbidden
            // 404 Not Found

            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null) return NotFound();
            return Ok(city);    
        }
    }
}
