// AUTHOR:      Hongru He
// FILENAME:    Plan.cs
// DATE:        02/14/2024
// PURPOSE:     This class is to simulate a plan which contains various Formulas. It could add
//              new Formula into the array. It could remove the last Formula from the array.
//              It could replace the formula in the specific index with a given one. When the 
//              array is full, adding new Formula could cause resize of the array. It could
//              also display all the Formulas inside the array. It allows deep copy of itself.

using System;
using System.Text;

namespace P3Test
{
    public class Plan
    {
        // Class Invariants:
        // 1. The planList must not contain null values.
        // 2. The capacity must be correctly managed, ensuring no overflows or unnecessary
        //    memory consumption.
        
        protected Formula[] planList;
        protected int size;
        protected int capacity;

        // Resize
        // Precondition:    The size is equal to the capacity.
        // Postcondition:   The capacity is modified as two times before.
        private void Resize()
        {
            int newCapacity = capacity * 2;
            Formula[] newList = new Formula[newCapacity];
            for (int i = 0; i < size; i++) {
                newList[i] = planList[i];
            }

            // Deep copy from the newList
            planList = new Formula[newCapacity];
            for (int j = 0; j < size; j++) {
                planList[j] = newList[j];
            }
            capacity = newCapacity;
        }

        // Default Constructor
        // Precondition:    None.
        // Postcondition:   Object is properly initialized with default values.
        public Plan()
        {
            size = 0;
            capacity = 10;
            planList = new Formula[capacity];
        }

        // Constructor
        // Precondition:    inputNum is non-negative.
        // Postcondition:   Object is properly initialized with planList, size, and capacity.
        public Plan(Formula[] formulaList, int inputNum)
        {
            size = inputNum;
            capacity = inputNum;
            planList = formulaList;
        }

        // DeepCopy
        // Precondition:    The Plan is valid and non-null object.
        // Postcondition:   A deep copy of this Plan instance is properly created with the same
        //                  values/content as the original one's.
        public virtual Plan DeepCopy()
        {
            Plan copy = new Plan();

            for (int i = 0; i < this.size; i++)
            {
                copy.Add(this.planList[i].DeepCopy());
            }
            
            copy.capacity = this.capacity;

            return copy;
        }
        
        // Add
        // Precondition:    None.
        // Postcondition:   The Formula newFor is added into the planList. The planList will
        //                  be resized if necessary.
        public void Add(Formula newFor)
        {
            if (size == capacity)
            {
                Resize();
            }

            planList[size++] = newFor;
        }

        // Remove
        // Precondition:    There is at least one non-empty Formula inside the planList.
        // Postcondition:   The last Formula inside the planList is removed from it.
        public virtual void Remove()
        {
            if (size > 0)
            {
                size--;
            }
        }

        // Replace
        // Precondition:    There is at least one non-empty Formula inside the planList.
        // Postcondition:   If the index is inside the range, the specific Formula is replaced
        //                  with the newFor. Otherwise, an IndexOutOfRangeException will be thrown.
        public virtual void Replace(Formula newFor, int index)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Index out of range.");
            }

            planList[index] = newFor;
        }

        // DisplayFormula
        // Precondition:    There is at least one non-empty Formula inside the planList.
        // Postcondition:   Returns a string containing the content of all Formulas inside
        //                  the planList. 
        public string DisplayFormula()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                result.Append("Formula " + (i + 1) + ":\n");
                result.Append(planList[i].QueryInputMaterial());
                result.Append(planList[i].QueryOutputMaterial());
            }

            return result.ToString();
        }
    }
}

// Implementation Invariants:
// 1. Methods that modify planList (like Add, Remove, Replace) must check the validity of
//    their arguments and the state of planList after modifications.
// 2. The DeepCopy method must create a true deep copy, ensuring no shared references
//    between the original and the copied Plan.