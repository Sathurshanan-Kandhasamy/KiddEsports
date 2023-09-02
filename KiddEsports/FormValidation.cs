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
        /// Method for checking if form field or fields are empty.
        /// </summary>
        /// <param name="formInputs">Array of form input strings.</param>
        /// <returns>If any of the form field is empty returns true, otherwise returns false.</returns>
        public bool IsAllFieldsFilled(string[] formInputs)
        {
            // Iterate throgh each string in the array and check if its empty, null, or whitespace.
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
        /// Method for checking if team name exist in teams list.
        /// </summary>
        /// <param name="teamName">Entered team name value.</param>
        /// <param name="teamList">List of team objects.</param>
        /// <returns>If team name exists returns true, otherwise returns false.</returns>
        public bool IsTeamAlreadyExist(string teamName, List<Team> teams)
        {
            // Remove whitespace at the start and end of the string.
            string sanitizedTeamName = teamName.Trim();
            // Iterate throgh each team in the teams list to check if team name exist.
            foreach (Team team in teams)
            {
                if (team.TeamName == sanitizedTeamName)
                {
                    return true;
                }
            }
            return false;
        }

        // Email validation.
        /// <summary>
        /// Mehod for validating email address.
        /// </summary>
        /// <param name="contactEmail">Entered email address.</param>
        /// <returns>If email address is valid returns true, otherwise returns false.</returns>
        public bool IsValidEmail(string contactEmail)
        {
            // Remove whitespace at the start and end of the email.
            string sanitizedContactEmail = contactEmail.ToLower().Trim();
            // if the email has period at the start and end, return false.
            if (sanitizedContactEmail.StartsWith(".") || sanitizedContactEmail.EndsWith("."))
            {
                return false;
            }

            // Split the email at @ symbol and store two strings in the array.
            string[] parts = contactEmail.Split("@");
            // If parts array length is not equal to two, return false.
            if (parts.Length != 2)
            {
                return false;
            }
            // Store the email domain name.
            string domain = parts[1];
            // If email and domain name are valid, return true otherwise return false.
            try
            {
                var email = new MailAddress(sanitizedContactEmail);
                var hostEntry = Dns.GetHostEntry(domain);
                return email.Address == sanitizedContactEmail && hostEntry.HostName.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        // Email already exist validation.
        /// <summary>
        /// Method for checking if email already exists in TeamData.csv file.
        /// </summary>
        /// <param name="contactEmail">Entered email</param>
        /// <param name="teamList">List of team objects.</param>
        /// <returns>If email exist in the team list return true, otherwise return false.</returns>
        public bool IsEmailAlreadyExist(string contactEmail, List<Team> teamList)
        {
            // Covert email to lowercase, remove whitespace at start and end.
            string sanitizedEmail = contactEmail.ToLower().Trim();
            // Iterate through each team in teams list, if a team has entered email address return true
            // Otherwise return false.
            foreach (Team team in teamList)
            {
                if (team.ContactEmail == sanitizedEmail)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Mehod that checks if competition points is a positive number.
        /// </summary>
        /// <param name="competitionPoints">Entered competiion points value.</param>
        /// <returns>If competition poits is positive numbers returns true, otherwise returns false.</returns>
        public bool IsPositiveNumber(string competitionPoints)
        {
            // If converting competition poits to number is failed, return false.
            if (int.TryParse(competitionPoints, out _) != true)
            {
                return false;
            }
            // If competion points less than zero, return false. Otherwise return true.
            if (int.Parse(competitionPoints) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Method for validating phone number.
        /// </summary>
        /// <param name="contactPhone">Entered phone number.</param>
        /// <returns>If phone number is valid returns true, otherwise returns false.</returns>
        public bool IsValidPhoneNumber(string contactPhone)
        {
            // If phone number is a number and length greater than zero and less than or equal to ten, return true.
            // Otherwiser return false.
            if (int.TryParse(contactPhone, out _) && (contactPhone.Length > 0 && contactPhone.Length <= 10))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
