// AUTHOR:      Hongru He
// FILENAME:    FormulaTest.cs
// DATE:        02/15/2024

using System;
using NUnit.Framework;

namespace P3Test
{
    [TestFixture]
    public class FormulaTest
    {
        [Test]
        public void Constructor_InitializePropertiesCorrectly()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input, 
                outputMat, outputNum, output);
            Assert.IsNotNull(formulaTest, "Formula instance should not be null after construction.");
        }
        
        [Test]
        public void Constructor_InvalidNullInputM()
        {
            int input = 2;
            string[] inputMat = { null, "Hydrogen" };
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Assert.Throws<ArgumentException>(() => new Formula(inputMat, inputNum, 
                input, outputMat, outputNum, output));
        }
        
        [Test]
        public void Constructor_InvalidEmptyInputM()
        {
            int input = 2;
            string[] inputMat = {" ", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Assert.Throws<ArgumentException>(() => new Formula(inputMat, inputNum, 
                input, outputMat, outputNum, output));
        }
        
        [Test]
        public void Constructor_InvalidNullOutputM()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = { null };
            int[] outputNum = {1};
            Assert.Throws<ArgumentException>(() => new Formula(inputMat, inputNum, 
                input, outputMat, outputNum, output));
        }
        
        [Test]
        public void Constructor_InvalidEmptyOutputM()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {" "};
            int[] outputNum = {1};
            Assert.Throws<ArgumentException>(() => new Formula(inputMat, inputNum, 
                input, outputMat, outputNum, output));
        }
     
        [Test]
        public void Constructor_InvalidNegativeInputN()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {-2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Assert.Throws<ArgumentException>(() => new Formula(inputMat, inputNum, 
                input, outputMat, outputNum, output));
        }
        
        [Test]
        public void Constructor_InvalidZeroInputN()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {0, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Assert.Throws<ArgumentException>(() => new Formula(inputMat, inputNum, 
                input, outputMat, outputNum, output));
        }
        
        [Test]
        public void Constructor_InvalidNegativeOutputN()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {-1};
            Assert.Throws<ArgumentException>(() => new Formula(inputMat, inputNum, 
                input, outputMat, outputNum, output));
        }
        
        [Test]
        public void Constructor_InvalidZeroOutputN()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {0};
            Assert.Throws<ArgumentException>(() => new Formula(inputMat, inputNum, 
                input, outputMat, outputNum, output));
        }

        [Test]
        public void DeepCopy_ShouldCreateDistinctInstance()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input, 
                outputMat, outputNum, output);

            Formula copyTest = formulaTest.DeepCopy();
            
            Assert.AreNotSame(formulaTest, copyTest, "Should return a distinct Formula instance.");
        }
        
        [Test]
        public void QueryInputMaterial_returnCorrectly()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input, 
                outputMat, outputNum, output);
            
            string result = formulaTest.QueryInputMaterial();
            
            Assert.IsNotNull(result, "Should not return null.");
            Assert.That(result, Is.EqualTo("2 Oxygen\n1 Hydrogen\n"));
        }
        
        [Test]
        public void QueryOutputMaterial_returnCorrectly()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input, 
                outputMat, outputNum, output);
            
            string result = formulaTest.QueryOutputMaterial();
            
            Assert.IsNotNull(result, "Should not return null.");
            Assert.That(result, Is.EqualTo("1 Water\n"));
        }
        
        [Test]
        public void apply_ShouldNotReturnNull()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input, 
                outputMat, outputNum, output);

            string result = formulaTest.Apply();
            
            Assert.IsNotNull(result, "Should not return null.");
            Assert.IsNotEmpty(result, "Should not return empty string.");
        }
    }
}