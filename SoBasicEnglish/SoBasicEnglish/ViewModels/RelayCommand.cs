using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoBasicEnglish.ViewModels
{
    public class RelayCommand<T> : ICommand //lớp generic, có thể tùy trọn kiểu tham số truyền vào theo ý muốn
    {
        #region Fields

        readonly Action<T> _execute = null; // lưu trữ hàm được ủy thác để thực hiện 1 việc nào đó
        readonly Predicate<T> _canExecute = null;// điều kiện để thực hiện command, hàm ủy thác có dc chạy hay ko là phụ thuộc vào biến này

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>


        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Predicate<T> canExecute, Action<T> execute) // hàm dựng của class này, truyền vào hàm ủy thác và điều kiện chạy command
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        public RelayCommand(Action<T> execute) : this(null, execute)
        {

        }
        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object parameter) //truyền vào 1 điều kiện để chạy event 
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged // tạo 1 event 
        {
            add { CommandManager.RequerySuggested += value; } // cá event được tạo ra hoặc xóa đi được lưu vào commandmanager 
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter) // hàm public từ view truyền vào lớp này để có thể chạy command
        {
            _execute((T)parameter);
        }

        #endregion
    }
}
