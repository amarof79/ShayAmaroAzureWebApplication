using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AzureWebApp.Models;
using AzureWebApplication.Data;

namespace AzureWebApplication.Pages.Computers
{
    public class CreateModel : PageModel
    {
        private readonly AzureWebApplication.Data.AzureWebApplicationContext _context;

        public CreateModel(AzureWebApplication.Data.AzureWebApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Computer Computer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Computer == null || Computer == null)
            {
                return Page();
            }

            _context.Computer.Add(Computer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
