using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using BusinessLogicFramework;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Speech.Synthesis;
using System.Data;
using System.Windows.Threading;
using System.IO;
using System.Windows.Xps.Packaging;
using System.IO.Packaging;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Windows.Controls.Primitives;

namespace SoBasicEnglish
{
    /// <summary>
    /// Interaction logic for StudyScreen.xaml
    /// </summary>
    public partial class StudyScreen : INotifyPropertyChanged
    {

        private ObservableCollection<KeyWordEx> KeyWordExList = new ObservableCollection<KeyWordEx>();
        private ObservableCollection<KeyWord> KeyWordsList = new ObservableCollection<KeyWord>();
        private ObservableCollection<Sentence> SentenceList = new ObservableCollection<Sentence>();
        private ObservableCollection<SentenceEx> SentenceExList = new ObservableCollection<SentenceEx>();
        private ObservableCollection<GrammarQuestion> GrammarQuestionList = new ObservableCollection<GrammarQuestion>();
        private ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList = new ObservableCollection<ListeningPart2Question>();
        private ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList___Chose = new ObservableCollection<ListeningPart2Question>();
        private string[] ListenPart2Ans;
        private ObservableCollection<ListeningQuestion> ListeningQuestionList = new ObservableCollection<ListeningQuestion>();
        private ObservableCollection<GettingReadyQuestion> GettingReadyQuestionList = new ObservableCollection<GettingReadyQuestion>();
        private GettingReadyQuestion curentGetReadyQuestion = new GettingReadyQuestion();
        private string lessonName; dbDateProcess dbDateProcess; public List<DateProcess> dateList = new List<DateProcess>();
        private List<ListeningQuestion> listeningQuestions = new List<ListeningQuestion>();
        private string date; private string userFullName; private string notChooseyet;
        private KeyWordEx curentKeyWordEx = new KeyWordEx();       
        private int turn;private SolidColorBrush quesContenBrush;
        private ObservableCollection<LessonRating> LessonratingList = new ObservableCollection<LessonRating>();
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();private DispatcherTimer timer;dbUserScore dbUserScore;
        private int GetReadyGainedScore = 0; private int KeyWordExGainedScore = 0;private int SentenceExGainedScore = 0;private int ListenPart1GainedScore = 0;private int GrammarExGainedScore = 0;
        private byte[] ListeningAudioFile; private byte[] GrammarWordFile;dbLesson  dbLesson;MediaPlayer audioMediaPlayer;private DispatcherTimer audioTimer;private bool isPlayingAudioFile = false;
        private dbLessonRating dbLessonRating;
        private ObservableCollection<TextBlock> OralWordList = new ObservableCollection<TextBlock>();
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: -170,
                offsetY: -250);
            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(2),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
        public KeyWordEx CurentKeyWordEx
        {
            get { return curentKeyWordEx; }
            set { curentKeyWordEx = value;
                OnpropertyChanged("CurentKeyWordEx");
                }
        }
        private SolidColorBrush keyWordExKeyBrush;
        public SolidColorBrush KeyWordExKeyBrush
        {
            get { return keyWordExKeyBrush; }
            set
            {
                keyWordExKeyBrush = value;
                OnpropertyChanged("KeyWordExKeyBrush");
            }
        }
        private SolidColorBrush aBr, bBr, cBr, dBr;
        public SolidColorBrush ABr
        {
            get { return aBr; }
            set { aBr = value;
                OnpropertyChanged("ABr");
            }
        }
        public SolidColorBrush BBr
        {
            get { return bBr; }
            set
            {
                bBr = value;
                OnpropertyChanged("BBr");
            }
        }
        public SolidColorBrush CBr
        {
            get { return cBr; }
            set
            {
                cBr = value;
                OnpropertyChanged("CBr");
            }
        }
        public SolidColorBrush DBr
        {
            get { return dBr; }
            set
            {
                dBr = value;
                OnpropertyChanged("DBr");
            }
        }
        public GettingReadyQuestion CurentGetReadyQuestion
        {
            get { return curentGetReadyQuestion; }
            set { curentGetReadyQuestion = value;
                OnpropertyChanged("CurentGetReadyQuestion");

            }
        }
        public SolidColorBrush QuesContenBrush
        {
            get { return quesContenBrush; }
            set { quesContenBrush = value;
                OnpropertyChanged("QuesContenBrush");
            }
        }
        public int Turn
        {
            get { return turn; }
            set
            {
                turn = value;
                OnpropertyChanged("TurnOfGetReadyQuestion");
            }
        }
        public string NotChooseyet
        {
            get { return notChooseyet; }
            set
            {
                notChooseyet = value;
                OnpropertyChanged("NotChooseyet");
            }
        }
        public string UserFullName
        {
            get { return userFullName; }
            set
            {
                userFullName = value;
                OnpropertyChanged("UserFullName");
            }
        }
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnpropertyChanged("Date");
            }
        }
        public string LessonName
        {
            get { return lessonName; }
            set
            {
                lessonName = value;
                OnpropertyChanged("LessonName");
            }
        }
       
        public StudyScreen()//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {

            InitializeComponent(); dbDateProcess = new dbDateProcess(Model.serverName);
            this.DataContext = this;
            dbLesson = new dbLesson(Model.serverName);dbLessonRating = new dbLessonRating(Model.serverName);
            var collectionVwSrc = new ListCollectionView(dateList);
            collectionVwSrc.GroupDescriptions.Add(new PropertyGroupDescription("levelDetail"));
            dataGroupedGrid.ItemsSource = collectionVwSrc;
            lbGettingReadyChoices.SelectedIndex = 0;
            LoadGettingReadyQuestionList();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += timer_Tick;dbUserScore = new dbUserScore(Model.serverName);
            audioTimer = new DispatcherTimer();
            audioTimer.Interval = TimeSpan.FromSeconds(1);
            audioTimer.Tick += AudioTimer_Tick;
            GetLessonRating(Model.dateProcess);
            string[] pr = { "i:", "I", "æ", "ɑ:", "ɔ", "ɔ:", "ʊ", "u:",  "ð", "ʃ", "ʒ", "ŋ", "ð", "ʃ", "ʒ", "ŋ" };
            foreach (var i in pr)
            {
                TextBlock temp = new TextBlock
                {
                    Text = i,
                    FontWeight = FontWeights.Bold,
                    FontSize = 25,
                    Foreground = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(5, 0, 0, 0),
                    Background = new SolidColorBrush(Colors.Transparent),
                 
                    
                };
                temp.MouseUp += Temp_Click;
               
                OralWordList.Add(temp);
               
                //  RatingChoose = new bool[5];
                //      LessonRating.AChecked = false;LessonRating.BChecked = false;LessonRating.CChecked = false;LessonRating.DChecked = false;LessonRating.EChecked = false;
            }
            SentenceWrap.ItemsSource = OralWordList;

        }

       

        private void GetLessonRating(int dateProcess)
        {
            LessonratingList.Clear();
            DataTable temp = dbLessonRating.GeLessonRatingById(dateProcess);
            foreach(DataRow i in temp.Rows)
            {
                switch (Int32.Parse(i["Rating"].ToString()))
                {
                    case 1:
                        LessonratingList.Add(new LessonRating {Check1=true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                    case 2:
                        LessonratingList.Add(new LessonRating { Check1 = true, Check2 = true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                    case 3:
                        LessonratingList.Add(new LessonRating { Check1 = true, Check2 = true, Check3 = true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                    case 4:
                        LessonratingList.Add(new LessonRating { Check1 = true, Check2 = true, Check3 = true,Check4=true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                    case 5:
                        LessonratingList.Add(new LessonRating { Check1 = true, Check2 = true, Check3 = true, Check4 = true,Check5=true, Rating = Int32.Parse(i["Rating"].ToString()), UserAvt = (byte[])i["Avt"], FeedBack = i["feedBack"].ToString(), UserName = i["userName"].ToString() });
                        break;
                }

            }
            lvLessonRating.ItemsSource = LessonratingList;
        }
        private void LoadDefaultData()
        {
            SentenceList.Clear(); KeyWordsList.Clear(); KeyWordExList.Clear(); SentenceExList.Clear(); ListeningPart2QuestionList.Clear(); ListeningQuestionList.Clear();
            GettingReadyQuestionList.Clear(); GrammarQuestionList.Clear();
            tigettingReady.IsEnabled = true;
          
        }
        private void AudioTimer_Tick(object sender, EventArgs e)
        {
          if(audioMediaPlayer!=null && audioMediaPlayer.NaturalDuration.HasTimeSpan)
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = audioMediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = audioMediaPlayer.Position.TotalSeconds;
            }
            
        }
        int tempOfTimer =0;
        static void SpeechFromText(string text)
        {
            
            PromptBuilder promptBuilder = new PromptBuilder();
            promptBuilder.AppendText(text);

            PromptStyle promptStyle = new PromptStyle();
            promptStyle.Volume = PromptVolume.Loud;
            promptStyle.Rate = PromptRate.ExtraSlow;
            promptBuilder.StartStyle(promptStyle);
            promptBuilder.EndStyle();
            Thread t1 = new Thread((obj)=>{
                PromptBuilder temp = (PromptBuilder)obj;
                SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.SelectVoiceByHints(VoiceGender.Female,VoiceAge.Adult);
                speechSynthesizer.Speak(temp);            


            });
            t1.Start(promptBuilder);

        //  t1.Abort();

          
        }
        private void timer_Tick(object sender, EventArgs e)
        {

            if (tcLesson.SelectedIndex == 0)
            {
                if (tempOfTimer == 2)
                {
                    NotChooseyet = "Not choose yet";
                    lbGettingReadyChoices.SelectedIndex = 0;
                    Turn += 1;

                    CurentGetReadyQuestion = GettingReadyQuestionList[Turn - 1];

                    btnNextQuestionGetReady.IsEnabled = false;
                    ABr = new SolidColorBrush(Colors.Transparent);
                    BBr = new SolidColorBrush(Colors.Transparent);
                    CBr = new SolidColorBrush(Colors.Transparent);
                    DBr = new SolidColorBrush(Colors.Transparent);


                }
                if (tempOfTimer == 3)
                    SpeechFromText(CurentGetReadyQuestion.KeyWord);
                if (tempOfTimer == 4)
                {
                    timer.Stop(); tempOfTimer = 0;
                }

                tempOfTimer++;
            }
            else
            {
                if (tcLesson.SelectedIndex == 1)
                {
                    if (tempOfTimer == 2)
                    {
                        NotChooseyet = "Not choose yet";
                        lbKeyWordExChoices.SelectedIndex = 0;
                        Turn += 1;
                        CurentKeyWordEx = KeyWordExList[Turn - 1];
                        btnNextKeywordQues.IsEnabled = false;
                        ABr = new SolidColorBrush(Colors.Transparent);
                        BBr = new SolidColorBrush(Colors.Transparent);
                        CBr = new SolidColorBrush(Colors.Transparent);
                        DBr = new SolidColorBrush(Colors.Transparent);


                    }
                    if (tempOfTimer == 3)                        
                         SpeechFromText(CurentKeyWordEx.KeyWord);
                    if (tempOfTimer == 4)
                    {
                        timer.Stop(); tempOfTimer = 0;
                    }

                    tempOfTimer++;
                }
            }
           
        }

        // load date function//////////////////////////
        private void LoadGettingReadyQuestionList()
        {
            QuesContenBrush = new SolidColorBrush(Colors.Transparent);
            GettingReadyQuestionList.Clear();
            Model.GetGettingReadyList(GettingReadyQuestionList, Model.dateProcess, Model.serverName);
            CurentGetReadyQuestion =GettingReadyQuestionList[0];
            Turn = 1;
            ABr = new SolidColorBrush(Colors.Transparent);
            BBr = new SolidColorBrush(Colors.Transparent);
            CBr = new SolidColorBrush(Colors.Transparent);
            DBr = new SolidColorBrush(Colors.Transparent);
        }
        private void LoadKeyWordList()
        {
            KeyWordsList.Clear();
            Model.GetKeyWordList(KeyWordsList, Model.dateProcess, Model.serverName);
            lvKeywords.ItemsSource = KeyWordsList;
            ABr = new SolidColorBrush(Colors.Transparent);
            BBr = new SolidColorBrush(Colors.Transparent);
            CBr = new SolidColorBrush(Colors.Transparent);
            DBr = new SolidColorBrush(Colors.Transparent);
            Model.GetKeyWordExListByLessonId(KeyWordExList, Model.dateProcess, Model.serverName);
            CurentKeyWordEx = KeyWordExList[0];Turn = 1;


        }
        private void LoadSentenceList()
        {
            SentenceList.Clear();
            Model.GetSentenceList(SentenceList, Model.dateProcess, Model.serverName);
            for (int i = 0; i < SentenceList.Count; i++)
            {
                SentenceList[i].Index = i;
            }

                lvSentences.ItemsSource = SentenceList;
            Model.GetSentenceExListByLessonId(SentenceExList, Model.dateProcess, Model.serverName);
           for(int i = 0; i < SentenceExList.Count; i++)
            {
                SentenceExList[i].ChoseA = false; SentenceExList[i].ChoseB = false; SentenceExList[i].ChoseC = false; SentenceExList[i].ChoseD = false;
               
            }
            lvSentenceExList.ItemsSource = SentenceExList;

        }
        private void LoadListeningData()
        {
            ListeningQuestionList.Clear();
            ListeningAudioFile = dbLesson.GetAudioFileById(Model.dateProcess);
            audioMediaPlayer = new MediaPlayer();
            audioMediaPlayer = LoadAudioFile(ListeningAudioFile);
            Model.GetListenQuestionByLessonId(ListeningQuestionList, Model.dateProcess, Model.serverName);
            for (int i = 0; i < ListeningQuestionList.Count; i++)
            {
                ListeningQuestionList[i].ChoseA = false; ListeningQuestionList[i].ChoseB = false; ListeningQuestionList[i].ChoseC = false; ListeningQuestionList[i].ChoseD = false;

            }
            lvListeningExList.ItemsSource = ListeningQuestionList;
            ListeningPart2QuestionList.Clear();          
            Model.GetListenPart2QuestionListById(ListeningPart2QuestionList, Model.dateProcess, Model.serverName);
            ListenPart2Ans = new string[5];

           // ListeningPart2QuestionList = ListeningPart2QuestionList___Chose;
            for (int i = 0; i < ListeningPart2QuestionList.Count; i++)
            {
                ListenPart2Ans[i] = ListeningPart2QuestionList[i].Value;
                ListeningPart2QuestionList[i].Value = "";

            }
            lvListenPart2QuestionList.ItemsSource = ListeningPart2QuestionList;
        }
        private void LoadGrammarData()
        {
            GrammarWordFile = dbLesson.GetGrammarWordFileByID(Model.dateProcess);
            dvGrammar.Document = GetFixedDocumentSequence(GrammarWordFile);
            Model.GetGrammarQuestionById(GrammarQuestionList, Model.dateProcess, Model.serverName);
            for (int i = 0; i < GrammarQuestionList.Count; i++)
            {
                GrammarQuestionList[i].ChoseA = false; GrammarQuestionList[i].ChoseB = false; GrammarQuestionList[i].ChoseC = false; GrammarQuestionList[i].ChoseD = false;

            }
            lvGrammarExList.ItemsSource = GrammarQuestionList;
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
        private MediaPlayer LoadAudioFile(byte[] AudioListenFile) {
            string name = System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".wav");
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), name);
            File.WriteAllBytes(path, AudioListenFile);
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(path));
            return mediaPlayer;
        }

        private void GetKeyWordExGainedScore()
        {
            foreach(KeyWordEx i in KeyWordExList)
            {
                if ((i.ChoseA && i.RightAns == 1) || (i.ChoseB && i.RightAns == 2) || (i.ChoseC && i.RightAns == 3) || (i.ChoseD && i.RightAns == 4))
                {
                   KeyWordExGainedScore  += 10;
                }
             
            }
            notifier.ShowInformation("You have gained " + KeyWordExGainedScore.ToString());
           // MessageBox.Show("You have gained " + KeyWordExGainedScore.ToString());
            tcLesson.SelectedIndex = 2;LoadSentenceList();
            tikeyWord.IsEnabled = false;
         //   tiKeyWordEx.IsEnabled = false;
        }
        private void GetTheGetReadyGainedScore()
        {
            foreach(GettingReadyQuestion i in GettingReadyQuestionList)
            {
                if((i.ChoseA && i.RightAns==1 ) || (i.ChoseB && i.RightAns == 2)|| (i.ChoseC && i.RightAns == 3)|| (i.ChoseD && i.RightAns == 4))
                {
                    GetReadyGainedScore += 10;
                }
            }

            notifier.ShowInformation("You have gained " + GetReadyGainedScore.ToString());
           // MessageBox.Show("You have gained " + GetReadyGainedScore.ToString());
                tcLesson.SelectedIndex = 1;
            tigettingReady.IsEnabled = false;
                LoadKeyWordList();
           
        }
        private void GetSentenceExGainedScore()
        {
            for(int i = 0; i<SentenceExList.Count; i++)
            {
                if((SentenceExList[i].ChoseA && SentenceExList[i].RightAns == 1) || (SentenceExList[i].ChoseB && SentenceExList[i].RightAns == 2) || (SentenceExList[i].ChoseC && SentenceExList[i].RightAns == 3) || (SentenceExList[i].ChoseD && SentenceExList[i].RightAns == 4))
                {
                    SentenceExGainedScore += 10;

                }
                switch (SentenceExList[i].RightAns)
                {
                    case 1:
                        if(SentenceExList[i].ChoseA)
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
           // MessageBox.Show("You have gained +" + SentenceExGainedScore.ToString());
            notifier.ShowInformation("You have gained +" + SentenceExGainedScore.ToString());
            LoadListeningData();
            tcLesson.SelectedIndex = 3;tiSentence.IsEnabled = false;tcListen.SelectedIndex = 0;


        }
        private void  GetListenPart1ExGainedScore() {
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
          //  MessageBox.Show("You have gained +" + ListenPart1GainedScore.ToString());
            notifier.ShowInformation("You have gained +" + ListenPart1GainedScore.ToString());
            tcListen.SelectedIndex = 1;tiListenPart1.IsEnabled = false;

        }
        private void GetListenPart2gainedScore()
        {
            int ListenPart2GainedScore = 0;
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
            ListenPart1GainedScore += ListenPart2GainedScore;
            notifier.ShowInformation("You have gained " + ListenPart2GainedScore.ToString());

            LoadGrammarData();
            tcLesson.SelectedIndex = 4;tcGrammar.SelectedIndex = 0;

           // notifier.Dispose();
        }
        private void GetGrammarEXGainedScore()
        {
            for (int i = 0; i < GrammarQuestionList.Count; i++)
            {
                if ((GrammarQuestionList[i].ChoseA && GrammarQuestionList[i].RightAns == 1) || (GrammarQuestionList[i].ChoseB && GrammarQuestionList[i].RightAns == 2) || (GrammarQuestionList[i].ChoseC && GrammarQuestionList[i].RightAns == 3) || (GrammarQuestionList[i].ChoseD && GrammarQuestionList[i].RightAns == 4))
                {
                     GrammarExGainedScore+= 10;

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
            notifier.ShowInformation("You have gained +" + GrammarExGainedScore.ToString());
            tcLesson.IsEnabled = false;
            PassOrNot();
            //MessageBox.Show("You have gained +" + GrammarExGainedScore.ToString());
        }
        private void PassOrNot()
        {
            int sumScore = 0;
            if (GetReadyGainedScore >= 50 && KeyWordExGainedScore>25 && SentenceExGainedScore>25 && ListenPart1GainedScore>=50 && GrammarExGainedScore>25  )
                sumScore += GetReadyGainedScore + KeyWordExGainedScore+ SentenceExGainedScore+ ListenPart1GainedScore+ GrammarExGainedScore;
            notifier.ShowError("You fail this leson !!");
            
        }
        private void Temp_Click(object sender, RoutedEventArgs e)
        {
            TextBlock temp = (TextBlock)sender;
            MessageBox.Show(temp.Text);
        }

        public class DateProcess
        {
            public int turnNumber { get; set; }
            public string detailInfo { get; set; }
            public string levelDetail { get; set; }
        }        
        public void GetDateProcess()
        {
            DataTable temp = new DataTable();
            temp = dbDateProcess.DatesprocessList();
            foreach (DataRow i in temp.Rows)
            {
                dateList.Add(new DateProcess { turnNumber = Convert.ToInt32(i["turnNumber"].ToString()), detailInfo = i["detailInfo"].ToString(), levelDetail = i["levelDetail"].ToString() });
            }
            var collectionVwSrc = new ListCollectionView(dateList);
            collectionVwSrc.GroupDescriptions.Add(new PropertyGroupDescription("levelDetail"));
            dataGroupedGrid.ItemsSource = collectionVwSrc;            
        }      
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnpropertyChanged(string lessonName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(lessonName));
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            LessonName = dbDateProcess.GetLessonByDateProcess(Model.dateProcess);
            Date = "LESSON "+ Model.dateProcess.ToString() ;
            UserFullName = Model.userFullname;
            NotChooseyet = "Not choose yet!!!";
           
            GetDateProcess();
          
        }       
        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DateProcess drv = (DateProcess)dataGroupedGrid.SelectedItem;
            LessonName = (drv.detailInfo).ToString(); Model.dateProcess = Int32.Parse((drv.turnNumber).ToString());

            Date = "LESSON "+ Model.dateProcess.ToString(); LoadDefaultData();
            LoadGettingReadyQuestionList();
            flProccess.IsOpen = false;
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            flProccess.IsOpen = true;
        }
        private void Tile_Click_1(object sender, RoutedEventArgs e)
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
            if(Turn<10)
                timer.Start();
            else
            {
                GetTheGetReadyGainedScore();
            }
          
        }
        private void MetroAnimatedSingleRowTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            NotChooseyet = "Not choose yet";
            lbGettingReadyChoices.SelectedIndex = 0;
        }   
        private void btnNextKeywordQues_Click(object sender, RoutedEventArgs e)
        {

            NotChooseyet = "Not choose yet";
            lbKeyWordExChoices.SelectedIndex = 0;
            switch (CurentKeyWordEx.RightAns)
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
            if (Turn < 5)
                timer.Start();
            else
            {
                GetKeyWordExGainedScore();
            }

        }
        private void ListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
         //   MessageBox.Show(lvListenQuestionList.SelectedIndex.ToString());
         //   lvListenQuestionList.SelectedIndex
        }
        private void GroupBox_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }   
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var selected = (CheckBox)sender;
            MessageBox.Show(selected.Content.ToString());
        }    
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            QuesContenBrush = new SolidColorBrush(Colors.Orchid);
        }
        private void btnAutoNextGetReadyQuestion_Checked(object sender, RoutedEventArgs e)
        {
            btnNextQuestionGetReady.IsEnabled = false;
            if(lbGettingReadyChoices.SelectedIndex > 0)
            {
                switch (lbGettingReadyChoices.SelectedIndex)
                {
                    case 1:
                        NotChooseyet = "";
                        GettingReadyQuestionList[Turn - 1].ChoseA = true;
                        GettingReadyQuestionList[Turn - 1].ChoseB = false;
                        GettingReadyQuestionList[Turn - 1].ChoseC = false;
                        GettingReadyQuestionList[Turn - 1].ChoseD = false;
                        break;
                    case 2:
                        NotChooseyet = "";
                        GettingReadyQuestionList[Turn - 1].ChoseA = false;
                        GettingReadyQuestionList[Turn - 1].ChoseB = true;
                        GettingReadyQuestionList[Turn - 1].ChoseC = false;
                        GettingReadyQuestionList[Turn - 1].ChoseD = false;
                        break;
                    case 3:
                        NotChooseyet = "";
                        GettingReadyQuestionList[Turn - 1].ChoseA = false;
                        GettingReadyQuestionList[Turn - 1].ChoseB = false;
                        GettingReadyQuestionList[Turn - 1].ChoseC = true;
                        GettingReadyQuestionList[Turn - 1].ChoseD = false;
                        break;
                    case 4:
                        NotChooseyet = "";
                        GettingReadyQuestionList[Turn - 1].ChoseA = false;
                        GettingReadyQuestionList[Turn - 1].ChoseB = false;
                        GettingReadyQuestionList[Turn - 1].ChoseC = false;
                        GettingReadyQuestionList[Turn - 1].ChoseD = true;
                        break;
                    default:
                        break;
                }
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
                    if (Turn < 10)
                    {
                        timer.Start();
                    }
                    else
                        GetTheGetReadyGainedScore();               
            }

        }
        private void btnAutoNextGetReadyQuestion_Unchecked(object sender, RoutedEventArgs e)
        {
            btnNextQuestionGetReady.IsEnabled = true;
        }
        private void btnHideKeyWordContent_Checked(object sender, RoutedEventArgs e)
        {
            KeyWordExKeyBrush = new SolidColorBrush(Colors.Orchid);
        }
        private void btnHideKeyWordContent_Unchecked(object sender, RoutedEventArgs e)
        {
            
            KeyWordExKeyBrush = new SolidColorBrush(Colors.Transparent);

        }
        private void lbKeyWordExChoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListBox)sender).SelectedIndex)
            {
                case 1:
                    NotChooseyet = "";
                    KeyWordExList[Turn - 1].ChoseA = true;
                    KeyWordExList[Turn - 1].ChoseB = false;
                    KeyWordExList[Turn - 1].ChoseC = false;
                    KeyWordExList[Turn - 1].ChoseD = false;
                    break;
                case 2:
                    NotChooseyet = "";
                    KeyWordExList[Turn - 1].ChoseA = false;
                    KeyWordExList[Turn - 1].ChoseB = true;
                    KeyWordExList[Turn - 1].ChoseC = false;
                    KeyWordExList[Turn - 1].ChoseD = false;
                    break;
                case 3:
                    NotChooseyet = "";
                    KeyWordExList[Turn - 1].ChoseA = false;
                    KeyWordExList[Turn - 1].ChoseB = false;
                    KeyWordExList[Turn - 1].ChoseC = true;
                    KeyWordExList[Turn - 1].ChoseD = false;
                    break;
                case 4:
                    NotChooseyet = "";
                    KeyWordExList[Turn - 1].ChoseA = false;
                    KeyWordExList[Turn - 1].ChoseB = false;
                    KeyWordExList[Turn - 1].ChoseC = false;
                    KeyWordExList[Turn - 1].ChoseD = true;
                    break;
                default:
                    break;
            }

            if (btnAutoNextKeyWordEx.IsChecked == true)
            {
                switch (CurentKeyWordEx.RightAns)
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
                if (Turn < 5)
                {
                    timer.Start();
                }
                else
                if (Turn >= 5)
                {
                    GetKeyWordExGainedScore();
                }
            }
            else
                if (Turn <= 5)
            {
                btnNextKeywordQues.IsEnabled = true;
            }
            else
                btnNextKeywordQues.IsEnabled = false;

        }
        private void btnAutoNextKeyWordEx_Checked(object sender, RoutedEventArgs e)
        {
            btnNextKeywordQues.IsEnabled = false;
            if (lbKeyWordExChoices.SelectedIndex > 0)
            {
                switch (lbKeyWordExChoices.SelectedIndex)
                {
                    case 1:
                        NotChooseyet = "";
                        KeyWordExList[Turn - 1].ChoseA = true;
                        KeyWordExList[Turn - 1].ChoseB = false;
                        KeyWordExList[Turn - 1].ChoseC = false;
                        KeyWordExList[Turn - 1].ChoseD = false;
                        break;
                    case 2:
                        NotChooseyet = "";
                        KeyWordExList[Turn - 1].ChoseA = false;
                        KeyWordExList[Turn - 1].ChoseB = true;
                        KeyWordExList[Turn - 1].ChoseC = false;
                        KeyWordExList[Turn - 1].ChoseD = false;
                        break;
                    case 3:
                        NotChooseyet = "";
                        KeyWordExList[Turn - 1].ChoseA = false;
                        KeyWordExList[Turn - 1].ChoseB = false;
                        KeyWordExList[Turn - 1].ChoseC = true;
                        KeyWordExList[Turn - 1].ChoseD = false;
                        break;
                    case 4:
                        NotChooseyet = "";
                        KeyWordExList[Turn - 1].ChoseA = false;
                        KeyWordExList[Turn - 1].ChoseB = false;
                        KeyWordExList[Turn - 1].ChoseC = false;
                        KeyWordExList[Turn - 1].ChoseD = true;
                        break;
                    default:
                        break;
                }
                switch (CurentKeyWordEx.RightAns)
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
                if (Turn < 5)
                {
                    timer.Start();
                }
                else
                    GetKeyWordExGainedScore();
            }
        }
        private void btnAutoNextKeyWordEx_Unchecked(object sender, RoutedEventArgs e)
        {
            btnNextKeywordQues.IsEnabled = true;
        }
        private void lvKeywords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpeechFromText(KeyWordsList[lvKeywords.SelectedIndex].Word);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SpeechFromText(CurentKeyWordEx.KeyWord);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button temp = (Button)sender;
            int index = Int32.Parse(temp.MinWidth.ToString());
            SpeechFromText(SentenceList[index].KeySentence);
        }
        private void btnSubmitDoingSentenceEx_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Done","You really want to submit?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                GetSentenceExGainedScore();
            }
        }
        private void btnResetDoingSentenceEx_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < SentenceExList.Count; i++)
            {
                SentenceExList[i].ChoseA = false; SentenceExList[i].ChoseB = false; SentenceExList[i].ChoseC = false; SentenceExList[i].ChoseD = false;
            }
        }
        private void btnResetListenSentenceEx_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ListeningQuestionList.Count; i++)
            {
                ListeningQuestionList[i].ChoseA = false; ListeningQuestionList[i].ChoseB = false; ListeningQuestionList[i].ChoseC = false; ListeningQuestionList[i].ChoseD = false;
            }
        }
        private void btnSubmitListenSentenceEx_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Done", "You really want to submit?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                GetListenPart1ExGainedScore();
            }
        }
        private void btnPlayAudio_Click(object sender, RoutedEventArgs e)
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
        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(sliProgress.Value  == sliProgress.Maximum && isPlayingAudioFile==true )
            {
                isPlayingAudioFile = false;
                audioMediaPlayer.Stop();
                audioMediaPlayer.Position = TimeSpan.FromSeconds(0);
                sliProgress.Value = 0;
                audioTimer.Stop();
            }
        }
        private void btnRePlayAudio_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Once again listen, One (-35) Score you will gain!!", "Warrning", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ListenPart1GainedScore += -35; audioTimer.Start();isPlayingAudioFile = true;
                audioMediaPlayer.Play();

            }
        }
        private void btnResetListenPart2SentenceEx_Click(object sender, RoutedEventArgs e)
        {
           for(int i = 0; i < ListeningPart2QuestionList___Chose.Count; i++)
            {
                ListeningPart2QuestionList___Chose[i].Value = "";
            }
        }     
        private void btnSubmitListenPart2SentenceEx_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Done?","you realy want to submit?", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                GetListenPart2gainedScore();
            }
            
        }
        private void btnResetGrammarEx_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < GrammarQuestionList.Count; i++)
            {
                GrammarQuestionList[i].ChoseA = false; GrammarQuestionList[i].ChoseB = false; GrammarQuestionList[i].ChoseC = false; GrammarQuestionList[i].ChoseD = false;
            }
        }
        private void btnSubmitGrammarEx_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Done?","you realy want to submit?", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                GetGrammarEXGainedScore();
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            flRating.IsOpen = true;
        }
        private void btnCloseRating_Click(object sender, RoutedEventArgs e)
        {
            flRating.IsOpen = false;
        }
        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            string er = "";
            if(dbLessonRating.InsertLessonRating(ref er,Model.dateProcess,Model.userLoginName,rtLesson.Value, tbFeedBackContent.Text)){
                notifier.ShowSuccess("you have Rated this Lesson");
                GetLessonRating(Model.dateProcess);
            }
            else
            {
                if(dbLessonRating.UpdateLessonRating(ref er, Model.dateProcess, Model.userLoginName, rtLesson.Value, tbFeedBackContent.Text))
                {
                    notifier.ShowSuccess("you have updated your rating on this Lesson");
                    GetLessonRating(Model.dateProcess);
                }
            }
        }
        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            QuesContenBrush = new SolidColorBrush(Colors.Transparent);
        }    
        private void btnSpeakGetReadyQuestion_Click(object sender, RoutedEventArgs e)
        {
           
            SpeechFromText(CurentGetReadyQuestion.KeyWord);
        }
        private void lbGettingReadyChoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (((ListBox)sender).SelectedIndex)
            {
                case 1:
                    NotChooseyet = "";
                    GettingReadyQuestionList[Turn - 1].ChoseA = true;
                    GettingReadyQuestionList[Turn - 1].ChoseB = false;
                    GettingReadyQuestionList[Turn - 1].ChoseC = false;
                    GettingReadyQuestionList[Turn - 1].ChoseD = false;
                    break;
                case 2:
                    NotChooseyet = "";
                    GettingReadyQuestionList[Turn - 1].ChoseA = false;
                    GettingReadyQuestionList[Turn - 1].ChoseB = true;
                    GettingReadyQuestionList[Turn - 1].ChoseC = false;
                    GettingReadyQuestionList[Turn - 1].ChoseD = false;
                    break;
                case 3:
                    NotChooseyet = "";
                    GettingReadyQuestionList[Turn - 1].ChoseA = false;
                    GettingReadyQuestionList[Turn - 1].ChoseB = false;
                    GettingReadyQuestionList[Turn - 1].ChoseC = true;
                    GettingReadyQuestionList[Turn - 1].ChoseD = false;
                    break;
                case 4:
                    NotChooseyet = "";
                    GettingReadyQuestionList[Turn - 1].ChoseA = false;
                    GettingReadyQuestionList[Turn - 1].ChoseB = false;
                    GettingReadyQuestionList[Turn - 1].ChoseC = false;
                    GettingReadyQuestionList[Turn - 1].ChoseD = true;
                    break;
                default:
                    break;
            }

            if (btnAutoNextGetReadyQuestion.IsChecked == true)
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
                if (Turn < 10)
                {
                    timer.Start();
                }
                else
                if(Turn >= 10)
                {
                    GetTheGetReadyGainedScore();
                }
            }
            else
                if (Turn <= 10)
            {
                btnNextQuestionGetReady.IsEnabled = true;
            }
            else
                btnNextQuestionGetReady.IsEnabled = false;

        }
    }
}
