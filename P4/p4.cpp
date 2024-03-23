// AUTHOR:      Hongru He
// FILENAME:    p4.cpp
// DATE:        02/28/2024
// VERSION:     V1.0
// PLATFORM:    Windows
// PROCESS:     The p4.cpp driver is designed to showcase the capabilities of
//              the ExecutablePlan and Stockpile classes. It includes
//              demonstrations of: Testing move semantics and overloaded
//              apply function of ExecutablePlan objects. Testing Increase and
//              Decrease functions of Stockpile class. Testing overloaded
//              operators of Formula, Plan, ExecutablePlan classes.
//              Demonstrating dynamic memory management and object-oriented
//              principles in action.
// INTERFACE:   The driver executes a series of predefined tests that output
//              to the console. Output includes confirmation of successful
//              operations and the results of specific tests, such as the
//              state of plan objects after manipulation.
// ASSUMPTION:  The driver assumes that all the tested classes are
//              implemented correctly and that their public interfaces provide
//              the necessary operations for testing. It is assumed that the
//              environment provides stable and consistent results for memory
//              allocation and object lifecycle management.

#include <iostream>
#include <memory>
#include "formula.h"
#include "executableplan.h"
#include "stockpile.h"

using namespace std;

stockpile createStockpile1();
// Create a new stockpile with given parameters

stockpile createStockpile2();

formula createNewFormula1();
// Create a new formula with given parameters

formula createNewFormula2();

formula createNewFormula3();

formula* createNewFormulaArray1();
// Create a new formula array with given parameters

void testIncreaseSP();
// Test the Increase function of Stockpile

void testDecreaseSP();
// Test the Decrease function of Stockpile

void testOverloadedApply();
// Test the overloaded Apply function of ExecutablePlan

void testFormulaOverloadedRelationalOperator();
// Test the Formula's overloaded relational operator

void testPlanOverloadedRelationalOperator();
// Test the Plan's overloaded relational operator

void testEPOverloadedRelationalOperator();
// Test the ExecutablePlan's overloaded relational operator

void testOverloadedArithmeticOperator();
// Test all the overloaded arithmetic operators

void testEPCopyConstructor();
// Test the copy constructor of executable plan

void testEPOverloadedAssignment();
// Test the overloaded assignment operator of executable plan

void testEPMoveConstructor();
// Test the move constructor of executable plan

void testEPMoveAssignment();
// Test the move assignment operator of executable plan

int main() {

    testIncreaseSP();
    testDecreaseSP();
    testEPCopyConstructor();
    testEPOverloadedAssignment();
    testEPMoveConstructor();
    testEPMoveAssignment();
    testOverloadedApply();
    testFormulaOverloadedRelationalOperator();
    testPlanOverloadedRelationalOperator();
    testEPOverloadedRelationalOperator();
    testOverloadedArithmeticOperator();

    return 0;
}

stockpile createStockpile1() {
    string* resources = new string[5]{"Oxygen", "Hydrogen", "Powder",
                                      "Sugar", "Water"};
    double* quantity = new double[5]{59.0, 67.6, 17.1, 24.0,
                                     1.3};
    int number = 5;
    stockpile S1(resources, quantity, number);

    return S1;
}

stockpile createStockpile2() {
    string* resources = new string[3]{"Oxygen", "Hydrogen", "Grain"};
    double* quantity = new double[3]{59.0, 67.6, 1.3};
    int number = 3;
    stockpile S2(resources, quantity, number);

    return S2;
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

formula* createNewFormulaArray1() {
    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();

    formula* formulaArray1 = new formula[2]{F1, F2};

    return formulaArray1;
}

void testIncreaseSP() {
    cout << "\n----------TEST INCREASE FUNCTION----------\n";

    stockpile SP1 = createStockpile1();

    cout << "\nThe stockpile before increase includes resources:\n"
        << SP1.QueryResources();

    SP1.IncreaseResource("Powder", 5);

    cout << "\nThe stockpile after increase includes resources:\n"
        << SP1.QueryResources();
}

void testDecreaseSP() {
    cout << "\n----------TEST DECREASE FUNCTION----------\n";

    stockpile SP1 = createStockpile1();

    cout << "\n----------For the successful situation----------\n";
    cout << "\nThe stockpile before decrease includes resources:\n"
         << SP1.QueryResources();

    SP1.DecreaseResource("Powder", 5);

    cout << "\nThe stockpile after decrease includes resources:\n"
         << SP1.QueryResources();

    cout << "\n----------For the unsuccessful situation----------\n";
    cout << "\nThe stockpile before decrease includes resources:\n"
         << SP1.QueryResources();

    SP1.DecreaseResource("Powder", 100);
    SP1.DecreaseResource("Apple", 5);

    cout << "\nThe stockpile after decrease includes resources:\n"
         << SP1.QueryResources();
}

void testOverloadedApply() {
    cout << "\n----------TEST OVERLOADED APPLY FUNCTION----------\n";

    formula* inputFormulas = createNewFormulaArray1();

    executableplan EP1(inputFormulas, 2);

    shared_ptr<stockpile> Ptr1 = make_shared<stockpile>(createStockpile1());

    cout << "\nThe stockpile before overloaded Apply includes resources:\n"
        << Ptr1->QueryResources();

    try {
        shared_ptr<stockpile> Ptr2 = EP1.Apply(std::move(Ptr1));
        cout << "\nThe stockpile of results includes resources:\n"
             << Ptr2->QueryResources();
    }
    catch (const exception& e) {
        cout << "Exception caught: " << e.what() << endl;
    }

    shared_ptr<stockpile> Ptr3 = make_shared<stockpile>(createStockpile2());

    cout << "\nThe stockpile before overloaded Apply includes resources:\n"
         << Ptr3->QueryResources();

    try {
        shared_ptr<stockpile> Ptr4 = EP1.Apply(std::move(Ptr3));
        cout << "\nThe stockpile after overloaded Apply includes resources:\n"
             << Ptr4->QueryResources();
    }
    catch (const exception& e) {
        cout << "Exception caught: " << e.what() << endl;
    }
}

void testFormulaOverloadedRelationalOperator() {
    cout << "\n----------TEST FORMULA'S OVERLOADED RELATIONAL "
            "OPERATOR----------\n";

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula1();

    if (F1 == F3 && F1 != F2) {
        cout << "\nTest of formula's overloaded relational operator passed.\n";
    }
}

void testPlanOverloadedRelationalOperator() {
    cout << "\n----------TEST PLAN'S OVERLOADED RELATIONAL "
            "OPERATOR----------\n";

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    formula* inputFormulas1 = new formula[2]{F1, F2};
    formula* inputFormulas2 = new formula[3]{F1, F2, F3};

    plan P1(inputFormulas1, 2);
    plan P2(inputFormulas2, 3);
    plan P3(inputFormulas1, 2);

    if (P1 == P3 && P1 != P2 && P1 < P2) {
        cout << "\nTest of plan's overloaded relational operator passed.\n";
    }
}

void testEPOverloadedRelationalOperator() {
    cout << "\n----------TEST EXECUTABLE PLAN'S OVERLOADED RELATIONAL "
            "OPERATOR----------\n";

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    formula* inputFormulas3 = new formula[2]{F1, F2};
    formula* inputFormulas4 = new formula[3]{F1, F2, F3};

    executableplan EP1(inputFormulas3, 2);
    executableplan EP2(inputFormulas4, 3);
    executableplan EP3(inputFormulas3, 2);

    if (EP1 == EP3 && EP1 != EP2 && EP1 < EP2) {
        cout << "\nTest of executable plan's overloaded relational operator "
                "passed.\n";
    }
}

void testOverloadedArithmeticOperator() {
    cout << "\n----------TEST OVERLOADED ARITHMETIC OPERATOR----------\n";

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    formula* inputFormulas1 = new formula[2]{F1, F2};
    formula* inputFormulas2 = new formula[1]{F3};

    plan P1(inputFormulas1, 2);
    plan P2(inputFormulas2, 1);

    formula F7 = createNewFormula1();
    formula F8 = createNewFormula2();
    formula F9 = createNewFormula3();

    formula* inputFormulas3 = new formula[2]{F7, F8};
    formula* inputFormulas4 = new formula[1]{F9};

    executableplan EP1(inputFormulas3, 2);
    executableplan EP2(inputFormulas4, 1);

    cout << "\nThe two plans as operands are:\n" << P1.DisplayFormula()
        << "\n" << P2.DisplayFormula();

    plan P3 = P1 + P2;

    cout << "\nThe result plan is:\n" << P3.DisplayFormula();

    cout << "\nThe two executable plans as operands are:\n"
        << EP1.DisplayFormula() << "\n" << EP2.DisplayFormula();

    executableplan EP3 = EP1 + EP2;

    cout << "\nThe result executable plan is:\n" << EP3.DisplayFormula();
}

void testEPCopyConstructor() {
    cout << "\n----------TEST EXECUTABLE PLAN'S COPY CONSTRUCTOR----------\n";

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();

    executableplan EP1;

    EP1.Add(std::move(F1));
    EP1.Add(std::move(F2));

    executableplan EP2(EP1);

    cout << "The Formulas in first EP is:\n" << EP1.DisplayFormula() << endl;
    cout << "The Formulas in second EP is:\n" << EP2.DisplayFormula() << endl;
}

void testEPOverloadedAssignment() {
    cout << "\n----------TEST EXECUTABLE PLAN'S OVERLOADED "
            "ASSIGNMENT----------\n";

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    executableplan EP1, EP2;

    EP1.Add(std::move(F1));
    EP1.Add(std::move(F2));

    EP2.Add(std::move(F3));

    cout << "The Formulas in first EP is:\n" << EP1.DisplayFormula() << endl;
    cout << "The Formulas in second EP before copy is:\n"
         << EP2.DisplayFormula() << endl;

    EP2 = EP1;

    cout << "The Formulas in second EP after copy is:\n"
         << EP2.DisplayFormula() << endl;
}

void testEPMoveConstructor() {
    cout << "\n----------TEST EXECUTABLE PLAN'S MOVE CONSTRUCTOR----------\n";

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();

    executableplan EP1;

    EP1.Add(std::move(F1));
    EP1.Add(std::move(F2));

    cout << "The Formulas in first Plan is:\n" << EP1.DisplayFormula() << endl;

    cout << "Move first Plan to create second Plan." << endl;
    executableplan EP2(std::move(EP1));

    cout << "The Formulas in second Plan is:\n" << EP2.DisplayFormula() << endl;
}

void testEPMoveAssignment() {
    cout << "\n----------TEST EXECUTABLE PLAN'S MOVE ASSIGNMENT----------\n";

    formula F1 = createNewFormula1();
    formula F2 = createNewFormula2();
    formula F3 = createNewFormula3();

    executableplan EP1, EP2;

    EP1.Add(std::move(F1));
    EP1.Add(std::move(F2));

    EP2.Add(std::move(F3));

    cout << "The Formulas in first Plan is:\n" << EP1.DisplayFormula() << endl;
    cout << "The Formulas in second Plan before move assignment is:\n"
         << EP2.DisplayFormula() << endl;

    EP2 = std::move(EP1);

    cout << "The Formulas in second Plan after move assignment is:\n"
         << EP2.DisplayFormula() << endl;
}