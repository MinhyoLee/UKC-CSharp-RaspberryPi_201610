#!/bin/sh

cd /sys/class/gpio
if [ ! -d "gpio4" ];then
    echo 4 > export
fi
if [ ! -d "gpio5" ];then
    echo 5 > export
fi
if [ ! -d "gpio6" ];then
    echo 6 > export
fi

echo out > gpio4/direction
echo out > gpio5/direction
echo out > gpio6/direction

while true
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
