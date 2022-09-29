using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebServiceCitiesApi.Models;
using WebServiceCitiesApi.Services;

namespace WebServiceCitiesApi.Controllers
{
    [ApiController]//not really necessary but improves developer xperience
    [Route("api/cities/{cityId:int}/interests")]
    public class PointOfInterestController : ControllerBase
    {
        //log to console by default
        //om ändring i appsettingDevelopment så ändras också om loggningen skrivs ner eller ej
        //har ändrat i appsettingDevelopment så att alla Controllers (WebServiceCitiesApi.Controllers) har Warning
        //det finns inbyggt för att logga till Eventloggen
        //nuget Serilog is a better logger, serilog.aspnetcore,serilog.sinks.console och serilog.sinks.file loggar till respektive
        private ILogger<PointOfInterestController> _logger;

        private IMailService _mailservice;

        //constructor injection
        public PointOfInterestController(ILogger<PointOfInterestController> log, IMailService mail) {

            //null-coalescing operator ??
            _logger = log ?? throw new Exception("Ilogger is null");
            _mailservice = mail ?? throw new Exception("Imail is null"); ;
            //HttpContext.RequestServices.GetService another way but recommend is to use constructor injection.

        }

        [HttpGet()]
        public ActionResult<IEnumerable<PointOfInterest>> GetPointsOfInterest(int cityId)
        {
            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            return Ok(city.PointOfInterests);
        }

        [HttpGet("{interestId:int}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterest> GetPointOfInterest(int cityId, int interestId)
        {
            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            var poi = city.PointOfInterests.FirstOrDefault(c => c.Id == interestId);
            if (poi is null) return NotFound();
            return Ok(poi);
        }


        [HttpPost()]
        public ActionResult<PointOfInterest> AddPointOfInterest(int cityId, [FromBody] PointOfInterest interest)
        {
            //FromBody not necessary, assumed to come from that by default
            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();
            var t = 0;
            if (city.PointOfInterests.Count > 0)
                t = city.PointOfInterests.Max(c => c.Id);
            interest.Id = ++t;
            city.PointOfInterests.Add(interest);
            Console.WriteLine("________________________________");
            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, interestId = interest.Id }, interest);
        }

        [HttpPut("{interestId:int}")]
        public ActionResult FullUpdatePointOfInterest(int cityId, int interestid, PointOfInterest interest)
        {
            //nedan är access till en Token
            //var tt = User.Claims.FirstOrDefault(c => c.Type == "City" && c.Value == "något")?.Value;
            //return Forbid; //tex om användaren är förbjuden att accessa resuren

            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            //kod för att uppdatera

            return NoContent();
        }

        [HttpPatch("{interestId:int}")]
        public ActionResult PatchUpdatePointOfInterest(int cityId, int interestId, JsonPatchDocument<PointOfInterest> patchdocument)
        {
            //nuget package microsoft.aspnetcore.jsonpatch + mvc.newtonsoftjson
            //using another serialize, added i Program builder.Services.AddControllers
            //.AddNewtonsoftJson() must be added as default xml serializer
            //klienten måste skriva speciellt patch-format i bodyn

            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();
            var poi = city.PointOfInterests.FirstOrDefault(c => c.Id == interestId);
            if (poi is null) return NotFound();

            patchdocument.ApplyTo(poi,ModelState); //check validationrules

            if(!ModelState.IsValid) return BadRequest(ModelState);   //validerar patch dokumentet bara, inte vår modell
            if(TryValidateModel(poi)) return BadRequest(ModelState);  //validerar modellen efter applypatch, behövs för att undvika tex att required fält sätt till null i en update
            //har uppdateringarna gått igenom fastän nullvärde i required fält????

            return NoContent();
        }

        [HttpDelete("{interestId:int}")]
        public ActionResult DeletePointOfInterest(int cityId, int interestId)
        {

            try
            {
                int i = 15;
                float ii = 15 / 1;
            }
            catch (Exception e) { _logger.LogCritical("mymessage",e);
                return StatusCode(500, "my msg");
            }
                    
                    
             //code for removal
            return NoContent();
        }
    }
}
