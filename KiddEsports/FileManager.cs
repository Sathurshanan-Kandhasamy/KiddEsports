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
        // Stores the name of the file to read teams data from.
        string fileName = "TeamData.csv";
        #endregion

        #region Methods
        /// <summary>
        /// Writes team data to TeamData.csv file.
        /// </summary>
        /// <param name="teamData">An array of team objects.</param>
        public void WriteDataToFile(Team[] teams)
        {
            // Uses stream writer to write team details in the TeamData.csv file.
            using (var writer = new StreamWriter(fileName))
            {
                // Interates through each team object in the teams array.
                foreach (Team team in teams)
                {
                       // Writes team data in TeamData.csv file, with each team object field value separated by a comma.
                       writer.WriteLine($"{team.TeamName},{team.PrimaryContact},{team.ContactPhone},{team.ContactEmail}," +
                        $"{team.CompetitionPoints}");
                }
            }
        }

        /// <summary>
        /// Reads team data from TeamData.csv file.
        /// </summary>
        /// <returns>A list of team objects or an empty list team list.</returns>
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
                        // Splits each line at comma and stores each string in temp array.
                        string[] temp = line.Split(',');
                        // Creates a new team object with temp array values.
                        Team team = new Team(temp[0], temp[1], temp[2], temp[3], int.Parse(temp[4]));
                        // Adds the team to teams list.
                        teams.Add(team);
                    }
                }

                // Returns teams list.
                return teams;
            }
            catch
            {
                // If an error occured while reading data from TeamData.csv file, returns an empty teams list.
                return teams;
            }
        }
        #endregion
    }
}
