using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierskiWPF
{
    class StatisticsItemsControlModel : ViewModelBase
    {
        private ObservableCollection<StatisticsItemsControlElement> _ItemsControllList = new ObservableCollection<StatisticsItemsControlElement>();
        public ObservableCollection<StatisticsItemsControlElement> ItemsControllList
        {
            get { return _ItemsControllList; }
            private set
            {
                if (_ItemsControllList == value)
                    return;

                _ItemsControllList = value;
                RaisePropertyChanged(nameof(ItemsControllList));
            }
        }
    }
}
