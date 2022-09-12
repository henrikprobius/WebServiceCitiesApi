using WebServiceCitiesApi.Models;
namespace WebServiceCitiesApi
{

    public class CityDatastore
    {
        private static List<CityDto> cities = new();

        public CityDatastore()
        {}

        public static List<CityDto> Cities {

            get {
                if (cities.Count == 0) TestData();
                return cities;  
            
            } 
        
        
        }

        private static void TestData()
        {
            cities.Add(new(1, "Paris", "Wonderful Paris"));
            cities.Add(new(2, "Stockholm", "Wonderful Stockholm"));
            cities.Add(new(3, "Oslo", "Wonderful Oslo"));
            cities.Add(new(4, "Berlin", "Wonderful Berlin"));
            cities.Add(new(5, "Wien", "Wonderful Vienna"));
            cities.Add(new(6, "Peking", "Wonderful Beijing"));
            cities[0].PointOfInterests.Add(new(55, "interest55"));
            cities[0].PointOfInterests.Add(new(66, "interest66"));
            cities[0].PointOfInterests.Add(new(26, "interest26"));
        }
    }
}
