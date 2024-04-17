using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.User
{
    public partial class AddAhtsServices : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        string email = "";
        string userId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["emailId"] != null)
            {
                email = Session["emailId"].ToString();
            }
            else
            {

                string script = "swal({ title: 'Something Wents Wrong', text: 'Click Ok to Continue!', icon: 'warning' }).then(function() { window.location = '../Login/Login.aspx'; },1000);";
                ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);

                // Return from the server-side code
                return;

            }
            if (!IsPostBack)
            {
                ServiceDropDownList();
                GetUserId();
                DisplayUserName();
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlserviceName.Text) || string.IsNullOrEmpty(customernameTextbox.Text) || string.IsNullOrEmpty(mobilenumTextbox.Text) || string.IsNullOrEmpty(emailTextBox.Text) || string.IsNullOrEmpty(addressTextbox.Text) || string.IsNullOrEmpty(requirementTextbox.Text))
            {
                string script = "swal({ title: 'Please fill all the blanks!', text: 'All fields are mandatory! Click OK to Continue!', icon: 'warning' }).then(function() {  });";
                ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script, true);
            }
            else
            {
                string servicename = ddlserviceName.Text.Trim();
                string cust_name = customernameTextbox.Text;
                string mob = mobilenumTextbox.Text;
                string email = emailTextBox.Text;
                string address = addressTextbox.Text;
                string requirement = requirementTextbox.Text;
                GetUserId();
                try
                {
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        conn.Open();
                        //string userId = Request.QueryString["UserID"].ToString();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[tbl_Project_Report] (Project_Name, Customer_Name, Customer_Mobile, Customer_Email, Customer_Address, Project_Requirement, Project_File, User_Id) VALUES (@ServiceName, @Customer_Name, @Customer_Mob, @Customer_Email, @Customer_Address, @Project_Requirement, @Project_File, @UserId)", conn))
                        {
                            cmd.Parameters.AddWithValue("@ServiceName", servicename);
                            cmd.Parameters.AddWithValue("@Customer_Name", cust_name);
                            cmd.Parameters.AddWithValue("@Customer_Mob", mob);
                            cmd.Parameters.AddWithValue("@Customer_Email", email);
                            cmd.Parameters.AddWithValue("@Customer_Address", address);
                            cmd.Parameters.AddWithValue("@Project_Requirement", requirement);
                            cmd.Parameters.AddWithValue("@UserId", userId);

                            if (FileUpload1.HasFile)
                            {
                                string fileName = FileUpload1.FileName;
                                string fileExtension = Path.GetExtension(fileName);

                                // Generate a random filename using Guid
                                //string newFilename = Guid.NewGuid().ToString() + fileExtension;

                                // Alternatively, use an incrementally ordered name
                                string newFilename = GetIncrementalFilename(fileExtension);

                                if (fileExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                    fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase) ||
                                    fileExtension.Equals(".zip", StringComparison.OrdinalIgnoreCase) ||
                                    fileExtension.Equals(".rar", StringComparison.OrdinalIgnoreCase))
                                {
                                    string filePath = Server.MapPath("~/User/Projects/") + newFilename;

                                    FileUpload1.SaveAs(filePath);
                                    cmd.Parameters.AddWithValue("@Project_File", newFilename);
                                }
                                else
                                {
                                    string script1 = "swal({ title: 'Only jpg, Pdf, zip, and rar files are allowed.!', text: 'Only jpg, pdf, zip, and rar files are allowed. Click OK to Continue!', icon: 'warning' }).then(function() {  });";
                                    ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script1, true);
                                    return;
                                }
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Project_File", DBNull.Value);
                            }

                            cmd.ExecuteNonQuery();

                            ddlserviceName.Text = "";
                            customernameTextbox.Text = "";
                            mobilenumTextbox.Text = "";
                            emailTextBox.Text = "";
                            addressTextbox.Text = "";
                            requirementTextbox.Text = "";

                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "SuccessScript", "alert('Service C');", true);
                            string script2 = "swal({ title: 'Service Added!', text: 'Service Added Successfully! Click OK to Continue!', icon: 'success' }).then(function() {  });";
                            ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script2, true);

                            string redirectUrl = "ViewAhtsServices.aspx"; // Replace with your desired URL
                            string script = "setTimeout(function() { window.location.href = '" + redirectUrl + "'; }, 2000);";
                            ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script, true);

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log them, or display an error message
                    string script5 = "swal({ title: 'Service Addition Failed!', text: 'Service Addition Failed!', icon: 'warning' }).then(function() {  });";
                    ClientScript.RegisterStartupScript(GetType(), "SweetAlert", script5, true);
                    return;

                }
            }
        }

        private string GetIncrementalFilename(string fileExtension)
        {
            // You may need to modify this logic based on your requirements
            string cust_name = customernameTextbox.Text;
            string baseName = cust_name;
            int counter = 1;

            while (File.Exists(Server.MapPath("~/User/Projects/") + $"{baseName}{counter}{fileExtension}"))
            {
                counter++;
            }

            return $"{baseName}{counter}{fileExtension}";
        }

        private void ServiceDropDownList()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = "SELECT [AhtsService_Id],[Service_Name] FROM [dbo].[tbl_Ahts_Services]";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddlserviceName.Items.Clear(); // Clear existing items

                    ddlserviceName.DataSource = reader;
                    ddlserviceName.DataTextField = "Service_Name";
                    ddlserviceName.DataValueField = "Service_Name";
                    ddlserviceName.DataBind();

                    // Add an initial item to avoid the SelectedValue error
                    ddlserviceName.Items.Insert(0, new ListItem("-- Select Service --", ""));
                    ddlserviceName.SelectedIndex = 0; // Set the default selected index
                }
            }
        }

        private string GetUserId()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Name,Id, Ahts_Id FROM [dbo].[UserRegister] WHERE E_mail = @EmailId";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Assuming EmailId is the column name in the UserRegister table
                cmd.Parameters.AddWithValue("@EmailId", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    userId = reader["Id"].ToString();
                }

                reader.Close();
            }
            return userId;
        }

        private void DisplayUserName()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Name,Id, Ahts_Id FROM [dbo].[UserRegister] WHERE E_mail = @EmailId";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Assuming EmailId is the column name in the UserRegister table
                cmd.Parameters.AddWithValue("@EmailId", email);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    userName.Text = reader["Name"].ToString();
                    userId = reader["Id"].ToString();
                }

                reader.Close();
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["emailId"] = null;
            Response.Redirect("../Login/Login.aspx");
        }
        private string GetPriceForService(string serviceName)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = "SELECT [Service_Price] FROM [dbo].[tbl_Ahts_Services] WHERE [Service_Name] = @ServiceName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ServiceName", serviceName);
                    object result = cmd.ExecuteScalar();

                    return (result != null) ? result.ToString() : string.Empty;
                }
            }
        }




        protected void ddlserviceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlserviceName.SelectedValue))
            {
                string selectedServiceName = ddlserviceName.SelectedValue;

                // Retrieve and display the price for the selected service
                string price = GetPriceForService(selectedServiceName);
                priceLabel.Text = $"Price: {price}";
            }
            else
            {
                // Clear the label if no service is selected
                priceLabel.Text = string.Empty;
            }
        }
    }
}