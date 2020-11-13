using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierskiWPF
{
    public class StatisticsItemsControlElement : ViewModelBase
    {
        
        private int _DeserterValue ;
        public int DeserterValue
        {
            get { return _DeserterValue; }
            set 
            {
                _DeserterValue = value;
                RaisePropertyChanged(nameof(DeserterValue));
            }
        }
        private int _DeserterPathCount;
        public int DeserterPathCount
        {
            get { return _DeserterPathCount; }
            set 
            {
                _DeserterPathCount = value;
                RaisePropertyChanged(nameof(DeserterPathCount));
            }
        }
        private bool _IsFinished;
        public bool IsFinished
        {
            get { return _IsFinished; }
            set 
            {
                _IsFinished = value;
                RaisePropertyChanged(nameof(IsFinished));
            }
        }
        public StatisticsItemsControlElement(int deserterValue, int deserterPathCount, bool deserterReachedFinishLine)
        {
            DeserterValue = deserterValue;
            DeserterPathCount = deserterPathCount;
            IsFinished = deserterReachedFinishLine;
        }
    }
}
