using Newtonsoft.Json;

namespace ARPass.Auth
{
	public class AuthEntity
	{
		[JsonProperty("name")]
		public int Name { get; private set; }

	}
}