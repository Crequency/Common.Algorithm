
cppOut=Common.Algorithm/out/build/x64-Debug/
desDir=Common.Algorithm.Interop.Test/bin/Debug/net6.0/Core

rm -r $desDir
mkdir $desDir

cp ${cppOut}Hash/Hash.dll $desDir
cp ${cppOut}Math/Math.dll ${desDir}/
