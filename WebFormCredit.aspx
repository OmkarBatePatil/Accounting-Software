<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="WebFormCredit.aspx.cs" Inherits="GNEnterprise.WebForm11" EnableEventValidation="false" %>

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

        $(document).on("click", function () {

        });
    </script>
    <style type="text/css">
        .auto-style5 {
            height: 10px;
            font-size: 12px;
            width: 197px;
        }

        .auto-style6 {
            width: 172px;
        }

        .auto-style7 {
            width: 99px;
        }

        .auto-style8 {
            width: 100%;
        }

        .auto-style9 {
            width: 395px;
        }

        .auto-style11 {
            width: 55%;
        }

        .auto-style13 {
            width: 318px;
        }

        .auto-style15 {
            width: 160px;
        }

        .auto-style16 {
            width: 111px;
        }

        .auto-style17 {
            width: 218px;
        }

        .auto-style20 {
            width: 96px;
        }

        .auto-style21 {
            width: 133px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="auto-style1">
        <tr>
            <td class="auto-style6">
                <asp:Label ID="lblPartyNameKey" runat="server" Text="Party Name -"></asp:Label>
            </td>
            <td class="auto-style5">
                <asp:Label ID="lblPartyNameValue" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style7">
                <asp:Label ID="lblDate" runat="server" Text="Date -"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="lblAmtPaid" runat="server" Text="Amount to Paid - "></asp:Label>
            </td>
            <td class="auto-style5">
                <asp:TextBox ID="txtAmtPaid" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style7"></td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="SAVE" />
            </td>
        </tr>
    </table>

    <br />
    <table class="auto-style8">
        <tr>
            <td class="auto-style9">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <%--<Columns>
                        <asp:TemplateField HeaderText="SR.NO">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Party Name" SortExpression="PartyName">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PartyName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPartyName" runat="server" Text='<%# Bind("PartyName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Amount" SortExpression="CreditAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CreditAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCreditAmount" runat="server" Text='<%# Bind("CreditAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="SR.NO">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="partyId" InsertVisible="False" SortExpression="partyId" Visible="False">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("partyId") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblpartyId" runat="server" Text='<%# Bind("partyId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="partyName">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("partyName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPartyName" runat="server" Text='<%# Bind("partyName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Amount" SortExpression="partyCreditAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("partyCreditAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCreditAmount" runat="server" Text='<%# Bind("partyCreditAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                <%--<asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Data/XMLCredit1.xml" TransformFile="~/Data/XSLTCredit1.xslt"></asp:XmlDataSource>--%>
                <table class="auto-style11">
                    <tr>
                        <td class="auto-style13"></td>
                        <td class="auto-style16"></td>
                        <td class="auto-style15"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style16"></td>
                        <td class="auto-style15"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <%-- <Columns>
                        <asp:TemplateField HeaderText="SR.NO">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Number" SortExpression="BillNumber">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BillNumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBillNumber" runat="server" Text='<%# Bind("BillNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Date" SortExpression="BillDate">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BillDate") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("BillDate") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Amount" SortExpression="BillAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("BillAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# Bind("BillAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending Amount" SortExpression="PendingAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("PendingAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPendingAmount" runat="server" Text='<%# Bind("PendingAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remaining Amount" SortExpression="RemainingAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("RemainingAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRemainingAmount" runat="server" Text='<%# Bind("RemainingAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="SR.NO">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Number" SortExpression="saleId">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("saleId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("saleId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Date" SortExpression="saleDate">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("saleDate") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("saleDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="partyId" HeaderText="partyId" SortExpression="partyId" Visible="False" />
                        <asp:TemplateField HeaderText="Bill Amount" SortExpression="saleBillAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("saleBillAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("saleBillAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Amount" SortExpression="salePaidAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("salePaidAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("salePaidAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remaining Amount" SortExpression="saleRemainingAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("saleRemainingAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("saleRemainingAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                <table class="auto-style11" id="tblBill2">
                    <tr>
                        <td class="auto-style17"></td>
                        <td class="auto-style20">
                            <asp:Label ID="Label3" runat="server" Text="Total -"></asp:Label>
                        </td>
                        <td class="auto-style21">
                            <asp:Label ID="Label4" runat="server" Text="Total -"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Total -"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style17">&nbsp;</td>
                        <td class="auto-style20">
                            <asp:Label ID="lblTotalBillAmount" runat="server" Text="Amount"></asp:Label>
                        </td>
                        <td class="auto-style21">
                            <asp:Label ID="lblTotalPendingAmount" runat="server" Text="Amount"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTotalRemainingAmount" runat="server" Text="Amount"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
    </table>

</asp:Content>
