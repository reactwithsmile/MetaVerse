using MetaSphere.Models;
using MetaSphere.Repositories;
using MetaSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MetaSphere.Controllers;

public class ExploreController(IItemRepository itemRepo, ILeadRepository leadRepo) : Controller
{
    private static readonly string[] KnownTags = ["Digital Asset", "Virtual Land", "Avatar", "Utility", "Wearable"];

    public async Task<IActionResult> Index(string? search, string? tag)
    {
        var vm = new ExploreViewModel
        {
            Items = await itemRepo.GetAllAsync(search, tag),
            Search = search,
            Tag = tag,
            Tags = KnownTags
        };
        return View(vm);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var item = await itemRepo.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(new ItemDetailViewModel { Item = item, Lead = new LeadViewModel { ItemId = id } });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Enquire(LeadViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            if (vm.ItemId.HasValue)
            {
                var item = await itemRepo.GetByIdAsync(vm.ItemId.Value);
                if (item != null)
                    return View("Detail", new ItemDetailViewModel { Item = item, Lead = vm });
            }
            return RedirectToAction("Index");
        }

        await leadRepo.AddAsync(new Lead
        {
            Name = vm.Name,
            Email = vm.Email,
            Phone = vm.Phone,
            Message = vm.Message,
            ItemId = vm.ItemId
        });

        TempData["Success"] = "Your inquiry has been submitted! We'll contact you soon.";
        return vm.ItemId.HasValue
            ? RedirectToAction("Detail", new { id = vm.ItemId })
            : RedirectToAction("Index");
    }
}
