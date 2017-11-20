using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BusinessLogicFramework;
using System.Windows.Input;
using Prism.Commands;
using System.Threading;
using System.IO;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Drawing;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;
using System.IO.Packaging;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using Prism.Mvvm;
using System.Windows.Forms;

namespace SoBasicEnglish.ViewModels
{
    public class LessonQuestionViewModel:  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private dbLessonQuestion dbLessonQuestion; DispatcherTimer TimerToCloseNotify;
        public string ContentOfNotify { get => _contentOfNotify; set { _contentOfNotify = value; NotifyPropertyChanged("ContentOfNotify"); } }
        private string _contentOfNotify = "";
        public string ContentOfQuestion { get => _contentOfQuestion; set { _contentOfQuestion = value; NotifyPropertyChanged("ContentOfQuestion"); } }
        private string _contentOfQuestion = "";
        public bool IsOpenNotify { get => _isOpenNotify; set { _isOpenNotify = value; NotifyPropertyChanged("IsOpenNotify"); } }
        private bool _isOpenNotify = false;
        public bool IsOpenQuestion { get => _isOpenQuestion; set { _isOpenQuestion = value; NotifyPropertyChanged("IsOpenQuestion"); } }
        private bool _isOpenQuestion = false;
        private ObservableCollection<LessonQuestion> _lessonQuestionList = new ObservableCollection<LessonQuestion>();
        public ObservableCollection<LessonQuestion> LessonQuestionList { get => _lessonQuestionList; set { _lessonQuestionList = value; NotifyPropertyChanged("AnswerOfQuestion"); } }
        public ICommand Click_ShowLessonQuestionMenu { get; set; }
        public ICommand CloseLessonQuestion { get; set; }
        public ICommand Click_InsertQuestion { get; set; }

        public LessonQuestionViewModel()
        {
            dbLessonQuestion = new dbLessonQuestion(Model.serverName);   
            Click_ShowLessonQuestionMenu = new RelayCommand<object>((p)=>true,ShowLessonQuestionMenu);
            CloseLessonQuestion = new DelegateCommand(CLose);
            Click_InsertQuestion = new RelayCommand<object>((p)=>true, InsertQuestion);
            TimerToCloseNotify = new DispatcherTimer();TimerToCloseNotify.Interval = TimeSpan.FromMilliseconds(1400); TimerToCloseNotify.Tick += TimerToCloseNotify_Tick;
           
        }

        private bool chekContenOfQuestion(object obj)
        {
            if (ContentOfNotify != "")
                return true;
            return false;
        }

        private void TimerToCloseNotify_Tick(object sender, EventArgs e)
        {
            IsOpenNotify = false;
            TimerToCloseNotify.Stop();
        }
        
        private void InsertQuestion(object obj)
        {
            string er = "";
            if(dbLessonQuestion.InsertLessnQuestion(ref er, Model.userLoginName, Model.dateProcess, ContentOfQuestion, DateTime.Now))
            {
                ContentOfNotify = "Your question will be responed, soon.";
                IsOpenNotify = true;
                TimerToCloseNotify.Start();
            }
        }

        private void CLose()
        {
            IsOpenQuestion = false;            
        
        }

        private void ShowLessonQuestionMenu(object o)
        {
           
            IsOpenQuestion = true;
            if(LessonQuestionList.Count==0)
              GetLessonQuestionByDateProcess();
        }
        private void GetLessonQuestionByDateProcess()
        {
            var temp = dbLessonQuestion.GetLessonQuestionByDateProcess(Model.dateProcess);
            foreach(DataRow i in temp.Rows)
            {
                LessonQuestionList.Add(new LessonQuestion {UserAvt=(byte[])i["userAvatar"],UserName=i["userName"].ToString(),ContentOfQuestion=i["contentOfQuestion"].ToString(),
                    AnswerOfQuestion = i["contentOfAnswer"].ToString(),TimeOfAsk = (Convert.ToDateTime(i["timeOfAsk"])).ToString()
                });
            }
         
           
        }
       
      
    }
}
