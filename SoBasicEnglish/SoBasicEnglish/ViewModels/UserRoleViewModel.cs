using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using BusinessLogicFramework;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoBasicEnglish.ViewModels
{
    public   class UserRoleViewModel : INotifyPropertyChanged
    {
        #region Icommand objects
        public ICommand Click_ChangRole { get; set; }
        public ICommand Click_ConfirmChangeRole { get; set; }
        public ICommand CLick_CancelChangeRole { get; set; }
        public ICommand Click_Delete { get; set; }
        #endregion
        #region objects
        private bool _openConfirmDiaglog = false;
        public bool OpenConfirmDiaglog { get => _openConfirmDiaglog; set { _openConfirmDiaglog = value; NotifyPropertyChanged("OpenConfirmDiaglog"); } }
        public event PropertyChangedEventHandler PropertyChanged;
        private   ObservableCollection<User> _userlist = new ObservableCollection<User>();
        private dbLogin dbLogin;
        public ObservableCollection<User> Userlist { get => _userlist; set { _userlist = value; NotifyPropertyChanged("Userlist"); } }

        public bool OpenConfirmDeleteUserDiaglog { get => _openConfirmDeleteUserDiaglog; set { _openConfirmDeleteUserDiaglog = value;NotifyPropertyChanged("OpenConfirmDeleteUserDiaglog"); } }

        private bool _openConfirmDeleteUserDiaglog = false;
        #endregion
        #region Constructor
        public UserRoleViewModel()
        {
            dbLogin = new dbLogin(Model.serverName);
            Click_ChangRole = new RelayCommand<object>((p)=>p!=null, ShowConfirmDialog);
            Click_ConfirmChangeRole =  new RelayCommand<object>((p) => p != null, ChangeUserRole);
            CLick_CancelChangeRole = new DelegateCommand(CancelChangeRole);
            Click_Delete = new RelayCommand<object>((p) => p != null, ShowConfirmDeleteUserDiaglog);
            LoadData();
        }

        private void ShowConfirmDeleteUserDiaglog(object obj)
        {
            OpenConfirmDeleteUserDiaglog = true;
            //User temp = obj as User;
          
        }

        private void CancelChangeRole()
        {
            OpenConfirmDiaglog = false;OpenConfirmDeleteUserDiaglog = false;
        }

        private void ShowConfirmDialog(object obj)
        {
            OpenConfirmDiaglog = true;
        }
        #endregion
        #region Icommand
        private void ChangeUserRole(object obj)
        {
            User temp = obj as User;
            if (OpenConfirmDiaglog)
            {
                string err = "";
                if (dbLogin.ChangTheRole(ref err, temp.UserLoginName, temp.RoleId))
                {
                    LoadData();
                }
                OpenConfirmDiaglog = false;
            }
            else
            {
                string er = "";
                if (dbLogin.DeleteUser(ref er, temp.UserLoginName))
                {
                    LoadData();
                }
                OpenConfirmDeleteUserDiaglog = false;
            }


        }      
        #endregion
        #region functions
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void LoadData()
        {
            DataTable temp = dbLogin.GetUserList();
            Userlist.Clear();
            foreach (DataRow i in temp.Rows)
            {
                Userlist.Add(new User { UserLoginName = i["loginName"].ToString(),UserScore = Int32.Parse(i["levelScore"].ToString()), UserAvatar = (byte[])i["userAvatar"], UserName = i["userName"].ToString(), RoleId = Int32.Parse(i["userRole"].ToString()), Role = i["roleName"].ToString(), UserLevel = Int32.Parse(i["userLevel"].ToString()) });
            }       
        }

        #endregion



    }
}
