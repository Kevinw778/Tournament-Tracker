using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        // TODO - Create "SelectTeamMember" and "TeamMembers"Updated event & handler(s)
        private List<PersonModel> availableTeamMembers = GlobalConfig.Source.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            WireUpLists();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Tim", LastName = "Corey" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Jim", LastName = "Jones" });
        }

        private void WireUpLists()
        {
            // TODO - Fix this weird refresh of data-binding issue
            selectTeamMemberDropdown.DataSource = null;

            selectTeamMemberDropdown.DataSource = availableTeamMembers;
            selectTeamMemberDropdown.DisplayMember = "FullName";

            teamMembersListbox.DataSource = null;

            teamMembersListbox.DataSource = selectedTeamMembers;
            teamMembersListbox.DisplayMember = "FullName";
        }

        private void CreateMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.PhoneNumber = phoneValue.Text;

                GlobalConfig.Source.CreatePerson(p);

                firstNameValue.Text = String.Empty;
                lastNameValue.Text = String.Empty;
                emailValue.Text = String.Empty;
                phoneValue.Text = String.Empty;

                availableTeamMembers.Add(p);
                WireUpLists();
            }
            else
            {
                MessageBox.Show("You need to enter a value for each field before submitting");
            }
        }

        private bool ValidateForm()
        {
            if (firstNameValue.Text.Trim().Length == 0)
            {
                return false;
            }

            if (lastNameValue.Text.Trim().Length == 0)
            {
                return false;
            }

            if (emailValue.Text.Trim().Length == 0)
            {
                return false;
            }

            if (phoneValue.Text.Trim().Length == 0)
            {
                return false;
            }
            return true;
        }

        private void AddTeamMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropdown.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists();
            }
        }

        private void RemoveSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListbox.SelectedItem;

            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }
        }

        private void CreateTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel
            {
                TeamName = teamNameValue.Text,
                TeamMembers = selectedTeamMembers
            };

            GlobalConfig.Source.CreateTeam(t);

            callingForm.TeamComplete(t);

            this.Close();
        }
    }
}
