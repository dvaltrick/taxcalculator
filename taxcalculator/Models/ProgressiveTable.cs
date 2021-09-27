using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taxcalculator.Models
{
    public class ProgressiveTable : BaseEntity
    {
        [Display(Name = "%")]
        public double Rate { get; set; }

        [Display(Name = "From")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double ValueFrom { get; set; }

        [Display(Name = "To")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double ValueTo { get; set; }

    }
}
