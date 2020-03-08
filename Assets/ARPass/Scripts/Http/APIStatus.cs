namespace ARPass.Http
{
	public enum APIStatus
	{
		OK = 200,
		Created = 201,
		Redirect = 302,
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403,
		NotFound = 404,
		MethodNotAllowed = 405,
		Timeout = 408,
		Invalid = 422,
		ServerError = 500,
		Unavailable = 503,
	}
}