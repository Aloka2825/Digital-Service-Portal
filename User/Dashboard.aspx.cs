using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dsportal.User
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string cs = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        string email = "";
        string id = "";
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
                DisplayUserName();
                DisplayProjectReport1();
                DisplayProjectReport2();
                DisplayProjectReport3();
                DisplayProjectReport4();
                Label1.Attributes.Add("style", "width: " + value_number1.Text);
                Label2.Attributes.Add("style", "width: " + value_number2.Text);
                Label3.Attributes.Add("style", "width: " + value_number3.Text);
                Label4.Attributes.Add("style", "width: " + value_number4.Text);



            }
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                string query = "SELECT * FROM [dbo].[tbl_Popular_Services]";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                string fileName = row["Service_Photo"].ToString();

                //Response.Redirect(link);

                string imagePath = "../Admin/Services/" + fileName;
                string textContent = row["Service_Name"].ToString();
                string link = row["Service_Link"].ToString();
                //string fullUrl = link;

                string html = $@"
                    <div class='box'>
                        <a href='{link}' target = '_blank'>
                            <img src='{imagePath}' alt='Image'>
                            <p>{textContent}</p>
                        </a>
                    </div>
                ";
                contentPanel.Controls.Add(new LiteralControl(html));
            }



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

        private void DisplayProjectReport1()
        {

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT TOP 1 Project_Id, Project_Name, Completion_Value FROM [dbo].[tbl_Project_Report] WHERE User_Id = " + userId + "ORDER BY Project_Id;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    projectReport1.Text = reader["Project_Name"].ToString();
                    value_number1.Text = reader["Completion_Value"].ToString() + "%";
                }

                reader.Close();
                conn.Close();
            }
        }
        private void DisplayProjectReport2()
        {

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Project_Id, Project_Name, Completion_Value FROM [dbo].[tbl_Project_Report] WHERE User_Id = " + userId + "ORDER BY Project_Id OFFSET 1 ROW FETCH NEXT 1 ROW ONLY;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    projectReport2.Text = reader["Project_Name"].ToString();
                    value_number2.Text = reader["Completion_Value"].ToString() + "%";
                }

                reader.Close();
                conn.Close();
            }
        }
        private void DisplayProjectReport3()
        {

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Project_Id, Project_Name, Completion_Value FROM [dbo].[tbl_Project_Report] WHERE User_Id = " + userId + "ORDER BY Project_Id OFFSET 2 ROWS FETCH NEXT 1 ROW ONLY;;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    projectReport3.Text = reader["Project_Name"].ToString();
                    value_number3.Text = reader["Completion_Value"].ToString() + "%";
                }

                reader.Close();
                conn.Close();
            }
        }
        private void DisplayProjectReport4()
        {

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query = "SELECT Project_Id, Project_Name, Completion_Value FROM [dbo].[tbl_Project_Report] WHERE User_Id = " + userId + "ORDER BY Project_Id OFFSET 3 ROWS FETCH NEXT 1 ROW ONLY;;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    projectReport4.Text = reader["Project_Name"].ToString();
                    value_number4.Text = reader["Completion_Value"].ToString() + "%";
                }

                reader.Close();
                conn.Close();
            }
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["emailId"] = null;
            Response.Redirect("../Login/Login.aspx");
        }
    }
}