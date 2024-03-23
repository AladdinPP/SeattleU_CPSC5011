// AUTHOR:      Hongru He
// FILENAME:    ExecutablePlan.cs
// DATE:        02/15/2024
// PURPOSE:     This class is to simulate a inherited plan - ExecutablePlan. Besides the base
//              functions (Add, Resize, DisplayFormula), the ExecutablePlan modifies Replace,
//              Remove, and DeepCopy functions with the unique currentStep attribute. It could
//              record which Formula is the next to be applied. The modified Replace and Remove
//              function adds a limit of no change of completed Formulas. The modified DeepCopy
//              function would deal with the unique attribute currentStep. Based on this new
//              attribute, there are getter function, ApplyCurrentStep function to apply the next
//              Formula, Reset function to let clients could reset the state of every Formula
//              after a full round of applying.

using System;
using System.Text;

namespace P3Test
{
    public class ExecutablePlan : Plan
    {
        // Class Invariants:
        // 1. currentStep could only be set at construction period or with Reset function, the
        //    clients will not be able to modify this attribute's value as they want.
        // 2. Execution state property (Formula's completed) accurately reflect the current
        //    state of execution.
        
        private int currentStep;
        
        // Default Constructor
        // Precondition:    None.
        // Postcondition:   Object is properly initialized with default values.
        public ExecutablePlan() : base()
        {
            currentStep = 0;
        }

        // Constructor
        // Precondition:    inputNum is non-negative.
        // Postcondition:   Object is properly initialized with planList, size, and capacity.
        public ExecutablePlan(Formula[] formulas, int inputN) : base(formulas, inputN)
        {
            currentStep = 0;
        }

        // DeepCopy
        // Precondition:    The ExecutablePlan is valid and non-null object.
        // Postcondition:   A deep copy of this instance is properly created with the same
        //                  values/content as the original one's.
        public override Plan DeepCopy()
        {
            Formula[] formulasCopy = new Formula[size];

            for (int i = 0; i < size; i++)
            {
                formulasCopy[i] = planList[i].DeepCopy();
            }

            ExecutablePlan epCopy = new ExecutablePlan(formulasCopy, size)
            {
                currentStep = this.currentStep
            };

            return epCopy;
        }
        
        // Clone
        // Precondition:    The ExecutablePlan is valid and non-null object.
        // Postcondition:   A deep copy of this instance is properly created with the same
        //                  values/content as the original one's.
        public ExecutablePlan Clone()
        {
            ExecutablePlan clonePlan = (ExecutablePlan)DeepCopy();
            
            return clonePlan;
        }
        
        // QueryCurrentStep
        // Precondition:    None.
        // Postcondition:   Returns a string containing content of the current step's Formula.
        public string QueryCurrentStep()
        {
            if (currentStep >= size)
            {
                throw new IndexOutOfRangeException("There is no uncompleted Formula.");
            }
            
            StringBuilder result = new StringBuilder();
            
            result.Append("The current Formula is:\n");
            result.Append(planList[currentStep].QueryInputMaterial());
            result.Append(planList[currentStep].QueryOutputMaterial());

            return result.ToString();
        }

        // ApplyCurrentStep
        // Precondition:    None.
        // Postcondition:   Returns a string containing the result of applying the current Formula.
        //                  If the currentStep is out of range, an exception is thrown.
        public string ApplyCurrentStep()
        {
            if (currentStep >= size)
            {
                throw new IndexOutOfRangeException("There is no more uncompleted Formula. " +
                                                   "You could choose to reset all Formulas.");
            }
            
            string result = planList[currentStep].Apply();

            currentStep++;
            return result;
        }

        // Reset
        // Precondition:    currentStep is out of range.
        // Postcondition:   All the Formula's completed state are reset, currentStep is reset.
        public void Reset()
        {
            if (currentStep >= size)
            {
                for (int i = 0; i < size; i++)
                {
                    planList[i].ResetCompleted();
                }

                currentStep = 0;
            }
        }

        // Replace
        // Precondition:    There is at least one non-empty Formula inside the planList.
        // Postcondition:   If the index is inside the range and the index is greater than
        //                  currentStep, the specific Formula is replaced with the newFor.
        //                  Otherwise, an exception will be thrown based on specific situation.
        public override void Replace(Formula newFor, int index)
        {
            if (index < currentStep)
            {
                throw new InvalidOperationException("Cannot replace the completed Formula.");
            }
            
            base.Replace(newFor, index);
        }

        // Remove
        // Precondition:    There is at least one non-empty Formula inside the planList.
        // Postcondition:   If the last Formula inside the planList is not completed, it
        //                  will be removed from it. Otherwise, an exception will be thrown.
        public override void Remove()
        {
            if (planList[size - 1].QueryCompleted())
            {
                throw new InvalidOperationException("Cannot remove the completed Formula.");
            }
            
            base.Remove();
        }
    }
}

// Implementation Invariants:
// 1. Methods altering the execution state must validate the currentStep within valid bounds
//    and ensure the execution state is consistent.
// 2. The DeepCopy override must include all ExecutablePlan specific properties, ensuring a
//    complete and independent copy.