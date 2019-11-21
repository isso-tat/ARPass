using Newtonsoft.Json;

namespace ARPass.Auth
{
	public class AuthEntity
	{
		[JsonProperty("id")]
		public int Id { get; private set; }

	}
}