
set(TEST_SOURCES
    test_hash.cpp
)

add_executable("Common.Algorithm.Test" ${TEST_SOURCES})

target_link_libraries("Common.Algorithm.Test" PRIVATE "Common.Algorithm.Core")

add_test(test_hash.cpp "Common.Algorithm.Test" COMMAND "Common.Algorithm.Test")
