using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;


namespace UI.Implementation
{
    public class UIService
    {
        public class ActionWrapper
        {
            private Action _userAction;

            public ActionWrapper(Action userAction)
            {
                _userAction = userAction;
            }

            public void Invoke(IUICommand command)
            {
                _userAction();
            }
        }

        public static async Task PresentMessageWithUndo(string message, Action undo)
        {
            await PresentMessageSingleAction(message, "Undo", new ActionWrapper(undo));
        }

        public static async Task PresentMessageNoAction(string message, string commandText)
        {
            await PresentMessageSingleAction(message, commandText, new ActionWrapper(() => { }));
        }

        public static async Task PresentMessageSingleAction(string message, Action userAction)
        {
            await PresentMessageSingleAction(message, "OK", new ActionWrapper(userAction));
        }

        public static async Task PresentMessageSingleAction(string message, string commandText, ActionWrapper aw)
        {
            var messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand(commandText, aw.Invoke));
            await messageDialog.ShowAsync();
        }

        public static async Task PresentMessageSingleActionCancel(string message, string commandText, ICommand userAction)
        {
            var messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand(commandText, userAction.Execute));
            messageDialog.Commands.Add(new UICommand("Cancel", command => { }));
            messageDialog.DefaultCommandIndex = 1;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }
    }
}
