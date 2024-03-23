// AUTHOR:      Hongru He
// FILENAME:    executableplan.cpp
// DATE:        02/28/2024
// VERSION:     V1.0

#include "executableplan.h"
#include "stockpile.h"
#include <iostream>
#include <memory>
#include <sstream>
#include <stdexcept>

using namespace std;

// Implementation Invariants:
// 1.   This class ensures actions are stored and executed in a deterministic
//      order.
// 2.   It provides mechanisms to add, remove, or modify actions within the
//      plan, maintaining the integrity and feasibility of the executable
//      sequence.
// 3.   The class must ensure that any dependencies between actions are
//      resolved before execution, guaranteeing that the plan is viable at
//      any point.

// Default Constructor
executableplan::executableplan() : plan() {
    currentStep = 0;
}

// Overloaded Constructor
executableplan::executableplan(formula * formulaList, int inputNum) :
plan(formulaList, inputNum) {
    currentStep = 0;
}

// Destructor
executableplan::~executableplan() {
    currentStep = 0;
}

// Copy Constructor
executableplan::executableplan(const executableplan& other) : plan(other) {
    currentStep = other.currentStep;
}

// Move Constructor
executableplan::executableplan(executableplan&& other) noexcept :
plan(std::move(other)) {
    currentStep = other.currentStep;
    other.currentStep = 0;
}

// Overloaded Assignment Operator
executableplan& executableplan::operator=(const executableplan& other) {
    if (this != &other) {
        plan::operator=(other);
        currentStep = other.currentStep;
    }
    return *this;
}

// Move Assignment Operator
executableplan& executableplan::operator=(executableplan&& other) noexcept {
    if (this != &other) {
        plan::operator=(std::move(other));
        currentStep = other.currentStep;
        other.currentStep = 0;
    }

    return *this;
}

// Overloaded Comparison Operator
bool executableplan::operator==(const executableplan& other) {
    if (plan::operator==(other)) {
        return currentStep == other.currentStep;
    }
    return false;
}

// Overloaded Comparison Operator
bool executableplan::operator!=(const executableplan& other) {
    if (currentStep != other.currentStep) {
        return true;
    }
    return plan::operator!=(other);
}

// Overloaded Arithmetic Operator
executableplan& executableplan::operator+(executableplan& other) {
    if (other.currentStep > 0) {
        other.Reset();
    }
    plan::operator+(other);
    return *this;
}

// Overloaded Relational Operator
bool executableplan::operator>(const executableplan& other) {
    if (plan::operator==(other)) {
        return currentStep > other.currentStep;
    }
    return plan::operator>(other);
}

// Overloaded Relational Operator
bool executableplan::operator<(const executableplan& other) {
    if (plan::operator==(other)) {
        return currentStep < other.currentStep;
    }
    return plan::operator<(other);
}

// Get the current step
string executableplan::QueryCurrentStep() {
    if (currentStep >= size) {
        throw std::out_of_range("There is no uncompleted formulas.");
    }

    stringstream result;
    result << "The current formula is:\n";
    result << planList[currentStep].QueryInput();
    result << planList[currentStep].QueryOutput();

    return result.str();
}

// Apply the current step's formula
string executableplan::ApplyCurrentStep() {
    if (currentStep >= size) {
        throw std::out_of_range("There is no uncompleted formulas. You could "
                                "choose to reset all formulas.");
    }

    string result = planList[currentStep].Apply();

    currentStep++;
    return result;
}

// Extract output quantities from the result string of Formula's Apply function
double* executableplan::ExtractOutputNumber(const string& resultStr, int size) {
    double* outputResult = new double[size];
    stringstream extractLine(resultStr);
    string resultLine;
    int i = 0;

    while (getline(extractLine, resultLine)) {
        stringstream extractNum(resultLine);
        double resultNum;
        extractNum >> resultNum;
        outputResult[i] = resultNum;
        i++;
    }

    return outputResult;
}

// Overloaded apply taking the smart pointer of a stockpile
shared_ptr<stockpile> executableplan::Apply(shared_ptr<stockpile> inputPtr) {
    if (currentStep >= size) {
        throw runtime_error("No more formulas to apply.");
    }

    bool resourcesAvailable = true;
    formula& currentFormula = planList[currentStep];

    for (int i = 0; i < this->size; ++i) {
        string resourceName = currentFormula.QueryInputMaterial(i);
        int requiredQuantity = currentFormula.QueryInputNumber(i);
        if (!inputPtr->CheckMaterial(resourceName, requiredQuantity)) {
            resourcesAvailable = false;
            break;
        }
    }

    if (!resourcesAvailable) {
        throw runtime_error("Insufficient resources to apply formula.");
    }

    string result = planList[currentStep].Apply();
    int resultSize = planList[currentStep].QueryOutputSize();

    double* outputNumber = ExtractOutputNumber(result, resultSize);
    string* outputMaterial = new string[resultSize];
    for (int j = 0; j < resultSize; j++) {
        outputMaterial[j] = planList[currentStep].QueryOutputMaterial(j);
    }

    for (int k = 0; k < resultSize; k++) {
        inputPtr->IncreaseResource(outputMaterial[k], outputNumber[k]);
    }

    currentStep++;

    return inputPtr;
}

// Reset all formulas
void executableplan::Reset() {
    if (currentStep >= size) {
        for (int i = 0; i < size; i++) {
            planList[i].ResetCompleted();
        }

        currentStep = 0;
    }
}

// Replace the formula in a specific index
void executableplan::Replace(formula && newFor, int index) {
    if (index < currentStep) {
        throw std::out_of_range("Cannot replace the completed formula.");
    }

    plan::Replace(std::move(newFor), index);
}

// Remove the last formula from the plan list
void executableplan::Remove() {
    if (planList[size - 1].QueryCompleted()) {
        throw std::out_of_range("Cannot remove the completed formula.");
    }

    plan::Remove();
}