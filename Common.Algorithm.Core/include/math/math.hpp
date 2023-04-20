#include <base.h>

namespace Common::Algorithm::Core::Math {

    const long double pi = 3.14159265358979323846;

    const long double e = 2.71828182845904523536;

    EXPORTED i64 max(i64 x, ...);

    EXPORTED i64 min(i64 x, ...);

    EXPORTED i64 mid(i64 a, i64 b, i64 c);

    EXPORTED void maxin(i64 *max, i64 *min, i64 x, ...);

    EXPORTED i64 absolute(i64 x);

    EXPORTED i64 power(i64 x, i64 t);

    EXPORTED i64 gcd(i64 a, i64 b);

    EXPORTED i64 gobit(i64 x, i32 len);

}
