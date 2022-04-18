using System.ComponentModel.DataAnnotations.Schema;

namespace RouteManagement.Models
{
    public class PersonViewModel
    {
        [NotMapped]
        public string Id { get; set; }
        public string Name { get; set; }
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
