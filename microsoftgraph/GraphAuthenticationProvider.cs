using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

public class GraphAuthenticationProvider : IAuthenticationProvider
{
    private readonly ITokenAcquisition _tokenAcquisition;

    public GraphAuthenticationProvider(ITokenAcquisition tokenAcquisition)
    {
        _tokenAcquisition = tokenAcquisition;
    }

    public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        // Kullanıcıdan erişim tokeni alma
        string[] scopes = new[] { "User.Read" }; // Gerekli izinleri belirtin
        var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);

        // Authorization başlığını ekleyin
        request.Headers.Add("Authorization", "Bearer " + accessToken);
    }

}
