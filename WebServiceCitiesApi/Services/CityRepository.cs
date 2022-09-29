using Microsoft.EntityFrameworkCore;
using WebServiceCitiesApi.DBContext;
using WebServiceCitiesApi.Models;

namespace WebServiceCitiesApi.Services
{
    public class CityRepository : ICityInfoRepository
    {
        private readonly LocalSQLContex _dbcontext;

        //constructor injection of the DBContexty
        public CityRepository(LocalSQLContex context)
        {
            this._dbcontext = context ?? throw new ArgumentNullException("null fel när SQL sätts i CityRepository");
        }

        public async Task<IEnumerable<EFCity>> GetCitiesAsync()
        {
            return await _dbcontext.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> GetCityExistsAsync(int cityID)
        {

            return await _dbcontext.Cities.AnyAsync(c => c.Id == cityID);
        }

        public async Task<EFCity?> GetCityAsync(int cityID, bool includeinterests)
        {
            if (includeinterests)
            {
                return await _dbcontext.Cities.Include(c => c.PointOfInterests).
                Where(ci => ci.Id == cityID).FirstOrDefaultAsync();  //FirstOrDefaultAsync() will execute the query
            }
            return await _dbcontext.Cities.Where(ci => ci.Id == cityID).FirstOrDefaultAsync();
        }

        public async Task<EFPointOfInterest?> GetInterestForCityAsync(int cityID, int interestID)
        {
            return await _dbcontext.Interests.Where(p => p.EFCity.Id == cityID && p.Id == interestID).FirstOrDefaultAsync();       
        }


        public async Task AddInterestForCityAsync(int cityID, EFPointOfInterest interest)
        {
            var city = await GetCityAsync(cityID, false);
            if(city != null)
            {
                city.PointOfInterests.Add(interest);
            }
        
        }

        //sparar till db
        public async Task<bool> SaveChangesAsync()
        {
            //returnerar antalet ändringar
            await _dbcontext.SaveChangesAsync(); return true; }   
    }
}
