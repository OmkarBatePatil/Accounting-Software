using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace GNEnterprise
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack == true)
            {
                Get_Xml();

            }
        }
        void Get_Xml()
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(Server.MapPath("~/Data/Item.xml"));
            if (dataSet != null && dataSet.HasChanges())
            {
                gvItem.DataSource = dataSet;
                gvItem.DataBind();
            }
        }
        protected void linkBtnInsert_Click(object sender, EventArgs e)
        {
            Insert_XML();
        }
        void Insert_XML()
        {
            TextBox txtProductName = (TextBox)gvItem.FooterRow.FindControl("txtProductName");
            TextBox txtQuantity = (TextBox)gvItem.FooterRow.FindControl("txtQuantity");
            TextBox txtUnitPrice = (TextBox)gvItem.FooterRow.FindControl("txtUnitPrice");
            TextBox txtTotal = (TextBox)gvItem.FooterRow.FindControl("txtTotal");

            XmlDocument MyXmlDocument = new XmlDocument();
            MyXmlDocument.Load(Server.MapPath("~/Data/Item.xml"));

            XmlElement ParentElement = MyXmlDocument.CreateElement("Item");

            XmlElement ProductName = MyXmlDocument.CreateElement("ProductName");
            ProductName.InnerText = txtProductName.Text;

            XmlElement Quantity = MyXmlDocument.CreateElement("Quantity");
            Quantity.InnerText = txtQuantity.Text;

            XmlElement UnitPrice = MyXmlDocument.CreateElement("UnitPrice");
            UnitPrice.InnerText = txtUnitPrice.Text;

            XmlElement Total = MyXmlDocument.CreateElement("Total");
            Total.InnerText = txtTotal.Text;

            ParentElement.AppendChild(ProductName);
            ParentElement.AppendChild(Quantity);
            ParentElement.AppendChild(UnitPrice);
            ParentElement.AppendChild(Total);

            MyXmlDocument.DocumentElement.AppendChild(ParentElement);
            MyXmlDocument.Save(Server.MapPath("~/Data/Item.xml"));

            Get_Xml();
        }
    }
}