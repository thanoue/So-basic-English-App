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
        public ICommand Click_ListenSentence { get; set; }
        public ICommand SelectionChanged_lvSentenceOralList { get; set; }
        public ICommand Click_StartToSpeech { get; set; }
        public ICommand Click_PlayTheAudio { get; set; }
        public ICommand Click_EidtTheNoteOfLesson { get; set; }
        #endregion
        #region objects
        public LessonQuestionViewModel LessonQuestionDataContext { get => _lessonQuestionDataContext; set { _lessonQuestionDataContext = value; NotifyPropertyChanged("LessonQuestionDataContext"); } }
        private LessonQuestionViewModel _lessonQuestionDataContext = new LessonQuestionViewModel();
        
        DispatcherTimer TimeToNextQuestion, audioTimer,TimeToCloseMessage; private bool CheckAutoNext = false;private int ListenPart1GainedScore = 0;private int ListenPart2GainedScore=0; private int KeyWordExGainedScore = 0; private int GetReadyGainedScore = 0;private int SentenceExGainedScore=0;
        private SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();private string keySentenceToSpeech = ""; private byte[] ListeningAudioFile; private byte[] GrammarWordFile;
        dbLesson dbLesson;MediaPlayer audioMediaPlayer;private bool isPlayingAudioFile = false;private string[] ListenPart2Ans;private int GrammarExGainedScore = 0;
        #region colors
        private SolidColorBrush _aBr = new SolidColorBrush(Colors.Transparent), _bBr = new SolidColorBrush(Colors.Transparent), _cBr = new SolidColorBrush(Colors.Transparent), _dBr = new SolidColorBrush(Colors.Transparent), _quesContenBrush = new SolidColorBrush(Colors.Transparent), _bVoiceRecBtn = new SolidColorBrush(Colors.Green);
        private SolidColorBrush _bResultOfSpeech = new SolidColorBrush(Colors.Wheat);
        public SolidColorBrush BVoiceRecBtn { get => _bVoiceRecBtn; set { _bVoiceRecBtn = value; NotifyPropertyChanged("BVoiceRecBtn"); } }
        public SolidColorBrush QuesContenBrush { get => _quesContenBrush; set { _quesContenBrush = value; NotifyPropertyChanged("QuesContenBrush"); } }
        public SolidColorBrush ABr { get => _aBr; set { _aBr = value; NotifyPropertyChanged("ABr"); } }
        public SolidColorBrush BBr { get => _bBr; set { _bBr = value; NotifyPropertyChanged("BBr"); } }
        public SolidColorBrush CBr { get => _cBr; set { _cBr = value; NotifyPropertyChanged("CBr"); } }
        public SolidColorBrush DBr { get => _dBr; set { _dBr = value; NotifyPropertyChanged("DBr"); } }
        #endregion
        private string _contentOfMessage = "";
        public string ContentOfMessage { get => _contentOfMessage; set { _contentOfMessage = value;NotifyPropertyChanged("ContentOfMessage"); } }
        public bool IsOpenMessage { get => _isOpenMessage; set { _isOpenMessage = value; NotifyPropertyChanged("IsOpenMessage"); } }
        private bool _isOpenMessage = false;
        public double ListenSliderValue { get => _listenSliderValue; set { _listenSliderValue = value; NotifyPropertyChanged("ListenSliderValue"); } }
        private double _listenSliderValue = 0;
        public string NoteOfLesson { get => _noteOfLesson; set { _noteOfLesson = value; NotifyPropertyChanged("NoteOfLesson"); } }
        private string _noteOfLesson = "";
        public double ListenSliderMaxValue { get => _listenSliderMaxValue; set { _listenSliderMaxValue = value; NotifyPropertyChanged("ListenSliderMaxValue"); } }
        private double _listenSliderMaxValue = 0;
        public string VoiceOfUserOraltest { get => _voiceOfUserOraltest; set { _voiceOfUserOraltest = value; NotifyPropertyChanged("VoiceOfUserOraltest"); } }
        public SolidColorBrush BResultOfSpeech { get => _bResultOfSpeech; set { _bResultOfSpeech = value; NotifyPropertyChanged("BResultOfSpeech"); } }
        private string _voiceOfUserOraltest = "";
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
        public IDocumentPaginatorSource DocumentWordFile { get => _documentWordFile; set { _documentWordFile = value; NotifyPropertyChanged("DocumentWordFile"); } }
        private IDocumentPaginatorSource _documentWordFile;
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
        public ObservableCollection<TextBlock> OralWordList { get => _oralWordList; set { _oralWordList = value; NotifyPropertyChanged("OralWordList"); } }
        private ObservableCollection<TextBlock> _oralWordList = new ObservableCollection<TextBlock>();
        public ObservableCollection<GrammarQuestion> GrammarQuestionList { get => _grammarQuestionList; set { _grammarQuestionList = value; NotifyPropertyChanged("GrammarQuestionList"); } }


        private ObservableCollection<GrammarQuestion> _grammarQuestionList = new ObservableCollection<GrammarQuestion>();
        #endregion
        #endregion
        #region constructor
        public StudyViewModel()
        {
            dbLesson = new dbLesson(Model.serverName);
            SelectionChanged_lbMenuLesson = new DelegateCommand(ChoosePartOfLesson);
            Click_GoToTheMenuLb = new RelayCommand<object>((p)=>!DoingEx,GoToTheLessonMenu);
            Click_GoToStudyTabItem = new RelayCommand<object>((p) => !DoingEx  , GoToStudyTabItem);
            Click_GoToPracticeTabItem = new RelayCommand<object>((p)=>!DoingEx , GoToPracticeTabItem);
            Click_GoToOralTestTabItem = new RelayCommand<object>((p) => !DoingEx, GoToOralTestTabItem);
            Click_GoToListenPart1EX = new RelayCommand<object>((p) => true, GoToListenPart1EX);
            Click_GoToListenPart2EX = new RelayCommand<object>((p) => true, GoToListenPart2EX);
            Click_GotoNextGetReadyQuestion = new RelayCommand<object>(CheckIfCanNextQuestion,GotoNextGetReadyQuestion);
            Check_ToggleBtnShowQuestion = new DelegateCommand(CheckBtnShowQuestion);
            lbGetReadyQuesListSelectionChanged = new DelegateCommand(Change);
            Click_ListenKeyWord = new RelayCommand<object>((p) => true, ListenKeyWord);
            Check_ToggleBtnAutoNextQuestion = new DelegateCommand(CheckBtnAutoNextQuestion);
            Click_ListenEXSentenceOfKeyWord = new RelayCommand<object>((p) => true, ListenEXSentenceOfKeyWord);
            Click_StartDoingEX = new RelayCommand<object>((p) => !DoingEx, StartDoingEX);
            Click_RestartDoingEX = new RelayCommand<object>((p) => DoingEx, RestartDoingEX);
            Click_ListenSentence = new RelayCommand<object>((p)=>true,ListenSentence);
            Click_SubmitEX = new RelayCommand<object>((p) => DoingEx, SubmitEX); Click_CloseArlet = new DelegateCommand(CLoseArlet);
            SelectionChanged_lvSentenceOralList = new RelayCommand<Object>((p) => true, lvSentenceOralListSelectionChanged);
            Click_StartToSpeech = new RelayCommand<object>( CheckSelectSentenceOrNot, StartToSpeech);
            Click_PlayTheAudio = new DelegateCommand(PlayTheAudioListenFile);
            Click_EidtTheNoteOfLesson = new DelegateCommand(EidtTheNoteOfLesson);
            audioTimer = new DispatcherTimer();audioTimer.Interval = TimeSpan.FromSeconds(1);
            audioTimer.Tick += AudioTimer_Tick;
            TimeToCloseMessage = new DispatcherTimer(); TimeToCloseMessage.Interval = TimeSpan.FromMilliseconds(1300);
            TimeToCloseMessage.Tick += TimeToCloseMessage_Tick;
              TimeToNextQuestion = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1.5)               
            }; TimeToNextQuestion.Tick += TimeToNextQuestion_Tick;
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
        }
        private void TimeToCloseMessage_Tick(object sender, EventArgs e)
        {
            IsOpenMessage = false;
            TimeToCloseMessage.Stop();
        }
        #endregion
        #region commad function
        private void EidtTheNoteOfLesson()
        {
            string er = "";
            if(dbLesson.UpdateLessonNote(ref er, Model.dateProcess, Model.userLoginName, NoteOfLesson))
            {
                ContentOfMessage = "You've edited your lesson note";
                IsOpenMessage = true;
                TimeToCloseMessage.Start();

            }
        }
        private bool CheckSelectSentenceOrNot(object a)
        {
            return !(OralWordList.Count == 0);
        }
        private void lvSentenceOralListSelectionChanged(object obj)
        {
            var temp = obj as Sentence;
            if(temp!=null)
            LoadOralSentence(temp.KeySentence);
            recEngine.SetInputToDefaultAudioDevice();
            keySentenceToSpeech = temp.KeySentence;
            //VoiceOfUserOraltest = "";
            GrammarBuilder(temp.KeySentence);
        }
        private void ListenSentence(object obj)
        {
            var temp = obj as Sentence;
            SpeechFromText_MaleVoice(temp.KeySentence);
        }
        private void CLoseArlet()
        {
            if (TcLessonSelectedIndex == 1)
            {
                IsOpenFailMenu = false; IsOpenSucessMenu = false;
                TcLessonSelectedIndex = 0; LbLessonMenuSelectedIndex = -1;
            }
            else
            {
               
                    IsOpenFailMenu = false; IsOpenSucessMenu = false;
            }

        }
        private void StartDoingEX(object obj)
        {
            if (TcLessonSelectedIndex == 2)
            {
                DoingEx = true;
                KeyWordExList.Clear();
                Model.GetKeyWordExListByLessonId(KeyWordExList, Model.dateProcess, Model.serverName);
            }
            else
            {
                if (TcLessonSelectedIndex == 3)
                {
                    DoingEx = true;
                    SentenceExList.Clear();
                    Model.GetSentenceExListByLessonId(SentenceExList, Model.dateProcess, Model.serverName);
                }
                else
                {
                    if (TcLessonSelectedIndex == 4)
                    {
                        DoingEx = true;
                        ListeningQuestionList.Clear();
                        Model.GetListenQuestionByLessonId(ListeningQuestionList, Model.dateProcess, Model.serverName);
                        ListeningPart2QuestionList.Clear();
                        Model.GetListenPart2QuestionListById(ListeningPart2QuestionList, Model.dateProcess, Model.serverName);
                        ListenPart2Ans = new string[ListeningPart2QuestionList.Count];                       
                        for (int i = 0; i < ListeningPart2QuestionList.Count; i++)
                        {
                            ListenPart2Ans[i] = ListeningPart2QuestionList[i].Value;
                            ListeningPart2QuestionList[i].Value = "";
                        }
                    }
                    if (TcLessonSelectedIndex == 5)
                    {
                        GrammarQuestionList.Clear();
                        DoingEx = true;
                        Model.GetGrammarQuestionById(GrammarQuestionList, Model.dateProcess, Model.serverName);

                    }
                }
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
            else
            {
                if (TcLessonSelectedIndex == 3)
                {
                    SentenceExGainedScore = 0;
                    for (int i = 0; i < SentenceExList.Count; i++)
                    {

                        if ((SentenceExList[i].ChoseA && SentenceExList[i].RightAns == 1) || (SentenceExList[i].ChoseB && SentenceExList[i].RightAns == 2) || (SentenceExList[i].ChoseC && SentenceExList[i].RightAns == 3) || (SentenceExList[i].ChoseD && SentenceExList[i].RightAns == 4))
                        {
                            SentenceExGainedScore += 10;

                        }
                        switch (SentenceExList[i].RightAns)
                        {
                            case 1:
                                if (SentenceExList[i].ChoseA)
                                    SentenceExList[i].BgA = new SolidColorBrush(Colors.PaleGreen);
                                else
                                    SentenceExList[i].BgA = new SolidColorBrush(Colors.Orchid);
                                break;
                            case 2:
                                if (SentenceExList[i].ChoseB)
                                    SentenceExList[i].BgB = new SolidColorBrush(Colors.PaleGreen);
                                else
                                    SentenceExList[i].BgB = new SolidColorBrush(Colors.Orchid);
                                break;
                            case 3:
                                if (SentenceExList[i].ChoseC)
                                    SentenceExList[i].BgC = new SolidColorBrush(Colors.PaleGreen);
                                else
                                    SentenceExList[i].BgC = new SolidColorBrush(Colors.Orchid);
                                break;
                            case 4:
                                if (SentenceExList[i].ChoseD)
                                    SentenceExList[i].BgD = new SolidColorBrush(Colors.PaleGreen);
                                else
                                    SentenceExList[i].BgD = new SolidColorBrush(Colors.Orchid);
                                break;
                            default:
                                break;
                        }
                    }
                    GainedScore = SentenceExGainedScore;
                    if (GainedScore >= (SentenceExList.Count * 10) / 2)
                    {
                        IsOpenSucessMenu = true;
                        SpeechFromText("Successfull!!");
                    }
                    else
                    {
                        IsOpenFailMenu = true;
                        SpeechFromText("You are fail!!");
                    }
                       
                    DoingEx = false;
                }
                else
                {
                    if (TcLessonSelectedIndex == 4)
                    {
                        GetListenGainedScore();
                        GainedScore = ListenPart1GainedScore;
                        GetListenPart2GainedScore();
                        GainedScore += ListenPart2GainedScore;
                        if (GainedScore >=( ((ListeningQuestionList.Count * 10) / 2)+((ListeningPart2QuestionList.Count*10)/2)))
                        {
                            IsOpenSucessMenu = true;
                            SpeechFromText("Successfull!!");
                        }
                        else
                        {
                            IsOpenFailMenu = true;
                            SpeechFromText("You are fail!!");
                        }

                        DoingEx = false;
                    }
                    else
                    {
                        if (TcLessonSelectedIndex == 5)
                        {
                            GrammarExGainedScore = 0;
                            for (int i = 0; i < GrammarQuestionList.Count; i++)
                            {
                                if ((GrammarQuestionList[i].ChoseA && GrammarQuestionList[i].RightAns == 1) || (GrammarQuestionList[i].ChoseB && GrammarQuestionList[i].RightAns == 2) || (GrammarQuestionList[i].ChoseC && GrammarQuestionList[i].RightAns == 3) || (GrammarQuestionList[i].ChoseD && GrammarQuestionList[i].RightAns == 4))
                                {
                                    GrammarExGainedScore += 10;

                                }
                                switch (GrammarQuestionList[i].RightAns)
                                {
                                    case 1:
                                        if (GrammarQuestionList[i].ChoseA)
                                            GrammarQuestionList[i].BgA = new SolidColorBrush(Colors.PaleGreen);
                                        else
                                            GrammarQuestionList[i].BgA = new SolidColorBrush(Colors.Orchid);
                                        break;
                                    case 2:
                                        if (GrammarQuestionList[i].ChoseB)
                                            GrammarQuestionList[i].BgB = new SolidColorBrush(Colors.PaleGreen);
                                        else
                                            GrammarQuestionList[i].BgB = new SolidColorBrush(Colors.Orchid);
                                        break;
                                    case 3:
                                        if (GrammarQuestionList[i].ChoseC)
                                            GrammarQuestionList[i].BgC = new SolidColorBrush(Colors.PaleGreen);
                                        else
                                            GrammarQuestionList[i].BgC = new SolidColorBrush(Colors.Orchid);
                                        break;
                                    case 4:
                                        if (GrammarQuestionList[i].ChoseD)
                                            GrammarQuestionList[i].BgD = new SolidColorBrush(Colors.PaleGreen);
                                        else
                                            GrammarQuestionList[i].BgD = new SolidColorBrush(Colors.Orchid);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            GainedScore = GrammarExGainedScore;
                            if (GainedScore >= (GrammarQuestionList.Count * 10) / 2)
                            {
                                IsOpenSucessMenu = true;
                                SpeechFromText("Successfull!!");
                            }
                            else
                            {
                                IsOpenFailMenu = true;
                                SpeechFromText("You are fail!!");
                            }

                            DoingEx = false;
                        }
                    }
                }

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
            else
            {
                if (TcLessonSelectedIndex == 3)
                {
                    for(int i = 0; i < SentenceExList.Count; i++)
                    {
                        SentenceExList[i].ChoseA = false; SentenceExList[i].ChoseB = false; SentenceExList[i].ChoseC = false; SentenceExList[i].ChoseD = false;
                    }
                }
                else
                {
                    if (TcLessonSelectedIndex == 4)
                    {
                        for(int i = 0; i < ListeningQuestionList.Count; i++)
                        {
                            ListeningQuestionList[i].ChoseA = false; ListeningQuestionList[i].ChoseB = false; ListeningQuestionList[i].ChoseC = false; ListeningQuestionList[i].ChoseD = false;
                        }
                        for(int i = 0;i< ListeningPart2QuestionList.Count; i++)
                        {
                            ListeningPart2QuestionList[i].Value = "";
                        }
                    }
                    else
                    {
                        if (TcLessonSelectedIndex == 5)
                        {
                            for(int i = 0; i < GrammarQuestionList.Count; i++)
                            {
                                GrammarQuestionList[i].ChoseA = false; GrammarQuestionList[i].ChoseB = false; GrammarQuestionList[i].ChoseC = false; GrammarQuestionList[i].ChoseD = false;
                            }
                        }
                    }
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
                        LoadSentenceData();
                        break;
                    case 3:
                        TcLessonSelectedIndex = 4;
                        if (audioMediaPlayer != null && audioMediaPlayer.NaturalDuration.HasTimeSpan)
                        {
                            audioMediaPlayer.Stop();                           
                            audioMediaPlayer.Position = TimeSpan.FromSeconds(0);
                            audioMediaPlayer.Stop();
                        }
                        else {
                            LoadListenData();
                        }                      
                        break;
                    case 4:
                        TcLessonSelectedIndex = 5;
                       if(GrammarWordFile==null)
                            GetGrammarData();
                    
                        break;
                    case 5:
                        TcLessonSelectedIndex = 6;
                        if (NoteOfLesson == "")
                            NoteOfLesson = dbLesson.GetLessonNoteByUserLoginName_Date(Model.userLoginName, Model.dateProcess);
                        break;
                }              

            }               
        }
        private void GoToTheLessonMenu(object obj)
        {
            TcLessonSelectedIndex = 0;
            if (audioMediaPlayer != null && audioMediaPlayer.NaturalDuration.HasTimeSpan)
            {
                audioMediaPlayer.Stop();
                audioMediaPlayer.Position = TimeSpan.FromSeconds(0);
                audioMediaPlayer.Stop();
            }
            TcKeyWordSelectedIndex = 0;TcSentenceSelectedIndex = 0; TcListenSelectedIndex = 0;
            LbLessonMenuSelectedIndex = -1; IsOpenSucessMenu = false; IsOpenFailMenu = false;
            PracticeButtonColor = new SolidColorBrush(Colors.Transparent);
            StudyButtonColor = new SolidColorBrush(Colors.Transparent);
            OralTestButtonColor = new SolidColorBrush(Colors.Transparent);
        }
        #endregion
        #region functions
        #region Grammar functions
        private FixedDocumentSequence GetFixedDocumentSequence(byte[] xpsBytes)
        {
            Uri packageUri;
            XpsDocument xpsDocument = null;
            using (MemoryStream xpsStream = new MemoryStream(xpsBytes))
            {
                using (Package package = Package.Open(xpsStream))
                {
                   // packageUri = new Uri("memorystream://myXps.xps");
                    string inMemoryPackageName = "memorystream://myXps.xps";
                    packageUri = new Uri(inMemoryPackageName);
                    try
                    {
                        PackageStore.AddPackage(packageUri, package);
                        xpsDocument = new XpsDocument(package, CompressionOption.Maximum, packageUri.AbsoluteUri);
                        return xpsDocument.GetFixedDocumentSequence();
                    }               
                    finally
                    {
                        PackageStore.RemovePackage(packageUri);
                        if (xpsDocument != null)
                        {
                            xpsDocument.Close();
                        }
                    }


                }
            }
        }
        private void GetGrammarData()
        {
            GrammarWordFile = dbLesson.GetGrammarWordFileByID(Model.dateProcess);
            DocumentWordFile = GetFixedDocumentSequence(GrammarWordFile);
        }
        #endregion
        #region Listening functions
        private void PlayTheAudioListenFile()
        {
            if (isPlayingAudioFile)
            {
                audioMediaPlayer.Pause(); isPlayingAudioFile = false;
            }
            else
            {
                audioTimer.Start(); audioMediaPlayer.Play(); isPlayingAudioFile = true;
            }
        }
        private void GetListenPart2GainedScore()
        {
            ListenPart2GainedScore = 0;
            for (int i = 0; i < ListeningPart2QuestionList.Count; i++)
            {
                if (ListeningPart2QuestionList[i].Value == ListenPart2Ans[i])
                {
                    ListeningPart2QuestionList[i].ValueBrush = new SolidColorBrush(Colors.Green);
                    ListenPart2GainedScore += 10;
                }
                else
                {
                    ListeningPart2QuestionList[i].ValueBrush = new SolidColorBrush(Colors.Red);
                    ListeningPart2QuestionList[i].Value = ListenPart2Ans[i];
                }
            }
        }
        private void GetListenGainedScore()
        {
            ListenPart1GainedScore = 0;
            for (int i = 0; i < ListeningQuestionList.Count; i++)
            {
                if ((ListeningQuestionList[i].ChoseA && ListeningQuestionList[i].RightAns == 1) || (ListeningQuestionList[i].ChoseB && ListeningQuestionList[i].RightAns == 2) || (ListeningQuestionList[i].ChoseC && ListeningQuestionList[i].RightAns == 3) || (ListeningQuestionList[i].ChoseD && ListeningQuestionList[i].RightAns == 4))
                {
                    ListenPart1GainedScore += 10;
                }
                switch (ListeningQuestionList[i].RightAns)
                {
                    case 1:
                        if (ListeningQuestionList[i].ChoseA)
                            ListeningQuestionList[i].BgA = new SolidColorBrush(Colors.PaleGreen);
                        else
                            ListeningQuestionList[i].BgA = new SolidColorBrush(Colors.Orchid);
                        break;
                    case 2:
                        if (ListeningQuestionList[i].ChoseB)
                            ListeningQuestionList[i].BgB = new SolidColorBrush(Colors.PaleGreen);
                        else
                            ListeningQuestionList[i].BgB = new SolidColorBrush(Colors.Orchid);
                        break;
                    case 3:
                        if (ListeningQuestionList[i].ChoseC)
                            ListeningQuestionList[i].BgC = new SolidColorBrush(Colors.PaleGreen);
                        else
                            ListeningQuestionList[i].BgC = new SolidColorBrush(Colors.Orchid);
                        break;
                    case 4:
                        if (ListeningQuestionList[i].ChoseD)
                            ListeningQuestionList[i].BgD = new SolidColorBrush(Colors.PaleGreen);
                        else
                            ListeningQuestionList[i].BgD = new SolidColorBrush(Colors.Orchid);
                        break;
                    default:
                        break;
                }
            }
        }
        private void AudioTimer_Tick(object sender, EventArgs e)
        {
            if (audioMediaPlayer != null && audioMediaPlayer.NaturalDuration.HasTimeSpan)
            {
                ListenSliderMaxValue = audioMediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                ListenSliderValue = audioMediaPlayer.Position.TotalSeconds;
            }         
        }
        private void LoadListenData()
        {
            ListeningAudioFile = dbLesson.GetAudioFileById(Model.dateProcess);
            audioMediaPlayer = new MediaPlayer();
            audioMediaPlayer = LoadAudioFile(ListeningAudioFile);
            ListenSliderValue = 0;
            
          
        }
        private MediaPlayer LoadAudioFile(byte[] AudioListenFile)
        {
            string name = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".wav");
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), name);
            File.WriteAllBytes(path, AudioListenFile);
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(path));
            return mediaPlayer;
        }
        #endregion
        private void StartToSpeech(object a)
        {
            BVoiceRecBtn = new SolidColorBrush(Colors.Brown);
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
        }
        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            VoiceOfUserOraltest = e.Result.Text;
            if (VoiceOfUserOraltest !="")
            {
                recEngine.RecognizeAsyncStop();
                BResultOfSpeech = new SolidColorBrush(Colors.Wheat);
                BVoiceRecBtn = new SolidColorBrush(Colors.Green);
            }
            //else
            //{
            //    recEngine.RecognizeAsyncStop();
            //    BResultOfSpeech = new SolidColorBrush(Colors.Green);
            //    BVoiceRecBtn = new SolidColorBrush(Colors.Green);
            //}
        }
        private void LoadOralSentence(string Sentence)
        {
            List<string> pr = new List<string>(); string tempString = "";
            foreach (char i in Sentence.Trim())
            {
                if (i != ' ')
                {
                    tempString += i;
                }
                else
                {
                    pr.Add(tempString);
                    tempString = "";
                }
            }
            pr.Add(tempString);
            OralWordList.Clear();
            
            foreach (var i in pr)
            {
                TextBlock temp = new TextBlock
                {
                    Text = i,
                    FontWeight = FontWeights.Bold,
                    FontSize = 28,
                    Foreground = new SolidColorBrush(Colors.Wheat),
                    Margin = new Thickness(9, 0, 0, 0),
                    Background = new SolidColorBrush(Colors.Transparent),             
                };
                temp.MouseUp += Temp_Click;
                OralWordList.Add(temp);
            }          

        }
        private void Temp_Click(object sender, MouseButtonEventArgs e)
        {
            var temp = sender as TextBlock;
            SpeechFromText(temp.Text);
        }
        private void GrammarBuilder(string inputgrammar)
        {
            Choices command = new Choices();
            command.Add(inputgrammar);
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.AppendDictation();
           // gBuilder.Append(command);
            Grammar grammar = new Grammar(gBuilder);
            recEngine.LoadGrammarAsync(grammar);
          
        }
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
        private void LoadSentenceData()
        {
            SentenceList.Clear();
            Model.GetSentenceList(SentenceList, Model.dateProcess, Model.serverName);
        }
        static void SpeechFromText_MaleVoice(string text)
        {

            PromptBuilder promptBuilder = new PromptBuilder();
            promptBuilder.AppendText(text);
            PromptStyle promptStyle = new PromptStyle();
            promptStyle.Volume = PromptVolume.ExtraLoud;
            promptStyle.Rate = PromptRate.Medium;
            promptBuilder.StartStyle(promptStyle);
            promptBuilder.EndStyle();
            Thread t1 = new Thread((obj) => {
                PromptBuilder temp = (PromptBuilder)obj;
                SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
                speechSynthesizer.Speak(temp);
            });
            t1.Start(promptBuilder);
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
