using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ReviewsForm : Form
    {
        private string requestID;

        // For "My Reviews" button
        public ReviewsForm()
        {
            InitializeComponent();

            AppTheme.ApplyToForm(this);
            StyleReviewsForm();
        }

        // For reviewing a specific request
        public ReviewsForm(string requestID)
        {
            InitializeComponent();

            this.requestID = requestID;

            AppTheme.ApplyToForm(this);
            StyleReviewsForm();
        }

        private void StyleReviewsForm()
        {
            this.BackColor = AppTheme.Background;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmitReview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(requestID))
            {
                MessageBox.Show(
                    "Please select a completed service before submitting a review."
                );

                return;
            }

            if (string.IsNullOrWhiteSpace(txtReview.Text))
            {
                MessageBox.Show(
                    "Please enter your review."
                );

                txtReview.Focus();
                return;
            }

            if (cmbRating.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select a rating."
                );

                cmbRating.Focus();
                return;
            }

            if (ClientProfileStore.CurrentClient == null)
            {
                MessageBox.Show(
                    "Client profile could not be found."
                );

                return;
            }

            string clientID =
                ClientProfileStore.CurrentClient.ClientID;

            if (string.IsNullOrWhiteSpace(clientID))
            {
                MessageBox.Show(
                    "Client ID could not be found."
                );

                return;
            }

            if (ReviewSqlService.HasReview(requestID))
            {
                MessageBox.Show(
                    "You have already submitted a review for this service.",
                    "Review Already Submitted"
                );

                return;
            }

            int rating =
                int.Parse(
                    cmbRating.SelectedItem.ToString()
                );

            try
            {
                Review review =
                    ReviewSqlService.CreateReview(
                        requestID,
                        clientID,
                        txtReview.Text.Trim(),
                        rating
                    );

                MessageBox.Show(
                    "Your review has been submitted successfully.\n\n" +
                    "Review ID: " + review.ReviewID + "\n" +
                    "Request ID: " + review.RequestID + "\n" +
                    "Rating: " + review.Rating + "/5",
                    "Review Submitted"
                );

                txtReview.Clear();
                cmbRating.SelectedIndex = -1;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The review could not be submitted.\n\n" +
                    ex.Message,
                    "Review Error"
                );
            }
        }
    }
}