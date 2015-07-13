using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class VM : INotifyPropertyChanged
    {
        private int sliderValue = 0;

        public int SliderValue

        {
            get
            {
                return sliderValue;
            }

            set
            {
                this.sliderValue = value;
                OnNotifyChanged();
            }
        }

        private void OnNotifyChanged([CallerMemberName] string memName = "") 
        {
            

            var keeper = PropertyChanged;
            if (keeper != null)
            {                
                keeper(this, new PropertyChangedEventArgs(memName));
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
