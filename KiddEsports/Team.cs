using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddEsports
{
    public class Team
    {
        #region Setup
        private string _teamName = string.Empty;
        private string _primaryContact = string.Empty;
        private string _contactPhone = string.Empty;
        private string _contactEmail = string.Empty;
        private int _competitionPoints = 0;

        // Get and set team name.
        public string TeamName
        {
            get
            {
                return _teamName;
            }
            set
            {
                _teamName = value;
            }
        }

        // Get and set primary contact.
        public string PrimaryContact
        {
            get
            {
                return _primaryContact;
            }
            set
            {
                _primaryContact = value;
            }
        }

        // Get and set contact phone.
        public string ContactPhone
        {
            get
            {
                return _contactPhone;
            }
            set
            {
                _contactPhone = value;
            }
        }

        // Get and set contact email.
        public string ContactEmail
        {
            get
            {
                return _contactEmail;
            }
            set
            {
                _contactEmail = value;
            }
        }

        // Get and set competition points.
        public int CompetitionPoints
        {
            get
            {
                return _competitionPoints;
            }
            set
            {
                _competitionPoints = value;
            }
        }

        // Contructor to create empty object.
        public Team()
        {

        }

        // Contructor to create object with values.
        public Team(string teamName, string primaryContact, string contactPhone, string contactEmail,
        int competitionPoints)
        {
            _teamName = teamName;
            _primaryContact = primaryContact;
            _contactPhone = contactPhone;
            _contactEmail = contactEmail;
            _competitionPoints = competitionPoints;
        }
        #endregion
    }
}
