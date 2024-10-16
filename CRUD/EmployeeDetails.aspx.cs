using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace CRUD
{
    public partial class EmployeeDetails : System.Web.UI.Page
    {
        // Connection string to connect to the database
        private string connectionString = ConfigurationManager.ConnectionStrings["CollegeDbConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the page is being loaded for the first time
            if (!IsPostBack)
            {
                BindGrid(); // Bind employee data to the GridView
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            // Check if the page is valid
            if (Page.IsValid)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        // Check if the email already exists
                        using (SqlCommand checkEmailCmd = new SqlCommand("SELECT COUNT(*) FROM Employee WHERE Email=@Email", con))
                        {
                            checkEmailCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            con.Open();
                            int emailCount = (int)checkEmailCmd.ExecuteScalar();

                            // If email exists, display feedback and exit
                            if (emailCount > 0)
                            {
                                lblFeedback.Text = "An employee with this email already exists.";
                                lblFeedback.Visible = true;
                                return; // Exit the method if email exists
                            }
                        }

                        // If email is unique, proceed with the insertion
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Employee (FirstName, LastName, DateOfBirth, Position, Email, Phone, Address) VALUES (@FirstName, @LastName, @DateOfBirth, @Position, @Email, @Phone, @Address)", con))
                        {
                            // Adding parameters to prevent SQL injection
                            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                            cmd.Parameters.AddWithValue("@DateOfBirth", txtDOB.Text);
                            cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);

                            // Execute the insert command
                            cmd.ExecuteNonQuery();
                        }

                        // Provide feedback to the user
                        lblFeedback.Text = "Employee added successfully!";
                        lblFeedback.Visible = true;
                        ClearFields(); // Clear input fields
                        BindGrid(); // Refresh the GridView
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exceptions specifically
                    lblFeedback.Text = "An error occurred while inserting the employee: " + sqlEx.Message;
                    lblFeedback.Visible = true;
                }
                catch (Exception ex)
                {
                    // Handle other unexpected exceptions
                    lblFeedback.Text = "An unexpected error occurred: " + ex.Message;
                    lblFeedback.Visible = true;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check if the page is valid
            if (Page.IsValid)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        // Update employee details
                        using (SqlCommand cmd = new SqlCommand("UPDATE Employee SET FirstName=@FirstName, LastName=@LastName, DateOfBirth=@DateOfBirth, Position=@Position, Email=@Email, Phone=@Phone, Address=@Address WHERE EmployeeID=@EmployeeID", con))
                        {
                            // Adding parameters to prevent SQL injection
                            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                            cmd.Parameters.AddWithValue("@DateOfBirth", txtDOB.Text);
                            cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                            cmd.Parameters.AddWithValue("@EmployeeID", gvEmployees.SelectedDataKey.Value);

                            con.Open(); // Open the connection
                            cmd.ExecuteNonQuery(); // Execute the update command
                        }

                        // Provide feedback to the user
                        lblFeedback.Text = "Employee updated successfully!";
                        lblFeedback.Visible = true;
                        ClearFields(); // Clear input fields
                        BindGrid(); // Refresh the GridView
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exceptions specifically
                    lblFeedback.Text = "An error occurred while updating the employee: " + sqlEx.Message;
                    lblFeedback.Visible = true;
                }
                catch (Exception ex)
                {
                    // Handle other unexpected exceptions
                    lblFeedback.Text = "An unexpected error occurred: " + ex.Message;
                    lblFeedback.Visible = true;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (gvEmployees.SelectedDataKey != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        // Delete selected employee
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE EmployeeID=@EmployeeID", con))
                        {
                            cmd.Parameters.AddWithValue("@EmployeeID", gvEmployees.SelectedDataKey.Value);
                            con.Open(); // Open the connection
                            cmd.ExecuteNonQuery(); // Execute the delete command
                        }

                        // Provide feedback to the user
                        lblFeedback.Text = "Employee deleted successfully!";
                        lblFeedback.Visible = true;
                        ClearFields(); // Clear input fields
                        BindGrid(); // Refresh the GridView
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL exceptions specifically
                    lblFeedback.Text = "An error occurred while deleting the employee: " + sqlEx.Message;
                    lblFeedback.Visible = true;
                }
                catch (Exception ex)
                {
                    // Handle other unexpected exceptions
                    lblFeedback.Text = "An unexpected error occurred: " + ex.Message;
                    lblFeedback.Visible = true;
                }
            }
            else
            {
                // Provide feedback when no row is selected
                lblFeedback.Text = "Please select an employee to delete.";
                lblFeedback.Visible = true;
            }
        }

        private void ClearFields()
        {
            // Clear all input fields
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private void BindGrid()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Retrieve employee data and bind to the GridView
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", con))
                    {
                        con.Open(); // Open the connection
                        SqlDataReader reader = cmd.ExecuteReader(); // Execute the query
                        gvEmployees.DataSource = reader; // Bind the reader to the GridView
                        gvEmployees.DataBind(); // Refresh the GridView
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL exceptions specifically
                lblFeedback.Text = "An error occurred while retrieving employees: " + sqlEx.Message;
                lblFeedback.Visible = true;
            }
            catch (Exception ex)
            {
                // Handle other unexpected exceptions
                lblFeedback.Text = "An unexpected error occurred: " + ex.Message;
                lblFeedback.Visible = true;
            }
        }

        protected void gvEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Retrieve the selected row
            GridViewRow selectedRow = gvEmployees.SelectedRow;

            // Get the value of the Date of Birth cell from the TemplateField
            Label lblDOB = (Label)selectedRow.FindControl("lblDOB"); // Change to the correct control type if needed

            string dobValue = lblDOB.Text; // Retrieve the Date of Birth value

            // Populate the form fields with the selected employee's details
            txtFirstName.Text = selectedRow.Cells[1].Text;
            txtLastName.Text = selectedRow.Cells[2].Text;

            // Use DateTime.TryParseExact to safely parse the date
            if (DateTime.TryParseExact(dobValue, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
            {
                // Set the text box to the correct format for HTML5 date input
                txtDOB.Text = dob.ToString("yyyy-MM-dd"); // Ensure date format
            }
            else
            {
                // Handle the case where the date format is not valid
                txtDOB.Text = string.Empty; // or display an error message
            }

            txtPosition.Text = selectedRow.Cells[4].Text;
            txtEmail.Text = selectedRow.Cells[5].Text;
            txtPhone.Text = selectedRow.Cells[6].Text;
            txtAddress.Text = selectedRow.Cells[7].Text;
        }

        protected void gvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Check if the current row is a data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assign the custom class to the row for styling
                e.Row.CssClass = "gridview-row"; // Add custom class for hover effect
            }
        }
    }
}