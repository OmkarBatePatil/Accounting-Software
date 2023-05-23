using Gnostice.Core.DOM;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using TextBox = System.Web.UI.WebControls.TextBox;
using Label = System.Web.UI.WebControls.Label;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Path = System.IO.Path;
using CommandType = System.Data.CommandType;
using ListItem = System.Web.UI.WebControls.ListItem;
using static CSJ2K.j2k.codestream.HeaderInfo;
using Gnostice.Documents.Word;
using Newtonsoft.Json.Linq;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Gnostice.Documents.Spreadsheet;
using System.Text;
using Paragraph = iTextSharp.text.Paragraph;
using System.Runtime.Remoting.Messaging;
using System.Windows.Documents;

namespace GNEnterprise
{
    public partial class WebForm13 : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["dbGNEnterpriseConStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownParty();
                DropDown();
                for (int i = 1; i <= 20; i++)
                {
                    string quantityTextboxId = "txtQuantity" + i;
                    string unitPriceTextboxId = quantityTextboxId.Replace("txtQuantity", "txtUnitPrice");
                    //ContentPlaceHolder contentPlaceHolder = this.Master.FindControl("MainContent") as ContentPlaceHolder;
                    //TextBox txt = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtId);
                    TextBox quantityTextbox = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(quantityTextboxId);
                    TextBox unitPriceTextbox = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(unitPriceTextboxId);

                    if (quantityTextbox != null)
                    {
                        quantityTextbox.TextChanged += new EventHandler(CalculateTotal);
                        quantityTextbox.AutoPostBack = true;
                    }
                    if (unitPriceTextbox != null)
                    {
                        unitPriceTextbox.TextChanged += new EventHandler(CalculateTotal);
                        unitPriceTextbox.AutoPostBack = true;
                    }
                }                
            }
        }
        protected void DropDown()
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT productId, productName FROM tblProduct", conn);
                conn.Open(); // Open the connection
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader); // Read data into DataTable

                ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                if (cph != null)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        string ddlName = "ddlProductName" + i.ToString();
                        DropDownList ddl = (DropDownList)cph.FindControl(ddlName);
                        if (ddl != null)
                        {
                            ddl.DataSource = dt;
                            ddl.DataValueField = "productId";
                            ddl.DataTextField = "productName";
                            ddl.DataBind();
                            ddl.Items.Insert(0, new ListItem("Select Product", ""));

                        }
                    }
                }
                reader.Close();
            }
        }       
        protected void DropDownParty()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                ddlPartyName.Items.Insert(0, new ListItem("Select Party", ""));
                ddlPartyName.AppendDataBoundItems = true;
                String strQuery = "SELECT partyId, partyName FROM tblParty";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    ddlPartyName.DataSource = cmd.ExecuteReader();
                    ddlPartyName.DataTextField = "partyName";
                    ddlPartyName.DataValueField = "partyId";
                    ddlPartyName.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        /*
             protected void Dropdown_SelectedIndexChanged1(object sender, EventArgs e)
         {
             DropDownList ddl = (DropDownList)sender;
             string ddlId = ddl.ID.Trim();
             string ddlIdNo = ddlId.Substring(Math.Max(0, ddlId.Length - 1));
             //TextBox textBox = new TextBox();
             //textBox.ID = "txtUnitPrice" + ddlIdNo;
             using (SqlConnection con = new SqlConnection(CS))
             {
                 String strQuery = "SELECT productMRP FROM tblProduct WHERE productId = @productId";
                 SqlCommand cmd = new SqlCommand();
                 cmd.Parameters.AddWithValue("@productId", ddl.SelectedValue);
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandText = strQuery;
                 cmd.Connection = con;
                 try
                 {
                     con.Open();
                     SqlDataReader sdr = cmd.ExecuteReader();
                     while (sdr.Read())
                     {
                         switch (ddlIdNo)
                         {
                             case "1":
                                 txtUnitPrice1.Text = sdr[0].ToString();
                                 break;
                             case "2":
                                 txtUnitPrice2.Text = sdr[0].ToString();
                                 break;
                             case "3":
                                 txtUnitPrice3.Text = sdr[0].ToString();
                                 break;
                             case "4":
                                 txtUnitPrice4.Text = sdr[0].ToString();
                                 break;
                             case "5":
                                 txtUnitPrice5.Text = sdr[0].ToString();
                                 break;
                             case "6":
                                 txtUnitPrice6.Text = sdr[0].ToString();
                                 break;
                             case "7":
                                 txtUnitPrice7.Text = sdr[0].ToString();
                                 break;
                             case "8":
                                 txtUnitPrice8.Text = sdr[0].ToString();
                                 break;
                             case "9":
                                 txtUnitPrice9.Text = sdr[0].ToString();
                                 break;
                             case "10":
                                 txtUnitPrice10.Text = sdr[0].ToString();
                                 break;
                             default:
                                 break;
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 finally
                 {
                     con.Close();
                     con.Dispose();
                 }
             }
         }
             */
        protected void Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string ddlId = ddl.ID;
            //string txtUnitPriceId = "txtUnitPrice" + ddlId.Substring("ddlProductName".Length);
            //TextBox txtUnitPrice = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtUnitPriceId);
            string txtQuantityId = "txtQuantity" + ddlId.Substring("ddlProductName".Length);
            TextBox txtQuantity = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtQuantityId);
            string txtUnitId = "txtUnit" + ddlId.Substring("ddlProductName".Length);
            TextBox txtUnit = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtUnitId);
            string txtUnitPriceId = "txtUnitPrice" + ddlId.Substring("ddlProductName".Length);
            TextBox txtUnitPrice = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtUnitPriceId);
            string txtHSNId = "txtHSN" + ddlId.Substring("ddlProductName".Length);
            TextBox txtHSN = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtHSNId);
            string lblTotalId = "lblTotal" + ddlId.Substring("ddlProductName".Length);
            Label lblTotal = (Label)Master.FindControl("ContentPlaceHolder1").FindControl(lblTotalId);

            if (ddl.SelectedItem != null && txtUnitPrice != null && txtHSN != null)
            {
                if (ddl.SelectedIndex == 0)
                {
                    txtHSN.Text = "";
                    txtQuantity.Text = "";
                    txtUnitPrice.Text = "";
                    lblTotal.Text = "";
                    CalculateTotal();
                    return;
                }

                string query = "SELECT productUnit,productMRP,productHSN FROM tblProduct WHERE productId = @productId";

                using (SqlConnection conn = new SqlConnection(CS))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@productId", ddl.SelectedValue);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Retrieve the values from the result and display them in textboxes
                        txtUnit.Text = reader.GetString(0);
                        txtUnitPrice.Text = reader.GetDecimal(1).ToString();
                        txtHSN.Text = reader.GetString(2);
                    }
                    else
                    {
                        txtHSN.Text = "";
                        txtQuantity.Text = "";
                        txtUnit.Text = "";
                        txtUnitPrice.Text = "";
                        lblTotal.Text = "";
                        return;
                    }

                    CalculateTotal();
                    
                    //object result = cmd.ExecuteScalar();

                    //if (result != null)
                    //{
                    //    txtUnitPrice.Text = result.ToString();
                    //    txtHSN.Text = result.ToString();
                    //}
                }
            }
        }

        protected void Dropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string ddlId = ddl.ID;

            string txtAddressId = "txtAddress" + ddlId.Substring("ddlPartyName".Length);
            TextBox txtAddress = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtAddressId);

           /* string txtGSTNoId = "txtGstNo" + ddlId.Substring("ddlPartyName".Length);
           // TextBox txtGstNo = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtGSTNoId);

            string txtUnitPriceId = "txtGSTNO" + ddlId.Substring("ddlPartyName".Length);
            //TextBox txtGSTNO = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtUnitPriceId);

            //string txtQuantityId = "txtQuantity" + ddlId.Substring("ddlProductName".Length);
            //TextBox txtQuantity = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtQuantityId);

            //string txtHSNId = "txtHSN" + ddlId.Substring("ddlProductName".Length);
            //TextBox txtHSN = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtHSNId);
            //string lblTotalId = "lblTotal" + ddlId.Substring("ddlProductName".Length);
            Label lblTotal = (Label)Master.FindControl("ContentPlaceHolder1").FindControl(lblTotalId);*/


            if (ddl.SelectedItem != null && txtAddress != null)
            {
                if (ddl.SelectedIndex == 0)
                {
                    txtAddress.Text = "";
                    //txtGstNo.Text = "";
                    return;
                }

                string query = "SELECT partyAddress FROM tblParty WHERE partyId = @partyId";

                using (SqlConnection conn = new SqlConnection(CS))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@partyId", ddl.SelectedValue);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Retrieve the values from the result and display them in textboxes
                        txtAddress.Text = reader.GetString(0).ToString();
                        //txtGstNo.Text = reader.GetString(1).ToString();
                    }
                    else
                    {
                        txtGstNo.Text = "";
                        txtAddress.Text = "";
                        return;
                    }
                }
            }
        }

        /*
                protected void CalculateTotal(string quantityTextboxId, string unitPriceTextboxId)
                {
                    // Get the quantity textbox
                    TextBox quantityTextbox = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(quantityTextboxId);

                    // Get the unit price textbox
                    TextBox unitPriceTextbox = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(unitPriceTextboxId);

                    // Get the total label
                    Label totalLabel = (Label)FindControl("lblTotal" + quantityTextboxId.Substring(quantityTextboxId.Length));

                    // Check if the textboxes are not null and have values
                    if (quantityTextbox != null && unitPriceTextbox != null
                        && !string.IsNullOrEmpty(quantityTextbox.Text)
                        && !string.IsNullOrEmpty(unitPriceTextbox.Text))
                    {
                        // Parse the values of the textboxes
                        int quantity = int.Parse(quantityTextbox.Text);
                        decimal unitPrice = decimal.Parse(unitPriceTextbox.Text);

                        // Calculate the total
                        decimal total = quantity * unitPrice;

                        // Set the total to the label
                        totalLabel.Text = total.ToString();
                    }
                    else
                    {
                        // If any of the textboxes is null or does not have a value, set the total label to empty
                        totalLabel.Text = string.Empty;
                    }
                }
                */
        protected void CalculateTotal(object sender, EventArgs e)
        {
            TextBox Textbox = sender as TextBox;
            string quantityTextboxId = "";
            string unitPriceTextboxId = "";
            if (Textbox != null)
            {
                string TextboxId = Textbox.ID;
                if (TextboxId.Contains("txtQuantity"))
                {
                    quantityTextboxId = TextboxId;
                    unitPriceTextboxId = TextboxId.Replace("txtQuantity", "txtUnitPrice");
                }
                else if (TextboxId.Contains("txtUnitPrice"))
                {
                    unitPriceTextboxId = TextboxId;
                    quantityTextboxId = TextboxId.Replace("txtUnitPrice", "txtQuantity");
                }
                string totalLabelId = quantityTextboxId.Replace("txtQuantity", "lblTotal");

                ContentPlaceHolder contentPlaceHolder = this.Master.FindControl("MainContent") as ContentPlaceHolder;
                //TextBox quantityTextbox = contentPlaceHolder.FindControl(quantityTextboxId) as TextBox;
                TextBox quantityTextbox = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(quantityTextboxId);
                TextBox unitPriceTextbox = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(unitPriceTextboxId);
                //TextBox unitPriceTextbox = contentPlaceHolder.FindControl(unitPriceTextboxId) as TextBox;
                Label totalLabel = (Label)Master.FindControl("ContentPlaceHolder1").FindControl(totalLabelId);
                //Label totalLabel = contentPlaceHolder.FindControl(totalLabelId) as Label;

                if (unitPriceTextbox != null && totalLabel != null)
                {
                    decimal quantity, unitPrice;
                    bool isQuantityValid = decimal.TryParse(quantityTextbox.Text, out quantity);
                    bool isUnitPriceValid = decimal.TryParse(unitPriceTextbox.Text, out unitPrice);

                    if (isQuantityValid && isUnitPriceValid)
                    {
                        decimal total = quantity * unitPrice;
                        totalLabel.Text = total.ToString("N", new CultureInfo("en-IN"));
                        CalculateTotal();
                    }
                    else
                    {
                        totalLabel.Text = "";
                    }
                }
            }           
        }

        private void CalculateTotalWithGST()
        {
            decimal grandTotal = Convert.ToDecimal(lblTotalValue.Text);
            decimal.TryParse(lblTotalValue.Text.Replace(",", ""), out grandTotal);
            int total = Convert.ToInt32(grandTotal);
            string totalInWords = ConvertNumberToWords(total);
            lblGrandTotalValueWithGSTAfterRoundOffInWords.Text = totalInWords.ToUpper()+"ONLY";

        }

        private void CalculateTotal()
        {
            decimal total = 0, valuesWithoutCommas = 0;
            ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            for (int i = 1; i <= 20; i++)
            {
                Label lbl = (Label)cph.FindControl("lblTotal" + i.ToString());
                if (lbl != null && !string.IsNullOrEmpty(lbl.Text))
                {
                    valuesWithoutCommas = 0;
                    decimal.TryParse(lbl.Text.Replace(",", ""), out valuesWithoutCommas);
                    total += valuesWithoutCommas;
                }
            }
            lblTotalValue.Text = total.ToString("N", new CultureInfo("en-IN"));
            CalculateTotalWithGST();
        }

        //protected void ddlGST_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CalculateTotalWithGST();
        //}
        //private void CalculateTotalWithGST()
        //{
        //    decimal grandTotal = Convert.ToDecimal(lblTotalValue.Text);
        //    //decimal.TryParse(lblTotalValue.Text.Replace(",", ""), out grandTotal);
        //    //decimal gstPercent = Convert.ToDecimal(ddlGST.SelectedValue);
        //    //decimal gstValue = (grandTotal * gstPercent) / 100;
        //   // decimal grandTotalWithGst = grandTotal + gstValue;
        //    //lblTotalGST.Text = gstValue.ToString("N", new CultureInfo("en-IN"));
        //    //decimal roundOffValue = Math.Round(grandTotalWithGst) - grandTotalWithGst;
        //    //lblRoundOffValue.Text = roundOffValue.ToString("N", new CultureInfo("en-IN"));
        //   // int finalTotal = Convert.ToInt32(grandTotalWithGst);
        //    //lblGrandTotalValueWithGSTAfterRoundOffValue.Text = Math.Round(grandTotalWithGst, MidpointRounding.AwayFromZero).ToString("N", new CultureInfo("en-IN"));
        //    //string totalInWords = ConvertNumberToWords(finalTotal);
        //    //lblGrandTotalValueWithGSTAfterRoundOffInWords.Text = totalInWords.ToUpper();
        //}

        public static string ConvertNumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ConvertNumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 10000000) > 0)
            {
                words += ConvertNumberToWords(number / 10000000) + " crore ";
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words += ConvertNumberToWords(number / 100000) + " lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }


        //protected void linkBtnInsert_Click(object sender, EventArgs e)
        //{
        //    SqlDataTemp.InsertParameters["tempSalesItemProductId"].DefaultValue =
        //      ((DropDownList)gvSales.FooterRow.FindControl("ddlProductAdd")).SelectedValue;

        //    SqlDataTemp.InsertParameters["tempSalesItemQuantity"].DefaultValue =
        //        ((TextBox)gvSales.FooterRow.FindControl("txtQuantity")).Text;

        //    SqlDataTemp.InsertParameters["tempSalesItemTotal"].DefaultValue =
        //        ((TextBox)gvSales.FooterRow.FindControl("txtTotal")).Text;

        //    SqlDataTemp.InsertParameters["tempSalesItemUnitPrice"].DefaultValue =
        //        ((TextBox)gvSales.FooterRow.FindControl("txtUnitPrice")).Text;

        //    SqlDataTemp.Insert();

        //}


        //protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList dropDownList = gvSales.FindControl("ddlProduct") as DropDownList;
        //    string id = dropDownList.ID;           
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + dropDownList.SelectedItem.Text + "');", true);
        //}

        //protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        //{


        //    //GridViewRow row = sender as GridViewRow;
        //    //int rowindex = row.RowIndex;
        //    //rowindex = gvSales.SelectedIndex;
        //    //TextBox txtUnitPriceView = (TextBox)row.FindControl("txtUnitPriceView");
        //    //DropDownList dropDownList = sender as DropDownList;
        //    //txtUnitPriceView.Text = dropDownList.SelectedValue;
        //    //DropDownList dropDownList = (DropDownList)gvSales.FindControl("ddlProductAdd");
        //    //TextBox txtUnitPriceView = (TextBox)gvSales.FindControl("txtUnitPriceView");
        //    //foreach (GridViewRow row in gvSales.Rows)
        //    //{
        //    //    if (row.RowIndex == gvSales.SelectedIndex)
        //    //    {
        //    //        using (SqlConnection con = new SqlConnection(CS))
        //    //        {
        //    //            String strQuery = "SELECT productMRP FROM tblProduct where productId = @productId";
        //    //            //SqlConnection con = new SqlConnection(strConnString);
        //    //            SqlCommand cmd = new SqlCommand();
        //    //            cmd.Parameters.AddWithValue("@productId", dropDownList.SelectedValue);
        //    //            cmd.CommandType = CommandType.Text;
        //    //            cmd.CommandText = strQuery;
        //    //            cmd.Connection = con;
        //    //            try
        //    //            {
        //    //                con.Open();
        //    //                SqlDataReader sdr = cmd.ExecuteReader();
        //    //                while (sdr.Read())
        //    //                {
        //    //                    txtUnitPriceView.Text = dropDownList.SelectedValue;
        //    //                    // lblCountry.Text = sdr["Country"].ToString();
        //    //                }
        //    //            }
        //    //            catch (Exception ex)
        //    //            {
        //    //                throw ex;
        //    //            }
        //    //            finally
        //    //            {
        //    //                con.Close();
        //    //                con.Dispose();
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}


        //protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
        //{            
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSales, "Select$" + e.Row.RowIndex);
        //        e.Row.ToolTip = "Click to select this row.";
        //    }

        //    //using (SqlConnection con = new SqlConnection(CS))
        //    //{
        //    //    con.Open();
        //    //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    //    {
        //    //        DropDownList DropDownList1 = e.Row.FindControl("ddlProduct") as DropDownList;
        //    //        SqlCommand cmd = new SqlCommand("select * from tblProduct", con);
        //    //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    //        DataTable dt = new DataTable();
        //    //        sda.Fill(dt);
        //    //        con.Close();
        //    //        DropDownList1.DataSource = dt;
        //    //        DropDownList1.DataTextField = "productName";
        //    //        DropDownList1.DataValueField = "productId";
        //    //        DropDownList1.DataBind();
        //    //        DropDownList1.Items.Insert(0, new ListItem("Select Product", "0"));
        //    //    }
        //    //}
        //}

        //protected void gvSales_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //DropDownList dropDownList = gvSales.FindControl("ddlProduct") as DropDownList;
        //   // DropDownList dropDownList = sender as DropDownList;
        //    //string id = dropDownList.ID;
        //    foreach (GridViewRow row in gvSales.Rows)
        //    {
        //        if (row.RowIndex == gvSales.SelectedIndex)
        //        {
        //            lblSaleValue.Text = gvSales.Rows[gvSales.SelectedIndex].RowIndex.ToString();

        //            row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
        //            row.ToolTip = string.Empty;
        //        }
        //        else
        //        {
        //            row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        //            row.ToolTip = "Click to select this row.";
        //        }
        //    }
        //}



        //void Get_Xml()
        //{
        //    DataSet dataSet = new DataSet();
        //    dataSet.ReadXml(Server.MapPath("~/Data/Item.xml"));
        //    if (dataSet != null && dataSet.HasChanges())
        //    {
        //        gvItem.DataSource = dataSet;
        //        gvItem.DataBind();
        //    }
        //}
        //protected void linkBtnInsert_Click(object sender, EventArgs e)
        //{
        //    Insert_XML();
        //}
        //void Insert_XML()
        //{
        //    TextBox txtProductName = (TextBox)gvSales.FooterRow.FindControl("txtProductName");
        //    TextBox txtQuantity = (TextBox)gvSales.FooterRow.FindControl("txtQuantity");
        //    TextBox txtUnitPrice = (TextBox)gvSales.FooterRow.FindControl("txtUnitPrice");
        //    TextBox txtTotal = (TextBox)gvSales.FooterRow.FindControl("txtTotal");

        //    XmlDocument MyXmlDocument = new XmlDocument();
        //    MyXmlDocument.Load(Server.MapPath("~/Data/Item.xml"));

        //    XmlElement ParentElement = MyXmlDocument.CreateElement("Item");

        //    XmlElement ProductName = MyXmlDocument.CreateElement("ProductName");
        //    ProductName.InnerText = txtProductName.Text;

        //    XmlElement Quantity = MyXmlDocument.CreateElement("Quantity");
        //    Quantity.InnerText = txtQuantity.Text;

        //    XmlElement UnitPrice = MyXmlDocument.CreateElement("UnitPrice");
        //    UnitPrice.InnerText = txtUnitPrice.Text;

        //    XmlElement Total = MyXmlDocument.CreateElement("Total");
        //    Total.InnerText = txtTotal.Text;

        //    ParentElement.AppendChild(ProductName);
        //    ParentElement.AppendChild(Quantity);
        //    ParentElement.AppendChild(UnitPrice);
        //    ParentElement.AppendChild(Total);

        //    MyXmlDocument.DocumentElement.AppendChild(ParentElement);
        //    MyXmlDocument.Save(Server.MapPath("~/Data/Item.xml"));

        //    //Get_Xml();
        //}

        // protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvItem, "Select$" + e.Row.RowIndex);
        //        e.Row.ToolTip = "Click to select this row.";
        //    }
        //    //if (e.Row.RowType == DataControlRowType.DataRow)
        //    //{
        //    //    int index = e.Row.RowIndex;
        //    //}
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        con.Open();

        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            DropDownList DropDownList1 = (e.Row.FindControl("ddlProductName") as DropDownList);
        //            SqlCommand cmd = new SqlCommand("select * from tblProduct", con);
        //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            sda.Fill(dt);
        //            con.Close();
        //            DropDownList1.DataSource = dt;
        //            DropDownList1.DataTextField = "productName";
        //            DropDownList1.DataValueField = "productId";
        //            DropDownList1.DataBind();
        //            DropDownList1.Items.Insert(0, new ListItem("Select Product", "0"));

        //        }
        //    }
        //}
        //protected void ddlProductName_OnSelectedIndexChanged(object sender, EventArgs e)
        //{            
        //            //String strConnString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        //            DropDownList dropDownList = sender as DropDownList;
        //            Label lblUnitPrice = (Label)gvItem.FindControl("UnitPrice");
        //            //lblUnitPrice.Text = dropDownList.SelectedValue;

        //           // DropDownList dropDownList = (DropDownList)gvItem.FindControl("ddlProductName");
        //            //TextBox txtUnitPrice = (TextBox)gvItem.FindControl("txtUnitPrice");
        //           // lblUnitPrice.Text = gvItem.FindControl("UnitPrice").ToString();
        //            using (SqlConnection con = new SqlConnection(CS))
        //            {
        //                String strQuery = "SELECT productMRP FROM tblProduct where productId = @productId";
        //                //SqlConnection con = new SqlConnection(strConnString);
        //                SqlCommand cmd = new SqlCommand();
        //                cmd.Parameters.AddWithValue("@productId", dropDownList.SelectedValue);
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandText = strQuery;
        //                cmd.Connection = con;
        //                try
        //                {
        //                    con.Open();
        //                    SqlDataReader sdr = cmd.ExecuteReader();
        //                    while (sdr.Read())
        //                    {
        //                        lblUnitPrice.Text = dropDownList.SelectedValue;
        //                        // lblCountry.Text = sdr["Country"].ToString();
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw ex;
        //                }
        //                finally
        //                {
        //                    con.Close();
        //                    con.Dispose();
        //                }
        //            }

        //            //DropDownList dropDownList = sender as DropDownList;
        //            //DropDownList dropDownList = (DropDownList)gvItem.FindControl("ddlProductName");
        //            //string id = dropDownList.ID;                    
        //            // TextBox txtUnitPrice = (TextBox)gvItem.FindControl("txtUnitPrice");
        //            // txtUnitPrice.Text = dropDownList.SelectedValue;                    
        //            ////ListViewDataItem item = dropDownList.NamingContainer as ListViewDataItem;
        //            //string query = "select productMRP from tblProduct where productId = " + dropDownList.SelectedValue + "";
        //            ////var selectedValue = dropDownList.SelectedValue;
        //            //txtUnitPrice.Text = query;
        //            //using (SqlConnection con = new SqlConnection(CS))
        //            //{
        //            //    con.Open();
        //            //    DropDownList dropDownList = sender as DropDownList;
        //            //    TextBox txtUnitPrice = (TextBox)gvItem.FindControl("txtUnitPrice");
        //            //    //Get the ID of the DropDownList.
        //            //    string id = dropDownList.ID;
        //            //    SqlCommand cmd = new SqlCommand("select productMRP from tblProduct where productId'"+ dropDownList.SelectedItem.Text + "'", con);
        //            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //            //    DataTable dt = new DataTable();
        //            //    sda.Fill(dt);
        //            //    con.Close();
        //            //    txtUnitPrice.DataSource = dt;
        //            //   // dropDownList.DataTextField = "productName";
        //            //    //dropDownList.DataValueField = "productId";
        //            //    dropDownList.DataBind();
        //            //    //gvItem.FindControl("txtUnitPrice");
        //            //    ////txtUnitPrice = dropDownList.SelectedItem.Text;
        //            //Display the Selected Text of DropDownList.
        //            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + dropDownList.SelectedItem.Text + gvItem.Rows + "');", true);
        //            //}                    
        //        }
        //protected void gvItem_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblSaleIDValue.Text = "" + gvItem.SelectedIndex;
        //    //foreach (GridViewRow row in gvItem.Rows)
        //    //{
        //    //    //DropDownList dropDownList = sender as DropDownList;
        //    //    //string id = dropDownList.ID;
        //    //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + dropDownList.SelectedItem.Text + "');", true);
        //    //    //if (row.RowIndex == gvItem.SelectedIndex)
        //    //    //{
        //    //    //    row.ToolTip = string.Empty;
        //    //    //    DropDownList dropDownList = sender as DropDownList;
        //    //    //    TextBox txtUnitPrice = (TextBox) gvItem.FindControl("txtUnitPrice");
        //    //    //    txtUnitPrice.Text = dropDownList.SelectedValue;
        //    //    //}
        //    //}
        //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('"+"hi"+"');", true);
        //}        

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            decimal grandTotal;
            int quotationId = 0;

            string time = DateTime.Now.ToString("h:mm:ss tt");
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                decimal.TryParse(lblTotalValue.Text.Replace(",", ""), out grandTotal);
                //decimal.TryParse(lblTotalGST.Text.Replace(",", ""), out gstAmt);
                

                string inputDate = txtDate.Text;
                DateTime date = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string outputDate = date.ToString("yyyy-MM-dd");

                decimal.TryParse(lblTotalValue.Text.Replace(",", ""), out grandTotal);
                /* SqlCommand cmd = new SqlCommand("insert into tblQuotation (quotationDate,quotationPartyId,quotationTotalAmount,quotationSRNo1,quotationDescription1,quotationSRNo2,quotationSRNo3,quotationDescription2,quotationDescription3) " +
                                                    "values('" + outputDate + "'," + ddlPartyName.SelectedValue + "," + grandTotal + "," + txtSRNo1.Text + ",'" + txtDescription1.Text + "','" + txtSRNo2.Text + "','" + txtSRNo3.Text + "','" + txtDescription2.Text + "','" + txtDescription3.Text + "');", con);*/

                SqlCommand cmd = new SqlCommand("insert into tblQuotation (quotationDate,quotationPartyId,quotationTotalAmount,quotationSRNo1,quotationDescription1,quotationSRNo2,quotationSRNo3,quotationDescription2,quotationDescription3) " +
                                                "values('" + outputDate + "'," + ddlPartyName.SelectedValue + "," + grandTotal + ",'" + txtPaymentMode.Text + "','" + txtDescription1.Text + "','" + txtSRNo2.Text + "','" + txtSRNo3.Text + "','" + txtDescription2.Text + "','" + txtDescription3.Text + "')", con);

                cmd.ExecuteNonQuery();
                //clear();
                ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                string inserttblSalesItem = "";
                for (int i = 1; i <= 20; i++)
                {
                    DropDownList ddl = (DropDownList)cph.FindControl("ddlProductName" + i.ToString());
                    if (ddl != null && ddl.SelectedIndex != 0)
                    {
                        TextBox quantityTextbox = (TextBox)cph.FindControl("txtQuantity" + i.ToString());
                        TextBox unitPriceTextbox = (TextBox)cph.FindControl("txtUnitPrice" + i.ToString());
                        Label lblTotal = (Label)cph.FindControl("lblTotal" + i.ToString());
                        if (quantityTextbox == null)
                        {
                            break;
                        }
                        if (unitPriceTextbox == null)
                        {
                            break;
                        }
                        if (lblTotal == null)
                        {
                            break;
                        }                   
                        cmd = new SqlCommand("SELECT MAX(quotationId) FROM tblQuotation", con);
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            quotationId = Convert.ToInt32(result);
                        }                      
                        decimal total = 0;
                        decimal.TryParse(lblTotal.Text.Replace(",", ""), out total);
                        inserttblSalesItem += @"INSERT INTO tblQuotationItem VALUES(" + quantityTextbox.Text + "," + quotationId + "," + ddl.SelectedValue + "," + unitPriceTextbox.Text +");";
                    }                    
                }
                cmd = new SqlCommand(inserttblSalesItem, con);
                cmd.ExecuteNonQuery();
            }
            /*
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();                
                SqlCommand cmd = new SqlCommand("insert into tblSalesItem (itemSalesQuantity," +
                                                                            "saleId," +
                                                                            "productId," +
                                                                            "itemSalesUnitPrice," +
                                                                            "itemSalesAmount) " +
                                                                            "values('" + txtDate.Text + "'," + ddlPartyName.SelectedValue + "," + grandTotal + "," + lblTotalGST.Text + ")", con);
                cmd.ExecuteNonQuery();
                //clear();
            }
            */
            string originalFile = Server.MapPath("~/PDF/GNEnterpriseV2Quotation.pdf");
           // string newFile = @"C:\Users\Admin\Downloads\GNEnterpriseQuotation.pdf";
            //tring newFile = @"C:\Software\GNEnterprises\PDF\GNEnterprise_Quotation.pdf";
            string newFile = @"C:\Software\PDF\GNEnterprises\Quotation\" + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + "GNEnterpriseQuotation.pdf";

            using (var reader = new PdfReader(originalFile))
            {
                using (var fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newFile), FileMode.Create, FileAccess.Write))
                {
                    var document = new Document(reader.GetPageSizeWithRotation(1));
                    PdfWriter writer = PdfWriter.GetInstance(document, fileStream);
                   // writer.PdfVersion = PdfWriter.PDF_VERSION_1_4;
                    string selectedValue = ddlPartyName.SelectedItem.Text;
                    string selectedValue1 = ddlProductName1.SelectedItem.Text;
                    string selectedValue2 = ddlProductName2.SelectedItem.Text;
                    string selectedValue3 = ddlProductName3.SelectedItem.Text;
                    string selectedValue4 = ddlProductName4.SelectedItem.Text;
                    string selectedValue5 = ddlProductName5.SelectedItem.Text;
                    string selectedValue6 = ddlProductName6.SelectedItem.Text;
                    string selectedValue7 = ddlProductName7.SelectedItem.Text;
                    string selectedValue8 = ddlProductName8.SelectedItem.Text;
                    string selectedValue9 = ddlProductName9.SelectedItem.Text;
                    string selectedValue10 = ddlProductName10.SelectedItem.Text;
                    string selectedValue11 = ddlProductName11.SelectedItem.Text;
                    string selectedValue12 = ddlProductName12.SelectedItem.Text;
                    string selectedValue13 = ddlProductName13.SelectedItem.Text;
                    string selectedValue14 = ddlProductName14.SelectedItem.Text;
                    string selectedValue15 = ddlProductName15.SelectedItem.Text;
                    string selectedValue16 = ddlProductName16.SelectedItem.Text;
                    string selectedValue17 = ddlProductName17.SelectedItem.Text;
                    string selectedValue18 = ddlProductName18.SelectedItem.Text;
                    string selectedValue19 = ddlProductName19.SelectedItem.Text;
                    string selectedValue20 = ddlProductName20.SelectedItem.Text;

                    //writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

                    document.Open();
                    //decimal unit = 0;
                    //decimal unitPrice = 0;
                    decimal quantity = 0;
                    string strValue = "";
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        document.NewPage();

                        var baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        var importedPage = writer.GetImportedPage(reader, i);
                        var contentByte = writer.DirectContent;

                        contentByte.BeginText();
                        contentByte.SetFontAndSize(baseFont, 11);
                        contentByte.SetColorFill(BaseColor.BLACK);

                        //Title
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, quotationId.ToString(), 435, 711, 0);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtDate.Text, 550, 710, 0);


                        //Party Name
                        if (ddlPartyName.SelectedIndex == 0)
                        {
                            //ddlPartyName.SelectedIndex = 0;
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 700, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue, 69, 715, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtGstNo.Text, 412, 650, 0);

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtAddress.Text, 61, 681, 100f);

                            ColumnText ct = new ColumnText(writer.DirectContent);
                            ct.SetSimpleColumn(35, 716, 340f, 200f);
                            BaseFont baseFont1 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                            Font font = new Font(baseFont1, 11, Font.NORMAL, BaseColor.BLACK);
                            //paragraph = new Paragraph(txtAddress.Text);
                            ct.AddText(new Paragraph(txtAddress.Text, font));
                            while (ColumnText.HasMoreText(ct.Go()))
                            {
                                ct.SetSimpleColumn(0, 0, 100f, 100f);
                                ct.AddText(new Phrase(""));
                            }
                        }

                        //All Twenty Record 

                        if (ddlProductName1.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO1.Text, 35, 611, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue1, 60, 611, 0);
                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN1.Text, 367, 611, 0);

                            quantity = Convert.ToDecimal(txtQuantity1.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit1.Text, 492, 611, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice1.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            //strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 611, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal1.Text, 558, 611, 0);

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 130, 164, 0);
                        }


                        if (ddlProductName2.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO2.Text, 35, 596, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue2, 60, 596, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN2.Text, 367, 596, 0);

                            quantity = Convert.ToDecimal(txtQuantity2.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit2.Text, 492, 596, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice2.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity2.Text + " " + txtUnit2.Text, 492, 596, 0);

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 596, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal2.Text, 558, 596, 0);
                        }

                        if (ddlProductName3.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO3.Text, 35, 581, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue3, 60, 581, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN3.Text, 367, 581, 0);

                            quantity = Convert.ToDecimal(txtQuantity3.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit3.Text, 492, 581, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice3.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity3.Text + " " + txtUnit3.Text, 492, 581, 0);

                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 581, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal3.Text, 558, 581, 0);
                        }

                        if (ddlProductName4.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO4.Text, 35, 564, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue4, 60, 564, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN4.Text, 367, 564, 0);

                            quantity = Convert.ToDecimal(txtQuantity4.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit4.Text, 492, 564, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice4.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity4.Text + " " + txtUnit4.Text, 492, 564, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 564, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal4.Text, 558, 564, 0);
                        }

                        if (ddlProductName5.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO5.Text, 35, 548, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue5, 60, 548, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN5.Text, 367, 548, 0);

                            quantity = Convert.ToDecimal(txtQuantity5.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit5.Text, 492, 548, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice5.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity5.Text + " " + txtUnit5.Text, 492, 548, 0);
                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 548, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal5.Text, 558, 548, 0);
                        }

                        if (ddlProductName6.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO6.Text, 35, 531, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue6, 60, 531, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN6.Text, 367, 531, 0);

                            quantity = Convert.ToDecimal(txtQuantity6.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit6.Text, 492, 531, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice6.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity6.Text + " " + txtUnit6.Text, 492, 531, 0);
                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 531, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal6.Text, 558, 531, 0);
                        }

                        if (ddlProductName7.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO7.Text, 35, 515, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue7, 60, 515, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN7.Text, 367, 515, 0);

                            quantity = Convert.ToDecimal(txtQuantity7.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit7.Text, 492, 515, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice7.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity7.Text + " " + txtUnit7.Text, 492, 515, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 515, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal7.Text, 558, 515, 0);
                        }

                        if (ddlProductName8.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO8.Text, 35, 498, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue8, 60, 498, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN8.Text, 367, 498, 0);

                            quantity = Convert.ToDecimal(txtQuantity8.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit8.Text, 492, 498, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice8.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity8.Text + " " + txtUnit8.Text, 492, 498, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 498, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal8.Text, 558, 498, 0);
                        }

                        if (ddlProductName9.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO9.Text, 35, 482, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue9, 60, 482, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN9.Text, 367, 482, 0);

                            quantity = Convert.ToDecimal(txtQuantity9.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit9.Text, 492, 482, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice9.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity9.Text + " " + txtUnit9.Text, 492, 482, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 482, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal9.Text, 558, 482, 0);
                        }

                        if (ddlProductName10.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO10.Text, 35, 465, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue10, 60, 465, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN10.Text, 367, 465, 0);

                            quantity = Convert.ToDecimal(txtQuantity10.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit10.Text, 492, 465, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice10.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity10.Text + " " + txtUnit10.Text, 492, 465, 0);
                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 465, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal10.Text, 558, 465, 0);
                        }

                        if (ddlProductName11.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO11.Text, 35, 447, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue11, 60, 447, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN11.Text, 367, 447, 0);

                            quantity = Convert.ToDecimal(txtQuantity11.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit11.Text, 492, 447, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice11.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity11.Text + " " + txtUnit11.Text, 492, 447, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 447, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal11.Text, 558, 447, 0);
                        }

                        if (ddlProductName12.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO12.Text, 35, 431, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue12, 60, 431, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN12.Text, 367, 431, 0);

                            quantity = Convert.ToDecimal(txtQuantity12.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit12.Text, 492, 431, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice12.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity12.Text + " " + txtUnit12.Text, 492, 431, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 431, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal12.Text, 558, 431, 0);
                        }

                        if (ddlProductName13.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO13.Text, 35, 414, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue13, 60, 414, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN13.Text, 367, 414, 0);

                            quantity = Convert.ToDecimal(txtQuantity13.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit13.Text, 492, 414, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice13.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity13.Text + " " + txtUnit13.Text, 492, 414, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 414, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal13.Text, 558, 414, 0);
                        }

                        if (ddlProductName14.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO14.Text, 35, 397, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue14, 60, 397, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN14.Text, 367, 397, 0);

                            quantity = Convert.ToDecimal(txtQuantity14.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit14.Text, 492, 397, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice14.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity14.Text + " " + txtUnit14.Text, 492, 397, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 397, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal14.Text, 558, 397, 0);
                        }

                        if (ddlProductName15.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO15.Text, 35, 380, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue15, 60, 380, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN15.Text, 367, 380, 0);

                            quantity = Convert.ToDecimal(txtQuantity15.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit15.Text, 492, 380, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice15.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity15.Text + " " + txtUnit15.Text, 492, 380, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 380, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal15.Text, 558, 380, 0);
                        }

                        if (ddlProductName16.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO16.Text, 35, 364, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue16, 60, 364, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN16.Text, 367, 364, 0);

                            quantity = Convert.ToDecimal(txtQuantity16.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit16.Text, 492, 364, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice16.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity16.Text + " " + txtUnit16.Text, 492, 364, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 364, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal16.Text, 558, 364, 0);
                        }

                        if (ddlProductName17.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO17.Text, 35, 348, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue17, 60, 348, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN17.Text, 367, 348, 0);

                            quantity = Convert.ToDecimal(txtQuantity17.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit17.Text, 492, 348, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice17.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity17.Text + " " + txtUnit17.Text, 492, 348, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 348, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal17.Text, 558, 348, 0);
                        }

                        if (ddlProductName18.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO18.Text, 35, 331, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue18, 60, 331, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN18.Text, 367, 331, 0);

                            quantity = Convert.ToDecimal(txtQuantity18.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit18.Text, 492, 331, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice18.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity18.Text + " " + txtUnit18.Text, 492, 331, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 331, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal18.Text, 558, 331, 0);
                        }

                        if (ddlProductName19.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO19.Text, 35, 315, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue19, 60, 315, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN19.Text, 367, 315, 0);

                            quantity = Convert.ToDecimal(txtQuantity19.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit19.Text, 492, 315, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice19.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity19.Text + " " + txtUnit19.Text, 492, 315, 0);
                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 315, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal19.Text, 558, 315, 0);
                        }

                        if (ddlProductName20.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO20.Text, 35, 298, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue20, 60, 298, 0);
                         //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN20.Text, 367, 298, 0);

                            quantity = Convert.ToDecimal(txtQuantity20.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit20.Text, 492, 298, 0);
                            //unitPrice = Convert.ToDecimal(txtUnitPrice20.Text);
                            //strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity20.Text + " " + txtUnit20.Text, 492, 298, 0);
                          //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 298, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal20.Text, 558, 298, 0);
                        }


                        //Srno
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtPaymentMode.Text, 35, 232, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo2.Text, 35, 212, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo3.Text, 35, 194, 0);

                        //Description
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription1.Text, 145, 232, 0);

                        ColumnText ct3 = new ColumnText(writer.DirectContent);
                        ct3.SetSimpleColumn(145, 248, 425f, 150f);
                        BaseFont baseFont3 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        Font font3 = new Font(baseFont3, 11, Font.NORMAL, BaseColor.BLACK);

                        ct3.AddText(new Paragraph(txtDescription1.Text, font3));
                        while (ColumnText.HasMoreText(ct3.Go()))
                        {
                            ct3.SetSimpleColumn(0, 0, 100f, 100f);
                            ct3.AddText(new Phrase(""));
                        }


                        ColumnText ct4 = new ColumnText(writer.DirectContent);
                        ct4.SetSimpleColumn(35, 165, 355f, 100f);
                        BaseFont baseFont4 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        Font font4 = new Font(baseFont4, 11, Font.NORMAL, BaseColor.BLACK);

                        ct4.AddText(new Paragraph(txtDescription2.Text, font4));
                        while (ColumnText.HasMoreText(ct4.Go()))
                        {
                            ct4.SetSimpleColumn(0, 0, 100f, 100f);
                            ct4.AddText(new Phrase(""));
                        }

                        /*contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription2.Text, 156, 192, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription3.Text, 156, 174, 0);

                        //Total
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotalValue.Text, 558, 247, 0);


                        if (ddlGST.SelectedIndex == 1)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblSGSTValue.Text, 558, 232, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblCGSTValue.Text, 558, 214, 0);
                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 210, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 465, 234, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 465, 217, 0);

                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
                        }

                        if (ddlGST.SelectedIndex == 2)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblSGSTValue.Text, 558, 232, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblCGSTValue.Text, 558, 214, 0);
                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 192, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 465, 234, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 465, 217, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
                        }

                        if (ddlGST.SelectedIndex == 3)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblSGSTValue.Text, 558, 232, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblCGSTValue.Text, 558, 214, 0);
                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 174, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 465, 234, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 465, 217, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
                        }


                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblRoundOffValue.Text, 558, 181, 0);*/


                        ColumnText ct2 = new ColumnText(writer.DirectContent);
                        ct2.SetSimpleColumn(103, 197, 400f, 100f);
                        BaseFont baseFont2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        Font font2 = new Font(baseFont2, 11, Font.NORMAL, BaseColor.BLACK);

                        ct2.AddText(new Paragraph(lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, font2));
                        while (ColumnText.HasMoreText(ct2.Go()))
                        {
                            ct2.SetSimpleColumn(0, 0, 100f, 100f);
                            ct2.AddText(new Phrase(""));
                        }

                        // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalValue.Text, 103, 181, 0);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotalValue.Text, 558, 168, 0);

                        contentByte.EndText();
                        contentByte.AddTemplate(importedPage, 0, 0);
                    }

                    document.Close();
                    writer.Close();


                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename= " + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + time.Replace(" ", "_") + "_" + "GNEnterpriseQuotation.pdf");
                    Response.TransmitFile(newFile);
                    Response.End();
                }
            }
        }

       
    }

}