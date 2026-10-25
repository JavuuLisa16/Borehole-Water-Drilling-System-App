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
    public partial class ClerkReviewRequestForm : Form
    {
        public ClerkReviewRequestForm()
        {
            InitializeComponent();
            SetupReviewGrid();
            LoadReviews();
            AppTheme.ApplyToForm(this);
        }
        private void LoadReviews()
        {
            dgvReviews.Rows.Clear();

            List<Review> reviews =
                ReviewSqlService.GetAllReviews();

            foreach (Review review in reviews)
            {
                int rowIndex = dgvReviews.Rows.Add();

                DataGridViewRow row =
                    dgvReviews.Rows[rowIndex];

                row.Cells["ReviewID"].Value =
                    review.ReviewID;

                row.Cells["RequestID"].Value =
                    review.RequestID;

                row.Cells["ClientID"].Value =
                    review.ClientID;

                row.Cells["ClientName"].Value =
                    review.ClientName;

                row.Cells["Description"].Value =
                    review.Description;

                row.Cells["Rating"].Value =
                    review.Rating;

                row.Cells["ReviewDate"].Value =
                    review.ReviewDate.ToShortDateString();

                row.Cells["Status"].Value =
                    review.Status;

                row.Cells["ClerkResponse"].Value =
                    review.ClerkResponse;

                row.Cells["ResponseDate"].Value =
                    review.ResponseDate.HasValue
                        ? review.ResponseDate.Value.ToShortDateString()
                        : "";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRespond_Click(object sender, EventArgs e)
        {
            if (dgvReviews.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a review.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtClerkResponse.Text))
            {
                MessageBox.Show("Please enter a response.");
                txtClerkResponse.Focus();
                return;
            }

            string reviewID =
                dgvReviews.SelectedRows[0]
                .Cells["ReviewID"]
                .Value?.ToString();

            Review review =
      ReviewSqlService.GetReviewByID(
          reviewID
      );

            if (review == null)
            {
                MessageBox.Show("Review could not be found.");
                return;
            }

            review.ClerkResponse = txtClerkResponse.Text.Trim();
            review.ResponseDate = DateTime.Today;
            review.Status = "Responded";

            LoadReviews();

            txtClerkResponse.Clear();

            MessageBox.Show(
                "Response submitted successfully.",
                "Review Response"
            );

        }

        private void SetupReviewGrid()
        {
            dgvReviews.AutoGenerateColumns = false;
            dgvReviews.Columns.Clear();

            dgvReviews.Columns.Add("ReviewID", "Review ID");
            dgvReviews.Columns.Add("RequestID", "Request ID");
            dgvReviews.Columns.Add("ClientID", "Client ID");
            dgvReviews.Columns.Add("ClientName", "Client Name");
            dgvReviews.Columns.Add("Description", "Description");
            dgvReviews.Columns.Add("Rating", "Rating");
            dgvReviews.Columns.Add("ReviewDate", "Review Date");
            dgvReviews.Columns.Add("Status", "Status");
            dgvReviews.Columns.Add("ClerkResponse", "Clerk Response");
            dgvReviews.Columns.Add("ResponseDate", "Response Date");

            dgvReviews.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgvReviews.AllowUserToAddRows = false;
        }
    }
}