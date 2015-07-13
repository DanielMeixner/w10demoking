using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Devices.Midi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private VM MyPageVM;

        
        public MainPage()
        {
            this.InitializeComponent();
            MyPageVM = App.Current.Resources["MyVM"] as VM;

            MyPageVM.PropertyChanged += MyPageVM_PropertyChanged;

            StartMidiStuff();

            MyCCSlider.ControlValueChanged += MyCCSlider_ControlValueChanged;
            My13Slider.ControlValueChanged += My13Slider_ControlValueChanged;
            My14Slider.ControlValueChanged += My14Slider_ControlValueChanged;
            My15Slider.ControlValueChanged += My15Slider_ControlValueChanged;
        }

        private void My14Slider_ControlValueChanged(object sender, EventArgs e)
        {
            MySliderEventArgs sea = (MySliderEventArgs)e;
            SendCCValue(14, sea.NewValue);
        }
        private void My15Slider_ControlValueChanged(object sender, EventArgs e)
        {
            MySliderEventArgs sea = (MySliderEventArgs)e;
            SendCCValue(15, sea.NewValue);
        }


        private void My13Slider_ControlValueChanged(object sender, EventArgs e)
        {
            MySliderEventArgs sea = (MySliderEventArgs)e;
            SendCCValue(13, sea.NewValue);
        }

        private void MyCCSlider_ControlValueChanged(object sender, EventArgs e)
        {
            MySliderEventArgs sea = (MySliderEventArgs)e;
            SendCCValue(1, sea.NewValue);
        }

      

        private async void SendCCValue(int ctrl, int ccvalue)
        {
            string device = cb.SelectedValue as string;
            //var device = devices[2];
            IMidiOutPort port = null;

            

            try
            {
                port = await MidiOutPort.FromIdAsync(device);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }


            if (port != null)
            {
                try
                {
                    MidiControlChangeMessage msg = new MidiControlChangeMessage(1,(byte) ctrl, (byte)ccvalue);
                    port.SendMessage(msg);
                    var x = 22;
                }
                catch (Exception)
                {


                }
            }
        }

        DeviceInformation dev;
        private async void StartMidiStuff()
        {
            var selector = MidiInPort.GetDeviceSelector();

            var devices = await DeviceInformation.FindAllAsync(selector);

            if (devices != null && devices.Count > 0)
            {
                // MIDI devices returned
                foreach (var device in devices)
                {
                    dev = device;
                }
            }

        }




        private void MyPageVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            VM s = sender as VM;
            if (s != null)
            {
                SendNewValue(s.SliderValue);
            }
        }

        private async void fillCB()
        {
            var selector = MidiOutPort.GetDeviceSelector();

            var devices = await DeviceInformation.FindAllAsync(selector);

            if (devices != null && devices.Count > 0)
            {



                // MIDI devices returned
                foreach (var device in devices)
                {

                    cb.Items.Add(device.Id as string);
                }
            }
        }

        private async void SendNewValue(int s)
        {
            string device = cb.SelectedValue as string;
            //var device = devices[2];
            IMidiOutPort port = null;

            byte prog = (byte) s ;

            try
            {
                port = await MidiOutPort.FromIdAsync(device);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }


            if (port != null)
            {
                try
                {
                    MidiProgramChangeMessage msg = new MidiProgramChangeMessage(1, prog);
                    port.SendMessage(msg);
                    var x = 22;
                }
                catch (Exception)
                {


                }
            }






            //device.
        }


        private void ListButton(object sender, RoutedEventArgs e)
        {
            fillCB();
        }
    

private void Button_Click(object sender, RoutedEventArgs e)
{
    SendNewValue((int)MySly.Value );
}

        private void MySly_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SendNewValue((int)MySly.Value);
        }
    }
}

