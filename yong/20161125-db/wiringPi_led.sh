#! /bin/sh

# author: Yong Choi <sk8er.choi@gmail.com>
# You may register this script at /etc/rc.local

PIN=$1
VALUE=$2
DIR=/home/pi/UKC-CSharp-RaspberryPi_201610/yong/20161125-db

if [ $# -eq 1 ] && [ -f $DIR/$PIN.on ]; then 
    gpio mode $PIN out
    gpio write $PIN 1 
elif [ $# -eq 2 ]; then
    gpio mode $PIN out
    if [ "$VALUE" = "on" ]; then
        gpio write $PIN 1 
        touch $DIR/$PIN.on
    elif [ $VALUE = "off" ]; then
        gpio write $PIN 0
        rm $DIR/$PIN.on
    fi
fi
