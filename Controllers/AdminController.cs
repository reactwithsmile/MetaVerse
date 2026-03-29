using MetaSphere.Models;
using MetaSphere.Repositories;
using MetaSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MetaSphere.Controllers;

public class AdminController(IItemRepository itemRepo, ILeadRepository leadRepo) : Controller
{
    public IActionResult Index() => View();

    // Items
    public async Task<IActionResult> Items() => View(await itemRepo.GetAllAsync());

    public IActionResult CreateItem() => View("ItemForm", new ItemFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateItem(ItemFormViewModel vm)
    {
        if (!ModelState.IsValid) return View("ItemForm", vm);
        await itemRepo.AddAsync(new Item
        {
            Title = vm.Title, Description = vm.Description, Tag = vm.Tag,
            Rarity = vm.Rarity, ImageUrl = vm.ImageUrl, IsFeatured = vm.IsFeatured
        });
        return RedirectToAction("Items");
    }

    public async Task<IActionResult> EditItem(int id)
    {
        var item = await itemRepo.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View("ItemForm", new ItemFormViewModel
        {
            Id = item.Id, Title = item.Title, Description = item.Description,
            Tag = item.Tag, Rarity = item.Rarity, ImageUrl = item.ImageUrl, IsFeatured = item.IsFeatured
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditItem(ItemFormViewModel vm)
    {
        if (!ModelState.IsValid) return View("ItemForm", vm);
        await itemRepo.UpdateAsync(new Item
        {
            Id = vm.Id, Title = vm.Title, Description = vm.Description,
            Tag = vm.Tag, Rarity = vm.Rarity, ImageUrl = vm.ImageUrl, IsFeatured = vm.IsFeatured
        });
        return RedirectToAction("Items");
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteItem(int id)
    {
        await itemRepo.DeleteAsync(id);
        return RedirectToAction("Items");
    }

    // Leads
    public async Task<IActionResult> Leads() => View(await leadRepo.GetAllAsync());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteLead(int id)
    {
        await leadRepo.DeleteAsync(id);
        return RedirectToAction("Leads");
    }
}
