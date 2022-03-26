#include "../Base/base.h"

namespace calg{
    extern "C" EXTERN_API int extern_test_getnum();
    extern "C" EXTERN_API void hash_str(uchar * src, uchar * rst);
    extern "C" EXTERN_API void hash_compress_str(uchar * src, uchar * rst);
    extern "C" EXTERN_API int hash_file(uchar * fileName, int type);
}

