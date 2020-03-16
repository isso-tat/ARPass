using Newtonsoft.Json;
using UnityEngine;

namespace ARPass.Core.Auth
{
	public class AuthEntity
	{
		[JsonProperty("name")]
		public string Name { get; private set; }
		
		[JsonProperty("position")]
		public Vector2 Position { get; private set; }
	}
}