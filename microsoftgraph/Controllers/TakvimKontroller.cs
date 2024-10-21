using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.Threading.Tasks;

[Authorize]
public class TakvimController : Controller
{
    private readonly GraphServiceClient _graphServiceClient;

    public TakvimController(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }

    public async Task<IActionResult> Index()
    {
        // Kullanıcının takvimindeki etkinlikleri al
        var user = await _graphServiceClient.Me.GetAsync();
        return View();
    }
}
