#include "Hash.h"


namespace calg{



    EXTERN_API int extern_test_getnum(){ return 1; }
    inline bool cmp_a(i32 a, i32 b){ return a < b; }
    inline bool cmp_b(i32 a, i32 b){ return a > b; }
    inline i32 mix_2(i32 a, i32 b){
        i32 ea1, ea2, ea3, eb1, eb2, eb3;
        exp_1(a, &ea1, &ea2, &ea3);
        exp_1(b, &eb1, &eb2, &eb3);
        i64 max, min, mid1, mid2;
        calg::maxin(&max, &min, 6, ea1, ea2, ea3, eb1, eb2, eb3);
        mid1 = calg::mid(ea2, ea3, eb1);
        mid2 = calg::mid(ea3, eb1, eb2);
        i32 m1 = (i32)max, m2 = (i32)min, m3 = (i32)mid1, m4 = (i32)mid2;
        i32 **arr = new i32 * [10]{
            &ea1, &ea2, &ea3, &eb1, &eb2, &eb3, &m1, &m2, &m3, &m4
        };
        std::sort(arr, arr + 10,
                  [](i32 *a, i32 *b){
                      return *a * *b % 2 == 0 ? *a > *b : *a < *b;
                  }
        );
        return (*arr[calg::abs((i64)(a * b)) % 9] + *arr[calg::abs((i64)(a * b - a)) % 9]
                + *arr[calg::abs((i64)(a * b - b)) % 9]
                + *arr[calg::abs((i64)(a * b - a - b)) % 9] + 1 + a ^ b + a & b) / 4;
    }
    inline i32 mix_3(i32 a, i32 b, i32 c){
        i64 t_max, t_min;
        calg::maxin(&t_max, &t_min, 3, a, b, c);
        i32 max = (i32)t_max, min = (i32)t_min;
        if (a == max) return ((i64)(a * b * c) - min) % INT32_MAX;
        else if (c - a > b) return (a * c + (b + c) * a) % INT32_MAX;
        else if (c - a < b) return ((i64)(b * c - b - c) + calg::pow(a, 2) % INT32_MAX);
        else return ((i64)max * min + (max ^ min) * calg::mid(a, b, c)) % INT32_MAX;
    }
    inline i32 mix_5(i32 a, i32 b, i32 c, i32 d, i32 e){
        i32 A = mix_3(a, b, c), B = mix_3(c, d, e), C = mix_3(b, c, d),
            D = mix_3(a, c, e), E = mix_3(a, b, d), F = mix_3(e, d, b),
            G = mix_3(a, d, e), H = mix_3(e, b, a), I = mix_3(b, c, e),
            J = mix_3(d, c, a), K = mix_3(a, b, e), L = mix_3(e, d, a);
        if (A ^ B & 1){
            i64 CDEF = (i64)C * D - E - F;
            return (i32)calg::abs(CDEF % INT32_MAX - (i64)K);
        } else{
            i64 t_a = calg::abs((i64)(G + H) ^ (i64)(I * J));
            return t_a % INT32_MAX - (i64)L;
        }
    }
    inline void exp_1(i32 x, i32 *a, i32 *b, i32 *c){
        i32 ea = (x << 1) & 114514, eb = x ^ 1919, ec = (x >> 1) & 810;
        i32 ca = (i32)gobit(x, 10), cb = (i32)gobit(x >> 10, 10), cc = (i32)gobit(x >> 20, 10);
        *a = (ea ^ ca) >> 1, *b = (eb ^ cb) >> 1, *c = (ec ^ cc) >> 1;
    }
    inline void exp_1(i32 x, i32 *a, i32 *b, i32 *c, i32 *d){
        i32 ea = (x << 1) & 114514, eb = x ^ 1919, ec = (x >> 1) & 810, ed = x;
        i32 ca = (i32)gobit(x, 8), cb = (i32)gobit(x >> 8, 8), cc = (i32)gobit(x >> 16, 8), cd = (i32)gobit(x >> 24, 8);
        *a = (ea ^ ca) >> 1, *b = (eb ^ cb) >> 1, *c = (ec ^ cc) >> 1, *d = (ed ^ cd) >> 1;
    }
    inline long double spring_func(long double x){
        ld A = sinl(calg::pi * log2l((ld)calg::abs((i64)x * 2 + 1)));
        ld B = powl(2, x * 2) + powl(calg::e, x * 4);
        return cosl(A + B);
    }
    EXTERN_API void hash_str(uchar *src, uchar *rst, int length){
        i32 *mid = new i32[hash_length];                                //  中间运算结果

        memset(mid, 0, sizeof(i32) * hash_length);                      //  初始化中间运算结果数组
        memset(mid, '0', sizeof(uchar) * hash_length);                  //  初始化结果数组

        /* 源字符串预填充 */
        if (length == hash_length)                                      //  长度刚好, 直接填充
            for (i32 i = 0; i < hash_length; ++i)
                mid[i] = (i32)src[i];
        else if (length > hash_length){                                 //  源较长, n 元混合
            mid[0] = mix_5(src[0], src[1], src[2], src[3], src[4]);
            mid[1] = mix_3(mid[0], src[1], src[2]);
            mid[2] = mix_5(mid[0], mid[1], src[2], src[3], src[4]);
            for (i32 i = 3, pos = 3; i < length;
                 ++i, pos = pos == hash_length ? 3 : pos + 1){
                mid[pos % hash_length] = mix_3(src[i], mid[pos - 1], mid[pos - 2]);
            }
        } else{                                                         //  源较短, n 元扩展
            for (i32 i = 0, t = 0; i < hash_length;                     //  初始化赋值
                 ++ i, t = t == 0x7fffffff ? 0 : t + 1){
                mid[i] += t ^ mid[i % length];
            }
            i32 α = 0, β = α + mid[0], γ = α ^ β;                       //  系数
            std::queue<i32> tar;                                        //  已扩展的存储队列
            for (i32 i = 0, a, b, c; i < length; ++ i){
                exp_1(src[i], &a, &b, &c);
                tar.push(a); tar.push(b); tar.push(c);
                if (mix_3(a, b, c) % 2 == 0) ++ α;
                else -- α;
                if (mix_5(a, b, c, α, β) & 1) β += α;
                else β /= α ^ b;
                if (mix_3(α, β, γ) % 2 == 0) γ += α * β;
                else γ -= β - α;
            }
            while (tar.size() < hash_length){                           //  数量不足 2048 时
                i32 x, y; x = tar.front(); tar.pop();                   //  取出两个进行分解并追加
                y = tar.front(); tar.pop();
                i32 xa, xb, xc, ya, yb, yc;                             //  分解值变量
                exp_1(x, &xa, &xb, &xc); exp_1(y, &ya, &yb, &yc);       //  分解值
                tar.push(mix_3(xa, xb, ya));
                tar.push(mix_3(xa, xb, yb));
                tar.push(mix_3(xa, xb, yc));
                tar.push(mix_3(ya, yb, xa));
                tar.push(mix_3(ya, yb, xb));
                tar.push(mix_3(ya, yb, xc));
            }
            for (i32 i = 0; i < hash_length; ++ i){
                mid[i] = (i32)calg::abs((i64)(tar.front() ^ tar.back() * α));
                tar.push(tar.front()); tar.pop();
                mid[i] *= tar.back() + (α - (β ^ γ));
            }
        }

        for (i32 i = 2; i < hash_length - 3; ++ i){
            i32 *α = new i32[5]{
                mid[i - 2], mid[i - 1], mid[i], mid[i + 1], mid[i + 2]
            }, *σ = new i32[3]{
                mid[i - 2] ^ mid[i - 1], mid[i], mid[i + 1] ^ mid[i + 2]
            };
            i64 sum = 0, mul = 1, γ = 114514;
            if (mid[i] % 2 == 0) std::sort(α, α + 5, cmp_a);
            else std::sort(σ, σ + 3, cmp_b);
            for (i32 j = 0; j < 5; ++ j)
                for (i32 k = 0; k < 3; ++ k){
                    i64 num = α[j] ^ σ[k];
                    if (!num) num += (i64)(j * k);
                    sum += num, mul *= num, γ ^= (sum * mul);
                    α[j] *= (i32)sum, σ[k] *= (i32)mul;
                    if (!α[j]) α[j] += j * j * k;
                    if (!σ[k]) σ[k] += j * k * k;
                }
            for (i32 j = i - 2, k = 0; j < i + 2; ++ j, ++ k)
                mid[j] += (α[k] & σ[k % 3]) ? α[k] & σ[k % 3] : α[k] * σ[k % 3];
            delete[] α, σ;
        }

        for (i32 i = 2; i <= hash_length - 3; ++ i){
            i32 a = mid[i - 1], b = mid[i], c = mid[i + 1], tmp;
            tmp = a, a = c, c = tmp;
            b = calg::max(3, a * c, a * b, b * c) % 255;
            a *= b, c *= b, a = c - b, c = a - b;
            std::sort(mid + i - 2, mid + i + 2, b % 2 == 0 ? cmp_a : cmp_b);
            mid[i + 2] += mid[i - 1] >= mid[i - 2] ? mid[i + 1] <<= 1 : mid[i] >>= 1;
            mid[i] *= mid[i + 2] - mid[i - 1];
            mid[i - 2] += mid[i] + (mid[i - 1] <<= 1);
            mid[i + 1] = mid[i + 2] ^ mid[i - 1] + mid[i] & mid[i - 2];
            mid[i - 2] -= mid[i + 1] - mid[i] - mid[i - 1] - mid[i - 2];
            mid[i - 1] -= mid[i] - mid[i - 1] - mid[i - 2];
            mid[i] -= mid[i - 1] - mid[i - 2];
            mid[i + 1] -= mid[i - 2];
            mid[i + 2] <<= 1;
            mid[i - 2] >>= 1;
            mid[i - 1] += mid[i - 2];
            mid[i] += mid[i - 1] - mid[i - 2];
            mid[i + 1] += mid[i] - mid[i - 1] - mid[i - 2];
            mid[i + 2] += mid[i + 1] - mid[i] - mid[i - 1] - mid[i - 2];
        }

        for (i32 i = 0, j = hash_length - 1, launched = 1;
             i != j && i < j && i != j - 1;
             ++ i, ++ launched, j -= ((~mid[i + 1] ^ mid[i]) % 3 == 0 ? 1 : 2)){
            mid[i] += mid[j] * mid[j - 1];
            mid[i] >>= (mid[i + 1] % 4);
            mid[i + 1] = mid[i] & (mid[j - 1] + mid[j]);
            mid[i + 2] = mid[i + 1] | (mid[j - 1] & mid[i]);
            mid[i] += mid[i] ^ mid[j];
            mid[i] += (mid[0] &= mid[j]) ^ (mid[hash_length - 1] >>= 1);
            mid[0] >>= 1;
            mid[j] -= mid[i] ^ mid[i + 1];
            mid[j - 1] -= (mid[j - 1] + ((
                mid[(j + i) >> 1] ^ ~mid[
                    calg::abs((i64)(mid[i] * mid[j] - mid[i] - mid[j])) % 2047
                ]) >> 1));
            mid[0] = mid[i] + mid[j];
            mid[hash_length - 1] = mid[i] - mid[j];
            if (launched >= 2048) break;
        }

        for (i32 i = 0; i < hash_length; ++ i)                          //  拷贝中间运算结果到结果
            rst[i] = (uchar)((i32)calg::abs((i64)mid[i]) % 255);

        delete[] mid; mid = NULL;                                       //  回收中间运算结果数组

        return;
    }
    EXTERN_API void hash_compress_128_str(uchar *src, uchar *rst){
        i32 *mid = new i32[128];
        i32 **at = new i32 * [16];
        for (i32 i = 0; i < 16; ++ i)
            at[i] = new i32[128];

        memset(mid, (i32)src[64], sizeof(i32) * 128);
        for (i32 i = 0, x = 0, y = 0; i < 2048; ++ i,
             x = y == 15 ? x + 1 : x,
             y = y == 15 ? 0 : y + 1){
            at[y][x] = (i32)src[i];
        }

        for (i32 i = 0; i < 128; ++ i){
            i64 tmp = 1;
            for (i32 j = 0; j < 16; ++ j){
                tmp *= (i64)(at[j][i] == 0 ? i * j : at[j][i] + i * j);
                if (i != 0 && j != 0)
                    tmp += (i64)at[j - 1][i - 1];
                if (i != 0) at[j][i - 1] ^= tmp;
                if (j != 0) at[j - 1][i] &= tmp;
            }
            rst[i] = (uchar)((i32)calg::abs(tmp) % 255);
        }

        for (i32 i = 0; i < 16; ++ i){
            delete[]at[i]; at[i] = NULL;
        }
        delete[]at; at = NULL;
    }
    EXTERN_API void hash_compress_64_str(uchar *src, uchar *rst){
        uchar *mid = new uchar[128];
        hash_compress_128_str(src, mid);
        for (i32 i = 0; i < 64; ++ i)
            rst[i] = (uchar)(calg::abs((i64)mix_2((i32)mid[i * 2], (i32)mid[i * 2 + 1])) % 255);
    }
    EXTERN_API void hash_compress_32_str(uchar *src, uchar *rst){
        uchar *mid = new uchar[64];
        hash_compress_64_str(src, mid);
        for (i32 i = 0; i < 32; ++ i)
            rst[i] = (uchar)(calg::abs((i64)mix_2((i32)mid[i * 2], (i32)mid[i * 2 + 1])) % 255);
    }
    EXTERN_API void hash_compress_16_str(uchar *src, uchar *rst){
        uchar *mid = new uchar[32];
        hash_compress_32_str(src, mid);
        for (i32 i = 0; i < 16; ++ i)
            rst[i] = (uchar)(calg::abs((i64)mix_2((i32)mid[i * 2], (i32)mid[i * 2 + 1])) % 255);
    }
    EXTERN_API void hash_compress_8_str(uchar *src, uchar *rst){
        uchar *mid = new uchar[16];
        hash_compress_16_str(src, mid);
        for (i32 i = 0; i < 8; ++ i)
            rst[i] = (uchar)(calg::abs((i64)mix_2((i32)mid[i * 2], (i32)mid[i * 2 + 1])) % 255);
    }
    EXTERN_API void hash_compress_4_str(uchar *src, uchar *rst){
        uchar *mid = new uchar[8];
        hash_compress_8_str(src, mid);
        for (i32 i = 0; i < 4; ++ i)
            rst[i] = (uchar)(calg::abs((i64)mix_2((i32)mid[i * 2], (i32)mid[i * 2 + 1])) % 255);
    }
    EXTERN_API int hash_file(uchar *fileName, int type){
        //TODO: 文件哈希

        return 1;
    }
}
