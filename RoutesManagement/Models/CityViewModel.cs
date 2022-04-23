using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoutesManagement.Models
{
    public class CityViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string State { get; set; }

        [NotMapped]
        public string CityState => $"{Name} - {State}";
    }
}
