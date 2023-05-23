<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormPurchase.aspx.cs" Inherits="GNEnterprise.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtDate.ClientID %>").datepicker(
                {
                    dateFormat: 'dd-mm-yy',
                    changeMonth: true,
                    changeYear: true
                }).datepicker('setDate', '0');
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 48%;
        }

        .auto-style5 {
            width: 300px;
        }

        .auto-style10 {
            width: 138px;
        }

        .auto-style11 {
            margin-left: 3px;
        }

        .auto-style12 {
            width: 145px;
        }

        .auto-style15 {
            margin-left: 0px;
        }

        .auto-style17 {
            width: 300px;
        }

        .auto-style18 {
            width: 214px;
        }
    </style>
    <table class="auto-style1" id="tblItem ">
        <tr>
            <td class="auto-style5">
                <asp:Label ID="lblPurchaseID" runat="server" Text="Purchase ID -"></asp:Label>
            </td>
            <td class="auto-style10">
                <asp:Label ID="lblPurchaseIDValue" runat="server" Text="101"></asp:Label>
            </td>
            <td class="auto-style17">
                <asp:Label ID="lblPartyName" runat="server" Text="Party Name -"></asp:Label>
            </td>
            <td class="auto-style5">
                <asp:TextBox ID="txtPartyName" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style12">
                <asp:Label ID="lblDate" runat="server" Text="Date - "></asp:Label>
            </td>
            <td class="auto-style18">
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvItem" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" AutoGenerateColumns="False"
        ShowFooter="True" HorizontalAlign="Center">
        <Columns>
            <asp:TemplateField HeaderText="SR.NO">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ImageUrl="~/Image/add.png" OnClick="linkBtnInsert_Click" ID="addIB" runat="server" CommandName="AddNew" ToolTip="AddNew" Width="30px" Height="30px" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Price" SortExpression="UnitPrice">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtUnitPrice" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total" SortExpression="Total">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Total") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Total") %>'></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</asp:Content>
