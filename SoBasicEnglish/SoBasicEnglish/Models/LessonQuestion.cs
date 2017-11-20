using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SoBasicEnglish
{
    public class LessonQuestion : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private byte[] _userAvt = Model.five;
        public byte[] UserAvt { get => _userAvt; set { _userAvt = value; NotifyPropertyChanged("UserAvt"); } }

        public string UserName { get => _userName; set { _userName = value;NotifyPropertyChanged("UserName"); } }
        private string _userName = "";
        public string TimeOfAsk { get => _timeOfAsk; set { _timeOfAsk = value; NotifyPropertyChanged("TimeOfAsk"); } }
        private string _timeOfAsk = "";
        private string _contentOfQuestion = "";
        public string ContentOfQuestion { get => _contentOfQuestion; set { _contentOfQuestion = value; NotifyPropertyChanged("ContentOfQuestion"); } }
        public string AnswerOfQuestion { get => _answerOfQuestion; set { _answerOfQuestion = value;NotifyPropertyChanged("AnswerOfQuestion"); } }

        public int Id { get => _id; set { _id = value; NotifyPropertyChanged("Id"); } }

        private string _answerOfQuestion = "";
        private int _id = 0;

    }
}
