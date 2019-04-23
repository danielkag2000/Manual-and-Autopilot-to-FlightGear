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
    public class AutoPilotVM : INotifyPropertyChanged
    {
        private IMainModel Model;
        public AutoPilotVM()
        {
            Model = MainModel.Instance;
        }

        public enum State { DIRTY, CLEAN, SENDING }
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

        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => OnClearClick()));
            }
        }

        private void OnClearClick()
        {
            curSend = false;
            TextCommand = "";
        }

        private ICommand _okCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OKCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => OnOKClick()));
            }
        }

        private void OnOKClick()
        {
            if (String.IsNullOrEmpty(text))
            {
                return;
            }
            string before = TextCommand;
            List<string> commands = TextCommand.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList<string>();

            Task task = new Task(delegate ()
            {

                foreach (string cmd in commands)
                {
                        Model.ClientModel.SendLine(cmd);
                        Thread.Sleep(2000);
                }
                if (TextCommand.Equals(before))
                {
                    CurState = State.SENDING;
                }
            });
            task.Start();
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
