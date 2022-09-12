namespace WebServiceCitiesApi.Models
{
    public class PointOfInterest
    {

        public PointOfInterest()
        {}
        public PointOfInterest(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; } 

        public string Name { get; set; }  = string.Empty;
    }
}
