using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taxcalculator.Models
{
    interface IEntity
    {
        Guid ID { get; set; }

        void GenerateId();
    }
}
