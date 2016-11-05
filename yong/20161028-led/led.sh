#!/usr/bin/sh

cd /sys/class/gpio
echo 4 > export
echo 5 > export
echo 6 > export

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
