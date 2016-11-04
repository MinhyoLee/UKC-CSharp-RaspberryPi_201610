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
        private byte[] LedAdress
        {
            get;
            set;
        }

        private DispatcherTimer timer;         // create a timer

        public MainPage()
        {
            this.InitializeComponent();

            this.InitSPI();
            this.InitLedAdress();
            this.initTimer();
        }

        private void initTimer()
        {
            // read timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200); //sample every 200mS
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            //checked Button status
            UInt16 Inputs = MCP23S17.ReadRegister16();

            if ((Inputs & 1 << PFDII.Sw0) == 0
                || (Inputs & 1 << PFDII.Sw1) == 0
                || (Inputs & 1 << PFDII.Sw2) == 0
                || (Inputs & 1 << PFDII.Sw3) == 0
                )
            {
                txtButtonStatusS.Text = "버튼눌림";
                MCP23S17.WritePin(LedAdress[0], MCP23S17.On);
                this.CheckLedStatus(LedAdress[0]);
            }
            else
            {
                txtButtonStatusS.Text = "";
                MCP23S17.WritePin(LedAdress[0], MCP23S17.Off);
                this.CheckLedStatus(LedAdress[0]);
            }
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
            LedAdress = new byte[8];
            LedAdress[0] = PFDII.LED0;
            LedAdress[1] = PFDII.LED1;
            LedAdress[2] = PFDII.LED2;
            LedAdress[3] = PFDII.LED3;
            LedAdress[4] = PFDII.LED4;
            LedAdress[5] = PFDII.LED5;
            LedAdress[6] = PFDII.LED6;
            LedAdress[7] = PFDII.LED7;
        }

        private void LedSwitch_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = (Button)sender;
            byte toBeLedStatus = ((SolidColorBrush)pressedButton.Background).Color.Equals(Colors.Red) ? MCP23S17.Off : MCP23S17.On;
            int ledNo = Convert.ToInt32(pressedButton.Name.Substring(9, 1));

            MCP23S17.WritePin(LedAdress[ledNo], toBeLedStatus);

            pressedButton.Background = this.CheckLedStatus(LedAdress[ledNo]);
        }

        private Brush CheckLedStatus(byte led)
        {
            UInt16 Inputs = MCP23S17.ReadRegister16();

            return ((Inputs & 1 << led) != 0) ? new SolidColorBrush(Windows.UI.Colors.Red) : new SolidColorBrush(Windows.UI.Colors.LightGray);
        }



        private void ButtonClick()
        {

        }
    }
}
