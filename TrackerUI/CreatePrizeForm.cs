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
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequester callingForm;

        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void CreatePrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNumberValue.Text,
                    placeNameValue.Text,
                    prizeAmountValue.Text,
                    prizePercentageValue.Text);

                GlobalConfig.Source.CreatePrize(model);

                callingForm.PrizeComplete(model);

                this.Close();

                // Clear form values
                placeNameValue.Text = String.Empty;
                placeNumberValue.Text = string.Empty;
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";

                // Reset focus to first field in form
                placeNumberValue.Focus();
            }
            else
            {
                MessageBox.Show("This form has invalid information. Please check it and try again.");
            }
        }

        private void PrizeAmountValue_Leave(object sender, EventArgs e)
        {
            if (prizeAmountValue.Text.Trim().Length == 0)
            {
                prizeAmountValue.Text = "0";
            }
        }

        private void PrizePercentageValue_Leave(object sender, EventArgs e)
        {
            if (prizePercentageValue.Text.Trim().Length == 0)
            {
                prizePercentageValue.Text = "0";
            }
        }

        private bool ValidateForm()
        {
            bool dataValid = true;

            int placeNumber = 0;
            bool placeNumberValid = int.TryParse(placeNumberValue.Text, out placeNumber);

            if (!placeNumberValid)
            {
                dataValid = false;
            }

            if (placeNumber < 1)
            {
                dataValid = false;
            }

            if (placeNameValue.Text.Trim().Length == 0)
            {
                dataValid = false;
            }

            decimal prizeAmount = 0;
            double prizePercentage = 0;

            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);
            bool prizePercentageValid = double.TryParse(prizePercentageValue.Text, out prizePercentage);

            if (!prizeAmountValid || !prizePercentageValid)
            {
                dataValid = false;
            }

            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                dataValid = false;
            }

            if (prizePercentage < 0 || prizePercentage > 100)
            {
                dataValid = false;
            }

            if (prizeAmount > 0 && prizePercentage > 0)
            {
                dataValid = false;
            }

            return dataValid;
        }
    }
}
