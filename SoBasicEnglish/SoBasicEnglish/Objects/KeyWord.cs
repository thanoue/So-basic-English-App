using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoBasicEnglish
{
    public class KeyWord : INotifyPropertyChanged
    {

        private int index;
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
        private string word;
        public string Word
        {
            get { return word; }
            set
            {

                if (this.word != value)
                {
                    this.word = value;
                    this.NotifyPropertyChanged("Word");

                }
            }
        }
        private string howToReadWord;
        public string HowToReadWord
        {
            get { return howToReadWord; }
            set
            {

                if (this.howToReadWord != value)
                {
                    this.howToReadWord = value;
                    this.NotifyPropertyChanged("HowToReadWord");
                }
            }
        }
        private string vieWord;
        public string VieWord
        {
            get { return vieWord; }
            set
            {

                if (this.vieWord != value)
                {
                    this.vieWord = value;
                    this.NotifyPropertyChanged("VieWord");
                }
            }
        }
        private string typeOfWord;
        public string TypeOfWord
        {
            get { return typeOfWord; }
            set
            {

                if (this.typeOfWord != value)
                {
                    this.typeOfWord = value;
                    this.NotifyPropertyChanged("TypeOfWord");
                }
            }
        }
        private string exSentence;
        public string ExSentence
        {
            get { return exSentence; }
            set
            {

                if (this.exSentence != value)
                {
                    this.exSentence = value;
                    this.NotifyPropertyChanged("ExSentence");
                }
            }
        }
        private byte[] pictureOfWord;
        public byte[] PictureOfWord
        {
            get { return pictureOfWord; }
            set
            {

                if (this.pictureOfWord != value)
                {
                    this.pictureOfWord = value;
                    this.NotifyPropertyChanged("PictureOfWord");
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
