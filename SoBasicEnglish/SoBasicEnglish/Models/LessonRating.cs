using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SoBasicEnglish
{
    public class LessonRating : INotifyPropertyChanged
    {
        private bool check1 = false;
        public bool Check1
        {
            get { return check1; }
            set
            {
                check1 = value;
                NotifyPropertyChanged("Check1");
            }
        }
        private bool check2 = false;
        public bool Check2
        {
            get { return check2; }
            set
            {
                check2 = value;
                NotifyPropertyChanged("Check2");
            }
        }
        private bool check3 = false;
        public bool Check3
        {
            get { return check3; }
            set
            {
                check3 = value;
                NotifyPropertyChanged("Check3");
            }
        }
        private bool check4 = false;
        public bool Check4
        {
            get { return check4; }
            set
            {
                check4 = value;
                NotifyPropertyChanged("Check4");
            }
        }
        private bool check5 = false;
        public bool Check5
        {
            get { return check5; }
            set
            {
                check5 = value;
                NotifyPropertyChanged("Check5");
            }
        }
        private string userName = "";
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                NotifyPropertyChanged("UserName");
            }
        }
        private byte[] userAvt = Model.eight;
        public byte[] UserAvt
        {
            get { return userAvt; }
            set
            {
                userAvt = value;
                NotifyPropertyChanged("UserAvt");
            }
        }
        private int rating = 0;
        public int Rating
        {
            get { return rating; }
            set
            {
                rating = value;
                NotifyPropertyChanged("Rating");
            }
        }
        private string feedBack = "";
        public string FeedBack
        {
            get { return feedBack; }
            set { feedBack = value;
                NotifyPropertyChanged("FeedBack");
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
