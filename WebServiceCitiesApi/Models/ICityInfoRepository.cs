namespace WebServiceCitiesApi.Models
{
    //The Repository-pattern
    public interface ICityInfoRepository
    {
        //diskussion om typ som ska returneras av nedan
        //bättre scalability, ej nödvändigtvis snabbar med Task<>
        Task<IEnumerable<EFCity>> GetCitiesAsync();

        Task<EFCity?> GetCityAsync(int cityID, bool includeinterests);
        Task<bool> GetCityExistsAsync(int cityID);

        Task<bool> SaveChangesAsync();

        Task<EFPointOfInterest> GetInterestForCityAsync(int cityID, int interestID);

        Task AddInterestForCityAsync(int cityID, EFPointOfInterest interest);

        //IQueryable<EFCity> GetCities() { }//här kan användaren av Repository lägga på LINQ villkor innan frågan körs
    }
}
