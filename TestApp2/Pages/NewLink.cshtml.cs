using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestApp2.Data;
using TestApp2.Data.Models;

namespace TestApp2.Pages
{
    [Authorize]
    public class NewLinkModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NewLinkModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LinkEntity LinkEntity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Links == null || LinkEntity == null)
            {
                return Page();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return Page();

            LinkEntity.CreateBy = user;
            _context.Links.Add(LinkEntity);
            await _context.SaveChangesAsync();
            return RedirectToPage("./MyLinks");
        }
    }
}
