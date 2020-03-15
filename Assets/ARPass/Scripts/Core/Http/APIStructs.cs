// All of these classes must be kept READONLY

using Newtonsoft.Json;

namespace ARPass.Core.Http
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
		
		[JsonProperty("level")]
		public int Level { get; private set; }
	}
}