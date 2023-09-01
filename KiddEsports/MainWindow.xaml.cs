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
        List<Team> teams = new List<Team>();

        string contactEmail = string.Empty;
        string teamName = string.Empty;

        FileManager file = new FileManager();

        FormValidation formValidation = new FormValidation();

        Message message = new Message();

        public MainWindow()
        {
            InitializeComponent();

            teams = file.ReadDataFromFile();

            UpdateTable();
        }
        #endregion

        #region Events
        // Add button click, add team.
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            string[] formFields = new string[5] { txtTeamName.Text, txtPrimaryContact.Text,
            txtContactPhone.Text, txtContactEmail.Text, txtCompetitionPoints.Text };

            if (formValidation.IsAllFieldsFilled(formFields) == false)
            {
                message.ShowErrorMessage("Fill out all the fields on the form.");
                return;
            }
            if (formValidation.IsTeamAlreadyExist(txtTeamName.Text, teams))
            {
                message.ShowErrorMessage("Team name already exist.");
                return;
            }
            if (!formValidation.IsValidPhoneNumber(txtContactPhone.Text))
            {
                message.ShowErrorMessage("Enter a local phone number without spaces and country code.");
                return;
            }
            if (!formValidation.IsValidEmail(txtContactEmail.Text))
            {
                message.ShowErrorMessage("Enter a valid email.");
                return;
            }
            if (formValidation.IsEmailAlreadyExist(txtContactEmail.Text, teams))
            {
                message.ShowErrorMessage("Email already exist.");
                return;
            }
            if (!formValidation.IsPositiveNumber(txtCompetitionPoints.Text))
            {
                message.ShowErrorMessage("Competition points must be a positive number.");
                return;
            }

            Team newTeam = new Team();
            newTeam.TeamName = txtTeamName.Text;
            newTeam.PrimaryContact = txtPrimaryContact.Text;
            newTeam.ContactPhone = txtContactPhone.Text;
            newTeam.ContactEmail = txtContactEmail.Text;
            newTeam.CompetitionPoints = int.Parse(txtCompetitionPoints.Text);

            teams.Add(newTeam);
            file.WriteDataToFile(teams.ToArray());

            UpdateTable();

            SetFormFieldsEmpty();

            txtTeamName.Focus();
        }

        // Update button click, update team.
        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            string[] formFields = new string[5] { txtTeamName.Text, txtPrimaryContact.Text, txtContactPhone.Text,
            txtContactEmail.Text, txtCompetitionPoints.Text };

            if (formValidation.IsAllFieldsFilled(formFields) == false)
            {
                message.ShowErrorMessage("Fill out all the fields on the form.");
                return;
            }
            if (!txtTeamName.Text.Equals(teamName))
            {
                if (formValidation.IsTeamAlreadyExist(txtTeamName.Text, teams))
                {
                    message.ShowErrorMessage("Team name already exist.");
                    return;
                }
            }
            if (!formValidation.IsValidPhoneNumber(txtContactPhone.Text))
            {
                message.ShowErrorMessage("Enter a local phone number wihout spaces and country code.");
                return;
            }
            if (!formValidation.IsValidEmail(txtContactEmail.Text))
            {
                message.ShowErrorMessage("Enter a valid email.");
                return;
            }
            if (!txtContactEmail.Text.Equals(contactEmail))
            {
                if (formValidation.IsEmailAlreadyExist(txtContactEmail.Text, teams))
                {
                    message.ShowErrorMessage("Email already exist.");
                    return;
                }
            }
            if (!formValidation.IsPositiveNumber(txtCompetitionPoints.Text))
            {
                message.ShowErrorMessage("Competition points must be a positive number.");
                return;
            }

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

            file.WriteDataToFile(teams.ToArray());

            UpdateTable();
        }

        // Delete button click, delete team.
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (message.ShowWarningMessageYes("Do you want to permanently delete this team?"))
            {
                int index = teams.FindIndex(team => team.TeamName.Equals(teamName));
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

            ClearFormFields();
        }

        // Clear button click.
        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            ClearFormFields();
        }

        // Table row double click, allow edit and delete actions.
        private void TableRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            var team = row.DataContext as Team;

            if (team != null)
            {
                contactEmail = team.ContactEmail;
                teamName = team.TeamName;

                txtTeamName.Text = team.TeamName;
                txtPrimaryContact.Text = team.PrimaryContact;
                txtContactPhone.Text = team.ContactPhone;
                txtContactEmail.Text = team.ContactEmail;
                txtCompetitionPoints.Text = team.CompetitionPoints.ToString();

                btnAdd.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
        }

        // Exit button click, close application.
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Helpers        
        // Clear form fields.
        private void ClearFormFields()
        {
            SetFormFieldsEmpty();

            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        // Update table.
        private void UpdateTable()
        {
            table.ItemsSource = teams;
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
