#!/bin/bash
basePath=../build/Core
tardi=./bin/Debug/Core
mkdir $tardi
for i in "Hash"; do
    cp $basePath/$i.dll $tardi/
done
sleep 3