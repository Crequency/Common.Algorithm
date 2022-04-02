#include "../Base/stdc++.h"
#include "../Math/Math.h"

#define ll long long

inline ll min(ll x, ...)
{
    va_list xs;
    va_start(xs, x);
    ll tmp = va_arg(xs, ll);
    for (int i = 1; i < x; ++ i){
        ll cur = va_arg(xs, ll);
        tmp = tmp < cur ? tmp : cur;
    }
    va_end(xs);
    return tmp;
}

//  µ¯»Éº¯Êý
inline long double spring_func(long double x){
    long double A = std::sinl(calg::pi * log2l(calg::abs(x * 2 + 1)));
    long double B = std::powl(2, x * 2) + std::powl(calg::e, x * 4);
    return std::cosl(A + B);
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

int main(){
    /*int a = 5, b = 3, c = 4;
    printf("%lld\n", min(3, a, b, c));*/

    //for (long double i = 0; i < 1; i += 0.0001){
    //    //std::cout << log2l(calg::abs(i * 2 + 1)) << std::endl;
    //    std::cout << i << " >> " << spring_func(i) << std::endl;
    //}

    std::cout << gobit(11, 2) << std::endl;
    return 0;
}
