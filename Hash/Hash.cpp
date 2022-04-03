#include "Hash.h"


namespace calg{



    EXTERN_API int extern_test_getnum(){ return 1; }
    inline bool cmp_a(uchar a, uchar b){ return a < b; }
    inline bool cmp_b(uchar a, uchar b){ return a > b; }
    inline i32 mix_3(i32 a, i32 b, i32 c){
        i64 t_max, t_min;
        calg::maxin(&t_max, &t_min, 3, a, b, c);
        i32 max = (i32)t_max, min = (i32)t_min;
        if(a == max) return ((i64)(a * b * c) - min) % INT32_MAX;
        else if(c - a > b) return (a * c + (b + c) * a) % INT32_MAX;
        else if(c - a < b) return ((i64)(b * c - b - c) + calg::pow(a, 2) % INT32_MAX);
        else return ((i64)max * min + (max ^ min) * mid(a, b, c)) % INT32_MAX;
    }
    inline i32 mix_5(i32 a, i32 b, i32 c, i32 d, i32 e){
        i32 A = mix_3(a, b, c), B = mix_3(c, d, e), C = mix_3(b, c, d),
            D = mix_3(a, c, e), E = mix_3(a, b, d), F = mix_3(e, d, b),
            G = mix_3(a, d, e), H = mix_3(e, b, a), I = mix_3(b, c, e),
            J = mix_3(d, c, a), K = mix_3(a, b, e), L = mix_3(e, d, a);
        if(A ^ B & 1){
            i64 CDEF = (i64)C * D - E - F;
            return (i32)calg::abs(CDEF % INT32_MAX - (i64)K);
        } else{
            i64 t_a = calg::abs((i64)(G + H) ^ (i64)(I * J));
            return t_a % INT32_MAX - (i64)L;
        }
    }
    inline void exp_1(i32 x, i32 *a, i32 *b, i32 *c){
        i32 ea = (x << 1) & 114514, eb = x ^ 1919, ec = (x >> 1) & 810;
        i32 ca = gobit(x, 10), cb = gobit(x >> 10, 10), cc = gobit(x >> 20, 10);
        *a = (ea ^ ca) >> 1, *b = (eb ^ cb) >> 1, *c = (ec ^ cc) >> 1;
    }
    inline long double spring_func(long double x){
        long double A = sinl(calg::pi * log2l(calg::abs(x * 2 + 1)));
        long double B = powl(2, x * 2) + powl(calg::e, x * 4);
        return cosl(A + B);
    }
    EXTERN_API void hash_str(uchar *src, uchar *rst, int length){
        i32 *mid = new i32[hash_length];                                //  中间运算结果
        i32 **ato = new i32 * [hash_length];                            //  Grava函数矩阵
        for(int i = 0; i < hash_length; ++i){
            ato[i] = new i32[hash_length];
            memset(ato[i], 0, sizeof(i32) * hash_length);
        }

        /* 源字符串预填充 */
        if(length == hash_length)                                       //  长度刚好, 直接填充
            for(int i = 0; i < hash_length; ++i)
                mid[i] = (i32)src[i];
        else if(length > hash_length){                                  //  源较长, n 元混合
            for(int i = 0, pos = 0; i < length;
                ++i, pos = pos == hash_length ? 0 : pos + 1){
                //TODO: 混合源串的填充

            }
        } else{                                                         //  源较短, n 元扩展
            //TODO: 扩展源串的填充

        }



        for(int i = 0; i < hash_length; ++i){
            delete[]ato[i];
            ato[i] = NULL;
        }
        delete[]ato;
        ato = NULL;

        //memset(rst, '0', sizeof(uchar) * 2048);
        //std::string str((char *)src);
        //if (str.length() <= hash_length){
        //    int index = hash_length - 1;
        //    for (int i = str.length() - 1;
        //         i >= 0; -- i, -- index){
        //        rst[index] = src[i];
        //    }
        //    uchar fill = 0;
        //    for (int i = 0; i < index; ++ i, fill = fill == 255 ? 0 : fill + 1){
        //        rst[i] = fill;
        //    }
        //} else{
        //    long long block = str.length() / hash_block_length;
        //    long long remain = str.length() - block * hash_block_length;
        //    uchar *blocks[hash_block_length + 1]{};
        //    for (int i = 1; i <= hash_block_length; ++ i)
        //        blocks[i] = new uchar[block + 1];
        //    for (int i = 1, cur = 0; i <= hash_block_length; ++ i)
        //        for (int j = 1; j <= block; ++ j, ++ cur)
        //            blocks[i][j] = rst[cur];
        //    //TODO: 添加CUDA选择器, 可选是否使用CUDA操作
        //    for (int i = 1; i <= block; ++ i){
        //        ull eq_a = 1, eq_b = 1, eq_c = 1, eq_d = 1;
        //        for (int k = 1; k <= 32; ++ k)
        //            eq_a *= ((int)blocks[k][i] << 1);
        //        for (int k = 32 + 1; k <= 64; ++ k)
        //            eq_b *= ((int)blocks[k][i] << 1);
        //        for (int k = 64 + 1; k <= 96; ++ k)
        //            eq_c *= ((int)blocks[k][i] << 1);
        //        for (int k = 96 + 1; k <= 128; ++ k)
        //            eq_d *= ((int)blocks[k][i] << 1);
        //        eq_a %= 255, eq_b %= 255, eq_c %= 255, eq_d %= 255;
        //        ull *eq_arr[4] = {&eq_a, &eq_b, &eq_c, &eq_d};
        //        for (int i = hash_length, index = 0;
        //             i < (int)str.length();
        //             ++ i, index = (index == 3 ? 0 : index + 1)){
        //            (*eq_arr[index]) += src[i];
        //            (*eq_arr[index]) <<= 1;
        //        }
        //        ull ans = ((eq_a * 32) % 256) * ((eq_b * 32 * 32) % 256);
        //        ans %= 256; ans *= ((eq_c * (int)std::pow(32, 3)) % 256);
        //        ans %= 256; ans *= ((eq_c * (int)std::pow(32, 4)) % 256);
        //        ans %= 256; rst[i] = (char)ans;
        //    }
        //    for (int i = 1; i <= block; ++ i)
        //        delete(blocks[i]);
        //}

        /* ============================= 以下是真哈希部分 ============================= */

        //for (int i = 2; i <= hash_length - 2; ++ i){
        //    uchar a = rst[i - 1], b = rst[i], c = rst[i + 1], tmp;
        //    tmp = a, a = c, c = tmp;
        //    b = std::max(std::max(a * c, a * b), b * c) % 255;
        //    a *= b, c *= b;
        //    a = c - b, c = a - b;
        //    rst[i - 1] = (uchar)std::pow(a * c - a - c, c % 5) % 255;
        //    rst[i] = (uchar)std::pow(b - a - c, a % 5) % 255;
        //    rst[i + 1] = (uchar)std::pow(c * b - c - b, b % 5) % 255;
        //    std::sort(rst + i - 2, rst + i + 2, b % 2 == 0 ? cmp_a : cmp_b);
        //    rst[i + 2] += rst[i - 1] >= rst[i - 2] ? rst[i + 1] <<= 1 : rst[i] >>= 1;
        //    rst[i - 1] -= rst[i + 2] % 3 == 2 ? rst[i] + (rst[i + 2] >>= 1) :
        //        (uchar)((rst[i - 2] + (int)std::pow(a + b, c)) % 255);
        //    rst[i] *= rst[i + 2] - rst[i - 1];
        //    rst[i - 2] += rst[i] + (rst[i - 1] <<= 1);
        //    rst[i + 1] = rst[i + 2] ^ rst[i - 1] + rst[i] & rst[i - 2];

        //    rst[i - 2] -= rst[i + 1] - rst[i] - rst[i - 1] - rst[i - 2];
        //    rst[i - 1] -= rst[i] - rst[i - 1] - rst[i - 2];
        //    rst[i] -= rst[i - 1] - rst[i - 2];
        //    rst[i + 1] -= rst[i - 2];
        //    rst[i + 2] <<= 1;
        //    rst[i - 2] >>= 1;
        //    rst[i - 1] += rst[i - 2];
        //    rst[i] += rst[i - 1] - rst[i - 2];
        //    rst[i + 1] += rst[i] - rst[i - 1] - rst[i - 2];
        //    rst[i + 2] += rst[i + 1] - rst[i] - rst[i - 1] - rst[i - 2];
        //}

        ////  解决小于 2048 时前导 '0' 重复导致一摸一样哈希的问题
        //for (int i = 0, j = hash_length - 1, launched = 1;
        //     i != j && i < j && i != j - 1;
        //     ++ i, ++ launched, j -= ((~rst[i + 1] ^ rst[i]) % 3 == 0 ? 1 : 2)){
        //    rst[i] += rst[j] * rst[j - 1];
        //    rst[i] >>= (rst[i + 1] % 4);
        //    rst[i + 1] = rst[i] & (rst[j - 1] + rst[j]);
        //    rst[i + 2] = rst[i + 1] | (rst[j - 1] & rst[i]);
        //    rst[i] += rst[i] ^ rst[j];
        //    rst[i] += (rst[0] &= rst[j]) ^ (rst[hash_length - 1] >>= 1);
        //    rst[0] >>= 1;
        //    rst[j] -= rst[i] ^ rst[i + 1];
        //    rst[j - 1] -= (rst[j - 1] + ((rst[(j + i) >> 1] ^
        //        ~rst[((rst[i] * rst[j] - rst[i] - rst[j]) % 2048) >> 1]) >> 1));
        //    rst[0] = rst[i] + rst[j];
        //    rst[hash_length - 1] = rst[i] - rst[j];
        //    if (launched >= 2048) break;
        //}
    }
    EXTERN_API void hash_compress_128_str(uchar *src, uchar *rst){

    }
    EXTERN_API void hash_compress_64_str(uchar *src, uchar *rst){
        /*uchar *f = new uchar[hash_length]; size_t len = sizeof(uchar) * 1024;
        memset(f, src[1024 - 1], len); memset(f + 1024, src[2048 - 1], len);
        ull block_a = src[1024 - 1], block_b = src[2048 - 1], α = block_a - block_b;
        for (int i = 1; i < 1024; ++ i){
            f[i] *= f[i - 1]; f[i + 1] -= f[i];
            block_a *= (f[i] ^ f[i + 1]); block_b += block_a;
        }
        for (int i = 1024; i < 2048 - 1; ++ i){
            f[i] *= f[i - 1]; f[i - 1] += f[i];
            block_b *= (f[i] ^ f[i - 1]); block_a -= block_b;
        }
        α *= block_a & block_b; uchar β = α % 255, γ = (α | β) % 255;
        memset(rst, ((β * γ) | (γ + β)) % 512, sizeof(uchar) * 64);
        for (int i = 0; i < 64; ++ i){
            ull tmp_sum = src[i];
            for (int j = i * 32; j < (i + 1) * 32; ++ j){
                tmp_sum *= src[j];
                rst[i] += tmp_sum ^ src[j];
            }
            rst[i] %= 512;
        }*/
    }
    EXTERN_API void hash_compress_32_str(uchar *src, uchar *rst){

    }
    EXTERN_API void hash_compress_16_str(uchar *src, uchar *rst){

    }
    EXTERN_API void hash_compress_8_str(uchar *src, uchar *rst){

    }
    EXTERN_API void hash_compress_4_str(uchar *src, uchar *rst){

    }
    EXTERN_API int hash_file(uchar *fileName, int type){
        //TODO: 文件哈希

        return 1;
    }
}
