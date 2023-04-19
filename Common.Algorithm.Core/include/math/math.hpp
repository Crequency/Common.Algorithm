#include <base.h>

namespace Common::Algorithm::Core::Math {

    const long double pi = 3.14159265358979323846;

    const long double e = 2.71828182845904523536;

    EXTERN_ALL inline i64 max(i64 x, ...);

    EXTERN_ALL inline i64 min(i64 x, ...);

    EXTERN_ALL inline i64 mid(i64 a, i64 b, i64 c);

    EXTERN_ALL inline void maxin(i64 *max, i64 *min, i64 x, ...);

    EXTERN_ALL inline i64 absolute(i64 x);

    EXTERN_ALL inline i64 power(i64 x, i64 t);

    EXTERN_ALL inline i64 gcd(i64 a, i64 b);

    EXTERN_ALL inline i64 gobit(i64 x, i32 len);

}
