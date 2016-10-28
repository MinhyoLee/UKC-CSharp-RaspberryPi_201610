using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿은 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 에 문서화되어 있습니다.

namespace PifaceLedControl
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        byte[] ledAdress
        {
            get;
            set;
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.InitSPI();
            this.InitLedAdress();
        }

        private async void InitSPI()
        {
            try
            {
                await MCP23S17.InitSPI();

                MCP23S17.InitMCP23S17();
                MCP23S17.setPinMode(0x00FF); // 0x0000 = all outputs, 0xffff=all inputs, 0x00FF is PIFace Default
                MCP23S17.pullupMode(0x00FF); // 0x0000 = no pullups, 0xffff=all pullups, 0x00FF is PIFace Default
                MCP23S17.WriteWord(0x0000); // 0x0000 = no pullups, 0xffff=all pullups, 0x00FF is PIFace Default
            }
            catch (Exception ex)
            {

            }
        }

        private void InitLedAdress()
        {
            ledAdress = new byte[8];

            ledAdress[0] = PFDII.LED0;
            ledAdress[1] = PFDII.LED1;
            ledAdress[2] = PFDII.LED2;
            ledAdress[3] = PFDII.LED3;
            ledAdress[4] = PFDII.LED4;
            ledAdress[5] = PFDII.LED5;
            ledAdress[6] = PFDII.LED6;
            ledAdress[7] = PFDII.LED7;
        }

        private void LedSwitch_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = (Button)sender;
            byte toBeLedStatus = ((SolidColorBrush)pressedButton.Background).Color.Equals(Colors.Red)
                ? MCP23S17.Off
                : MCP23S17.On;

            switch (pressedButton.Name)
            {
                case "LedSwitch0":
                    MCP23S17.WritePin(PFDII.LED0, toBeLedStatus);
                    break;
                case "LedSwitch1":
                    MCP23S17.WritePin(PFDII.LED1, toBeLedStatus);
                    break;
                case "LedSwitch2":
                    MCP23S17.WritePin(PFDII.LED2, toBeLedStatus);
                    break;
                case "LedSwitch3":
                    MCP23S17.WritePin(PFDII.LED3, toBeLedStatus);
                    break;
                case "LedSwitch4":
                    MCP23S17.WritePin(PFDII.LED4, toBeLedStatus);
                    break;
                case "LedSwitch5":
                    MCP23S17.WritePin(PFDII.LED5, toBeLedStatus);
                    break;
                case "LedSwitch6":
                    MCP23S17.WritePin(PFDII.LED6, toBeLedStatus);
                    break;
                case "LedSwitch7":
                    MCP23S17.WritePin(PFDII.LED7, toBeLedStatus);
                    break;
            }

            this.CheckLed();
        }

        private void CheckLed()
        {
            UInt16 Inputs = MCP23S17.ReadRegister16();

            LedSwitch0.Background = ((Inputs & 1 << PFDII.LED0) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);
            LedSwitch1.Background = ((Inputs & 1 << PFDII.LED1) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);
            LedSwitch2.Background = ((Inputs & 1 << PFDII.LED2) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);
            LedSwitch3.Background = ((Inputs & 1 << PFDII.LED3) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);
            LedSwitch4.Background = ((Inputs & 1 << PFDII.LED4) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);
            LedSwitch5.Background = ((Inputs & 1 << PFDII.LED5) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);
            LedSwitch6.Background = ((Inputs & 1 << PFDII.LED6) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);
            LedSwitch7.Background = ((Inputs & 1 << PFDII.LED7) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);

        }
    }
}
