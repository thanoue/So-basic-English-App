﻿#pragma checksum "..\..\..\Views\StudyWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E73A48DC469A6A2331BF1D2185A22A614EF1F5A4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using SoBasicEnglish.ViewModels;
using SoBasicEnglish.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace SoBasicEnglish.Views {
    
    
    /// <summary>
    /// StudyWindow
    /// </summary>
    public partial class StudyWindow : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 190 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSpeakGetReadyQuestion;
        
        #line default
        #line hidden
        
        
        #line 219 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton btnAutoNextGetReadyQuestion;
        
        #line default
        #line hidden
        
        
        #line 268 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbGetReadyQuestion;
        
        #line default
        #line hidden
        
        
        #line 356 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvKeywords;
        
        #line default
        #line hidden
        
        
        #line 436 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvKeyWordExList;
        
        #line default
        #line hidden
        
        
        #line 515 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvSentences;
        
        #line default
        #line hidden
        
        
        #line 567 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvSentenceExList;
        
        #line default
        #line hidden
        
        
        #line 621 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl SentenceWrap;
        
        #line default
        #line hidden
        
        
        #line 634 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvSentenceOrals;
        
        #line default
        #line hidden
        
        
        #line 747 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvListeningExList;
        
        #line default
        #line hidden
        
        
        #line 785 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvListenPart2QuestionList;
        
        #line default
        #line hidden
        
        
        #line 844 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DocumentViewer dvGrammar;
        
        #line default
        #line hidden
        
        
        #line 863 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnResetGrammarEx;
        
        #line default
        #line hidden
        
        
        #line 868 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSubmitGrammarEx;
        
        #line default
        #line hidden
        
        
        #line 872 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvGrammarExList;
        
        #line default
        #line hidden
        
        
        #line 939 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.RichTextBox _richTextBox;
        
        #line default
        #line hidden
        
        
        #line 1065 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout flChampions;
        
        #line default
        #line hidden
        
        
        #line 1086 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtContentOfQuestion;
        
        #line default
        #line hidden
        
        
        #line 1135 "..\..\..\Views\StudyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Tile btnCloseChampions;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SoBasicEnglish;component/views/studywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\StudyWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnSpeakGetReadyQuestion = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.btnAutoNextGetReadyQuestion = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 3:
            this.lbGetReadyQuestion = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.lvKeywords = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.lvKeyWordExList = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.lvSentences = ((System.Windows.Controls.ListView)(target));
            return;
            case 7:
            this.lvSentenceExList = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            this.SentenceWrap = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 9:
            this.lvSentenceOrals = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.lvListeningExList = ((System.Windows.Controls.ListView)(target));
            return;
            case 11:
            this.lvListenPart2QuestionList = ((System.Windows.Controls.ListView)(target));
            return;
            case 12:
            this.dvGrammar = ((System.Windows.Controls.DocumentViewer)(target));
            return;
            case 13:
            this.btnResetGrammarEx = ((System.Windows.Controls.Button)(target));
            return;
            case 14:
            this.btnSubmitGrammarEx = ((System.Windows.Controls.Button)(target));
            return;
            case 15:
            this.lvGrammarExList = ((System.Windows.Controls.ListView)(target));
            return;
            case 16:
            this._richTextBox = ((Xceed.Wpf.Toolkit.RichTextBox)(target));
            return;
            case 17:
            this.flChampions = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 18:
            this.txtContentOfQuestion = ((System.Windows.Controls.TextBox)(target));
            return;
            case 19:
            this.btnCloseChampions = ((MahApps.Metro.Controls.Tile)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

