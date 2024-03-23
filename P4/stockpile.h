// AUTHOR:      Hongru He
// FILENAME:    stockpile.h
// DATE:        01/31/2024
// VERSION:     V1.0

#ifndef P4_STOCKPILE_H
#define P4_STOCKPILE_H
#include <iostream>
#include <unordered_map>

using namespace std;

// The Stockpile class simulates a stockpile consisting of multiple
// resource names and quantities.
// Class Invariants:
// 1.   It ensures that the quantity of any resource is accurately tracked
//      and can be increased or decreased based on operations performed on
//      the stockpile.
// 2.   The stockpile does not allow negative quantities; operations that
//      would result in negative quantities fail or are prevented.

class stockpile {
private:
    unordered_map<string, double> resources;

public:
    stockpile();
    // Default Constructor
    // Explanation:     Initializes a Stockpile with an empty map.
    // Precondition:    None.
    // Postcondition:   Stockpile object is initialized with default setting.

    stockpile(string*, double*, int);
    // Overloaded Constructor
    // Explanation:     Initializes a Stockpile with a set of initial resources.
    // Precondition:    None.
    // Postcondition:   Stockpile object is initialized with the given
    //                  resources.

    ~stockpile();
    // Destructor
    // Explanation:     Destroys the Stockpile object and releases allocated
    //                  resources.
    // Precondition:    None.
    // Postcondition:   Stockpile object is properly cleaned up.

    stockpile(const stockpile&);
    // Copy Constructor
    // Explanation:     Creates a new Stockpile as a copy of another Stockpile.
    // Precondition:    The parameter is a valid, existing Stockpile object.
    // Postcondition:   A new Stockpile object is created as a copy of the
    //                  given one.

    stockpile(stockpile&&) noexcept;
    // Move Constructor
    // Explanation:     Moves an existing Stockpile to a new Stockpile.
    // Precondition:    The parameter is a valid, existing Stockpile object.
    // Postcondition:   The new Stockpile takes ownership of parameter's
    //                  resources.

    stockpile& operator=(const stockpile&);
    // Overloaded Constructor
    // Explanation:     Assigns one Stockpile to another using copy semantics.
    // Precondition:    The parameter is a valid, existing Stockpile object.
    // Postcondition:   The original Stockpile is a copy of the given one.

    stockpile& operator=(stockpile&&) noexcept;
    // Move Assignment Operator
    // Explanation:     Assigns one Stockpile to another using move semantics.
    // Precondition:    The parameter is a valid, existing Stockpile object.
    // Postcondition:   The new Stockpile takes ownership of parameter's
    //                  resources.

    string QueryResources();
    // Get all the resources with their quantities
    // Explanation:     Return all the resources in the Stockpile as a string.
    // Precondition:    The Stockpile is not empty.
    // Postcondition:   Return a string containing all the resource names and
    //                  quantities.

    int QueryQuantity(const string&) const;
    // Get the quantity of the specific resource
    // Explanation:     Return the quantity of the specific resource.
    // Precondition:    None.
    // Postcondition:   Return the quantity if resource is in the Stockpile,
    //                  or return -1.

    void IncreaseResource(const string&, double);
    // Increase the quantity of the specific resource
    // Explanation:     Increase the quantity of the specific resource.
    // Precondition:    None.
    // Postcondition:   The quantity of the specific resource is increased.

    bool DecreaseResource(const string&, int);
    // Decrease the quantity of the specific resource
    // Explanation:     Decrease the quantity of the specific resource if valid.
    // Precondition:    None.
    // Postcondition:   If the resource is in the Stockpile and the quantity
    //                  is valid, it gets decreased.

    bool CheckMaterial(string&, int);
    // Check if the stockpile has sufficient quantity of parameter resource
    // Explanation:     Check if the Stockpile holds sufficient quantity of
    //                  the specific resource.
    // Precondition:    None.
    // Postcondition:   Return the boolean result.
};


#endif //P4_STOCKPILE_H
