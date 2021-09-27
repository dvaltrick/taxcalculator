using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using taxcalculator.Database;
using taxcalculator.Models;

namespace taxcalculator.Pages
{
    public class EditModel : PageModel
    {
        private readonly taxcalculator.Database.TaxContext _context;

        public EditModel(taxcalculator.Database.TaxContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["TypeId"] = new SelectList(_context.TaxTypes, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Calc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalcExists(Calc.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CalcExists(Guid id)
        {
            return _context.Calcs.Any(e => e.ID == id);
        }
    }
}
