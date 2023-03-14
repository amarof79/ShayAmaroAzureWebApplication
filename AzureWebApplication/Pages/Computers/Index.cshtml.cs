using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureWebApp.Models;
using AzureWebApplication.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace AzureWebApplication.Pages.Computers {
    [Authorize]
    public class IndexModel : PageModel {
        private readonly AzureWebApplication.Data.AzureWebApplicationContext _context;

        public IndexModel(AzureWebApplication.Data.AzureWebApplicationContext context) {
            _context = context;
        }

        public IList<Computer> ComputerList { get; set; } = default!;

        //-----------------------------------------added

        //Search Bar String
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        //Manufacturer Variables
        public SelectList? Manufacturers { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ComputerManufacturers { get; set; }

        //Serial Number Variables
        public SelectList? SerialNumbers { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ComputerSerialNumbers { get; set; }
        //-------------------------------------added

        public async Task OnGetAsync() {

            //gets serial number values in database
            IQueryable<string> serialNumberQuery = from m in _context.Computer
                                                   orderby m.SerialNumber
                                                   select m.SerialNumber;

            //get manufacturer values in database
            IQueryable<string> manufacturerQuery = from m in _context.Computer
                                                   orderby m.Manufacturer
                                                   select m.Manufacturer;

            //gets all values in database
            var computers = from m in _context.Computer
                            select m;

            //----------------------if string not empty, find value (applies to all 3 ifs)
            if (!String.IsNullOrEmpty(SearchString)) {
                //finds value from search box
                computers = computers.Where(s => s.ComputerName.Contains(SearchString));
            }

            if (!String.IsNullOrEmpty(ComputerSerialNumbers)) {
                computers = computers.Where(s => s.SerialNumber == ComputerSerialNumbers);
            }

            if (!String.IsNullOrEmpty(ComputerManufacturers)) {
                computers = computers.Where(s => s.Manufacturer == ComputerManufacturers);
            }

            //---------------------------------------------------------------------------------

            //returns all distinct values into list
            Manufacturers = new SelectList(await manufacturerQuery.Distinct().ToListAsync());
            SerialNumbers = new SelectList(await serialNumberQuery.Distinct().ToListAsync());

            //waits until all values returns
            ComputerList = await computers.ToListAsync();

            /*
            if (_context.Computers != null)
            {
                Computer = await _context.Computers.ToListAsync();
            } */
        }
    }
}
