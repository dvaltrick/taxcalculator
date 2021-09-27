using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using taxcalculator.Database;
using taxcalculator.Models;

namespace taxcalculator.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly taxcalculator.Database.TaxContext _context;

        public DetailsModel(taxcalculator.Database.TaxContext context)
        {
            _context = context;
        }

        public Calc Calc { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calc = await _context.Calcs
                .Include(c => c.Type).FirstOrDefaultAsync(m => m.ID == id);

            if (Calc == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
