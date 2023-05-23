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
    public partial class WebForm7 : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["dbGNEnterpriseConStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {            
            int salesId = 0;

            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("SELECT MAX(saleId) + 1 FROM tblSale", con);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        salesId = Convert.ToInt32(result);
                        txtInvoiceNo.Text = salesId + "";
                    }
                    cmd.ExecuteNonQuery();
                }
                DropDownParty();
                DropDown();
                DropDownSales();
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
                btnUpdateBill.Visible = false;
                txtDateUpdate.Visible = false;
            }
        }
        protected void DropDown()
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT productId, productName FROM tblProduct order by productName ASC", conn);
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
        /*
        protected void DropDown2()
    {
        using (SqlConnection con = new SqlConnection(CS))
        {
            //ddlProductName2.Items.Add(new ListItem("Select Product Name"));
            //ddlProductName2.AppendDataBoundItems = true;
            String strQuery = "SELECT productId, productName FROM tblProduct";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                ddlProductName2.DataSource = cmd.ExecuteReader();
                ddlProductName2.DataTextField = "productName";
                ddlProductName2.DataValueField = "productId";
                ddlProductName2.DataBind();
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
    protected void DropDown3()
    {
        using (SqlConnection con = new SqlConnection(CS))
        {
            //ddlProductName2.Items.Add(new ListItem("Select Product Name"));
            //ddlProductName2.AppendDataBoundItems = true;
            String strQuery = "SELECT productId, productName FROM tblProduct";
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = strQuery,
                Connection = con
            };
            try
            {
                con.Open();
                ddlProductName3.DataSource = cmd.ExecuteReader();
                ddlProductName3.DataTextField = "productName";
                ddlProductName3.DataValueField = "productId";
                ddlProductName3.DataBind();
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
        protected void DropDownParty()
        {
            btnPDF.Visible = true;
            using (SqlConnection con = new SqlConnection(CS))
            {
                ddlPartyName.Items.Insert(0, new ListItem("Select Party", ""));
                ddlPartyName.AppendDataBoundItems = true;
                String strQuery = "SELECT partyId, partyName FROM tblParty order by partyName asc";
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

        protected void DropDownSales()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                // ddlSales.Items.Insert(0, new ListItem("Select Previous Sale", ""));
                ddlSales.AppendDataBoundItems = true;
                String strQuery = "SELECT saleId, partyName FROM tblSale, tblParty where tblSale.partyId = tblParty.partyId order by saleId desc ";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    ddlSales.Items.Clear();
                    txtAddress.Text = ""; // clear the existing text
                    txtDate.Text = ""; // clear existing date
                    ddlSales.Items.Insert(0, new ListItem("Select Previous Sale", ""));
                    /*
                    DropDownListSalesId.DataSource = cmd.ExecuteReader();
                    DropDownListSalesId.DataTextField = "saleId";
                    DropDownListSalesId.DataValueField = "saleId";
                    DropDownListSalesId.DataBind();
                    */
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem item = new ListItem(dr["saleId"].ToString() + " - " + dr["partyName"].ToString(), dr["saleId"].ToString());
                        ddlSales.Items.Add(item);
                    }
                    dr.Close();
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

            if (ddl.SelectedItem != null && txtUnit != null && txtUnitPrice != null && txtHSN != null)
            {
                if (ddl.SelectedIndex == 0)
                {
                    txtHSN.Text = "";
                    txtQuantity.Text = "";
                    txtUnit.Text = "";
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
                }
            }
        }

        protected void Dropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            string ddlId = ddl.ID;

            string txtAddressId = "txtAddress" + ddlId.Substring("ddlPartyName".Length);
            TextBox txtAddress = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtAddressId);

            string txtGSTNoId = "txtGstNo" + ddlId.Substring("ddlPartyName".Length);
            TextBox txtGstNo = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtGSTNoId);

            /*tring txtUnitPriceId = "txtGSTNO" + ddlId.Substring("ddlPartyName".Length);
            //TextBox txtGSTNO = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtUnitPriceId);

            //string txtQuantityId = "txtQuantity" + ddlId.Substring("ddlProductName".Length);
            //TextBox txtQuantity = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtQuantityId);

            //string txtHSNId = "txtHSN" + ddlId.Substring("ddlProductName".Length);
            //TextBox txtHSN = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtHSNId);
            //string lblTotalId = "lblTotal" + ddlId.Substring("ddlProductName".Length);
            Label lblTotal = (Label)Master.FindControl("ContentPlaceHolder1").FindControl(lblTotalId);*/


            if (ddl.SelectedItem != null && txtAddress != null && txtGstNo != null)
            {
                if (ddl.SelectedIndex == 0)
                {
                    txtAddress.Text = "";
                    txtGstNo.Text = "";
                    return;
                }

                string query = "SELECT partyAddress,partyGSTINNo FROM tblParty WHERE partyId = @partyId";

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
                        txtGstNo.Text = reader.GetString(1).ToString();
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

        private void CalculateTotal()
        {
            decimal total = 0, valuesWithoutCommas = 0, unitprice = 0;
            ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            for (int i = 1; i <= 20; i++)
            {
                Label lbl = (Label)cph.FindControl("lblTotal" + i.ToString());
                TextBox txt = (TextBox)cph.FindControl("txtUnitPrice" + i.ToString());
                if (lbl != null && !string.IsNullOrEmpty(lbl.Text) || txt != null && !string.IsNullOrEmpty(txt.Text))
                {
                    valuesWithoutCommas = 0;
                    decimal.TryParse(lbl.Text.Replace(",", ""), out valuesWithoutCommas);
                    total += valuesWithoutCommas;
                    decimal.TryParse(txt.Text.Replace(",", ""), out valuesWithoutCommas);
                    unitprice += valuesWithoutCommas;
                }
            }
            lblTotalValue.Text = total.ToString("N", new CultureInfo("en-IN"));

            CalculateTotalWithGST();
        }

        protected void ddlGST_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotalWithGST();
        }

        private void CalculateTotalWithGST()
        {
            decimal grandTotal = Convert.ToDecimal(lblTotalValue.Text);
            //decimal.TryParse(lblTotalValue.Text.Replace(",", ""), out grandTotal);
            decimal gstPercent = Convert.ToDecimal(ddlGST.SelectedValue);
            decimal gstPer = (gstPercent) / 2;

            lblSGSTPer.Text = gstPer.ToString("N", new CultureInfo("en-IN")) + "%";
            lblCGSTPer.Text = gstPer.ToString("N", new CultureInfo("en-IN")) + "%";

            decimal gstValue = (grandTotal * gstPercent) / 100;
            decimal gstValue1 = (gstValue) / 2;
            decimal grandTotalWithGst = grandTotal + gstValue;
            lblTotalGST.Text = gstValue.ToString("N", new CultureInfo("en-IN"));

            lblCGSTValue.Text = gstValue1.ToString("N", new CultureInfo("en-IN"));
            lblSGSTValue.Text = gstValue1.ToString("N", new CultureInfo("en-IN"));

            decimal roundOffValue = Math.Round(grandTotalWithGst) - grandTotalWithGst;
            lblRoundOffValue.Text = roundOffValue.ToString("N", new CultureInfo("en-IN"));
            int finalTotal = Convert.ToInt32(grandTotalWithGst);
            lblGrandTotalValueWithGSTAfterRoundOffValue.Text = Math.Round(grandTotalWithGst, MidpointRounding.AwayFromZero).ToString("N", new CultureInfo("en-IN"));
            string totalInWords = ConvertNumberToWords(finalTotal);
            lblGrandTotalValueWithGSTAfterRoundOffInWords.Text = totalInWords.ToUpper() + " ONLY";

        }

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            decimal grandTotal, gstAmt;
            //int salesId = 0;
            string inputDate = txtDate.Text;
            DateTime date = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string outputDate = date.ToString("yyyy-MM-dd");

            string time = DateTime.Now.ToString("h:mm:ss tt");

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                decimal.TryParse(lblGrandTotalValueWithGSTAfterRoundOffValue.Text.Replace(",", ""), out grandTotal);
                decimal.TryParse(lblTotalGST.Text.Replace(",", ""), out gstAmt);
                decimal.TryParse(lblGrandTotalValueWithGSTAfterRoundOffValue.Text.Replace(",", ""), out grandTotal);
                decimal.TryParse(lblTotalGST.Text.Replace(",", ""), out gstAmt);

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "SET IDENTITY_INSERT tblSale ON";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                cmd.CommandText = "insert into tblSale (saleId,saleDate,partyId,saleBillAmount,saleSGSTPer,saleCGSTPer,saleSGSTRs,saleCGSTRs,saleSRNo1,saleDescription1,saleSRNo2,saleSRNo3,saleDescription2,saleDescription3,saleTotalAmount,saleTotalGST,saleROValue,saleChallanNo) values" +
                                                                 "(" + txtInvoiceNo.Text + ",'" + outputDate + "'," + ddlPartyName.SelectedValue + "," + grandTotal + "," + lblCGSTPer.Text.ToString().Replace("%", "") + "," + lblSGSTPer.Text.ToString().Replace("%", "") + "," + lblCGSTValue.Text.ToString().Replace(",", "") + "," + lblSGSTValue.Text.ToString().Replace(",", "") + ",'" +
                                                                     txtPaymentMode.Text + "','" + txtDescription1.Text + "','" + txtSRNo2.Text + "','" + txtSRNo3.Text + "','" + txtDescription2.Text + "','" + txtDescription3.Text + "'," + lblTotalValue.Text.ToString().Replace(",", "") + "," + lblTotalGST.Text.ToString().Replace(",", "") + "," + lblRoundOffValue.Text.ToString().Replace(",", "") + ",'" + txtChallanNo.Text + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SET IDENTITY_INSERT tblSale OFF";
                cmd.ExecuteNonQuery();


                //    SqlCommand cmd = new SqlCommand("insert into tblSale (saleId,saleDate,partyId,saleBillAmount,saleSGSTPer,saleCGSTPer,saleSGSTRs,saleCGSTRs,saleSRNo1,saleDescription1,saleSRNo2,saleSRNo3,saleDescription2,saleDescription3,saleTotalAmount,saleTotalGST,saleROValue,saleChallanNo) values" +
                //                                                "(" + txtInvoiceNo.Text + ",'" + outputDate + "'," + ddlPartyName.SelectedValue + "," + grandTotal + "," + lblCGSTPer.Text.ToString().Replace("%", "") + "," + lblSGSTPer.Text.ToString().Replace("%", "") + "," + lblCGSTValue.Text.ToString().Replace(",", "") + "," + lblSGSTValue.Text.ToString().Replace(",", "") + ",'" +
                //                                                    txtPaymentMode.Text + "','" + txtDescription1.Text + "','" + txtSRNo2.Text + "','" + txtSRNo3.Text + "','" + txtDescription2.Text + "','" + txtDescription3.Text + "'," + lblTotalValue.Text.ToString().Replace(",", "") + "," + lblTotalGST.Text.ToString().Replace(",", "") + "," + lblRoundOffValue.Text.ToString().Replace(",", "") + ",'" + txtChallanNo.Text + "')", con);
                //cmd.ExecuteNonQuery();
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

                        //cmd = new SqlCommand("SELECT MAX(saleId) FROM tblSale", con);
                        //object result = cmd.ExecuteScalar();
                        //if (result != DBNull.Value)
                        //{
                        //    salesId = Convert.ToInt32(result);
                        //    txtInvoiceNo.Text = salesId+"";
                        //}



                        decimal total = 0;
                        decimal.TryParse(lblTotal.Text.Replace(",", ""), out total);
                        inserttblSalesItem += "INSERT INTO tblSalesItem VALUES(" + quantityTextbox.Text + "," + txtInvoiceNo.Text + "," + ddl.SelectedValue + "," + unitPriceTextbox.Text + "," + total + ");";

                    }
                    //else { break; }
                }
                cmd = new SqlCommand(inserttblSalesItem, con);
                cmd.ExecuteNonQuery();

            }

            //PDF Path

            string originalFile = Server.MapPath("~/PDF/GNEnterpriseNewV2_5.pdf");
            //string newFile = @"C:\Users\Admin\Downloads\GNEnterpriseSales.pdf";
            string newFile = @"C:\Software\PDF\GNEnterprises\Sales\" + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + "GNEnterpriseSales.pdf";

            /*using (var reader = new PdfReader(originalFile))
            //{
            //    using (var fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newFile), FileMode.Create, FileAccess.Write))
            //    {
            //        var document = new Document(reader.GetPageSizeWithRotation(1));
            //        var writer = PdfWriter.GetInstance(document, fileStream);
            //        string selectedValue = ddlPartyName.SelectedItem.Text;
            //        string selectedValue1 = ddlProductName1.SelectedItem.Text;
            //        string selectedValue2 = ddlProductName2.SelectedItem.Text;
            //        string selectedValue3 = ddlProductName3.SelectedItem.Text;
            //        string selectedValue4 = ddlProductName4.SelectedItem.Text;
            //        string selectedValue5 = ddlProductName5.SelectedItem.Text;
            //        string selectedValue6 = ddlProductName6.SelectedItem.Text;
            //        string selectedValue7 = ddlProductName7.SelectedItem.Text;
            //        string selectedValue8 = ddlProductName8.SelectedItem.Text;
            //        string selectedValue9 = ddlProductName9.SelectedItem.Text;
            //        string selectedValue10 = ddlProductName10.SelectedItem.Text;
            //        string selectedValue11 = ddlProductName11.SelectedItem.Text;
            //        string selectedValue12 = ddlProductName12.SelectedItem.Text;
            //        string selectedValue13 = ddlProductName13.SelectedItem.Text;
            //        string selectedValue14 = ddlProductName14.SelectedItem.Text;
            //        string selectedValue15 = ddlProductName15.SelectedItem.Text;
            //        string selectedValue16 = ddlProductName16.SelectedItem.Text;
            //        string selectedValue17 = ddlProductName17.SelectedItem.Text;
            //        string selectedValue18 = ddlProductName18.SelectedItem.Text;
            //        string selectedValue19 = ddlProductName19.SelectedItem.Text;
            //        string selectedValue20 = ddlProductName20.SelectedItem.Text;

            //        document.Open();

            //        for (var i = 1; i <= reader.NumberOfPages; i++)
            //        {
            //            document.NewPage();

            //            var baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //            var importedPage = writer.GetImportedPage(reader, i);
            //            var contentByte = writer.DirectContent;

            //            contentByte.BeginText();
            //            contentByte.SetFontAndSize(baseFont, 11);
            //            contentByte.SetColorFill(BaseColor.BLACK);

            //            //Title
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, salesId.ToString(), 393, 693, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtDate.Text, 512, 693, 0);


            //            //Party Name
            //            if (ddlPartyName.SelectedIndex == 0)
            //            {
            //                //ddlPartyName.SelectedIndex = 0;
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 700, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue, 92, 700, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtGstNo.Text , 375, 660, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtAddress.Text, 61, 681, 0);
            //            }


            //            //All Twenty Record 

            //            if (ddlProductName1.SelectedIndex == 0)
            //            {
            //                txtHSN1.Text = "";
            //                txtQuantity1.Text = "";
            //                txtUnitPrice1.Text = "";
            //                lblTotal1.Text = "";
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO1.Text, 62, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue1, 92, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN1.Text, 300, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity1.Text + " " + txtUnit1.Text, 357, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice1.Text, 408, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal1.Text, 468, 614, 0);

            //                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 130, 164, 0);
            //            }

            //            if (ddlProductName2.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO2.Text, 62, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue2, 92, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN2.Text, 300, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity2.Text + " " + txtUnit2.Text, 357, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice2.Text, 408, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal2.Text, 468, 596, 0);
            //            }

            //            if (ddlProductName3.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO3.Text, 62, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue3, 92, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN3.Text, 300, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity3.Text + " " + txtUnit3.Text, 357, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice3.Text, 408, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal3.Text, 468, 578, 0);
            //            }

            //            if (ddlProductName4.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO4.Text, 62, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue4, 92, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN4.Text, 300, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity4.Text + " " + txtUnit4.Text, 357, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice4.Text, 408, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal4.Text, 468, 560, 0);
            //            }

            //            if (ddlProductName5.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO5.Text, 62, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue5, 92, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN5.Text, 300, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity5.Text + " " + txtUnit5.Text, 357, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice5.Text, 408, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal5.Text, 468, 542, 0);
            //            }

            //            if (ddlProductName6.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO6.Text, 62, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue6, 92, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN6.Text, 300, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity6.Text + " " + txtUnit6.Text, 357, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice6.Text, 408, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal6.Text, 468, 524, 0);
            //            }

            //            if (ddlProductName7.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO7.Text, 62, 508, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue7, 92, 508, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN7.Text, 300, 507, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity7.Text + " " + txtUnit7.Text, 357, 508, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice7.Text, 408, 508, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal2.Text, 468, 508, 0);
            //            }

            //            if (ddlProductName8.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO8.Text, 62, 490, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue8, 92, 490, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN8.Text, 300, 489, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity8.Text + " " + txtUnit8.Text, 357, 490, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice8.Text, 408, 490, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal8.Text, 468, 490, 0);
            //            }

            //            if (ddlProductName9.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO9.Text, 62, 472, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue9, 92, 472, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN9.Text, 300, 473, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity9.Text + " " + txtUnit9.Text, 357, 472, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice9.Text, 408, 472, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal9.Text, 468, 472, 0);
            //            }

            //            if (ddlProductName10.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO10.Text, 62, 454, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue10, 92, 454, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN10.Text, 300, 468, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity10.Text + " " + txtUnit10.Text, 357, 454, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice10.Text, 408, 454, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal10.Text, 468, 454, 0);
            //            }

            //            if (ddlProductName11.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO11.Text, 62, 438, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue11, 92, 438, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN11.Text, 300, 435, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity11.Text + " " + txtUnit11.Text, 357, 438, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice11.Text, 408, 438, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal11.Text, 468, 438, 0);
            //            }

            //            if (ddlProductName12.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO12.Text, 62, 420, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue12, 92, 420, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN12.Text, 300, 423, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity12.Text, 357, 420, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice12.Text + " " + txtUnit12.Text, 408, 420, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal12.Text, 468, 420, 0);
            //            }

            //            if (ddlProductName13.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO13.Text, 62, 402, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue13, 92, 402, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN13.Text, 300, 405, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity13.Text, 357, 402, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice13.Text + " " + txtUnit13.Text, 408, 402, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal13.Text, 468, 402, 0);
            //            }

            //            if (ddlProductName14.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO14.Text, 62, 384, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue14, 92, 384, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN14.Text, 300, 387, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity14.Text + " " + txtUnit14.Text, 357, 384, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice14.Text, 408, 384, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal14.Text, 468, 384, 0);
            //            }

            //            if (ddlProductName15.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO15.Text, 62, 368, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue15, 92, 368, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN15.Text, 300, 369, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity15.Text + " " + txtUnit15.Text, 357, 368, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice15.Text, 408, 368, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal15.Text, 468, 368, 0);
            //            }

            //            if (ddlProductName16.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO16.Text, 62, 350, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue16, 92, 350, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN16.Text, 300, 351, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity16.Text + " " + txtUnit16.Text, 357, 350, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice16.Text, 408, 350, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal16.Text, 468, 350, 0);
            //            }

            //            if (ddlProductName17.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO17.Text, 62, 332, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue17, 92, 332, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN17.Text, 300, 333, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity17.Text + " " + txtUnit17.Text, 357, 332, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice17.Text, 408, 332, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal12.Text, 468, 332, 0);
            //            }

            //            if (ddlProductName18.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO18.Text, 62, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue18, 92, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN18.Text, 300, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity18.Text + " " + txtUnit18.Text, 357, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice18.Text, 408, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal18.Text, 468, 314, 0);
            //            }

            //            if (ddlProductName19.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO19.Text, 62, 296, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue19, 92, 296, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN19.Text, 300, 297, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity19.Text + " " + txtUnit19.Text, 357, 296, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice19.Text, 408, 296, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal19.Text, 468, 296, 0);
            //            }

            //            if (ddlProductName20.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO20.Text, 62, 278, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue20, 92, 278, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN20.Text, 300, 279, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity20.Text + " " + txtUnit20.Text, 357, 278, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice20.Text, 408, 278, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal20.Text, 468, 278, 0);
            //            }

            //            //Srno
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo1.Text, 55, 213, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo2.Text, 55, 192, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo3.Text, 55, 174, 0);

            //            //Description
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription1.Text, 156, 213, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription2.Text, 156, 192, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription3.Text, 156, 174, 0);

            //            //Total
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalValue.Text, 468, 228, 0);


            //            if (ddlGST.SelectedIndex == 1)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTValue.Text, 468, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTValue.Text, 468, 192, 0);
            //                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 405, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 405, 192, 0);

            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
            //            }

            //            if (ddlGST.SelectedIndex == 2)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTValue.Text, 468, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTValue.Text, 468, 192, 0);
            //                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 192, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 405, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 405, 192, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
            //            }

            //            if (ddlGST.SelectedIndex == 3)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTValue.Text, 468, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTValue.Text, 468, 192, 0);
            //                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 174, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 405, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 405, 192, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
            //            }


            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblRoundOffValue.Text, 468, 158, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffValue.Text, 468, 143, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 52, 145, 0);

            //            contentByte.EndText();
            //            contentByte.AddTemplate(importedPage, 0, 0);
            //        }

            //        document.Close();
            //        writer.Close();

            //        Response.ContentType = "application/pdf";
            //        Response.AppendHeader("Content-Disposition", "attachment; filename= "+ddlPartyName.SelectedItem.Text.Replace(" ","_")+"_"+txtDate.Text.Replace("-","_")+"_"+"GNEnterpriseSales.pdf");
            //        Response.TransmitFile(newFile);
            //        Response.End();
            //        clear();
            //    }
            //}
        }

        void clear()
        {
            ddlPartyName.SelectedIndex = 0;
            //txtUnitPrice1.Text = "";
            //txtHSN1.Text = "";
            //txtQuantity1.Text = "";
            //lblTotal1.Text = "";
        }

        //protected void btnTest_Click(object sender, EventArgs e)
        //{
        //    var orignalPDF = @"C:\Users\Admin\OneDrive\Desktop\Test_14_04_2023.pdf";
        //    var editPDF = @"C:\Users\Admin\Downloads\Test_14_04_2023_Edit.pdf";

        //    PdfReader reader = new PdfReader(orignalPDF);
        //    Rectangle size = reader.GetPageSizeWithRotation(1);
        //    Document document = new Document(size);

        //    // open the writer
        //    FileStream fs = new FileStream(editPDF, FileMode.Create, FileAccess.Write);
        //    PdfWriter writer = PdfWriter.GetInstance(document, fs);
        //    document.Open();

        //    PdfContentByte cb = writer.DirectContent;

        //    // select the font properties
        //    BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //    cb.SetColorFill(BaseColor.BLACK);
        //    cb.SetFontAndSize(bf, 11);

        //    int maxWordLength = 5;

        //    StringBuilder modifiedText = new StringBuilder();            

        //    cb.BeginText();
        //    string text = "Some random blablablabla...";
        //    // put the alignment and coordinates here
        //    cb.ShowTextAligned(1, text, 244, 792, 0);
        //    cb.EndText();
        //    cb.BeginText();
        //    text = "Other random blabla...";
        //    // put the alignment and coordinates here
        //    cb.ShowTextAligned(2, text, 503, 756, 0);
        //    cb.EndText();

        //    string[] words = text.Split();

        //    foreach (string word in words)
        //    {
        //        if (word.Length > maxWordLength)
        //        {
        //            // Split the word into smaller chunks that fit within the limit
        //            for (int i = 0; i < word.Length; i += maxWordLength)
        //            {
        //                int length = Math.Min(maxWordLength, word.Length - i);
        //                string chunk = word.Substring(i, length);
        //                modifiedText.AppendLine(chunk);
        //            }
        //        }
        //        else
        //        {
        //            modifiedText.Append(word + " ");
        //        }
        //    }
                      

        //    // create the new page and add it to the pdf
        //    PdfImportedPage page = writer.GetImportedPage(reader, 1);
        //    cb.AddTemplate(page, 0, 0);

        //    document.Close();
        //    fs.Close();
        //    writer.Close();
        //    reader.Close();

        //    Response.ContentType = "application/pdf";
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=Test_14_04_2023_Edit.pdf");
        //    Response.TransmitFile(editPDF);
        //    Response.End();

        //}

        //protected void ddlSales_SelectedIndexChanged(object sender, EventArgs e)
        //{            
        //    int salesId;
        //    Int32.TryParse(lblSaleValue.Text, out salesId);            
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        SqlCommand cmd = new SqlCommand(@"SELECT tblSalesItem.productId,itemSalesQuantity,itemSalesUnitPrice,itemSalesAmount, tblProduct.productHSN
        //                                            FROM tblSale, tblProduct , tblSalesItem
        //                                            WHERE tblSale.saleId = tblSalesItem.itemSalesId
        //                                            AND tblSalesItem.itemSalesId = "+ salesId +"", con);

        //        con.Open();

        //        /*SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);

        //        // Bind the DataTable to the table on the web form
        //        ddlSales.DataSource = dataTable;
        //        ddlSales.DataBind();

        //        /*SqlDataReader rdr = cmd.ExecuteReader();
        //        string typeId = "";
        //        if (rdr.HasRows)
        //        {
        //            while (rdr.Read())
        //            {
        //                typeId = rdr[0].ToString();
        //            }
        //        }
        //        ddlType.SelectedValue = typeId;
           }*/

            using (var reader = new PdfReader(originalFile))
            {
                using (var fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newFile), FileMode.Create, FileAccess.Write))
                {                    
                    var document = new Document(reader.GetPageSizeWithRotation(1));
                    var writer = PdfWriter.GetInstance(document, fileStream);
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

                    writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

                    document.Open();
                    //string srno = "0";
                    //decimal unit = 0;
                    //int difference = 17;
                    decimal unitPrice = 0;
                    decimal quantity = 0;
                    string strValue = "";
                    //string ddlID = "";
                    for (var j = 1; j <= reader.NumberOfPages; j++)
                    {
                        document.NewPage();

                        var baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.EMBEDDED);
                        var importedPage = writer.GetImportedPage(reader, j);
                        var contentByte = writer.DirectContent;

                        contentByte.BeginText();
                        contentByte.SetFontAndSize(baseFont, 11);
                        contentByte.SetColorFill(BaseColor.BLACK);

                        //Title
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtInvoiceNo.Text, 435, 711, 0);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtChallanNo.Text, 417, 677, 0);
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



                        /*for (int i = 1; i <= 20; i++)
                        {                            
                            DropDownList ddlProduct = (DropDownList)Master.FindControl("ContentPlaceHolder1").FindControl("ddlProductName" + i);
                            Label lblSRNO = (Label)Master.FindControl("ContentPlaceHolder1").FindControl("lblSRNO" + i);
                            Label lblTotal = (Label)Master.FindControl("ContentPlaceHolder1").FindControl("lblTotal" + i);
                            TextBox txtHSN = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl("txtHSN" + i);
                            TextBox txtQuantity = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl("txtQuantity" + i);
                            TextBox txtUnit = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl("txtUnit" + i);
                            TextBox txtUnitPrice = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl("txtUnitPrice" + i);

                            if (ddlProduct.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614 - ((i - 1) * difference), 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO.Text, 35, 606 - ((i - 1) * difference), 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ddlProduct.SelectedItem.Text, 60, 606 - ((i - 1) * difference), 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN.Text, 367, 606 - ((i - 1) * difference), 0);

                                quantity = Convert.ToDecimal(txtQuantity.Text);
                                string strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                                // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                                strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit.Text, 425, 606 - ((i - 1) * difference), 0);
                                unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                                strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                                // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                                strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 606 - ((i - 1) * difference), 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal.Text, 558, 606 - ((i - 1) * difference), 0);
                            }
                        }*/



                        if (ddlProductName1.SelectedIndex == 0)
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
                        }
                        else
                        {
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO1.Text, 35, 611, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue1, 60, 611, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN1.Text, 367, 611, 0);

                            quantity = Convert.ToDecimal(txtQuantity1.Text);
                            
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit1.Text, 425, 611, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice1.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 611, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN2.Text, 367, 596, 0);

                            quantity = Convert.ToDecimal(txtQuantity2.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit2.Text, 425, 596, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice2.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity2.Text + " " + txtUnit2.Text, 425, 596, 0);

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 596, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN3.Text, 367, 581, 0);

                            quantity = Convert.ToDecimal(txtQuantity3.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit3.Text, 425, 581, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice3.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity3.Text + " " + txtUnit3.Text, 425, 581, 0);

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 581, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN4.Text, 367, 564, 0);

                            quantity = Convert.ToDecimal(txtQuantity4.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit4.Text, 425, 564, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice4.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity4.Text + " " + txtUnit4.Text, 425, 564, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 564, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN5.Text, 367, 548, 0);

                            quantity = Convert.ToDecimal(txtQuantity5.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit5.Text, 425, 548, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice5.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity5.Text + " " + txtUnit5.Text, 425, 548, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 548, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN6.Text, 367, 531, 0);

                            quantity = Convert.ToDecimal(txtQuantity6.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit6.Text, 425, 531, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice6.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity6.Text + " " + txtUnit6.Text, 425, 531, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 531, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN7.Text, 367, 515, 0);

                            quantity = Convert.ToDecimal(txtQuantity7.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit7.Text, 425, 515, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice7.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity7.Text + " " + txtUnit7.Text, 425, 515, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 515, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN8.Text, 367, 498, 0);

                            quantity = Convert.ToDecimal(txtQuantity8.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit8.Text, 425, 498, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice8.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity8.Text + " " + txtUnit8.Text, 425, 498, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 498, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN9.Text, 367, 482, 0);

                            quantity = Convert.ToDecimal(txtQuantity9.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit9.Text, 425, 482, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice9.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity9.Text + " " + txtUnit9.Text, 425, 482, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 482, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN10.Text, 367, 465, 0);

                            quantity = Convert.ToDecimal(txtQuantity10.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit10.Text, 425, 465, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice10.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity10.Text + " " + txtUnit10.Text, 425, 465, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 465, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN11.Text, 367, 447, 0);

                            quantity = Convert.ToDecimal(txtQuantity11.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit11.Text, 425, 447, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice11.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity11.Text + " " + txtUnit11.Text, 425, 447, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 447, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN12.Text, 367, 431, 0);

                            quantity = Convert.ToDecimal(txtQuantity12.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit12.Text, 425, 431, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice12.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity12.Text + " " + txtUnit12.Text, 425, 431, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 431, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN13.Text, 367, 414, 0);

                            quantity = Convert.ToDecimal(txtQuantity13.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit13.Text, 425, 414, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice13.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity13.Text + " " + txtUnit13.Text, 425, 414, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 414, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN14.Text, 367, 397, 0);

                            quantity = Convert.ToDecimal(txtQuantity14.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit14.Text, 425, 397, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice14.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity14.Text + " " + txtUnit14.Text, 425, 397, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 397, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN15.Text, 367, 380, 0);

                            quantity = Convert.ToDecimal(txtQuantity15.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit15.Text, 425, 380, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice15.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity15.Text + " " + txtUnit15.Text, 425, 380, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 380, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN16.Text, 367, 364, 0);

                            quantity = Convert.ToDecimal(txtQuantity16.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit16.Text, 425, 364, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice16.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity16.Text + " " + txtUnit16.Text, 425, 364, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 364, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN17.Text, 367, 348, 0);

                            quantity = Convert.ToDecimal(txtQuantity17.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit17.Text, 425, 348, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice17.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity17.Text + " " + txtUnit17.Text, 425, 348, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 348, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN18.Text, 367, 331, 0);

                            quantity = Convert.ToDecimal(txtQuantity18.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit18.Text, 425, 331, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice18.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity18.Text + " " + txtUnit18.Text, 425, 331, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 331, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN19.Text, 367, 315, 0);

                            quantity = Convert.ToDecimal(txtQuantity19.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit19.Text, 425, 315, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice19.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity19.Text + " " + txtUnit19.Text, 425, 315, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 315, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN20.Text, 367, 298, 0);

                            quantity = Convert.ToDecimal(txtQuantity20.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit20.Text, 425, 298, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice20.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity20.Text + " " + txtUnit20.Text, 425, 298, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 298, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal20.Text, 558, 298, 0);
                        }


                        //Srno
                        //ColumnText ct4 = new ColumnText(writer.DirectContent);
                        //ct4.SetSimpleColumn(35, 232, 100f, 100f);
                        //BaseFont baseFont4 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        //Font font4 = new Font(baseFont4, 11, Font.NORMAL, BaseColor.BLACK);

                        //ct4.AddText(new Paragraph(txtPaymentMode.Text, font4));
                        //while (ColumnText.HasMoreText(ct4.Go()))
                        //{
                        //    ct4.SetSimpleColumn(0, 0, 100f, 100f);
                        //    ct4.AddText(new Phrase(""));
                        //}
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtPaymentMode.Text, 35, 232, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo2.Text, 35, 212, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo3.Text, 35, 194, 0);

                        //Description
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription1.Text, 145, 232, 0);

                        ColumnText ct3 = new ColumnText(writer.DirectContent);
                        ct3.SetSimpleColumn(145, 248, 430f, 100f);
                        BaseFont baseFont3 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        Font font3 = new Font(baseFont3, 11, Font.NORMAL, BaseColor.BLACK);

                        ct3.AddText(new Paragraph(txtDescription1.Text, font3));
                        while (ColumnText.HasMoreText(ct3.Go()))
                        {
                            ct3.SetSimpleColumn(0, 0, 100f, 100f);
                            ct3.AddText(new Phrase(""));
                        }

                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription2.Text, 156, 192, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription3.Text, 156, 174, 0);

                        //Total
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotalValue.Text, 558, 247, 0);


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


                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblRoundOffValue.Text, 558, 181, 0);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblGrandTotalValueWithGSTAfterRoundOffValue.Text, 558, 168, 0);

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

                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 103, 181, 0);


                        contentByte.EndText();
                        contentByte.AddTemplate(importedPage, 0, 0);
                    }

                    document.Close();
                    writer.Close();

                    btnPDF.Visible = false;


                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename= " + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + time.Replace(" ", "_") + "_" + "GNEnterpriseSales.pdf");
                    Response.TransmitFile(newFile);
                    Response.End();
                }
            }
        }

        protected void ddlSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSales.SelectedIndex != 0)
            {
                txtInvoiceNo.Enabled = false;
            }
            txtDateUpdate.Visible = true;
            txtDate.Visible = false;
            btnPDF.Visible = false;
            btnUpdateBill.Visible = true;
            int saleId;
            CultureInfo culture = new CultureInfo("en-IN");
            if (int.TryParse(ddlSales.SelectedValue, out saleId))
            {
                string sql = @"SELECT p.partyName, p.partyAddress,p.partyGSTINNo, 
		                            s.saleId,s.saleDate,s.saleBillAmount,s.saleCGSTPer,s.saleCGSTRs,s.saleDescription1,s.saleDescription2,s.saleDescription3,
		                            s.salePaidAmount,s.saleRemainingAmount,s.saleSGSTPer,s.saleSGSTRs,s.saleSRNo1,s.saleSRNo2,s.saleSRNo3,s.saleTotalAmount,s.saleTotalGST,s.saleROValue,s.saleChallanNo,
		                            pr.productName, pr.productHSN,pr.productUnit, 
		                            si.itemSalesQuantity,si.itemSalesUnitPrice, si.itemSalesAmount
                            FROM tblSale s
                            JOIN tblSalesItem si ON s.saleId = si.saleId
                            JOIN tblProduct pr ON si.productId = pr.productId
                            JOIN tblParty p ON s.partyId = p.partyId
                            WHERE s.saleId = @saleId";

                using (SqlConnection conn = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@saleId", saleId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    // select party in ddlparty
                    while (reader.Read())
                    {
                        string partyAddress = reader["partyAddress"].ToString();                        ;
                        string partyGSTINNo = reader["partyGSTINNo"].ToString();
                        string partyPaymentMode = reader["saleSRNo1"].ToString();
                        string partyDescription1 = reader["saleDescription1"].ToString();
                        string partysaleBillAmount = Convert.ToDecimal(reader["saleBillAmount"]).ToString("N", culture);
                        string partysaleSGSTPer = reader["saleSGSTPer"].ToString();
                        string partysaleSGSTRs = Convert.ToDecimal(reader["saleSGSTRs"]).ToString("N", culture);
                        string partysaleCGSTPer = reader["saleCGSTPer"].ToString();
                        string partysaleCGSTRs = Convert.ToDecimal(reader["saleCGSTRs"]).ToString("N", culture);                        
                        string partysaleTotalAmount = Convert.ToDecimal(reader["saleTotalAmount"]).ToString("N", culture);
                        string partysaleTotalGST = Convert.ToDecimal(reader["saleTotalGST"]).ToString("N", culture);
                        string partysaleROValue = Convert.ToDecimal(reader["saleROValue"]).ToString("N", culture);
                        string partyChallanNo = reader["saleChallanNo"].ToString();
                        string partysaleId = reader["saleId"].ToString();

                        txtInvoiceNo.Text = partysaleId;
                        txtAddress.Text = partyAddress;                        
                        txtGstNo.Text = partyGSTINNo;
                        txtPaymentMode.Text = partyPaymentMode;
                        txtDescription1.Text = partyDescription1;
                        lblTotalValue.Text = partysaleTotalAmount;
                        lblSGSTPer.Text = partysaleSGSTPer + "%";
                        lblSGSTValue.Text = partysaleSGSTRs;
                        lblCGSTPer.Text = partysaleCGSTPer + "%";
                        lblCGSTValue.Text = partysaleCGSTRs;                         
                        lblTotalGST.Text = partysaleTotalGST;
                        lblRoundOffValue.Text = partysaleROValue;
                        txtChallanNo.Text = partyChallanNo;
                        lblGrandTotalValueWithGSTAfterRoundOffValue.Text = partysaleBillAmount;

                        decimal saleBillAmount = Convert.ToDecimal(reader["saleBillAmount"], culture);
                        //string partysaleBillAmount = saleBillAmount.ToString("N", culture);
                        lblGrandTotalValueWithGSTAfterRoundOffValue.Text = partysaleBillAmount;

                        int no = Decimal.ToInt32(saleBillAmount);
                        string totalInWords = ConvertNumberToWords(no);
                        lblGrandTotalValueWithGSTAfterRoundOffInWords.Text = totalInWords.ToUpper() + " ONLY";



                        DateTime saleDate = (DateTime)reader["saleDate"];
                        string formattedDate = saleDate.ToString("dd-MM-yyyy");
                        txtDateUpdate.Text = formattedDate;


                        string partyName = reader["partyName"].ToString();
                        ListItem selectedItem = ddlPartyName.Items.FindByText(partyName);
                        if (selectedItem != null)
                        {
                            ddlPartyName.ClearSelection();
                            selectedItem.Selected = true;
                            break;
                        }                        
                        //reader.Close();
                    }
                    
                    reader.Close();
                }
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@saleId", saleId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();                   

                    ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    if (cph != null)
                    {
                        for (int j = 1; j <= 20; j++)
                        {
                            string ddlName = "ddlProductName" + j.ToString();
                            string txtName = "txtQuantity" + j.ToString();
                            string txtUnitPrice = "txtUnitPrice" + j.ToString();
                            string lblTotal = "lblTotal" + j.ToString();
                            string txtHSN = "txtHSN" + j.ToString();
                            string txtUnit = "txtUnit" + j.ToString();

                            DropDownList ddl = (DropDownList)cph.FindControl(ddlName);
                            TextBox tb = (TextBox)cph.FindControl(txtName);
                            TextBox tb2 = (TextBox)cph.FindControl(txtUnitPrice);
                            Label tb3 = (Label)cph.FindControl(lblTotal);
                            TextBox tb4 = (TextBox)cph.FindControl(txtHSN);
                            TextBox tb5 = (TextBox)cph.FindControl(txtUnit);

                            if (ddl != null)
                            {
                                ddl.ClearSelection(); // Clear the previously selected item, if any.
                                ddl.SelectedIndex = 0;
                                                                                      
                            }
                            if (tb != null || tb2 != null || tb3 != null || tb4 != null || tb5 != null)
                            {
                                tb.Text = "";
                                tb2.Text = "";
                                tb3.Text = "";
                                tb4.Text = "";
                                tb5.Text = "";
                            }
                        } 
                        
                        string txtPaymentMode = "txtPaymentMode";
                        string txtDescription1 = "txtDescription1";

                        TextBox tb6 = (TextBox)cph.FindControl(txtPaymentMode);
                        TextBox tb7 = (TextBox)cph.FindControl(txtDescription1);

                        if (tb6 != null || tb7 != null)
                        {
                            tb6.Text = "";
                            tb7.Text = "";
                        }

                        int i = 1;
                        while (reader.Read())
                        {                           
                            string ddlName = "ddlProductName" + i.ToString();
                            string txtName = "txtQuantity" + i.ToString();
                            string txtUnitPrice = "txtUnitPrice" + i.ToString();
                            string lblTotal = "lblTotal" + i.ToString();
                            string txtHSN = "txtHSN" + i.ToString();
                            string txtUnit = "txtUnit" + i.ToString();

                            //DropDownList ddlparty = (DropDownList)cph.FindControl(ddlpartyName);
                            DropDownList ddl = (DropDownList)cph.FindControl(ddlName);
                            TextBox tb = (TextBox)cph.FindControl(txtName);
                            TextBox tb2 = (TextBox)cph.FindControl(txtUnitPrice);
                            Label tb3 = (Label)cph.FindControl(lblTotal);
                            TextBox tb4 = (TextBox)cph.FindControl(txtHSN);
                            TextBox tb5 = (TextBox)cph.FindControl(txtUnit);

                            if (ddl != null)
                            {
                                string selectedItemText = reader["productName"].ToString();
                                ListItem selectedItem = ddl.Items.FindByText(selectedItemText);
                                if (selectedItem != null)
                                {
                                    ddl.ClearSelection(); // Clear the previously selected item, if any.
                                                          //ddlProductName1.Items.FindByText(selectedItemText).Selected = true;
                                    selectedItem.Selected = true; // Set the new item as selected.
                                                                  //break;
                                }
                                //CalculateTotal();
                                tb.Text = reader["itemSalesQuantity"].ToString();
                                tb2.Text = reader["itemSalesUnitPrice"].ToString();
                                tb3.Text = reader["itemSalesAmount"].ToString();
                                tb4.Text = reader["productHSN"].ToString();
                                tb5.Text = reader["productUnit"].ToString();
                                tb6.Text = reader["saleSRNo1"].ToString();
                                tb7.Text = reader["saleDescription1"].ToString();                                
                            }
                            i++;
                        }
                        reader.Close(); 
                    }
                }
            }
        }

        protected void btnUpdateBill_Click(object sender, EventArgs e)
        {
            txtDateUpdate.Visible = false;
            decimal grandTotal, gstAmt;
            string salesId = ddlSales.SelectedValue;

            string inputDate = txtDateUpdate.Text;
            DateTime date = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string outputDate = date.ToString("yyyy-MM-dd");

            string time = DateTime.Now.ToString("h:mm:ss tt");

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                decimal.TryParse(lblGrandTotalValueWithGSTAfterRoundOffValue.Text.Replace(",", ""), out grandTotal);
                decimal.TryParse(lblTotalGST.Text.Replace(",", ""), out gstAmt);
                decimal.TryParse(lblGrandTotalValueWithGSTAfterRoundOffValue.Text.Replace(",", ""), out grandTotal);
                decimal.TryParse(lblTotalGST.Text.Replace(",", ""), out gstAmt);

                SqlCommand cmd = new SqlCommand(@"UPDATE tblSale 
                                                  SET " +
                                                        "saleDate = @saleDate," +
                                                        "partyId = @partyId," +
                                                        "saleBillAmount = @saleBillAmount," +
                                                        "saleSGSTPer = @saleSGSTPer," +
                                                        "saleCGSTPer = @saleCGSTPer," +
                                                        "saleSGSTRs = @saleSGSTRs," +
                                                        "saleCGSTRs = @saleCGSTRs," +
                                                        "saleSRNo1 = @saleSRNo1," +
                                                        "saleDescription1 = @saleDescription1," +
                                                        "saleSRNo2 = @saleSRNo2," +
                                                        "saleSRNo3 = @saleSRNo3," +
                                                        "saleDescription2 = @saleDescription2," +
                                                        "saleDescription3 = @saleDescription3," +
                                                        "saleTotalAmount = @saleTotalAmount," +
                                                        "saleTotalGST = @saleTotalGST," +
                                                        "saleROValue = @saleROValue, " +
                                                        "saleChallanNo = @saleChallanNo " +
                                                "WHERE[saleId] = @saleId", con);



                //SqlCommand cmd = new SqlCommand(query,con);

                cmd.Parameters.AddWithValue("@saleDate", outputDate);
                cmd.Parameters.AddWithValue("@partyId", ddlPartyName.SelectedValue);
                cmd.Parameters.AddWithValue("@saleBillAmount", grandTotal);
                cmd.Parameters.AddWithValue("@saleSGSTPer", lblSGSTPer.Text.ToString().Replace("%", ""));
                cmd.Parameters.AddWithValue("@saleCGSTPer", lblCGSTPer.Text.ToString().Replace("%", ""));
                cmd.Parameters.AddWithValue("@saleSGSTRs", lblSGSTValue.Text.ToString().Replace(",", ""));
                cmd.Parameters.AddWithValue("@saleCGSTRs", lblCGSTValue.Text.ToString().Replace(",", ""));
                cmd.Parameters.AddWithValue("@saleSRNo1", txtPaymentMode.Text);
                cmd.Parameters.AddWithValue("@saleDescription1", txtDescription1.Text);
                cmd.Parameters.AddWithValue("@saleSRNo2", txtSRNo2.Text);
                cmd.Parameters.AddWithValue("@saleSRNo3", txtSRNo3.Text);
                cmd.Parameters.AddWithValue("@saleDescription2", txtDescription2.Text);
                cmd.Parameters.AddWithValue("@saleDescription3", txtDescription3.Text);
                cmd.Parameters.AddWithValue("@saleTotalAmount", lblTotalValue.Text.ToString().Replace(",", ""));
                cmd.Parameters.AddWithValue("@saleTotalGST", lblTotalGST.Text.ToString().Replace(",", ""));
                cmd.Parameters.AddWithValue("@saleROValue", lblRoundOffValue.Text.ToString().Replace(",", ""));
                cmd.Parameters.AddWithValue("@saleChallanNo", txtChallanNo.Text);
                cmd.Parameters.AddWithValue("@saleId", salesId);

                cmd.ExecuteNonQuery();


                cmd = new SqlCommand(@"DELETE FROM [tblSalesItem]
                                       WHERE [saleId] = " + salesId, con);
                cmd.ExecuteNonQuery();

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

                        decimal total = 0;
                        decimal.TryParse(lblTotal.Text.Replace(",", ""), out total);
                        inserttblSalesItem += "INSERT INTO tblSalesItem VALUES(" + quantityTextbox.Text + "," + salesId + "," + ddl.SelectedValue + "," + unitPriceTextbox.Text + "," + total + ");";
                    }
                    //else { break; }
                }
                cmd = new SqlCommand(inserttblSalesItem, con);
                cmd.ExecuteNonQuery();
            }

            string originalFile = Server.MapPath("~/PDF/GNEnterpriseNewV2_5.pdf");
            //string newFile = @"C:\Users\Admin\Downloads\GNEnterpriseSales.pdf";
            string newFile = @"C:\Software\PDF\GNEnterprises\Sales\" + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + "GNEnterpriseSales.pdf";

            /*using (var reader = new PdfReader(originalFile))
            //{
            //    using (var fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newFile), FileMode.Create, FileAccess.Write))
            //    {
            //        var document = new Document(reader.GetPageSizeWithRotation(1));
            //        var writer = PdfWriter.GetInstance(document, fileStream);
            //        string selectedValue = ddlPartyName.SelectedItem.Text;
            //        string selectedValue1 = ddlProductName1.SelectedItem.Text;
            //        string selectedValue2 = ddlProductName2.SelectedItem.Text;
            //        string selectedValue3 = ddlProductName3.SelectedItem.Text;
            //        string selectedValue4 = ddlProductName4.SelectedItem.Text;
            //        string selectedValue5 = ddlProductName5.SelectedItem.Text;
            //        string selectedValue6 = ddlProductName6.SelectedItem.Text;
            //        string selectedValue7 = ddlProductName7.SelectedItem.Text;
            //        string selectedValue8 = ddlProductName8.SelectedItem.Text;
            //        string selectedValue9 = ddlProductName9.SelectedItem.Text;
            //        string selectedValue10 = ddlProductName10.SelectedItem.Text;
            //        string selectedValue11 = ddlProductName11.SelectedItem.Text;
            //        string selectedValue12 = ddlProductName12.SelectedItem.Text;
            //        string selectedValue13 = ddlProductName13.SelectedItem.Text;
            //        string selectedValue14 = ddlProductName14.SelectedItem.Text;
            //        string selectedValue15 = ddlProductName15.SelectedItem.Text;
            //        string selectedValue16 = ddlProductName16.SelectedItem.Text;
            //        string selectedValue17 = ddlProductName17.SelectedItem.Text;
            //        string selectedValue18 = ddlProductName18.SelectedItem.Text;
            //        string selectedValue19 = ddlProductName19.SelectedItem.Text;
            //        string selectedValue20 = ddlProductName20.SelectedItem.Text;

            //        document.Open();

            //        for (var i = 1; i <= reader.NumberOfPages; i++)
            //        {
            //            document.NewPage();

            //            var baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //            var importedPage = writer.GetImportedPage(reader, i);
            //            var contentByte = writer.DirectContent;

            //            contentByte.BeginText();
            //            contentByte.SetFontAndSize(baseFont, 11);
            //            contentByte.SetColorFill(BaseColor.BLACK);

            //            //Title
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, salesId.ToString(), 393, 693, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtDate.Text, 512, 693, 0);


            //            //Party Name
            //            if (ddlPartyName.SelectedIndex == 0)
            //            {
            //                //ddlPartyName.SelectedIndex = 0;
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 700, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue, 92, 700, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtGstNo.Text , 375, 660, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtAddress.Text, 61, 681, 0);
            //            }


            //            //All Twenty Record 

            //            if (ddlProductName1.SelectedIndex == 0)
            //            {
            //                txtHSN1.Text = "";
            //                txtQuantity1.Text = "";
            //                txtUnitPrice1.Text = "";
            //                lblTotal1.Text = "";
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO1.Text, 62, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue1, 92, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN1.Text, 300, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity1.Text + " " + txtUnit1.Text, 357, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice1.Text, 408, 614, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal1.Text, 468, 614, 0);

            //                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 130, 164, 0);
            //            }

            //            if (ddlProductName2.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO2.Text, 62, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue2, 92, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN2.Text, 300, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity2.Text + " " + txtUnit2.Text, 357, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice2.Text, 408, 596, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal2.Text, 468, 596, 0);
            //            }

            //            if (ddlProductName3.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO3.Text, 62, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue3, 92, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN3.Text, 300, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity3.Text + " " + txtUnit3.Text, 357, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice3.Text, 408, 578, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal3.Text, 468, 578, 0);
            //            }

            //            if (ddlProductName4.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO4.Text, 62, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue4, 92, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN4.Text, 300, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity4.Text + " " + txtUnit4.Text, 357, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice4.Text, 408, 560, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal4.Text, 468, 560, 0);
            //            }

            //            if (ddlProductName5.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO5.Text, 62, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue5, 92, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN5.Text, 300, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity5.Text + " " + txtUnit5.Text, 357, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice5.Text, 408, 542, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal5.Text, 468, 542, 0);
            //            }

            //            if (ddlProductName6.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO6.Text, 62, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue6, 92, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN6.Text, 300, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity6.Text + " " + txtUnit6.Text, 357, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice6.Text, 408, 524, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal6.Text, 468, 524, 0);
            //            }

            //            if (ddlProductName7.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO7.Text, 62, 508, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue7, 92, 508, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN7.Text, 300, 507, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity7.Text + " " + txtUnit7.Text, 357, 508, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice7.Text, 408, 508, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal2.Text, 468, 508, 0);
            //            }

            //            if (ddlProductName8.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO8.Text, 62, 490, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue8, 92, 490, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN8.Text, 300, 489, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity8.Text + " " + txtUnit8.Text, 357, 490, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice8.Text, 408, 490, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal8.Text, 468, 490, 0);
            //            }

            //            if (ddlProductName9.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO9.Text, 62, 472, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue9, 92, 472, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN9.Text, 300, 473, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity9.Text + " " + txtUnit9.Text, 357, 472, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice9.Text, 408, 472, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal9.Text, 468, 472, 0);
            //            }

            //            if (ddlProductName10.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO10.Text, 62, 454, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue10, 92, 454, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN10.Text, 300, 468, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity10.Text + " " + txtUnit10.Text, 357, 454, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice10.Text, 408, 454, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal10.Text, 468, 454, 0);
            //            }

            //            if (ddlProductName11.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO11.Text, 62, 438, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue11, 92, 438, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN11.Text, 300, 435, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity11.Text + " " + txtUnit11.Text, 357, 438, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice11.Text, 408, 438, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal11.Text, 468, 438, 0);
            //            }

            //            if (ddlProductName12.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO12.Text, 62, 420, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue12, 92, 420, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN12.Text, 300, 423, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity12.Text, 357, 420, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice12.Text + " " + txtUnit12.Text, 408, 420, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal12.Text, 468, 420, 0);
            //            }

            //            if (ddlProductName13.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO13.Text, 62, 402, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue13, 92, 402, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN13.Text, 300, 405, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity13.Text, 357, 402, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice13.Text + " " + txtUnit13.Text, 408, 402, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal13.Text, 468, 402, 0);
            //            }

            //            if (ddlProductName14.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO14.Text, 62, 384, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue14, 92, 384, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN14.Text, 300, 387, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity14.Text + " " + txtUnit14.Text, 357, 384, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice14.Text, 408, 384, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal14.Text, 468, 384, 0);
            //            }

            //            if (ddlProductName15.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO15.Text, 62, 368, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue15, 92, 368, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN15.Text, 300, 369, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity15.Text + " " + txtUnit15.Text, 357, 368, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice15.Text, 408, 368, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal15.Text, 468, 368, 0);
            //            }

            //            if (ddlProductName16.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO16.Text, 62, 350, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue16, 92, 350, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN16.Text, 300, 351, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity16.Text + " " + txtUnit16.Text, 357, 350, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice16.Text, 408, 350, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal16.Text, 468, 350, 0);
            //            }

            //            if (ddlProductName17.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO17.Text, 62, 332, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue17, 92, 332, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN17.Text, 300, 333, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity17.Text + " " + txtUnit17.Text, 357, 332, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice17.Text, 408, 332, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal12.Text, 468, 332, 0);
            //            }

            //            if (ddlProductName18.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO18.Text, 62, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue18, 92, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN18.Text, 300, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity18.Text + " " + txtUnit18.Text, 357, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice18.Text, 408, 314, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal18.Text, 468, 314, 0);
            //            }

            //            if (ddlProductName19.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO19.Text, 62, 296, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue19, 92, 296, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN19.Text, 300, 297, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity19.Text + " " + txtUnit19.Text, 357, 296, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice19.Text, 408, 296, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal19.Text, 468, 296, 0);
            //            }

            //            if (ddlProductName20.SelectedIndex == 0)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 92, 614, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO20.Text, 62, 278, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue20, 92, 278, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN20.Text, 300, 279, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtQuantity20.Text + " " + txtUnit20.Text, 357, 278, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice20.Text, 408, 278, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal20.Text, 468, 278, 0);
            //            }

            //            //Srno
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo1.Text, 55, 213, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo2.Text, 55, 192, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo3.Text, 55, 174, 0);

            //            //Description
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription1.Text, 156, 213, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription2.Text, 156, 192, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription3.Text, 156, 174, 0);

            //            //Total
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalValue.Text, 468, 228, 0);


            //            if (ddlGST.SelectedIndex == 1)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTValue.Text, 468, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTValue.Text, 468, 192, 0);
            //                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 405, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 405, 192, 0);

            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
            //            }

            //            if (ddlGST.SelectedIndex == 2)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTValue.Text, 468, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTValue.Text, 468, 192, 0);
            //                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 192, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 405, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 405, 192, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
            //            }

            //            if (ddlGST.SelectedIndex == 3)
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTValue.Text, 468, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTValue.Text, 468, 192, 0);
            //                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 174, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSGSTPer.Text, 405, 210, 0);
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblCGSTPer.Text, 405, 192, 0);
            //            }
            //            else
            //            {
            //                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 456, 278, 0);
            //            }


            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblRoundOffValue.Text, 468, 158, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffValue.Text, 468, 143, 0);
            //            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 52, 145, 0);

            //            contentByte.EndText();
            //            contentByte.AddTemplate(importedPage, 0, 0);
            //        }

            //        document.Close();
            //        writer.Close();

            //        Response.ContentType = "application/pdf";
            //        Response.AppendHeader("Content-Disposition", "attachment; filename= "+ddlPartyName.SelectedItem.Text.Replace(" ","_")+"_"+txtDate.Text.Replace("-","_")+"_"+"GNEnterpriseSales.pdf");
            //        Response.TransmitFile(newFile);
            //        Response.End();
            //        clear();
            //    }
            //}
        }

        void clear()
        {
            ddlPartyName.SelectedIndex = 0;
            //txtUnitPrice1.Text = "";
            //txtHSN1.Text = "";
            //txtQuantity1.Text = "";
            //lblTotal1.Text = "";
        }

        //protected void btnTest_Click(object sender, EventArgs e)
        //{
        //    var orignalPDF = @"C:\Users\Admin\OneDrive\Desktop\Test_14_04_2023.pdf";
        //    var editPDF = @"C:\Users\Admin\Downloads\Test_14_04_2023_Edit.pdf";

        //    PdfReader reader = new PdfReader(orignalPDF);
        //    Rectangle size = reader.GetPageSizeWithRotation(1);
        //    Document document = new Document(size);

        //    // open the writer
        //    FileStream fs = new FileStream(editPDF, FileMode.Create, FileAccess.Write);
        //    PdfWriter writer = PdfWriter.GetInstance(document, fs);
        //    document.Open();

        //    PdfContentByte cb = writer.DirectContent;

        //    // select the font properties
        //    BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //    cb.SetColorFill(BaseColor.BLACK);
        //    cb.SetFontAndSize(bf, 11);

        //    int maxWordLength = 5;

        //    StringBuilder modifiedText = new StringBuilder();            

        //    cb.BeginText();
        //    string text = "Some random blablablabla...";
        //    // put the alignment and coordinates here
        //    cb.ShowTextAligned(1, text, 244, 792, 0);
        //    cb.EndText();
        //    cb.BeginText();
        //    text = "Other random blabla...";
        //    // put the alignment and coordinates here
        //    cb.ShowTextAligned(2, text, 503, 756, 0);
        //    cb.EndText();

        //    string[] words = text.Split();

        //    foreach (string word in words)
        //    {
        //        if (word.Length > maxWordLength)
        //        {
        //            // Split the word into smaller chunks that fit within the limit
        //            for (int i = 0; i < word.Length; i += maxWordLength)
        //            {
        //                int length = Math.Min(maxWordLength, word.Length - i);
        //                string chunk = word.Substring(i, length);
        //                modifiedText.AppendLine(chunk);
        //            }
        //        }
        //        else
        //        {
        //            modifiedText.Append(word + " ");
        //        }
        //    }
                      

        //    // create the new page and add it to the pdf
        //    PdfImportedPage page = writer.GetImportedPage(reader, 1);
        //    cb.AddTemplate(page, 0, 0);

        //    document.Close();
        //    fs.Close();
        //    writer.Close();
        //    reader.Close();

        //    Response.ContentType = "application/pdf";
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=Test_14_04_2023_Edit.pdf");
        //    Response.TransmitFile(editPDF);
        //    Response.End();

        //}

        //protected void ddlSales_SelectedIndexChanged(object sender, EventArgs e)
        //{            
        //    int salesId;
        //    Int32.TryParse(lblSaleValue.Text, out salesId);            
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        SqlCommand cmd = new SqlCommand(@"SELECT tblSalesItem.productId,itemSalesQuantity,itemSalesUnitPrice,itemSalesAmount, tblProduct.productHSN
        //                                            FROM tblSale, tblProduct , tblSalesItem
        //                                            WHERE tblSale.saleId = tblSalesItem.itemSalesId
        //                                            AND tblSalesItem.itemSalesId = "+ salesId +"", con);

        //        con.Open();

        //        /*SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);

        //        // Bind the DataTable to the table on the web form
        //        ddlSales.DataSource = dataTable;
        //        ddlSales.DataBind();

        //        /*SqlDataReader rdr = cmd.ExecuteReader();
        //        string typeId = "";
        //        if (rdr.HasRows)
        //        {
        //            while (rdr.Read())
        //            {
        //                typeId = rdr[0].ToString();
        //            }
        //        }
        //        ddlType.SelectedValue = typeId;
           }*/

            using (var reader = new PdfReader(originalFile))
            {
                using (var fileStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newFile), FileMode.Create, FileAccess.Write))
                {
                    var document = new Document(reader.GetPageSizeWithRotation(1));
                    var writer = PdfWriter.GetInstance(document, fileStream);
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
                    decimal unitPrice = 0;
                    decimal quantity = 0;
                    string strValue = "";
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        document.NewPage();

                        var baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        var importedPage = writer.GetImportedPage(reader, i);
                        var contentByte = writer.DirectContent;

                        contentByte.BeginText();
                        contentByte.SetFontAndSize(baseFont, 11);
                        contentByte.SetColorFill(BaseColor.BLACK);

                        //Title
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, salesId.ToString(), 435, 711, 0);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtChallanNo.Text, 417, 677, 0);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtDateUpdate.Text, 550, 710, 0);


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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN1.Text, 367, 611, 0);

                            quantity = Convert.ToDecimal(txtQuantity1.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit1.Text, 425, 611, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice1.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 611, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN2.Text, 367, 596, 0);

                            quantity = Convert.ToDecimal(txtQuantity2.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit2.Text, 425, 596, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice2.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity2.Text + " " + txtUnit2.Text, 425, 596, 0);

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 596, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN3.Text, 367, 581, 0);

                            quantity = Convert.ToDecimal(txtQuantity3.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit3.Text, 425, 581, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice3.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity3.Text + " " + txtUnit3.Text, 425, 581, 0);

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 581, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN4.Text, 367, 564, 0);

                            quantity = Convert.ToDecimal(txtQuantity4.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit4.Text, 425, 564, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice4.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity4.Text + " " + txtUnit4.Text, 425, 564, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 564, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN5.Text, 367, 548, 0);

                            quantity = Convert.ToDecimal(txtQuantity5.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit5.Text, 425, 548, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice5.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity5.Text + " " + txtUnit5.Text, 425, 548, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 548, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN6.Text, 367, 531, 0);

                            quantity = Convert.ToDecimal(txtQuantity6.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit6.Text, 425, 531, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice6.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity6.Text + " " + txtUnit6.Text, 425, 531, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 531, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN7.Text, 367, 515, 0);

                            quantity = Convert.ToDecimal(txtQuantity7.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit7.Text, 425, 515, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice7.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity7.Text + " " + txtUnit7.Text, 425, 515, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 515, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN8.Text, 367, 498, 0);

                            quantity = Convert.ToDecimal(txtQuantity8.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit8.Text, 425, 498, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice8.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity8.Text + " " + txtUnit8.Text, 425, 498, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 498, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN9.Text, 367, 482, 0);

                            quantity = Convert.ToDecimal(txtQuantity9.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit9.Text, 425, 482, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice9.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity9.Text + " " + txtUnit9.Text, 425, 482, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 482, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN10.Text, 367, 465, 0);

                            quantity = Convert.ToDecimal(txtQuantity10.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit10.Text, 425, 465, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice10.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity10.Text + " " + txtUnit10.Text, 425, 465, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 465, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN11.Text, 367, 447, 0);

                            quantity = Convert.ToDecimal(txtQuantity11.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit11.Text, 425, 447, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice11.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity11.Text + " " + txtUnit11.Text, 425, 447, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 447, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN12.Text, 367, 431, 0);

                            quantity = Convert.ToDecimal(txtQuantity12.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit12.Text, 425, 431, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice12.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity12.Text + " " + txtUnit12.Text, 425, 431, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 431, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN13.Text, 367, 414, 0);

                            quantity = Convert.ToDecimal(txtQuantity13.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit13.Text, 425, 414, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice13.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity13.Text + " " + txtUnit13.Text, 425, 414, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 414, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN14.Text, 367, 397, 0);

                            quantity = Convert.ToDecimal(txtQuantity14.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit14.Text, 425, 397, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice14.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity14.Text + " " + txtUnit14.Text, 425, 397, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 397, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN15.Text, 367, 380, 0);

                            quantity = Convert.ToDecimal(txtQuantity15.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit15.Text, 425, 380, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice15.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity15.Text + " " + txtUnit15.Text, 425, 380, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 380, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN16.Text, 367, 364, 0);

                            quantity = Convert.ToDecimal(txtQuantity16.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit16.Text, 425, 364, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice16.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity16.Text + " " + txtUnit16.Text, 425, 364, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 364, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN17.Text, 367, 348, 0);

                            quantity = Convert.ToDecimal(txtQuantity17.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit17.Text, 425, 348, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice17.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity17.Text + " " + txtUnit17.Text, 425, 348, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 348, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN18.Text, 367, 331, 0);

                            quantity = Convert.ToDecimal(txtQuantity18.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit18.Text, 425, 331, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice18.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity18.Text + " " + txtUnit18.Text, 425, 331, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 331, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN19.Text, 367, 315, 0);

                            quantity = Convert.ToDecimal(txtQuantity19.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit19.Text, 425, 315, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice19.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity19.Text + " " + txtUnit19.Text, 425, 315, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 315, 0);
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
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtHSN20.Text, 367, 298, 0);

                            quantity = Convert.ToDecimal(txtQuantity20.Text);
                            strValue = quantity.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue + " " + txtUnit20.Text, 425, 298, 0);
                            unitPrice = Convert.ToDecimal(txtUnitPrice20.Text);
                            strValue = unitPrice.ToString("#,##0.################", CultureInfo.CreateSpecificCulture("en-IN"));

                            // Remove trailing zeros and decimal point if the value contains only zeros after the decimal point
                            strValue = strValue.Contains(".") ? strValue.TrimEnd('0').TrimEnd('.') : strValue;

                            //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity20.Text + " " + txtUnit20.Text, 425, 298, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, strValue, 492, 298, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotal20.Text, 558, 298, 0);
                        }


                        //Srno
                        //ColumnText ct4 = new ColumnText(writer.DirectContent);
                        //ct4.SetSimpleColumn(35, 232, 100f, 100f);
                        //BaseFont baseFont4 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        //Font font4 = new Font(baseFont4, 11, Font.NORMAL, BaseColor.BLACK);

                        //ct4.AddText(new Paragraph(txtPaymentMode.Text, font4));
                        //while (ColumnText.HasMoreText(ct4.Go()))
                        //{
                        //    ct4.SetSimpleColumn(0, 0, 100f, 100f);
                        //    ct4.AddText(new Phrase(""));
                        //}
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtPaymentMode.Text, 35, 232, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo2.Text, 35, 212, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo3.Text, 35, 194, 0);

                        //Description
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription1.Text, 145, 232, 0);

                        ColumnText ct3 = new ColumnText(writer.DirectContent);
                        ct3.SetSimpleColumn(145, 248, 430f, 100f);
                        BaseFont baseFont3 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        Font font3 = new Font(baseFont3, 11, Font.NORMAL, BaseColor.BLACK);

                        ct3.AddText(new Paragraph(txtDescription1.Text, font3));
                        while (ColumnText.HasMoreText(ct3.Go()))
                        {
                            ct3.SetSimpleColumn(0, 0, 100f, 100f);
                            ct3.AddText(new Phrase(""));
                        }

                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription2.Text, 156, 192, 0);
                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDescription3.Text, 156, 174, 0);

                        //Total
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblTotalValue.Text, 558, 247, 0);


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


                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblRoundOffValue.Text, 558, 181, 0);
                        contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, lblGrandTotalValueWithGSTAfterRoundOffValue.Text, 558, 168, 0);

                        ColumnText ct2 = new ColumnText(writer.DirectContent);
                        ct2.SetSimpleColumn(103, 199, 400f, 100f);
                        BaseFont baseFont2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        Font font2 = new Font(baseFont2, 11, Font.NORMAL, BaseColor.BLACK);

                        ct2.AddText(new Paragraph(lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, font2));
                        while (ColumnText.HasMoreText(ct2.Go()))
                        {
                            ct2.SetSimpleColumn(0, 0, 100f, 100f);
                            ct2.AddText(new Phrase(""));
                        }

                        //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 103, 181, 0);


                        contentByte.EndText();
                        contentByte.AddTemplate(importedPage, 0, 0);
                    }

                    document.Close();
                    writer.Close();

                    btnPDF.Visible = false;


                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename= " + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDateUpdate.Text.ToString().Replace("-", "_") + "_" + time.Replace(" ", "_") + "_" + "GNEnterpriseSales.pdf");
                    Response.TransmitFile(newFile);
                    Response.End();
                }
            }
            btnUpdateBill.Visible = false;
            txtDate.Visible = true;
            btnPDF.Visible = true;
            clear();
        }
        void clear()
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            for (int i = 1; i <= 20; i++)
            {
                DropDownList ddl = (DropDownList)cph.FindControl("ddlProductName" + i.ToString());
                TextBox quantityTextbox = (TextBox)cph.FindControl("txtQuantity" + i.ToString());
                TextBox unitPriceTextbox = (TextBox)cph.FindControl("txtUnitPrice" + i.ToString());
                TextBox hsnTextbox = (TextBox)cph.FindControl("txtHSN" + i.ToString());
                TextBox unitTextbox = (TextBox)cph.FindControl("txtUnit" + i.ToString());
                Label lblTotal = (Label)cph.FindControl("lblTotal" + i.ToString());
                ddl.SelectedIndex = 0;
                ddlSales.SelectedIndex = 0;
                ddlPartyName.SelectedIndex = 0;
                txtChallanNo.Text = "";
                quantityTextbox.Text = "";
                unitPriceTextbox.Text = "";
                hsnTextbox.Text = "";
                unitTextbox.Text = "";
                lblTotal.Text = "0";
                txtPaymentMode.Text = "";
                txtDescription1.Text = "";
                lblTotalValue.Text = "0";
                lblSGSTPer.Text = "";
                lblCGSTPer.Text = "";
                lblSGSTValue.Text = "0";
                lblCGSTValue.Text = "0";
                lblTotalGST.Text = "0";
                lblRoundOffValue.Text = "0";
                lblGrandTotalValueWithGSTAfterRoundOffValue.Text = "0";
                lblGrandTotalValueWithGSTAfterRoundOffInWords.Text = "";
            }
        }

    }
}