using System.Threading.Tasks;

namespace WebAppiApiKey.Authentication.ApiKey
{
    public interface IApiKeyAuthenticationService
	{
		Task<bool> IsValidApiKey(string apiKey);
	}
}