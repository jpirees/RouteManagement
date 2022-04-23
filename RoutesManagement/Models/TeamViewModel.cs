using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoutesManagement.Models
{
    public class TeamViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        [Display(Name = "Cidade de Operação")]
        public CityViewModel OperatingCity { get; set; }

        [Display(Name = "Time")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Integrantes")]
        public virtual IList<PersonViewModel> People { get; set; }
    }
}
