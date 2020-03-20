using Newtonsoft.Json;

namespace D365.Plugins.Model
{
	public class EmployeeRequest
	{
		[JsonProperty("name")]
		public object EmployeeName { get; set; }

		[JsonProperty("salary")]
		public int Salary { get; set; }

		[JsonProperty("age")]
		public int Age { get; set; }
	}
}