using System.Threading.Tasks;

namespace WebAppi.Authentication.ApiKey
{
    public interface IApiKeyAuthenticationService
	{
		Task<bool> IsValidApiKey(string apiKey);
	}
}