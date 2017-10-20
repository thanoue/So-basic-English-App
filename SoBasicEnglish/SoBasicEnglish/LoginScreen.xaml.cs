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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;
using BusinessLogicFramework;
using System.Data.SqlClient;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

namespace SoBasicEnglish
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
           
            Model.serverName=@".\";
            dbLogin = new dbLogin(Model.serverName);
        }
        dbLogin dbLogin;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sqlConnect = "data source = "+Model.serverName+"; initial catalog =SoBasicEnglishApp; integrated security = true";
            // string sqlConnect = @"Server=tcp:trankhaserver.database.windows.net,1433;Initial Catalog=demo;Persist Security Info=False;User ID=thanoue;Password=Namidth123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(sqlConnect);
            try
            {
                conn.Open();
                conn.Close();
                if (dbLogin.Login(txtUserLoginName.Text, txtUserLoginpassWord.Password) == txtUserLoginName.Text.Trim())
                {
                    string userLoginName = dbLogin.GetUserNameByUserLoginName(txtUserLoginName.Text.Trim());
                    Model.role = dbLogin.GetRoleByUserLoginName(txtUserLoginName.Text.Trim());
                    Model.userFullname = userLoginName;Model.userLoginName = txtUserLoginName.Text.Trim();
                    try
                    {
                        Model.userAVT = dbLogin.GetUserAVT(txtUserLoginName.Text);
                        Menu mc = new Menu();
                        this.Hide();
                        mc.ShowDialog();
                        this.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                
                }
                else
                    MessageBox.Show("Đăng nhập thất bại");
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void Login()
        {

        }

        private void txtUserLoginpassWord_KeyDown(object sender, KeyEventArgs e)
        {
          
        }
        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            string pass = dbLogin.GetPassWordByUserLoginName(txtLoginNameToReSetPassWord.Text.Trim(), txtEmail.Text.Trim());
            if ( pass != null)
            {
                GuiThu("khoikhaguitar.vl@gmail.com", "khoikha123", txtEmail.Text, "Get Your Own Password here!!!", "Your password is : "+pass+"");
            }
          
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpScreen signup = new SignUpScreen();
            signup.Show();

        }
    }
}
