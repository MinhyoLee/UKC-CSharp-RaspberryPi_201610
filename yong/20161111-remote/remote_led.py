#!/usr/bin/python3

import os
import pifacecad

cad = pifacecad.PiFaceCAD()
cad.lcd.backlight_on()

while True:
    cad.lcd.clear()
    if cad.switches[0].value == 1:
        cad.lcd.write("20161028-led/\nled.sh")
        os.system('ssh pi@rpi3 "sudo /home/pi/UKC-CSharp-RaspberryPi_201610/yong/20161028-led/led.sh"')
    if cad.switches[1].value == 1:
        cad.lcd.write("20161104-button-\nled/GPIO/simple")
        os.system("ssh pi@rpi3 /home/pi/UKC-CSharp-RaspberryPi_201610/yong/20161104-button-led/GPIO/simple")
    if cad.switches[4].value == 1:
        break

cad.lcd.clear()
cad.lcd.backlight_off()
