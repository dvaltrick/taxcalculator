using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taxcalculator.Models
{
    public class Calc : BaseEntity
    {
        public Guid TypeId { get; set; }
        [Display(Name = "Postal Code")]
        public TaxType Type { get; set; }
        [Display(Name = "Input Value")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double InValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        [Display(Name = "Calculated Taxes")]
        public double OutValue { get; set; }
        [Display(Name = "Calculated At")]
        public DateTime calculatedAt { get; set; }
    }
}
