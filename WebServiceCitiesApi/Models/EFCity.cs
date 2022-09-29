using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceCitiesApi.Models
{
    public class EFCity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = String.Empty;


        public string Description { get; set; } = String.Empty;

        public ICollection<EFPointOfInterest> PointOfInterests { get;} = new List<EFPointOfInterest>();

    }
}
