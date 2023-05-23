using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GNEnterprise
{
    public partial class Home : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/TextFileDate.txt"); // Path to the text file

            if (File.Exists(filePath))
            {
                string dateStr = File.ReadAllText(filePath);
                DateTime openDate;

                if (DateTime.TryParse(dateStr, out openDate))
                {
                    DateTime currentDate = DateTime.Now;
                    if (currentDate < openDate)
                    {
                        Response.Redirect("AccessDenied.aspx");
                    }
                    else
                    {
                        Response.Redirect("Home.master");
                    }
                }
                else
                {
                    // Handle invalid date format in the text file
                    Response.Write("Invalid date format in the text file.");
                }
            }
            else
            {
                // Handle missing text file
                Response.Write("Text file not found.");
            }
        }
    }
}
