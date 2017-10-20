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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessLogicFramework;
using MahApps.Metro.Controls;

namespace SoBasicEnglish
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : MetroWindow
    {
        public MainScreen()
        {
            InitializeComponent(); dbUserScore = new dbUserScore(@".\");
        }
        dbUserScore dbUserScore;

        private void lbHome2_MouseEnter(object sender, MouseEventArgs e)
        {
            lbHome2.Foreground = Brushes.Aqua;
        }

        private void lbHome2_MouseLeave(object sender, MouseEventArgs e)
        {
            lbHome2.Foreground = Brushes.White;
        }

        private void lbHome_MouseEnter(object sender, MouseEventArgs e)
        {
            lbHome.Foreground = Brushes.Aqua;
        }

        private void lbHome_MouseLeave_1(object sender, MouseEventArgs e)
        {
            lbHome.Foreground = Brushes.White;
        }
        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbUserName.Content = Model.userFullname;
            var bm = new BitmapImage();
            bm = ToImage(Model.userAVT);
            imgAVT.ImageSource = bm;
            GetScore();

        }
        private void GetScore()
        {
            int level = dbUserScore.GetLevelByUserLoginName(Model.userLoginName);int userScore = dbUserScore.GetLevelScoreByUserLoginName(Model.userLoginName);
            lbUserLevel.Content = level.ToString();lbUserLevelScore.Text = userScore.ToString();
            int currentScoreToGain = dbUserScore.GetScoreToGainByLevel(level);
            int nextScoreToGain = dbUserScore.GetScoreToGainByLevel(level + 1);
            double percent = Math.Round(((double)nextScoreToGain - (double)currentScoreToGain) / 100.0, 1);
            var leverUpScore = (double)userScore - (double)currentScoreToGain;
            pcLevel.Width = leverUpScore / percent;
        }

    }
}
