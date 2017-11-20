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
using System.Windows.Threading;

namespace SoBasicEnglish.Views
{
    /// <summary>
    /// Interaction logic for StudyWindow.xaml
    /// </summary>
    public partial class StudyWindow 
    {
        public StudyWindow()
        {
            InitializeComponent();
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
          //  UnhandledException exWin = new UnhandledException(e.Exception);
            //exWin.ShowDialog();
        }
    }
}
