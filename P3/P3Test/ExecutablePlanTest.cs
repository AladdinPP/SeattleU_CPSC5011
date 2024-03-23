// AUTHOR:      Hongru He
// FILENAME:    ExecutablePlanTest.cs
// DATE:        02/16/2024

using System;
using NUnit.Framework;

namespace P3Test
{
    [TestFixture]
    public class ExecutablePlanTest
    {
        [Test]
        public void Constructor_DefaultCreateInstance()
        {
            ExecutablePlan epTest = new ExecutablePlan();
            Assert.IsNotNull(epTest, "Expected non-null ExecutablePlan instance.");
        }

        [Test]
        public void Constructor_OverloadedCreateInstance()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            ExecutablePlan epTest = new ExecutablePlan(formulasTest, 1);
            
            Assert.IsNotNull(epTest, "Expected non-null ExecutablePlan instance.");
        }

        [Test]
        public void Clone_ShouldCreateDistinctInstance()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            ExecutablePlan epTest = new ExecutablePlan(formulasTest, 1);

            ExecutablePlan cloneTest = epTest.Clone();
            
            Assert.AreNotSame(epTest, cloneTest, "Expected creating a distinct instance.");
        }

        [Test]
        public void QueryCurrentStep_ReturnsNonNullString()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            ExecutablePlan epTest = new ExecutablePlan(formulasTest, 1);

            string resultTest = epTest.QueryCurrentStep();

            Assert.IsNotNull(resultTest, "Expected non-null string returned.");
            Assert.IsNotEmpty(resultTest, "Expected non-empty string returned.");
        }
        
        [Test]
        public void QueryCurrentStep_ThrowsException()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            ExecutablePlan epTest = new ExecutablePlan(formulasTest, 1);

            epTest.ApplyCurrentStep();

            Assert.Throws<IndexOutOfRangeException>(() => epTest.QueryCurrentStep());
        }

        [Test]
        public void Reset_ResetState()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            ExecutablePlan epTest = new ExecutablePlan(formulasTest, 1);

            epTest.ApplyCurrentStep();
            Assert.Throws<IndexOutOfRangeException>(() => epTest.QueryCurrentStep());
            
            epTest.Reset();
            string resultTest = epTest.QueryCurrentStep();
            
            Assert.IsNotEmpty(resultTest, "Expected non-empty string.");

        }
        
        [Test]
        public void Remove_ValidFormula_ShouldRemoveFormula()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            ExecutablePlan epTest1 = new ExecutablePlan(formulasTest, 1);
            ExecutablePlan epTest2 = epTest1.Clone();
            
            epTest1.Remove();
            
            Assert.AreNotEqual(epTest1, epTest2, "Expected containing different Formula " +
                                                 "array after removing.");
        }

        [Test]
        public void Remove_CompletedFormula_ThrowException()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            ExecutablePlan epTest = new ExecutablePlan(formulasTest, 1);
            epTest.ApplyCurrentStep();

            Assert.Throws<InvalidOperationException>(() => epTest.Remove());
        }

        [Test]
        public void Replace_ValidFormulaAndIndex_ShouldReplaceFormula()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            int input2 = 3;
            string[] inputMat2 = {"Water", "Powder", "Sugar"};
            int[] inputNum2 = {3, 3, 1};

            int output2 = 1;
            string[] outputMat2 = {"Cookie"};
            int[] outputNum2 = {1};

            Formula formulaReplaced = new Formula(inputMat2, inputNum2, input2, 
                outputMat2, outputNum2, output2);
            
            ExecutablePlan epTest1 = new ExecutablePlan(formulasTest, 1);
            ExecutablePlan epTest2 = epTest1.Clone();
            
            epTest1.Replace(formulaReplaced, 0);
            
            Assert.AreNotEqual(epTest1, epTest2, "Expected containing different Formula " +
                                                 "after replacement.");
        }
        
        [Test]
        public void Replace_InvalidIndex_ThrowException()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            int input2 = 3;
            string[] inputMat2 = {"Water", "Powder", "Sugar"};
            int[] inputNum2 = {3, 3, 1};

            int output2 = 1;
            string[] outputMat2 = {"Cookie"};
            int[] outputNum2 = {1};

            Formula formulaReplaced = new Formula(inputMat2, inputNum2, input2, 
                outputMat2, outputNum2, output2);
            
            ExecutablePlan epTest = new ExecutablePlan(formulasTest, 1);

            Assert.Throws<IndexOutOfRangeException>(() => epTest.Replace(formulaReplaced, 1));
        }
        
        [Test]
        public void Replace_CompletedFormula_ThrowException()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);
            Formula[] formulasTest = { formulaTest };
            
            int input2 = 3;
            string[] inputMat2 = {"Water", "Powder", "Sugar"};
            int[] inputNum2 = {3, 3, 1};

            int output2 = 1;
            string[] outputMat2 = {"Cookie"};
            int[] outputNum2 = {1};

            Formula formulaReplaced = new Formula(inputMat2, inputNum2, input2, 
                outputMat2, outputNum2, output2);
            
            ExecutablePlan epTest = new ExecutablePlan(formulasTest, 1);

            epTest.ApplyCurrentStep();

            Assert.Throws<InvalidOperationException>(() => epTest.Replace(formulaReplaced, 0));
        }
    }
}