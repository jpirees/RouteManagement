using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteManagement.Models
{
    public class TeamViewModel
    {
        [NotMapped]
        public string Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public bool IsAvailable { get; set; }

        [NotMapped]
        public virtual IList<PersonViewModel> People { get; set; }
    }
}
