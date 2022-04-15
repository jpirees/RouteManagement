using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.API.Configuration
{
	public interface IMongoDatabaseSettings
	{
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
		string CollectionName { get; set; }
	}
}
