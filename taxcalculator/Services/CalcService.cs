using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taxcalculator.Models;
using taxcalculator.Repositories;

namespace taxcalculator.Services
{
    public class CalcService
    {
        private readonly CalcRepository _repository;
        private readonly TaxTypeRepository _taxTypeRepository;
        private readonly ProgressiveTableRepository _progressiveRepository;

        public CalcService(CalcRepository repository,
            TaxTypeRepository taxTypeRepository,
            ProgressiveTableRepository _progressiveRepository) {
            this._repository = repository;
            this._taxTypeRepository = taxTypeRepository;
            this._progressiveRepository = _progressiveRepository;
        }

        public Calc Create(Calc calc) {
            var type = _taxTypeRepository.GetById(calc.TypeId);
            switch (type.CalcType) {
                case TaxCalculationType.FLAT_RATE:
                    calc.OutValue = CalcFlatRate(calc, type);
                    break;
                case TaxCalculationType.FLAT_VALUE:
                    calc.OutValue = CalcFlatValue(calc, type);
                    break;
                case TaxCalculationType.PROGRESSIVE:
                    calc.OutValue = CalcProgressive(calc);
                    break;
            }
            calc.calculatedAt = DateTime.Now;

            return _repository.Create(calc);
        }

        private double CalcFlatRate(Calc calc, TaxType type) {
            return (calc.InValue * (type.DefaultRate / 100));
        }

        private double CalcFlatValue(Calc calc, TaxType type) {
            if (calc.InValue < type.MaxEarns)
            {
                return calc.InValue * (type.DefaultRate / 100);
            }
            else
            {
                return type.DefaultValue;
            }
        }

        private double CalcProgressive(Calc calc) {
            var tables = _progressiveRepository.GetAll().OrderBy(i => i.ValueFrom);
            var acumulatedTax = 0.00;
            var lastStep = 0.00;

            foreach (ProgressiveTable table in tables) {
                if (calc.InValue > table.ValueTo)
                {
                    acumulatedTax += (table.ValueTo - lastStep) * (table.Rate / 100);
                    lastStep = table.ValueTo;
                }
                else
                {
                    acumulatedTax += (calc.InValue - lastStep) * (table.Rate / 100);
                    break;
                }
            }

            return acumulatedTax;
        }


    }
}
