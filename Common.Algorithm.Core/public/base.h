//#include "stdc++.h"

#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <algorithm>
#include <cstdarg>
#include <cstring>
#include <stack>
#include <list>
#include <queue>
#include <ostream>
#include <sstream>

#define EXTERN_INC extern "C"
#define EXTERN_CPP extern "C++"

#if defined _WIN32 || defined __CYGWIN__
#   ifdef __GNUC__
#       define EXTERN_API __attribute__ ((dllexport))
#       define EXTERN_ALL __attribute__ ((dllexport))
#   else
#       // Note: actually gcc seems to also supports this syntax.
#       define EXTERN_API __declspec(dllexport)
#       define EXTERN_ALL extern "C" __declspec(dllexport)
#   endif
#   define NOT_EXPORTED
#else
#   if __GNUC__ >= 4
#       define EXPORTED __attribute__ ((visibility ("default")))
#       define NOT_EXPORTED  __attribute__ ((visibility ("hidden")))
#
#       define EXTERN_API __attribute__ ((visibility ("default")))
#       define EXTERN_ALL __attribute__ ((visibility ("default")))
#   else
#       define EXPORTED
#       define NOT_EXPORTED
#
#       define EXTERN_API
#       define EXTERN_ALL
#   endif
#endif

typedef unsigned char uchar;
typedef long double ld;

typedef int64_t i64;
typedef int32_t i32;
typedef uint64_t u64;
typedef uint32_t u32;
