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

namespace SoBasicEnglish.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        dbLogin dbLogin;
        private bool openDiaglog = false;
        public bool OpenDiaglog
        {
            get { return openDiaglog; }
            set
            {
                openDiaglog = value;
                NotifyPropertyChanged("OpenDiaglog");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public LoginViewModel()
        {
            dbLogin = new dbLogin(Model.serverName);
            Login = new RelayCommand<UIElementCollection>((p) => true, LoginToSystem);
            CLickNo = new RelayCommand<object>((p) => true, Close);

            ShowMenuCommand = new DelegateCommand(ShowMenu);
            ShowSignUpCommand = new DelegateCommand(ShowSignUp);

        }

        private void ShowMenu()
        {
            MenuWindow menu = new MenuWindow();
            OpenDiaglog = false;
            menu.ShowDialog();
        }
        private void ShowSignUp()
        {
            SignUpWindow signUp = new SignUpWindow();
            signUp.ShowDialog();
        }
        public void LoginToSystem(UIElementCollection p)
        {
            string sqlConnect = "data source = " + Model.serverName + "; initial catalog =SoBasicEnglishApp; integrated security = true";
            // string sqlConnect = @"Server=tcp:trankhaserver.database.windows.net,1433;Initial Catalog=demo;Persist Security Info=False;User ID=thanoue;Password=Namidth123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(sqlConnect); string loginName = ""; string passWord = "";
            try
            {
                foreach (var i in p)
                {
                    TextBox temp = i as TextBox;
                    if (temp == null)
                    {
                        PasswordBox pass = i as PasswordBox;
                        if (pass != null)
                        {

                            switch (pass.Name)
                            {
                                case "txtUserLoginName":
                                    loginName = temp.Text;
                                    break;
                                case "txtUserLoginpassWord":
                                    passWord = pass.Password;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (temp.Name)
                        {
                            case "txtUserLoginName":
                                loginName = temp.Text;
                                break;
                            case "txtUserLoginpassWord":
                                passWord = temp.Text;
                                break;
                            default:
                                break;
                        }
                    }


                }
                conn.Open();
                conn.Close();
                if (loginName != "" && passWord != "")
                {
                    if (dbLogin.Login(loginName, passWord) == loginName)
                    {
                        string userLoginName = dbLogin.GetUserNameByUserLoginName(loginName);
                        Model.role = dbLogin.GetRoleByUserLoginName(loginName);
                        Model.userFullname = userLoginName; Model.userLoginName = loginName;
                      
                            Model.userAVT = dbLogin.GetUserAVT(loginName);
                            //Menu mc = new Menu();
                            OpenDiaglog = true;

                            conn.Open();
                        conn.Close();
                     
                    }
                    else
                        DisplayMessageBox("thất bại!!");
                }

            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void DisplayMessageBox(object message)
        {
            MessageBox.Show((string)message);
        }
        public void Close(object message)
        {
            OpenDiaglog = false;
        }
        public ICommand Login { get; set; }
        public ICommand CLickNo { get; set; }      
        public ICommand ShowSignUpCommand { get; set; }
        public ICommand ShowMenuCommand
        {
            get;
            set;
        }


    }
}
