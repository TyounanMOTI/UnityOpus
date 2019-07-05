cmake_minimum_required(VERSION 3.4.1)

add_library(unityopus SHARED debug.cpp entrypoint.cpp runtime_error.cpp)
include_directories(libopus/include)

add_subdirectory(libopus/src)
target_link_libraries(unityopus opus)
