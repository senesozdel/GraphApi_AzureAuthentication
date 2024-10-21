using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web;

namespace microsoftgraph.Controllers
{
    public class UserController : Controller
    {
        private readonly GraphServiceClient _graphServiceClient;

        public UserController(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }
        [AuthorizeForScopes(Scopes = new[] { "User.Read" })]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Eğer kullanıcı giriş yapmamışsa Azure AD oturum açma sayfasına yönlendir
                return Challenge(new AuthenticationProperties { RedirectUri = Url.Action("Index") });
            }

            // Kullanıcı bilgilerini alma
            var user = await _graphServiceClient.Me.GetAsync();
            return View(user);
        }
    }
}
