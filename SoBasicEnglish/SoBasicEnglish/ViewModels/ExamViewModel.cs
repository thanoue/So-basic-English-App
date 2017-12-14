using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
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
using System.Windows.Forms;

namespace SoBasicEnglish.ViewModels
{
   public  class ExamViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #region command objects
        public ICommand  Click_Start { get; set; }
        #endregion
        #region objects
        dbExam dbExam;
        public double ProgressBarValue { get => _progressBarValue; set { _progressBarValue = value; NotifyPropertyChanged("ProgressBarValue"); } }
        private double _progressBarValue = 170;
        private double MaxValueOfProgress =0;
        private int CurentNumber = 0;
        public byte[] BigNumImage { get => _bigNumImage; set { _bigNumImage = value; NotifyPropertyChanged("BigNumImage"); } }
        private byte[] _bigNumImage = Model.zero;
        public byte[] SmallNumImage { get => _smallNumImage; set { _smallNumImage = value; NotifyPropertyChanged("SmallNumImage"); } }
        private byte[] _smallNumImage = Model.zero;
        DispatcherTimer DoingEXClock;
        public bool IsDoingEX { get => _isDoingEX; set { _isDoingEX = value; NotifyPropertyChanged("IsDoingEX"); } }
        private bool _isDoingEX = false;
        #endregion
        #region constructor
        public ExamViewModel()
        {
            dbExam = new dbExam(Model.serverName);
            Click_Start = new RelayCommand<object>((p) => !IsDoingEX, StartDoingEX);
            DoingEXClock = new DispatcherTimer();
            DoingEXClock.Interval = TimeSpan.FromSeconds(0.1);
            DoingEXClock.Tick += DoingEXClock_Tick;
            MaxValueOfProgress = ProgressBarValue;
            GetTest();

        }

        private void DoingEXClock_Tick(object sender, EventArgs e)
        {
            ProgressBarValue -= (double)(MaxValueOfProgress / 99.0);
            CurentNumber += 1;
            int firstnum = 0;int secondnum = 0;
            firstnum = CurentNumber / 10;
            secondnum = CurentNumber % 10;
            switch (firstnum)
            {
                case 9:
                    BigNumImage = Model.nine;
                    break;
                case 8:
                    BigNumImage = Model.eight;
                    break;
                case 7:
                    BigNumImage = Model.seven;
                    break;
                case 6:
                    BigNumImage = Model.six;
                    break;
                case 5:
                    BigNumImage = Model.five;
                    break;
                case 4:
                    BigNumImage = Model.four;
                    break;
                case 3:
                    BigNumImage = Model.three;
                    break;
                case 2:
                    BigNumImage = Model.two;
                    break;
                case 1:
                    BigNumImage = Model.one;
                    break;
                case 0:
                    BigNumImage = Model.zero;
                    break;
                default:
                    break;
            }
            switch (secondnum)
            {
                case 9:
                    SmallNumImage = Model.nine;
                    break;
                case 8:
                    SmallNumImage = Model.eight;
                    break;
                case 7:
                    SmallNumImage = Model.seven;
                    break;
                case 6:
                    SmallNumImage = Model.six;
                    break;
                case 5:
                    SmallNumImage = Model.five;
                    break;
                case 4:
                    SmallNumImage = Model.four;
                    break;
                case 3:
                    SmallNumImage = Model.three;
                    break;
                case 2:
                    SmallNumImage = Model.two;
                    break;
                case 1:
                    SmallNumImage = Model.one;
                    break;
                case 0:
                    SmallNumImage = Model.zero;
                    break;
                default:
                    break;
            }
            if (CurentNumber == 99)
            {
                DoingEXClock.Stop();
                CurentNumber = 0;
                ProgressBarValue = MaxValueOfProgress;
                IsDoingEX = false;
                SmallNumImage = Model.zero;BigNumImage = Model.zero;
            }

        }
        #endregion
        #region command functions
        private void StartDoingEX(object obj)
        {
            DoingEXClock.Start();
            IsDoingEX = true;
        }
        #endregion
        #region functions
        private void GetTest()
        {
            int turn = dbExam.GetProcessLevel(Model.userDateProcess);

        }
        #endregion

    }
}
