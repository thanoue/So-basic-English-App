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
using System.Net.Mail;

namespace SoBasicEnglish.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Login { get; set; }
        public ICommand CLickNo { get; set; }
        public ICommand ShowSignUpCommand { get; set; }
        public ICommand ShowMenuCommand
        {
            get;
            set;
        }
        dbLogin dbLogin;
        private bool openDiaglog = false;
        private bool _isOpenError = false;
        private string _errorMessage = "";
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; NotifyPropertyChanged("ErrorMessage"); } }
        public bool OpenDiaglog
        {
            get { return openDiaglog; }
            set
            {
                openDiaglog = value;
                NotifyPropertyChanged("OpenDiaglog");
            }
        }
        private bool _openErrorDialog = false;
        public bool OpenErrorDialog { get => _openErrorDialog; set { _openErrorDialog = value; NotifyPropertyChanged("OpenErrorDialog"); } }

        public bool IsOpenError { get => _isOpenError; set { _isOpenError = value; NotifyPropertyChanged("IsOpenError"); } }

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
                        string userName = dbLogin.GetUserNameByUserLoginName(loginName);
                        Model.role = dbLogin.GetRoleByUserLoginName(loginName);
                        Model.userFullname = userName; Model.userLoginName = loginName;
                        Model.userPassword = passWord;

                        Model.userAVT = dbLogin.GetUserAVT(loginName);
                        //Menu mc = new Menu();
                        conn.Open();
                        conn.Close();
                        OpenDiaglog = true;


                    }
                    else
                    {
                        IsOpenError = true;
                        ErrorMessage = "Check your password again!";
                    }
                        
                }

            }
            catch (System.Exception )
            {

                IsOpenError = true;
                ErrorMessage = "System Error";
            }
        }      
        public void Close(object message)
        {
            if(OpenErrorDialog)
                OpenErrorDialog = false;
            else
            OpenDiaglog = false;
          
        }
        private void GuiThu(string diachigui, string matkhau, string diachinhan, string tieude, string noidung)
        {
            MailMessage mail = new MailMessage(diachigui, diachinhan, tieude, noidung);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(diachigui, matkhau);
            client.EnableSsl = true;
            client.Send(mail);
            MessageBox.Show("Check your Email to get your own password!!!");
        }

    }
}
