
$fileName="Common.Algorithm.Core.dll"
$srcDir="build/Debug/"
$desDir="build/tests/Debug/"

rm ${desDir}${fileName}
cp ${srcDIr}${fileName} ${desDir}

& ${desDir}Common.Algorithm.Test.exe
