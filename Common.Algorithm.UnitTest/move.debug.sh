#!/bin/bash
basePath=../build/Core
tardi=./bin/Debug/Core
mkdir $tardi
for i in "Hash" "Math"; do
    rm -f $tardi/$i.dll
    cp $basePath/$i.dll $tardi/
    echo "cp $basePath/$i.dll $tardi/$i.dll"
done
sleep 3