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
using System.ComponentModel;
using System.Collections.ObjectModel;
using BusinessLogicFramework;
using System.Windows.Threading;
using System.Data;

namespace SoBasicEnglish
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : INotifyPropertyChanged
    {
        private double valueProgress;dbLesson dbLesson;int temp = 0;string err = "";
        private ObservableCollection<KeyWordEx> KeyWordExList = new ObservableCollection<KeyWordEx>();
        private ObservableCollection<KeyWord> KeyWordsList = new ObservableCollection<KeyWord>();
        private ObservableCollection<Sentence> SentenceList = new ObservableCollection<Sentence>();
        private ObservableCollection<SentenceEx> SentenceExList = new ObservableCollection<SentenceEx>();
        private ObservableCollection<GrammarQuestion> GrammarQuestionList = new ObservableCollection<GrammarQuestion>();
        private ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList = new ObservableCollection<ListeningPart2Question>();
        private ObservableCollection<ListeningQuestion> ListeningQuestionList = new ObservableCollection<ListeningQuestion>();
        private ObservableCollection<GettingReadyQuestion> GettingReadyQuestionList = new ObservableCollection<GettingReadyQuestion>();
        byte[] WordGrammarFile; byte[] AudioListenFile;int Level = 0;int DateProcess = 0; DispatcherTimer timer;
        private string progresstext = "";
        public string ProgressText
        {
            get { return progresstext; }
            set
            {
                progresstext = value;
                this.NotifyPropertyChanged("ProgressText");
            }
        }
        public double ValueProgress{
            get { return valueProgress; }
            set
            {
                valueProgress = value;
                this.NotifyPropertyChanged("ValueProgress");
            }

            }
        public SplashScreen(int level, ObservableCollection<KeyWord> keyWordsList, ObservableCollection<KeyWordEx> keyWordExList, ObservableCollection<Sentence> sentenceList,
            ObservableCollection<SentenceEx> sentenceExList, ObservableCollection<GrammarQuestion> grammarQuestionList, ObservableCollection<ListeningPart2Question> listeningPart2QuestionList,
            ObservableCollection<ListeningQuestion> listeningQuestionList, ObservableCollection<GettingReadyQuestion> gettingReadyQuestionList,byte[] audioListenFile,byte[] wordGrammarFile)
        {

            InitializeComponent(); this.DataContext = this; dbLesson = new dbLesson(Model.serverName);
            this.Level = level; this.KeyWordsList = keyWordsList; this.KeyWordExList = keyWordExList;
            this.SentenceList = sentenceList;this.SentenceExList = sentenceExList;this.GrammarQuestionList = grammarQuestionList; this.ListeningPart2QuestionList = listeningPart2QuestionList;
            this.ListeningQuestionList = listeningQuestionList;this.GettingReadyQuestionList = gettingReadyQuestionList;this.AudioListenFile = audioListenFile; this.WordGrammarFile = wordGrammarFile;
            valueProgress = 0;ProgressText = "";
            pbInsert.Maximum = 44;temp = 0; ValueProgress = 0;        
        }
        private bool InsertGetReadyQuestion()
        {
            bool result = false;
            for(int turnNumber=0; turnNumber< GettingReadyQuestionList.Count;turnNumber++)
            {
                GettingReadyQuestion i = GettingReadyQuestionList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result =  dbLesson.InsertGetReadyQuestion(ref err, DateProcess, turnNumber + 1, i.KeyWord, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        private bool InsertKeyWordExes()
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < KeyWordExList.Count; turnNumber++)
            {
                KeyWordEx i = KeyWordExList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertKeyWordExes(ref err, DateProcess, turnNumber + 1, i.KeyWord, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        private bool InsertKeyWord()
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < KeyWordsList.Count; turnNumber++)
            {
                KeyWord i = KeyWordsList[turnNumber];
      
                result = dbLesson.InsertKeyWords(ref err, DateProcess, turnNumber + 1,i.Word,i.HowToReadWord,i.VieWord,i.TypeOfWord,i.ExSentence,i.PictureOfWord);
            }
            return result;
        }
        private bool InsertSentences()
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < SentenceList.Count; turnNumber++)
            {
                Sentence i = SentenceList[turnNumber];

                result = dbLesson.InsertSentence(ref err, DateProcess, turnNumber + 1,i.KeySentence,i.HowToRead,i.VieMeanOfSentence);
            }
            return result;
        }
        private bool InsertSentenceExes()
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < SentenceExList.Count; turnNumber++)
            {
                SentenceEx i = SentenceExList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertSentenceExes(ref err, DateProcess, turnNumber + 1, i.KeyWord, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        private bool InsertListenAudioFile()
        {
            //bool result = false;
            return  dbLesson.InsertListenAudioFile(ref err, DateProcess, AudioListenFile);
        }
        private bool InsertListeningExes()
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < ListeningQuestionList.Count; turnNumber++)
            {
                ListeningQuestion i = ListeningQuestionList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertListeningExes(ref err, DateProcess, turnNumber + 1, i.QuestionContent, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        private bool InsertListeningPart2Exes()
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < ListeningPart2QuestionList.Count; turnNumber++)
            {
                ListeningPart2Question i = ListeningPart2QuestionList[turnNumber];
               
                result = dbLesson.InsertListeningExPart2(ref err, DateProcess, turnNumber + 1,i.Before,i.After,i.Value);
            }
            return result;
        }
        private bool InsertGrammar()
        {
            //bool result = false;
            return dbLesson.spInsertGrammar(ref err, DateProcess, WordGrammarFile);
        }
        private bool InsertGrammarExes()
        {
            bool result = false;
            for (int turnNumber = 0; turnNumber < GrammarQuestionList.Count; turnNumber++)
            {
                GrammarQuestion i = GrammarQuestionList[turnNumber];
                string ans = "";
                if (i.ChoseA)
                    ans = "A";
                if (i.ChoseB)
                    ans = "B";
                if (i.ChoseC)
                    ans = "C";
                if (i.ChoseD)
                    ans = "D";
                result = dbLesson.InsertGrammarExes(ref err, DateProcess, turnNumber + 1, i.KeyWord, i.AnsA, i.AnsB, i.AnsC, i.AnsD, ans);
            }
            return result;
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {          
            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            bool result = false; temp += 2; ValueProgress += 2;
            switch (temp)
            {
                case 2:
                    ProgressText = "Lesson...";
                    DateProcess = dbLesson.GetMaxLessonIndexByLevelId(this.Level);
                    break;                    
                case 4:
                    ProgressText = "Getting ready...";
                    result = InsertGetReadyQuestion();
                    break;
                case 8:
                    ProgressText = "Key Words...";
                    result = InsertKeyWord();
                    break;
                case 12:
                    ProgressText = "Key Words Exe...";
                    result = InsertKeyWordExes();
                    break;
                case 16:
                    ProgressText = "Sentences...";
                    result = InsertSentences();
                    break;
                case 20:
                    ProgressText = "Sentences exe...";
                    result = InsertSentenceExes();
                    break;
                case 24:
                    ProgressText = "Audio listening file...";
                    result = InsertListenAudioFile();
                    break;
                case 28:
                    ProgressText = " listening Exe Part 1...";
                    result = InsertListeningExes();
                    break;
                case 32:
                    ProgressText = " listening Exe Part 2...";
                    result = InsertListeningPart2Exes();
                    break;
                case 36:
                    ProgressText = " Grammar...";
                    result = InsertGrammar();
                    break;
                case 40:
                    ProgressText = " Grammar Exe...";
                    result = InsertGrammarExes();
                    break;
                case 44:
                    timer.Stop();
                    MessageBox.Show("Done?");
                    this.Close();
                    break;
                default:
                    break;

            }
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
