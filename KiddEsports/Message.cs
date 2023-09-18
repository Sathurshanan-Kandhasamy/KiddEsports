using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KiddEsports
{
    public static class Message
    {
        #region Methods
        /// <summary>
        /// Shows an error message.
        /// </summary>
        /// <param name="message">Message string.</param>
        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Team Tracker", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Shows an warning message with YES and No buttons.
        /// </summary>
        /// <param name="message">Message string.</param>
        /// <returns>True or false.</returns>
        public static bool ShowWarningMessageYes(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Team Tracker", MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
            return result == MessageBoxResult.Yes;
        }
        #endregion
    }
}
