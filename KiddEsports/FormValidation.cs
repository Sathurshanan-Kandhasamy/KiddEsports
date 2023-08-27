using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KiddEsports
{
    public class FormValidation
    {
        #region Methods
        // Form fields are empty validation.
        public bool IsAllFieldsFilled(string[] formInputs)
        {
            foreach (string input in formInputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return false;
                }
            }
            return true;
        }

        // Team already exist validation.
        public bool IsTeamAlreadyExist(string teamName, List<Team> teamList)
        {
            string sanitizedTeamName = teamName.Trim();
            foreach (Team team in teamList)
            {
                if (team.TeamName == sanitizedTeamName)
                {
                    return true;
                }
            }
            return false;
        }

        // Email validation.
        public bool IsValidEmail(string contactEmail)
        {
            string sanitizedContactEmail = contactEmail.ToLower().Trim();
            if (sanitizedContactEmail.StartsWith(".") || sanitizedContactEmail.EndsWith("."))
            {
                return false;
            }

            string[] parts = contactEmail.Split("@");
            if (parts.Length != 2)
            {
                return false;
            }
            string domain = parts[1];
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
        public bool IsEmailAlreadyExist(string contactEmail, List<Team> teamList)
        {
            string sanitizedEmail = contactEmail.ToLower().Trim();
            foreach (Team team in teamList)
            {
                if (team.ContactEmail == sanitizedEmail)
                {
                    return true;
                }
            }
            return false;
        }

        // Positive number validation.
        public bool IsPositiveNumber(string competitionPoints)
        {
            if (int.TryParse(competitionPoints, out _) != true)
            {
                return false;
            }
            if (int.Parse(competitionPoints) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Validate phone number.
        public bool IsValidPhoneNumber(string contactPhone)
        {
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
