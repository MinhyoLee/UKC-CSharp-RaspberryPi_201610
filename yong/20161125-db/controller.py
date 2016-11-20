#!/usr/bin/python3

import os
import pifacecad

cad = pifacecad.PiFaceCAD()
cad.lcd.clear()
cad.lcd.backlight_on()

while True:
    if cad.switches[0].value == 1:
        cad.lcd.write("20161028-led/\nled.sh")
        os.system('ssh pi@rpi3 "sudo /home/pi/UKC-CSharp-RaspberryPi_201610/yong/20161028-led/led.sh"')
    if cad.switches[1].value == 1:
        cad.lcd.write("20161104-button-\nled/GPIO/simple")
        os.system("ssh pi@rpi3 /home/pi/UKC-CSharp-RaspberryPi_201610/yong/20161104-button-led/GPIO/simple")

    # wiringPi
    if cad.switches[2].value == 1:
        cad.lcd.write("20161125\nLED ON...")
        os.system('ssh pi@rpi3 "/home/pi/UKC-CSharp-RaspberryPi_201610/yong/20161125-db/wiringPi_led.sh 21 on"')
    if cad.switches[3].value == 1:
        cad.lcd.write("20161125\nLED OFF...")
        os.system('ssh pi@rpi3 "/home/pi/UKC-CSharp-RaspberryPi_201610/yong/20161125-db/wiringPi_led.sh 21 off"')

    # reboot
    if cad.switches[4].value == 1:
        cad.lcd.write("20161125\nRebooting...")
        os.system('ssh pi@rpi3 "sudo reboot"')

cad.lcd.clear()
cad.lcd.backlight_off()
