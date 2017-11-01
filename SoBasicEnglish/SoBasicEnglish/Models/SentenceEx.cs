using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace SoBasicEnglish
{
    public class SentenceEx : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool choseA;
        public bool ChoseA
        {
            get { return choseA; }
            set
            {

                if (this.choseA != value)
                {
                    this.choseA = value;
                    this.NotifyPropertyChanged("ChoseA");
                }
            }
        }
        private bool choseB;
        public bool ChoseB
        {
            get { return choseB; }
            set
            {

                if (this.choseB != value)
                {
                    this.choseB = value;
                    this.NotifyPropertyChanged("ChoseB");
                }
            }
        }
        private bool choseC;
        public bool ChoseC
        {
            get { return choseC; }
            set
            {

                if (this.choseC != value)
                {
                    this.choseC = value;
                    this.NotifyPropertyChanged("ChoseC");
                }
            }
        }


        private bool choseD;
        public bool ChoseD
        {
            get { return choseD; }
            set
            {

                if (this.choseD != value)
                {
                    this.choseD = value;
                    this.NotifyPropertyChanged("ChoseD");
                }
            }
        }
        private string keyWord;
        public string KeyWord
        {
            get { return keyWord; }
            set
            {

                if (this.keyWord != value)
                {
                    this.keyWord = value;
                    this.NotifyPropertyChanged("KeyWord");
                }
            }
        }
        private string ansA;
        public string AnsA
        {
            get { return ansA; }
            set
            {

                if (this.ansA != value)
                {
                    this.ansA = value;
                    this.NotifyPropertyChanged("AnsA");
                }
            }
        }
        private string ansB;
        public string AnsB
        {
            get { return ansB; }
            set
            {

                if (this.ansB != value)
                {
                    this.ansB = value;
                    this.NotifyPropertyChanged("AnsB");
                }
            }
        }
        private string ansC;
        public string AnsC
        {
            get { return ansC; }
            set
            {

                if (this.ansC != value)
                {
                    this.ansC = value;
                    this.NotifyPropertyChanged("AnsC");
                }
            }
        }
        private string ansD;
        public string AnsD
        {
            get { return ansD; }
            set
            {

                if (this.ansD != value)
                {
                    this.ansD = value;
                    this.NotifyPropertyChanged("AnsD");
                }
            }
        }
        private int rightAns;
        public int RightAns
        {
            get { return rightAns; }
            set
            {

                if (this.rightAns != value)
                {
                    this.rightAns = value;
                    this.NotifyPropertyChanged("RightAns");
                }
            }
        }
        private SolidColorBrush bgA = new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush BgA
        {
            get { return bgA; }
            set { bgA = value;
                NotifyPropertyChanged("BgA");
                }
        }
        private SolidColorBrush bgB = new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush BgB
        {
            get { return bgB; }
            set
            {
                bgB = value;
                NotifyPropertyChanged("BgB");
            }
        }
        private SolidColorBrush bgC = new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush BgC
        {
            get { return bgC; }
            set
            {
                bgC = value;
                NotifyPropertyChanged("BgC");
            }
        }
        private SolidColorBrush bgD = new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush BgD
        {
            get { return bgD; }
            set
            {
                bgD = value;
                NotifyPropertyChanged("BgD");
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
