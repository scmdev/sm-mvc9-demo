using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc9Demo.MvcWeb.Models;

namespace Mvc9Demo.MvcWeb.Controllers;

public class ItemsController : Controller
{
    private readonly Data.PostgresContext _context;

    public ItemsController(Data.PostgresContext context)
    {
        _context = context;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var items = await _context.Items.ToListAsync();

        return View(items);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id", "Name", "Price")] Item item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }

        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Id", "Name", "Price")] Item item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }

        _context.Update(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // [HttpPost, ActionName("Edit")]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Edit(int id)
    // {
    //     var itemToEdit = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
    //
    //     if (itemToEdit == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     if (!await TryUpdateModelAsync(
    //             itemToEdit,
    //             "",
    //             s => s.Name, s => s.Price))
    //     {
    //         return View(itemToEdit);
    //     }
    //
    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //         return RedirectToAction(nameof(Index));
    //     }
    //     catch (DbUpdateException /* ex */)
    //     {
    //         //Log the error (uncomment ex variable name and write a log.)
    //         ModelState.AddModelError("", "Unable to save changes. " +
    //                                      "Try again, and if the problem persists, " +
    //                                      "see your system administrator.");
    //     }
    //
    //     return View(itemToEdit);
    // }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

        if (item != null)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}