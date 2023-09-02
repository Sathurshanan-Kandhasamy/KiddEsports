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
        // Store CSV file name.
        string fileName = "TeamData.csv";
        #endregion

        #region Methods
        /// <summary>
        /// Writes team data to TeamData.csv file.
        /// </summary>
        /// <param name="teamData">Array of team objects.</param>
        public void WriteDataToFile(Team[] teams)
        {
            // Use stream writer to write team details in the TeamData.csv file.
            using (var writer = new StreamWriter(fileName))
            {
                // Interate through each team object in the array and write to TeamData.csv file, 
                // with each team field value separated by a comma.
                foreach (Team team in teams)
                {
                    writer.WriteLine($"{team.TeamName},{team.PrimaryContact},{team.ContactPhone},{team.ContactEmail}," +
                        $"{team.CompetitionPoints}");
                }
            }
        }

        // Read data from file.
        /// <summary>
        /// Reads team data from TeamData.csv file.
        /// </summary>
        /// <returns>Returns list of team objects or an empty list team list.</returns>
        public List<Team> ReadDataFromFile()
        {
            // Empty list to store team objects.
            List<Team> teams = new List<Team>();
            try
            {
                // Use stream reader to read from TeamData.csv file.
                using (var reader = new StreamReader(fileName))
                {
                    // Variable to store each line in the TeamData.csv file
                    string line;
                    // While line is not null or whitespace, continue reading TeamData.csv file.
                    while (String.IsNullOrWhiteSpace(line = reader.ReadLine()) == false)
                    {
                        // Split line at each comma. Store each string in temp array.
                        string[] temp = line.Split(',');
                        // Create new team with temp array values.
                        Team team = new Team(temp[0], temp[1], temp[2], temp[3], int.Parse(temp[4]));
                        // add team to teams
                        teams.Add(team);
                    }
                }
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
