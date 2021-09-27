using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using taxcalculator.Database;
using taxcalculator.Models;

namespace taxcalculator.Pages.TaxTypes
{
    public class TaxTypesModel : PageModel
    {
        private readonly taxcalculator.Database.TaxContext _context;

        public TaxTypesModel(taxcalculator.Database.TaxContext context)
        {
            _context = context;
        }

        public IList<TaxType> TaxType { get;set; }

        public async Task OnGetAsync()
        {
            TaxType = await _context.TaxTypes.ToListAsync();
        }
    }
}
