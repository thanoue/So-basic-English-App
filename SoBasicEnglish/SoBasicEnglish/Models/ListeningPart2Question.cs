using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace SoBasicEnglish
{
    public class ListeningPart2Question : INotifyPropertyChanged
    {
        private string before;
        private SolidColorBrush valueBrush = new SolidColorBrush(Colors.Black);
        public SolidColorBrush ValueBrush
        {
            get { return valueBrush; }
            set
            {
                valueBrush = value;
                NotifyPropertyChanged("ValueBrush");
            }
        }
        public string Before
        {
            get { return before; }
            set
            {

                if (this.before != value)
                {
                    this.before = value;
                    this.NotifyPropertyChanged("Before");
                }
            }
        }
        private string value;
        public string Value
        {
            get { return value; }
            set
            {

                if (this.value != value)
                {
                    this.value = value;
                    this.NotifyPropertyChanged("Value");
                }
            }
        }
        private string after;
        public string After
        {
            get { return after; }
            set
            {

                if (this.after != value)
                {
                    this.after = value;
                    this.NotifyPropertyChanged("After");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
    }
