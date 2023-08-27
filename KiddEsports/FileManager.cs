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
        string fileName = "TeamData.csv";
        #endregion

        #region Methods
        // Write data to file.
        public void WriteDataToFile(Team[] teamData)
        {
            using (var writer = new StreamWriter(fileName))
            {
                foreach (Team team in teamData)
                {
                    writer.WriteLine($"{team.TeamName},{team.PrimaryContact},{team.ContactPhone},{team.ContactEmail}," +
                        $"{team.CompetitionPoints}");
                }
            }
        }

        // Read data from file.
        public List<Team> ReadDataFromFile()
        {
            List<Team> teamList = new List<Team>();
            try
            {
                using (var reader = new StreamReader(fileName))
                {
                    string line;
                    while (String.IsNullOrWhiteSpace(line = reader.ReadLine()) == false)
                    {
                        string[] temp = line.Split(',');
                        Team details = new Team(temp[0], temp[1], temp[2], temp[3], int.Parse(temp[4]));
                        teamList.Add(details);
                    }
                }
                return teamList;
            }
            catch
            {
                return teamList;
            }
        }
        #endregion
    }
}
