using MetaSphere.Repositories;
using MetaSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MetaSphere.Controllers;

public class HomeController(IItemRepository itemRepo) : Controller
{
    public async Task<IActionResult> Index()
    {
        var featured = await itemRepo.GetFeaturedAsync();
        return View(featured);
    }
}
