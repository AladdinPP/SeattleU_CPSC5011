// AUTHOR:      Hongru He
// FILENAME:    plan.h
// DATE:        01/31/2024
// VERSION:     V1.0

#ifndef P4_PLAN_H
#define P4_PLAN_H
#include "formula.h"
#include <iostream>
#include <memory>

using namespace std;

class plan {
protected:
    formula* planList;
    int size, capacity;

    // Resize the Plan when necessary.
    // Explanation:     Resizes the Plan's capacity if the current size
    //                  reaches the capacity.
    // Precondition:    None.
    // Postcondition:   If necessary, increases the Plan's capacity to
    //                  accommodate more Formulas.
    void Resize();

public:
    plan();
    // Default Constructor
    // Explanation:     Initializes a Plan with an empty array of formulas.
    // Precondition:    None.
    // Postcondition:   Plan object is initialized with default setting.

    plan(formula*, int);
    // Overloaded Constructor
    // Explanation:     Initializes a Plan with a set of initial formulas.
    // Precondition:    the parameter formula* points to an array of Formulas,
    //                  the parameter int is the number of elements.
    // Postcondition:   Plan object is initialized with the given formulas.

    ~plan();
    // Destructor
    // Explanation:     Destroys the Plan object and releases allocated
    //                  resources.
    // Precondition:    None.
    // Postcondition:   The Plan object is properly cleaned up.

    plan(const plan&);
    // Copy Constructor
    // Explanation:     Creates a new Plan as a copy of another Plan.
    // Precondition:    the parameter is a valid, existing Plan object.
    // Postcondition:   A new Plan object is created as a copy of the given one.

    plan& operator=(const plan&);
    // Overloaded Assignment Operator
    // Explanation:     Assigns one Plan to another using copy semantics.
    // Precondition:    the parameter is a valid, existing Plan object.
    // Postcondition:   The original Plan is a copy of the given one.

    bool operator==(const plan&);
    // Overloaded Relational Operator

    bool operator!=(const plan&);
    // Overloaded Relational Operator

    plan(plan&&) noexcept;
    // Move Constructor
    // Explanation:     Moves an existing Plan to a new Plan.
    // Precondition:    the parameter is a valid, existing Plan object.
    // Postcondition:   The new Plan takes ownership of parameter's resources.

    plan& operator=(plan&&) noexcept;
    // Move Assignment Operator
    // Explanation:     Assigns one Plan to another using move semantics.
    // Precondition:    the parameter is a valid, existing Plan object.
    // Postcondition:   The original Plan takes ownership of parameter's
    //                  resources.

    bool operator>(const plan&);
    // Overloaded Relational Operator

    bool operator<(const plan&);
    // Overloaded Relational Operator

    plan& operator+(plan&);
    // Overloaded Arithmetic Operator

    void Add(formula&&);
    // Add a new Formula to the Plan.
    // Explanation:     Adds a new Formula to the Plan.
    // Precondition:    the parameter is a valid, constructed Formula object.
    // Postcondition:   The new formula is added to the Plan.

    virtual void Remove();
    // Remove the last Formula from the Plan.
    // Explanation:     Removes the last Formula from the Plan.
    // Precondition:    The Plan contains at least one Formula.
    // Postcondition:   The last Formula is removed from the Plan.

    virtual void Replace(formula&&, int);
    // Replace a Formula at a specific index.
    // Explanation:     Replaces a Formula in the Plan at the specified index
    //                  with a new Formula.
    // Precondition:    the parameter int is within the range of the Plan's
    //                  size.
    // Postcondition:   The Formula at 'index' is replaced with 'formula'.

    string DisplayFormula();
    // Display the Formulas in the Plan.
    // Explanation:     Returns a string representation of all formulas
    //                  in the Plan.
    // Precondition:    None.
    // Postcondition:   Returns a string without modifying the Plan.
};


#endif //P4_PLAN_H
