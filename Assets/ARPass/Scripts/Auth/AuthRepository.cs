using UnityEngine;

namespace ARPass.Auth
{
	public class AuthRepository
	{
		public static AuthRepository Instance = new AuthRepository();

		AuthEntity _entity;

		public AuthEntity Entity => _entity;

		public void SaveAuth(AuthEntity entity)
		{
			_entity = entity;
		}
	}
}