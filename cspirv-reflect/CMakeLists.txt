cmake_minimum_required(VERSION 3.1)

project(spirv-reflect)

OPTION(SPIRV_REFLECT_BUILD_TESTS    "Build the SPIRV-Reflect test suite" OFF)
OPTION(SPIRV_REFLECT_ENABLE_ASSERTS "Enable asserts for debugging" OFF)

set_property(GLOBAL PROPERTY USE_FOLDERS ON)
set(CMAKE_CXX_STANDARD 14)
set(BUILD_SHARED_LIBS ON)

set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/bin")

add_library(spirv-reflect ${CMAKE_CURRENT_SOURCE_DIR}/cspirv_reflect.h
                             ${CMAKE_CURRENT_SOURCE_DIR}/spirv_reflect.c)
target_compile_options(spirv-reflect PRIVATE
    $<$<CXX_COMPILER_ID:MSVC>:/W4 /WX>
    $<$<CXX_COMPILER_ID:GNU>:-Wall -Werror>
    $<$<CXX_COMPILER_ID:Clang>:-Wall -Werror>
    $<$<CXX_COMPILER_ID:AppleClang>:-Wall -Werror>)
if (SPIRV_REFLECT_ENABLE_ASSERTS)
    target_compile_definitions(spirv-reflect PRIVATE SPIRV_REFLECT_ENABLE_ASSERTS)
endif()
set_target_properties(spirv-reflect PROPERTIES CXX_STANDARD 11)
target_include_directories(spirv-reflect PRIVATE ${CMAKE_CURRENT_SOURCE_DIR})
if(WIN32)
    target_compile_definitions(spirv-reflect PRIVATE _CRT_SECURE_NO_WARNINGS)
endif()

add_subdirectory(examples)
add_subdirectory(util/stripper)

install(TARGETS spirv-reflect RUNTIME DESTINATION bin)
