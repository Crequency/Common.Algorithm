#include "Hash.h"

namespace calg{
    EXTERN_API int extern_test_getnum(){ return 1; }

    const int hash_length = 2048;
    const int hash_block_length = 128;

    inline bool cmp_a(uchar a, uchar b){ return a < b; }
    inline bool cmp_b(uchar a, uchar b){ return a > b; }

    EXTERN_API void hash_str(uchar *src, uchar *rst){
        memset(rst, '0', sizeof(uchar) * 2048);
        std::string str(re_ca_charc(src));
        if (str.length() <= hash_length){
            int index = hash_length - 1;
            for (int i = st_ca_int(str.length()) - 1;
                 i >= 0; -- i, -- index){
                rst[index] = str[i];
            }
            uchar fill = 0;
            for (int i = 0; i < index; ++ i, fill = fill == 255 ? 0 : fill + 1){
                rst[i] = fill;
            }
        } else{
            long long block = str.length() / hash_block_length;
            long long remain = str.length() - block * hash_block_length;
            uchar *blocks[hash_block_length + 1]{};
            for (int i = 1; i <= hash_block_length; ++ i)
                blocks[i] = new uchar[block + 1];
            for (int i = 1, cur = 0; i <= hash_block_length; ++ i)
                for (int j = 1; j <= block; ++ j, ++ cur)
                    blocks[i][j] = rst[cur];
            //TODO: 添加CUDA选择器, 可选是否使用CUDA操作
            for (int i = 1; i <= block; ++ i){
                ull eq_a = 1, eq_b = 1, eq_c = 1, eq_d = 1;
                for (int k = 1; k <= 32; ++ k)
                    eq_a *= (st_ca<int>(blocks[k][i]) << 1);
                for (int k = 32 + 1; k <= 64; ++ k)
                    eq_b *= (st_ca<int>(blocks[k][i]) << 1);
                for (int k = 64 + 1; k <= 96; ++ k)
                    eq_c *= (st_ca<int>(blocks[k][i]) << 1);
                for (int k = 96 + 1; k <= 128; ++ k)
                    eq_d *= (st_ca<int>(blocks[k][i]) << 1);
                eq_a %= 255, eq_b %= 255, eq_c %= 255, eq_d %= 255;
                ull *eq_arr[4] = {&eq_a, &eq_b, &eq_c, &eq_d};
                for (int i = hash_length, index = 0;
                     i < st_ca_int(str.length());
                     ++ i, index = (index == 3 ? 0 : index + 1)){
                    (*eq_arr[index]) += str[i];
                    (*eq_arr[index]) <<= 1;
                }
                ull ans = ((eq_a * 32) % 256) * ((eq_b * 32 * 32) % 256);
                ans %= 256; ans *= ((eq_c * st_ca_int(std::pow(32, 3))) % 256);
                ans %= 256; ans *= ((eq_c * st_ca_int(std::pow(32, 4))) % 256);
                ans %= 256; rst[i] = st_ca<char>(ans);
            }
            for (int i = 1; i <= block; ++ i)
                delete(blocks[i]);
        }

        /* ============================= 以下是真哈希部分 ============================= */

        for (int i = 2; i <= hash_length - 2; ++ i){
            uchar a = rst[i - 1], b = rst[i], c = rst[i + 1], tmp;
            tmp = a, a = c, c = tmp;
            b = std::max(std::max(a * c, a * b), b * c) % 255;
            a *= b, c *= b;
            a = c - b, c = a - b;
            rst[i - 1] = (uchar)std::pow(a * c - a - c, c % 5) % 255;
            rst[i] = (uchar)std::pow(b - a - c, a % 5) % 255;
            rst[i + 1] = (uchar)std::pow(c * b - c - b, b % 5) % 255;
            std::sort(rst + i - 2, rst + i + 2, b % 2 == 0 ? cmp_a : cmp_b);
            rst[i + 2] += rst[i - 1] >= rst[i - 2] ? rst[i + 1] <<= 1 : rst[i] >>= 1;
            rst[i - 1] -= rst[i + 2] % 3 == 2 ? rst[i] + (rst[i + 2] >>= 1) :
                (uchar)((rst[i - 2] + (int)std::pow(a + b, c)) % 255);
            rst[i] *= rst[i + 2] - rst[i - 1];
            rst[i - 2] += rst[i] + (rst[i - 1] <<= 1);
            rst[i + 1] = rst[i + 2] ^ rst[i - 1] + rst[i] & rst[i - 2];

            rst[i - 2] -= rst[i + 1] - rst[i] - rst[i - 1] - rst[i - 2];
            rst[i - 1] -= rst[i] - rst[i - 1] - rst[i - 2];
            rst[i] -= rst[i - 1] - rst[i - 2];
            rst[i + 1] -= rst[i - 2];
            rst[i + 2] <<= 1;
            rst[i - 2] >>= 1;
            rst[i - 1] += rst[i - 2];
            rst[i] += rst[i - 1] - rst[i - 2];
            rst[i + 1] += rst[i] - rst[i - 1] - rst[i - 2];
            rst[i + 2] += rst[i + 1] - rst[i] - rst[i - 1] - rst[i - 2];
        }

        //  解决小于 2048 时前导 '0' 重复导致一摸一样哈希的问题
        for (int i = 0, j = hash_length - 1, launched = 1;
             i != j && i < j && i != j - 1;
             ++ i, ++ launched, j -= ((~rst[i + 1] ^ rst[i]) % 3 == 0 ? 1 : 2)){
            rst[i] += rst[j] * rst[j - 1];
            rst[i] >>= (rst[i + 1] % 4);
            rst[i + 1] = rst[i] & (rst[j - 1] + rst[j]);
            rst[i + 2] = rst[i + 1] | (rst[j - 1] & rst[i]);
            rst[i] += rst[i] ^ rst[j];
            rst[i] += (rst[0] &= rst[j]) ^ (rst[hash_length - 1] >>= 1);
            rst[0] >>= 1;
            rst[j] -= rst[i] ^ rst[i + 1];
            rst[j - 1] -= (rst[j - 1] + ((rst[(j + i) >> 1] ^
                ~rst[((rst[i] * rst[j] - rst[i] - rst[j]) % 2048) >> 1]) >> 1));
            rst[0] = rst[i] + rst[j];
            rst[hash_length - 1] = rst[i] - rst[j];
            if (launched >= 2048) break;
        }

        // 四指针混淆器
        /*for (int i = 1023, j = 1024, x = 0, y = 2048 - 1;
             i != 0 && j != 2048 - 1 && x != 1023 && y != 1024 && i < j && x < y;
             -- i, ++ j, ++ x, -- y){

            if(i >= 2){
                rst[i - 1] = rst[i] | st_ca_int(std::sin(rst[j]));
                rst[i - 2] = rst[i - 1] & st_ca_int(std::sin(rst[j - 1]));
                rst[i] = (rst[i - 1] & rst[i - 2]) ^ st_ca_int(std::cos(rst[i - 1] | rst[i - 2]));
            }
            if(j <= 2048 - 3){
                rst[j + 1] = rst[j] & st_ca_int(std::cos(rst[i]));
                rst[j + 2] = rst[j - 1] | st_ca_int(std::cos(rst[i + 1]));
                rst[j] = (rst[j + 1] | rst[j + 2]) ^ st_ca_int(std::sin(rst[j + 1] & rst[j + 2]));
            }
            rst[x] = rst[x] * ((rst[i] * rst[j] + (rst[i] & rst[j]) - (rst[j] ^ rst[i])) % 255);
            rst[y] = rst[y] * ((rst[i] * rst[j] - (rst[i] | rst[j]) - (rst[i] ^ rst[j])) % 255);
        }*/
    }

    EXTERN_API void hash_compress_128_str(uchar *src, uchar *rst)
    {

    }

    EXTERN_API void hash_compress_64_str(uchar *src, uchar *rst){
        uchar *f = new uchar[hash_length]; size_t len = sizeof(uchar) * 1024;
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
        }
    }

    EXTERN_API void hash_compress_32_str(uchar *src, uchar *rst)
    {

    }

    EXTERN_API void hash_compress_16_str(uchar *src, uchar *rst)
    {

    }

    EXTERN_API void hash_compress_8_str(uchar *src, uchar *rst)
    {

    }

    EXTERN_API void hash_compress_4_str(uchar *src, uchar *rst)
    {

    }

    EXTERN_API int hash_file(uchar *fileName, int type){
        //TODO: 文件哈希

        return 1;
    }
}
