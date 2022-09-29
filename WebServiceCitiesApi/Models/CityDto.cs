using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage ="Här kan man skriva ett eget felmeddelande")]
        [MaxLength(50)]
        //System.ComponentModel.DataAnnotations in here are many datavalidations that can be used
        // dessa checkas vid anrop i ModelState.IsValid i Controllern och denna returnerar BadRequest automatiskts
        // om mycket komplexa regler, fluentvalidation.net
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Descripton { get; set; }  

        public ICollection<PointOfInterest> PointOfInterests { get; set; } = new List<PointOfInterest>();
    }
}
