using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky.MSTest
{
    [TestClass]
    public class CalculatorMSTests
    {
        [TestMethod]
        public void AddNumbers_TwoInt_GetCorrectAddition()
        {
            //Arange
            var calculatorMSTests = new Calculator();

            //Act
            var result = calculatorMSTests.AddNumbers(10, 20);

            //Assert
            Assert.AreEqual(30, result);
        }
    }
}
