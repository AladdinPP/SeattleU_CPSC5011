// AUTHOR:      Hongru He
// FILENAME:    FormulaTest.cs
// DATE:        01/12/2024

using System;
using NUnit.Framework;

namespace P1
{
    [TestFixture]
    public class FormulaTest
    {
        [Test]
        public void Constructor_InitializePropertiesCorrectly()
        {
            string inputM = "iron ore";
            string outputM = "iron bar";
            int inputN = 2;
            int outputN = 1;
            Formula formulaTest = new Formula(inputN, inputM, outputN, outputM);
            Assert.AreEqual(inputM, formulaTest.QueryInputMaterial());
            Assert.AreEqual(inputN, formulaTest.QueryInputNumber());
            Assert.AreEqual(outputM, formulaTest.QueryOutputMaterial());
            Assert.AreEqual(outputN, formulaTest.QueryOutputNumber());
        }
        
        [Test]
        public void Constructor_InvalidNullInputM()
        {
            string outputM = "iron bar";
            int inputN = 2;
            int outputN = 1;
            Assert.Throws<ArgumentException>(() => new Formula(inputN, null, outputN, outputM));
        }
        
        [Test]
        public void Constructor_InvalidEmptyInputM()
        {
            string inputM = " ";
            string outputM = "iron bar";
            int inputN = 2;
            int outputN = 1;
            Assert.Throws<ArgumentException>(() => new Formula(inputN, inputM, outputN, outputM));
        }
        
        [Test]
        public void Constructor_InvalidNullOutputM()
        {
            string inputM = "iron ore";
            int inputN = 2;
            int outputN = 1;
            Assert.Throws<ArgumentException>(() => new Formula(inputN, inputM, outputN, null));
        }
        
        [Test]
        public void Constructor_InvalidEmptyOutputM()
        {
            string inputM = "iron ore";
            string outputM = " ";
            int inputN = 2;
            int outputN = 1;
            Assert.Throws<ArgumentException>(() => new Formula(inputN, inputM, outputN, outputM));
        }
     
        [Test]
        public void Constructor_InvalidNegativeInputN()
        {
            string inputM = "iron ore";
            string outputM = "iron bar";
            int inputN = -2;
            int outputN = 1;
            Assert.Throws<ArgumentException>(() => new Formula(inputN, inputM, outputN, outputM));
        }
        
        [Test]
        public void Constructor_InvalidZeroInputN()
        {
            string inputM = "iron ore";
            string outputM = "iron bar";
            int inputN = 0;
            int outputN = 1;
            Assert.Throws<ArgumentException>(() => new Formula(inputN, inputM, outputN, outputM));
        }
        
        [Test]
        public void Constructor_InvalidNegativeOutputN()
        {
            string inputM = "iron ore";
            string outputM = "iron bar";
            int inputN = 2;
            int outputN = -1;
            Assert.Throws<ArgumentException>(() => new Formula(inputN, inputM, outputN, outputM));
        }
        
        [Test]
        public void Constructor_InvalidZeroOutputN()
        {
            string inputM = "iron ore";
            string outputM = "iron bar";
            int inputN = 2;
            int outputN = 0;
            Assert.Throws<ArgumentException>(() => new Formula(inputN, inputM, outputN, outputM));
        }
        
        [Test]
        public void QueryInputMaterial_returnCorrectly()
        {
            Formula formulaTest = new Formula(2, "iron ore", 1,"iron bar");
            string result = formulaTest.QueryInputMaterial();
            Assert.That(result, Is.EqualTo("iron ore"));
        }
        
        [Test]
        public void QueryOutputMaterial_returnCorrectly()
        {
            Formula formulaTest = new Formula(2, "iron ore", 1,"iron bar");
            string result = formulaTest.QueryOutputMaterial();
            Assert.That(result, Is.EqualTo("iron bar"));
        }
        
        [Test]
        public void QueryInputNumber_returnCorrectly()
        {
            Formula formulaTest = new Formula(2, "iron ore", 1,"iron bar");
            int result = formulaTest.QueryInputNumber();
            Assert.That(result, Is.EqualTo(2));
        }
        
        [Test]
        public void QueryOutputNumber_returnCorrectly()
        {
            Formula formulaTest = new Formula(2, "iron ore", 1,"iron bar");
            int result = formulaTest.QueryOutputNumber();
            Assert.That(result, Is.EqualTo(1));
        }
        
        [Test]
        public void apply_ValidInput()
        {
            Formula formulaTest = new Formula(2, "iron ore", 1,"iron bar");
            int result = formulaTest.Apply(6);
            Assert.That(result, Is.AtLeast(0));
            Assert.That(result, Is.LessThanOrEqualTo(1 * (6 / 2) + 1));
        }
        
        [Test]
        public void apply_InvalidInput()
        {
            Formula formulaTest = new Formula(2, "iron ore", 1,"iron bar");
            Assert.Throws<ArgumentException>(() => formulaTest.Apply(5));
        }
    }
}