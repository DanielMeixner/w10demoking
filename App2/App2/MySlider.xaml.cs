using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App2
{
    public class MySliderEventArgs: EventArgs
    {
        MySlider slider;
        string sliderName;
        int newValue;
        int oldValue;

        public int NewValue
        {
            get
            {
                return newValue;
            }

            set
            {
                newValue = value;
            }
        }

        public int OldValue
        {
            get
            {
                return oldValue;
            }

            set
            {
                oldValue = value;
            }
        }

        public MySlider Slider
        {
            get
            {
                return slider;
            }

            set
            {
                slider = value;
            }
        }

        public string SliderName
        {
            get
            {
                return sliderName;
            }

            set
            {
                sliderName = value;
            }
        }
    }

    public sealed partial class MySlider : UserControl, INotifyPropertyChanged
    {


        
        public MySlider()
        {
            this.InitializeComponent();
        }

        private int myValue;

        public int MyValueProp 
        {
            get
            {
                return myValue;
            }

            set
            {
                myValue = value;
                NotifyPropertyChanged();
            }
        }

        

        private void NotifyPropertyChanged([CallerMemberName ] string caller = null)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        public event EventHandler ControlValueChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private void MySly_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            MyValueProp = (int) e.NewValue;

            MySliderEventArgs ea = new MySliderEventArgs();
            ea.SliderName = "empty";
            ea.NewValue = (int) e.NewValue;
            ea.OldValue = (int) e.OldValue;
            ea.Slider = this;


            if (ControlValueChanged != null)
            {
                ControlValueChanged(sender,  ea);
            }
        }
    }
}
