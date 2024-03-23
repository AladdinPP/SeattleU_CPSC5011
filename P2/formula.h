// AUTHOR:      Hongru He
// FILENAME:    formula.h
// DATE:        01/31/2024
// VERSION:     V1.0

#ifndef P2_FORMULA_H
#define P2_FORMULA_H

#include <iostream>
#include <string>
#include <sstream>
#include <random>

using namespace std;

class formula {
private:
    string* inputMaterial;
    string* outputMaterial;
    int* inputNumber;
    int* outputNumber;
    int inputSize;
    int outputSize;
    int failure;
    int partial;
    int normal;
    int bonus;
    int proficiencyLevel;
    int experienceNum;
    random_device rd;
    mt19937 gen{rd()};
    uniform_int_distribution<> dis{0, 100};
    const int MAXEXP = 6;
    const int MAXPRO = 2;

    void IncreaseExp();
    void IncreaseLevel();

public:
    formula();
    formula(string*, int*, int, string*, int*, int);
    ~formula();
    formula(const formula&);
    formula& operator=(const formula&);
    bool operator==(const formula&) const;
    formula(formula&&) noexcept;
    formula& operator=(formula&&) noexcept;
    string QueryInput();
    string QueryOutput();
    string Apply();
};


#endif //P2_FORMULA_H
