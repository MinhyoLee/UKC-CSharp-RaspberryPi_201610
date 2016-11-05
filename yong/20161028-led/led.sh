#!/bin/bash

cd /sys/class/gpio

if [ -d "gpio4" ];then
    echo 4 > unexport
fi
echo 4 > export

if [ -d "gpio5" ];then
    echo 5 > unexport
fi
echo 5 > export

if [ -d "gpio6" ];then
    echo 6 > unexport
fi
echo 6 > export

echo out > gpio4/direction
echo out > gpio5/direction
echo out > gpio6/direction

for i in {0..4}
do
    echo 1 > gpio4/value
    sleep 1
    echo 1 > gpio5/value
    sleep 1
    echo 1 > gpio6/value
    sleep 1
    echo 0 > gpio4/value
    sleep 1
    echo 0 > gpio5/value
    sleep 1
    echo 0 > gpio6/value
    sleep 1
done

echo 4 > unexport
echo 5 > unexport
echo 6 > unexport
