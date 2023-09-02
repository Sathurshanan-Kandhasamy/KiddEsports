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
        // Teams is a list that stores Team objects.
        List<Team> teams = new List<Team>();

        // This variable stores contact email input field value from the form.
        string contactEmail = string.Empty;
        // This variable stores team name input field value from the form.
        string teamName = string.Empty;

        // Create a instance of file manager class.
        FileManager file = new FileManager();

        // Create a instance of form validation class.
        FormValidation formValidation = new FormValidation();

        // Create a instance of message class.
        Message message = new Message();

        // Main window contructor.
        public MainWindow()
        {
            // Initialize component.
            InitializeComponent();

            // Read team data from TeamData.CSV file and store them in teams variable.
            teams = file.ReadDataFromFile();

            // Call update table method to update the table.
            UpdateTable();
        }
        #endregion

        #region Events
        // Add button click event handler.
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            // Store all form fields values in an array.
            string[] formFields = new string[5] { txtTeamName.Text, txtPrimaryContact.Text,
            txtContactPhone.Text, txtContactEmail.Text, txtCompetitionPoints.Text };

            // If any of the form fields are empty, show an error message.
            if (formValidation.IsAllFieldsFilled(formFields) == false)
            {
                message.ShowErrorMessage("Fill out all the fields on the form.");
                return;
            }
            // If team name already exist in the TeamData.CSV file, show an error message.
            if (formValidation.IsTeamAlreadyExist(txtTeamName.Text, teams))
            {
                message.ShowErrorMessage("Team name already exist.");
                return;
            }
            // If entered phone number is not valid, show an error message.
            if (!formValidation.IsValidPhoneNumber(txtContactPhone.Text))
            {
                message.ShowErrorMessage("Enter a local phone number without spaces and country code.");
                return;
            }
            // If entered email is not valid, show an error message. 
            if (!formValidation.IsValidEmail(txtContactEmail.Text))
            {
                message.ShowErrorMessage("Enter a valid email.");
                return;
            }
            // If email already exist in the TeamData.CSV file, show an error message.
            if (formValidation.IsEmailAlreadyExist(txtContactEmail.Text, teams))
            {
                message.ShowErrorMessage("Email already exist.");
                return;
            }
            // If entered competition points is not a positive number, show an error message.
            if (!formValidation.IsPositiveNumber(txtCompetitionPoints.Text))
            {
                message.ShowErrorMessage("Competition points must be a positive number.");
                return;
            }

            // If all form fields values passed the validations.
            // Create a new team with Team class.
            Team newTeam = new Team();
            newTeam.TeamName = txtTeamName.Text;
            newTeam.PrimaryContact = txtPrimaryContact.Text;
            newTeam.ContactPhone = txtContactPhone.Text;
            newTeam.ContactEmail = txtContactEmail.Text;
            // Convert competition points which is a string to a number.
            newTeam.CompetitionPoints = int.Parse(txtCompetitionPoints.Text);

            // Add newly created team to teams list
            teams.Add(newTeam);
            // Convert teams list to an array and save it to TeamData.CSV file.
            file.WriteDataToFile(teams.ToArray());

            // Update the table to show the newly created team with other teams in the table.
            UpdateTable();

            // Clear all form fields values.
            SetFormFieldsEmpty();

            // Set the cursor on the team name input field in the form.
            txtTeamName.Focus();
        }

        // Update button click event handler.
        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            // Store all form fields values in an array.
            string[] formFields = new string[5] { txtTeamName.Text, txtPrimaryContact.Text, txtContactPhone.Text,
            txtContactEmail.Text, txtCompetitionPoints.Text };

            // If any of the form fields are empty, show an error message.
            if (formValidation.IsAllFieldsFilled(formFields) == false)
            {
                message.ShowErrorMessage("Fill out all the fields on the form.");
                return;
            }
            // If entered team name already exist in the TeamData.CSV file, show an error message.
            if (!txtTeamName.Text.Equals(teamName))
            {
                if (formValidation.IsTeamAlreadyExist(txtTeamName.Text, teams))
                {
                    message.ShowErrorMessage("Team name already exist.");
                    return;
                }
            }
            // If entered phone number is not valid, show an error message.
            if (!formValidation.IsValidPhoneNumber(txtContactPhone.Text))
            {
                message.ShowErrorMessage("Enter a local phone number wihout spaces and country code.");
                return;
            }
            // If entered email is not valid, show an error message. 
            if (!formValidation.IsValidEmail(txtContactEmail.Text))
            {
                message.ShowErrorMessage("Enter a valid email.");
                return;
            }
            // If entered email already exist in the TeamData.CSV file, show an error message.
            if (!txtContactEmail.Text.Equals(contactEmail))
            {
                if (formValidation.IsEmailAlreadyExist(txtContactEmail.Text, teams))
                {
                    message.ShowErrorMessage("Email already exist.");
                    return;
                }
            }
            // If entered competition points is not a positive number, show an error message.
            if (!formValidation.IsPositiveNumber(txtCompetitionPoints.Text))
            {
                message.ShowErrorMessage("Competition points must be a positive number.");
                return;
            }

            
             // Go through each team in the teams list. 
             // if entered team name exist in the teams list, set teamName variable to found team's name.
             // Then break the loop.
            foreach (Team team in teams)
            {
                if (team.TeamName.Equals(teamName))
                {
                    team.TeamName = txtTeamName.Text;
                    team.PrimaryContact = txtPrimaryContact.Text;
                    team.ContactPhone = txtContactPhone.Text;
                    team.ContactEmail = txtContactEmail.Text;
                    team.CompetitionPoints = int.Parse(txtCompetitionPoints.Text);
                    teamName = team.TeamName;
                    break;
                }
            }

            // Convert teams list to an array save it to TeamData.CSV file.
            file.WriteDataToFile(teams.ToArray());

            // Update the table.
            UpdateTable();
        }

        // Delete button click event handler.
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            // When delete button clicked, show an warning message box with YES or NO choice.
            // if YES is clicked.
            if (message.ShowWarningMessageYes("Do you want to permanently delete this team?"))
            {
                // Find the position of the team name in the teams list.
                int index = teams.FindIndex(team => team.TeamName.Equals(teamName));
                // If team name exist in the team list, remove team and update TeamData.CSV file.
                // Set contact email and team name input fields empty and update table.
                // If team not found in the teams list, show an error message.
                if (index > -1)
                {
                    teams.RemoveAt(index);
                    file.WriteDataToFile(teams.ToArray());
                    contactEmail = string.Empty;
                    teamName = string.Empty;
                    UpdateTable();
                }
                else
                {
                    message.ShowErrorMessage("Team does not exist.");
                }
            }

            // Clear all form fields.
            ClearFormFields();
        }

        // Clear button click event handler.
        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            // Clear all form fields.
            ClearFormFields();
        }

        // Table row double click event handler.
        private void TableRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the team object from table and store it in the team variable.
            var row = sender as DataGridRow;
            var team = row.DataContext as Team;

            // if team variable value is not empty.
            if (team != null)
            {
                // Set contactEmail and teamName variables values.
                contactEmail = team.ContactEmail;
                teamName = team.TeamName;

                // Set form fields values.
                txtTeamName.Text = team.TeamName;
                txtPrimaryContact.Text = team.PrimaryContact;
                txtContactPhone.Text = team.ContactPhone;
                txtContactEmail.Text = team.ContactEmail;
                txtCompetitionPoints.Text = team.CompetitionPoints.ToString();

                // Disable add button
                btnAdd.IsEnabled = false;
                // Enable update and delete buttons.
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
        }

        // Exit button click event handler.
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            // Close the GUI application.
            Close();
        }
        #endregion

        #region Helpers        
        // Clear form fields.
        private void ClearFormFields()
        {
            // Set form fields empty.
            SetFormFieldsEmpty();

            // Enable add button
            btnAdd.IsEnabled = true;
            // Disable update and delete buttons.
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        // Update table.
        private void UpdateTable()
        {
            // Set table source to teams list.
            table.ItemsSource = teams;
            // Refresh the table.
            table.Items.Refresh();
        }

        // Set form fields empty.
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
