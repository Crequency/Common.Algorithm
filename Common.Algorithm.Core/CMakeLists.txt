# CMakeList.txt: 顶层 CMake 项目文件，在此处执行全局配置
# 并包含子项目。

cmake_minimum_required(VERSION 3.8)

project(Common.Algorithm.Core)

set(CMAKE_CXX_STANDARD 20)

file(GLOB SOURCE_FILES
    "${CMAKE_CURRENT_SOURCE_DIR}/source/*.cpp"
        "${CMAKE_CURRENT_SOURCE_DIR}/source/encode/*.cpp"
        "${CMAKE_CURRENT_SOURCE_DIR}/source/hash/*.cpp"
        "${CMAKE_CURRENT_SOURCE_DIR}/source/math/*.cpp"
)

include_directories(
    ${CMAKE_CURRENT_SOURCE_DIR}/include
        ${CMAKE_CURRENT_SOURCE_DIR}/include/encode
        ${CMAKE_CURRENT_SOURCE_DIR}/include/hash
        ${CMAKE_CURRENT_SOURCE_DIR}/include/math
    ${CMAKE_CURRENT_SOURCE_DIR}/public
)

add_library(Common.Algorithm.Core SHARED ${SOURCE_FILES})

add_subdirectory(tests)

