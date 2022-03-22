#include "stdc++.h"
#define uchar unsigned char
#define ulong unsigned long long

#define st_ca static_cast
#define st_ca_int static_cast<int>
#define re_ca reinterpret_cast
#define re_ca_charc reinterpret_cast<char *>

#define EXTERN_API __declspec(dllexport)

extern "C" EXTERN_API int extern_test_getnum();

extern "C" EXTERN_API void hash_str(uchar * src, uchar * rst);

extern "C" EXTERN_API void hash_compress_str(uchar * src, uchar * rst);

extern "C" EXTERN_API int hash_file(uchar * fileName, int type);


