# CMakeList.txt: 顶层 CMake 项目文件，在此处执行全局配置
# 并包含子项目。
#
cmake_minimum_required (VERSION 3.8)

project ("Common.Algorithm")

# 包含子项目。
add_subdirectory ("Base")
add_subdirectory ("Hash")
add_subdirectory ("Math")
add_subdirectory ("Encode")

add_subdirectory ("Test")

add_definitions ("cpp.hint")
