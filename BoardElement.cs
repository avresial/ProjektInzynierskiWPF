using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace ProjektInzynierskiWPF
{
    class BoardElement : ViewModelBase
    {
        private Brush _Color;
        public Brush Color
        {
            get
            {
                return _Color;
            }

            set
            {
                _Color = value;
                RaisePropertyChanged(nameof(Color));
            }
        }
        private int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
        public BoardElement(Brush color, int value) {
            Value = value;
            Color = color;
        }


    }
}
