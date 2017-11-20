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

namespace SoBasicEnglish.ViewModels
{
    public class EditorViewModel : INotifyPropertyChanged
    {
        #region ICommand object
        public ICommand Click_ResetItem { get; set; }
        public ICommand Click_AddItem { get; set; }
        public ICommand Click_DeleteItem { get; set; }
        public ICommand GotFocus_HowToReadKeyWord { get; set; }
        public ICommand Click_ChooseKeyWordImage { get; set; }
        public ICommand Click_PlayAudioListenFile { get; set; }
        public ICommand Click_ChooseAudioListenFile { get; set; }
        public ICommand Click_PauseAudioListenFile { get; set; }
        public ICommand Click_StopAudioListenFile { get; set; }
        public ICommand Click_BrowWordFile { get; set; }
        public ICommand Click_ViewWordFile { get; set; }
        public ICommand Click_StartCreatingLeson { get; set; }
        public ICommand Click_SubmitCreatingLesson { get; set; }
        public ICommand lvDate_SelectedtionChange { get; set; }
        public ICommand Click_CancelCreatingLesson { get; set; }
        public ICommand TcLessonDate_SelectedtionChange { get; set; }
        public ICommand Click_EditLessonItem { get; set; }
        public ICommand GotFocus_HowToReadSentence { get; set; }
        public ICommand Click_DeleteLesson { get; set; }
        public ICommand Click_EditNameOfLesson { get; set; }
        public ICommand Click_SubmitAnswerQuestion { get; set; }
        #endregion
        #region objects

        private dbDateProcess dbDateProcess;private byte[] DefaultImage;private byte[] AudioListenFile = null; private MediaPlayer mediaPlayer = new MediaPlayer();
        private DispatcherTimer timer;private DispatcherTimer TimeCoShowMessage;private bool mediaPlayerIsPlaying = false; private byte[] WordFile; private string convertedXpsDoc = ""; int temp = 0;private int DateProcess = 0;
        dbLesson dbLesson;private bool EditingLesson = false;private dbLessonQuestion dbLessonQuestion;
        private DispatcherTimer TimerForCreating;
        public ObservableCollection<LessonQuestion> LessonQuestionList { get => _lessonQuestionList; set { _lessonQuestionList = value; NotifyPropertyChanged("AnswerOfQuestion"); } }
        private ObservableCollection<LessonQuestion> _lessonQuestionList = new ObservableCollection<LessonQuestion>();
        public ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList { get => _listeningPart2QuestionList; set { _listeningPart2QuestionList = value; NotifyPropertyChanged("ListeningPart2QuestionList"); } }
        private ObservableCollection<ListeningPart2Question> _listeningPart2QuestionList = new ObservableCollection<ListeningPart2Question>();
        public ObservableCollection<ListeningQuestion> ListeningQuestionList { get => _listeningQuestionList; set { _listeningQuestionList = value; NotifyPropertyChanged("ListeningQuestionList"); } }
        private ObservableCollection<ListeningQuestion> _listeningQuestionList = new ObservableCollection<ListeningQuestion>();
        public ObservableCollection<Sentence> SentenceList { get => _sentenceList; set { _sentenceList = value; NotifyPropertyChanged("SentenceList"); } }
        private ObservableCollection<Sentence> _sentenceList = new ObservableCollection<Sentence>();
        public ObservableCollection<KeyWordEx> KeyWordExList { get => _keyWordExList; set { _keyWordExList = value; NotifyPropertyChanged("KeyWordExList"); } }
        private ObservableCollection<KeyWordEx> _keyWordExList = new ObservableCollection<KeyWordEx>();
        public ObservableCollection<DateProcess> DateList { get => _dateList; set { _dateList = value; NotifyPropertyChanged("DateList"); } }
        private ObservableCollection<DateProcess> _dateList = new ObservableCollection<DateProcess>();
        public ObservableCollection<GettingReadyQuestion> GettingReadyQuestionList { get => _gettingReadyQuestionList; set { _gettingReadyQuestionList = value; NotifyPropertyChanged("GettingReadyQuestionList"); } }
        private ObservableCollection<GettingReadyQuestion> _gettingReadyQuestionList = new ObservableCollection<GettingReadyQuestion>();
        public ObservableCollection<KeyWord> KeyWordsList { get => _keyWordsList; set { _keyWordsList = value; NotifyPropertyChanged("KeyWordsList"); } }
        private ObservableCollection<KeyWord> _keyWordsList = new ObservableCollection<KeyWord>();
        public ObservableCollection<Button> PrnonList { get => _prnonList; set { _prnonList = value; NotifyPropertyChanged("PrnonList"); } }
        private ObservableCollection<Button> _prnonList = new ObservableCollection<Button>();
        public ObservableCollection<SentenceEx> SentenceExList { get => _sentenceExList; set { _sentenceExList = value; NotifyPropertyChanged("SentenceExList"); } }
        private ObservableCollection<SentenceEx> _sentenceExList = new ObservableCollection<SentenceEx>();
        public ObservableCollection<GrammarQuestion> GrammarQuestionList { get => _grammarQuestionList; set { _grammarQuestionList = value; NotifyPropertyChanged("GrammarQuestionList"); } }
        private ObservableCollection<GrammarQuestion> _grammarQuestionList = new ObservableCollection<GrammarQuestion>();
        public ObservableCollection<Level> Levels { get => _levels; set { _levels = value; NotifyPropertyChanged("Levels"); } }
        private ObservableCollection<Level> _levels = new ObservableCollection<Level>();
        public int TcSentenceSelectedInDex { get => _tcSentenceSelectedInDex; set { _tcSentenceSelectedInDex = value; NotifyPropertyChanged("TcSentenceSelectedInDex"); } }
        private int _tcSentenceSelectedInDex = 0;
        public int TcListenSelectedInDex { get => _tcListenSelectedInDex; set { _tcListenSelectedInDex = value; NotifyPropertyChanged("TcListenSelectedInDex"); } }
        private int _tcListenSelectedInDex = 0;
        public bool Isopen_Symbols { get => _isopen_Symbols; set { _isopen_Symbols = value; NotifyPropertyChanged("Isopen_Symbols"); } }
        private bool _isopen_Symbols = false;
        public int SelectedIndexOfList { get => _selectedIndexOfKeyWord; set { _selectedIndexOfKeyWord = value; NotifyPropertyChanged("SelectedIndexOfList"); } }
        private int _selectedIndexOfKeyWord = 0;
        public int TcLessonSelectedIndex { get => _tcLessonSelectedIndex; set { _tcLessonSelectedIndex = value; NotifyPropertyChanged("TcLessonSelectedIndex"); } }
        private int _tcLessonSelectedIndex = 0;
        private int _tcKeyWordSelectedIndex = 0;
        public int TcKeyWordSelectedInDex { get => _tcKeyWordSelectedIndex; set { _tcKeyWordSelectedIndex = value; NotifyPropertyChanged("TcKeyWordSelectedInDex"); } }
        public double SliderMaxValue { get => _sliderMaxValue; set { _sliderMaxValue = value; NotifyPropertyChanged("SliderMaxValue"); } }
        private double _sliderMaxValue = 0;
        public double SliderValue { get => _sliderValue; set { _sliderValue = value; NotifyPropertyChanged("SliderValue"); } }
        private double _sliderValue = 0;
        public bool IsLoadingGrammarFile { get => _isLoadingGrammarFile; set { _isLoadingGrammarFile = value; NotifyPropertyChanged("IsLoadingGrammarFile"); } }
        private bool _isLoadingGrammarFile = false;
        public string WordFilePath { get => _wordFilePath; set { _wordFilePath = value; NotifyPropertyChanged("WordFilePath"); } }
        private string _wordFilePath = "";
        public IDocumentPaginatorSource DocumentWordFile { get => _documentWordFile; set { _documentWordFile = value; NotifyPropertyChanged("DocumentWordFile"); } }
        private IDocumentPaginatorSource _documentWordFile;
        public int CbLessonTypeSelectedIndex { get => _cbLessonTypeSelectedIndex; set { _cbLessonTypeSelectedIndex = value; NotifyPropertyChanged("CbLessonTypeSelectedIndex"); } }
        private int _cbLessonTypeSelectedIndex = 0;
        public string LessonName { get => _lessonName; set { _lessonName = value; NotifyPropertyChanged("LessonName"); } }
        private string _lessonName = "";
        public bool CreatingLesson { get => _creatingLesson; set { _creatingLesson = value; NotifyPropertyChanged("CreatingLesson"); } }
        private bool _creatingLesson = false;
        public bool IsCreating { get => _isCreating; set { _isCreating = value; NotifyPropertyChanged("IsCreating"); } }
        private bool _isCreating = false;
        public string StateOfCreating { get => _stateOfCreating; set { _stateOfCreating = value; NotifyPropertyChanged("StateOfCreating"); } }
        private string _stateOfCreating = "";
        public bool IsOpenMessage { get => _isOpenMessage; set { _isOpenMessage = value; NotifyPropertyChanged("IsOpenMessage"); } }
        private bool _isOpenMessage = false;
        public string MessageOfEdit { get => _messageOfEdit; set { _messageOfEdit = value; NotifyPropertyChanged("MessageOfEdit"); } }
        private string _messageOfEdit = "";        
        public ListCollectionView DateListOnGridView
        {
            get => _dateListOnGridView; set
            {
                _dateListOnGridView = value;
                NotifyPropertyChanged("DateListOnGridView");
            }
        }
        private ListCollectionView _dateListOnGridView;
        public event PropertyChangedEventHandler PropertyChanged;        
        #endregion
        #region Constructor
        public EditorViewModel()
        {
            dbDateProcess = new dbDateProcess(Model.serverName); GetDateProcess(); GetSymbols();dbLesson = new dbLesson(Model.serverName);
            System.Data.DataTable temp2 = dbDateProcess.GetLevelList(); Levels.Clear(); GetDefaultData(); dbLessonQuestion = new dbLessonQuestion(Model.serverName);
            foreach (DataRow i in temp2.Rows)
            {
                Levels.Add(new Level { LevelIndex = Int32.Parse(i["Id"].ToString()), LevelValue = i["levelDetail"].ToString() });
            }            
            Click_ResetItem = new DelegateCommand(ResetItems);
            Click_AddItem = new DelegateCommand(AddItem);
            Click_DeleteItem = new RelayCommand<object>((p) => p != null, DeleteItem);
            GotFocus_HowToReadKeyWord = new RelayCommand<object>((p)=>true,GotFocusHowToReadKeyWord);
            Click_ChooseKeyWordImage = new RelayCommand<object>((p) => true, ClickChooseKeyWordImage);
            Click_ChooseAudioListenFile = new DelegateCommand(GetAudioListenFile);
            Click_PlayAudioListenFile = new DelegateCommand(PlayAudioListenFile);
            Click_PauseAudioListenFile = new DelegateCommand(PauseAudioListenFile);
            Click_StopAudioListenFile = new DelegateCommand(StopAudioListenFile);
            Click_BrowWordFile = new DelegateCommand(BrowseWordFile);
            Click_ViewWordFile = new RelayCommand<object>(CheckFilePathGrammar,ViewTheGrammarFile);
            Click_StartCreatingLeson = new RelayCommand<object>((p) => !CreatingLesson, StartCreatingLesson);
            Click_SubmitCreatingLesson = new RelayCommand<object>(CheckCanCreatLessonOrNot, SubmitCreatingLesson);
            lvDate_SelectedtionChange = new RelayCommand<object>((p) => !CreatingLesson, SrartEditLesson);
            Click_CancelCreatingLesson = new RelayCommand<object>((p)=>CreatingLesson,CancelCreatingLesson);
            TcLessonDate_SelectedtionChange = new DelegateCommand(TcLessonSelecionChanged);
            Click_EditLessonItem = new RelayCommand<object>((p) => EditingLesson, EditingLessonItem);
            Click_DeleteLesson = new RelayCommand<object>((p) => EditingLesson, DeleteLesson);
            GotFocus_HowToReadSentence = new RelayCommand<object>((p) => true, GotFocusHowToReadSentence);
            Click_EditNameOfLesson = new RelayCommand<object>((p) => EditingLesson, EditNameOfLesson);
            Click_SubmitAnswerQuestion = new RelayCommand<UIElementCollection>((p) => p != null, SubmitAnswerQuestion);

             timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            TimerForCreating = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            TimerForCreating.Tick += TimerForCreating_Tick;
            TimeCoShowMessage = new DispatcherTimer();
            TimeCoShowMessage.Interval = TimeSpan.FromMilliseconds(1200);
            TimeCoShowMessage.Tick += TimeCoShowMessage_Tick;
        }
    
        private void SubmitAnswerQuestion(UIElementCollection obj)////////////////////////////////
        {
            string tbContentOfAnswer = "";
            int IdOfQuestion = 0;
           foreach (var i in obj)
            {
                TextBox tb = i as TextBox;
                if (tb != null)
                {
                    if (tb.Name == "tbContentOfAnswer")
                        tbContentOfAnswer = tb.Text;
                }
                else
                {
                    TextBlock tbl = i as TextBlock;
                    if (tbl != null)
                        if (tbl.Name == "IdOfQuestion")
                            IdOfQuestion = Int32.Parse(tbl.Text);
                }

            }
            string er = "";
            if(dbLessonQuestion.UpdateAnswerOfLessonQuestion(ref er, IdOfQuestion, tbContentOfAnswer))
            {
                LessonQuestionList.Clear();
                IsOpenMessage = true;
                MessageOfEdit = "A question has been answered";
                TimeCoShowMessage.Start();
                dbUserNotify dbUserNotify = new dbUserNotify(Model.serverName);
                if(dbUserNotify.InsertNotify(ref er,Model.userLoginName,"Your question about the lesson '"+LessonName+"' is responsed. Check it out!"))
                {
                    if (LessonQuestionList.Count == 0)
                    {
                        var temp = dbLessonQuestion.GetLessonQuestionByDateProcess_full(Model.dateProcess);

                        foreach (DataRow i in temp.Rows)
                        {
                            LessonQuestionList.Add(new LessonQuestion
                            {
                                UserAvt = (byte[])i["userAvatar"],
                                UserName = i["userName"].ToString(),
                                ContentOfQuestion = i["contentOfQuestion"].ToString(),
                                AnswerOfQuestion = i["contentOfAnswer"].ToString(),
                                TimeOfAsk = (Convert.ToDateTime(i["timeOfAsk"])).ToString(),
                                Id = Int32.Parse(i["id"].ToString())
                            });
                        }
                    }
                }
                
             
            }
        }
        #endregion
        #region ICommands
        private void EditNameOfLesson(object obj)
        {
            string er = "";
            if (dbLesson.EditNameOfLesson(ref er, LessonName, Model.dateProcess))
            {
                IsOpenMessage = true;
                MessageOfEdit = "You have edited the name of lesson!";
                GetDateProcess();
                TimeCoShowMessage.Start();
            }
        }

        private void DeleteLesson(object obj)
        {
            string er = "";
            if (dbLesson.DeleteLessonByTurnNumber(ref er, Model.dateProcess))
            {
                GetDateProcess();
                CreatingLesson = false; EditingLesson = false;
            }
        }
        private void TcLessonSelecionChanged()
        {
            if (EditingLesson)
            {

                if (TcLessonSelectedIndex == 1)
                {
                    if (KeyWordsList.Count == 0)
                        Model.GetKeyWordList(KeyWordsList, Model.dateProcess, Model.serverName);
                    if (KeyWordExList.Count == 0)
                    {
                        Model.GetKeyWordExListByLessonId_full(KeyWordExList, Model.dateProcess, Model.serverName);
                    }
                }
                else
                {
                    if (TcLessonSelectedIndex == 2)
                    {
                        if (SentenceList.Count == 0)
                            Model.GetSentenceList(SentenceList, Model.dateProcess, Model.serverName);
                        if (SentenceExList.Count == 0)
                            Model.GetSentenceExListByLessonId_full(SentenceExList, Model.dateProcess, Model.serverName);
                    }
                    else
                    {
                        if (TcLessonSelectedIndex == 3)
                        {

                            if (ListeningQuestionList.Count == 0)
                            {
                                Model.GetListenQuestionByLessonId_full(ListeningQuestionList, Model.dateProcess, Model.serverName);
                            }
                            if (ListeningPart2QuestionList.Count == 0)
                                Model.GetListenPart2QuestionListById_full(ListeningPart2QuestionList, Model.dateProcess, Model.serverName);
                            if (AudioListenFile == null)
                            {
                                AudioListenFile = dbLesson.GetAudioFileById(Model.dateProcess);
                                string name = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".wav");
                                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), name);
                                File.WriteAllBytes(path, AudioListenFile);
                                mediaPlayer.Stop();
                                mediaPlayer.Open(new Uri(path));
                                timer.Stop();
                                mediaPlayerIsPlaying = false;
                            }
                        }
                        else
                        {
                            if (TcLessonSelectedIndex == 4)
                            {
                                if (WordFile == null)
                                {
                                    WordFile = dbLesson.GetGrammarWordFileByID(Model.dateProcess);
                                    DocumentWordFile = GetFixedDocumentSequence(WordFile);
                                }
                                if (GrammarQuestionList.Count == 0)
                                {
                                    Model.GetGrammarQuestionById_full(GrammarQuestionList, Model.dateProcess, Model.serverName);
                                }
                            }
                            else
                            {
                                if (TcLessonSelectedIndex == 5)
                                {
                                    if (LessonQuestionList.Count == 0)
                                    {
                                        //load data here

                                        var temp = dbLessonQuestion.GetLessonQuestionByDateProcess_full(Model.dateProcess);
                                        foreach (DataRow i in temp.Rows)
                                        {
                                            LessonQuestionList.Add(new LessonQuestion
                                            {
                                                UserAvt = (byte[])i["userAvatar"],
                                                UserName = i["userName"].ToString(),
                                                ContentOfQuestion = i["contentOfQuestion"].ToString(),
                                                AnswerOfQuestion = i["contentOfAnswer"].ToString(),
                                                TimeOfAsk = (Convert.ToDateTime(i["timeOfAsk"])).ToString(),
                                                Id = Int32.Parse(i["id"].ToString())
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
           
        }
        private void EditingLessonItem(object obj)
        {
            string er = "";
            if (TcLessonSelectedIndex == 0)
            {
               
                //edit the get ready leson 
                if (dbLesson.DeleteGetReadyQuestionByLessonId(ref er, Model.dateProcess))
                {
                    IsOpenMessage = Model.InsertGetReadyQuestion(GettingReadyQuestionList, Model.dateProcess);
                    MessageOfEdit = "You have edited the GetreadyQuestionList!";
                    TimeCoShowMessage.Start();
                }

            }
            else
            {
                if (TcLessonSelectedIndex == 1)
                {
                    if (TcKeyWordSelectedInDex == 0)
                    {
                        if(dbLesson.DeleteKeyWordByLessonId(ref er, Model.dateProcess))
                        {
                            IsOpenMessage = Model.InsertKeyWord(KeyWordsList, Model.dateProcess);
                            MessageOfEdit = "You have edited the KeyWordList!";
                            TimeCoShowMessage.Start();
                        }
                    }
                    else
                    {
                        if(dbLesson.DeleteKeyWordExesByLessonId(ref er, Model.dateProcess)){
                            IsOpenMessage = Model.InsertKeyWordExes(KeyWordExList, Model.dateProcess);
                            MessageOfEdit = "You have edited the KeyWordExList!";
                            TimeCoShowMessage.Start();
                        }
                    }
                }
                else
                {
                    if (TcLessonSelectedIndex == 2)
                    {
                        if (TcSentenceSelectedInDex == 0)
                        {
                            if(dbLesson.DeleteSentencesByLessonId(ref er, Model.dateProcess))
                            {
                                IsOpenMessage = Model.InsertSentences(SentenceList, Model.dateProcess);
                                MessageOfEdit = "You have edited the SentenceQuestionList!";
                                TimeCoShowMessage.Start();
                            }
                        }
                        else
                        {
                            if(dbLesson.DeleteSentencesEXByLessonId(ref er, Model.dateProcess))
                            {
                                IsOpenMessage = Model.InsertSentenceExes(SentenceExList, Model.dateProcess);
                                MessageOfEdit = "You have edited the SentenceExQuestionList!";
                                TimeCoShowMessage.Start();
                            }
                        }
                    }
                    else
                    {
                        if (TcLessonSelectedIndex == 3)
                        {
                            if(dbLesson.UpdateListenAudioFile(ref er,Model.dateProcess,AudioListenFile) && dbLesson.DeleteListenQuestionByLessonId(ref er,Model.dateProcess) && dbLesson.DeleteListenPart2QuestionByLessonId(ref er, Model.dateProcess))
                            {
                                IsOpenMessage = Model.InsertListeningExes(ListeningQuestionList, Model.dateProcess) && Model.InsertListeningPart2Exes(ListeningPart2QuestionList, Model.dateProcess);
                                MessageOfEdit = "You have edited the Listening part";
                                TimeCoShowMessage.Start();
                            }
                        }
                        else
                        {
                            if (TcLessonSelectedIndex == 4)
                            {
                                if(dbLesson.UpdateWordGrammarFile(ref er,Model.dateProcess,WordFile) && dbLesson.DeleteGrammarQuestionByLessonId(ref er,Model.dateProcess))
                                {
                                    IsOpenMessage = Model.InsertGrammarExes(GrammarQuestionList, Model.dateProcess);
                                    MessageOfEdit = "You have edited the Grammar part";
                                    TimeCoShowMessage.Start();
                                }
                            }
                        }
                    }
                }
            }
        }
        private void CancelCreatingLesson(object obj)
        {
            GetDefaultData();
            EditingLesson = false;
            CreatingLesson = false;
        }
        private void SrartEditLesson(object obj)
        {
            GetDefaultData();
            DateProcess temp = obj as DateProcess;
            LessonName = temp.DetailInfo;
            CbLessonTypeSelectedIndex = temp.ProcessLevel -1;
            CreatingLesson = true;EditingLesson = true;
            TcLessonSelectedIndex = 0;Model.dateProcess = temp.TurnNumber;
            Model.GetGettingReadyList_full(GettingReadyQuestionList,temp.TurnNumber, Model.serverName);
        }
        private bool CheckCanCreatLessonOrNot(object obj)
        {
            return CreatingLesson && !String.IsNullOrEmpty(LessonName) && WordFile != null && AudioListenFile != null;
        }
        private void SubmitCreatingLesson(object e)
        {
            IsCreating = true;
            TimerForCreating.Start();
        }
      
        private void StartCreatingLesson(object e)
        {
            CreatingLesson = true;EditingLesson = false;
            CbLessonTypeSelectedIndex = 0;
        }
        private bool CheckFilePathGrammar(object p)
        {
            string a = p as string;
            return !String.IsNullOrEmpty(a) && !IsLoadingGrammarFile;
        }
        private void ViewTheGrammarFile(object p)
        {
            DocumentWordFile = GetFixedDocumentSequence(WordFile);
        }
        private void BrowseWordFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            //  openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Word documents(*.doc;*.docx)|*.doc;*.docx";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                IsLoadingGrammarFile = true;
                if (openFileDialog.FileName.Length > 0)
                {
                   WordFilePath = openFileDialog.FileName;
                    Thread t1 = new Thread((p) => {
                        convertedXpsDoc = string.Concat(System.IO.Path.GetTempPath(), "\\", Guid.NewGuid().ToString(), ".xps");
                        XpsDocument xpsDocument = ConvertWordToXps(WordFilePath, convertedXpsDoc);
                        WordFile = System.IO.File.ReadAllBytes(convertedXpsDoc);
                        IsLoadingGrammarFile = false;
                    });
                    t1.Start();                 
                }
            }
        }
        private void ClickChooseKeyWordImage(object obj)
        {
            KeyWord temp = obj as KeyWord;
            try
            {
                if (temp != null)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    //  openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (openFileDialog.ShowDialog() != null)
                    {

                        BitmapImage a = new BitmapImage(new Uri(openFileDialog.FileName));
                        var bm = new Bitmap(SoBasicEnglish.Properties.Resources.customer);
                        MemoryStream memStream = new MemoryStream();
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(a));
                        encoder.Save(memStream);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);

                        memStream = Model.compress(img);
                        KeyWordsList[temp.Index].PictureOfWord = memStream.ToArray(); memStream.Close();
                    };
                }
            }
            catch (Exception) { }
           
        }
        private void GotFocusHowToReadKeyWord(object pram)
        {

            KeyWord temp = pram as KeyWord;
            if (temp != null)
                SelectedIndexOfList = temp.Index;

            Isopen_Symbols = true;
        }
        private void GotFocusHowToReadSentence(object obj)
        {
            Sentence temp = obj as Sentence;
            if (temp != null)
            {
                SelectedIndexOfList = temp.Index;
                Isopen_Symbols = true;

            }
        }
        #region itemActions
        private void DeleteItem(object obj)
        {
            Isopen_Symbols = false;
            if (TcLessonSelectedIndex == 0)
            {
                
                GettingReadyQuestionList.RemoveAt(SelectedIndexOfList);
            }
            else
            {
                if (TcLessonSelectedIndex == 1)
                {
                    if (TcKeyWordSelectedInDex == 0)
                    {
                        for (int i = KeyWordsList.Count - 1; i > SelectedIndexOfList; i--)
                        {
                            KeyWordsList[i].Index = KeyWordsList[i - 1].Index;
                        }
                        KeyWordsList.RemoveAt(SelectedIndexOfList);
                    }
                    else
                    {
                        KeyWordExList.RemoveAt(SelectedIndexOfList);
                    }
                }
                else
                {
                    if (TcLessonSelectedIndex == 2)
                    {
                        if (TcSentenceSelectedInDex == 0)
                        {
                            for (int i = SentenceList.Count - 1; i > SelectedIndexOfList; i--)
                            {
                                SentenceList[i].Index = SentenceList[i - 1].Index;
                            }                          
                            SentenceList.RemoveAt(SelectedIndexOfList);
                        }
                        else
                        {
                            SentenceExList.RemoveAt(SelectedIndexOfList);
                        }
                    }
                    else
                    {
                        if (TcLessonSelectedIndex == 3)
                        {
                            if (TcListenSelectedInDex == 0)
                            {
                                ListeningQuestionList.RemoveAt(SelectedIndexOfList);
                            }
                            else
                                ListeningPart2QuestionList.RemoveAt(SelectedIndexOfList);
                        }
                        else
                        {
                            if (TcLessonSelectedIndex == 4)
                            {
                                GrammarQuestionList.RemoveAt(SelectedIndexOfList);
                            }
                        }
                    }
                }
               
            }
        }
        private void ResetItems()
        {
            Isopen_Symbols = false;
            if (TcLessonSelectedIndex == 0)
            {
                GettingReadyQuestionList.Clear();
                for (int i = 0; i < 5; i++)
                {
                    GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                    GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                }
            }
            else
            {
                if (TcLessonSelectedIndex == 1)
                {
                    if (TcKeyWordSelectedInDex == 0)
                    {
                        KeyWordsList.Clear();
                        for(int i = 0; i < 10; i++)
                        {
                            KeyWordsList.Add(new KeyWord { Index = i, Word = "word", ExSentence = "aa bb", HowToReadWord = "ádbasijci", VieWord = "sadđ", PictureOfWord = DefaultImage, TypeOfWord = "noun" });
                        }
                    }
                    else
                    {
                        KeyWordExList.Clear();
                        for(int i = 0; i < 10; i++)
                        {
                            KeyWordExList.Add(new KeyWordEx { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "a", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                        }
                    }
                }
                else
                {
                    if(TcLessonSelectedIndex == 2)
                    {
                        if (TcSentenceSelectedInDex == 0)
                        {
                            SentenceList.Clear();
                            for (int i = 0; i < 10; i++)
                            {
                                SentenceList.Add(new Sentence { Index = i, KeySentence = "What'up man??", HowToRead = "ádasdaasda", VieMeanOfSentence = " Gì đó mày ?" });
                            }
                        }
                        else
                        {
                            SentenceExList.Clear();
                            for (int i = 0; i < 10; i++)
                            {
                                SentenceExList.Add(new SentenceEx { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                            }
                        }
                    }
                    else
                    {
                        if (TcLessonSelectedIndex == 3)
                        {
                            if(TcListenSelectedInDex==0)
                            {
                                ListeningQuestionList.Clear();
                                for(int i = 0; i < 10; i ++)
                                {
                                    ListeningQuestionList.Add(new ListeningQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, QuestionContent = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                                }
                            }
                            else
                            {
                                ListeningPart2QuestionList.Clear();
                                for(int i=0;i<10;i++)
                                    ListeningPart2QuestionList.Add(new ListeningPart2Question { Before = "aaaaaaaaaaaa", Value = "bbbbbbbbb", After = "cccccccccc" });
                            }

                        }
                        else
                        {
                            if (TcLessonSelectedIndex == 4)
                            {
                                GrammarQuestionList.Clear();
                                for(int i=0;i<10;i++)
                                    GrammarQuestionList.Add(new GrammarQuestion { ChoseB = true, ChoseA = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });


                            }
                        }
                    }

                }
            }
           
        }
        private void AddItem()
        {
            Isopen_Symbols = false;
            if (TcLessonSelectedIndex==0)
                GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
            else
            {
                if (TcLessonSelectedIndex == 1)
                {
                    if (TcKeyWordSelectedInDex == 0)
                        KeyWordsList.Add(new KeyWord { Index = KeyWordsList.Count, Word = "word", ExSentence = "aa bb", HowToReadWord = "ádbasijci", VieWord = "sadđ", PictureOfWord = DefaultImage, TypeOfWord = "noun" });
                    else
                        KeyWordExList.Add(new KeyWordEx { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "a", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                }
                else
                {
                    if (TcLessonSelectedIndex == 2)
                    {
                        if(TcSentenceSelectedInDex==0)
                            SentenceList.Add(new Sentence { Index = SentenceList.Count, KeySentence = "What'up man??", HowToRead = "ádasdaasda", VieMeanOfSentence = " Gì đó mày ?" });
                        else
                            SentenceExList.Add(new SentenceEx { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });

                    }
                    else
                    {
                        if (TcLessonSelectedIndex == 3)
                        {
                            if(TcListenSelectedInDex==0)
                                ListeningQuestionList.Add(new ListeningQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, QuestionContent = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                             else
                                ListeningPart2QuestionList.Add(new ListeningPart2Question { Before = "aaaaaaaaaaaa", Value = "bbbbbbbbb", After = "cccccccccc" });
                        }
                        else
                        {
                            if(TcLessonSelectedIndex==4)
                                GrammarQuestionList.Add(new GrammarQuestion { ChoseB = true, ChoseA = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                        }
                    }
                }

            }

        }
        #endregion
        private void GetAudioListenFile()
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
            try
            {
                var result = openFileDialog.ShowDialog();
                AudioListenFile = File.ReadAllBytes(openFileDialog.FileName);
                string name = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".wav");
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), name);
                File.WriteAllBytes(path, AudioListenFile);
                mediaPlayer.Stop();
                mediaPlayer.Open(new Uri(path));
                timer.Stop();
                mediaPlayerIsPlaying = false;
              
            }
            catch (Exception) { }
        }
        #endregion
        #region functons
        private void GetDefaultData()
        {
            MemoryStream ms = new MemoryStream();
            Properties.Resources.customer.Save(ms, Properties.Resources.customer.RawFormat);
            AudioListenFile = null;WordFile = null; 
            DocumentWordFile = null ; WordFilePath = "";WordFile = null;
            DefaultImage = ms.GetBuffer();
            ms.Close();
            GettingReadyQuestionList.Clear(); KeyWordsList.Clear(); KeyWordExList.Clear(); SentenceList.Clear(); SentenceExList.Clear(); ListeningQuestionList.Clear(); ListeningPart2QuestionList.Clear();
            GrammarQuestionList.Clear();LessonQuestionList.Clear();

        }
        private XpsDocument ConvertWordToXps(string wordFilename, string xpsFilename)
        {
            // Create a WordApplication and host word document
            Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            try
            {
                wordApp.Documents.Open(wordFilename);

                // To Invisible the word document
                wordApp.Application.Visible = false;

                // Minimize the opened word document
                wordApp.WindowState = WdWindowState.wdWindowStateMinimize;

                Document doc = wordApp.ActiveDocument;

                doc.SaveAs(xpsFilename, WdSaveFormat.wdFormatXPS);

                XpsDocument xpsDocument = new XpsDocument(xpsFilename, FileAccess.Read);
                return xpsDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs, The error message is  " + ex.ToString());
                return null;
            }
            finally
            {
                wordApp.Documents.Close();
                ((_Application)wordApp).Quit(WdSaveOptions.wdDoNotSaveChanges);
            }
        }
        private FixedDocumentSequence GetFixedDocumentSequence(byte[] xpsBytes)
        {
            Uri packageUri;
            XpsDocument xpsDocument = null;

            using (MemoryStream xpsStream = new MemoryStream(xpsBytes))
            {
                using (Package package = Package.Open(xpsStream))
                {
                    packageUri = new Uri("memorystream://myXps.xps");

                    try
                    {
                        PackageStore.AddPackage(packageUri, package);
                        xpsDocument = new XpsDocument(package, CompressionOption.Maximum, packageUri.AbsoluteUri);

                        return xpsDocument.GetFixedDocumentSequence();
                    }
                    finally
                    {
                        if (xpsDocument != null)
                        {
                            xpsDocument.Close();
                        }
                        PackageStore.RemovePackage(packageUri);
                    }
                }
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void GetDateProcess()
        {
            System.Data.DataTable temp = new System.Data.DataTable();
            temp = dbDateProcess.DatesprocessList();
            DateList.Clear();
            foreach (DataRow i in temp.Rows)
            {
                DateList.Add(new DateProcess { ProcessLevel = Int32.Parse(i["processLevel"].ToString()),TurnNumber = Convert.ToInt32(i["turnNumber"].ToString()), DetailInfo = i["detailInfo"].ToString(), LevelDetail = i["levelDetail"].ToString() });
            }
            _dateListOnGridView = new ListCollectionView(DateList);
            _dateListOnGridView.GroupDescriptions.Add(new PropertyGroupDescription("LevelDetail"));
            
        }
        private void GetSymbols()
        {
            string[] pr = { "i:", "æ", "ɑ:", "ɔ", "ɔ:", "ʊ", "u:", "ʌ", "ɜ:", "ə", "ei", "əʊ", "ai", "aʊ", "ɔi", "iə", "eə", "ʊə", "tʃ", "dʒ", "ɵ", "ð", "ʃ", "ʒ", "ŋ", };
            PrnonList.Clear();
            foreach (var i in pr)
            {
                Button temp = new Button
                {
                    Content = i,
                    FontWeight = FontWeights.Bold,
                    FontSize = 20,
                    Margin = new Thickness(5, 5, 5, 5),
                    BorderThickness = new Thickness(2),
                    Width = 50,
                    Height = 50,
                    BorderBrush = new SolidColorBrush(Colors.Black)
                };
                temp.Click += Temp_Click;
                PrnonList.Add(temp);
            }
        }
        private void Temp_Click(object sender, RoutedEventArgs e)
        {
            if (TcLessonSelectedIndex == 1)
            {
                Button temp = (Button)sender;
                KeyWordsList[SelectedIndexOfList].HowToReadWord += temp.Content;
            }
            else
            {
                Button temp = (Button)sender;
                SentenceList[SelectedIndexOfList].HowToRead += temp.Content;
            }
          

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
          
                SliderMaxValue = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                SliderValue = mediaPlayer.Position.TotalSeconds;
            }

        }
        private void PauseAudioListenFile()
        {
            if (mediaPlayer != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                mediaPlayer.Pause();
                timer.Stop();
                mediaPlayerIsPlaying = false;
            }
              
        }
        private void PlayAudioListenFile()
        {
            if (AudioListenFile != null)
            {
                mediaPlayer.Play();
                timer.Start();
                mediaPlayerIsPlaying = true;
            }
        }
        private void StopAudioListenFile()
        {
            if (mediaPlayer != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                mediaPlayer.Stop();
                mediaPlayerIsPlaying = false;
                mediaPlayer.Position = TimeSpan.FromSeconds(0);
                timer.Stop();
            }
        }
        private void TimeCoShowMessage_Tick(object sender, EventArgs e)
        {
            IsOpenMessage = false;
            TimeCoShowMessage.Stop();
        }
        private void TimerForCreating_Tick(object sender, EventArgs e)
        {
            bool result = false; temp += 2;
            switch (temp)
            {
                case 2:
                    StateOfCreating = "Lesson...";string err = "";
                    result = dbLesson.InsertLesson(ref err, CbLessonTypeSelectedIndex + 1, LessonName);                   
                    break;
                case 4:
                    StateOfCreating = "Getting ready...";
                    DateProcess = dbLesson.GetMaxLessonIndexByLevelId(CbLessonTypeSelectedIndex + 1);
                    result = Model.InsertGetReadyQuestion(GettingReadyQuestionList,DateProcess);
                    break;
                case 8:
                    StateOfCreating = "Key Words...";
                    result = Model.InsertKeyWord(KeyWordsList,DateProcess);
                    break;
                case 12:
                    StateOfCreating = "Key Words Exe...";
                    result = Model.InsertKeyWordExes(KeyWordExList,DateProcess);
                    break;
                case 16:
                    StateOfCreating = "Sentences...";
                    result = Model.InsertSentences(SentenceList,DateProcess);
                    break;
                case 20:
                    StateOfCreating = "Sentences exe...";
                    result = Model.InsertSentenceExes(SentenceExList,DateProcess);
                    break;
                case 24:
                    StateOfCreating = "Audio listening file...";
                    result = Model.InsertListenAudioFile(AudioListenFile,DateProcess);
                    break;
                case 28:
                    StateOfCreating = " listening Exe Part 1...";
                    result = Model.InsertListeningExes(ListeningQuestionList,DateProcess);
                    break;
                case 32:
                    StateOfCreating = " listening Exe Part 2...";
                    result = Model.InsertListeningPart2Exes(ListeningPart2QuestionList,DateProcess);
                    break;
                case 36:
                    StateOfCreating = " Grammar...";
                    result = Model.InsertGrammar(DateProcess,WordFile);
                    break;
                case 40:
                    StateOfCreating = " Grammar Exe...";
                    result = Model.InsertGrammarExes(GrammarQuestionList,DateProcess);
                    break;
                case 44:                    
                    StateOfCreating = " Done";
                    break;
                case 46:                    
                    IsCreating = false;
                    GetDefaultData();
                    temp = 0;
                    StateOfCreating = "";
                    TimerForCreating.Stop();
                    GetDateProcess();
                    break;
                default:
                    break;

            }
        }
        #endregion  


    }
}
