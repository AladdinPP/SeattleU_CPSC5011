// AUTHOR:      Hongru He
// FILENAME:    executableplan.h
// DATE:        01/31/2024
// VERSION:     V1.0

#ifndef P4_EXECUTABLEPLAN_H
#define P4_EXECUTABLEPLAN_H
#include "plan.h"
#include "stockpile.h"
#include <memory>

using namespace std;

// The ExecutablePlan class simulates an executable plan consisting of multiple
// formulas.
// Class Invariants:
// 1.   It maintains an ordered collection of actions, where each action might
//      depend on the completion of previous actions.
// 2.   The object ensures that actions are executable in the sequence they
//      are added unless explicitly modified.

class executableplan : public plan {
private:
    int currentStep;

    double* ExtractOutputNumber(const string&, int);

public:
    executableplan();
    // Default Constructor

    executableplan(formula*, int);
    // Overloaded Constructor

    ~executableplan();
    // Destructor

    executableplan(const executableplan&);
    // Copy Constructor

    executableplan(executableplan&&) noexcept;
    // Move Constructor

    executableplan& operator=(const executableplan&);
    // Overloaded Assignment Operator

    executableplan& operator=(executableplan&&) noexcept;
    // Move Assignment Operator

    bool operator==(const executableplan&);
    // Overloaded Relational Operator

    bool operator!=(const executableplan&);
    // Overloaded Relational Operator

    executableplan& operator+(executableplan&);
    // Overloaded Arithmetic Operator

    bool operator>(const executableplan&);
    // Overloaded Relational Operator

    bool operator<(const executableplan&);
    // Overloaded Relational Operator

    string QueryCurrentStep();
    // Get the current step

    string ApplyCurrentStep();
    // Apply the current step's formula

    shared_ptr<stockpile> Apply(shared_ptr<stockpile>);
    // Overloaded apply taking the smart pointer of a stockpile

    void Reset();
    // Reset current step and all the formulas' states

    void Replace(formula&&, int) override;
    // Replace a formula at a specific index

    void Remove() override;
    // Remove the last formula if it is not completed

};


#endif //P4_EXECUTABLEPLAN_H
