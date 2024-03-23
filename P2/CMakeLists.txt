cmake_minimum_required(VERSION 3.27)
project(P2)

set(CMAKE_CXX_STANDARD 14)

include_directories(.)

add_executable(P2
        formula.cpp
        formula.h
        p2.cpp
        plan.h
        plan.cpp)
