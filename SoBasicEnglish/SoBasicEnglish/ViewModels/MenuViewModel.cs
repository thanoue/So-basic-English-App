using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data.SqlClient;
using BusinessLogicFramework;
using System.Windows;
using System.ComponentModel;
using SoBasicEnglish.Views;
using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Data;
using System.Windows.Data;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Windows.Threading;

namespace SoBasicEnglish.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        #region  Icommand Object
        public ICommand Click_AboutApp { get; set; }
        public ICommand ClickUserTile { get; set; }
         public ICommand ClickStudyTile { get; set; }
       public  ICommand ClickChampionTile { get; set; }
        public ICommand ClickEditUserInfo { get; set; }
        public ICommand ClickChangePassWord { get; set; }
        public ICommand ChangeUserProfile { get; set; }
        public ICommand CancelChangUserProfile { get; set; }
        public ICommand ChangePassword { get; set; }
        public ICommand CancelChangePass { get; set; }
        public ICommand ChooeDateProcess { get; set; }
        public ICommand SelectIimage { get; set; }
        public ICommand FlipSelectionChanged { get; set; }
        public ICommand ClickAboutAuthor { get; set; }
        public ICommand ClickSetting { get; set; }
        public ICommand ClickEditing { get; set; }
        public ICommand  Click_ToTheExam { get; set; }
        #endregion
        #region Object
        private int _gvDateProcessSelectedIndex = -1;
        public bool OpenAboutApp { get => _openAboutApp; set { _openAboutApp = value; NotifyPropertyChanged("OpenAboutApp"); } }
        private bool _openAboutApp = false;
        public event PropertyChangedEventHandler PropertyChanged;       
        dbUserScore dbUserScore;dbDateProcess dbDateProcess;dbLogin dbLogin; 

        private bool _openAboutAuthor = false;
        public bool OpenAboutAuthor { get => _openAboutAuthor; set { _openAboutAuthor = value; NotifyPropertyChanged("OpenAboutAuthor"); } }
        private byte[] _userAvt;
        public byte[] UserAvt { get => _userAvt; set { _userAvt = value;
                NotifyPropertyChanged("UserAvt");
            } }
        private int _flipSelectedIndex = 0;
        public int FlipSelectedIndex { get => _flipSelectedIndex; set { _flipSelectedIndex = value; NotifyPropertyChanged("FlipSelectedIndex"); } }
        public string TitleOfFlip { get => _titleOfFlip; set { _titleOfFlip = value; NotifyPropertyChanged("TitleOfFlip"); } }
        private string _titleOfFlip = "The Basic English app you can see!";
        public string UserLevel { get => _userLevel; set { _userLevel = value;
                NotifyPropertyChanged("UserLevel");
            }
            }
        private string _userLevel = "";
        private string _userScore = "";
        public string UserScore { get => _userScore; set { _userScore = value;
                NotifyPropertyChanged("UserScorre");
            } }
        public double WidthOfProgress { get => _widthOfProgress; set { _widthOfProgress = value;
                NotifyPropertyChanged("WidthOfProgress");
            } }
        public string UserFullName { get => _userFullName; set { _userFullName = value;
                NotifyPropertyChanged("UserFullName");
            } }
        private bool _openUserFlyout = false;
        public bool OpenUserFlyout { get => _openUserFlyout; set { _openUserFlyout = value;  NotifyPropertyChanged("OpenUserFlyout"); } }      
        private double _widthOfProgress = 0;
        private string _userFullName = "";
        private string _basicInfo = "";
        public string BasicInfo { get => _basicInfo; set { _basicInfo = value; NotifyPropertyChanged("BasicInfo"); } }
        private bool _openChampionFlyout = false;
        public bool OpenChampionFlyout { get => _openChampionFlyout; set { _openChampionFlyout = value; NotifyPropertyChanged("OpenChampionFlyout"); } }
        public bool OpenDateFlyout { get => _openDateFlyout; set { _openDateFlyout = value; NotifyPropertyChanged("OpenDateFlyout"); } }      
        private bool _openDateFlyout = false;
        private bool _openEditUserInFo = false;
        public bool OpenEditUserInFo { get => _openEditUserInFo; set { _openEditUserInFo = value; NotifyPropertyChanged("OpenEditUserInFo"); } }
        public bool OpenChangePassWord { get => _openChangePassWord; set { _openChangePassWord = value;NotifyPropertyChanged("OpenChangePassWord"); } }
        private bool _openChangePassWord = false;
        private bool _isOpenChangePassError = false;
        public bool IsOpenChangePassError { get => _isOpenChangePassError; set { _isOpenChangePassError = value; NotifyPropertyChanged("IsOpenChangePassError"); } }
        private string _changePassErrorMessage ="";
        public string ChangePassErrorMessage { get => _changePassErrorMessage; set { _changePassErrorMessage = value; NotifyPropertyChanged("ChangePassErrorMessage"); } }
        public ObservableCollection<User> UserChampionList { get => _userChampionList; set { _userChampionList = value;
                NotifyPropertyChanged("UserChampionList");
            } }
        private ObservableCollection<User> _userChampionList = new ObservableCollection<User>();
     
        public ListCollectionView DateListOnGridView { get => _dateListOnGridView; set { _dateListOnGridView = value;
                NotifyPropertyChanged("DateListOnGridView");
            } }
        private ObservableCollection<DateProcess> _dateList = new ObservableCollection<DateProcess>();
        public ObservableCollection<DateProcess> DateList { get => _dateList; set { _dateList = value; NotifyPropertyChanged("DateList"); } }

        public int GvDateProcessSelectedIndex { get => _gvDateProcessSelectedIndex; set { _gvDateProcessSelectedIndex = value; NotifyPropertyChanged("GvDateProcessSelectedIndex"); } }

        //public List<DateProcess> dateList = new List<DateProcess>();
        private ListCollectionView _dateListOnGridView;
        DispatcherTimer timerToCloseNotify;
        DispatcherTimer TimerToCloseError;
        #endregion
        #region Constructor
        public MenuViewModel()
        {
            UserAvt = Model.userAVT; dbUserScore = new dbUserScore(Model.serverName); ; dbDateProcess = new dbDateProcess(Model.serverName); dbLogin = new dbLogin(Model.serverName);
            UserFullName = Model.userFullname; BasicInfo = dbLogin.GetBasicInfoByUserLoginName(Model.userLoginName);
            GetScore(); GetChampionList(); GetDateProcess();
            ClickUserTile = new DelegateCommand(Click_UserTile);
            ClickStudyTile = new DelegateCommand(Click_StudyTile);
            ClickChampionTile = new DelegateCommand(Click_ChampionTile);
            ClickEditUserInfo = new DelegateCommand(Click_EditUserInfo);
            ClickChangePassWord = new DelegateCommand(Click_ChangePassWord);
            ChangeUserProfile = new DelegateCommand(Submit_ChanguserProfile);
            CancelChangUserProfile = new DelegateCommand(Cancel_ChangeUserProfile);
            ChangePassword = new RelayCommand<UIElementCollection>(CheckIfCanChangePass, Submit_ChangePassword);
            CancelChangePass = new DelegateCommand(Cancel_ChangePass);
            ChooeDateProcess = new RelayCommand<object>(ChooseDateToStudy);
            ClickAboutAuthor = new DelegateCommand(Click_AboutAuthor);
            ClickSetting = new DelegateCommand(GotoSettingWindow, CheckIfCanGoToSetting);
            ClickEditing = new DelegateCommand(GoToEditWinDow, CheckIfCanGoToEdit);
            SelectIimage = new DelegateCommand(Browse);
            FlipSelectionChanged = new DelegateCommand(Flip_SelectionChanged);
            Click_AboutApp = new DelegateCommand(OpenAboutAppMiniWindow);
            Click_ToTheExam = new DelegateCommand(ToTheExam);
            timerToCloseNotify = new DispatcherTimer();
            timerToCloseNotify.Tick += TimerToCloseNotify_Tick;
            timerToCloseNotify.Interval = new TimeSpan(0, 0, 4);
            timerToCloseNotify.Start();
            TimerToCloseError = new DispatcherTimer();
            TimerToCloseError.Tick += TimerToCloseError_Tick;
            TimerToCloseError.Interval = new TimeSpan(0, 0, 2);
        }

        private void ToTheExam()
        {
            ExamWindow a = new ExamWindow();a.ShowDialog();
        }
        #endregion
        #region Commands
        private void OpenAboutAppMiniWindow()
        {
            OpenAboutApp = true;
        }
        private bool CheckIfCanGoToEdit()
        {
            if (Model.role > 1)
                return true;
            return false;
        }
        private void GoToEditWinDow()
        {
            try {
                EditorWindow temp = new EditorWindow();
                temp.ShowDialog();
            }
            catch (Exception) { }
           
        }
        private void TimerToCloseError_Tick(object sender, EventArgs e)
        {
            IsOpenChangePassError = false;
            TimerToCloseError.Stop();
        }
        private bool CheckIfCanGoToSetting()
        {
            if (Model.role > 2)
                return true;
            return false;
        }
        private void GotoSettingWindow()
        {
            UserRoleWindow tem = new UserRoleWindow();
            tem.ShowDialog();
        }

        private void Click_AboutAuthor()
        {
            OpenAboutAuthor = true;
        }
        private void Flip_SelectionChanged()
        {
            switch (FlipSelectedIndex)
            {
                case 0:
                    TitleOfFlip = "The Basic English app you can see!";
                    break;
                case 1:
                    TitleOfFlip = "You can study and do many Exams";
                    break;
                case 2:
                    TitleOfFlip = "Your Target is our mission";
                    break;
                case 3:
                    TitleOfFlip = "this is the best choice you can have !!!";
                    break;
                default :
                    break;
            }
        }
        private void TimerToCloseNotify_Tick(object sender, EventArgs e)
        {
            if (FlipSelectedIndex >= 0 && FlipSelectedIndex < 3)
                FlipSelectedIndex += 1;
            else
                FlipSelectedIndex = 0;
        }
        private void  ChooseDateToStudy(object obj)
        {
            if (GvDateProcessSelectedIndex != -1)
            {
                DateProcess temp = obj as DateProcess;
                //MessageBox.Show(temp.TurnNumber.ToString());
                try
                {
                    Model.dateProcess = temp.TurnNumber;
                    StudyWindow a = new StudyWindow(); a.ShowDialog();
                    GvDateProcessSelectedIndex = -1;

                }
                catch (Exception ) {
                   
                }
               
            }          
        }
        private void Cancel_ChangePass()
        {
            OpenChangePassWord = false;
        }
        private void Submit_ChangePassword(UIElementCollection obj)
        {
            string oldPass = "";string newPass = "";string confPass = "";
            foreach(var i in obj)
            {
                PasswordBox temp = i as PasswordBox;
                if (temp != null)
                {
                    switch (temp.Name)
                    {
                        case "txtOldPass":
                            oldPass = temp.Password;
                            break;
                        case "txtNewPass":
                            newPass = temp.Password;
                            break;
                        case "txtConfiPass":
                            confPass = temp.Password;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (oldPass == Model.userPassword)
            {
                if (newPass == confPass)
                {
                    string er = "";
                    if(dbLogin.UpdateUserPassword(ref er, Model.userLoginName, newPass))
                    {
                        OpenChangePassWord = false;
                    }
                }
                else
                {
                    ChangePassErrorMessage = "Retype your new password!!";
                    IsOpenChangePassError = true;
                    TimerToCloseError.Start();
                }
            }
            else
            {
                ChangePassErrorMessage = "Your old pasword isn't match";
                IsOpenChangePassError = true;
                TimerToCloseError.Start();
            }
          
        }

        private bool CheckIfCanChangePass(UIElementCollection obj)
        {
            string oldPass = ""; string newPass = ""; string confPass = "";
            foreach (var i in obj)
            {
                PasswordBox temp = i as PasswordBox;
                if (temp != null)
                {
                    switch (temp.Name)
                    {
                        case "txtOldPass":
                            oldPass = temp.Password;
                            break;
                        case "txtNewPass":
                            newPass = temp.Password;
                            break;
                        case "txtConfiPass":
                            confPass = temp.Password;
                            break;
                        default:
                            break;
                    }
                }
            }
            return !String.IsNullOrWhiteSpace(oldPass) && !String.IsNullOrWhiteSpace(newPass) && !String.IsNullOrWhiteSpace(confPass);
        }

        private void Cancel_ChangeUserProfile()
        {
           
            UserAvt = Model.userAVT; OpenEditUserInFo = false; BasicInfo = dbLogin.GetBasicInfoByUserLoginName(Model.userLoginName);
            UserFullName = Model.userFullname;
        }

        private void Submit_ChanguserProfile()
        {
            string er = "";
            if (dbLogin.UpdateUserProfile(ref er, Model.userLoginName, UserFullName, BasicInfo, UserAvt)){
                Model.userAVT = UserAvt;Model.userFullname = UserFullName;
                OpenEditUserInFo = false;
            }
        }

        private void Click_ChangePassWord()
        {
            if (OpenChangePassWord)
                OpenChangePassWord = false;
            else
                OpenChangePassWord = true;
        }

        private void Click_EditUserInfo()
        {
            if (OpenEditUserInFo)
                OpenEditUserInFo = false;
            else
                OpenEditUserInFo = true;
        }

        private void Click_ChampionTile()
        {
            if (OpenChampionFlyout)
                OpenChampionFlyout = false;
            else
                OpenChampionFlyout = true;
        }
        private void Click_UserTile()
        {
            if (OpenUserFlyout)
                OpenUserFlyout = false;
            else
                OpenUserFlyout = true;
        }
        private void Click_StudyTile()
        {
            if (OpenDateFlyout)
                OpenDateFlyout = false;
            else
                OpenDateFlyout = true;
        }
        #endregion
        #region Function
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void GetChampionList()
        {
            UserChampionList.Clear();
            DataTable temp = new DataTable();
            temp = dbUserScore.GetChampionShip();
            foreach (DataRow i in temp.Rows)
            {
                UserChampionList.Add(new User { UserLoginName = i["loginName"].ToString(), UserScore = Int32.Parse(i["levelScore"].ToString()), UserAvatar = (byte[])i["userAvatar"], UserName = i["userName"].ToString(), UserLevel = Int32.Parse(i["userLevel"].ToString()) });
            }

        }
        public void GetDateProcess()
        {
            DataTable temp = new DataTable();
            temp = dbDateProcess.DatesprocessList();
            int process = dbLogin.GetProcessByUserLoginName(Model.userLoginName);
            DateList.Clear();
            foreach (DataRow i in temp.Rows)
            {
                if(process >= Convert.ToInt32(i["turnNumber"].ToString()))
                    DateList.Add(new DateProcess { TurnNumber = Convert.ToInt32(i["turnNumber"].ToString()), DetailInfo = i["detailInfo"].ToString(), LevelDetail = i["levelDetail"].ToString() });
            }
            _dateListOnGridView = new ListCollectionView(DateList);
            _dateListOnGridView.GroupDescriptions.Add(new PropertyGroupDescription("LevelDetail"));
        }
        private void GetScore()
        {
            int level = dbUserScore.GetLevelByUserLoginName(Model.userLoginName); int userScore = dbUserScore.GetLevelScoreByUserLoginName(Model.userLoginName);
            UserLevel = level.ToString(); UserScore = userScore.ToString();
            int currentScoreToGain = dbUserScore.GetScoreToGainByLevel(level);
            int nextScoreToGain = dbUserScore.GetScoreToGainByLevel(level + 1);
            double percent = Math.Round(((double)nextScoreToGain - (double)currentScoreToGain) / 100.0, 1);
            var leverUpScore = (double)userScore - (double)currentScoreToGain;
            WidthOfProgress = leverUpScore / percent;
        }
        void Browse()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
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
                UserAvt = memStream.ToArray(); memStream.Close();
            };

        }
        #endregion
    }
}
