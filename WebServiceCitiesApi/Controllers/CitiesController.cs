using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServiceCitiesApi.Models;

namespace WebServiceCitiesApi.Controllers
{
    [ApiController]//not really necessary but improves developer xperience
    [Route("api/cities")]
    [Authorize]
    //[Authorize(Policy = "policyName")] så här om man använder AuthorizationPolicies

    //om man ska mappa över data mellan olika objekt finns Automapper som NuGet
    //här använder han Automapper.dependencyInjection, typ
    //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.getAssemblies()) i Program före builder.Build
    //under mapp Profiles skapar du en class som ärver Profile i Automapper, i contructorn anropas CreateMap
    //automapper mappar på samma namn, Automapper mappar även listor
    //Addera AutoMapper med contructor injection
    public class CitiesController:ControllerBase //or inherit from Controller if using MVC
    {
        private ICityInfoRepository _repository;
        CitiesController(ICityInfoRepository rep) { _repository = rep ?? throw new ArgumentNullException("blala"); }

       [HttpGet()]
        public async Task<ActionResult<IEnumerable<EFCity>>> GetCities()
        {
            var tt = await _repository.GetCitiesAsync();  
            return Ok(tt);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CityDto>> GetCity(int id)
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

            //var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == id);
            var city = await _repository.GetCityAsync(id,true);
            if (city == null) return NotFound();
            return Ok(city);    
        }
    }
}
