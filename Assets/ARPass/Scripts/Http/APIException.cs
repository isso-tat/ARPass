using System;

namespace ARPass.Http
{
	public sealed class APIException : Exception
	{
		public APIStatus Status { get; }
		public string Data { get; }

		public APIException(APIStatus status, string data)
		{
			Status = status;
			Data = data;
		}
	}
}