using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestApp2.Data;
using TestApp2.Data.Models;

namespace TestApp2.Pages;

public class LinkDetailsModel : PageModel
{
    private readonly TestApp2.Data.ApplicationDbContext _context;

    public LinkDetailsModel(TestApp2.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public LinkEntity LinkEntity { get; set; } = default!;
    public IList<LinkEntryEntity> History { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null || _context.Links == null)
        {
            return NotFound();
        }

        var linkentity = await _context.Links
            .Include(x => x.History)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (linkentity == null)
        {
            return NotFound();
        }
        else
        {
            LinkEntity = linkentity;
            History = linkentity.History;
        }
        return Page();
    }
}