using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GNEnterprise
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["dbGNEnterpriseConStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get_Xml();
            GridView1Bind();
            //GridView2Bind();
            GridView2.Visible = false;
        }
        //void Get_Xml()
        //{
        //    DataSet dataSet = new DataSet();
        //    dataSet.ReadXml(Server.MapPath("~/Data/XMLCredit1.xml"));
        //    if (dataSet != null && dataSet.HasChanges())
        //    {
        //        GridView1.DataSource = dataSet;
        //        GridView1.DataBind();
        //    }
        //}

        protected void GridView1Bind()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select partyId,partyName,partyCreditAmount from tblParty", con);
                cmd.ExecuteNonQuery();
                GridView1.DataBind();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    GridView1.DataSource = reader;
                    GridView1.DataBind();
                }
            }
        }

        //protected void GridView2Bind()
        //{
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT saleId,saleDate,saleBillAmount,salePaidAmount,saleRemainingAmount from tblSale;", con);
        //        cmd.ExecuteNonQuery();
        //        GridView2.DataBind();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.HasRows == true)
        //        {
        //            GridView2.DataSource = reader;
        //            GridView2.DataBind();
        //        }
        //    }
        //}

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView2.Visible = true;
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                    Label lblpartyId = row.FindControl("lblpartyId") as Label;
                    Label lblPartyName = row.FindControl("lblPartyName") as Label;
                    //Label lblCreditAmount = row.FindControl("lblCreditAmount") as Label;
                    lblPartyNameValue.Text = lblPartyName.Text;
                    int partyId;
                    Int32.TryParse(lblpartyId.Text, out partyId);
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(@" SELECT tblSale.saleId,saleDate,saleBillAmount,salePaidAmount,saleRemainingAmount 
                                                            FROM tblParty,tblSale
                                                            WHERE tblSale.partyId = tblParty.partyId
                                                            AND tblSale.partyId = " + partyId + "", con);
                        cmd.ExecuteNonQuery();
                        GridView2.DataBind();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows == true)
                        {
                            GridView2.DataSource = reader;
                            GridView2.DataBind();
                        }
                    }
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
    }
}