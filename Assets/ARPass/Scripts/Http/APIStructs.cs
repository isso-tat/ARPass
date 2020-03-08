// All of these classes must be kept READONLY

using Newtonsoft.Json;

namespace ARPass.Http
{
	public sealed class LoginResult
	{
		[JsonProperty("token")]
		public string Token { get; private set; }
	}

	public sealed class UserEntity
	{
		[JsonProperty("username")]
		public string UserName { get; private set; }
	}
}