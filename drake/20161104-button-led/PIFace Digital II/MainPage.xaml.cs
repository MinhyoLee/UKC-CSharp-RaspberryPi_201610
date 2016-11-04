using System;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using SPI_GPIO;
using Windows.UI.Xaml.Input;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PIFace_Digital_II
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;         // create a timer
        private const byte Off = MCP23S17.Off;
        private const byte On = MCP23S17.On;
        private int counts = 0;
        public MainPage()
        {
            this.InitializeComponent();
            /* Register for the unloaded event so we can clean up upon exit */
            Unloaded += MainPage_Unloaded;

            /* Initialize GPIO, SPI, and the display */
            InitAll(); 

        }
        private void MainPage_Unloaded(object sender, object args)
        {
            /* Cleanup */
        }

        private void initTimer()
        {
            // read timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200); //sample every 200mS
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        // read GPIO and display it
        private void Timer_Tick(object sender, object e)
        {
            LightLEDs(MCP23S17.ReadRegister16());    // do something with the values
        }

        /* Initialize GPIO, SPI, and the display */
        private async void InitAll()
        {
            try
            {
                await MCP23S17.InitSPI();

                MCP23S17.InitMCP23S17();
                MCP23S17.setPinMode(0x00FF); // 0x0000 = all outputs, 0xffff=all inputs, 0x00FF is PIFace Default
                MCP23S17.pullupMode(0x00FF); // 0x0000 = no pullups, 0xffff=all pullups, 0x00FF is PIFace Default
                MCP23S17.WriteWord(0x0000); // 0x0000 = no pullups, 0xffff=all pullups, 0x00FF is PIFace Default
                Switch0.AddHandler(PointerPressedEvent, new PointerEventHandler(Switch0_PointerPressed), true);
                Switch0.AddHandler(PointerReleasedEvent, new PointerEventHandler(Switch0_PointerReleased), true);
                Switch1.AddHandler(PointerPressedEvent, new PointerEventHandler(Switch1_PointerPressed), true);
                Switch1.AddHandler(PointerReleasedEvent, new PointerEventHandler(Switch1_PointerReleased), true);
                Switch2.AddHandler(PointerPressedEvent, new PointerEventHandler(Switch2_PointerPressed), true);
                Switch2.AddHandler(PointerReleasedEvent, new PointerEventHandler(Switch2_PointerReleased), true);
                Switch3.AddHandler(PointerPressedEvent, new PointerEventHandler(Switch3_PointerPressed), true);
                Switch3.AddHandler(PointerReleasedEvent, new PointerEventHandler(Switch3_PointerReleased), true);

                initTimer();
            }
            catch (Exception ex)
            {
                //Text_Status.Text = "Exception: " + ex.Message;
                //if (ex.InnerException != null)
                //{
                //    Text_Status.Text += "\nInner Exception: " + ex.InnerException.Message;
                //}
                //return;
            }
        }

        private void LightLEDs(UInt16 Inputs)
        {
            image0.Source = ((Inputs & 1 << PFDII.IN0) != 0 && !Switch0.IsPressed) ? imageOn.Source : imageOff.Source;
            image1.Source = ((Inputs & 1 << PFDII.IN1) != 0 && !Switch1.IsPressed) ? imageOn.Source : imageOff.Source;
            image2.Source = ((Inputs & 1 << PFDII.IN2) != 0 && !Switch2.IsPressed) ? imageOn.Source : imageOff.Source;
            image3.Source = ((Inputs & 1 << PFDII.IN3) != 0 && !Switch3.IsPressed) ? imageOn.Source : imageOff.Source;
            image4.Source = ((Inputs & 1 << PFDII.IN4) != 0) ? imageOn.Source : imageOff.Source;
            image5.Source = ((Inputs & 1 << PFDII.IN5) != 0) ? imageOn.Source : imageOff.Source;
            image6.Source = ((Inputs & 1 << PFDII.IN6) != 0) ? imageOn.Source : imageOff.Source;
            image7.Source = ((Inputs & 1 << PFDII.IN7) != 0) ? imageOn.Source : imageOff.Source;

            image0_Out.Source = ((Inputs & 1 << PFDII.LED0) != 0) ? imageROn.Source : imageROff.Source;
            image1_Out.Source = ((Inputs & 1 << PFDII.LED1) != 0) ? imageROn.Source : imageROff.Source;
            image2_Out.Source = ((Inputs & 1 << PFDII.LED2) != 0) ? imageROn.Source : imageROff.Source;
            image3_Out.Source = ((Inputs & 1 << PFDII.LED3) != 0) ? imageROn.Source : imageROff.Source;
            image4_Out.Source = ((Inputs & 1 << PFDII.LED4) != 0) ? imageROn.Source : imageROff.Source;
            image5_Out.Source = ((Inputs & 1 << PFDII.LED5) != 0) ? imageROn.Source : imageROff.Source;
            image6_Out.Source = ((Inputs & 1 << PFDII.LED6) != 0) ? imageROn.Source : imageROff.Source;
            image7_Out.Source = ((Inputs & 1 << PFDII.LED7) != 0) ? imageROn.Source : imageROff.Source;

            if((Inputs & 1 << PFDII.IN0) == 0) 
            {
                LED0_Checked(this, null);
                LED4_Checked(this, null);
            }
            else
            {
                LED0_Unchecked(this, null);
                LED4_Unchecked(this, null);

            }
            if ((Inputs & 1 << PFDII.IN1) == 0)
            {
                LED1_Checked(this, null);
                LED5_Checked(this, null);
            }
            else
            {
                LED1_Unchecked(this, null);
                LED5_Unchecked(this, null);
            }
            if ((Inputs & 1 << PFDII.IN2) == 0)
            {
                LED2_Checked(this, null);
                LED6_Checked(this, null);
            }
            else
            {
                LED2_Unchecked(this, null);
                LED6_Unchecked(this, null);
            }
            if ((Inputs & 1 << PFDII.IN3) == 0)
            {
                LED3_Checked(this, null);
                LED7_Checked(this, null);
            }
            else
            {
                LED3_Unchecked(this, null);
                LED7_Unchecked(this, null);
            }
        }


        //Note RelayA = R1, RelayB = R0
        private void RelayA_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.RelayA , On);
            RelayAImage.Source =  RelayOn.Source;
            LED1.IsChecked = true; ;
        }

        private void RelayB_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.RelayB, On);
            RelayBImage.Source = RelayOn.Source;
            LED0.IsChecked = true; ;
        }

        private void RelayA_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.RelayA, Off);
            RelayAImage.Source = RelayOff.Source;
            LED1.IsChecked = false; ;
        }

        private void RelayB_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.RelayB, Off);
            RelayBImage.Source = RelayOff.Source;
            LED0.IsChecked = false; ;
        }

        private void LED0_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED0, On);
            RelayB.IsChecked = true; // LED0 and RelayA are the same output pin
        }

        private void LED1_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED1, On);
            RelayA.IsChecked = true; // LED1 and RelayB are the same output pin
        }

        private void LED2_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED2, On);
        }

        private void LED3_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED3, On);
        }

        private void LED4_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED4, On);
        }

        private void LED5_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED5, On);
        }

        private void LED6_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED6, On);
        }

        private void LED7_Checked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED7, On);
        }

        private void LED0_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED0, Off);
            RelayB.IsChecked = false; // LED0 and RelayA are the same output pin

        }

        private void LED1_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED1, Off);
            RelayA.IsChecked = false; // LED1 and RelayB are the same output pin
        }

        private void LED2_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED2, Off);
        }

        private void LED3_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED3, Off);
        }

        private void LED4_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED4, Off);
        }

        private void LED5_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED5, Off);
        }

        private void LED6_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED6, Off);
        }

        private void LED7_Unchecked(object sender, RoutedEventArgs e)
        {
            MCP23S17.WritePin(PFDII.LED7, Off);
        }

        private void Switch0_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Switch0.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        }

        private void Switch0_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Switch0.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
        }
        private void Switch1_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Switch1.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        }

        private void Switch1_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Switch1.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
        }
        private void Switch2_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Switch2.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        }

        private void Switch2_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Switch2.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
        }
        private void Switch3_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Switch3.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        }

        private void Switch3_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Switch3.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit(); // exit the app
        }
    } // End of Class
} // End of NS
