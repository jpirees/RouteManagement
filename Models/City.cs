using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Models
{
    public class City : BaseEntity
    {
        [BsonRequired]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [BsonRequired]
        [JsonProperty("State")]
        public string State { get; set; }
    }
}
