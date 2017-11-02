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

namespace SoBasicEnglish.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        dbUserScore dbUserScore;dbDateProcess dbDateProcess;dbLogin dbLogin;
        private byte[] _userAvt;
        public byte[] UserAvt { get => _userAvt; set { _userAvt = value;
                NotifyPropertyChanged("UserAvt");
            } }
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
        public ObservableCollection<DateProcess> DateList { get => _dateList; set { _dateList = value; NotifyPropertyChanged("DateList"); } }
        public ListCollectionView DateListOnGridView { get => _dateListOnGridView; set { _dateListOnGridView = value;
                NotifyPropertyChanged("DateListOnGridView");
            } }

      

        private ObservableCollection<DateProcess> _dateList = new ObservableCollection<DateProcess>();
        //public List<DateProcess> dateList = new List<DateProcess>();
        private ListCollectionView _dateListOnGridView;        
        public MenuViewModel()
        {
            UserAvt = Model.userAVT;dbUserScore = new dbUserScore(Model.serverName); ;dbDateProcess = new dbDateProcess(Model.serverName);dbLogin = new dbLogin(Model.serverName);
            UserFullName = Model.userFullname;BasicInfo = dbLogin.GetBasicInfoByUserLoginName(Model.userLoginName);
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
        }

        private void  ChooseDateToStudy(object obj)
        {
            DateProcess temp = obj as DateProcess;
            MessageBox.Show(temp.TurnNumber.ToString());
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
                }
            }
            else
            {
                ChangePassErrorMessage = "Your old pasword isn't match";
                IsOpenChangePassError = true;
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

        private void Click_StudyTile()
        {
            if (OpenDateFlyout)
                OpenDateFlyout = false;
            else
                OpenDateFlyout = true;
        }

        private void Click_UserTile()
        {
            if (OpenUserFlyout)
                OpenUserFlyout = false;
            else
                OpenUserFlyout = true;
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
            DateList.Clear();
            foreach (DataRow i in temp.Rows)
            {
                DateList.Add(new DateProcess { TurnNumber = Convert.ToInt32(i["turnNumber"].ToString()), DetailInfo = i["detailInfo"].ToString(), LevelDetail = i["levelDetail"].ToString() });
            }
            _dateListOnGridView = new ListCollectionView(DateList);
            _dateListOnGridView.GroupDescriptions.Add(new PropertyGroupDescription("LevelDetail"));
        }
        private void GetScore()
        {
            int level = dbUserScore.GetLevelByUserLoginName(Model.userLoginName); int userScore = dbUserScore.GetLevelScoreByUserLoginName(Model.userLoginName);
            UserLevel = level.ToString(); UserScore= userScore.ToString();
            int currentScoreToGain = dbUserScore.GetScoreToGainByLevel(level);
            int nextScoreToGain = dbUserScore.GetScoreToGainByLevel(level + 1);
            double percent = Math.Round(((double)nextScoreToGain - (double)currentScoreToGain) / 100.0, 1);
            var leverUpScore = (double)userScore - (double)currentScoreToGain;
            WidthOfProgress = leverUpScore / percent;
        }
    }
}
