using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using taxcalculator.Database;
using taxcalculator.Models;

namespace taxcalculator.Pages.ProgressiveTables
{
    public class ProgressiveTableModel : PageModel
    {
        private readonly taxcalculator.Database.TaxContext _context;

        public ProgressiveTableModel(taxcalculator.Database.TaxContext context)
        {
            _context = context;
        }

        public IList<ProgressiveTable> ProgressiveTable { get;set; }

        public async Task OnGetAsync()
        {
            ProgressiveTable = await _context.Progressives.ToListAsync();
        }
    }
}
