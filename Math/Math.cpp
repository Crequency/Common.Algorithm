#include "Math.h"

namespace calg{
    EXTERN_API inline ll max(ll x, ...){
        va_list xs;
        va_start(xs, x);
        ll tmp = va_arg(xs, ll);
        for (int i = 1; i < x; ++ i){
            ll cur = va_arg(xs, ll);
            tmp = tmp > cur ? tmp : cur;
        }
        va_end(xs);
        return tmp;
    }
    EXTERN_API inline ll min(ll x, ...){
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
    EXTERN_API inline ll abs(ll x){ return x < 0 ? -x : x; }
    EXTERN_API inline ll pow(ll x, ll t){
        ll ans = 1; while (t--) ans *= x; return ans;
    }

    EXTERN_API inline ll gcd(ll a, ll b){
        if (a == b) return a;
        if (a < b){ ll tmp = a; a = b, b = tmp; }
        ll dis = max(a, b) - min(a, b);
        return gcd(dis, min(a, b));
    }
}
