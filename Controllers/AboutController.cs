using Microsoft.AspNetCore.Mvc;

namespace MetaSphere.Controllers;

public class AboutController : Controller
{
    public IActionResult Index() => View();
}
