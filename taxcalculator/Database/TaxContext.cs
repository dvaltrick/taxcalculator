using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taxcalculator.Models;

namespace taxcalculator.Database
{
    public class TaxContext : DbContext
    {
        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<ProgressiveTable> Progressives { get; set; }
        public DbSet<Calc> Calcs { get; set; }


        public TaxContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TaxCalculatorDb");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxType>().HasData(
                new { ID = Guid.NewGuid(), 
                    PostalCode = "7441", 
                    CalcType = TaxCalculationType.PROGRESSIVE,
                    DefaultRate = 0.0,
                    DefaultValue = 0.0,
                    MaxEarns = 0.0
                },
                new { ID = Guid.NewGuid(), 
                    PostalCode = "A100", 
                    CalcType = TaxCalculationType.FLAT_VALUE, 
                    DefaultRate = 5.0,
                    DefaultValue = 10000.0,
                    MaxEarns = 200000.0
                },
                new { ID = Guid.NewGuid(), 
                    PostalCode = "7000", 
                    CalcType = TaxCalculationType.FLAT_RATE, 
                    DefaultRate = 17.5,
                    DefaultValue = 0.0,
                    MaxEarns = 0.0
                },
                new { ID = Guid.NewGuid(), 
                    PostalCode = "1000", 
                    CalcType = TaxCalculationType.PROGRESSIVE,
                    DefaultRate = 0.0,
                    DefaultValue = 0.0,
                    MaxEarns = 0.0
                });

            modelBuilder.Entity<ProgressiveTable>().HasData(
                new { ID = Guid.NewGuid(), Rate = 10.0, ValueFrom = 0.0, ValueTo = 8350.0 },
                new { ID = Guid.NewGuid(), Rate = 15.0, ValueFrom = 8351.0, ValueTo = 33950.0 },
                new { ID = Guid.NewGuid(), Rate = 25.0, ValueFrom = 33951.0, ValueTo = 82250.0 },
                new { ID = Guid.NewGuid(), Rate = 28.0, ValueFrom = 82251.0, ValueTo = 171550.0 },
                new { ID = Guid.NewGuid(), Rate = 33.0, ValueFrom = 171551.0, ValueTo = 372950.0 },
                new { ID = Guid.NewGuid(), Rate = 35.0, ValueFrom = 372951.0, ValueTo = Double.MaxValue }
                );
        }
    }
}
