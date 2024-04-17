using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void loginbtn_Click(object sender, EventArgs e)
        {
            string email = emailTextbox.Text.Trim();
            string password = pwTextbox.Text.Trim();
            string role = ChooseUser.SelectedValue;
            Session["emailId"] = emailTextbox.Text; 
            // Check for empty fields or invalid role
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || role == "Select")
            {
                DisplayErrorMessage("Please select a valid role.");
                return;
            }

            // Check the credentials against the database
            if (role == "Admin")
            {
                if (ValidateAdmin(email, password))
                {
                    // Redirect to the dashboard or home page upon successful login
                    Response.Redirect("../Admin/Dashboard.aspx");
                }
                else
                {
                    DisplayErrorMessage("Invalid credentials. Please try again.");
                }
                //DisplayErrorMessage("Invalid role selected.");
            }
            else if (role == "User")
            {
                if (ValidateUser(email, password))
                {
                    // Redirect to the dashboard or home page upon successful login
                    Response.Redirect("../User/Dashboard.aspx");
                }
                else
                {
                    DisplayErrorMessage("Invalid credentials. Please try again.");
                }
            }
            else
            {
                DisplayErrorMessage("Invalid role selected.");
            }
        }

        private void DisplayErrorMessage(string message)
        {
            // Display an error message on the web page
            errorLabel.Text = message;
        }
        private bool ValidateUser(string email, string password)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to prevent SQL injection
                string query = "SELECT COUNT(*) FROM [dbo].[UserRegister] WHERE E_mail = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);


                    int count = (int)command.ExecuteScalar();

                    // If count is greater than 0, the user is valid
                    return count > 0;
                }
            }
        }
        private bool ValidateAdmin(string email, string password)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to prevent SQL injection
                string query = "SELECT COUNT(*) FROM [dbo].[AdminRegister] WHERE E_mail = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);


                    int count = (int)command.ExecuteScalar();

                    // If count is greater than 0, the user is valid
                    return count > 0;
                }
            }
        }
    }
}