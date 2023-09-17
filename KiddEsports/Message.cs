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
        // Stores MessageBox title value.
        string applicationName = "Team Tracker";
        #endregion


        #region Methods
        /// <summary>
        /// Shows an error message.
        /// </summary>
        /// <param name="message">Message string.</param>
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, applicationName, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Shows an warning message with YES and No buttons.
        /// </summary>
        /// <param name="message">Message string.</param>
        /// <returns>True or false.</returns>
        public bool ShowWarningMessageYes(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, applicationName, MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
            return result == MessageBoxResult.Yes;
        }
        #endregion
    }
}
