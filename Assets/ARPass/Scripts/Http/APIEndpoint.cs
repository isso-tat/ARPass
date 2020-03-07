using UniRx.Async;

namespace ARPass.Http
{
	public partial class APIClient
	{
		public async UniTask<string> Test()
		{
			var response = await Get("test/a");
			return response.Data;
		}
	}
}