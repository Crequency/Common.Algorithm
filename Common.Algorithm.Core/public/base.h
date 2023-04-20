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

#if defined _WIN32 || defined __CYGWIN__
#   ifdef __GNUC__
#       define EXPORTED __attribute__ ((dllexport))
#   else
#       // Note: actually gcc seems to also supports this syntax.
#       define EXPORTED extern "C" __declspec(dllexport)
#   endif
#   define NOT_EXPORTED
#else
#   if __GNUC__ >= 4
#       define EXPORTED extern "C" __attribute__ ((visibility ("default")))
#       define NOT_EXPORTED  __attribute__ ((visibility ("hidden")))
#   else
#       define EXPORTED
#       define NOT_EXPORTED
#   endif
#endif

typedef unsigned char uchar;
typedef long double ld;

typedef int64_t i64;
typedef int32_t i32;
typedef uint64_t u64;
typedef uint32_t u32;
