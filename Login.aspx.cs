using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GNEnterprise
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
                Response.Redirect("PDF_Editing.aspx");
            lblErrorMessage.Visible = false;
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SL1THHN;Initial Catalog=dbGNEnterprise;Integrated Security=True;"))
            {
                con.Open();
                string query = "SELECT COUNT(1) FROM tblLogin WHERE userName=@userName AND userPassword=@userPassword";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userName", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue(@"userPassword", txtPassword.Text.Trim());
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    Session["userName"] = txtUsername.Text.Trim();
                    Response.Redirect("PDF_Editing.aspx");
                }
                else
                {
                    lblErrorMessage.Visible = true;
                }
            }
        }
    }
}