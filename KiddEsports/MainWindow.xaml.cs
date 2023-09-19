using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KiddEsports
{
    public partial class MainWindow : Window
    {
        #region Setup
        // Stores list of team objects.
        List<Team> teams = new List<Team>();

        // Stores entered contactEmail value.
        string contactEmail = string.Empty;
        // Stores entered teamName value.
        string teamName = string.Empty;

        // Creates an instance of FileManager class.
        FileManager file = new FileManager();

        // Main window constructor.
        public MainWindow()
        {
            // Initializes component.
            InitializeComponent();

            // Reads team data from TeamData.csv file and stores them in teams variable.
            teams = file.ReadDataFromFile();

            // Updates the teams table.
            UpdateTable();
        }
        #endregion

        #region Events
        // Handles addBtn click event.
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            // Stores all form input fields values in an array.
            string[] formFields = new string[5] { txtTeamName.Text, txtPrimaryContact.Text,
            txtContactPhone.Text, txtContactEmail.Text, txtCompetitionPoints.Text };

            // If any of the form input fields are empty.
            if (FormValidation.IsAllFieldsFilled(formFields) == false)
            {
                // Shows an error messsage.
                Message.ShowErrorMessage("Fill out all the fields on the form.");
                return;
            }
            // If the entered teamName already exists in the TeamData.csv file.
            if (FormValidation.IsTeamAlreadyExist(txtTeamName.Text, teams))
            {
                // Shows an error message.
                Message.ShowErrorMessage("Team name already exist.");
                return;
            }
            // If the entered contactPhone is invalid.
            if (!FormValidation.IsValidPhoneNumber(txtContactPhone.Text))
            {
                // Shows an error message.
                Message.ShowErrorMessage("Enter a local phone number without spaces and country code.");
                return;
            }
            // If the entered contactEmail is invalid.
            if (!FormValidation.IsValidEmail(txtContactEmail.Text))
            {
                // Shows an error message. 
                Message.ShowErrorMessage("Enter a valid email.");
                return;
            }
            // If the entered contactEmail already exists in the TeamData.csv file.
            if (FormValidation.IsEmailAlreadyExist(txtContactEmail.Text, teams))
            {
                // Shows an error message.
                Message.ShowErrorMessage("Email already exist.");
                return;
            }
            // If the entered competitionPoints is not a positive number.
            if (!FormValidation.IsPositiveNumber(txtCompetitionPoints.Text))
            {
                // Shows an error message.
                Message.ShowErrorMessage("Competition points must be a positive number.");
                return;
            }

            // If all the form input field values are valid.
            // Creates a new team with Team class.
            Team newTeam = new Team();
            newTeam.TeamName = txtTeamName.Text;
            newTeam.PrimaryContact = txtPrimaryContact.Text;
            newTeam.ContactPhone = txtContactPhone.Text;
            newTeam.ContactEmail = txtContactEmail.Text;
            // Parses competitionPoints to number.
            newTeam.CompetitionPoints = int.Parse(txtCompetitionPoints.Text);

            // Adds the new team to teams list.
            teams.Add(newTeam);
            // Converts teams list to an array and saves it to the TeamData.csv file.
            file.WriteDataToFile(teams.ToArray());

            // Updates the teams table to show the new team.
            UpdateTable();

            // Clears all form input fields.
            SetFormFieldsEmpty();

            // Sets the cursor on the teamName input field in the form.
            txtTeamName.Focus();
        }

        // Handles btnUpdate click event.
        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            // Stores all form input fields values in an array.
            string[] formFields = new string[5] { txtTeamName.Text, txtPrimaryContact.Text, txtContactPhone.Text,
            txtContactEmail.Text, txtCompetitionPoints.Text };

            // If any of the form input fields are empty.
            if (FormValidation.IsAllFieldsFilled(formFields) == false)
            {
                // Shows an error message.
                Message.ShowErrorMessage("Fill out all the fields on the form.");
                return;
            }
            // If the entered teamName already exists in the TeamData.csv file.
            if (!txtTeamName.Text.Equals(teamName))
            {
                if (FormValidation.IsTeamAlreadyExist(txtTeamName.Text, teams))
                {
                    // Shows an error message.
                    Message.ShowErrorMessage("Team name already exist.");
                    return;
                }
            }
            // If the entered contactPhone is invalid.
            if (!FormValidation.IsValidPhoneNumber(txtContactPhone.Text))
            {
                // Shows an error message.
                Message.ShowErrorMessage("Enter a local phone number wihout spaces and country code.");
                return;
            }
            // If the entered contactEmail is invalid.
            if (!FormValidation.IsValidEmail(txtContactEmail.Text))
            {
                // Shows an error message.
                Message.ShowErrorMessage("Enter a valid email.");
                return;
            }
            // If the entered contactEmail already exists in the TeamData.csv file.
            if (!txtContactEmail.Text.Equals(contactEmail))
            {
                if (FormValidation.IsEmailAlreadyExist(txtContactEmail.Text, teams))
                {
                    // Shows an error messsage.
                    Message.ShowErrorMessage("Email already exist.");
                    return;
                }
            }
            // If the entered competitionPoints is not a positive number.
            if (!FormValidation.IsPositiveNumber(txtCompetitionPoints.Text))
            {
                // Shows an error message.
                Message.ShowErrorMessage("Competition points must be a positive number.");
                return;
            }

             // Interates through each team in the teams list.
            foreach (Team team in teams)
            {
                // If the team's teamName is entered teamName value.
                if (team.TeamName.Equals(teamName))
                {
                    // Sets form input fields values to this team's field values.
                    team.TeamName = txtTeamName.Text;
                    team.PrimaryContact = txtPrimaryContact.Text;
                    team.ContactPhone = txtContactPhone.Text;
                    team.ContactEmail = txtContactEmail.Text;
                    // Parses conpetitionPoints values to a number.
                    team.CompetitionPoints = int.Parse(txtCompetitionPoints.Text);
                    // Sets teamName variable to team's teamName.
                    teamName = team.TeamName;
                    // Breaks the loop.
                    break;
                }
            }

            // Converts teams list to an array and saves it to TeamData.csv file.
            file.WriteDataToFile(teams.ToArray());

            // Updates the teams table.
            UpdateTable();
        }

        // Handles btnDelete click event.
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            // Shows a warning message with YES and NO buttons and if YES button is clicked.
            if (Message.ShowWarningMessageYes("Do you want to permanently delete this team?"))
            {
                // Finds the position of the teamName in the teams list.
                int index = teams.FindIndex(team => team.TeamName.Equals(teamName));
                // If the teamName exists in the teams list.
                if (index > -1)
                {
                    // Removes a team at specified index in the teams list.
                    teams.RemoveAt(index);
                    // Converts teams list to an array and saves it to TeamData.csv file.
                    file.WriteDataToFile(teams.ToArray());
                    // Sets contactEmail variable to an empty string.
                    contactEmail = string.Empty;
                    // Sets contactEmail variable to an empty string.
                    teamName = string.Empty;
                    // Updates the teams table.
                    UpdateTable();
                }
                else
                {
                    // Otherwise shows an error messsage.
                    Message.ShowErrorMessage("Team does not exist.");
                }
            }

            // Clears all form input fields.
            ClearFormFields();
        }

        // Handles btnClear click event.
        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            // Clears all form input fields.
            ClearFormFields();
        }

        // Handles table row double click event.
        private void TableRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Gets the team object from table and stores it in the team variable.
            var row = sender as DataGridRow;
            var team = row.DataContext as Team;

            // if the team variable value is not null.
            if (team != null)
            {
                // Sets contactEmail variable value.
                contactEmail = team.ContactEmail;
                // Sets teamName variable value.
                teamName = team.TeamName;
                
                // Sets form input fields values to team's field values.
                txtTeamName.Text = team.TeamName;
                txtPrimaryContact.Text = team.PrimaryContact;
                txtContactPhone.Text = team.ContactPhone;
                txtContactEmail.Text = team.ContactEmail;
                // Converts competitionPoints value to string.
                txtCompetitionPoints.Text = team.CompetitionPoints.ToString();

                // Disables addBtn.
                btnAdd.IsEnabled = false;
                // Enables updateBtn.
                btnUpdate.IsEnabled = true;
                // Enables deleteBtn.
                btnDelete.IsEnabled = true;
            }
        }

        // Handles exit button click event.
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            // Closes the GUI.
            Close();
        }
        #endregion

        #region Helpers        
        // Clears form input fields.
        private void ClearFormFields()
        {
            // Sets all form input fields value to an empty string.
            SetFormFieldsEmpty();

            // Enables addBtn.
            btnAdd.IsEnabled = true;
            // Disables updateBtn.
            btnUpdate.IsEnabled = false;
            // Disables deleteBtn.
            btnDelete.IsEnabled = false;
        }

        // Updates teams table.
        private void UpdateTable()
        {
            // Sets table source to teams list.
            table.ItemsSource = teams;
            // Refreshs the teams table.
            table.Items.Refresh();
        }

        // Sets all form input field values to an empty string.
        private void SetFormFieldsEmpty()
        {
            txtTeamName.Text = string.Empty;
            txtPrimaryContact.Text = string.Empty;
            txtContactPhone.Text = string.Empty;
            txtContactEmail.Text = string.Empty;
            txtCompetitionPoints.Text = string.Empty;
        }
        #endregion
    }
}
