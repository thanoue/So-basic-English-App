using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SoBasicEnglish
{
    public class Sentence : INotifyPropertyChanged
    {
        private int index=0;
        public int Index
        {
            get { return index; }
            set
            {

                if (this.index != value)
                {
                    this.index = value;
                    this.NotifyPropertyChanged("Index");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private string keySentence;
        private string howToRead;
        private string vieMeanOfSentence;
        public string VieMeanOfSentence
        {
            get { return vieMeanOfSentence; }
            set
            {

                if (this.vieMeanOfSentence != value)
                {
                    this.vieMeanOfSentence = value;
                    this.NotifyPropertyChanged("VieMeanOfSentence");
                }
            }
        }
        public string KeySentence
        {
            get { return keySentence; }
            set
            {

                if (this.keySentence != value)
                {
                    this.keySentence = value;
                    this.NotifyPropertyChanged("KeySentence");
                }
            }
        }
        public string HowToRead
        {
            get { return howToRead; }
            set
            {

                if (this.howToRead != value)
                {
                    this.howToRead = value;
                    this.NotifyPropertyChanged("HowToRead");
                }
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
