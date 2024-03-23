// AUTHOR:      Hongru He
// FILENAME:    p2.cpp
// DATE:        01/31/2024
// VERSION:     V1.0
// PLATFORM:    Windows
// PROCESS:     The p2.cpp driver is designed to showcase the capabilities of
//              the formula and plan classes. It includes demonstrations of:
//              Creating new formula objects using different constructors.
//              Testing the overloaded copy constructor, move constructor,
//              overloaded assignment operator, and move assignment for formula
//              and plan objects. Adding and managing formula objects within a
//              plan, demonstrating dynamic memory management and
//              object-oriented principles in action.
// INTERFACE:   The driver executes a series of predefined tests that output
//              to the console. Output includes confirmation of successful
//              operations and the results of specific tests, such as the
//              state of plan objects after manipulation.
// ASSUMPTION:  The driver assumes that the formula and plan classes are
//              implemented correctly and that their public interfaces provide
//              the necessary operations for testing. It is assumed that the
//              environment provides stable and consistent results for memory
//              allocation and object lifecycle management.

#include "formula.h"
#include "plan.h"
#include <iostream>
#include <string>
#include <stdexcept>

using namespace std;

formula createNewFormula1();
// Create a new formula with given parameters

formula createNewFormula2();

formula createNewFormula3();

void testOverloadedConstructor();
// Test the overloaded constructor of plan

void testCopyConstructor();
// Test the copy constructor of plan

void testOverloadedAssignment();
// Test the overloaded assignment operator of plan

void testMoveConstructor();
// Test the move constructor of plan

void testMoveAssignment();
// Test the move assignment operator of plan

void testAdd();
// Test the add function of plan

void testRemove();
// Test the remove function of plan

void testReplaceProperly();
// Test the replace function with proper parameters

void testReplaceOutOfRange();
// Test the error handling of the replace function

int main() {
    testOverloadedConstructor();
    testCopyConstructor();
    testOverloadedAssignment();
    testMoveConstructor();
    testMoveAssignment();
    testAdd();
    testRemove();
    testReplaceProperly();
    testReplaceOutOfRange();

    return 0;
}

formula createNewFormula1() {
    int input1 = 2;
    string* inputMat1 = new string[2]{"Oxygen", "Hydrogen"};
    int* inputNum1 = new int[2]{2, 1};

    int output1 = 1;
    string* outputMat1 = new string[1]{"Water"};
    int* outputNum1 = new int[1]{1};

    formula F1(inputMat1, inputNum1, input1, outputMat1, outputNum1, output1);

    return F1;
}

formula createNewFormula2() {
    int input2 = 3;
    string* inputMat2 = new string[3]{"Water", "Powder", "Sugar"};
    int* inputNum2 = new int[3]{3, 3, 1};

    int output2 = 1;
    string* outputMat2 = new string[1]{"Cookie"};
    int* outputNum2 = new int[1]{1};

    formula F2(inputMat2, inputNum2, input2, outputMat2, outputNum2, output2);

    return F2;
}

formula createNewFormula3() {
    int input3 = 1;
    string* inputMat3 = new string[1]{"Grain"};
    int* inputNum3 = new int[1]{1};

    int output3 = 1;
    string* outputMat3 = new string[1]{"Rice"};
    int* outputNum3 = new int[1]{1};

    formula F3(inputMat3, inputNum3, input3, outputMat3, outputNum3, output3);

    return F3;
}

void testOverloadedConstructor() {
    cout << "----------TEST OVERLOADED CONSTRUCTOR----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    formula* formulaList = new formula[3]{F1, F2, F3};

    plan P1(formulaList, 3);

    cout << "The Formulas in the Plan is:\n" << P1.DisplayFormula() << endl;
}

void testCopyConstructor() {
    cout << "-----------TEST COPY CONSTRUCTOR----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();

    plan P1;

    P1.Add(std::move(F1));
    P1.Add(std::move(F2));

    plan P2(P1);

    cout << "The Formulas in first Plan is:\n" << P1.DisplayFormula() << endl;
    cout << "The Formulas in second Plan is:\n" << P2.DisplayFormula() << endl;
}

void testOverloadedAssignment() {
    cout << "-----------TEST OVERLOADED ASSIGNMENT----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    plan P1, P2;

    P1.Add(std::move(F1));
    P1.Add(std::move(F2));

    P2.Add(std::move(F3));

    cout << "The Formulas in first Plan is:\n" << P1.DisplayFormula() << endl;
    cout << "The Formulas in second Plan before copy is:\n"
         << P2.DisplayFormula() << endl;

    P2 = P1;

    cout << "The Formulas in second Plan after copy is:\n"
         << P2.DisplayFormula() << endl;
}

void testMoveConstructor() {
    cout << "-----------TEST MOVE CONSTRUCTOR----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();

    plan P1;

    P1.Add(std::move(F1));
    P1.Add(std::move(F2));

    cout << "The Formulas in first Plan is:\n" << P1.DisplayFormula() << endl;

    cout << "Move first Plan to create second Plan." << endl;
    plan P2(std::move(P1));

    cout << "The Formulas in second Plan is:\n" << P2.DisplayFormula() << endl;
}

void testMoveAssignment() {
    cout << "-----------TEST MOVE ASSIGNMENT----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    plan P1, P2;

    P1.Add(std::move(F1));
    P1.Add(std::move(F2));

    P2.Add(std::move(F3));

    cout << "The Formulas in first Plan is:\n" << P1.DisplayFormula() << endl;
    cout << "The Formulas in second Plan before move assignment is:\n"
         << P2.DisplayFormula() << endl;

    P2 = std::move(P1);

    cout << "The Formulas in second Plan after move assignment is:\n"
         << P2.DisplayFormula() << endl;
}

void testAdd() {
    cout << "-----------TEST ADD FUNCTION----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    formula* formulaList = new formula[2]{F1, F2};

    plan P1(formulaList, 2);

    cout << "The original Plan has Formulas:\n" << P1.DisplayFormula() << endl;

    P1.Add(std::move(F3));

    cout << "The Formulas in the Plan after Add function:\n"
         << P1.DisplayFormula() << endl;
}

void testRemove() {
    cout << "-----------TEST REMOVE FUNCTION----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    formula* formulaList = new formula[3]{F1, F2, F3};

    plan P1(formulaList, 3);

    cout << "The original Plan has Formulas:\n" << P1.DisplayFormula() << endl;

    P1.Remove();

    cout << "The Formulas in the Plan after Remove function:\n"
         << P1.DisplayFormula() << endl;
}

void testReplaceProperly() {
    cout << "-----------TEST REPLACE FUNCTION----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    formula* formulaList = new formula[2]{F1, F2};

    plan P1(formulaList, 2);

    cout << "The original Plan has Formulas:\n" << P1.DisplayFormula() << endl;

    cout << "Try to replace the second Formula in the Plan." << endl;

    P1.Replace(std::move(F3), 1);

    cout << "The Formulas in the Plan after Replace function:\n"
         << P1.DisplayFormula() << endl;
}

void testReplaceOutOfRange() {
    cout << "-----------TEST REPLACE FUNCTION ERROR HANDLING----------" << endl;

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    formula* formulaList = new formula[2]{F1, F2};

    plan P1(formulaList, 2);

    cout << "The original Plan has Formulas:\n" << P1.DisplayFormula() << endl;

    cout << "Try to replace the fifth Formula (out of range) in the Plan."
         << endl;

    try {
        P1.Replace(std::move(F3), 4);
    }
    catch (const std::out_of_range& e) {
        cout << "Error occurred: " << e.what() << endl;
    }
}