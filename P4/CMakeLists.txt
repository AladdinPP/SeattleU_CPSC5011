cmake_minimum_required(VERSION 3.27)
project(P4)

set(CMAKE_CXX_STANDARD 17)

add_executable(P4 p4.cpp
        formula.h
        formula.cpp
        plan.h
        plan.cpp
        executableplan.h
        executableplan.cpp
        stockpile.h
        stockpile.cpp)
