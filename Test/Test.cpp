#include "../Base/stdc++.h"

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

int main(){
    int a = 5, b = 3, c = 4;
    printf("%lld\n", min(3, a, b, c));
    return 0;
}
