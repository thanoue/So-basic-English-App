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
   public class RatingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #region command objects
        public ICommand Click_ViewLessonRating { get; set; }
        public ICommand Click_Rating { get; set; }
        #endregion
        #region objects
        dbLessonRating dbLessonRating;
        public ObservableCollection<LessonRating> LessonratingList { get => _lessonratingList; set { _lessonratingList = value; NotifyPropertyChanged("LessonratingList"); } }
        private ObservableCollection<LessonRating> _lessonratingList = new ObservableCollection<LessonRating>();
        public bool IsOpenLessonRating { get => _isOpenLessonRating; set { _isOpenLessonRating = value; NotifyPropertyChanged("IsOpenLessonRating"); } }
        private bool _isOpenLessonRating = false;
        #endregion
        #region constructor
        public RatingViewModel()
        {
            dbLessonRating = new dbLessonRating(Model.serverName);
            Click_ViewLessonRating = new DelegateCommand(ViewLessonRating);
            Click_Rating = new RelayCommand<UIElementCollection>((p)=>true,RateForLesson);
            GetLessonRating(Model.dateProcess);
        }
        #endregion
        #region command functions
        private void RateForLesson(UIElementCollection obj)
        {
            int value = 0;string content = "";
            string er = "";
            foreach(var i in obj)
            {
                Rating temp = i as Rating;
                if (temp != null)
                {
                    value = temp.Value;
                }
                else
                {
                    System.Windows.Controls.TextBox temp2 = i as System.Windows.Controls.TextBox;
                    if (temp2 != null)
                        content = temp2.Text;
                }
            }
            if (dbLessonRating.InsertLessonRating(ref er, Model.dateProcess, Model.userLoginName, value, content))
            {
               
                GetLessonRating(Model.dateProcess);
            }
            else
            {
                if (dbLessonRating.UpdateLessonRating(ref er, Model.dateProcess, Model.userLoginName, value, content))
                {

                    GetLessonRating(Model.dateProcess);
                }
            }
        }
        private void ViewLessonRating()
        {
            if (IsOpenLessonRating)
                IsOpenLessonRating = false;
            else
                IsOpenLessonRating = true;
        }
        #endregion
        #region functions
        private void GetLessonRating(int dateProcess)/////////////////////////////////////////////////////////
        {
            LessonratingList.Clear();
            System.Data.DataTable temp = dbLessonRating.GeLessonRatingById(dateProcess);
            foreach (DataRow i in temp.Rows)
            {
                switch (Int32.Parse(i["Rating"].ToString()))
                {
                    case 1:
                        LessonratingList.Add(new LessonRating { Check1 = true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                    case 2:
                        LessonratingList.Add(new LessonRating { Check1 = true, Check2 = true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                    case 3:
                        LessonratingList.Add(new LessonRating { Check1 = true, Check2 = true, Check3 = true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                    case 4:
                        LessonratingList.Add(new LessonRating { Check1 = true, Check2 = true, Check3 = true, Check4 = true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                    case 5:
                        LessonratingList.Add(new LessonRating { Check1 = true, Check2 = true, Check3 = true, Check4 = true, Check5 = true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                }

            }           
        }
        #endregion
    }
}
