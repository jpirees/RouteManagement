using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Models
{
    public class Team : BaseEntity
    {
        [BsonRequired]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [BsonRequired]
        [JsonProperty("People")]
        public virtual List<Person> People { get; set; }

        [BsonRequired]
        [JsonProperty("IsAvailable")]
        public bool IsAvailable { get; set; }

        [BsonRequired]
        [JsonProperty("State")]
        public string State { get; set; }

        [BsonRequired]
        [JsonProperty("City")]
        public string City { get; set; }
        
    }

}
