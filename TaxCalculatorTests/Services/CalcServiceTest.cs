using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taxcalculator.Database;
using taxcalculator.Models;
using taxcalculator.Repositories;
using taxcalculator.Services;

namespace TaxCalculatorTests.Services
{
    public class CalcServiceTest
    {
        private Mock<CalcRepository> _calcRepoMock;
        private CalcService _service;
        private Mock<TaxTypeRepository> _taxTypeRepoMock;
        private Mock<ProgressiveTableRepository> _progressRepoMock;

        [SetUp]
        public void Setup()
        {
            _taxTypeRepoMock = new Mock<TaxTypeRepository>();
            _progressRepoMock = new Mock<ProgressiveTableRepository>();
            _calcRepoMock = new Mock<CalcRepository>();
            _service = new CalcService(_calcRepoMock.Object,
                _taxTypeRepoMock.Object,
                _progressRepoMock.Object);
        }

        private IEnumerable<ProgressiveTable> GetFakeProgressiveTable()
        {
            var types = new List<ProgressiveTable>
            {
                new ProgressiveTable { ID = Guid.NewGuid(), Rate = 10.0, ValueFrom = 0.0, ValueTo = 8350.0 },
                new ProgressiveTable { ID = Guid.NewGuid(), Rate = 15.0, ValueFrom = 8351.0, ValueTo = 33950.0 },
                new ProgressiveTable { ID = Guid.NewGuid(), Rate = 25.0, ValueFrom = 33951.0, ValueTo = 82250.0 },
                new ProgressiveTable { ID = Guid.NewGuid(), Rate = 28.0, ValueFrom = 82251.0, ValueTo = 171550.0 },
                new ProgressiveTable { ID = Guid.NewGuid(), Rate = 33.0, ValueFrom = 171551.0, ValueTo = 372950.0 },
                new ProgressiveTable { ID = Guid.NewGuid(), Rate = 35.0, ValueFrom = 372951.0, ValueTo = Double.MaxValue }
            };

            return types;
        }


        [Test]
        public void testCreateFlatRate()
        {
            var guidType = Guid.NewGuid();
            var dateTime = DateTime.Now;

            var mockType = new TaxType();
            mockType.ID = guidType;
            mockType.PostalCode = "7000";
            mockType.DefaultRate = 17.5;
            mockType.CalcType = TaxCalculationType.FLAT_RATE;

            List<TaxType> listTaxTypeMock = new List<TaxType>();
            listTaxTypeMock.Add(mockType);

            var calc = new Calc();
            calc.InValue = 1000.00;
            calc.TypeId = guidType;

            _taxTypeRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => listTaxTypeMock.First());

            _calcRepoMock
                .Setup(x => x.Create(It.IsAny<Calc>()))
                .Returns<Calc>((Calc ret) => ret);

            var result = _service.Create(calc);
            Assert.AreEqual(result.OutValue, 175.00);
            Assert.GreaterOrEqual(result.calculatedAt, dateTime);   
        }

        [Test]
        public void testCreateFlatValueLowerThanMaxEarns()
        {
            var guidType = Guid.NewGuid();
            var dateTime = DateTime.Now;

            var mockType = new TaxType();
            mockType.ID = guidType;
            mockType.PostalCode = "A100";
            mockType.DefaultRate = 5.0;
            mockType.DefaultValue = 10000.00;
            mockType.MaxEarns = 200000.00;
            mockType.CalcType = TaxCalculationType.FLAT_VALUE;

            List<TaxType> listTaxTypeMock = new List<TaxType>();
            listTaxTypeMock.Add(mockType);

            var calc = new Calc();
            calc.InValue = 1000.00;
            calc.TypeId = guidType;

            _taxTypeRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => listTaxTypeMock.First());

            _calcRepoMock
                .Setup(x => x.Create(It.IsAny<Calc>()))
                .Returns<Calc>((Calc ret) => ret);

            var result = _service.Create(calc);
            Assert.AreEqual(result.OutValue, 50.00);
            Assert.GreaterOrEqual(result.calculatedAt, dateTime);
        }

        [Test]
        public void testCreateFlatValueGreaterThanMaxEarns()
        {
            var guidType = Guid.NewGuid();
            var dateTime = DateTime.Now;

            var mockType = new TaxType();
            mockType.ID = guidType;
            mockType.PostalCode = "A100";
            mockType.DefaultRate = 5.0;
            mockType.DefaultValue = 10000.00;
            mockType.MaxEarns = 200000.00;
            mockType.CalcType = TaxCalculationType.FLAT_VALUE;

            List<TaxType> listTaxTypeMock = new List<TaxType>();
            listTaxTypeMock.Add(mockType);

            var calc = new Calc();
            calc.InValue = 10000000.00;
            calc.TypeId = guidType;

            _taxTypeRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => listTaxTypeMock.First());

            _calcRepoMock
                .Setup(x => x.Create(It.IsAny<Calc>()))
                .Returns<Calc>((Calc ret) => ret);

            var result = _service.Create(calc);
            Assert.AreEqual(result.OutValue, 10000.00);
            Assert.GreaterOrEqual(result.calculatedAt, dateTime);
        }

        [Test]
        public void testCreateProgressiveFirstTier()
        {
            var guidType = Guid.NewGuid();
            var dateTime = DateTime.Now;

            var mockType = new TaxType();
            mockType.ID = guidType;
            mockType.PostalCode = "7441";
            mockType.CalcType = TaxCalculationType.PROGRESSIVE;

            List<TaxType> listTaxTypeMock = new List<TaxType>();
            listTaxTypeMock.Add(mockType);

            var calc = new Calc();
            calc.InValue = 500.00;
            calc.TypeId = guidType;

            _taxTypeRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => listTaxTypeMock.First());

            _progressRepoMock
                .Setup(x => x.GetAll())
                .Returns(GetFakeProgressiveTable().AsQueryable<ProgressiveTable>());

            _calcRepoMock
                .Setup(x => x.Create(It.IsAny<Calc>()))
                .Returns<Calc>((Calc ret) => ret);

            var result = _service.Create(calc);
            Assert.AreEqual(result.OutValue, 50.00);
            Assert.GreaterOrEqual(result.calculatedAt, dateTime);
        }

        [Test]
        public void testCreateProgressiveSecondTier()
        {
            var guidType = Guid.NewGuid();
            var dateTime = DateTime.Now;

            var mockType = new TaxType();
            mockType.ID = guidType;
            mockType.PostalCode = "1000";
            mockType.CalcType = TaxCalculationType.PROGRESSIVE;

            List<TaxType> listTaxTypeMock = new List<TaxType>();
            listTaxTypeMock.Add(mockType);

            var calc = new Calc();
            calc.InValue = 10000.00;
            calc.TypeId = guidType;

            _taxTypeRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => listTaxTypeMock.First());

            _progressRepoMock
                .Setup(x => x.GetAll())
                .Returns(GetFakeProgressiveTable().AsQueryable<ProgressiveTable>());

            _calcRepoMock
                .Setup(x => x.Create(It.IsAny<Calc>()))
                .Returns<Calc>((Calc ret) => ret);

            var result = _service.Create(calc);
            Assert.AreEqual(result.OutValue, 1082.5);
            Assert.GreaterOrEqual(result.calculatedAt, dateTime);
        }

        [Test]
        public void testCreateProgressiveLastTier()
        {
            var guidType = Guid.NewGuid();
            var dateTime = DateTime.Now;

            var mockType = new TaxType();
            mockType.ID = guidType;
            mockType.PostalCode = "1000";
            mockType.CalcType = TaxCalculationType.PROGRESSIVE;

            List<TaxType> listTaxTypeMock = new List<TaxType>();
            listTaxTypeMock.Add(mockType);

            var calc = new Calc();
            calc.InValue = 400000.00;
            calc.TypeId = guidType;

            _taxTypeRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns((Guid id) => listTaxTypeMock.First());

            _progressRepoMock
                .Setup(x => x.GetAll())
                .Returns(GetFakeProgressiveTable().AsQueryable<ProgressiveTable>());

            _calcRepoMock
                .Setup(x => x.Create(It.IsAny<Calc>()))
                .Returns<Calc>((Calc ret) => ret);

            var result = _service.Create(calc);
            Assert.AreEqual(result.OutValue, 117683.5);
            Assert.GreaterOrEqual(result.calculatedAt, dateTime);
        }
    }
}
