using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Models
{
	public class Person : BaseEntity
	{
		[BsonRequired]
		[JsonProperty("Name")]
		public string Name { get; set; }

		[BsonRequired]
		[JsonProperty("IsAvailable")]
		public bool IsAvailable { get; set; }
	}

}
