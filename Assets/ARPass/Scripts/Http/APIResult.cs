namespace ARPass.Http
{
	public class APIResult
	{
		public APIStatus Status { get; }
		public string Data { get; }
		public bool IsError => Status >= APIStatus.BadRequest;

		public APIResult(APIStatus status, string data)
		{
			Status = status;
			Data = data;
		}
	}
}