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

namespace SoBasicEnglish.ViewModels
{
    public class EditorViewModel : INotifyPropertyChanged
    {
        #region ICommand object
        public ICommand Click_ResetItem { get; set; }
        public ICommand Click_AddItem { get; set; }
        public ICommand Click_DeleteItem { get; set; }
        public ICommand GotFocus_HowToReadKeyWord { get; set; }
        #endregion
        #region objects
        private dbDateProcess dbDateProcess;private byte[] DefaultImage;int IndexOfKeyWord = 0;
        public ListCollectionView DateListOnGridView
        {
            get => _dateListOnGridView; set
            {
                _dateListOnGridView = value;
                NotifyPropertyChanged("DateListOnGridView");
            }
        }
        private ListCollectionView _dateListOnGridView;
        public ObservableCollection<DateProcess> DateList { get => _dateList; set { _dateList = value; NotifyPropertyChanged("DateList"); } }
        private ObservableCollection<DateProcess> _dateList = new ObservableCollection<DateProcess>();
        public ObservableCollection<GettingReadyQuestion> GettingReadyQuestionList { get => _gettingReadyQuestionList; set { _gettingReadyQuestionList = value; NotifyPropertyChanged("GettingReadyQuestionList"); } }
        private ObservableCollection<GettingReadyQuestion> _gettingReadyQuestionList = new ObservableCollection<GettingReadyQuestion>();
        public ObservableCollection<KeyWord> KeyWordsList { get => _keyWordsList; set { _keyWordsList = value; NotifyPropertyChanged("KeyWordsList"); } }
        private ObservableCollection<KeyWord> _keyWordsList = new ObservableCollection<KeyWord>();
        public ObservableCollection<Button> PrnonList { get => _prnonList; set { _prnonList = value; NotifyPropertyChanged("PrnonList"); } }
        private ObservableCollection<Button> _prnonList = new ObservableCollection<Button>();
        public bool Isopen_Symbols { get => _isopen_Symbols; set { _isopen_Symbols = value; NotifyPropertyChanged("Isopen_Symbols"); } }
        private bool _isopen_Symbols = false;
        public int SelectedIndexOfKeyWord { get => _selectedIndexOfKeyWord; set { _selectedIndexOfKeyWord = value; NotifyPropertyChanged("SelectedIndexOfKeyWord"); } }
        private int _selectedIndexOfKeyWord = 0;
        public int TcLessonSelectedIndex { get => _tcLessonSelectedIndex; set { _tcLessonSelectedIndex = value; NotifyPropertyChanged("TcLessonSelectedIndex"); } }
        public int TcKeyWordSelectedInDex { get => _tcKeyWordSelectedIndex; set { _tcKeyWordSelectedIndex = value; NotifyPropertyChanged("TcKeyWordSelectedInDex"); } }


        private int _tcLessonSelectedIndex = 0;
        private int _tcKeyWordSelectedIndex = 0;

       

        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion
        #region Constructor
        public EditorViewModel()
        {
            dbDateProcess = new dbDateProcess(Model.serverName); GetDateProcess(); GetDefaultData(); GetSymbols();
            Click_ResetItem = new DelegateCommand(ResetItems);
            Click_AddItem = new DelegateCommand(AddItem);
            Click_DeleteItem = new RelayCommand<object>((p) => p != null, DeleteItem);
            GotFocus_HowToReadKeyWord = new RelayCommand<object>((p)=>true,GotFocusHowToReadKeyWord);
        }

        private void GotFocusHowToReadKeyWord(object pram)
        {
            
                KeyWord temp = pram as KeyWord;
            if (temp != null)
                SelectedIndexOfKeyWord = temp.Index;
           
            Isopen_Symbols = true;
          
            //foreach (var i in obj)
            //{
            //    TextBlock temp = i as TextBlock;

            //    if (temp != null)
            //    {
            //        SelectedIndexOfKeyWord = Convert.ToInt32(Math.Floor(temp.MinWidth));
            //        break;
            //    }             
            //}           

        }

        private void DeleteItem(object obj)
        {
            if (TcLessonSelectedIndex == 0)
            {
                GettingReadyQuestion temp = obj as GettingReadyQuestion;
                GettingReadyQuestionList.Remove(temp);
            }
            else
            {
                if (TcLessonSelectedIndex == 1)
                {
                    if (TcKeyWordSelectedInDex == 0)
                    {
                        for (int i = KeyWordsList.Count - 1; i > SelectedIndexOfKeyWord; i--)
                        {
                            KeyWordsList[i].Index = KeyWordsList[i - 1].Index;
                        }
                        KeyWordsList.RemoveAt(SelectedIndexOfKeyWord);                      
                    }
                }
            }
        }

        #endregion
        #region ICommands
        private void ResetItems()
        {
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
                }
            }
           
        }
        private void AddItem()
        {
            if(TcLessonSelectedIndex==0)
                GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
            else
            {
                if (TcLessonSelectedIndex == 1)
                {
                    if(TcKeyWordSelectedInDex==0)
                        KeyWordsList.Add(new KeyWord { Index = KeyWordsList.Count, Word = "word", ExSentence = "aa bb", HowToReadWord = "ádbasijci", VieWord = "sadđ", PictureOfWord = DefaultImage, TypeOfWord = "noun" });
                }
            }

        }
        #endregion
        #region functons
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void GetDateProcess()
        {
            DataTable temp = new DataTable();
            temp = dbDateProcess.DatesprocessList();
            DateList.Clear();
            foreach (DataRow i in temp.Rows)
            {
                DateList.Add(new DateProcess { TurnNumber = Convert.ToInt32(i["turnNumber"].ToString()), DetailInfo = i["detailInfo"].ToString(), LevelDetail = i["levelDetail"].ToString() });
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
            Button temp = (Button)sender;
            KeyWordsList[SelectedIndexOfKeyWord].HowToReadWord += temp.Content;
        }

        private void GetDefaultData()
        {
            //Thread t1 = new Thread((p) => {
               

            //});
            //t1.Start();
            MemoryStream ms = new MemoryStream();
            Properties.Resources.customer.Save(ms, Properties.Resources.customer.RawFormat);
            DefaultImage = ms.GetBuffer();
            ms.Close();
            GettingReadyQuestionList.Clear(); KeyWordsList.Clear();
            for (int i = 0; i < 10; i++)
            {
                GettingReadyQuestionList.Add(new GettingReadyQuestion { ChoseA = true, ChoseB = false, ChoseC = false, ChoseD = false, RightAns = 1, KeyWord = "aa", AnsA = "a", AnsB = "b", AnsC = "c", AnsD = "d" });
                KeyWordsList.Add(new KeyWord { Index = i, Word = "word", ExSentence = "aa bb", HowToReadWord = "ádbasijci", VieWord = "sadđ", PictureOfWord = DefaultImage, TypeOfWord = "noun" });
            }

        }
        #endregion  


    }
}
