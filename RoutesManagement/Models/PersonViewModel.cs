using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoutesManagement.Models
{
    public class PersonViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        [Display(Name = "Status")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Time")]
        public string Team { get; set; }

        public PersonViewModel() { }

        public PersonViewModel(string id, string name, bool isAvailable, string team)
        {
            Id = id;
            Name = name;
            IsAvailable = isAvailable;
            Team = team;
        }
    }
}
