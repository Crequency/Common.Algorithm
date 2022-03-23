#!/bin/bash

echo "Start building up project, copying files ... "

buildup="./build/"
cmake="./out/build/x64-debug/"
com_cspro="bin/Debug/net6.0"
binFiles="./build/"
coreFiles="./build/Core"

mkdir $binFiles
mkdir $coreFiles

echo "Copying CS Dll Project:"
for i in "Algorithm.Interop"; do
    echo "    $i"
    echo "        cp ./$i/$com_cspro/$i.dll $binFiles"
    cp ./$i/$com_cspro/$i.dll $binFiles
done

echo "Copying CMake Algorithm Project:"
for i in "Hash"; do
    echo "    $i"
    echo "        cp $cmake/$i/$i.dll $coreFiles"
    cp $cmake/$i/$i.dll $coreFiles
done

sleep 3
