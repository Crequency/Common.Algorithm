
cmake_minimum_required (VERSION 3.8)

add_executable (Test "Test.cpp" "Test.h" )

target_link_libraries(Test "Hash" )
