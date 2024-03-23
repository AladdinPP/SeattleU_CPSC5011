// AUTHOR:      Hongru He
// FILENAME:    formula.h
// DATE:        01/31/2024
// VERSION:     V1.0

#ifndef P4_FORMULA_H
#define P4_FORMULA_H
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
    bool completed;
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
    bool operator!=(const formula&) const;
    formula(formula&&) noexcept;
    formula& operator=(formula&&) noexcept;
    string QueryInput();
    string QueryOutput();
    string QueryInputMaterial(int) const;
    string QueryOutputMaterial(int) const;
    int QueryInputNumber(int) const;
    int QueryInputSize();
    int QueryOutputSize();
    bool QueryCompleted() const;
    void ResetCompleted();
    string Apply();
};


#endif //P4_FORMULA_H
