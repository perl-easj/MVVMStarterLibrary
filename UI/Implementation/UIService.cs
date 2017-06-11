using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Commands.Implementation;
#pragma warning disable 4014

namespace UI.Implementation
{
    public class UIService
    {
        public static void PresentMessageWithUndo(string message, Action undo)
        {
            PresentMessageOkOnly(message, "Undo", new RelayCommand(undo));
        }

        public static async Task PresentMessageNoAction(string message, string commandText)
        {
            await PresentMessageOkOnly(message, commandText, new RelayCommand(() => { }));
        }

        public static async Task PresentMessageOkOnly(string message, string commandText, RelayCommand userAction)
        {
            var messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand(commandText, userAction.Execute));
            await messageDialog.ShowAsync();
        }

        public static async Task PresentMessageOkCancel(string message, string commandText, RelayCommand userAction)
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
