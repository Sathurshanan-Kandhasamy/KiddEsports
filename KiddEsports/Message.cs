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
        // Store application name for message box title.
        string applicationName = "Team Tracker";
        #endregion


        #region Methods
        /// <summary>
        /// Method for howing error message.
        /// </summary>
        /// <param name="message">Message for the message box.</param>
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, applicationName, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method for showing warning message with YES and No buttons.
        /// </summary>
        /// <param name="message">Message for the message box.</param>
        /// <returns>If YES clicked returns true, otherwise returns false.</returns>
        public bool ShowWarningMessageYes(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, applicationName, MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
            return result == MessageBoxResult.Yes;
        }
        #endregion
    }
}
