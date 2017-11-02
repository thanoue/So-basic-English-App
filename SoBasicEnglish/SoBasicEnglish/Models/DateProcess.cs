using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoBasicEnglish
{
    public class DateProcess : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private int _turnNumber = 0;
        private string _detailInfo = "";
        private string _levelDetail = "";

        public int TurnNumber
        {
            get => _turnNumber; set
            {
                _turnNumber = value;
                NotifyPropertyChanged("TurnNumber");
            }
        }

        public string DetailInfo
        {
            get => _detailInfo; set { _detailInfo = value; NotifyPropertyChanged("DetailInfo"); }
        }

        public string LevelDetail { get => _levelDetail; set { _levelDetail = value; NotifyPropertyChanged("LevelDetail"); } }
    }
}