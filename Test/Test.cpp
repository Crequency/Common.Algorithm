#include "../Base/stdc++.h"
typedef __int32 i32;
typedef __int64 i64;
namespace calg{
    const long double pi = 3.14159265358979323846;
    const long double e = 2.71828182845904523536;
    i64 max(i64 x, ...){
        va_list xs;
        va_start(xs, x);
        i64 tmp = va_arg(xs, i64);
        for (int i = 1; i < x; ++ i){
            i64 cur = va_arg(xs, i64);
            tmp = tmp > cur ? tmp : cur;
        }
        va_end(xs);
        return tmp;
    }
    i64 min(i64 x, ...){
        va_list xs;
        va_start(xs, x);
        i64 tmp = va_arg(xs, i64);
        for (int i = 1; i < x; ++ i){
            i64 cur = va_arg(xs, i64);
            tmp = tmp < cur ? tmp : cur;
        }
        va_end(xs);
        return tmp;
    }
    i64 mid(i64 a, i64 b, i64 c){
        i64 l = max(3, a, b, c), s = min(3, a, b, c);
        if (a != l && a != s) return a;
        if (b != l && b != s) return b;
        if (c != l && c != s) return c;
        return NULL;
    }
    void maxin(i64 *max, i64 *min, i64 x, ...){
        va_list xs; va_start(xs, x);
        *max = va_arg(xs, i64), *min = va_arg(xs, i64);
        for (int i = 3; i < x; ++ i){
            i64 cur = va_arg(xs, i64);
            if (cur > *max) *max = cur;
            if (cur < *min) *min = cur;
        }
        va_end(xs);
    }
    i64 abs(i64 x){ return x < 0 ? -x : x; }
    i64 pow(i64 x, i64 t){
        i64 ans = 1; while (t--) ans *= x; return ans;
    }
    i64 gcd(i64 a, i64 b){
        if (a == b) return a;
        if (a < b){ i64 tmp = a; a = b, b = tmp; }
        i64 dis = max(a, b) - min(a, b);
        return gcd(dis, min(a, b));
    }
    i64 gobit(i64 x, i32 len){
        i64 ans = 0, cx = x;
        for (i32 i = 0, p = 0; i < len; ++ i, ++ p){
            if (cx & 1) ans += pow(2, p);
            else ans <<= 1;
            cx >>= 1;
        }
        return ans;
    }
}
using namespace calg;
i32 mix_3(i32 a, i32 b, i32 c){
    i64 t_max, t_min;
    calg::maxin(&t_max, &t_min, 3, a, b, c);
    i32 max = (i32)t_max, min = (i32)t_min;
    if (a == max) return ((i64)(a * b * c) - min) % INT32_MAX;
    else if (c - a > b) return (a * c + (b + c) * a) % INT32_MAX;
    else if (c - a < b) return ((i64)(b * c - b - c) + calg::pow(a, 2) % INT32_MAX);
    else return ((i64)max * min + (max ^ min) * calg::mid(a, b, c)) % INT32_MAX;
}
i32 mix_5(i32 a, i32 b, i32 c, i32 d, i32 e){
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
void exp_1(i32 x, i32 *a, i32 *b, i32 *c){
    i32 ea = (x << 1) & 114514, eb = x ^ 1919, ec = (x >> 1) & 810;
    i32 ca = gobit(x, 10), cb = gobit(x >> 10, 10), cc = gobit(x >> 20, 10);
    *a = (ea ^ ca) >> 1, *b = (eb ^ cb) >> 1, *c = (ec ^ cc) >> 1;
}
void exp_1(i32 x, i32 *a, i32 *b, i32 *c, i32 *d){
    i32 ea = (x << 1) & 114514, eb = x ^ 1919, ec = (x >> 1) & 810, ed = x;
    i32 ca = gobit(x, 8), cb = gobit(x >> 8, 8), cc = gobit(x >> 16, 8), cd = gobit(x >> 24, 8);
    *a = (ea ^ ca) >> 1, *b = (eb ^ cb) >> 1, *c = (ec ^ cc) >> 1, *d = (ed ^ cd) >> 1;
}
long double spring_func(long double x){
    long double A = sinl(calg::pi * log2l(calg::abs(x * 2 + 1)));
    long double B = powl(2, x * 2) + powl(calg::e, x * 4);
    return cosl(A + B);
}

int main(){
    int a, b, c, d, e, f, g, h, i, j, k, l;
    scanf("%d %d %d %d %d %d %d %d %d", &a, &b, &c, &d, &e, &f, &g, &h, &i);
    exp_1(i, &j, &k, &l);
    printf("%d %d %d %d %d", mix_3(a, b, c), mix_5(d, e, f, g, h), j, k, l);
    return 0;
}
