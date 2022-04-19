using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteManagement.Models
{
    public class PersonViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Disponível")]
        public bool IsAvailable { get; set; }

        public PersonViewModel() { }

        public PersonViewModel(string id, string name, bool isAvailable)
        {
            Id = id;
            Name = name;
            IsAvailable = isAvailable;
        }
    }
}
