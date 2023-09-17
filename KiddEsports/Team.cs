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
        // Fields.
        private string _teamName = string.Empty;
        private string _primaryContact = string.Empty;
        private string _contactPhone = string.Empty;
        private string _contactEmail = string.Empty;
        private int _competitionPoints = 0;

        // Gets and sets teamName.
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

        // Gets and sets primaryContact.
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

        // Gets and sets contactPhone.
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

        // Gets and sets contactmail.
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
        
        // Gets and sets competitionPoints.
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

        // Constructor to create an empty team object.
        public Team()
        {

        }

        // Constructor to create a team object with values.
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
