using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.TextFormatting;

namespace GNEnterprise
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["dbGNEnterpriseConStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateParty.Visible = false;
            if (!IsPostBack)
            {
                GVBind();
            }
        }
        protected void GVBind()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblParty", con);
                cmd.ExecuteNonQuery();
                gvParty.DataBind();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    gvParty.DataSource = reader;
                    gvParty.DataBind();
                }
            }
        }
        void clear()
        {
            txtName.Text = "";
            ddlPartyType.SelectedIndex = 0;
            ddlPriceList.SelectedIndex = 0;
            ddlGSTType.SelectedIndex = 0;
            txtVendorCode.Text = "";
            txtDiscount.Text = "";
            ddlTranMode.SelectedIndex = 0;
            txtAddress.Text = "";
            ddlState.SelectedIndex = 0;
            txtPincode.Text = "";
            txtEwayPincode.Text = "";
            txtRoute.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmail.Text = "";
            txtContactInfo.Text = "";
            txtGSTINNo.Text = "";
            txtEGSTINNo.Text = "";
            txtPANNo.Text = "";
            txtNote.Text = "";
        }
        protected void gvParty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvParty, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void gvParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvParty.Rows)
            {
                if (row.RowIndex == gvParty.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                    Label lblpartyId = row.FindControl("lblpartyId") as Label;
                    Label lblName = row.FindControl("lblName") as Label;
                    Label lblPartyType = row.FindControl("lblPartyType") as Label;
                    Label lblPriceList = row.FindControl("lblPriceList") as Label;
                    Label lblGSTType = row.FindControl("lblGSTType") as Label;
                    Label lblVendorCode = row.FindControl("lblVendorCode") as Label;
                    Label lblDiscount = row.FindControl("lblDiscount") as Label;
                    Label lblDefaultTransactionMode = row.FindControl("lblDefaultTransactionMode") as Label;
                    Label lblAddress = row.FindControl("lblAddress") as Label;
                    Label lblState = row.FindControl("lblState") as Label;
                    Label lblPincode = row.FindControl("lblPincode") as Label;
                    Label lblEwayPincode = row.FindControl("lblEwayPincode") as Label;
                    Label lblRoute = row.FindControl("lblRoute") as Label;
                    Label lblPhoneNo = row.FindControl("lblPhoneNo") as Label;
                    Label lblMobileNo = row.FindControl("lblMobileNo") as Label;
                    Label lblEmail = row.FindControl("lblEmail") as Label;
                    Label lblContactInfo = row.FindControl("lblContactInfo") as Label;
                    Label lblGSTINNo = row.FindControl("lblGSTINNo") as Label;
                    Label lblEGSTINNo = row.FindControl("lblEGSTINNo") as Label;
                    Label lblPANNo = row.FindControl("lblPANNo") as Label;
                    Label lblNote = row.FindControl("lblNote") as Label;

                    lblpartyId.Text = lblpartyId.Text;
                    txtName.Text = lblName.Text;
                    ddlPartyType.SelectedValue = lblPartyType.Text;
                    ddlPriceList.SelectedValue = lblPriceList.Text;
                    ddlGSTType.SelectedValue = lblGSTType.Text;
                    txtVendorCode.Text = lblVendorCode.Text;
                    txtDiscount.Text = lblDiscount.Text;
                    ddlTranMode.SelectedValue = lblDefaultTransactionMode.Text;
                    txtAddress.Text = lblAddress.Text;
                    ddlState.SelectedValue = lblState.Text;
                    txtPincode.Text = lblPincode.Text;
                    txtEwayPincode.Text = lblEwayPincode.Text;
                    txtRoute.Text = lblRoute.Text;
                    txtPhoneNo.Text = lblPhoneNo.Text;
                    txtMobileNo.Text = lblMobileNo.Text;
                    txtEmail.Text = lblEmail.Text;
                    txtContactInfo.Text = lblContactInfo.Text;
                    txtGSTINNo.Text = lblGSTINNo.Text;
                    txtEGSTINNo.Text = lblEGSTINNo.Text;
                    txtPANNo.Text = lblPANNo.Text;
                    txtNote.Text = lblNote.Text;
                }
                else
                {
                    row.ToolTip = "Click to select this row.";
                }
            }
            btnAddParty.Visible = false;
            btnUpdateParty.Visible = true;
        }
        protected void btnAddParty_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                string query = @"INSERT INTO tblParty ( [partyName], [partyType], [partyPriceList], [partyGSTType],
                                                    [partyVendorCode], [partyFixDiscount], [partyDefaultTranMode],
                                                    [partyAddress],[partyState], [partyPincode], [partyEwayPincode],
                                                    [partyRoute], [partyPhone], [partyMobile], [partyEmail],
                                                    [partyContact], [partyGSTINNo], [partyEGSTINNo], [partyPANNNo],
                                                    [partyNote], [partyCreditAmount] ) VALUES (
                                                    @partyName, @partyType, @partyPriceList, @partyGSTType,
                                                    @partyVendorCode, @partyFixDiscount, @partyDefaultTranMode,@partyAddress,                                      
                                                    @partyState, @partyPincode, @partyEwayPincode,
                                                    @partyRoute, @partyPhone, @partyMobile, @partyEmail,
                                                    @partyContact, @partyGSTINNo, @partyEGSTINNo, @partyPANNNo,
                                                    @partyNote, @partyCreditAmount);";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    decimal temp = 0;
                    cmd.Parameters.AddWithValue("@partyName", txtName.Text);
                    cmd.Parameters.AddWithValue("@partyType", ddlPartyType.SelectedValue);
                    cmd.Parameters.AddWithValue("@partyPriceList", ddlPriceList.SelectedValue);
                    cmd.Parameters.AddWithValue("@partyGSTType", ddlGSTType.SelectedValue);
                    cmd.Parameters.AddWithValue("@partyVendorCode", txtVendorCode.Text);

                    decimal.TryParse(txtDiscount.Text, out temp);
                    cmd.Parameters.AddWithValue("@partyFixDiscount", temp);
                    temp = 0;

                    cmd.Parameters.AddWithValue("@partyDefaultTranMode", ddlTranMode.SelectedValue);
                    cmd.Parameters.AddWithValue("@partyAddress", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@partyState", ddlState.SelectedValue);
                    cmd.Parameters.AddWithValue("@partyPincode", txtPincode.Text);
                    cmd.Parameters.AddWithValue("@partyEwayPincode", txtEwayPincode.Text);
                    cmd.Parameters.AddWithValue("@partyRoute", txtRoute.Text);
                    cmd.Parameters.AddWithValue("@partyPhone", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@partyMobile", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@partyEmail", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@partyContact", txtContactInfo.Text);
                    cmd.Parameters.AddWithValue("@partyGSTINNo", txtGSTINNo.Text);
                    cmd.Parameters.AddWithValue("@partyEGSTINNo", txtEGSTINNo.Text);
                    cmd.Parameters.AddWithValue("@partyPANNNo", txtPANNo.Text);
                    cmd.Parameters.AddWithValue("@partyNote", txtNote.Text);

                    decimal.TryParse(txtCreditAmount.Text, out temp);
                    cmd.Parameters.AddWithValue("@partyCreditAmount", temp);
                    temp = 0;

                    cmd.ExecuteNonQuery();

                    GVBind();
                    clear();
                }
            }
        }
        protected void btnUpdateParty_Click(object sender, EventArgs e)
        {
           
            foreach (GridViewRow row in gvParty.Rows)
            {
                if (row.RowIndex == gvParty.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE tblParty SET " +
                        "partyName = @partyName," +
                        "partyType = @partyType," +
                        "partyPriceList = @partyPriceList," +
                        "partyGSTType = @partyGSTType," +
                        "partyVendorCode = @partyVendorCode," +
                        "partyFixDiscount = @partyFixDiscount," +
                        "partyDefaultTranMode = @partyDefaultTranMode," +
                        "partyAddress = @partyAddress," +
                        "partyState = @partyState," +
                        "partyPincode = @partyPincode," +
                        "partyEwayPincode = @partyEwayPincode," +
                        "partyRoute = @partyRoute," +
                        "partyPhone = @partyPhone," +
                        "partyMobile = @partyMobile," +
                        "partyEmail = @partyEmail," +
                        "partyContact = @partyContact," +
                        "partyGSTINNo = @partyGSTINNo," +
                        "partyEGSTINNo = @partyEGSTINNo," +
                        "partyPANNNo = @partyPANNNo," +
                        "partyNote = @partyNote " +
                        "WHERE partyId = @partyId", con);

                        cmd.Parameters.AddWithValue("@partyName", txtName.Text);
                        cmd.Parameters.AddWithValue("@partyType", ddlPartyType.SelectedValue);
                        cmd.Parameters.AddWithValue("@partyPriceList", ddlPriceList.SelectedValue);
                        cmd.Parameters.AddWithValue("@partyGSTType", ddlGSTType.SelectedValue);
                        cmd.Parameters.AddWithValue("@partyVendorCode", txtVendorCode.Text);
                        cmd.Parameters.AddWithValue("@partyFixDiscount", txtDiscount.Text);
                        cmd.Parameters.AddWithValue("@partyDefaultTranMode", ddlTranMode.SelectedValue);
                        cmd.Parameters.AddWithValue("@partyAddress", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@partyState", ddlState.SelectedValue);
                        cmd.Parameters.AddWithValue("@partyPincode", txtPincode.Text);
                        cmd.Parameters.AddWithValue("@partyEwayPincode", txtEwayPincode.Text);
                        cmd.Parameters.AddWithValue("@partyRoute", txtRoute.Text);
                        cmd.Parameters.AddWithValue("@partyPhone", txtPhoneNo.Text);
                        cmd.Parameters.AddWithValue("@partyMobile", txtMobileNo.Text);
                        cmd.Parameters.AddWithValue("@partyEmail", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@partyContact", txtContactInfo.Text);
                        cmd.Parameters.AddWithValue("@partyGSTINNo", txtGSTINNo.Text);
                        cmd.Parameters.AddWithValue("@partyEGSTINNo", txtEGSTINNo.Text);
                        cmd.Parameters.AddWithValue("@partyPANNNo", txtPANNo.Text);
                        cmd.Parameters.AddWithValue("@partyNote", txtNote.Text);
                        cmd.Parameters.AddWithValue("@partyId", (row.FindControl("lblpartyId") as Label).Text);

                        cmd.ExecuteNonQuery();
                        clear();
                        GVBind();
                        con.Close();
                    }                    
                }
                else
                {
                    row.ToolTip = "Click to select this row.";
                }
                btnUpdateParty.Visible = false;
                btnAddParty.Visible = true;
            }
        }
    }
}