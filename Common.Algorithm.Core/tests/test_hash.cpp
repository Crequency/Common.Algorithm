﻿#include <stdc++.h>

#include <hash.hpp>

typedef int32_t i32;
typedef int64_t i64;
typedef unsigned char uchar;

using namespace std;

int main() {
    uchar *arr = new uchar[47]{ "SHVIOSJDifjDKljkJ$*F$W*939r5834r89we9fIOSFJOIS" };
    uchar *hash = new uchar[2048];
    uchar *ans = new uchar[64];

    Common::Algorithm::Core::Hash::hash_str(arr, hash, 47);
    Common::Algorithm::Core::Hash::hash_compress_64_str(hash, ans);

    for (i32 i = 0; i < 47; ++i)
        cout << arr[i];
    cout << endl;
    for (i32 i = 0; i < 2048; ++i)
        cout << setw(2) << setfill('0') << hex << (i32)hash[i] << '-';
    cout << endl;
    for (i32 i = 0; i < 64; ++i)
        cout << setw(2) << setfill('0') << hex << (i32)ans[i] << '-';
    cout << endl;

    delete[]arr, hash, ans;
    arr = hash = ans = NULL;

    return 0;
}
