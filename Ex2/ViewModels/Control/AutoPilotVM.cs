using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ex2.Model.Client;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using Ex2.Model;

namespace Ex2.ViewModels
{
    /// <summary>
    /// Auto Pilot ViewModel
    /// </summary>
    public class AutoPilotVM : INotifyPropertyChanged
    {
        // the model
        private IMainModel Model;
        public AutoPilotVM()
        {
            Model = MainModel.Instance;
        }

        /// <summary>
        /// the enum of states
        /// </summary>
        public enum State { DIRTY, CLEAN, SENDING }

        /// <summary>
        ///  the state of the text
        /// </summary>
        private State curState = State.CLEAN;
        public State CurState
        {
            get
            {
                return curState;
            }

            set
            {
                curState = value;
                NotifyPropertyChanged("CurState");
            }
        }

        /// <summary>
        /// current sending
        /// </summary>
        private bool curSend;
        public bool CurSend
        {
            set
            {
                curSend = value;

                if (curSend)
                {
                    CurState = State.SENDING;
                }
            }
        }

        /// <summary>
        /// the text in the TextBOX
        /// </summary>
        private string text;
        public string TextCommand
        {
            get { return text; }
            set
            {
                text = value;
                NotifyPropertyChanged("TextCommand");

                if (String.IsNullOrEmpty(text))
                {
                    CurState = State.CLEAN;
                }
                else
                {
                    CurState = State.DIRTY;
                }
            }
        }

        /// <summary>
        /// the clear command
        /// </summary>
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => OnClearClick()));
            }
        }

        /// <summary>
        /// on click clear action
        /// </summary>
        private void OnClearClick()
        {
            curSend = false;
            TextCommand = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// the ok command
        /// </summary>
        private ICommand _okCommand;
        public ICommand OKCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => OnOKClick()));
            }
        }

        /// <summary>
        /// on click ok action
        /// </summary>
        private void OnOKClick()
        {
            // if the text is empty or the client is not connected
            if (String.IsNullOrEmpty(text) || !Model.ClientModel.IsOpen)
            {
                return;
            }

            // change the state to sending
            CurState = State.SENDING;

            // sperate by lines
            List<string> commands = TextCommand.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList<string>();

            // create a task that each 2 second sent a command to the model
            Task task = new Task(delegate ()
            {

                foreach (string cmd in commands)
                {
                        Model.ClientModel.SendLine(cmd);
                        Thread.Sleep(2000);
                }
            });
            task.Start();
        }

        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="propName">the property change value</param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
