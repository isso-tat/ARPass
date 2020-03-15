using Newtonsoft.Json;

namespace ARPass.Core.Auth
{
	public class AuthEntity
	{
		[JsonProperty("name")]
		public string Name { get; private set; }
	}
}