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
    public class IndexModel : PageModel
    {
        private readonly taxcalculator.Database.TaxContext _context;

        public IndexModel(taxcalculator.Database.TaxContext context)
        {
            _context = context;
        }

        public IList<Calc> Calc { get;set; }

        public async Task OnGetAsync()
        {
            Calc = await _context.Calcs
                .Include(c => c.Type).ToListAsync();
        }
    }
}
