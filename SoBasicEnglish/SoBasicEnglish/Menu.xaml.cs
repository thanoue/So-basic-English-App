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
using  System.Windows.Threading;
using System.Data;
using System.ComponentModel;

namespace SoBasicEnglish
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : INotifyPropertyChanged
    {
        public class DateProcess
        {
            public int turnNumber { get; set; }
            public string detailInfo { get; set; }
            public string levelDetail { get; set; }
        }
        public void GetDateProcess()
        {
            DataTable temp = new DataTable();
            temp = dbDateProcess.DatesprocessList();
            foreach(DataRow i in temp.Rows)
            {
                dateList.Add(new DateProcess { turnNumber = Convert.ToInt32(i["turnNumber"].ToString()),detailInfo = i["detailInfo"].ToString(),levelDetail = i["levelDetail"].ToString() });
            }
        }
        public Menu()
        {
            InitializeComponent(); dbUserScore = new dbUserScore(Model.serverName);dbDateProcess = new dbDateProcess(Model.serverName);
            GetDateProcess();
            var collectionVwSrc = new ListCollectionView(dateList);
            collectionVwSrc.GroupDescriptions.Add(new PropertyGroupDescription("levelDetail"));
            dataGroupedGrid.ItemsSource = collectionVwSrc;
            btnsetting.Click += async ( o, e) =>
            {
                await this.ShowMessageAsync("This is the title", "Some message");
            };
        }
        DispatcherTimer dispatcherTimer; SolidColorBrush gridColor;dbDateProcess dbDateProcess;
     public List<DateProcess> dateList = new List<DateProcess>();private byte[] userAvtIMG;
        public byte[] UserAvtIMG
        {
            get { return userAvtIMG; }
            set
            {
                userAvtIMG = value;
                OnpropertyChanged("UserAvtIMG");
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
             gridColor = new SolidColorBrush(Colors.CadetBlue);
            gridColor.Opacity = 0.2;
            this.DataContext = this;
            lbUserName.Text = Model.userFullname;
            UserAvtIMG = Model.userAVT;
            //var bm = new BitmapImage();
            //bm = ToImage(Model.userAVT);
            //imgAVT.ImageSource = bm;
            GetScore();

        }
        dbUserScore dbUserScore;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnpropertyChanged(string lessonName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(lessonName));
            }
        }
        private void GetScore()
        {
            int level = dbUserScore.GetLevelByUserLoginName(Model.userLoginName); int userScore = dbUserScore.GetLevelScoreByUserLoginName(Model.userLoginName);
            lbUserLevel.Content = level.ToString(); lbUserLevelScore.Text = userScore.ToString();
            int currentScoreToGain = dbUserScore.GetScoreToGainByLevel(level);
            int nextScoreToGain = dbUserScore.GetScoreToGainByLevel(level + 1);
            double percent = Math.Round(((double)nextScoreToGain - (double)currentScoreToGain) / 100.0, 1);
            var leverUpScore = (double)userScore - (double)currentScoreToGain;
            pcLevel.Width = leverUpScore / percent;
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
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (FlipViewTest.SelectedIndex >= 0 && FlipViewTest.SelectedIndex < 2)
                FlipViewTest.SelectedIndex = FlipViewTest.SelectedIndex + 1;
            else
                FlipViewTest.SelectedIndex = 0;
            CommandManager.InvalidateRequerySuggested();
        }
        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flipview = ((FlipView)sender);
            switch (flipview.SelectedIndex)
            {
                case 0:
                    flipview.BannerText = "Cupcakes!";
                    break;
                case 1:
                    flipview.BannerText = "Xbox!";
                    break;
                case 2:
                    flipview.BannerText = "Chess!";
                    break;
            }
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            flUser.IsOpen = true;
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            if (flProccess.IsOpen == false)
                flProccess.IsOpen = true;
            else flProccess.IsOpen = false;
        }

        private void btnCloseChampions_Click(object sender, RoutedEventArgs e)
        {
            flChampions.IsOpen = false;
        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            flChampions.IsOpen = true;
        }

        private void gEdituserInfo_MouseEnter(object sender, MouseEventArgs e)
        {
            gEdituserInfo.Background = Brushes.CadetBlue;
        }

        private void gEdituserInfo_MouseLeave(object sender, MouseEventArgs e)
        {
            gEdituserInfo.Background = gridColor;      
       }


        private void gChangePass_MouseEnter(object sender, MouseEventArgs e)
        {
            gChangePass.Background = Brushes.CadetBlue;
        }

        private void gChangePass_MouseLeave(object sender, MouseEventArgs e)
        {
            gChangePass.Background = gridColor;
        }

        private void gLogOut_MouseEnter(object sender, MouseEventArgs e)
        {
            gLogOut.Background = Brushes.CadetBlue;
        }

        private void gLogOut_MouseLeave(object sender, MouseEventArgs e)
        {
            gLogOut.Background = gridColor;
        }

        private void gChangePass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangePasswordScreen change = new ChangePasswordScreen();change.ShowDialog();
        }

        private void gEdituserInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            EditUserProfileScreen edit = new EditUserProfileScreen();edit.ShowDialog(); 
        }

        private void dataGroupedGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //StudyScreen study = new StudyScreen();study.ShowDialog();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DateProcess drv = (DateProcess)dataGroupedGrid.SelectedItem;
                String result = (drv.turnNumber).ToString(); Model.dateProcess = Int32.Parse(result);
               // this.Hide();
                new StudyScreen().ShowDialog();
               // this.Show();
            }
            catch (Exception)
            {

            }
           
        }

        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {
            new UserRoleScreen().ShowDialog();
        }

        private void Tile_Click_4(object sender, RoutedEventArgs e)
        {
            new EditorScreen().ShowDialog();
        }

        private void Tile_Click_5(object sender, RoutedEventArgs e)
        {
            new ExamScreen().ShowDialog();
        }
    }
}
