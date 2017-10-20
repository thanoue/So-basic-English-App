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
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using System.Windows.Threading;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SoBasicEnglish
{
    /// <summary>
    /// Interaction logic for UserRoleScreen.xaml
    /// </summary>
    public partial class UserRoleScreen : INotifyPropertyChanged
    {
        public class User
        {
            public string userName { get; set; }
            public int userLevel { get; set; }
            public string role { get; set; }
            public int roleId { get; set; }
            public byte[] userAvatar { get; set; }
            public string userLoginName { get; set; }
        }
        private User selectedUser = new User();private ObservableCollection<User> userList = new ObservableCollection<User>();dbLogin dbLogin;
        public ObservableCollection<User> UserList
        {
            get { return userList; }
            set { userList = value;
                OnpropertyChanged("UserList");
            }
        }
        public void LoadData()
        {
            DataTable temp = dbLogin.GetUserList();
            UserList.Clear();
            foreach(DataRow i in temp.Rows)
            {
                UserList.Add(new User { userLoginName = i["loginName"].ToString(), userAvatar = (byte[])i["userAvatar"], userName = i["userName"].ToString(), roleId = Int32.Parse(i["userRole"].ToString()), role = i["roleName"].ToString(), userLevel = Int32.Parse(i["userLevel"].ToString())});
            }
            SelectedUser = UserList[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnpropertyChanged(string lessonName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(lessonName));
            }
        }
        public User SelectedUser
        {
            get { return selectedUser; }
            set {
                selectedUser = value;
                OnpropertyChanged("SelectedUser");
            }
        }
        public UserRoleScreen()
        {
            InitializeComponent();dbLogin = new dbLogin(Model.serverName);
   
            LoadData();
          
            //  A.userName = "Tran Kha";A.userLevel = 2;A.userAvatar = Model.userAVT;A.role = "admin";A.roleId = 3;A.userLoginName = "trankha";
        }    
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User temp = (User)gvUserList.SelectedItem;
            SelectedUser = temp;


        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }

        private void btnChangeRole_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("You realy want to change the role of this user?","Change the role", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string err = "";
                if(dbLogin.ChangTheRole(ref err, SelectedUser.userLoginName, SelectedUser.roleId))
                {
                    LoadData();                    
                }
            }
        }
    }
}
