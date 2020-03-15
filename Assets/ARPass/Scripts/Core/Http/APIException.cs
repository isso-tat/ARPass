using System;

namespace ARPass.Core.Http
{
	public sealed class APIException : Exception
	{
		public APIStatus Status { get; }
		public string ExceptionMessage { get; }

		public APIException(APIStatus status, string data)
		{
			Status = status;
			ExceptionMessage = data;
		}
	}
}