// AUTHOR:      Hongru He
// FILENAME:    Formula.cs
// DATE:        01/12/2024
// PURPOSE:     This class is to simulate a formula which takes into input materials and returns
//              output materials. It will increase the proficiency experience each time it returns
//              output materials successfully. When the experience reach a specific value, the
//              proficiency level will increment. The proficiency level will affect the
//              probabilities of different output results.

using System;

namespace P1
{
    public class Formula
    {
        // Class Invariants:
        // 1. inputMaterial and outputMaterial should not be empty strings. This ensures that the
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
        //    they should be static class attributes, shared by all Formula instances. But they
        //    should be modified only by the class designer, so they should be private.
        // 7. All attributes are encapsulated for the safety of class members, so client can only
        //    access those members with public getters and could not modify any values directly.
        
        private string inputMaterial;
        private string outputMaterial;
        private int inputNumber;
        private int outputNumber;
        private int failure;
        private int partial;
        private int normal;
        private int bonus;
        private int proficiencyLevel;
        private int experienceNum;
        private readonly Random getRandomNumber = new Random();
        
        private static int maxExp = 6;
        private static int maxPro = 2;

        // Constructor
        // Precondition:    inputM and outputM are non-empty strings; inputN and outputN are
        //                  positive integers.
        // Postcondition:   Object is properly initialized with inputMaterial, outputMaterial,
        //                  inputNumber, outputNumber, and default probabilities. Otherwise,
        //                  any invalid arguments could cause ArgumentException thrown.
        public Formula(int inputN, string inputM, int outputN, string outputM)
        {
            if (String.IsNullOrWhiteSpace(inputM))
            {
                throw new ArgumentException("You should give a valid input material.");
            }

            if (String.IsNullOrWhiteSpace(outputM))
            {
                throw new ArgumentException("You should give a valid output material.");
            }

            if (inputN <= 0)
            {
                throw new ArgumentException("You should give a positive input integer.");
            }
            
            if (outputN <= 0)
            {
                throw new ArgumentException("You should give a positive output integer.");
            }
            
            inputMaterial = inputM;
            outputMaterial = outputM;
            inputNumber = inputN;
            outputNumber = outputN;
            proficiencyLevel = 0;
            experienceNum = 0;
            failure = 30;
            partial = 25;
            normal = 42;
            bonus = 3;
        }

        // Get the inputMaterial
        // Precondition:    None.
        // Postcondition:   Returns the inputMaterial value.
        public string QueryInputMaterial()
        {
            return inputMaterial;
        }

        // Get the inputNumber
        // Precondition:    None.
        // Postcondition:   Returns the inputNumber value.
        public int QueryInputNumber()
        {
            return inputNumber;
        }

        // Get the outputMaterial
        // Precondition:    None.
        // Postcondition:   Returns the outputMaterial value.
        public string QueryOutputMaterial()
        {
            return outputMaterial;
        }

        // Get the outputNumber
        // Precondition:    None.
        // Postcondition:   Returns the outputNumber value.
        public int QueryOutputNumber()
        {
            return outputNumber;
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

        // Simulate the application of the formula
        // Precondition:    materialNum is a non-negative integer and a
        //                  multiple of inputNumber.
        // Postcondition:   Returns output quantity based on probabilities;
        //                  If materialNum is not a multiple of inputNumber,
        //                  ArgumentException thrown.
        public int Apply(int materialNum)
        {
            if (materialNum % inputNumber != 0)
            {
                throw new ArgumentException("Your input is not valid.");
            }

            int proficiency = getRandomNumber.Next(1, 101);

            if (proficiency <= failure)
            {
                return 0;
            }

            if (proficiency <= failure + partial)
            {
                IncreaseExp();
                return outputNumber * (materialNum / inputNumber) - 1;
            }

            if (proficiency <= failure + partial + normal)
            {
                IncreaseExp();
                return outputNumber * (materialNum / inputNumber);
            }

            IncreaseExp();
            return outputNumber * (materialNum / inputNumber) + 1;
        }
    }
}

// Implementation Invariants:
// 1. The IncreaseExp method is responsible for incrementing the expNum based on the formula's use.
//    The condition ensures that expNum is incremented consistently and within its bounds.
// 2. The IncreaseLevel method manages the proficiency level and adjusts the probabilities
//    (failure, partial, normal, bonus). It ensures that these probabilities are adjusted while
//    maintaining their sum at 100.
// 3. The output of Apply method should always be integers. So the argument of Apply method should
//    always be a multiple of the inputNumber. This should be told in the device interface, but in
//    case client does not follow the interface, an ArgumentException is needed.
// 4. The experienceNum only increase when one applying of formula successfully returns an output,
//    so the Apply method only calls IncreaseExp method when it is not a failure.
// 5. IncreaseExp and IncreaseLevel are designed as private function to protect experienceNum and
//    proficiencyLevel, so client could affect their values by calling Apply method but could not
//    modify those private attributes directly. Client would not even realize the existence of
//    these class members. If there is future need to give client the access to those attributes,
//    or should make them being aware of them, two public getter methods could achieve that.