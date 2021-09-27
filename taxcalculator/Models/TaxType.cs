using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taxcalculator.Models
{
    public class TaxType : BaseEntity
    {
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Type")]
        public TaxCalculationType CalcType { get; set; }
        public double DefaultRate { get; set; }
        public double DefaultValue { get; set; }
        public double MaxEarns { get; set; }

    }
}
