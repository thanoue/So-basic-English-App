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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;
using System.IO;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using System.IO.Packaging;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.Threading;
using MahApps.Metro.Controls.Dialogs;
namespace SoBasicEnglish
{
    /// <summary>
    /// Interaction logic for EditorScreen.xaml
    /// </summary>
    public partial class EditorScreen : INotifyPropertyChanged
    {
        dbDateProcess dbDateProcess;
        private ObservableCollection<DateProcess> dateProcesses = new ObservableCollection<DateProcess>();
        public event PropertyChangedEventHandler PropertyChanged; private string lessonName = "";
        private ObservableCollection<Level> levels = new ObservableCollection<Level>();
        private ObservableCollection<KeyWordEx> KeyWordExList = new ObservableCollection<KeyWordEx>();
        private ObservableCollection<Button> PrnonList = new ObservableCollection<Button>();
        private ObservableCollection<KeyWord> KeyWordsList = new ObservableCollection<KeyWord>();
        private ObservableCollection<Sentence> SentenceList = new ObservableCollection<Sentence>();
        private ObservableCollection<SentenceEx> SentenceExList = new ObservableCollection<SentenceEx>();
        private bool mediaPlayerIsPlaying = false; DispatcherTimer timer; private  bool userIsDraggingSlider= false;
        private ObservableCollection<GrammarQuestion> GrammarQuestionList = new ObservableCollection<GrammarQuestion>();
        private ObservableCollection<ListeningPart2Question> ListeningPart2QuestionList = new ObservableCollection<ListeningPart2Question>();
        private ObservableCollection<ListeningQuestion> ListeningQuestionList = new ObservableCollection<ListeningQuestion>();
        private ObservableCollection<GettingReadyQuestion> GettingReadyQuestionList = new ObservableCollection<GettingReadyQuestion>();
        byte[] WordGrammarFile; byte[] AudioListenFile;

        private  bool isloadComplete =false;
        public bool IsloadComplete
        {
            get { return isloadComplete; }
            set
            {
                isloadComplete = value;
                OnpropertyChanged("IsloadComplete");
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
        public ObservableCollection<Level> Levels
        {
            get
            {
                return levels;
            }
            set
            {
                levels = value;
                OnpropertyChanged("Levels");
            }
        }
        public ObservableCollection<DateProcess> DateProcesses
        {
            get { return dateProcesses; }
            set
            {
                dateProcesses = value;
                OnpropertyChanged("DateProcesses");
            }
        }
        protected virtual void OnpropertyChanged(string lessonName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(lessonName));
            }
        }
        public class DateProcess
        {
            public int turnNumber { get; set; }
            public string detailInfo { get; set; }
            public string processLevel { get; set; }
            public int processLevelIndex { get; set; }
        }
        public class Level
        {
            public string levelValue { get; set; }
            public int levelIndex { get; set; }
        }      
        public EditorScreen() /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            InitializeComponent(); this.DataContext = this;
            dbDateProcess = new dbDateProcess(Model.serverName);
            LoadDateData();dbLesson = new dbLesson(Model.serverName);
            var collectionVwSrcListening = new ListCollectionView(DateProcesses);
            collectionVwSrcListening.GroupDescriptions.Add(new PropertyGroupDescription("processLevel"));
            lvDateList.ItemsSource = collectionVwSrcListening;
           
            lvKeyWordExList.ItemsSource = KeyWordExList;
            lvKeyWordsList.ItemsSource = KeyWordsList;
            lvSentencesList.ItemsSource = SentenceList;
            lvSentenceExList.ItemsSource = SentenceExList;
            lvListeningQuestionList.ItemsSource = ListeningQuestionList;
            lvListeningPart2List.ItemsSource = ListeningPart2QuestionList;
            lvGetReadyQuestionList.ItemsSource = GettingReadyQuestionList;
            lvGrammarQuestionList.ItemsSource = GrammarQuestionList;
            string[] pr = { "i:", "I", "æ", "ɑ:", "ɔ", "ɔ:", "ʊ", "u:", "ʌ", "ɜ:", "ə", "ei", "əʊ", "ai", "aʊ", "ɔi", "iə", "eə", "ʊə", "tʃ", "dʒ", "ɵ", "ð", "ʃ", "ʒ", "ŋ", };
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
            tcLesson.IsEnabled = false;stDateChoose.IsEnabled = true;
            WrapPron.ItemsSource = PrnonList;
             timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;          
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mediaPlayer.Position.TotalSeconds;
            }

        }

        private void Temp_Click(object sender, RoutedEventArgs e)
        {
            var temp = (Button)sender;
            if (tcLesson.SelectedIndex == 1 && tcKeyWords.SelectedIndex == 0)
                KeyWordsList[pronTbSelectedIndex].HowToReadWord += temp.Content.ToString();
            if (tcLesson.SelectedIndex == 2 && tcSentence.SelectedIndex == 0)
                SentenceList[pronTbSelectedIndex].HowToRead += temp.Content.ToString();
            //      MessageBox.Show(temp.Content.ToString());
        }
        void LoadDateData()
        {
            System.Data.DataTable temp = dbDateProcess.DatesprocessList();
            DateProcesses.Clear();LessonName = "";cbLevel.SelectedIndex = 0;Levels.Clear();
            foreach (DataRow i in temp.Rows)
            {
                DateProcesses.Add(new DateProcess { turnNumber = Int32.Parse(i["turnNumber"].ToString()), detailInfo = i["detailInfo"].ToString(), processLevel = i["levelDetail"].ToString(), processLevelIndex = Int32.Parse(i["processLevel"].ToString()) });
            }
            System.Data.DataTable temp2 = dbDateProcess.GetLevelList();
            foreach(DataRow i in temp2.Rows)
            {
                Levels.Add(new Level { levelIndex= Int32.Parse(i["Id"].ToString()) , levelValue = i["levelDetail"].ToString() });
            }
            SentenceList.Clear(); KeyWordsList.Clear(); KeyWordExList.Clear(); SentenceExList.Clear(); ListeningPart2QuestionList.Clear(); ListeningQuestionList.Clear();
            GettingReadyQuestionList.Clear(); GrammarQuestionList.Clear();
            for (int i = 0; i < 5; i++)
            {
                SentenceList.Add(new Sentence { Index = i, KeySentence = "What'up man??", HowToRead = "ádasdaasda", VieMeanOfSentence = " Gì đó mày ?" });
                KeyWordsList.Add(new KeyWord { Index = i, Word = "word", ExSentence = "aa bb", HowToReadWord = "ádbasijci", VieWord = "sadđ", PictureOfWord = Model.userAVT, TypeOfWord = "noun" });
                KeyWordExList.Add(new KeyWordEx { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                SentenceExList.Add(new SentenceEx { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                ListeningQuestionList.Add(new ListeningQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, QuestionContent = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                ListeningPart2QuestionList.Add(new ListeningPart2Question { Before = "aaaaaaaaaaaa", Value = "bbbbbbbbb", After = "cccccccccc" });
                GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                GrammarQuestionList.Add(new GrammarQuestion { ChoseB = true, ChoseA = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aaaa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
            }
        }

        private void lvDateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LessonName = DateProcesses[lvDateList.SelectedIndex].detailInfo;
                cbLevel.SelectedIndex = DateProcesses[lvDateList.SelectedIndex].processLevelIndex - 1;
                tcLesson.IsEnabled = true; spLessonName.IsEnabled = true;
                GettingReadyQuestionList.Clear();tcLesson.SelectedIndex = 0;
                Model.GetGettingReadyList(GettingReadyQuestionList,DateProcesses[lvDateList.SelectedIndex].turnNumber,Model.serverName);
                btnDeleteLesson.IsEnabled = true;btnCancelNewLesson.IsEnabled = false;
            }
            catch (Exception )
            {
               
            }
           
        }

        private void btnAddLesson_Click(object sender, RoutedEventArgs e)
        {
            btnCancelNewLesson.IsEnabled = true;
            spLessonName.IsEnabled = true;
            btnSubmitEditing.IsEnabled = false;btnDeleteLesson.IsEnabled = false;
            // spGetReadyQues.IsEnabled = true;
            tcLesson.IsEnabled = true;stDateChoose.IsEnabled = false;
            LoadDateData();
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var temp = (Button)sender;
            int index = Int32.Parse(temp.MinWidth.ToString());
            var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            byte[] buffer = File.ReadAllBytes(dialog.FileName);
            KeyWordsList[index].PictureOfWord = buffer;


        }   
        private void btnAddkeyWords_Click(object sender, RoutedEventArgs e)
        {
            KeyWordsList.Add(new KeyWord { Index = KeyWordsList.Count });
        }

        private void btnDeletekeyWords_Click(object sender, RoutedEventArgs e)
        {
            for (int i = KeyWordsList.Count - 1; i > lvKeyWordsList.SelectedIndex; i--)
            {
                KeyWordsList[i].Index = KeyWordsList[i - 1].Index;
            }
            KeyWordsList.RemoveAt(lvKeyWordsList.SelectedIndex);
        }

        private void lvKeyWordsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnResetkeyWords_Click(object sender, RoutedEventArgs e)
        {
            KeyWordsList.Clear();
            for (int i = 0; i < 5; i++)
            {
                KeyWordsList.Add(new KeyWord { Index = i });

            }
        }

        private void btnResetkeyWordEx_Click(object sender, RoutedEventArgs e)
        {
            KeyWordExList.Clear();
            for (int i = 0; i < 5; i++)
            {
                KeyWordExList.Add(new KeyWordEx { RightAns = 0 });
            }
        }

        private void btnAddkeyWordEx_Click(object sender, RoutedEventArgs e)
        {
            KeyWordExList.Add(new KeyWordEx { RightAns = 0 });

        }

        private void btnDeletekeyWordEx_Click(object sender, RoutedEventArgs e)
        {
            KeyWordExList.RemoveAt(lvKeyWordExList.SelectedIndex);
        }
        int pronTbSelectedIndex = 0;
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PronFlyout.IsOpen = true;
            var texb = (TextBox)sender;
            pronTbSelectedIndex = Convert.ToInt32(Math.Floor(texb.MinWidth));

        }

        private void btnResetSentences_Click(object sender, RoutedEventArgs e)
        {
            SentenceList.Clear();
            for (int i = 0; i < 5; i++)
            {
                SentenceList.Add(new Sentence { });
            }
        }

        private void btnAddSentences_Click(object sender, RoutedEventArgs e)
        {
            SentenceList.Add(new Sentence());
        }

        private void btnDeletekeySentences_Click(object sender, RoutedEventArgs e)
        {
            SentenceList.RemoveAt(lvSentencesList.SelectedIndex);
        }

        private void btnResetSentenceEx_Click(object sender, RoutedEventArgs e)
        {
            SentenceExList.Clear();
            for (int i = 0; i < 5; i++)
            {
                SentenceExList.Add(new SentenceEx { RightAns = 0 });
            }
        }

        private void btnAddSentenceEx_Click(object sender, RoutedEventArgs e)
        {
            SentenceExList.Add(new SentenceEx { RightAns = 0 });
        }

        private void btnDeleteSentenceEx_Click(object sender, RoutedEventArgs e)
        {
            SentenceExList.RemoveAt(lvSentenceExList.SelectedIndex);
        }
        MediaPlayer mediaPlayer = new MediaPlayer();
     

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
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
                mediaPlayer.Open(new Uri(path));
            }
            catch (Exception) { }
            
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            timer.Start();
            mediaPlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayerIsPlaying = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(0);
            timer.Stop(); 
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void btnResetListeningQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (tcListening.SelectedIndex == 0)
            {
                ListeningQuestionList.Clear();
                for (int i = 0; i < 5; i++)
                {
                    ListeningQuestionList.Add(new ListeningQuestion { ChoseA = true });
                }
               
            }
            else
            {
                ListeningPart2QuestionList.Clear();
                for (int i = 0; i < 5; i++)
                {
                    ListeningPart2QuestionList.Add(new ListeningPart2Question ());
                }

            }
        }

        private void btnAddListeningQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (tcListening.SelectedIndex == 0)
            {
                   ListeningQuestionList.Add(new ListeningQuestion { ChoseA = true }); 
            }
            else
            {            
                    ListeningPart2QuestionList.Add(new ListeningPart2Question());
               
            }
        }

        private void btnDeleteListeningQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (tcListening.SelectedIndex == 0)
            {
                ListeningQuestionList.RemoveAt(lvListeningQuestionList.SelectedIndex);
            }
            else
            {
                ListeningPart2QuestionList.RemoveAt(lvListeningPart2List.SelectedIndex);

            }
        }

        private void btnResetGettingReadyQuestion_Click(object sender, RoutedEventArgs e)
        {
            GettingReadyQuestionList.Clear();
            for (int i = 0; i < 5; i++)
            {
                GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true });
            }
        }

        private void btnAddGettingReadyQuestion_Click(object sender, RoutedEventArgs e)
        {
            GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true });
        }

        private void btnDeleteGettingReadyQuestion_Click(object sender, RoutedEventArgs e)
        {
            GettingReadyQuestionList.RemoveAt(lvGetReadyQuestionList.SelectedIndex);
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
        private void btnOpenWordFile_Click(object sender, RoutedEventArgs e)
        {
            // Initialize an OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter and RestoreDirectory
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Word documents(*.doc;*.docx)|*.doc;*.docx";

            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                if (openFileDialog.FileName.Length > 0)
                {
                    tbWordFilePath.Text = openFileDialog.FileName;
                    
                }
            }
          
        }
        private string convertedXpsDoc;dbLesson dbLesson;
        void Splash()
        {
            string er = "";
            try
            {
                if (dbLesson.InsertLesson(ref er, cbLevel.SelectedIndex+1, LessonName))
                {

                    new SplashScreen( cbLevel.SelectedIndex + 1, KeyWordsList, KeyWordExList, SentenceList,
                    SentenceExList, GrammarQuestionList, ListeningPart2QuestionList,
                     ListeningQuestionList, GettingReadyQuestionList, AudioListenFile, WordGrammarFile).ShowDialog();
                }
                   
               
            }
            catch (Exception)
            {
            
            }         
        }
        private void btnReviewWordFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                convertedXpsDoc = string.Concat(System.IO.Path.GetTempPath(), "\\", Guid.NewGuid().ToString(), ".xps");
            XpsDocument xpsDocument = ConvertWordToXps(tbWordFilePath.Text, convertedXpsDoc);
                if (xpsDocument == null)
                {
                    return;
                }
                dvGrammar.Document = xpsDocument.GetFixedDocumentSequence();
                WordGrammarFile = System.IO.File.ReadAllBytes(convertedXpsDoc);
            }
            catch (Exception)
            {

            }
           


        }

        private void btnSubmitNewLesson_Click(object sender, RoutedEventArgs e)
        {

            Splash();
            LoadDateData(); stDateChoose.IsEnabled = true;btnAddLesson.IsEnabled = true;btnDeleteLesson.IsEnabled = false;
            btnCancelNewLesson.IsEnabled = false;
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            PronFlyout.IsOpen = true;
            var texb = (TextBox)sender;
            pronTbSelectedIndex = Convert.ToInt32(Math.Floor(texb.MinWidth));
        }

        private void btnCancelNewLesson_Click(object sender, RoutedEventArgs e)
        {
            LoadDateData();
        }

        private void btnDeleteLesson_Click(object sender, RoutedEventArgs e)
        {
            string er = "";
            if(dbLesson.DeleteLessonByTurnNumber(ref er, DateProcesses[lvDateList.SelectedIndex].turnNumber))
            {
                btnDeleteLesson.IsEnabled = false;
                LoadDateData();
            }
            lvDateList.SelectedIndex = -1;
        }
    }
}
