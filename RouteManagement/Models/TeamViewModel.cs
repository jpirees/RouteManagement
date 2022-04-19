using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteManagement.Models
{
    public class TeamViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Cidade")]
        [Required]
        public string City { get; set; }

        [Display(Name = "Estado")]
        [Required]
        public string State { get; set; }

        [Display(Name = "Time Disponível")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Integrantes")]
        public virtual IList<PersonViewModel> People { get; set; }
    }
}
