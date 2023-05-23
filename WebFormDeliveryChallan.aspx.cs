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
    public partial class WebForm12 : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["dbGNEnterpriseConStr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string inputDate = txtDate.Text;
                //DateTime date = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                //string outputDate = date.ToString("yyyy-MM-dd");

                DropDownParty();
                DropDown();
                /*for (int i = 1; i <= 20; i++)
                //{
                //    string quantityTextboxId = "txtQuantity" + i;
                //    string unitPriceTextboxId = quantityTextboxId.Replace("txtQuantity", "txtUnitPrice");
                //    //ContentPlaceHolder contentPlaceHolder = this.Master.FindControl("MainContent") as ContentPlaceHolder;
                //    //TextBox txt = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(txtId);
                //    TextBox quantityTextbox = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(quantityTextboxId);
                //    TextBox unitPriceTextbox = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(unitPriceTextboxId);

                //    if (quantityTextbox != null)
                //    {
                //        quantityTextbox.TextChanged += new EventHandler(CalculateTotal);
                //        quantityTextbox.AutoPostBack = true;
                //    }
                //    if (unitPriceTextbox != null)
                //    {
                //        unitPriceTextbox.TextChanged += new EventHandler(CalculateTotal);
                //        unitPriceTextbox.AutoPostBack = true;
                //    }
                }*/
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

        protected void DropDownQuatation()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                //DropDownListSalesId.Items.Insert(0, new ListItem("Select Previous Sales", ""));
                DropDownListSalesId.AppendDataBoundItems = true;
                String strQuery = "SELECT quotationId FROM tblQuotation";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    DropDownListSalesId.DataSource = cmd.ExecuteReader();
                    DropDownListSalesId.DataTextField = "quotationId";
                    DropDownListSalesId.DataValueField = "quotationId";
                    DropDownListSalesId.DataBind();
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            int deliveryChallanId = 0;
            string filename = "", newFile = "";
            string inputDate = txtDate.Text;
            DateTime date = DateTime.ParseExact(inputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string outputDate = date.ToString("yyyy-MM-dd");

            string time = DateTime.Now.ToString("h:mm:ss tt");

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "INSERT INTO tblDeliveryChallan (deliveryChallanDate,deliveryChallanAddress,deliveryChallanTransport,deliveryChallanRNo,deliveryChallanPartyId) VALUES('" + outputDate + "','" + txtAddress.Text + "','" + txtTransport.Text + "','" + txtRNo.Text + "','" + ddlPartyName.SelectedValue + "');";
                cmd.CommandText = "INSERT INTO tblDeliveryChallan (deliveryChallanDate,deliveryChallanAddress,deliveryChallanTransport,deliveryChallanRNo,deliveryChallanPartyId) " +
                    "VALUES('" + outputDate + "','" + txtAddress.Text + "','" + txtTransport.Text + "','" + txtRNo.Text + "'," + ddlPartyName.SelectedValue + ");";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
                string inserttblDeliveryChallanItem = "";
                for (int i = 1; i <= 20; i++)
                {
                    DropDownList ddl = (DropDownList)cph.FindControl("ddlProductName" + i.ToString());
                    if (ddl != null && ddl.SelectedIndex != 0)
                    {
                        TextBox quantityTextbox = (TextBox)cph.FindControl("txtQuantity" + i.ToString());

                        string dcMaxID = @"SELECT MAX(deliveryChallanId) FROM tblDeliveryChallan";

                        cmd = new SqlCommand(dcMaxID, con);
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            deliveryChallanId = Convert.ToInt32(result);
                        }

                        inserttblDeliveryChallanItem += "INSERT INTO tblDeliveryChallanItem (DeliveryChallanItemQuantity, DeliveryChallanItemProductId, deliveryChallanId)VALUES(" + quantityTextbox.Text + ", " + ddl.SelectedValue + " , " + deliveryChallanId + ");";
                    }
                }
                cmd = new SqlCommand(inserttblDeliveryChallanItem, con);
                cmd.ExecuteNonQuery();
            }

            if (rbSales.Selected)
            {
                string originalFile = Server.MapPath("~/PDF/GNEnterpriseDelivery_Challan_GST.pdf");
                //string newFile = @"C:\Software\GNEnterprises\PDF\GNEnterpriseDeliveryChallan.pdf";
                newFile = @"C:\Software\PDF\GNEnterprises\DeliveryChallan\Sales\" + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + "GNEnterpriseDeliveryChallanSale.pdf";
                //newFile = @"~/../../PDF/GNEnterprises/DeliveryChallan/Sales/" + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + "GNEnterpriseDeliveryChallanSale.pdf";

                using (var reader = new PdfReader(originalFile))
                {
                    using (var fileStream = new FileStream(Path.Combine(newFile), FileMode.Create, FileAccess.Write))
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

                        document.Open();

                        for (var i = 1; i <= reader.NumberOfPages; i++)
                        {
                            document.NewPage();

                            var baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            var importedPage = writer.GetImportedPage(reader, i);
                            var contentByte = writer.DirectContent;

                            contentByte.BeginText();
                            contentByte.SetFontAndSize(baseFont, 12);
                            contentByte.SetColorFill(BaseColor.BLACK);

                            //DeliveryChallan
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, deliveryChallanId.ToString(), 86, 693, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDate.Text, 417, 687, 0);

                            if (ddlPartyName.SelectedIndex == 0)
                            {
                                //ddlPartyName.SelectedIndex = 0;
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 95, 689, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue, 86, 667, 0);
                            }

                           // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtAddress.Text, 117, 645, 0);

                            ColumnText ct2 = new ColumnText(writer.DirectContent);
                            ct2.SetSimpleColumn(117, 660, 540f, 100f);
                            BaseFont baseFont2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                            Font font2 = new Font(baseFont2, 11, Font.NORMAL, BaseColor.BLACK);

                            ct2.AddText(new Paragraph(txtAddress.Text, font2));
                            while (ColumnText.HasMoreText(ct2.Go()))
                            {
                                ct2.SetSimpleColumn(0, 0, 100f, 100f);
                                ct2.AddText(new Phrase(""));
                            }

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtTransport.Text, 127, 585, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtRNo.Text, 341, 584, 0);

                            //DeliveryChallanItem
                            if (ddlProductName1.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 100, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO1.Text, 53, 538, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue1, 97, 538, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN1.Text, 300, 614, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity1.Text + txtUnit1.Text, 539, 538, 0);
                                //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice1.Text, 393, 614, 0);
                                //     contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal1.Text, 456, 614, 0);

                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 130, 164, 0);
                            }

                            if (ddlProductName2.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 100, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO2.Text, 53, 521, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue2, 97, 521, 0);
                                //     contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN2.Text, 300, 596, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity2.Text + txtUnit2.Text, 539, 521, 0);
                                //       contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice2.Text, 393, 596, 0);
                                //        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal2.Text, 456, 596, 0);
                            }

                            if (ddlProductName3.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO3.Text, 53, 504, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue3, 97, 504, 0);
                                //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN3.Text, 300, 578, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity3.Text + txtUnit3.Text, 539, 504, 0);
                                //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice3.Text, 393, 578, 0);
                                //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal3.Text, 456, 578, 0);
                            }

                            if (ddlProductName4.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO4.Text, 53, 487, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue4, 97, 487, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN4.Text, 300, 560, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity4.Text + txtUnit4.Text, 539, 487, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice4.Text, 393, 560, 0);
                                //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal4.Text, 456, 560, 0);
                            }

                            if (ddlProductName5.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO5.Text, 53, 470, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue5, 97, 470, 0);
                                //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN5.Text, 300, 542, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity5.Text + txtUnit5.Text, 539, 470, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice5.Text, 393, 542, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal5.Text, 456, 542, 0);
                            }

                            if (ddlProductName6.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO6.Text, 53, 453, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue6, 97, 453, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN6.Text, 300, 524, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity6.Text + txtUnit6.Text, 539, 453, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice6.Text, 393, 524, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal6.Text, 456, 524, 0);
                            }

                            if (ddlProductName7.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO7.Text, 53, 436, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue7, 97, 436, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN7.Text, 300, 507, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity7.Text + txtUnit7.Text, 539, 436, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice7.Text, 393, 508, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal2.Text, 456, 508, 0);
                            }

                            if (ddlProductName8.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO8.Text, 53, 419, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue8, 97, 419, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity8.Text + txtUnit8.Text, 539, 419, 0);
                                //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice8.Text, 393, 490, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal8.Text, 456, 490, 0);
                            }

                            if (ddlProductName9.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO9.Text, 53, 401, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue9, 97, 401, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity9.Text + txtUnit9.Text, 539, 401, 0);
                                //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice9.Text, 393, 539, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal9.Text, 456, 539, 0);
                            }

                            if (ddlProductName10.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO10.Text, 53, 384, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue10, 97, 384, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity10.Text + txtUnit10.Text, 539, 384, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice10.Text, 393, 454, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal10.Text, 456, 454, 0);
                            }

                            if (ddlProductName11.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO11.Text, 53, 366, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue11, 97, 366, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity11.Text + txtUnit11.Text, 539, 366, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice11.Text, 393, 438, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal11.Text, 456, 438, 0);
                            }

                            if (ddlProductName12.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO12.Text, 53, 349, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue12, 97, 349, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity12.Text + txtUnit12.Text, 539, 349, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice12.Text, 393, 420, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal12.Text, 456, 420, 0);
                            }

                            if (ddlProductName13.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO13.Text, 53, 332, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue13, 97, 332, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity13.Text + txtUnit13.Text, 539, 332, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice13.Text, 393, 402, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal13.Text, 456, 402, 0);
                            }

                            if (ddlProductName14.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO14.Text, 53, 314, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue14, 97, 314, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity14.Text + txtUnit14.Text, 539, 314, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice14.Text, 393, 384, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal14.Text, 456, 384, 0);
                            }

                            if (ddlProductName15.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO15.Text, 53, 295, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue15, 97, 295, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity15.Text + txtUnit15.Text, 539, 295, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice15.Text, 393, 368, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal15.Text, 456, 368, 0);
                            }

                            if (ddlProductName16.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO16.Text, 53, 278, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue16, 97, 278, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity16.Text + txtUnit16.Text, 539, 278, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice16.Text, 393, 350, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal16.Text, 456, 350, 0);
                            }

                            if (ddlProductName17.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO17.Text, 53, 261, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue17, 97, 261, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity17.Text + txtUnit17.Text, 539, 261, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice17.Text, 393, 332, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal12.Text, 456, 332, 0);
                            }

                            if (ddlProductName18.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO18.Text, 53, 244, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue18, 97, 244, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity18.Text + txtUnit18.Text, 539, 244, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice18.Text, 393, 314, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal18.Text, 456, 314, 0);
                            }

                            if (ddlProductName19.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO19.Text, 53, 225, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue19, 97, 225, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity19.Text + txtUnit19.Text, 539, 225, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice19.Text, 393, 296, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal19.Text, 456, 296, 0);
                            }

                            if (ddlProductName20.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO20.Text, 53, 208, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue20, 97, 208, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity20.Text + txtUnit20.Text, 539, 208, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice20.Text, 393, 278, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal20.Text, 456, 278, 0);
                            }


                            /*contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN8.Text, 300, 489, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN9.Text, 300, 473, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN10.Text, 300, 456, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN11.Text, 300, 435, 0);

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN12.Text, 300, 423, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN13.Text, 300, 405, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN14.Text, 300, 387, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN15.Text, 300, 369, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN16.Text, 300, 351, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN17.Text, 300, 333, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN18.Text, 300, 314, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN19.Text, 300, 297, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN20.Text, 300, 279, 0);

                            SRNO
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo1.Text, 55, 209, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo2.Text, 55, 191, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo3.Text, 55, 173, 0);

                            //Total
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalValue.Text, 443, 192, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ddlGST.SelectedItem.Value + " %", 390, 176, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 174, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblRoundOffValue.Text, 443, 158, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffValue.Text, 443, 143, 0);*/

                            contentByte.EndText();
                            contentByte.AddTemplate(importedPage, 0, 0);
                        }

                        document.Close();
                        writer.Close();

                        filename = ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + time.Replace(" ", "_") + "_" + "GNEnterpriseDeliveryChallanSale.pdf";
                        /*
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename= " + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + time.Replace(" ", "_") + "_" + "GNEnterpriseDeliveryChallanSale.pdf");
                        Response.TransmitFile(newFile);
                        Response.End();
                        */
                    }
                }
            }
            else
            {
                string originalFile = Server.MapPath("~/PDF/GNEnterpriseDelivery_Challan_Without_GST.pdf");
                //string newFile = @"C:\Software\GNEnterprises\PDF\GNEnterpriseDeliveryChallan.pdf";
                newFile = @"C:\Software\PDF\GNEnterprises\DeliveryChallan\Quotation\" + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + "GNEnterpriseDeliveryChallanQuote.pdf";
                //newFile = @"C:\Software\PDF\GNEnterprises\DeliveryChallan\Quotation\" + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + "GNEnterpriseDeliveryChallanQuote.pdf";

                using (var reader = new PdfReader(originalFile))
                {
                    using (var fileStream = new FileStream(Path.Combine(newFile), FileMode.Create, FileAccess.Write))
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

                        document.Open();

                        for (var i = 1; i <= reader.NumberOfPages; i++)
                        {
                            document.NewPage();

                            var baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            var importedPage = writer.GetImportedPage(reader, i);
                            var contentByte = writer.DirectContent;

                            contentByte.BeginText();
                            contentByte.SetFontAndSize(baseFont, 12);
                            contentByte.SetColorFill(BaseColor.BLACK);

                            //DeliveryChallan
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, deliveryChallanId.ToString(), 86, 693, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtDate.Text, 417, 687, 0);

                            if (ddlPartyName.SelectedIndex == 0)
                            {
                                //ddlPartyName.SelectedIndex = 0;
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 95, 689, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue, 86, 667, 0);
                            }

                            // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtAddress.Text, 117, 645, 0);

                            ColumnText ct2 = new ColumnText(writer.DirectContent);
                            ct2.SetSimpleColumn(117, 660, 540f, 100f);
                            BaseFont baseFont2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED);
                            Font font2 = new Font(baseFont2, 11, Font.NORMAL, BaseColor.BLACK);

                            ct2.AddText(new Paragraph(txtAddress.Text, font2));
                            while (ColumnText.HasMoreText(ct2.Go()))
                            {
                                ct2.SetSimpleColumn(0, 0, 100f, 100f);
                                ct2.AddText(new Phrase(""));
                            }

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtTransport.Text, 127, 585, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtRNo.Text, 341, 584, 0);

                            //DeliveryChallanItem
                            if (ddlProductName1.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 100, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO1.Text, 53, 538, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue1, 97, 538, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN1.Text, 300, 614, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity1.Text + txtUnit1.Text, 539, 538, 0);
                                //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice1.Text, 393, 614, 0);
                                //     contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal1.Text, 456, 614, 0);

                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffInWords.Text, 130, 164, 0);
                            }

                            if (ddlProductName2.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 100, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO2.Text, 53, 521, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue2, 97, 521, 0);
                                //     contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN2.Text, 300, 596, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity2.Text + txtUnit2.Text, 539, 521, 0);
                                //       contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice2.Text, 393, 596, 0);
                                //        contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal2.Text, 456, 596, 0);
                            }

                            if (ddlProductName3.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO3.Text, 53, 504, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue3, 97, 504, 0);
                                //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN3.Text, 300, 578, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity3.Text + txtUnit3.Text, 539, 504, 0);
                                //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice3.Text, 393, 578, 0);
                                //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal3.Text, 456, 578, 0);
                            }

                            if (ddlProductName4.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO4.Text, 53, 487, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue4, 97, 487, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN4.Text, 300, 560, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity4.Text + txtUnit4.Text, 539, 487, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice4.Text, 393, 560, 0);
                                //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal4.Text, 456, 560, 0);
                            }

                            if (ddlProductName5.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO5.Text, 53, 470, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue5, 97, 470, 0);
                                //    contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN5.Text, 300, 542, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity5.Text + txtUnit5.Text, 539, 470, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice5.Text, 393, 542, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal5.Text, 456, 542, 0);
                            }

                            if (ddlProductName6.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO6.Text, 53, 453, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue6, 97, 453, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN6.Text, 300, 524, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity6.Text + txtUnit6.Text, 539, 453, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice6.Text, 393, 524, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal6.Text, 456, 524, 0);
                            }

                            if (ddlProductName7.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO7.Text, 53, 436, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue7, 97, 436, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN7.Text, 300, 507, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity7.Text + txtUnit7.Text, 539, 436, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice7.Text, 393, 508, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal2.Text, 456, 508, 0);
                            }

                            if (ddlProductName8.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO8.Text, 53, 419, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue8, 97, 419, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity8.Text + txtUnit8.Text, 539, 419, 0);
                                //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice8.Text, 393, 490, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal8.Text, 456, 490, 0);
                            }

                            if (ddlProductName9.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO9.Text, 53, 401, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue9, 97, 401, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity9.Text + txtUnit9.Text, 539, 401, 0);
                                //   contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice9.Text, 393, 539, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal9.Text, 456, 539, 0);
                            }

                            if (ddlProductName10.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO10.Text, 53, 384, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue10, 97, 384, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity10.Text + txtUnit10.Text, 539, 384, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice10.Text, 393, 454, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal10.Text, 456, 454, 0);
                            }

                            if (ddlProductName11.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO11.Text, 53, 366, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue11, 97, 366, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity11.Text + txtUnit11.Text, 539, 366, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice11.Text, 393, 438, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal11.Text, 456, 438, 0);
                            }

                            if (ddlProductName12.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO12.Text, 53, 349, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue12, 97, 349, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity12.Text + txtUnit12.Text, 539, 349, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice12.Text, 393, 420, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal12.Text, 456, 420, 0);
                            }

                            if (ddlProductName13.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO13.Text, 53, 332, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue13, 97, 332, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity13.Text + txtUnit13.Text, 539, 332, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice13.Text, 393, 402, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal13.Text, 456, 402, 0);
                            }

                            if (ddlProductName14.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO14.Text, 53, 314, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue14, 97, 314, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity14.Text + txtUnit14.Text, 539, 314, 0);
                                //  contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice14.Text, 393, 384, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal14.Text, 456, 384, 0);
                            }

                            if (ddlProductName15.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO15.Text, 53, 295, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue15, 97, 295, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity15.Text + txtUnit15.Text, 539, 295, 0);
                                // contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice15.Text, 393, 368, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal15.Text, 456, 368, 0);
                            }

                            if (ddlProductName16.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO16.Text, 53, 278, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue16, 97, 278, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity16.Text + txtUnit16.Text, 539, 278, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice16.Text, 393, 350, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal16.Text, 456, 350, 0);
                            }

                            if (ddlProductName17.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO17.Text, 53, 261, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue17, 97, 261, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity17.Text + txtUnit17.Text, 539, 261, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice17.Text, 393, 332, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal12.Text, 456, 332, 0);
                            }

                            if (ddlProductName18.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO18.Text, 53, 244, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue18, 97, 244, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity18.Text + txtUnit18.Text, 539, 244, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice18.Text, 393, 314, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal18.Text, 456, 314, 0);
                            }

                            if (ddlProductName19.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO19.Text, 53, 225, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue19, 97, 225, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity19.Text + txtUnit19.Text, 539, 225, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice19.Text, 393, 296, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal19.Text, 456, 296, 0);
                            }

                            if (ddlProductName20.SelectedIndex == 0)
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 97, 614, 0);
                            }
                            else
                            {
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblSRNO20.Text, 53, 208, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, selectedValue20, 97, 208, 0);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, txtQuantity20.Text + txtUnit20.Text, 539, 208, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtUnitPrice20.Text, 393, 278, 0);
                                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotal20.Text, 456, 278, 0);
                            }


                            /*contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN8.Text, 300, 489, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN9.Text, 300, 473, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN10.Text, 300, 456, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN11.Text, 300, 435, 0);

                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN12.Text, 300, 423, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN13.Text, 300, 405, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN14.Text, 300, 387, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN15.Text, 300, 369, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN16.Text, 300, 351, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN17.Text, 300, 333, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN18.Text, 300, 314, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN19.Text, 300, 297, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtHSN20.Text, 300, 279, 0);

                            SRNO
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo1.Text, 55, 209, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo2.Text, 55, 191, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, txtSRNo3.Text, 55, 173, 0);

                            //Total
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalValue.Text, 443, 192, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ddlGST.SelectedItem.Value + " %", 390, 176, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblTotalGST.Text, 443, 174, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblRoundOffValue.Text, 443, 158, 0);
                            contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, lblGrandTotalValueWithGSTAfterRoundOffValue.Text, 443, 143, 0);*/

                            contentByte.EndText();
                            contentByte.AddTemplate(importedPage, 0, 0);
                        }

                        document.Close();
                        writer.Close();

                        filename = ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + time.Replace(" ", "_") + "_" + "GNEnterpriseDeliveryChallanQuote.pdf";

                        /*
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename= " + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + time.Replace(" ", "_") + "_" + "GNEnterpriseDeliveryChallanSale.pdf");
                        Response.TransmitFile(newFile);
                        Response.End();


                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename= " + ddlPartyName.SelectedItem.Text.Replace(" ", "_") + "_" + txtDate.Text.ToString().Replace("-", "_") + "_" + time.Replace(" ", "_") + "_" + "GNEnterpriseDeliveryChallanQuote.pdf");
                        Response.TransmitFile(newFile);
                        Response.End();
                        */
                    }
                }
            }
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename= " + filename);
            Response.TransmitFile(newFile);
            Response.Flush();
            Response.End();
        }
       
        protected void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            if (cph != null)
            {
                for (int j = 1; j <= 20; j++)
                {
                    string ddlName = "ddlProductName" + j.ToString();
                    string txtName = "txtQuantity" + j.ToString();
                    DropDownList ddl = (DropDownList)cph.FindControl(ddlName);
                    TextBox tb = (TextBox)cph.FindControl(txtName);
                    if (ddl != null)
                    {
                        ddl.ClearSelection(); // Clear the previously selected item, if any.
                        ddl.SelectedIndex = 0;
                        //ddl.SelectedTextbox = 0;
                    }
                    if (tb != null)
                    {
                        tb.Text = "";
                    }
                }
            }
            if (rblOptions.SelectedValue == "Sales")
            {
                //Handle Sales selection
                //rbSales.Checked = false;

                using (SqlConnection con = new SqlConnection(CS))
                {

                    DropDownListSalesId.AppendDataBoundItems = true;
                    String strQuery = "SELECT saleId, partyName FROM tblSale, tblParty where tblSale.partyId = tblParty.partyId order by saleId desc ";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        DropDownListSalesId.Items.Clear();
                        txtAddress.Text = ""; // clear the existing text
                        txtDate.Text = ""; // clear existing date
                        DropDownListSalesId.Items.Insert(0, new ListItem("Select Sales", ""));
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
                            DropDownListSalesId.Items.Add(item);
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
            else if (rblOptions.SelectedValue == "Quotation")
            {
                // Handle Quotation selection

                using (SqlConnection con = new SqlConnection(CS))
                {

                    DropDownListSalesId.Items.Insert(0, new ListItem("Select Quatation", ""));
                    DropDownListSalesId.AppendDataBoundItems = true;
                    String strQuery = "SELECT quotationId, partyName FROM tblQuotation, tblParty where tblQuotation.quotationPartyId = tblParty.partyId order by quotationId desc";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        DropDownListSalesId.Items.Clear();
                        txtAddress.Text = ""; // clear the existing text
                        txtDate.Text = ""; // clear existing date
                        DropDownListSalesId.Items.Insert(0, new ListItem("Select Quatation", ""));
                        SqlDataReader dr = cmd.ExecuteReader();
                        /*
                                            DropDownListSalesId.DataSource = cmd.ExecuteReader();
                                            DropDownListSalesId.DataTextField = "partyName";
                                            DropDownListSalesId.DataValueField = "partyId";
                                            DropDownListSalesId.DataBind();
                                            */
                        while (dr.Read())
                        {
                            ListItem item = new ListItem(dr["quotationId"].ToString() + " - " + dr["partyName"].ToString(), dr["quotationId"].ToString());
                            DropDownListSalesId.Items.Add(item);
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
        }

        protected void DropDownListSalesId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbSales.Selected)
            {
                int saleId;
                if (int.TryParse(DropDownListSalesId.SelectedValue, out saleId))
                {
                    string sql = @"SELECT p.partyName, p.partyAddress, s.saleDate, pr.productName,pr.productUnit, si.itemSalesQuantity 
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
                            string partyAddress = reader["partyAddress"].ToString();
                            txtAddress.Text = partyAddress;
                            //string saleDate = reader["saleDate"].ToString();
                            //DateTime date = DateTime.ParseExact(saleDate, "yy-MM-dd", CultureInfo.InvariantCulture);
                            //string formattedDate = date.ToString("dd-MM-yy");
                            //txtDate.Text = reader.GetDateTime(2);
                            //ddlPartyName.ClearSelection();
                            DateTime saleDate = (DateTime)reader["saleDate"];
                            string formattedDate = saleDate.ToString("dd-MM-yyyy");
                            txtDate.Text = formattedDate;



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

                        //ddlPartyName.Items.Clear();
                        //ddlProductName1.Items.Clear();
                        //txtQuantity20.Text = string.Empty;

                        ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                        if (cph != null)
                        {
                            for (int j = 1; j <= 20; j++)
                            {
                                string ddlName = "ddlProductName" + j.ToString();
                                string txtName = "txtQuantity" + j.ToString();
                                string txtUnit = "txtUnit" + j.ToString();
                                DropDownList ddl = (DropDownList)cph.FindControl(ddlName);
                                TextBox tb = (TextBox)cph.FindControl(txtName);
                                TextBox tb1 = (TextBox)cph.FindControl(txtUnit);
                                if (ddl != null)
                                {
                                    ddl.ClearSelection(); // Clear the previously selected item, if any.
                                    ddl.SelectedIndex = 0;
                                    //ddl.SelectedTextbox = 0;
                                }
                                if (tb != null || tb1 != null)
                                {
                                    tb.Text = "";
                                    tb1.Text = "";
                                }
                            }
                            int i = 1;
                            while (reader.Read())
                            {
                                ddlPartyName.Items.Add(new ListItem(reader["partyName"].ToString()));
                                //ddlProductName1.Items.Add(new ListItem(reader["productName"].ToString()));

                                string ddlpartyName = "partyName".ToString();

                                string ddlName = "ddlProductName" + i.ToString();
                                string txtName = "txtQuantity" + i.ToString();
                                string txtUnit = "txtUnit" + i.ToString();
                                DropDownList ddlparty = (DropDownList)cph.FindControl(ddlpartyName);
                                DropDownList ddl = (DropDownList)cph.FindControl(ddlName);
                                TextBox tb = (TextBox)cph.FindControl(txtName);
                                TextBox tb1 = (TextBox)cph.FindControl(txtUnit);
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
                                    tb.Text = reader["itemSalesQuantity"].ToString();
                                    tb1.Text = reader["productUnit"].ToString();

                                }                               
                                i++;
                            }
                            reader.Close();

                        }
                    }
                }
            }
            else
            {
                int quotationId;
                if (int.TryParse(DropDownListSalesId.SelectedValue, out quotationId))
                {
                    string sql = @"SELECT p.partyName, p.partyAddress,q.quotationDate, pr.productName,pr.productUnit, qi.itemQuotationQuantity
                                    FROM tblQuotation q
                                    JOIN tblQuotationItem qi ON q.quotationId = qi.saleQuotationId
                                    JOIN tblProduct pr ON pr.productId = qi.QuotationProductId
                                    JOIN tblParty p ON p.partyId = q.quotationPartyId
                                    WHERE q.quotationId = @quotationId";
                    using (SqlConnection conn = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@quotationId", quotationId);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        // select party in ddlparty
                        while (reader.Read())
                        {
                            string partyAddress = reader["partyAddress"].ToString();
                            txtAddress.Text = partyAddress;
                            //string saleDate = reader["saleDate"].ToString();
                            //DateTime date = DateTime.ParseExact(saleDate, "yy-MM-dd", CultureInfo.InvariantCulture);
                            //string formattedDate = date.ToString("dd-MM-yy");
                            //txtDate.Text = reader.GetDateTime(2);
                            //ddlPartyName.ClearSelection();
                            DateTime saleDate = (DateTime)reader["quotationDate"];
                            string formattedDate = saleDate.ToString("dd-MM-yyyy");
                            txtDate.Text = formattedDate;



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
                        cmd.Parameters.AddWithValue("@quotationId", quotationId);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        //ddlPartyName.Items.Clear();
                        //ddlProductName1.Items.Clear();
                        //txtQuantity20.Text = string.Empty;

                        ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                        if (cph != null)
                        {
                            for (int j = 1; j <= 20; j++)
                            {
                                string ddlName = "ddlProductName" + j.ToString();
                                string txtName = "txtQuantity" + j.ToString();
                                string txtUnit = "txtUnit" + j.ToString();
                                DropDownList ddl = (DropDownList)cph.FindControl(ddlName);
                                TextBox tb = (TextBox)cph.FindControl(txtName);
                                TextBox tb1 = (TextBox)cph.FindControl(txtUnit);
                                if (ddl != null)
                                {
                                    ddl.ClearSelection(); // Clear the previously selected item, if any.
                                    ddl.SelectedIndex = 0;
                                    //ddl.SelectedTextbox = 0;
                                }
                                if (tb != null || tb1 != null)
                                {
                                    tb.Text = "";
                                    tb1.Text = "";
                                }
                            }
                            int i = 1;
                            while (reader.Read())
                            {
                                ddlPartyName.Items.Add(new ListItem(reader["partyName"].ToString()));
                                //ddlProductName1.Items.Add(new ListItem(reader["productName"].ToString()));

                                string ddlpartyName = "partyName".ToString();

                                string ddlName = "ddlProductName" + i.ToString();
                                string txtName = "txtQuantity" + i.ToString();
                                string txtUnit = "txtUnit" + i.ToString();
                                DropDownList ddlparty = (DropDownList)cph.FindControl(ddlpartyName);
                                DropDownList ddl = (DropDownList)cph.FindControl(ddlName);
                                TextBox tb = (TextBox)cph.FindControl(txtName);
                                TextBox tb1 = (TextBox)cph.FindControl(txtUnit);
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
                                    tb.Text = reader["itemQuotationQuantity"].ToString();
                                    tb1.Text = reader["productUnit"].ToString();

                                }

                                /*
                                                    if (i == 1)
                                                    {
                                                        string selectedItemText = reader["productName"].ToString();
                                                        ListItem selectedItem = ddlProductName1.Items.FindByText(selectedItemText);
                                                        if (selectedItem != null)
                                                        {
                                                            ddlProductName1.ClearSelection(); // Clear the previously selected item, if any.
                                                                                              //ddlProductName1.Items.FindByText(selectedItemText).Selected = true;
                                                            selectedItem.Selected = true; // Set the new item as selected.
                                                            //break;
                                                        }
                                                        txtQuantity1.Text = reader["itemSalesQuantity"].ToString();
                                                    }
                                                    if (i == 2)
                                                    {
                                                        string selectedItemText = reader["productName"].ToString();
                                                        ListItem selectedItem = ddlProductName2.Items.FindByText(selectedItemText);
                                                        if (selectedItem != null)
                                                        {
                                                            ddlProductName2.ClearSelection(); // Clear the previously selected item, if any.
                                                                                              //ddlProductName1.Items.FindByText(selectedItemText).Selected = true;
                                                            selectedItem.Selected = true; // Set the new item as selected.
                                                            //break;
                                                        }
                                                        txtQuantity2.Text = reader["itemSalesQuantity"].ToString();
                                                    }
                                                    if (i == 3)
                                                    {
                                                        string selectedItemText = reader["productName"].ToString();
                                                        ListItem selectedItem = ddlProductName2.Items.FindByText(selectedItemText);
                                                        if (selectedItem != null)
                                                        {
                                                            ddlProductName3.ClearSelection(); // Clear the previously selected item, if any.
                                                                                              //ddlProductName1.Items.FindByText(selectedItemText).Selected = true;
                                                            selectedItem.Selected = true; // Set the new item as selected.
                                                            break;
                                                        }
                                                        txtQuantity3.Text = reader["itemSalesQuantity"].ToString();
                                                    }
                                  */
                                i++;
                            }
                            reader.Close();

                        }
                    }
                }
            }
        }
    }
}