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

namespace SoBasicEnglish.ViewModels
{
    class StudyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #region Command objects
        public ICommand SelectionChanged_lbMenuLesson { get; set; }
        public ICommand Click_GoToTheMenuLb { get; set; }
        public ICommand Click_GoToTheLessonMenu { get; set; }
        public ICommand Click_GoToStudyTabItem { get; set; }
        public ICommand Click_GoToPracticeTabItem { get; set; }
        public ICommand Click_GoToOralTestTabItem { get; set; }
        public ICommand Click_GoToListenPart1EX { get; set; }
        public ICommand Click_GoToListenPart2EX { get; set; }
        public ICommand Click_GotoNextGetReadyQuestion { get; set; }
        public ICommand Check_ToggleBtnShowQuestion { get; set; }
        public ICommand lbGetReadyQuesListSelectionChanged { get; set; }
        public ICommand Check_ToggleBtnAutoNextQuestion { get; set; }
        public ICommand Click_ListenKeyWord { get; set; }
        public ICommand Click_ListenEXSentenceOfKeyWord { get; set; }
        public ICommand Click_StartDoingEX { get; set; }
        public ICommand Click_RestartDoingEX { get; set; }
        public ICommand Click_SubmitEX { get; set; }
        public ICommand Click_CloseArlet { get; set; }
        #endregion
        #region objects
        DispatcherTimer TimeToNextQuestion;private bool CheckAutoNext = false; private int KeyWordExGainedScore = 0; private int GetReadyGainedScore = 0;
        #region colors
        private SolidColorBrush _aBr = new SolidColorBrush(Colors.Transparent), _bBr = new SolidColorBrush(Colors.Transparent), _cBr = new SolidColorBrush(Colors.Transparent), _dBr = new SolidColorBrush(Colors.Transparent), _quesContenBrush =new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush QuesContenBrush { get => _quesContenBrush; set { _quesContenBrush = value; NotifyPropertyChanged("QuesContenBrush"); } }
        public SolidColorBrush ABr { get => _aBr; set { _aBr = value; NotifyPropertyChanged("ABr"); } }
        public SolidColorBrush BBr { get => _bBr; set { _bBr = value; NotifyPropertyChanged("BBr"); } }
        public SolidColorBrush CBr { get => _cBr; set { _cBr = value; NotifyPropertyChanged("CBr"); } }
        public SolidColorBrush DBr { get => _dBr; set { _dBr = value; NotifyPropertyChanged("DBr"); } }
        #endregion
        public int GainedScore { get => _gainedScore; set { _gainedScore = value; NotifyPropertyChanged("GainedScore"); } }
        private int _gainedScore = 0;
        public bool IsOpenFailMenu { get => _isOpenFailMenu; set { _isOpenFailMenu = value; NotifyPropertyChanged("IsOpenFailMenu"); } }
        private bool _isOpenFailMenu = false;
        public bool IsOpenSucessMenu { get => _isOpenSucessMenu; set { _isOpenSucessMenu = value; NotifyPropertyChanged("IsOpenSucessMenu"); } }
        private bool _isOpenSucessMenu = false;
        public int TcLessonSelectedIndex { get => _tcLessonSelectedIndex; set { _tcLessonSelectedIndex = value; NotifyPropertyChanged("TcLessonSelectedIndex"); } }
        private int _tcLessonSelectedIndex = 0;
        public int Turn { get => _turn; set { _turn = value; NotifyPropertyChanged("Turn"); } }
        private int _turn = 0;
        public int LbLessonMenuSelectedIndex { get => _lbLessonMenuSelectedIndex; set { _lbLessonMenuSelectedIndex = value; NotifyPropertyChanged("LbLessonMenuSelectedIndex"); } }
        private int _lbLessonMenuSelectedIndex = -1;
        public int TcKeyWordSelectedIndex { get => _tcKeyWordSelectedIndex; set { _tcKeyWordSelectedIndex = value; NotifyPropertyChanged("TcKeyWordSelectedIndex"); } }
        private int _tcKeyWordSelectedIndex = 0;
        public bool DoingEx { get => _doingEx; set { _doingEx = value;NotifyPropertyChanged("DoingEx"); } }
        private bool _doingEx = false;
        public int TcSentenceSelectedIndex { get => _tcSentenceSelectedIndex; set { _tcSentenceSelectedIndex = value; NotifyPropertyChanged("TcSentenceSelectedIndex"); } }
        private int _tcSentenceSelectedIndex = 0;
        public int TcListenSelectedIndex { get => _tcListenSelectedIndex; set { _tcListenSelectedIndex = value; NotifyPropertyChanged("TcListenSelectedIndex"); } }
        private int _tcListenSelectedIndex = 0;
        public int TcGrammarSelectIndex { get => _tcGrammarSelectIndex; set { _tcGrammarSelectIndex = value; NotifyPropertyChanged("TcGrammarSelectIndex"); } }
        private int _tcGrammarSelectIndex = 0;
        public SolidColorBrush StudyButtonColor { get => _studyButtonColor; set { _studyButtonColor = value; NotifyPropertyChanged("StudyButtonColor"); } }
        private SolidColorBrush _studyButtonColor = new SolidColorBrush( Colors.Brown);
        public SolidColorBrush PracticeButtonColor { get => _practiceButtonColor; set { _practiceButtonColor = value; NotifyPropertyChanged("PracticeButtonColor"); } }
        private SolidColorBrush _practiceButtonColor = new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush OralTestButtonColor { get => _oralTestButtonColor; set { _oralTestButtonColor = value; NotifyPropertyChanged("OralTestButtonColor"); } }
        private SolidColorBrush _oralTestButtonColor = new SolidColorBrush(Colors.Transparent);
        public GettingReadyQuestion CurentGetReadyQuestion { get => _curentGetReadyQuestion; set { _curentGetReadyQuestion = value; NotifyPropertyChanged("CurentGetReadyQuestion"); } }
        private GettingReadyQuestion _curentGetReadyQuestion = new GettingReadyQuestion();
        #region List objects       
        public ObservableCollection<KeyWord> KeyWordsList { get => _keyWordsList; set { _keyWordsList = value; NotifyPropertyChanged("KeyWordsList"); } }
        private ObservableCollection<KeyWord> _keyWordsList = new ObservableCollection<KeyWord>();
        public ObservableCollection<Sentence> SentenceList { get => _sentenceList; set { _sentenceList = value; NotifyPropertyChanged("SentenceList"); } }
        private ObservableCollection<Sentence> _sentenceList = new ObservableCollection<Sentence>();
        public ObservableCollection<ListeningQuestion> ListeningQuestionList { get => _listeningQuestionList; set { _listeningQuestionList = value; NotifyPropertyChanged("ListeningQuestionList"); } }
        private ObservableCollection<ListeningQuestion> _listeningQuestionList = new ObservableCollection<ListeningQuestion>();
        public ObservableCollection<SentenceEx> SentenceExList { get => _sentenceExList; set { _sentenceExList = value; NotifyPropertyChanged("SentenceExList"); } }
        private ObservableCollection<SentenceEx> _sentenceExList = new ObservableCollection<SentenceEx>();
        public ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList { get => _listeningPart2QuestionList; set { _listeningPart2QuestionList = value; NotifyPropertyChanged("ListeningPart2QuestionList"); } }
        private ObservableCollection<ListeningPart2Question> _listeningPart2QuestionList = new ObservableCollection<ListeningPart2Question>();
        public ObservableCollection<GettingReadyQuestion> GettingReadyQuestionList { get => _gettingReadyQuestionList; set { _gettingReadyQuestionList = value; NotifyPropertyChanged("GettingReadyQuestionList"); } }
        private ObservableCollection<GettingReadyQuestion> _gettingReadyQuestionList = new ObservableCollection<GettingReadyQuestion>();
        public ObservableCollection<KeyWordEx> KeyWordExList { get => _keyWordExList; set { _keyWordExList = value; NotifyPropertyChanged("KeyWordExList"); } }

        private ObservableCollection<KeyWordEx> _keyWordExList = new ObservableCollection<KeyWordEx>();
        #endregion
        #endregion
        #region constructor
        public StudyViewModel()
        {
            SelectionChanged_lbMenuLesson = new DelegateCommand(ChoosePartOfLesson);
            Click_GoToTheMenuLb = new RelayCommand<object>((p)=>!DoingEx,GoToTheLessonMenu);
            Click_GoToStudyTabItem = new RelayCommand<object>((p) => !DoingEx, GoToStudyTabItem);
            Click_GoToPracticeTabItem = new RelayCommand<object>((p)=>!DoingEx,GoToPracticeTabItem);
            Click_GoToOralTestTabItem = new RelayCommand<object>((p) => !DoingEx, GoToOralTestTabItem);
            Click_GoToListenPart1EX = new RelayCommand<object>((p) => !DoingEx, GoToListenPart1EX);
            Click_GoToListenPart2EX = new RelayCommand<object>((p) => !DoingEx, GoToListenPart2EX);
            Click_GotoNextGetReadyQuestion = new RelayCommand<object>(CheckIfCanNextQuestion,GotoNextGetReadyQuestion);
            Check_ToggleBtnShowQuestion = new DelegateCommand(CheckBtnShowQuestion);
            lbGetReadyQuesListSelectionChanged = new DelegateCommand(Change);
            Click_ListenKeyWord = new RelayCommand<object>((p) => true, ListenKeyWord);
            Check_ToggleBtnAutoNextQuestion = new DelegateCommand(CheckBtnAutoNextQuestion);
            Click_ListenEXSentenceOfKeyWord = new RelayCommand<object>((p) => true, ListenEXSentenceOfKeyWord);
            Click_StartDoingEX = new RelayCommand<object>((p) => !DoingEx, StartDoingEX);
            Click_RestartDoingEX = new RelayCommand<object>((p) => DoingEx, RestartDoingEX);
            Click_SubmitEX = new RelayCommand<object>((p) => DoingEx, SubmitEX); Click_CloseArlet = new DelegateCommand(CLoseArlet);
             TimeToNextQuestion = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1.5)               
            }; TimeToNextQuestion.Tick += TimeToNextQuestion_Tick;

        }
        private void CLoseArlet()
        {
            if (TcLessonSelectedIndex == 1)
            {
                IsOpenFailMenu = false; IsOpenSucessMenu = false;
                TcLessonSelectedIndex = 0; LbLessonMenuSelectedIndex = -1;
            }
           
        }
        #endregion
        #region commad functions
        private void StartDoingEX(object obj)
        {
            if (TcLessonSelectedIndex == 2)
            {
                DoingEx = true;
                KeyWordExList.Clear();
                Model.GetKeyWordExListByLessonId(KeyWordExList, Model.dateProcess, Model.serverName);
            }
        }
        private void SubmitEX(object obj)
        {
            if (TcLessonSelectedIndex == 2)
            {
                KeyWordExGainedScore = 0;
                for (int i = 0; i < KeyWordExList.Count; i++)
                {
                    if ((KeyWordExList[i].ChoseA && KeyWordExList[i].RightAns == 1) || (KeyWordExList[i].ChoseB && KeyWordExList[i].RightAns == 2) || (KeyWordExList[i].ChoseC && KeyWordExList[i].RightAns == 3) || (KeyWordExList[i].ChoseD && KeyWordExList[i].RightAns == 4))
                    {
                        KeyWordExGainedScore += 10;
                    }

                    switch (KeyWordExList[i].RightAns)
                    {
                        case 1:
                            if (KeyWordExList[i].ChoseA)
                                KeyWordExList[i].BgA = new SolidColorBrush(Colors.PaleGreen);
                            else
                                KeyWordExList[i].BgA = new SolidColorBrush(Colors.Orchid);
                            break;
                        case 2:
                            if (KeyWordExList[i].ChoseB)
                                KeyWordExList[i].BgB = new SolidColorBrush(Colors.PaleGreen);
                            else
                                KeyWordExList[i].BgB = new SolidColorBrush(Colors.Orchid);
                            break;
                        case 3:
                            if (KeyWordExList[i].ChoseC)
                                KeyWordExList[i].BgC = new SolidColorBrush(Colors.PaleGreen);
                            else
                                KeyWordExList[i].BgC = new SolidColorBrush(Colors.Orchid);
                            break;
                        case 4:
                            if (KeyWordExList[i].ChoseD)
                                KeyWordExList[i].BgD = new SolidColorBrush(Colors.PaleGreen);
                            else
                                KeyWordExList[i].BgD = new SolidColorBrush(Colors.Orchid);
                            break;
                        default:
                            break;
                    }
                }
                GainedScore = KeyWordExGainedScore;
                if (GainedScore >= (KeyWordExList.Count * 10) / 2)
                {
                    IsOpenSucessMenu = true;
                }
                else
                    IsOpenFailMenu = true;
                DoingEx = false;
            }

        }
        private void RestartDoingEX(object obj)
        {
            if (TcLessonSelectedIndex == 2)
            {
                for(int i = 0; i < KeyWordExList.Count; i++)
                {
                    KeyWordExList[i].ChoseA = false; KeyWordExList[i].ChoseB = false; KeyWordExList[i].ChoseC = false; KeyWordExList[i].ChoseD = false;
                }
            }

        }
        private void ListenEXSentenceOfKeyWord(object obj)
        {
            var temp = obj as KeyWord;
            SpeechFromText(temp.ExSentence);
        }
        private void ListenKeyWord(object obj)
        {
            var temp = obj as KeyWord;
            SpeechFromText(temp.Word);
        }
        private void CheckBtnAutoNextQuestion()
        {
            if (CheckAutoNext)
                CheckAutoNext = false;
            else
                CheckAutoNext = true;
            if (!DoingEx)
            {
                DoingEx = true;
                //add first data question
                // GetGetReadyQuestionList();

                SpeechFromText(CurentGetReadyQuestion.KeyWord);
            }
        }
        private void Change()
        {
           
            if (CheckAutoNext)
            {
                switch (CurentGetReadyQuestion.RightAns)
                {
                    case 1:
                        ABr = new SolidColorBrush(Colors.Tomato);
                        break;
                    case 2:
                        BBr = new SolidColorBrush(Colors.Tomato);
                        break;
                    case 3:
                        CBr = new SolidColorBrush(Colors.Tomato);
                        break;
                    case 4:
                        DBr = new SolidColorBrush(Colors.Tomato);
                        break;
                    default:
                        break;
                }
                if (Turn < GettingReadyQuestionList.Count)
                    TimeToNextQuestion.Start();
                else
                {
                    DoingEx = false;
                    //ABr = new SolidColorBrush(Colors.Transparent);
                    //BBr = new SolidColorBrush(Colors.Transparent);
                    //CBr = new SolidColorBrush(Colors.Transparent);
                    //DBr = new SolidColorBrush(Colors.Transparent);
                    TimeToNextQuestion.Start();                    
                }
            }
        }
        private void TimeToNextQuestion_Tick(object sender, EventArgs e)
        {

            if (Turn < GettingReadyQuestionList.Count)
            {
                Turn += 1;
                CurentGetReadyQuestion = GettingReadyQuestionList[Turn - 1];
                ABr = new SolidColorBrush(Colors.Transparent);
                BBr = new SolidColorBrush(Colors.Transparent);
                CBr = new SolidColorBrush(Colors.Transparent);
                DBr = new SolidColorBrush(Colors.Transparent);
                SpeechFromText(CurentGetReadyQuestion.KeyWord);
                TimeToNextQuestion.Stop();
            }
            else
            {
                
                ABr = new SolidColorBrush(Colors.Transparent);
                BBr = new SolidColorBrush(Colors.Transparent);
                CBr = new SolidColorBrush(Colors.Transparent);
                DBr = new SolidColorBrush(Colors.Transparent);                
                CurentGetReadyQuestion.ChoseA = false; CurentGetReadyQuestion.ChoseB = false; CurentGetReadyQuestion.ChoseC = false; CurentGetReadyQuestion.ChoseD = false;
                GetTheGetReadyGainedScore(); TimeToNextQuestion.Stop();
            }           
        }
        public bool CheckIfCanNextQuestion(object p) {

            return (CurentGetReadyQuestion.ChoseA || CurentGetReadyQuestion.ChoseB || CurentGetReadyQuestion.ChoseC || CurentGetReadyQuestion.ChoseD || !DoingEx ) && !CheckAutoNext;
        }
        private void CheckBtnShowQuestion()
        {
            if(QuesContenBrush.Color == Colors.Transparent)
            {
                QuesContenBrush = new SolidColorBrush(Colors.Orchid);
            }
            else
            {
                QuesContenBrush = new SolidColorBrush(Colors.Transparent);
            }
        }
        private void GotoNextGetReadyQuestion(object a)
        {
            if (!DoingEx)
            {
                DoingEx = true;
                SpeechFromText(CurentGetReadyQuestion.KeyWord);
            }
            else
            {
                switch (CurentGetReadyQuestion.RightAns)
                {
                    case 1:
                        ABr = new SolidColorBrush(Colors.Tomato);
                        break;
                    case 2:
                        BBr = new SolidColorBrush(Colors.Tomato);
                        break;
                    case 3:
                        CBr = new SolidColorBrush(Colors.Tomato);
                        break;
                    case 4:
                        DBr = new SolidColorBrush(Colors.Tomato);
                        break;
                    default:
                        break;
                }
                if (Turn < GettingReadyQuestionList.Count)
                     TimeToNextQuestion.Start();
                else
                {
                    DoingEx = false;
                    //ABr = new SolidColorBrush(Colors.Transparent);
                    //BBr = new SolidColorBrush(Colors.Transparent);
                    //CBr = new SolidColorBrush(Colors.Transparent);
                    //DBr = new SolidColorBrush(Colors.Transparent);
                    TimeToNextQuestion.Start();
                    
                }
            }
        }
        private void GetTheGetReadyGainedScore()
        {
            GetReadyGainedScore = 0;
            foreach (GettingReadyQuestion i in GettingReadyQuestionList)
            {
                if ((i.ChoseA && i.RightAns == 1) || (i.ChoseB && i.RightAns == 2) || (i.ChoseC && i.RightAns == 3) || (i.ChoseD && i.RightAns == 4))
                {
                    GetReadyGainedScore += 10;
                }
            }
            GainedScore = GetReadyGainedScore;
            if (GainedScore >= (GettingReadyQuestionList.Count * 10) / 2)
            {
                IsOpenSucessMenu = true;
                SpeechFromText("SuccessFull");
            }
            else
            {
                IsOpenFailMenu = true;
                SpeechFromText("You are fail!!");
               
            }
               
        }
        private void GoToListenPart2EX(object obj)
        {
            TcListenSelectedIndex = 1;
            PracticeButtonColor = new SolidColorBrush(Colors.Brown);
            StudyButtonColor = new SolidColorBrush(Colors.Transparent);
        }
        private void GoToListenPart1EX(object obj)
        {
            TcListenSelectedIndex = 0;
            PracticeButtonColor = new SolidColorBrush(Colors.Transparent);
            StudyButtonColor = new SolidColorBrush(Colors.Brown);
        }
        private void GoToOralTestTabItem(object obj)
        {
            PracticeButtonColor = new SolidColorBrush(Colors.Transparent);
            StudyButtonColor = new SolidColorBrush(Colors.Transparent);
            OralTestButtonColor = new SolidColorBrush(Colors.Brown);
            TcSentenceSelectedIndex = 2;
        }
        private void GoToPracticeTabItem(object obj)
        {
            if (TcLessonSelectedIndex == 2)
            {
                TcKeyWordSelectedIndex = 1;
                PracticeButtonColor =new SolidColorBrush( Colors.Brown);
                StudyButtonColor = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                if (TcLessonSelectedIndex == 3)
                {
                    TcSentenceSelectedIndex = 1;
                    PracticeButtonColor = new SolidColorBrush(Colors.Brown);
                    StudyButtonColor = new SolidColorBrush(Colors.Transparent);
                    OralTestButtonColor = new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    if (TcLessonSelectedIndex == 5)
                    {
                        TcGrammarSelectIndex = 1;
                        PracticeButtonColor = new SolidColorBrush(Colors.Brown);
                        StudyButtonColor = new SolidColorBrush(Colors.Transparent);
                    }

                }
            }
        }
        private void GoToStudyTabItem(object obj)
        {
          
            if (TcLessonSelectedIndex == 2)
            {
                TcKeyWordSelectedIndex = 0;
                PracticeButtonColor = new SolidColorBrush(Colors.Transparent);
                StudyButtonColor = new SolidColorBrush(Colors.Brown);
            }
            else
            {
                if (TcLessonSelectedIndex == 3)
                {
                    TcSentenceSelectedIndex = 0;
                    PracticeButtonColor = new SolidColorBrush(Colors.Transparent);
                    StudyButtonColor = new SolidColorBrush(Colors.Brown);
                    OralTestButtonColor = new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    if (TcLessonSelectedIndex == 5)
                    {
                        TcGrammarSelectIndex = 0;
                        PracticeButtonColor = new SolidColorBrush(Colors.Transparent);
                        StudyButtonColor = new SolidColorBrush(Colors.Brown);
                    }
                }
            }
        }       
        private void ChoosePartOfLesson()
        {
            if (TcLessonSelectedIndex == 0)
            {
                StudyButtonColor = new SolidColorBrush(Colors.Brown);
                switch (LbLessonMenuSelectedIndex)
                {
                    case 0:
                        TcLessonSelectedIndex = 1;
                        GetGetReadyQuestionList();
                        break;
                    case 1:
                        TcLessonSelectedIndex = 2;
                        LoadKeyWorData();
                        break;
                    case 2:
                        TcLessonSelectedIndex = 3;
                        break;
                    case 3:
                        TcLessonSelectedIndex = 4;
                        break;
                    case 4:
                        TcLessonSelectedIndex = 5;
                        break;
                    case 5:
                        TcLessonSelectedIndex = 6;
                        break;
                }              

            }               
        }     
        private void GoToTheLessonMenu(object obj)
        {
            TcLessonSelectedIndex = 0;
            TcKeyWordSelectedIndex = 0;TcSentenceSelectedIndex = 0; TcListenSelectedIndex = 0;
            LbLessonMenuSelectedIndex = -1; IsOpenSucessMenu = false; IsOpenFailMenu = false;
            PracticeButtonColor = new SolidColorBrush(Colors.Transparent);
            StudyButtonColor = new SolidColorBrush(Colors.Transparent);
            OralTestButtonColor = new SolidColorBrush(Colors.Transparent);
        }
        #endregion
        #region functions
        private void LoadKeyWorData()
        {
            if (KeyWordsList.Count == 0)
            {
                KeyWordsList.Clear();
                Model.GetKeyWordList(KeyWordsList, Model.dateProcess, Model.serverName);
            }

        }
        private void GetGetReadyQuestionList()
        {
          
                GettingReadyQuestionList.Clear();
            ABr = new SolidColorBrush(Colors.Transparent);
            BBr = new SolidColorBrush(Colors.Transparent);
            CBr = new SolidColorBrush(Colors.Transparent);
            DBr = new SolidColorBrush(Colors.Transparent);
            Model.GetGettingReadyList(GettingReadyQuestionList, Model.dateProcess, Model.serverName);
            CurentGetReadyQuestion = GettingReadyQuestionList[0];
            Turn = 1;
        }
        static void SpeechFromText(string text)
        {

            PromptBuilder promptBuilder = new PromptBuilder();
            promptBuilder.AppendText(text);

            PromptStyle promptStyle = new PromptStyle();
            promptStyle.Volume = PromptVolume.Loud;
            promptStyle.Rate = PromptRate.ExtraSlow;
            promptBuilder.StartStyle(promptStyle);
            promptBuilder.EndStyle();
            Thread t1 = new Thread((obj) => {
                PromptBuilder temp = (PromptBuilder)obj;
                SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
                speechSynthesizer.Speak(temp);


            });
            t1.Start(promptBuilder);


        }
        #endregion
    }
}
