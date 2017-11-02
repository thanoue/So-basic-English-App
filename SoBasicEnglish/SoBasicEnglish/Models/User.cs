using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoBasicEnglish
{    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private int userScore;
        public int UserScore
        {
            get { return userScore; }
            set
            {
                userScore = value;
                NotifyPropertyChanged("UserScore");
            }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        private int userLevel;
        public int UserLevel
        {
            get { return userLevel; }
            set
            {
                userLevel = value;
                NotifyPropertyChanged("UserLevel");
            }
        }
        private string role = "";
        public string Role
        {
            get { return role; }
            set
            {
                role = value;
                NotifyPropertyChanged("Role");
            }
        }
        private int roleId = 0;
        public int RoleId
        {
            get { return roleId; }
            set
            {
                roleId = value;
                NotifyPropertyChanged("RoleId");
            }
        }

        private byte[] userAvatar;
        public byte[] UserAvatar
        {
            get { return userAvatar; }
            set
            {
                userAvatar = value;
                NotifyPropertyChanged("UserAvatar");
            }
        }
        private string userLoginName;
        public string UserLoginName
        {
            get { return userLoginName; }
            set
            {
                userLoginName = value;
                NotifyPropertyChanged("UserLoginName");
            }
        }
    }
}
