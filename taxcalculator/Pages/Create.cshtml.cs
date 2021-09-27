using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using taxcalculator.Database;
using taxcalculator.Models;
using taxcalculator.Repositories;
using taxcalculator.Services;

namespace taxcalculator.Pages
{
    public class CreateModel : PageModel
    {
        private readonly taxcalculator.Database.TaxContext _context;
        private readonly CalcService _service;

        public CreateModel(taxcalculator.Database.TaxContext context, CalcService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult OnGet()
        {
            ViewData["TypeId"] = new SelectList(_context.TaxTypes, "ID", "PostalCode");
            return Page();
        }

        [BindProperty]
        public Calc Calc { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _service.Create(Calc);

            return RedirectToPage("./Index");
        }
    }
}
