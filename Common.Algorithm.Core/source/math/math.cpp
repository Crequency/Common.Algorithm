#include <math.hpp>

namespace Common::Algorithm::Core::Math {

    EXPORTED inline i64 max(i64 x, ...) {

        va_list xs;

        va_start(xs, x);

        i64 tmp = va_arg(xs, i64);

        for (int i = 1; i < x; ++i) {

            i64 cur = va_arg(xs, i64);

            tmp = tmp > cur ? tmp : cur;
        }

        va_end(xs);

        return tmp;
    }

    EXPORTED inline i64 min(i64 x, ...) {

        va_list xs;

        va_start(xs, x);

        i64 tmp = va_arg(xs, i64);

        for (int i = 1; i < x; ++i) {

            i64 cur = va_arg(xs, i64);

            tmp = tmp < cur ? tmp : cur;
        }

        va_end(xs);

        return tmp;
    }

    EXPORTED inline i64 mid(i64 a, i64 b, i64 c) {

        i64 l = max(3, a, b, c), s = min(3, a, b, c);

        if (a != l && a != s) return a;

        if (b != l && b != s) return b;

        if (c != l && c != s) return c;

        return NULL;
    }

    EXPORTED inline void maxin(i64 *max, i64 *min, i64 x, ...) {

        va_list xs;

        va_start(xs, x);

        *max = va_arg(xs, i64), *min = va_arg(xs, i64);

        for (int i = 3; i < x; ++i) {

            i64 cur = va_arg(xs, i64);

            if (cur > *max) *max = cur;

            if (cur < *min) *min = cur;
        }

        va_end(xs);
    }

    EXPORTED inline i64 absolute(i64 x) {

        return x < 0 ? -x : x;
    }

    EXPORTED inline i64 power(i64 x, i64 t) {

        i64 ans = 1;

        while (t--) ans *= x;

        return ans;
    }

    EXPORTED inline i64 gcd(i64 a, i64 b) {

        if (a == b) return a;

        if (a < b) { i64 tmp = a; a = b, b = tmp; }

        i64 dis = max(a, b) - min(a, b);

        return gcd(dis, min(a, b));
    }

    EXPORTED i64 gobit(i64 x, i32 len) {

        i64 ans = 0, cx = x;

        for (i32 i = 0, p = 0; i < len; ++i, ++p) {

            if (cx & 1) ans += power(2, p);
            else ans <<= 1;

            cx >>= 1;
        }

        return ans;
    }

}
