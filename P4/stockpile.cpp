// AUTHOR:      Hongru He
// FILENAME:    stockpile.cpp
// DATE:        02/28/2024
// VERSION:     V1.0

#include "stockpile.h"
#include <string>
#include <sstream>

using namespace std;

// Implementation Invariants:
// 1.   This class uses a hash map to store resource names as keys and their
//      quantities as values, facilitating efficient querying and updating of
//      resources.
// 2.   It provides functions to query the quantity of resources, add or
//      remove resources, and check for the availability of specific quantities
//      of resources.
// 3.   Operations that modify the stockpile (e.g., adding or removing
//      resources) ensure the integrity of the stockpile by not allowing
//      invalid states, such as negative quantities.

// Default Constructor
stockpile::stockpile() = default;

// Overloaded Constructor
stockpile::stockpile(string* material, double* number, int size) {
    for (int i = 0; i < size; i++) {
        resources[material[i]] = number[i];
    }
}

// Destructor
stockpile::~stockpile() {
    resources.clear();
}

// Copy Constructor
stockpile::stockpile(const stockpile& other) {
    for (auto& x : other.resources) {
        resources[x.first] = x.second;
    }
}

// Move Constructor
stockpile::stockpile(stockpile&& other) noexcept {
    for (auto& x : other.resources) {
        resources[x.first] = x.second;
    }
    other.resources.clear();
}

// Overloaded Assignment Operator
stockpile& stockpile::operator=(const stockpile& other) {
    if (this != &other) {
        resources.clear();
        for (auto &x: other.resources) {
            resources[x.first] = x.second;
        }
    }

    return *this;
}

// Move Assignment Operator
stockpile& stockpile::operator=(stockpile&& other) noexcept {
    if (this != &other) {
        resources.clear();

        for (auto &x: other.resources) {
            resources[x.first] = x.second;
        }

        other.resources.clear();
    }

    return *this;
}

// Get all the resources and their quantities
string stockpile::QueryResources() {
    if (resources.empty()) {
        return "This stockpile is empty.";
    }

    stringstream result;

    for (auto x : resources) {
        result << x.second << " " << x.first << "\n";
    }

    return result.str();
}

// Get the quantity of a specific resource
int stockpile::QueryQuantity(const string& resourceName) const {
    auto item = resources.find(resourceName);
    if (item != resources.end()) {
        return item->second;
    }
    return -1;
}

// Increase the quantity of the specific resource
void stockpile::IncreaseResource(const string& resourceName, double numAdd) {
    resources[resourceName] += numAdd;
}

// Decrease the quantity of the specific resource
bool stockpile::DecreaseResource(const string& resourceName, int numDec) {
    auto item = resources.find(resourceName);
    if (item != resources.end() && resources[resourceName] >= numDec) {
        resources[resourceName] -= numDec;
        return true;
    }
    return false;
}

// Check if the stockpile has sufficient quantity of parameter resource
bool stockpile::CheckMaterial(string& material, int number) {
    if (resources.find(material) != resources.end()
        && resources[material] >= number) {
        return true;
    }
    return false;
}