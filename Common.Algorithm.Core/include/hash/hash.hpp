#include <base.h>
#include <math.hpp>

namespace Common::Algorithm::Core::Hash {

    EXTERN_ALL const i32 hash_length = 2048;

    EXTERN_ALL const i32 hash_block_length = 128;



    inline bool cmp_a(i32 a, i32 b);

    inline bool cmp_b(i32 a, i32 b);

    inline i32 mix_2(i32 a, i32 b);

    inline i32 mix_3(i32 a, i32 b, i32 c);

    inline i32 mix_5(i32 a, i32 b, i32 c, i32 d, i32 e);

    inline void exp_1(i32 x, i32 *a, i32 *b, i32 *c);

    inline void exp_1(i32 x, i32 *a, i32 *b, i32 *c, i32 *d);

    inline long double spring_func(long double x);



    EXTERN_ALL int extern_test_getnum();

    EXTERN_ALL void hash_str(uchar *src, uchar *rst, int length);

    EXTERN_ALL void hash_compress_128_str(uchar *src, uchar *rst);

    EXTERN_ALL void hash_compress_64_str(uchar *src, uchar *rst);

    EXTERN_ALL void hash_compress_32_str(uchar *src, uchar *rst);

    EXTERN_ALL void hash_compress_16_str(uchar *src, uchar *rst);

    EXTERN_ALL void hash_compress_8_str(uchar *src, uchar *rst);

    EXTERN_ALL void hash_compress_4_str(uchar *src, uchar *rst);

    EXTERN_ALL int hash_file(uchar *fileName, int type);

}
