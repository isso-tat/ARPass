using Newtonsoft.Json;

namespace ARPass.Auth
{
	public class AuthEntity
	{
		[JsonProperty("name")]
		public string Name { get; private set; }
	}
}