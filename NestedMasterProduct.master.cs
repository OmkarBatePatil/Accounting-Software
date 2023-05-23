using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Drawing;
using System.Web.Configuration;
using System.Web.DynamicData;
using iTextSharp.xmp.impl;

namespace GNEnterprise
{
    public partial class NestedMasterProduct : System.Web.UI.MasterPage
    {
        string CS = ConfigurationManager.ConnectionStrings["dbGNEnterpriseConStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateProduct.Visible = false;

            if (!IsPostBack)
            {
                //cascadingdropdown();
                GVBind();

                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("Select typeId, typeName from tblType", con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    ddlType.DataSource = rdr;
                    ddlType.DataTextField = "typeName";
                    ddlType.DataValueField = "typeId";
                    ddlType.DataBind();

                    //if (!String.IsNullOrEmpty(Session["productType"] as string))
                    //{
                    //    ddlType.SelectedIndex =
                    //    ddlType.Items.IndexOf(ddlType.Items.FindByValue(Session["productType"].ToString()));
                    //}

                    //ListItem selectType = new ListItem("Select Type", "-1");
                    //ddlType.Items.Insert(0, selectType);
                }
            }
        }
        //protected void cascadingdropdown()
        //{
        //    SqlConnection sqlConnection = new SqlConnection(CS);
        //    sqlConnection.Open();
        //    SqlCommand sqlCommand = new SqlCommand("select * from tblSubType", sqlConnection);
        //    sqlCommand.CommandType = CommandType.Text;
        //    ddlSubType.DataSource = sqlCommand.ExecuteReader();
        //    ddlSubType.DataTextField = "subtypeName";
        //    ddlSubType.DataValueField = "subtypeId";
        //    ddlSubType.DataBind();
        //    //ddlSubType.Items.Insert(0, new ListItem("Select Type", "0"));
        //    ddlSubType.Items.Insert(0, new ListItem("Select SubType", "0"));
        //}

        //protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //int subtypeId = Convert.ToInt32(ddlSubType.SelectedValue);
        //    SqlConnection sqlConnection = new SqlConnection(CS);
        //    sqlConnection.Open();
        //    SqlCommand sqlCommand = new SqlCommand("select * from tblType", sqlConnection);
        //    sqlCommand.CommandType = CommandType.Text;
        //    ddlType.DataSource = sqlCommand.ExecuteReader();
        //    ddlType.DataTextField = "typeName";
        //    ddlType.DataValueField = "typeId";
        //    ddlType.DataBind();
        //    //ddlType.Items.Insert(0, new ListItem("Select Type", "0"));
        //}

        //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int typeId = Convert.ToInt32(ddlType.SelectedValue);
        //    SqlConnection sqlConnection = new SqlConnection(CS);
        //    sqlConnection.Open();
        //    SqlCommand sqlCommand = new SqlCommand("select * from tblSubType where typeId = " + typeId, sqlConnection);
        //    sqlCommand.CommandType = CommandType.Text;
        //    ddlSubType.DataSource = sqlCommand.ExecuteReader();
        //    ddlSubType.DataTextField = "subtypeName";
        //    ddlSubType.DataValueField = "subtypeId";
        //    ddlSubType.DataBind();
        //    ddlSubType.Items.Insert(0, new ListItem("Select SubType", "0"));
        //}

        //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string typeId = ddlType.SelectedItem.Value;
        //    string typeName = ddlType.SelectedItem.Text;
        //    Session["productType"] = typeId;
        //    Response.Redirect("WebForm" + typeName + ".aspx", true);
        //}
        void clear()
        {
            txtName.Text = "";
            txtCompany.Text = "";
            txtUnit.Text = "";
            txtStock.Text = "";
            txtPurchasePrice.Text = "";
            txtMRP.Text = "";
            txtSalesPrice.Text = "";
            txtDiscount.Text = "";
            txtHSN.Text = "";
            txtMinimumStock.Text = "";
            txtMaximumStock.Text = "";
            txtKey1.Text = "";
            txtKey2.Text = "";
            txtKey3.Text = "";
            txtKey4.Text = "";
            txtKey5.Text = "";
            txtValue1.Text = "";
            txtValue2.Text = "";
            txtValue3.Text = "";
            txtValue4.Text = "";
            txtValue5.Text = "";
            txtShortCode.Text = "";
            ddlStockType.SelectedValue = "";
            txtReOrder.Text = "";
            txtDiscountAmt.Text = "";
            txtStoreLocation.Text = "";
            txtIGSTRATE.Text = "";
            txtCGSTRATE.Text = "";
            txtSGSTRATE.Text = "";
            txtMasterPacking.Text = "";
            txtSaleRateAPer.Text = "";
            txtSaleRateBPer.Text = "";
            txtSaleRateCPer.Text = "";
            txtSaleRateDPer.Text = "";
            txtSaleRateARs.Text = "";
            txtSaleRateBRs.Text = "";
            txtSaleRateCRs.Text = "";
            txtSaleRateDRs.Text = "";
            txtNote.Text = "";
        }
        protected void GVBind()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblProduct", con);
                cmd.ExecuteNonQuery();
                gvProduct.DataBind();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    gvProduct.DataSource = reader;
                    gvProduct.DataBind();
                }
            }
        }
        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvProduct, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void gvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvProduct.Rows)
            {
                if (row.RowIndex == gvProduct.SelectedIndex)
                {
                    row.ToolTip = string.Empty;

                    Label lblproductId = row.FindControl("lblproductId") as Label;
                    Label lblName = row.FindControl("lblName") as Label;
                    Label lblShortCode = row.FindControl("lblShortCode") as Label;
                    Label lblCompany = row.FindControl("lblCompany") as Label;
                    Label lblUnit = row.FindControl("lblUnit") as Label;
                    Label lblStock = row.FindControl("lblStock") as Label;
                    Label lblStockType = row.FindControl("lblStockType") as Label;
                    Label lblReOrder = row.FindControl("lblReOrder") as Label;
                    Label lblPurchasePrice = row.FindControl("lblPurchasePrice") as Label;
                    Label lblMRP = row.FindControl("lblMRP") as Label;
                    Label lblSalesPrice = row.FindControl("lblSalesPrice") as Label;
                    Label lblDiscount = row.FindControl("lblDiscount") as Label;
                    Label lblDiscountRs = row.FindControl("lblDiscountRs") as Label;
                    Label lblMinimumStock = row.FindControl("lblMinimumStock") as Label;
                    Label lblMaximumStock = row.FindControl("lblMaximumStock") as Label;
                    Label lblStoreLocation = row.FindControl("lblStoreLocation") as Label;
                    Label lblIGSTRATE = row.FindControl("lblIGSTRATE") as Label;
                    Label lblCGSTRATE = row.FindControl("lblCGSTRATE") as Label;
                    Label lblSGSTRATE = row.FindControl("lblSGSTRATE") as Label;
                    Label lblHSN = row.FindControl("lblHSN") as Label;
                    Label lblMasterPacking = row.FindControl("lblMasterPacking") as Label;
                    Label lblSaleRateAPer = row.FindControl("lblSaleRateAPer") as Label;
                    Label lblSaleRateBPer = row.FindControl("lblSaleRateBPer") as Label;
                    Label lblSaleRateCPer = row.FindControl("lblSaleRateCPer") as Label;
                    Label lblSaleRateDPer = row.FindControl("lblSaleRateDPer") as Label;
                    Label lblSaleRateARs = row.FindControl("lblSaleRateARs") as Label;
                    Label lblSaleRateBRs = row.FindControl("lblSaleRateBRs") as Label;
                    Label lblSaleRateCRs = row.FindControl("lblSaleRateCRs") as Label;
                    Label lblSaleRateDRs = row.FindControl("lblSaleRateDRs") as Label;
                    Label lblNote = row.FindControl("lblNote") as Label;
                    Label lblsubtypeId = row.FindControl("lblsubtypeId") as Label;
                    Label lblDescription1 = row.FindControl("lblDescription1") as Label;
                    Label lblDescription2 = row.FindControl("lblDescription2") as Label;
                    Label lblDescription3 = row.FindControl("lblDescription3") as Label;
                    Label lblDescription4 = row.FindControl("lblDescription4") as Label;
                    Label lblDescription5 = row.FindControl("lblDescription5") as Label;

                    /*String pCompany;
                    pCompany = row.FindControl("Company") as TextBox;

                    String pUnit;
                    pUnit = row.Cells[1].Text;

                    String pStock;
                    pStock = row.Cells[2].Text;

                    String pPurchasePrice;
                    pPurchasePrice = row.Cells[3].Text;

                    String pMRP;
                    pMRP = row.Cells[4].Text;

                    String pSalesPrice;
                    pSalesPrice = row.Cells[5].Text;

                    String pDiscount;
                    pDiscount = row.Cells[6].Text;

                    String pHSN;
                    pHSN = row.Cells[7].Text;

                    String pMinimumStock;
                    pMinimumStock = row.Cells[8].Text;

                    String pMaximumStock;
                    pMaximumStock = row.Cells[9].Text;

                    String pKey1;
                    pKey1 = row.Cells[10].Text;

                    String pKey2;
                    pKey2 = row.Cells[11].Text;

                    String pKey3;
                    pKey3 = row.Cells[12].Text;

                    String pKey4;
                    pKey4 = row.Cells[13].Text;

                    String pKey5;
                    pKey5 = row.Cells[14].Text;

                    String pValue1;
                    pValue1 = row.Cells[15].Text;

                    String pValue2;
                    pValue2 = row.Cells[16].Text;

                    String pValue3;
                    pValue3 = row.Cells[17].Text;

                    String pValue4;
                    pValue4 = row.Cells[18].Text;

                    String pValue5;
                    pValue5 = row.Cells[19].Text;*/


                    txtName.Text = lblName.Text;
                    txtShortCode.Text = lblShortCode.Text;
                    txtCompany.Text = lblCompany.Text;
                    txtUnit.Text = lblUnit.Text;
                    txtStock.Text = lblStock.Text;
                    ddlStockType.Text = lblStockType.Text;
                    txtReOrder.Text = lblReOrder.Text;
                    txtPurchasePrice.Text = lblPurchasePrice.Text;
                    txtMRP.Text = lblMRP.Text;
                    txtSalesPrice.Text = lblSalesPrice.Text;
                    txtDiscount.Text = lblDiscount.Text;
                    txtDiscountAmt.Text = lblDiscountRs.Text;                    
                    txtMinimumStock.Text = lblMinimumStock.Text;
                    txtMaximumStock.Text = lblMaximumStock.Text;
                    txtStoreLocation.Text = lblStoreLocation.Text;
                    txtIGSTRATE.Text = lblIGSTRATE.Text;
                    txtSGSTRATE.Text = lblSGSTRATE.Text;
                    txtCGSTRATE.Text = lblCGSTRATE.Text;
                    txtHSN.Text = lblHSN.Text;
                    txtMasterPacking.Text = lblMasterPacking.Text;
                    txtSaleRateAPer.Text = lblSaleRateAPer.Text;
                    txtSaleRateBPer.Text = lblSaleRateBPer.Text;
                    txtSaleRateCPer.Text = lblSaleRateCPer.Text;
                    txtSaleRateDPer.Text = lblSaleRateDPer.Text;
                    txtSaleRateARs.Text = lblSaleRateARs.Text;
                    txtSaleRateBRs.Text = lblSaleRateBRs.Text;
                    txtSaleRateCRs.Text = lblSaleRateCRs.Text;
                    txtSaleRateDRs.Text = lblSaleRateDRs.Text;
                    txtNote.Text = lblNote.Text;

                    ddlSubType.SelectedValue = lblsubtypeId.Text;
                    // txtKey1.Text + txtValue1.Text = lblDescription1.Text;
                    txtKey1.Text = lblDescription1.Text;
                    txtValue1.Text = lblDescription1.Text;

                    txtKey2.Text = lblDescription2.Text;
                    txtValue2.Text = lblDescription2.Text;

                    txtKey3.Text = lblDescription3.Text;
                    txtValue3.Text = lblDescription3.Text;

                    txtKey4.Text = lblDescription4.Text;
                    txtValue4.Text = lblDescription4.Text;

                    txtKey5.Text = lblDescription5.Text;
                    txtValue5.Text = lblDescription5.Text;

                    //Cascading
                    int productId, subtypeId;
                    Int32.TryParse(lblproductId.Text, out productId);
                    Int32.TryParse(lblsubtypeId.Text, out subtypeId);
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand(@" select tblType.typeId
                                                                    from tblSubType, tblType, tblProduct
                                                                    where tblType.typeId = tblSubType.typeId
                                                                     and tblSubType.subtypeId = tblProduct.subtypeId
                                                                     and tblProduct.subtypeId = " + subtypeId +
                                                                "and tblProduct.productId = " + productId + "", con);
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        string typeId = "";
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                typeId = rdr[0].ToString();
                            }
                        }
                        ddlType.SelectedValue = typeId;
                    }
                }
                else
                {
                    row.ToolTip = "Click to select this row.";
                }
            }
            btnAddProduct.Visible = false;
            btnUpdateProduct.Visible = true;
        }
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            /*using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"insert into tblProduct (productCompany," +
                                                                            "productName," +
                                                                            "productUnit, " +
                                                                            "productStock, " +
                                                                            "productPurchasePrice," +
                                                                            "productMRP," +
                                                                            "productSalesPrice," +
                                                                            "productDiscount," +
                                                                            "productHSN," +
                                                                            "productMinimumStock," +
                                                                            "productMaximumStock," +
                                                                            "subTypeId," +
                                                                            "description1," +
                                                                            "description2," +
                                                                            "description3," +
                                                                            "description4," +
                                                                            "description5) " +
                                                                            "values('" +
                                                                                    txtCompany.Text + "','" +
                                                                                    txtName.Text + "','" +
                                                                                    txtUnit.Text + "','" +
                                                                                    txtStock.Text + "','" +
                                                                                    txtPurchasePrice.Text + "','" +
                                                                                    txtMRP.Text + "','" +
                                                                                    txtSalesPrice.Text + "','" +
                                                                                    txtDiscount.Text + "','" +
                                                                                    txtHSN.Text + "','" +
                                                                                    txtMinimumStock.Text + "','" +
                                                                                    txtMaximumStock.Text + "','" +
                                                                                    ddlSubType.SelectedValue + "','" +
                                                                                    txtKey1.Text + txtValue1.Text + "','" +
                                                                                    txtKey2.Text + txtValue2.Text + "','" +
                                                                                    txtKey3.Text + txtValue3.Text + "','" +
                                                                                    txtKey4.Text + txtValue4.Text + "','" +
                                                                                    txtKey5.Text + txtValue5.Text + "')", con);
                cmd.ExecuteNonQuery();

                clear();
                GVBind();
            }*/

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                string query = @"INSERT INTO tblProduct (
                        [productCompany], [productName], [productUnit], [productStock],
                        [productPurchasePrice], [productMRP], [productSalesPrice],
                        [productDiscount], [productHSN], [productMinimumStock],
                        [productMaximumStock], [subtypeId], [description1], [description2],
                        [description3], [description4], [description5], [productShortCode],
                        [productStockType], [productReorder], [productDiscountRs],
                        [productStoreLocation], [productIGST], [productCGST], [productSGST],
                        [productMasterPacking], [productSaleAper], [productSaleBper],
                        [productSaleCper], [productSaleDper], [productSaleArs],
                        [productSaleBrs], [productSaleCrs], [productSaleDrs], [productNote]
                    ) VALUES (@productCompany, @productName, @productUnit, @productStock,
                        @productPurchasePrice, @productMRP, @productSalesPrice,
                        @productDiscount, @productHSN, @productMinimumStock,
                        @productMaximumStock, @subtypeId, @description1, @description2,
                        @description3, @description4, @description5, @productShortCode,
                        @productStockType, @productReorder, @productDiscountRs,
                        @productStoreLocation, @productIGST, @productCGST, @productSGST,
                        @productMasterPacking, @productSaleAper, @productSaleBper,
                        @productSaleCper, @productSaleDper, @productSaleArs,
                        @productSaleBrs, @productSaleCrs, @productSaleDrs, @productNote)";
                
                SqlCommand cmd = new SqlCommand(query, con);
                decimal temp = 0;
                cmd.Parameters.AddWithValue("@productCompany", txtCompany.Text);
                cmd.Parameters.AddWithValue("@productName", txtName.Text);
                cmd.Parameters.AddWithValue("@productUnit", txtUnit.Text);
                cmd.Parameters.AddWithValue("@productStock", txtStock.Text);
                cmd.Parameters.AddWithValue("@productPurchasePrice", txtPurchasePrice.Text);

                decimal.TryParse(txtMRP.Text, out temp);
                cmd.Parameters.AddWithValue("@productMRP",  temp);
                temp = 0;

                cmd.Parameters.AddWithValue("@productSalesPrice", txtSalesPrice.Text);

                decimal.TryParse(txtDiscount.Text, out temp);
                cmd.Parameters.AddWithValue("@productDiscount", temp);
                temp = 0;

                cmd.Parameters.AddWithValue("@productHSN", txtHSN.Text);
                cmd.Parameters.AddWithValue("@productMinimumStock", txtMinimumStock.Text);
                cmd.Parameters.AddWithValue("@productMaximumStock", txtMaximumStock.Text);
                cmd.Parameters.AddWithValue("@subtypeId", ddlSubType.SelectedValue);
                cmd.Parameters.AddWithValue("@description1", txtKey1.Text + txtValue1.Text);
                cmd.Parameters.AddWithValue("@description2", txtKey2.Text + txtValue2.Text);
                cmd.Parameters.AddWithValue("@description3", txtKey3.Text + txtValue3.Text);
                cmd.Parameters.AddWithValue("@description4", txtKey4.Text + txtValue4.Text);
                cmd.Parameters.AddWithValue("@description5", txtKey5.Text + txtValue5.Text);
                cmd.Parameters.AddWithValue("@productShortCode", txtShortCode.Text);
                cmd.Parameters.AddWithValue("@productStockType", ddlStockType.SelectedValue);
                                
                cmd.Parameters.AddWithValue("@productReorder", txtReOrder.Text);
                decimal.TryParse(txtDiscountAmt.Text, out temp);
                cmd.Parameters.AddWithValue("@productDiscountRs", temp);
                temp = 0;

                cmd.Parameters.AddWithValue("@productStoreLocation", txtStoreLocation.Text);

                decimal.TryParse(txtIGSTRATE.Text, out temp);
                cmd.Parameters.AddWithValue("@productIGST", temp);
                temp = 0;

                decimal.TryParse(txtCGSTRATE.Text, out temp);
                cmd.Parameters.AddWithValue("@productCGST", temp);
                temp = 0;

                decimal.TryParse(txtSGSTRATE.Text,out temp);
                cmd.Parameters.AddWithValue("@productSGST", temp);
                temp = 0;

                cmd.Parameters.AddWithValue("@productMasterPacking", txtMasterPacking.Text);

                decimal.TryParse(txtSaleRateAPer.Text, out temp);
                cmd.Parameters.AddWithValue("@productSaleAper", temp);
                temp = 0;

                decimal.TryParse(txtSaleRateBPer.Text, out temp);
                cmd.Parameters.AddWithValue("@productSaleBper", temp);
                temp = 0;

                decimal.TryParse(txtSaleRateCPer.Text, out temp);
                cmd.Parameters.AddWithValue("@productSaleCper", temp);
                temp = 0;

                decimal.TryParse(txtSaleRateDPer.Text, out temp);
                cmd.Parameters.AddWithValue("@productSaleDper", temp);
                temp = 0;

                decimal.TryParse(txtSaleRateARs.Text, out temp);
                cmd.Parameters.AddWithValue("@productSaleArs", temp);
                temp = 0;

                decimal.TryParse(txtSaleRateBRs.Text, out temp);
                cmd.Parameters.AddWithValue("@productSaleBrs", temp);
                temp = 0;

                decimal.TryParse(txtSaleRateCRs.Text, out temp);
                cmd.Parameters.AddWithValue("@productSaleCrs", temp);
                temp = 0;

                decimal.TryParse(txtSaleRateDRs.Text, out temp);
                cmd.Parameters.AddWithValue("@productSaleDrs", temp);
                temp = 0;

                cmd.Parameters.AddWithValue("@productNote", txtNote.Text);  
                
                cmd.ExecuteNonQuery();

                clear();
                GVBind();

                /*string query = @"INSERT INTO tblProduct (
                //                    [productCompany], [productName], [productUnit], [productStock],
                //                    [productPurchasePrice], [productMRP], [productSalesPrice],
                //                    [productDiscount], [productHSN], [productMinimumStock],
                //                    [productMaximumStock], [subtypeId], [description1], [description2],
                //                    [description3], [description4], [description5], [productShortCode],
                //                    [productStockType], [productReorder], [productDiscountRs],
                //                    [productStoreLocation], [productIGST], [productCGST], [productSGST],
                //                    [productMasterPacking], [productSaleAper], [productSaleBper],
                //                    [productSaleCper], [productSaleDper], [productSaleArs],
                //                    [productSaleBrs], [productSaleCrs], [productSaleDrs], [productNote]
                //                ) VALUES ('" + txtCompany.Text + "','" + 
                //                    txtName.Text + "','" +
                //                    txtUnit.Text + "','" +
                //                    txtStock.Text + "','" +
                //                    txtPurchasePrice.Text + "'," +
                //                    txtMRP.Text + ",'" +
                //                    txtSalesPrice.Text + "'," +
                //                    txtDiscount.Text + ",'" +
                //                    txtHSN.Text + "','" +
                //                    txtMinimumStock.Text + "','" +
                //                    txtMaximumStock.Text + "','" +
                //                    ddlSubType.SelectedValue + "','" +
                //                    txtKey1.Text + txtValue1.Text + "','" +
                //                    txtKey2.Text + txtValue2.Text + "','" +
                //                    txtKey3.Text + txtValue3.Text + "','" +
                //                    txtKey4.Text + txtValue4.Text + "','" +
                //                    txtKey5.Text + txtValue5.Text + "','" +
                //                    txtShortCode.Text + "','" +
                //                    ddlStockType.SelectedValue + "','" +
                //                    txtReOrder.Text + "'," +
                //                    txtDiscountAmt.Text + ",'" +
                //                    txtStoreLocation.Text + "'," +
                //                    txtIGSTRATE.Text + "," +
                //                    txtCGSTRATE.Text + "," +
                //                    txtSGSTRATE.Text + ",'" +
                //                    txtMasterPacking.Text + "'," +
                //                    txtSaleRateAPer.Text + "," +
                //                    txtSaleRateBPer.Text + "," +
                //                    txtSaleRateCPer.Text + "," +
                //                    txtSaleRateDPer.Text + "," +
                //                    txtSaleRateARs.Text + "," +
                //                    txtSaleRateBRs.Text + "," +
                //                    txtSaleRateCRs.Text + "," +
                //                    txtSaleRateDRs.Text + ",'" +
                               txtNote.Text + "');";   */                                                                 
            }
        }
        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            /* foreach (GridViewRow row in gvProduct.Rows)
             {
                 if (row.RowIndex == gvProduct.SelectedIndex)
                 {
                     row.ToolTip = string.Empty;
                    /* Label lblproductId = row.FindControl("lblproductId") as Label;
                     int productId;
                     Int32.TryParse(lblproductId.Text, out productId);

                     using (SqlConnection con = new SqlConnection(CS))
                     {
                         con.Open();
                         SqlCommand cmd = new SqlCommand(@"UPDATE tblProduct SET productCompany = @productCompany, 
                                                                 productName = @productName, 
                                                                 productUnit = @productUnit, 
                                                                 productStock = @productStock, 
                                                                 productPurchasePrice = @productPurchasePrice, 
                                                                 productMRP = @productMRP, 
                                                                 productSalesPrice = @productSalesPrice, 
                                                                 productDiscount = @productDiscount, 
                                                                 productHSN = @productHSN, 
                                                                 productMinimumStock = @productMinimumStock, 
                                                                 productMaximumStock = @productMaximumStock, 
                                                                 description1 = @description1, 
                                                                 description2 = @description2, 
                                                                 description3 = @description3, 
                                                                 description4 = @description4, 
                                                                 description5 = @description5, 
                                                                 productShortCode = @productShortCode, 
                                                                 productStockType = @productStockType, 
                                                                 productReorder = @productReorder, 
                                                                 productDiscountRs = @productDiscountRs, 
                                                                 productStoreLocation = @productStoreLocation, 
                                                                 productIGST = @productIGST, 
                                                                 productCGST = @productCGST, 
                                                                 productSGST = @productSGST, 
                                                                 productMasterPacking = @productMasterPacking, 
                                                                 productSaleAper = @productSaleAper, 
                                                                 productSaleBper = @productSaleBper, 
                                                                 productSaleCper = @productSaleCper, 
                                                                 productSaleDper = @productSaleDper, 
                                                                 productSaleArs = @productSaleArs, 
                                                                 productSaleBrs = @productSaleBrs, 
                                                                 productSaleCrs = @productSaleCrs, 
                                                                 productSaleDrs = @productSaleDrs, 
                                                                 productNote = @productNote 
                                                                 WHERE productId = @productId)", con);

                         cmd.Parameters.AddWithValue("@productCompany", txtCompany.Text);
                         cmd.Parameters.AddWithValue("@productName", txtName.Text);
                         cmd.Parameters.AddWithValue("@productUnit", txtUnit.Text);
                         cmd.Parameters.AddWithValue("@productStock", txtStock.Text);
                         cmd.Parameters.AddWithValue("@productPurchasePrice", txtPurchasePrice.Text);                        
                         cmd.Parameters.AddWithValue("@productMRP", txtMRP.Text);                        
                         cmd.Parameters.AddWithValue("@productSalesPrice", txtSalesPrice.Text);                        
                         cmd.Parameters.AddWithValue("@productDiscount", txtDiscount.Text);                        
                         cmd.Parameters.AddWithValue("@productHSN", txtHSN.Text);
                         cmd.Parameters.AddWithValue("@productMinimumStock", txtMinimumStock.Text);
                         cmd.Parameters.AddWithValue("@productMaximumStock", txtMaximumStock.Text);
                         cmd.Parameters.AddWithValue("@subtypeId", ddlSubType.SelectedValue);
                         cmd.Parameters.AddWithValue("@description1", txtKey1.Text + txtValue1.Text);
                         cmd.Parameters.AddWithValue("@description2", txtKey2.Text + txtValue2.Text);
                         cmd.Parameters.AddWithValue("@description3", txtKey3.Text + txtValue3.Text);
                         cmd.Parameters.AddWithValue("@description4", txtKey4.Text + txtValue4.Text);
                         cmd.Parameters.AddWithValue("@description5", txtKey5.Text + txtValue5.Text);
                         cmd.Parameters.AddWithValue("@productShortCode", txtShortCode.Text);
                         cmd.Parameters.AddWithValue("@productStockType", ddlStockType.SelectedValue);
                         cmd.Parameters.AddWithValue("@productReorder", txtReOrder.Text);                        
                         cmd.Parameters.AddWithValue("@productDiscountRs", txtDiscountAmt.Text);                        
                         cmd.Parameters.AddWithValue("@productStoreLocation", txtStoreLocation.Text);                        
                         cmd.Parameters.AddWithValue("@productIGST", txtIGSTRATE.Text);                                                 
                         cmd.Parameters.AddWithValue("@productCGST", txtCGSTRATE.Text);                                                 
                         cmd.Parameters.AddWithValue("@productSGST", txtSGSTRATE.Text);                         
                         cmd.Parameters.AddWithValue("@productMasterPacking", txtMasterPacking.Text);                        
                         cmd.Parameters.AddWithValue("@productSaleAper", txtSaleRateAPer.Text);                                                  
                         cmd.Parameters.AddWithValue("@productSaleBper", txtSaleRateBPer.Text);                                                  
                         cmd.Parameters.AddWithValue("@productSaleCper", txtSaleRateCPer.Text);                                                  
                         cmd.Parameters.AddWithValue("@productSaleDper", txtSaleRateDPer.Text);                                                 
                         cmd.Parameters.AddWithValue("@productSaleArs", txtSaleRateARs.Text);                                                  
                         cmd.Parameters.AddWithValue("@productSaleBrs", txtSaleRateBRs.Text);                                                  
                         cmd.Parameters.AddWithValue("@productSaleCrs", txtSaleRateCRs.Text);                                                  
                         cmd.Parameters.AddWithValue("@productSaleDrs", txtSaleRateDRs.Text);                         
                         cmd.Parameters.AddWithValue("@productNote", txtNote.Text);
                         cmd.Parameters.AddWithValue("@productId", (row.FindControl("lblproductId") as Label).Text);

                         /* decimal temp = 0;
                          cmd.Parameters.AddWithValue("@productCompany", txtCompany.Text);
                          cmd.Parameters.AddWithValue("@productName", txtName.Text);
                          cmd.Parameters.AddWithValue("@productUnit", txtUnit.Text);
                          cmd.Parameters.AddWithValue("@productStock", txtStock.Text);
                          cmd.Parameters.AddWithValue("@productPurchasePrice", txtPurchasePrice.Text);

                          decimal.TryParse(txtMRP.Text, out temp);
                          cmd.Parameters.AddWithValue("@productMRP", temp);
                          temp = 0;

                          cmd.Parameters.AddWithValue("@productSalesPrice", txtSalesPrice.Text);

                          decimal.TryParse(txtDiscount.Text, out temp);
                          cmd.Parameters.AddWithValue("@productDiscount", temp);
                          temp = 0;

                          cmd.Parameters.AddWithValue("@productHSN", txtHSN.Text);
                          cmd.Parameters.AddWithValue("@productMinimumStock", txtMinimumStock.Text);
                          cmd.Parameters.AddWithValue("@productMaximumStock", txtMaximumStock.Text);
                          cmd.Parameters.AddWithValue("@subtypeId", ddlSubType.SelectedValue);
                          cmd.Parameters.AddWithValue("@description1", txtKey1.Text + txtValue1.Text);
                          cmd.Parameters.AddWithValue("@description2", txtKey2.Text + txtValue2.Text);
                          cmd.Parameters.AddWithValue("@description3", txtKey3.Text + txtValue3.Text);
                          cmd.Parameters.AddWithValue("@description4", txtKey4.Text + txtValue4.Text);
                          cmd.Parameters.AddWithValue("@description5", txtKey5.Text + txtValue5.Text);
                          cmd.Parameters.AddWithValue("@productShortCode", txtShortCode.Text);
                          cmd.Parameters.AddWithValue("@productStockType", ddlStockType.SelectedValue);

                          cmd.Parameters.AddWithValue("@productReorder", txtReOrder.Text);
                          decimal.TryParse(txtDiscountAmt.Text, out temp);
                          cmd.Parameters.AddWithValue("@productDiscountRs", temp);
                          temp = 0;

                          cmd.Parameters.AddWithValue("@productStoreLocation", txtStoreLocation.Text);

                          decimal.TryParse(txtIGSTRATE.Text, out temp);
                          cmd.Parameters.AddWithValue("@productIGST", temp);
                          temp = 0;

                          decimal.TryParse(txtCGSTRATE.Text, out temp);
                          cmd.Parameters.AddWithValue("@productCGST", temp);
                          temp = 0;

                          decimal.TryParse(txtSGSTRATE.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSGST", temp);
                          temp = 0;

                          cmd.Parameters.AddWithValue("@productMasterPacking", txtMasterPacking.Text);

                          decimal.TryParse(txtSaleRateAPer.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSaleAper", temp);
                          temp = 0;

                          decimal.TryParse(txtSaleRateBPer.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSaleBper", temp);
                          temp = 0;

                          decimal.TryParse(txtSaleRateCPer.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSaleCper", temp);
                          temp = 0;

                          decimal.TryParse(txtSaleRateDPer.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSaleDper", temp);
                          temp = 0;

                          decimal.TryParse(txtSaleRateARs.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSaleArs", temp);
                          temp = 0;

                          decimal.TryParse(txtSaleRateBRs.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSaleBrs", temp);
                          temp = 0;

                          decimal.TryParse(txtSaleRateCRs.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSaleCrs", temp);
                          temp = 0;

                          decimal.TryParse(txtSaleRateDRs.Text, out temp);
                          cmd.Parameters.AddWithValue("@productSaleDrs", temp);
                          temp = 0;

                          cmd.Parameters.AddWithValue("@productNote", txtNote.Text);

                          cmd.Parameters.AddWithValue("@productId", (row.FindControl("lblproductId") as Label).Text);*/

            /*SqlCommand cmd = new SqlCommand(@"UPDATE tblProduct SET " +
                         //                                                        "productCompany = '" + txtCompany.Text + "'," +
                         //                                                        "productName = '" + txtName.Text + "'," +
                         //                                                        "productUnit = '" + txtUnit.Text + "'," +
                         //                                                        "productStock = '" + txtStock.Text + "'," +
                         //                                                        "productPurchasePrice = '" + txtPurchasePrice.Text + "'," +
                         //                                                        "productMRP = " + txtMRP.Text + "," +
                         //                                                        "productSalesPrice = '" + txtSalesPrice.Text + "'," +
                         //                                                        "productDiscount = " + txtDiscount.Text + "," +
                         //                                                        "productHSN = '" + txtHSN.Text + "'," +
                         //                                                        "productMinimumStock = '" + txtMinimumStock.Text + "'," +
                         //                                                        "productMaximumStock = '" + txtMaximumStock.Text + "'," +
                         //                                                        "description1 = '" + txtKey1.Text + txtValue1.Text + "'," +
                         //                                                        "description2 = '" + txtKey2.Text + txtValue2.Text + "'," +
                         //                                                        "description3 = '" + txtKey3.Text + txtValue3.Text + "'," +
                         //                                                        "description4 = '" + txtKey4.Text + txtValue4.Text + "'," +
                         //                                                        "description5 = '" + txtKey5.Text + txtValue5.Text + "'," +
                         //                                                        "productShortCode = '" + txtShortCode.Text + "'," +
                         //                                                        "productStockType = '" + ddlStockType.SelectedValue + "'," +
                         //                                                        "productReorder = '" + txtReOrder.Text + "'," +
                         //                                                        "productDiscountRs = " + txtDiscountAmt.Text + "," +
                         //                                                        "productStoreLocation = '" + txtStoreLocation.Text + "'," +
                         //                                                        "productIGST = " + txtIGSTRATE.Text + "," +
                         //                                                        "productCGST = " + txtCGSTRATE.Text + "," +
                         //                                                        "productSGST = " + txtSGSTRATE.Text + "," +
                         //                                                        "productMasterPacking = '" + txtMasterPacking.Text + "'," +
                         //                                                        "productSaleAper = " + txtSaleRateAPer.Text + "," +
                         //                                                        "productSaleBper = " + txtSaleRateBPer.Text + "," +
                         //                                                        "productSaleCper = " + txtSaleRateCPer.Text + "," +
                         //                                                        "productSaleDper = " + txtSaleRateDPer.Text + "," +
                         //                                                        "productSaleArs = " + txtSaleRateARs.Text + "," +
                         //                                                        "productSaleBrs = " + txtSaleRateBRs.Text + "," +
                         //                                                        "productSaleCrs = " + txtSaleRateCRs.Text + "," +
                         //                                                        "productSaleDrs = " + txtSaleRateDRs.Text + "," +
                         //                                                        "productNote = '" + txtNote.Text + "'" +
                                                                                 "WHERE productId = " + productId + "", con);
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
                 btnUpdateProduct.Visible = false;
                 btnAddProduct.Visible = true;
             }*/

            foreach (GridViewRow row in gvProduct.Rows)
            {
                if (row.RowIndex == gvProduct.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        decimal temp = 0;
                        SqlCommand cmd = new SqlCommand(@"UPDATE tblProduct SET productCompany = @productCompany, 
                                                                 productName = @productName, 
                                                                 productUnit = @productUnit, 
                                                                 productStock = @productStock, 
                                                                 productPurchasePrice = @productPurchasePrice, 
                                                                 productMRP = @productMRP, 
                                                                 productSalesPrice = @productSalesPrice, 
                                                                 productDiscount = @productDiscount, 
                                                                 productHSN = @productHSN, 
                                                                 productMinimumStock = @productMinimumStock, 
                                                                 productMaximumStock = @productMaximumStock, 
                                                                 description1 = @description1, 
                                                                 description2 = @description2, 
                                                                 description3 = @description3, 
                                                                 description4 = @description4, 
                                                                 description5 = @description5, 
                                                                 productShortCode = @productShortCode, 
                                                                 productStockType = @productStockType, 
                                                                 productReorder = @productReorder, 
                                                                 productDiscountRs = @productDiscountRs, 
                                                                 productStoreLocation = @productStoreLocation, 
                                                                 productIGST = @productIGST, 
                                                                 productCGST = @productCGST, 
                                                                 productSGST = @productSGST, 
                                                                 productMasterPacking = @productMasterPacking, 
                                                                 productSaleAper = @productSaleAper, 
                                                                 productSaleBper = @productSaleBper, 
                                                                 productSaleCper = @productSaleCper, 
                                                                 productSaleDper = @productSaleDper, 
                                                                 productSaleArs = @productSaleArs, 
                                                                 productSaleBrs = @productSaleBrs, 
                                                                 productSaleCrs = @productSaleCrs, 
                                                                 productSaleDrs = @productSaleDrs, 
                                                                 productNote = @productNote 
                                                                 WHERE productId = @productId", con);

                        cmd.Parameters.AddWithValue("@productCompany", txtCompany.Text);
                        cmd.Parameters.AddWithValue("@productName", txtName.Text);
                        cmd.Parameters.AddWithValue("@productUnit", txtUnit.Text);
                        cmd.Parameters.AddWithValue("@productStock", txtStock.Text);
                        cmd.Parameters.AddWithValue("@productPurchasePrice", txtPurchasePrice.Text);

                        decimal.TryParse(txtMRP.Text, out temp);
                        cmd.Parameters.AddWithValue("@productMRP", temp);
                        temp = 0;

                        cmd.Parameters.AddWithValue("@productSalesPrice", txtSalesPrice.Text);

                        decimal.TryParse(txtDiscount.Text, out temp);
                        cmd.Parameters.AddWithValue("@productDiscount", temp);
                        temp = 0;

                        cmd.Parameters.AddWithValue("@productHSN", txtHSN.Text);
                        cmd.Parameters.AddWithValue("@productMinimumStock", txtMinimumStock.Text);
                        cmd.Parameters.AddWithValue("@productMaximumStock", txtMaximumStock.Text);
                        cmd.Parameters.AddWithValue("@subtypeId", ddlSubType.SelectedValue);
                        cmd.Parameters.AddWithValue("@description1", txtKey1.Text + txtValue1.Text);
                        cmd.Parameters.AddWithValue("@description2", txtKey2.Text + txtValue2.Text);
                        cmd.Parameters.AddWithValue("@description3", txtKey3.Text + txtValue3.Text);
                        cmd.Parameters.AddWithValue("@description4", txtKey4.Text + txtValue4.Text);
                        cmd.Parameters.AddWithValue("@description5", txtKey5.Text + txtValue5.Text);
                        cmd.Parameters.AddWithValue("@productShortCode", txtShortCode.Text);
                        cmd.Parameters.AddWithValue("@productStockType", ddlStockType.SelectedValue);

                        cmd.Parameters.AddWithValue("@productReorder", txtReOrder.Text);
                        decimal.TryParse(txtDiscountAmt.Text, out temp);
                        cmd.Parameters.AddWithValue("@productDiscountRs", temp);
                        temp = 0;

                        cmd.Parameters.AddWithValue("@productStoreLocation", txtStoreLocation.Text);

                        decimal.TryParse(txtIGSTRATE.Text, out temp);
                        cmd.Parameters.AddWithValue("@productIGST", temp);
                        temp = 0;

                        decimal.TryParse(txtCGSTRATE.Text, out temp);
                        cmd.Parameters.AddWithValue("@productCGST", temp);
                        temp = 0;

                        decimal.TryParse(txtSGSTRATE.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSGST", temp);
                        temp = 0;

                        cmd.Parameters.AddWithValue("@productMasterPacking", txtMasterPacking.Text);

                        decimal.TryParse(txtSaleRateAPer.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSaleAper", temp);
                        temp = 0;

                        decimal.TryParse(txtSaleRateBPer.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSaleBper", temp);
                        temp = 0;

                        decimal.TryParse(txtSaleRateCPer.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSaleCper", temp);
                        temp = 0;

                        decimal.TryParse(txtSaleRateDPer.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSaleDper", temp);
                        temp = 0;

                        decimal.TryParse(txtSaleRateARs.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSaleArs", temp);
                        temp = 0;

                        decimal.TryParse(txtSaleRateBRs.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSaleBrs", temp);
                        temp = 0;

                        decimal.TryParse(txtSaleRateCRs.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSaleCrs", temp);
                        temp = 0;

                        decimal.TryParse(txtSaleRateDRs.Text, out temp);
                        cmd.Parameters.AddWithValue("@productSaleDrs", temp);
                        temp = 0;

                        cmd.Parameters.AddWithValue("@productNote", txtNote.Text);

                        cmd.Parameters.AddWithValue("@productId", (row.FindControl("lblproductId") as Label).Text);

                        /* cmd.Parameters.AddWithValue("@productCompany", txtCompany.Text);
                         cmd.Parameters.AddWithValue("@productName", txtName.Text);
                         cmd.Parameters.AddWithValue("@productUnit", txtUnit.Text);
                         cmd.Parameters.AddWithValue("@productStock", txtStock.Text);
                         cmd.Parameters.AddWithValue("@productPurchasePrice", txtPurchasePrice.Text);
                         cmd.Parameters.AddWithValue("@productMRP", txtMRP.Text);
                         cmd.Parameters.AddWithValue("@productSalesPrice", txtSalesPrice.Text);
                         cmd.Parameters.AddWithValue("@productDiscount", txtDiscount.Text);
                         cmd.Parameters.AddWithValue("@productHSN", txtHSN.Text);
                         cmd.Parameters.AddWithValue("@productMinimumStock", txtMinimumStock.Text);
                         cmd.Parameters.AddWithValue("@productMaximumStock", txtMaximumStock.Text);
                         cmd.Parameters.AddWithValue("@subtypeId", ddlSubType.SelectedValue);
                         cmd.Parameters.AddWithValue("@description1", txtKey1.Text + txtValue1.Text);
                         cmd.Parameters.AddWithValue("@description2", txtKey2.Text + txtValue2.Text);
                         cmd.Parameters.AddWithValue("@description3", txtKey3.Text + txtValue3.Text);
                         cmd.Parameters.AddWithValue("@description4", txtKey4.Text + txtValue4.Text);
                         cmd.Parameters.AddWithValue("@description5", txtKey5.Text + txtValue5.Text);
                         cmd.Parameters.AddWithValue("@productShortCode", txtShortCode.Text);
                         cmd.Parameters.AddWithValue("@productStockType", ddlStockType.SelectedValue);
                         cmd.Parameters.AddWithValue("@productReorder", txtReOrder.Text);
                         cmd.Parameters.AddWithValue("@productDiscountRs", txtDiscountAmt.Text);
                         cmd.Parameters.AddWithValue("@productStoreLocation", txtStoreLocation.Text);
                         cmd.Parameters.AddWithValue("@productIGST", txtIGSTRATE.Text);
                         cmd.Parameters.AddWithValue("@productCGST", txtCGSTRATE.Text);
                         cmd.Parameters.AddWithValue("@productSGST", txtSGSTRATE.Text);
                         cmd.Parameters.AddWithValue("@productMasterPacking", txtMasterPacking.Text);
                         cmd.Parameters.AddWithValue("@productSaleAper", txtSaleRateAPer.Text);
                         cmd.Parameters.AddWithValue("@productSaleBper", txtSaleRateBPer.Text);
                         cmd.Parameters.AddWithValue("@productSaleCper", txtSaleRateCPer.Text);
                         cmd.Parameters.AddWithValue("@productSaleDper", txtSaleRateDPer.Text);
                         cmd.Parameters.AddWithValue("@productSaleArs", txtSaleRateARs.Text);
                         cmd.Parameters.AddWithValue("@productSaleBrs", txtSaleRateBRs.Text);
                         cmd.Parameters.AddWithValue("@productSaleCrs", txtSaleRateCRs.Text);
                         cmd.Parameters.AddWithValue("@productSaleDrs", txtSaleRateDRs.Text);
                         cmd.Parameters.AddWithValue("@productNote", txtNote.Text);
                         cmd.Parameters.AddWithValue("@productId", (row.FindControl("lblproductId") as Label).Text);*/

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
                btnUpdateProduct.Visible = false;
                btnAddProduct.Visible = true;
            }
        }
    }
}