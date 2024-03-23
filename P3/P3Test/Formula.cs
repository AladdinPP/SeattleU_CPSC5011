// AUTHOR:      Hongru He
// FILENAME:    Formula.cs
// DATE:        02/13/2024
// PURPOSE:     This class is to simulate a formula which takes into input materials and returns
//              output materials. It will increase the proficiency experience each time it returns
//              output materials successfully. When the experience reach a specific value, the
//              proficiency level will increment. The proficiency level will affect the
//              probabilities of different output results. The boolean Completed is a control of
//              state to fulfill specific purpose in the ExecutablePlan class.

using System;
using System.Text;

namespace P3Test
{
    public class Formula
    {
        // Class Invariants:
        // 1. inputMaterial and outputMaterial should not contain empty strings. This ensures that
        //    formula always has valid values of input and output materials.
        // 2. inputNumber and outputNumber should be positive integers. They represent the
        //    quantities for the formula's inputs and outputs, and negative values are illogical.
        // 3. The sum of failure, partial, normal, and bonus should always be 100. These
        //    probabilities represent different outcome scenarios and their total should always
        //    be a complete probability distribution.
        // 4. experienceNum must always be non-negative and should not exceed maxExp. This
        //    represents the experience gained in applying the formula, bounded within a range.
        // 5. proficiencyLevel and experienceNum are private attributes to decide the probabilities
        //    together. They will be modified with multiple times of applying the formula, but
        //    client should not know them, so they and methods modifying them should be hided
        //    from client.
        // 6. maxExp and maxPro are the max limitation of proficiencyLevel and experienceNum, so
        //    they should be readonly attributes, unchangeable after construction. So as the 
        //    partialRate and bonusRate.
        // 7. All attributes are encapsulated for the safety of class members, so client can only
        //    access those members with public getters and could not modify any values directly.
        
        private string[] inputMaterial;
        private string[] outputMaterial;
        private int[] inputNumber;
        private int[] outputNumber;
        private int inputSize;
        private int outputSize;
        private int failure;
        private int partial;
        private int normal;
        private int bonus;
        private int proficiencyLevel;
        private int experienceNum;
        private bool completed;
        private readonly Random getRandomNumber = new Random();
        private readonly double partialRate = 0.75;
        private readonly double bonusRate = 1.1;
        private readonly int maxExp = 6;
        private readonly int maxPro = 2;

        // Default Constructor
        // Precondition:    None.
        // Postcondition:   Object is properly initialized with default values.
        public Formula()
        {
            inputMaterial = null;
            inputNumber = null;
            outputMaterial = null;
            outputNumber = null;
            inputSize = 0;
            outputSize = 0;
            failure = 0;
            partial = 0;
            normal = 0;
            bonus = 0;
            proficiencyLevel = 0;
            experienceNum = 0;
            completed = false;
        }
        
        // Constructor
        // Precondition:    inputM and outputM are non-empty strings; inputN and outputN are
        //                  positive integers.
        // Postcondition:   Object is properly initialized with inputMaterial, outputMaterial,
        //                  inputNumber, outputNumber, and default probabilities. Otherwise,
        //                  any invalid arguments could cause ArgumentException thrown.
        public Formula(string[] inputM, int[] inputN, int inputS, string[] outputM, int[] outputN,
            int outputS)
        {
            for (int i = 0; i < inputS; i++)
            {
                if (String.IsNullOrWhiteSpace(inputM[i]))
                {
                    throw new ArgumentException("You should give a valid input material.");
                }
            }

            for (int j = 0; j < outputS; j++)
            {
                if (String.IsNullOrWhiteSpace(outputM[j]))
                {
                    throw new ArgumentException("You should give a valid output material.");
                }
            }

            for (int i = 0; i < inputS; i++)
            {
                if (inputN[i] <= 0)
                {
                    throw new ArgumentException("You should give a positive input integer.");
                }
            }

            for (int j = 0; j < outputS; j++)
            {
                if (outputN[j] <= 0)
                {
                    throw new ArgumentException("You should give a positive output integer.");
                }
            }

            inputMaterial = inputM;
            outputMaterial = outputM;
            inputNumber = inputN;
            outputNumber = outputN;
            inputSize = inputS;
            outputSize = outputS;
            proficiencyLevel = 0;
            experienceNum = 0;
            failure = 30;
            partial = 25;
            normal = 42;
            bonus = 3;
            completed = false;
        }
        
        // DeepCopy
        // Precondition:    The Formula is valid and non-null instance
        // Postcondition:   A deep copy of this Formula instance is properly created with the same
        //                  values/content as the original one's.
        public Formula DeepCopy()
        {
            Formula copy = new Formula(this.inputMaterial, this.inputNumber, this.inputSize,
                this.outputMaterial, this.outputNumber, this.outputSize);

            copy.proficiencyLevel = this.proficiencyLevel;
            copy.experienceNum = this.experienceNum;
            copy.failure = this.failure;
            copy.partial = this.partial;
            copy.normal = this.normal;
            copy.bonus = this.bonus;
            copy.completed = this.completed;

            return copy;
        }
        
        // Get the inputMaterial
        // Precondition:    None.
        // Postcondition:   Returns a string of the name(s) and number(s) of inputMaterial.
        public string QueryInputMaterial()
        {
            StringBuilder result = new StringBuilder();
            
            for (int i = 0; i < inputSize; i++)
            {
                result.Append(inputNumber[i]);
                result.Append(" ");
                result.Append(inputMaterial[i]);
                result.Append("\n");
            }

            return result.ToString();
        }

        // Get the outputMaterial
        // Precondition:    None.
        // Postcondition:   Returns a string of the name(s) and number(s) of outputMaterial.
        public string QueryOutputMaterial()
        {
            StringBuilder result = new StringBuilder();
            
            for (int i = 0; i < outputSize; i++)
            {
                result.Append(outputNumber[i]);
                result.Append(" ");
                result.Append(outputMaterial[i]);
                result.Append("\n");
            }

            return result.ToString();
        }

        // QueryCompleted
        // Precondition:    None.
        // Postcondition:   Returns the state of the completed attribute.
        public bool QueryCompleted()
        {
            return completed;
        }
        
        // Increase the expNum
        // Precondition:    None.
        // Postcondition:   The expNum may be incremented.
        private void IncreaseExp()
        {
            if (experienceNum < maxExp)
            {
                experienceNum++;
                if (experienceNum == 1 / 3 * maxExp)
                {
                    IncreaseLevel();
                }
                else if (experienceNum == maxExp)
                {
                    IncreaseLevel();
                }
            }
        }
        
        // Increase the proficiencyLevel
        // Precondition:    None.
        // Postcondition:   proficiencyLevel could be incremented,
        //                  probabilities could be adjusted.
        private void IncreaseLevel()
        {
            if (proficiencyLevel < maxPro)
            {
                proficiencyLevel++;
                failure -= 5;
                partial -= 5;
                normal += 8;
                bonus += 2;
            }
        }

        // ResetCompleted
        // Precondition:    None.
        // Postcondition:   The completed attribute's value becomes 'false'.
        public void ResetCompleted()
        {
            completed = false;
        }
        
        // Simulate the application of the formula
        // Precondition:    None.
        // Postcondition:   Returns a string containing the content of the formula.
        public string Apply()
        {
            int proficiency = getRandomNumber.Next(1, 101);

            if (proficiency <= failure)
            {
                completed = true;
                return "Fail to produce.";
            }

            if (proficiency <= failure + partial)
            {
                StringBuilder result1 = new StringBuilder();

                for (int i = 0; i < outputSize; i++)
                {
                    result1.Append(outputNumber[i] * partialRate);
                    result1.Append(" ");
                    result1.Append(outputMaterial[i]);
                    result1.Append("\n");
                }
                
                IncreaseExp();
                completed = true;
                return result1.ToString();
            }

            if (proficiency <= failure + partial + normal)
            {
                StringBuilder result2 = new StringBuilder();

                for (int i = 0; i < outputSize; i++)
                {
                    result2.Append(outputNumber[i]);
                    result2.Append(" ");
                    result2.Append(outputMaterial[i]);
                    result2.Append("\n");
                }
                
                IncreaseExp();
                completed = true;
                return result2.ToString();
            }

            StringBuilder result3 = new StringBuilder();

            for (int i = 0; i < outputSize; i++)
            {
                result3.Append(outputNumber[i] * bonusRate);
                result3.Append(" ");
                result3.Append(outputMaterial[i]);
                result3.Append("\n");
            }
            
            IncreaseExp();
            completed = true;
            return result3.ToString();
        }
    }
}

// Implementation Invariants:
// 1. The IncreaseExp method is responsible for incrementing the expNum based on the formula's use.
//    The condition ensures that expNum is incremented consistently and within its bounds.
// 2. The IncreaseLevel method manages the proficiency level and adjusts the probabilities
//    (failure, partial, normal, bonus). It ensures that these probabilities are adjusted while
//    maintaining their sum at 100.
// 3. The experienceNum only increase when one applying of formula successfully returns an output,
//    so the Apply method only calls IncreaseExp method when it is not a failure.
// 4. IncreaseExp and IncreaseLevel are designed as private function to protect experienceNum and
//    proficiencyLevel, so client could affect their values by calling Apply method but could not
//    modify those private attributes directly. Client would not even realize the existence of
//    these class members. If there is future need to give client the access to those attributes,
//    or should make them being aware of them, two public getter methods could achieve that.