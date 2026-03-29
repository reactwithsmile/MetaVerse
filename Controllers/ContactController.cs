using MetaSphere.Repositories;
using MetaSphere.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MetaSphere.Controllers;

public class ContactController(ILeadRepository leadRepo) : Controller
{
    public IActionResult Index() => View(new LeadViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LeadViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        await leadRepo.AddAsync(new MetaSphere.Models.Lead
        {
            Name = vm.Name,
            Email = vm.Email,
            Phone = vm.Phone,
            Message = vm.Message
        });

        TempData["Success"] = "Message sent! We'll be in touch shortly.";
        return RedirectToAction("Index");
    }
}
