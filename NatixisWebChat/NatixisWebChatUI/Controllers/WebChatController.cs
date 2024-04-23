namespace NatixisWebChatUI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class WebChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
