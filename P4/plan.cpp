// AUTHOR:      Hongru He
// FILENAME:    plan.cpp
// DATE:        01/31/2024
// VERSION:     V1.0

#include "plan.h"
#include <stdexcept>

using namespace std;

// Implementation Invariants:
// 1.   The Add and Replace functions use move assignment operator of Formula
//      class to take over the ownership of the resources, ensuring the move
//      semantic. Remember to use std::move() to pass the rvalue of the
//      parameter. If the index parameter in Replace function out of the
//      range of the array, there should be error handling. Do use try-catch
//      to print the error message out.
// 2.   Move constructor and move assignment operator should release the
//      parameter's resource after 'this' Plan taking over the ownership
// 3.   The resize method maintains the integrity of the planList, ensuring
//      no formulas are lost during the resizing process.

// Default Constructor
// Initializes a Plan object with the default setting
plan::plan() {
    size = 0;
    capacity = 10;
    planList = new formula[capacity];
}

// Overloaded Constructor
// Initializes a Plan object with an array of initial formulas and size.
plan::plan(formula* formulaList, int inputNum) {
    size = inputNum;
    capacity = inputNum;
    planList = new formula[capacity];
    for (int i= 0; i < size; i++) {
        planList[i] = formulaList[i];
    }
}

// Deconstructor
// Destroys the Plan object and frees its resources.
plan::~plan() {
    delete[] planList;
    size = 0;
    capacity = 0;
}

// Copy Constructor
// Creates a new Plan object by copying another Plan object.
plan::plan(const plan& other) {
    size = other.size;
    capacity = other.capacity;
    planList = new formula[capacity];
    for (int i = 0; i < size; i++) {
        planList[i] = other.planList[i];
    }
}

// Copy Assignment Operator
// Deep copy the content of another Plan object to this one.
plan& plan::operator=(const plan& other) {
    if (this != &other) {
        delete[] planList;
        size = other.size;
        capacity = other.capacity;

        planList = new formula[capacity];
        for (int i = 0; i < size; i++) {
            planList[i] = other.planList[i];
        }
    }

    return *this;
}

// Comparison Operator
bool plan::operator==(const plan& other) {
    if (size != other.size)
        return false;

    for (int i = 0; i < size; i++) {
        if (!(planList[i] == other.planList[i]))
            return false;
    }
    return true;
}

// Overloaded Comparison Operator
bool plan::operator!=(const plan& other) {
    return !operator==(other);
}

// Move Constructor
// Creates a new Plan object by moving another Plan object.
plan::plan(plan&& other) noexcept {
    planList = other.planList;
    size = other.size;
    capacity = other.capacity;

    other.planList = nullptr;
    other.size = 0;
    other.capacity = 0;
}

// Move Assignment Operator
// Moves the content of another Plan object to this one.
plan& plan::operator=(plan&& other) noexcept {
    if (this != &other) {
        delete[] planList;
        planList = other.planList;
        size = other.size;
        capacity = other.capacity;

        other.planList = nullptr;
        other.size = 0;
        other.capacity = 0;
    }
    return *this;
}

// Overloaded Relational Operator
bool plan::operator>(const plan& other) {
    if (size > other.size) {
        return true;
    }
    return false;
}

// Overloaded Relational Operator
bool plan::operator<(const plan& other) {
    return !operator>(other);
}

// Overloaded Arithmetic Operator
plan& plan::operator+(plan& other) {
    for (int i = 0; i < other.size; i++) {
        this->Add(std::move(other.planList[i]));
    }
    return *this;
}

// Resize
// Expand the capacity of the Plan object when necessary.
void plan::Resize() {
    int newCapacity = capacity * 2;
    formula* newList = new formula[newCapacity];
    for (int i = 0; i < size; i++) {
        newList[i] = planList[i];
    }
    delete[] planList;

    // Deep copy from the newList
    planList = new formula[newCapacity];
    for (int j = 0; j < size; j++) {
        planList[j] = newList[j];
    }
    delete[] newList;
    capacity = newCapacity;
}

// Add
// Adds a new Formula to the end of the array of Formulas
void plan::Add(formula&& newFor) {
    if (size == capacity) {
        Resize();
    }

    planList[size++] = std::move(newFor);
}

// Remove
// Removes the last Formula in the array
void plan::Remove() {
    if (size > 0) {
        size--;
    }
}

// Replace
// Replaces the Formula at the given index
void plan::Replace(formula&& newFor, int index) {
    if (index < 0 || index >= size) {
        throw std::out_of_range("Index out of range.");
    }
    planList[index] = std::move(newFor);
}

// Display Formula
// Returns a string containing information about all the Formulas in the Plan
string plan::DisplayFormula() {
    stringstream ssr;
    for (int i = 0; i < size; i++) {
        ssr << "Formula " << i + 1 << ":\n";
        ssr << planList[i].QueryInput();
        ssr << planList[i].QueryOutput();
    }

    return ssr.str();
}