namespace WebServiceCitiesApi.Models
{
    public class CityDto
    {
        public CityDto() { }
        public CityDto(int id, string name, string? descripton)
        {
            Id = id;
            Name = name;
            Descripton = descripton;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 

        public string? Descripton { get; set; }  

        public ICollection<PointOfInterest> PointOfInterests { get; set; } = new List<PointOfInterest>();
    }
}
