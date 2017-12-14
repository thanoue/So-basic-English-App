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
using System.Windows.Threading;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using SoBasicEnglish.ViewModels;

namespace SoBasicEnglish.Views
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow
    {                 
        public SignInWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

     
    }
}
