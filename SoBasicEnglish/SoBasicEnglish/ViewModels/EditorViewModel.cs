using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BusinessLogicFramework;

namespace SoBasicEnglish.ViewModels
{
    public  class EditorViewModel : INotifyPropertyChanged
    {
        #region ICommand object
        #endregion
        #region objects
        private dbDateProcess dbDateProcess;
        private ListCollectionView _dateListOnGridView;
        public ListCollectionView DateListOnGridView
        {
            get => _dateListOnGridView; set
            {
                _dateListOnGridView = value;
                NotifyPropertyChanged("DateListOnGridView");
            }
        }
        private ObservableCollection<DateProcess> _dateList = new ObservableCollection<DateProcess>();
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<DateProcess> DateList { get => _dateList; set { _dateList = value; NotifyPropertyChanged("DateList"); } }
        #endregion
        #region Constructor
        public EditorViewModel()
        {
            dbDateProcess = new dbDateProcess(Model.serverName); GetDateProcess();
        }
        #endregion
        #region ICommands
        #endregion
        #region functons
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void GetDateProcess()
        {
            DataTable temp = new DataTable();
            temp = dbDateProcess.DatesprocessList();
            DateList.Clear();
            foreach (DataRow i in temp.Rows)
            {
                DateList.Add(new DateProcess { TurnNumber = Convert.ToInt32(i["turnNumber"].ToString()), DetailInfo = i["detailInfo"].ToString(), LevelDetail = i["levelDetail"].ToString() });
            }
            _dateListOnGridView = new ListCollectionView(DateList);
            _dateListOnGridView.GroupDescriptions.Add(new PropertyGroupDescription("LevelDetail"));
        }
        #endregion  


    }
}
