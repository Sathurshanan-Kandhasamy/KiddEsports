using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddEsports
{
    public class FileManager
    {
        #region Setup
        // This variable stores the file name to read from.
        string fileName = "TeamData.csv";
        #endregion

        #region Methods
        /// <summary>
        /// Writes team data to TeamData.csv file.
        /// </summary>
        /// <param name="teamData">Array of team objects.</param>
        public void WriteDataToFile(Team[] teams)
        {
            // Uses stream writer to write team details in the TeamData.csv file.
            using (var writer = new StreamWriter(fileName))
            {
                /* Interates through each team object in the array and writes to TeamData.csv file, 
                   with each team field value separated by a comma.
                */
                foreach (Team team in teams)
                {
                    writer.WriteLine($"{team.TeamName},{team.PrimaryContact},{team.ContactPhone},{team.ContactEmail}," +
                        $"{team.CompetitionPoints}");
                }
            }
        }

        /// <summary>
        /// Reads team data from TeamData.csv file.
        /// </summary>
        /// <returns>List of team objects or an empty list team list.</returns>
        public List<Team> ReadDataFromFile()
        {
            // An empty list to store team objects.
            List<Team> teams = new List<Team>();
            try
            {
                // Uses stream reader to read data from TeamData.csv file.
                using (var reader = new StreamReader(fileName))
                {
                    // Variable to store each line in the TeamData.csv file
                    string line;
                    // While line is not null or whitespace, continue reading TeamData.csv file.
                    while (String.IsNullOrWhiteSpace(line = reader.ReadLine()) == false)
                    {
                        // Split line at each comma and store each string in temp array.
                        string[] temp = line.Split(',');
                        // Create a new team object with temp array values.
                        Team team = new Team(temp[0], temp[1], temp[2], temp[3], int.Parse(temp[4]));
                        // Add team to teams list.
                        teams.Add(team);
                    }
                }

                // Return teams list.
                return teams;
            }
            catch
            {
                // If an error occured while reading data from TeamData.csv file, return an empty team list.
                return teams;
            }
        }
        #endregion
    }
}
