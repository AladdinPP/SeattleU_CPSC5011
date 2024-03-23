// AUTHOR:      Hongru He
// FILENAME:    PlanTest.cs
// DATE:        02/15/2024

using System;
using NUnit.Framework;

namespace P3Test
{
    [TestFixture]
    public class PlanTest
    {
        [Test]
        public void Constructor_DefaultCreateInstance()
        {
            Plan planTest = new Plan();
            Assert.IsNotNull(planTest, "Expected non-null Plan instance.");
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
            Plan planTest = new Plan(formulasTest, 1);
            
            Assert.IsNotNull(planTest, "Expected non-null plan instance.");
        }
        
        [Test]
        public void DeepCopy_CreateInstinctCopy()
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

            Plan planTest = new Plan(formulasTest, 1);
            Plan copyTest = planTest.DeepCopy();
            
            Assert.IsNotNull(copyTest, "DeepCopy should not return null.");
            Assert.AreNotSame(planTest, copyTest, "DeepCopy should create a distinct object.");
        }

        [Test]
        public void Add_ValidFormula_ShouldAddFormula()
        {
            int input = 2;
            string[] inputMat = {"Oxygen", "Hydrogen"};
            int[] inputNum = {2, 1};

            int output = 1;
            string[] outputMat = {"Water"};
            int[] outputNum = {1};
            Formula formulaTest = new Formula(inputMat, inputNum, input,
                outputMat, outputNum, output);

            Plan planTest1 = new Plan();
            Plan planTest2 = new Plan();
            planTest1.Add(formulaTest);
            
            Assert.AreNotEqual(planTest1, planTest2, "Expected not containing the same content " +
                                                     "after adding.");
        }

        [Test]
        public void Remove_ValidOperation_ShouldRemoveFormula()
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
            
            Plan planTest1 = new Plan(formulasTest, 1);
            Plan planTest2 = new Plan(formulasTest, 1);
            
            planTest1.Remove();
            
            Assert.AreNotEqual(planTest1, planTest2, "Expected not containing the same content " + 
                                                     "after removing.");
        }

        [Test]
        public void Replace_ValidIndex_ShouldReplaceFormula()
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
            
            Plan planTest1 = new Plan(formulasTest, 1);
            Plan planTest2 = new Plan(formulasTest, 1);
            
            planTest1.Replace(formulaReplaced, 0);
            
            Assert.AreNotEqual(planTest1, planTest2, "Expected not containing the same content " +
                                                     "after replacement.");
        }

        [Test]
        public void Replace_InvalidIndex_ShouldThrowException()
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
            
            Plan planTest = new Plan(formulasTest, 1);

            Assert.Throws<IndexOutOfRangeException>(() => planTest.Replace(formulaReplaced, 1));
        }
    }
}