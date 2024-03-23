// AUTHOR:      Hongru He
// FILENAME:    P3.cs
// DATE:        02/15/2024
// VERSION:     V1.0
// PLATFORM:    Windows
// PROCESS:     The P3.cpp driver is designed to showcase the capabilities of
//              the Plan and ExecutablePlan classes. It includes demonstrations of:
//              Creating a heterogeneous collection of instances of both classes.
//              Testing the same Add() function for both base and derived instance.
//              Testing the modified Remove() and Replace() functions for derived
//              instance, with the comparison of the base functions.
// INTERFACE:   The driver executes a series of predefined tests that output
//              to the console. Output includes confirmation of successful
//              operations and the results of specific tests, such as the
//              state of Plan and ExecutablePlan objects after manipulation.
// ASSUMPTION:  The driver assumes that the Plan and ExecutablePlan classes are
//              implemented correctly and that their public interfaces provide
//              the necessary operations for testing. It is assumed that the
//              environment provides stable and consistent results for memory
//              allocation and object lifecycle management.

using System;
using P3Test;

namespace P3Driver
{
    internal class P3
    {
        public static void Main(string[] args)
        {
            Formula F1 = createNewFormula1();
            Formula F2 = createNewFormula2();
            Formula F3 = createNewFormula3();

            Formula[] initialFormulas = { F1, F2 };
            Plan P1 = new Plan(initialFormulas, 2);
            ExecutablePlan P2 = new ExecutablePlan(initialFormulas, 2);

            Plan[] testCollection = { P1, P2 };
            
            testAdd(testCollection, 2, F3);

            Console.WriteLine("--------------------\nBefore testing Replace and Remove, making " +
                              "every Formulas completed.\n--------------------\n");
            
            if (testCollection[1] is ExecutablePlan ep1)
            {
                ep1.ApplyCurrentStep();
                ep1.ApplyCurrentStep();
                ep1.ApplyCurrentStep();
            }
            
            testReplace(testCollection, 2, F1);
            testRemove(testCollection, 2);
        }
        
        // Create a new formula with given parameters
        private static Formula createNewFormula1()
        {
            int input1 = 2;
            string[] inputMat1 = {"Oxygen", "Hydrogen"};
            int[] inputNum1 = {2, 1};

            int output1 = 1;
            string[] outputMat1 = {"Water"};
            int[] outputNum1 = {1};

            Formula F1 = new Formula(inputMat1, inputNum1, input1, 
                outputMat1, outputNum1, output1);

            return F1;
        }

        // Create a new formula with given parameters
        private static Formula createNewFormula2()
        {
            int input2 = 3;
            string[] inputMat2 = {"Water", "Powder", "Sugar"};
            int[] inputNum2 = {3, 3, 1};

            int output2 = 1;
            string[] outputMat2 = {"Cookie"};
            int[] outputNum2 = {1};

            Formula F2 = new Formula(inputMat2, inputNum2, input2, 
                outputMat2, outputNum2, output2);

            return F2;
        }

        // Create a new formula with given parameters
        private static Formula createNewFormula3()
        {
            int input3 = 1;
            string[] inputMat3 = {"Grain"};
            int[] inputNum3 = {1};

            int output3 = 1;
            string[] outputMat3 = {"Rice"};
            int[] outputNum3 = {1};

            Formula F3 = new Formula(inputMat3, inputNum3, input3, 
                outputMat3, outputNum3, output3);

            return F3;
        }

        // Test the same Add function of both Plan and ExecutablePlan objects
        private static void testAdd(Plan[] test, int size, Formula addedFor)
        {
            Console.WriteLine("----------TEST ADD FUNCTION----------\n");
            
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("The " + (i + 1) + " Plan before adding:\n");
                Console.WriteLine(test[i].DisplayFormula());
            }

            Console.WriteLine("Execute add function.\n");
            
            for (int i = 0; i < size; i++)
            {
                test[i].Add(addedFor);
            }

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("The " + (i + 1) + " Plan after adding:\n");
                Console.WriteLine((test[i].DisplayFormula()));
            }
        }

        // Test the modified Replace function of ExecutablePlan object, comparing with
        // the base Replace function of Plan object
        private static void testReplace(Plan[] test, int size, Formula replace)
        {
            Console.WriteLine("----------TEST REPLACE FUNCTION----------\n");
            
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("The " + (i + 1) + " Plan before replacement:\n");
                Console.WriteLine(test[i].DisplayFormula());
            }
            
            Console.WriteLine("Execute replace function.\n");
            
            for (int i = 0; i < size; i++)
            {
                try
                {
                    test[i].Replace(replace, 2);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("For the Plan " + (i + 1) + ":\n" + e.Message + "\n");
                }
            }

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("The " + (i + 1) + " Plan after replacement:\n");
                Console.WriteLine((test[i].DisplayFormula()));
            }
        }

        // Test the modified Remove function of ExecutablePlan object, comparing with
        // the base Remove function of Plan object
        private static void testRemove(Plan[] test, int size)
        {
            Console.WriteLine("----------TEST REMOVE FUNCTION----------\n");
            
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("The " + (i + 1) + " Plan before remove:\n");
                Console.WriteLine(test[i].DisplayFormula());
            }
            
            Console.WriteLine("Execute remove function.\n");
            
            for (int i = 0; i < size; i++)
            {
                try
                {
                    test[i].Remove();
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("For the Plan " + (i + 1) + ":\n" + e.Message + "\n");
                }
            }

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("The " + (i + 1) + " Plan after remove:\n");
                Console.WriteLine((test[i].DisplayFormula()));
            }
        }
    }
}