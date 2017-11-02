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
using SoBasicEnglish.ViewModels;
using WPFNotification.Services;

namespace SoBasicEnglish.Views
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow  
    {
        
        public SignUpWindow()
        {
            InitializeComponent();
          
            DataContext = new SignUpViewModel();
        }
    }
}
