using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KiddEsports
{
    public class Message
    {
        #region Setup
        string applicationName = "Team Tracker";
        #endregion


        #region Methods
        // Show error message.
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, applicationName, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowWarningMessageYes(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, applicationName, MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
            return result == MessageBoxResult.Yes;
        }
        #endregion
    }
}
