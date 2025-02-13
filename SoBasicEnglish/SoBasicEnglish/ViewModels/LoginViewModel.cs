﻿using System.Text;
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
using System.Windows.Threading;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;

namespace SoBasicEnglish.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Command Object
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Login { get; set; }
        public ICommand CLickNo { get; set; }
        public ICommand ShowSignUpCommand { get; set; }
        public ICommand Click_ForgetPassword { get; set; }
        public ICommand ExpanderControl_Expanded { get; set; }
        public ICommand ExpanderControl_Collapsed { get; set; }
        public ICommand ShowMenuCommand
        {
            get;
            set;
        }
        public RelayCommand<Window> CloseWindowCommand { get; private set; }
        #endregion
        #region Objects
        dbLogin dbLogin;
        public double HeighOfWindow { get => _heighOfWindow; set { _heighOfWindow = value; NotifyPropertyChanged("HeighOfWindow"); } }
        private double _heighOfWindow = 381;
        private bool _isSendingEmai = false;
        public bool IsActive
        { get => _isSendingEmai; set { _isSendingEmai = value;
                NotifyPropertyChanged("IsActive");
            } }
        private bool openDiaglog = false;
        private bool _isOpenError = false;
        private string _errorMessage = "";
        DispatcherTimer timerToCloseNotify;
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
        #endregion
        #region Constructor
        public LoginViewModel()
        {
            Model.serverName = @".\";
            dbLogin = new dbLogin(Model.serverName);
            Login = new RelayCommand<UIElementCollection>((p) => true, LoginToSystem);
            CLickNo = new RelayCommand<object>((p) => true, Close);
            ShowMenuCommand = new DelegateCommand(ShowMenu);
            ShowSignUpCommand = new DelegateCommand(ShowSignUp);
            Click_ForgetPassword = new RelayCommand<UIElementCollection>(CanClickForgetPassword, ForgetPassword);
            CloseWindowCommand = new RelayCommand<Window>((p)=>true,CloseWindow);
            ExpanderControl_Collapsed = new DelegateCommand(CollapsedExpander);
            ExpanderControl_Expanded = new DelegateCommand(ExpandedExpander);
            timerToCloseNotify = new DispatcherTimer();
            timerToCloseNotify.Tick += TimerToCloseNotify_Tick;
            timerToCloseNotify.Interval = new TimeSpan(0, 0, 2);

        }

        private void CloseWindow(Window obj)
        {
            if (obj != null)
            {
                obj.Close();
            }
        }

        private void ExpandedExpander()
        {
            HeighOfWindow = 516;
        }

        private void CollapsedExpander()
        {
            HeighOfWindow = 381;
        }
        #endregion
        #region Functions
        private void ShowNotify()
        {
            NotifyIcon trayIcon = new NotifyIcon();
            List<string> NotiList = new List<string>();
            trayIcon = new NotifyIcon();
            NotiList.Clear();
            dbUserNotify dbUserNotify = new dbUserNotify(Model.serverName);
            var temp = dbUserNotify.GetNotAnsweredyetNotify(Model.userLoginName);
            //int userRole = dbLogin.GetRoleByUserLoginName(Model.userLoginName);
            dbLessonQuestion dbLessonQuestion = new dbLessonQuestion(Model.serverName);
            if (Model.role > 1)
            {
                if (dbLessonQuestion.GetNotAnsweredYetLessonQuestion())
                {
                    trayIcon.Icon = Properties.Resources.ROFL_01_WF;
                    trayIcon.BalloonTipText = "You have some question to answer, check it out!";
                    trayIcon.Visible = true;
                    trayIcon.BalloonTipTitle = "Your Message";
                    trayIcon.ShowBalloonTip(2000);
                }
            }           
            new Thread(() =>
            {
                foreach (DataRow t in temp.Rows)
                {
                    trayIcon.Icon = Properties.Resources.ROFL_01_WF;
                    trayIcon.BalloonTipText = t["contentOfNotify"].ToString();
                    trayIcon.Visible = true;
                    trayIcon.BalloonTipTitle = "Your Message";
                    trayIcon.ShowBalloonTip(2000);
                }
            }
            ).Start();
        }
        private void ForgetPassword(UIElementCollection obj)
        {
            string Email = ""; string UserName = "";
            foreach(var i in obj)
            {
                System.Windows.Controls.TextBox temp = i as System.Windows.Controls.TextBox;
                if (temp != null)
                {
                    switch (temp.Name)
                    {
                        case "txtEmailToResetPassword":
                            Email = temp.Text;
                            break;
                        case "txtLoginNameToReSetPassWord":
                            UserName = temp.Text;
                            break;
                        default:
                            break;

                    }
                }
            }
            string pass = dbLogin.GetPassWordByUserLoginName(UserName, Email);
            if (pass != null)
            {
                GuiThu("khoikhaguitar.vl@gmail.com", "khoikha123", Email, "[So Basic English App] Get Your Own Password here!!!", "Your password is : " + pass + "");
            }
        }
        private bool CanClickForgetPassword(UIElementCollection obj)
        {
            string Email = ""; string UserName = "";
            foreach (var i in obj)
            {
                System.Windows.Controls.TextBox temp = i as System.Windows.Controls.TextBox;
                if (temp != null)
                {
                    switch (temp.Name)
                    {
                        case "txtEmailToResetPassword":
                            Email = temp.Text;
                            break;
                        case "txtLoginNameToReSetPassWord":
                            UserName = temp.Text;
                            break;
                        default:
                            break;

                    }
                }
            }
            return !String.IsNullOrWhiteSpace(Email) && !String.IsNullOrWhiteSpace(UserName);
        }
        private void TimerToCloseNotify_Tick(object sender, EventArgs e)
        {
            if (IsOpenError)
            {
                timerToCloseNotify.Stop();
                IsOpenError = false;
            }
        
        }
        private void ShowMenu()
        {
            try{
                ShowNotify();
                MenuWindow menu = new MenuWindow();
                OpenDiaglog = false;
                menu.ShowDialog();
            }
            catch (Exception) { }
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
                    System.Windows.Controls.TextBox temp = i as System.Windows.Controls.TextBox;
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
                        ErrorMessage = "Your password or login name is not match !!";
                        IsOpenError = true;                      
                        timerToCloseNotify.Start();
                    }

                }

            }
            catch (System.Exception)
            {
                ErrorMessage = "System Error";
                IsOpenError = true;                
                timerToCloseNotify.Start();
            }
        }
        public void Close(object message)
        {
            if (OpenErrorDialog)
                OpenErrorDialog = false;
            else
                OpenDiaglog = false;

        }
        private void GuiThu(string diachigui, string matkhau, string diachinhan, string tieude, string noidung)
        {
            IsActive = true;

            Thread t1 = new Thread((p)=>{
              

                MailMessage mail = new MailMessage(diachigui, diachinhan, tieude, noidung);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential(diachigui, matkhau);
                client.EnableSsl = true;
                client.Send(mail);
                ErrorMessage = "Check your Email to get your own password!!!";
                IsOpenError = true;
                IsActive = false;
                timerToCloseNotify.Start();
            });
            t1.Start();
           
          
        }
        #endregion

    }
}
