using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace KiddEsports
{
    public class FormValidation
    {
        #region Methods
        /// <summary>
        /// Checks if form input field or fields are empty.
        /// </summary>
        /// <param name="formInputs">Array of form input field values.</param>
        /// <returns>True or false.</returns>
        public bool IsAllFieldsFilled(string[] formInputs)
        {
            // Iterates through each string in the formInputs array and check if a value empty, null, or whitespace.
            foreach (string input in formInputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if entered team name already exist in the teams list.
        /// </summary>
        /// <param name="teamName">Entered teamName value.</param>
        /// <param name="teamList">List of team objects.</param>
        /// <returns>True or false.</returns>
        public bool IsTeamAlreadyExist(string teamName, List<Team> teams)
        {
            // Removes whitespace at the start and end of teamName string.
            string sanitizedTeamName = teamName.Trim();
            // Iterates through each team in the teams list to check if team name already exists.
            foreach (Team team in teams)
            {
                // If teamName exists, returns true.
                if (team.TeamName == sanitizedTeamName)
                {
                    return true;
                }
            }
            // Otherwise returns false.
            return false;
        }

        /// <summary>
        /// Validates email address.
        /// </summary>
        /// <param name="contactEmail">Entered contactEmail value.</param>
        /// <returns>True or false.</returns>
        public bool IsValidEmail(string contactEmail)
        {
            // Removes whitespace at the start and end of the contactEmail string.
            string sanitizedContactEmail = contactEmail.ToLower().Trim();
            // if the contactEmail has period at the start and end of the string, return false.
            if (sanitizedContactEmail.StartsWith(".") || sanitizedContactEmail.EndsWith("."))
            {
                return false;
            }

            // Splits the email at @ symbol and stores two strings in the array.
            string[] parts = contactEmail.Split("@");
            // If parts array length is not equal to two, returns false.
            if (parts.Length != 2)
            {
                return false;
            }
            // Stores the email domain name.
            string domain = parts[1];
            try
            {
                // Checks if contactEmail is a valid email address.
                var email = new MailAddress(sanitizedContactEmail);
                // Validates contactEmail's domain.
                var hostEntry = Dns.GetHostEntry(domain);
                // If the contactEmail and domain name are valid, returns true.
                return email.Address == sanitizedContactEmail && hostEntry.HostName.Length > 0;
            }
            catch
            {
                // If the contactEmail and domain name invalid, returns false.
                return false;
            }
        }

        /// <summary>
        /// Checks if entered contactEmail already exists in the teamList.
        /// </summary>
        /// <param name="contactEmail">Entered contactEmail value.</param>
        /// <param name="teamList">List of team objects.</param>
        /// <returns>True or false.</returns>
        public bool IsEmailAlreadyExist(string contactEmail, List<Team> teamList)
        {
            // Converts contactEmail string to lowercase and removes whitespace at start and end.
            string sanitizedEmail = contactEmail.ToLower().Trim();
            // Iterates through each team in the teamList.
            foreach (Team team in teamList)
            {
                // if the team's contactEmail is same as entered contactEmail value, returns true.
                if (team.ContactEmail == sanitizedEmail)
                {
                    return true;
                }
            }
            // Otherwise returns false.
            return false;
        }

        /// <summary>
        /// Checks if the competitionPoints is a positive number.
        /// </summary>
        /// <param name="competitionPoints">Entered competiionPoints value.</param>
        /// <returns>True or false.</returns>
        public bool IsPositiveNumber(string competitionPoints)
        {
            // If parsing competitionPoits value to number was failed, returns false.
            if (int.TryParse(competitionPoints, out _) != true)
            {
                return false;
            }
            // If the competionPoints value is less than zero, returns false.
            if (int.Parse(competitionPoints) < 0)
            {
                return false;
            }
            else
            {
                // Otherwise return true.
                return true;
            }
        }

        /// <summary>
        /// Validates contactPhone value.
        /// </summary>
        /// <param name="contactPhone">Entered contactPhone value.</param>
        /// <returns>True or false.</returns>
        public bool IsValidPhoneNumber(string contactPhone)
        {
            // If the contactPhone value is a number, length is greater than zero and less than or equal to ten, returns true.
            // Otherwiser return false.
            if (int.TryParse(contactPhone, out _) && (contactPhone.Length > 0 && contactPhone.Length <= 10))
            {
                return true;
            }
            else
            {
                // Otherwise returns false.
                return false;
            }
        }
        #endregion
    }
}
