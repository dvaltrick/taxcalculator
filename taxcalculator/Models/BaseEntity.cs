using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taxcalculator.Models
{
    public class BaseEntity : IEntity
    {
        public Guid ID { get; set; }

        public void GenerateId()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
