using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Drawing.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.IO;
using BusinessLogicFramework;
using Microsoft.Win32;
using MahApps.Metro.Controls;
namespace SoBasicEnglish
{
    /// <summary>
    /// Interaction logic for SignUpScreen.xaml
    /// </summary>
    public partial class SignUpScreen : MetroWindow
    {
        dbLogin dbLogin;
        public SignUpScreen()
        {
            InitializeComponent();def();dbLogin = new dbLogin(Model.serverName);
        }
        byte[] byteAVT;
        void def()
        {
            MemoryStream ms = new MemoryStream();
            //    Image a;
            //  a.Source= new BitmapImage(new Uri(@"Images/Avt/customer.png"));
            Properties.Resources.customer.Save(ms, Properties.Resources.customer.RawFormat);
            byteAVT = ms.GetBuffer();
            ms.Close();

        }
        public bool VerifyEmail(string emailVerify)
        {
            try
            {
                var eMailValidator = new System.Net.Mail.MailAddress(emailVerify);

            }
            catch (FormatException )
            {
                return false;
            }
            return true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyEmail(txtEmail.Text))
            {
                if (txtUserLoginpPassword.Password == txtUserLoginpPasswordRetype.Password)
                {
                    if (txtUserLoginName.Text.Length >= 8)
                    {
                        if (txtUserFullName.Text.Length >= 10)
                        {
                            string er = "";
                            string myText = new TextRange(rtxtBasicInfo.Document.ContentStart, rtxtBasicInfo.Document.ContentEnd).Text;
                            try {
                                if (dbLogin.SignUp(ref er, txtUserLoginName.Text, txtUserLoginpPassword.Password, txtEmail.Text, txtUserFullName.Text, myText, byteAVT))
                                {
                                    MessageBox.Show("Done!!!");
                                }
                            }
                            catch (Exception ex){
                                MessageBox.Show(ex.ToString());
                            }
                        
                        }

                        else
                            MessageBox.Show("The User login name must be at least 10 chars!!!");
                    }
                    else
                        MessageBox.Show("The User login name must be at least 8 chars!!!");
                }
                else
                    MessageBox.Show("Retype the password...");
            }
            else
                MessageBox.Show("Fake Email!!!");
        }

        private void btnAVT_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
          //  openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                // MessageBox.Show(openFileDialog.FileName);
                imgAVT.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                BitmapImage a = new BitmapImage(new  Uri(openFileDialog.FileName));
                
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(a));
                encoder.Save(memStream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
                memStream = Model.compress(img);
                byteAVT = memStream.ToArray();memStream.Close();
            }
        }
      
        private void rtxtBasicInfo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
