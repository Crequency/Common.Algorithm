
# $query = (reg.exe query "HKLM\SOFTWARE\Microsoft\MSBuild\ToolsVersions\4.0" /v MSBuildToolsPath)
# $match = $query -match 'C:\\\S*'
# $path = $match -replace 'MSBuildToolsPath','' -replace 'REG_SZ','' -replace ' ',''

mkdir build
cd build
cmake ..
# & "${path}MSBuild.exe" ./ALL_BUILD.vcxproj
msbuild ./ALL_BUILD.vcxproj
cd ..

