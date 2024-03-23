// AUTHOR:      Hongru He
// FILENAME:    formula.cpp
// DATE:        01/31/2024
// VERSION:     V1.0

#include "formula.h"
#include <iostream>

using namespace std;

// Default Constructor
formula::formula() {
    inputMaterial = nullptr;
    inputNumber = nullptr;
    outputMaterial = nullptr;
    outputNumber = nullptr;

    inputSize = 0;
    outputSize = 0;
    failure = 0;
    partial = 0;
    normal = 0;
    bonus = 0;
    proficiencyLevel = 0;
    experienceNum = 0;
}

// Overloaded Constructor
formula::formula(string* inputM, int* inputN, int input, string* outputM,
                 int* outputN, int output) {
    inputMaterial = inputM;
    inputNumber = inputN;
    outputMaterial = outputM;
    outputNumber = outputN;


    inputSize = input;
    outputSize = output;

    proficiencyLevel = 0;
    experienceNum = 0;
    failure = 30;
    partial = 25;
    normal = 42;
    bonus = 3;
}

// Deconstructor
formula::~formula() {
    delete[] inputMaterial;
    delete[] inputNumber;
    delete[] outputMaterial;
    delete[] outputNumber;
}

// Copy Constructor
formula::formula(const formula& other) {
    inputMaterial = other.inputMaterial;
    inputNumber = other.inputNumber;
    outputMaterial = other.outputMaterial;
    outputNumber = other.outputNumber;

    inputSize = other.inputSize;
    outputSize = other.outputSize;

    proficiencyLevel = other.proficiencyLevel;
    experienceNum = other.experienceNum;
    failure = other.failure;
    partial = other.partial;
    normal = other.normal;
    bonus = other.bonus;
}

// Overloaded Assignment Operator
formula& formula::operator=(const formula& other) {
    if (this != &other) {
        delete[] inputMaterial;
        delete[] inputNumber;
        delete[] outputMaterial;
        delete[] outputNumber;

        inputSize = other.inputSize;
        outputSize = other.outputSize;

        inputMaterial = new string[inputSize];
        inputNumber = new int[inputSize];

        for (int i = 0; i < inputSize; i++) {
            inputMaterial[i] = other.inputMaterial[i];
            inputNumber[i] = other.inputNumber[i];
        }

        outputMaterial = new string[outputSize];
        outputNumber = new int[outputSize];

        for (int j = 0; j < outputSize; j++) {
            outputMaterial[j] = other.outputMaterial[j];
            outputNumber[j] = other.outputNumber[j];
        }

        proficiencyLevel = other.proficiencyLevel;
        experienceNum = other.experienceNum;
        failure = other.failure;
        partial = other.partial;
        normal = other.normal;
        bonus = other.bonus;
    }

    return *this;
}

// Comparison Operator
bool formula::operator==(const formula& other) const {
    if (inputSize != other.inputSize || outputSize != other.outputSize)
        return false;
    for (int i = 0; i < inputSize; i++) {
        if (inputMaterial[i] != other.inputMaterial[i] ||
            inputNumber[i] != other.inputNumber[i])
            return false;
    }
    for (int j = 0; j < outputSize; j++) {
        if (outputMaterial[j] != other.outputMaterial[j] ||
            outputNumber[j] != other.outputNumber[j])
            return false;
    }

    return proficiencyLevel == other.proficiencyLevel && experienceNum ==
                                                         other.experienceNum;
}

// Move Constructor
formula::formula(formula&& other) noexcept {

    inputMaterial = other.inputMaterial;
    inputNumber = other.inputNumber;
    outputMaterial = other.outputMaterial;
    outputNumber = other.outputNumber;

    inputSize = other.inputSize;
    outputSize = other.outputSize;

    proficiencyLevel = other.proficiencyLevel;
    experienceNum = other.experienceNum;
    failure = other.failure;
    partial = other.partial;
    normal = other.normal;
    bonus = other.bonus;

    other.inputMaterial = nullptr;
    other.inputNumber = nullptr;
    other.outputMaterial = nullptr;
    other.outputNumber = nullptr;
    other.inputSize = 0;
    other.outputSize = 0;
}

// Move Assignment Operator
formula& formula::operator=(formula&& other) noexcept {
    if (this != &other) {
        delete[] inputMaterial;
        delete[] inputNumber;
        delete[] outputMaterial;
        delete[] outputNumber;

        inputMaterial = other.inputMaterial;
        inputNumber = other.inputNumber;
        outputMaterial = other.outputMaterial;
        outputNumber = other.outputNumber;

        inputSize = other.inputSize;
        outputSize = other.outputSize;

        proficiencyLevel = other.proficiencyLevel;
        experienceNum = other.experienceNum;
        failure = other.failure;
        partial = other.partial;
        normal = other.normal;
        bonus = other.bonus;

        other.inputMaterial = nullptr;
        other.inputNumber = nullptr;
        other.inputSize = 0;
        other.outputMaterial = nullptr;
        other.outputNumber = nullptr;
        other.outputSize = 0;
    }

    return *this;
}

void formula::IncreaseExp() {
    if (experienceNum < MAXEXP) {
        experienceNum++;
        if (experienceNum == 1 / 3 * MAXEXP) {
            IncreaseLevel();
        }
        else if (experienceNum == MAXEXP) {
            IncreaseLevel();
        }
    }
}

void formula::IncreaseLevel() {
    if (proficiencyLevel < MAXPRO) {
        proficiencyLevel++;
        failure -= 5;
        partial -= 5;
        normal += 8;
        bonus += 2;
    }
}

string formula::QueryInput() {
    stringstream inputResult;
    for (int i = 0; i < inputSize; i++) {
        inputResult << inputNumber[i] << " " << inputMaterial[i] << "\n";
    }
    return inputResult.str();
}

string formula::QueryOutput() {
    stringstream outputResult;
    for (int i = 0; i < outputSize; i++) {
        outputResult << outputNumber[i] << " " << outputMaterial[i] << "\n";
    }
    return outputResult.str();
}

string formula::Apply() {
    stringstream ssr;
    string result;
    int randomNum = dis(gen);
    if (randomNum <= failure) {
        result = "There is nothing produced.";
        return result;
    }
    if (randomNum <= failure + partial) {
        for (int i = 0; i < outputSize; i++) {
            ssr << to_string(outputNumber[i] * 0.75) + " " + outputMaterial[i]
                   + "\n";
        }
        ssr >> result;
        IncreaseExp();
        return result;
    }
    if (randomNum <= failure + partial +normal) {
        for (int i = 0; i < outputSize; i++) {
            ssr << to_string(outputNumber[i]) + " " + outputMaterial[i] + "\n";
        }
        ssr >> result;
        IncreaseExp();
        return result;
    }
    else {
        for (int i = 0; i < outputSize; i++) {
            ssr << to_string(outputNumber[i] * 1.1) + " " + outputMaterial[i]
                   + "\n";
        }
        ssr >> result;
        IncreaseExp();
        return result;
    }
}