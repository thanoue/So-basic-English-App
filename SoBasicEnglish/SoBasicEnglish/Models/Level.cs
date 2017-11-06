using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoBasicEnglish
{
   public  class Level : INotifyPropertyChanged
    
    {
        private string _levelValue = "";

        public string LevelValue { get => _levelValue; set { _levelValue = value; NotifyPropertyChanged("LevelValue"); } }
        public int LevelIndex { get => _levelIndex; set { _levelIndex = value; NotifyPropertyChanged("LevelIndex"); } }

        private int _levelIndex = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
