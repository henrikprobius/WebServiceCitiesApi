using Microsoft.AspNetCore.Mvc;
using WebServiceCitiesApi.Models;

namespace WebServiceCitiesApi.Controllers
{
    [ApiController]//not really necessary but improves developer xperience
    [Route("api/cities/{cityId:int}/interests")]
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet()]
        public ActionResult<IEnumerable<PointOfInterest>> GetPointsOfInterest(int cityId)
        {
            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            return Ok(city.PointOfInterests);
        }

        [HttpGet("{interestId:int}",Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterest> GetPointOfInterest(int cityId, int interestId)
        {
            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) return NotFound();

            var poi = city.PointOfInterests.FirstOrDefault(c => c.Id == interestId);
            if (poi is null) return NotFound();
            return Ok(poi);
        }


        [HttpPost()]
        public ActionResult<PointOfInterest> AddPointOfInterest(int cityId, PointOfInterest interest)
        {
            //, PointOfInterest interest
            var city = CityDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null) return NotFound();
            var t = 0;
            if(city.PointOfInterests.Count > 0)
                t = city.PointOfInterests.Max(c => c.Id);
            interest.Id = ++t;
            city.PointOfInterests.Add(interest);
            Console.WriteLine("________________________________");
            return CreatedAtRoute("GetPointOfInterest",new { cityId = cityId, interestId = interest.Id }, interest);
        }
    }
}
