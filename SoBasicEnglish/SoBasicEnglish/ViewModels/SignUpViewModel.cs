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
using System.IO;
using Microsoft.Win32;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Threading;

namespace SoBasicEnglish.ViewModels
{
   
    public class SignUpViewModel :  INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;


        DispatcherTimer timerToCloseNotify;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        dbLogin dbLogin;
        public ICommand SignUpCommand { get; set; }
        public ICommand SelectIimage { get; set; }
        public ICommand ClosingWindow { get; set; }
        public RelayCommand<Window> CloseWindowCommand { get; private set; }
        private byte[] _userAvt;
        public byte[] UserAvt { get => _userAvt; set  {
                _userAvt = value;
                NotifyPropertyChanged("UserAvt");
            } }
        private string _notiMessage = "";
        public string NotiMessage { get => _notiMessage; set { _notiMessage = value;
                NotifyPropertyChanged("NotiMessage");
            } }

        private bool _isOpenNoti = false;
        public bool IsOpenNoti { get => _isOpenNoti; set { _isOpenNoti = value;
                NotifyPropertyChanged("IsOpenNoti");
            } }
        private string _userLoginName = "";
        public string UserLoginName
        {
            get { return _userLoginName; }
            set
            {
                _userLoginName = value;
                NotifyPropertyChanged("UserLoginName");
            }
        }

        public bool OpenDiaglog { get => _openDiaglog; set { _openDiaglog = value;
                NotifyPropertyChanged("OpenDialog");
            } }

        private bool _openDiaglog = false;

        public ICommand DefaultNotification { get; set; }
        private int temp = 0;

        public SignUpViewModel()
        {
            
            SignUpCommand = new RelayCommand<UIElementCollection>((p) =>true, SignUp);
            ClosingWindow = new DelegateCommand(Closing);
            SelectIimage = new DelegateCommand(Browse);
            CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
            MemoryStream ms = new MemoryStream();
            //    Image a;
            //  a.Source= new BitmapImage(new Uri(@"Images/Avt/customer.png"));
            Properties.Resources.customer.Save(ms, Properties.Resources.customer.RawFormat);
            UserAvt = ms.GetBuffer();
            ms.Close();dbLogin = new dbLogin(Model.serverName);
            timerToCloseNotify = new DispatcherTimer();
            timerToCloseNotify.Tick += TimerToCloseNotify_Tick;
            timerToCloseNotify.Interval = new TimeSpan(0, 0, 1);

        }

        private void TimerToCloseNotify_Tick(object sender, EventArgs e)
        {
            if (temp == 1)
            {
                timerToCloseNotify.Stop();
                IsOpenNoti = false;
                NotiMessage = "";
                temp = 0;
            }
            else
                temp += 1;

        }

        private void CloseWindow(Window window)
        {
            OpenDiaglog = false;
            if (window != null)
            {
                window.Close();
            }
        }
        private void Closing() {
            OpenDiaglog = false;
            
        }
        public bool VerifyEmail(string emailVerify)
        {
            try
            {
                if (emailVerify == "")
                    return false;
                var eMailValidator = new System.Net.Mail.MailAddress(emailVerify);

            }
            catch (FormatException)
            {
                return  false;
            }
            return true;
        }
        private void SignUp(UIElementCollection obj)
        {
            string UserLoginName="";
            string Email=""; string PassWord="";string RetypePassWord="";string UserName="";string DetailInfo="";
            foreach(var i in obj)
            {
                TextBox temp = i as TextBox;
                if (temp != null)
                {
                    switch (temp.Name)
                    {
                        case "txtUserLoginName":
                            UserLoginName = temp.Text;
                            break;
                        case "txtEmail":
                            Email = temp.Text;
                            break;
                        case "txtUserFullName":
                            UserName = temp.Text;
                            break;
                        case "txtBasicInfo":
                            DetailInfo = temp.Text;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    PasswordBox temp2 = i as PasswordBox;
                    if (temp2 != null)
                    {
                        switch (temp2.Name)
                        {
                            case "txtUserLoginpPassword":
                                PassWord = temp2.Password;
                                break;
                            case "txtUserLoginpPasswordRetype":
                                RetypePassWord = temp2.Password;
                                    break;
                            default:
                                break;

                        }
                    }
                }
            }

            if (VerifyEmail(Email))
            {
                if (PassWord == RetypePassWord)
                {
                    if( UserLoginName.Length >= 8)
                    {
                        if (UserName.Length >= 10)
                        {
                            string er = "";
                           
                            try
                            {
                                if (dbLogin.SignUp(ref er, UserLoginName, PassWord, Email, UserName, DetailInfo, UserAvt))
                                {
                                    OpenDiaglog = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }

                        }

                        else
                        {
                            IsOpenNoti = true;
                            NotiMessage = "The User full name must be at least 10 chars!!!";
                            timerToCloseNotify.Start();
                        }
                          
                    }
                    else
                    {
                        IsOpenNoti = true;
                        NotiMessage = "The User login name must be at least 8 chars!!!";
                        timerToCloseNotify.Start();
                    }
                        
                }
                else
                {
                    IsOpenNoti = true;
                    NotiMessage = "Retype the password...";
                    timerToCloseNotify.Start();
                }
                    
            }
            else
            {
                IsOpenNoti = true;
                NotiMessage = "Fake Email!!!";
                timerToCloseNotify.Start();
            }

         
        }
        void Browse()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            //  openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() != null) {

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

        
    }
}
