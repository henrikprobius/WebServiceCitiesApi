using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServiceCitiesApi.Models
{
        public class EFPointOfInterest
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; } = 0;

             [Required]
             [MaxLength(50)]
            public string Name { get; set; } = String.Empty;


            public string Description { get; set; } = String.Empty;

            [ForeignKey("Id")]
            public EFCity? EFCity { get; set; }

        }
}
