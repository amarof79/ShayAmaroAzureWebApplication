using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureWebApp.Models;
using AzureWebApplication.Data;

namespace AzureWebApplication.Pages.Computers
{
    public class DeleteModel : PageModel
    {
        private readonly AzureWebApplication.Data.AzureWebApplicationContext _context;

        public DeleteModel(AzureWebApplication.Data.AzureWebApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Computer Computer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Computer == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer.FirstOrDefaultAsync(m => m.Id == id);

            if (computer == null)
            {
                return NotFound();
            }
            else 
            {
                Computer = computer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Computer == null)
            {
                return NotFound();
            }
            var computer = await _context.Computer.FindAsync(id);

            if (computer != null)
            {
                Computer = computer;
                _context.Computer.Remove(Computer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
