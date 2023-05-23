<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormParty.aspx.cs"
    Inherits="GNEnterprise.WebForm10" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style3 {
            width: 217px;
        }

        .auto-style9 {
            width: 186px;
        }

        .auto-style21 {
            width: 182px;
            height: 11px;
        }

        .auto-style30 {
            width: 139px;
        }

        .auto-style31 {
            width: 139px;
            height: 11px;
        }

        .auto-style33 {
            width: 246px;
            height: 11px;
        }

        .auto-style34 {
            width: 246px;
        }

        .auto-style35 {
            width: 194px;
        }

        .auto-style37 {
            width: 182px;
        }

        .auto-style38 {
            width: 201px;
        }

        .auto-style39 {
            width: 201px;
            height: 11px;
        }

        .auto-style40 {
            width: 154px;
        }

        .auto-style42 {
            width: 191px;
            height: 11px;
        }
        .auto-style43 {
            width: 191px;
        }
        .auto-style44 {
            width: 191px;
            height: 29px;
        }
        .auto-style45 {
            width: 246px;
            height: 29px;
        }
        .auto-style46 {
            width: 186px;
            height: 29px;
        }
        .auto-style47 {
            width: 182px;
            height: 29px;
        }
        .auto-style48 {
            width: 194px;
            height: 29px;
        }
        .auto-style49 {
            width: 191px;
            height: 27px;
        }
        .auto-style50 {
            width: 246px;
            height: 27px;
        }
        .auto-style51 {
            width: 186px;
            height: 27px;
        }
        .auto-style52 {
            width: 182px;
            height: 27px;
        }
        .auto-style53 {
            width: 154px;
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style43">
                <asp:Label ID="lblCodeNo" runat="server" Text="Code No."></asp:Label>
            </td>
            <td class="auto-style34">

                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>

            </td>
            <td class="auto-style30">
                <asp:Label ID="lblPartyType" runat="server" Text="Party Type"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblPriceList" runat="server" Text="Price List"></asp:Label>
            </td>
            <td class="auto-style40">
                <asp:Label ID="lblGSTType" runat="server" Text="GST Type"></asp:Label>
            </td>
            <td class="auto-style38">
                <asp:Label ID="lblVendorCode" runat="server" Text="Vendor Code"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style43">
                <asp:Label ID="lblpartyId" runat="server" Text="1001"></asp:Label>

            </td>
            <td class="auto-style34">

                <asp:TextBox ID="txtName" runat="server" Width="219px"></asp:TextBox>

            </td>
            <td class="auto-style3">
                <asp:DropDownList ID="ddlPartyType" runat="server">
                    <asp:ListItem>Select Party Type</asp:ListItem>
                    <asp:ListItem>Customer</asp:ListItem>
                    <asp:ListItem>Distributor</asp:ListItem>
                    <asp:ListItem>Both</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlPriceList" runat="server">
                    <asp:ListItem>Select Price List</asp:ListItem>
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>B</asp:ListItem>
                    <asp:ListItem>C</asp:ListItem>
                    <asp:ListItem>D</asp:ListItem>
                    <asp:ListItem>M</asp:ListItem>
                    <asp:ListItem>P</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style40">
                <asp:DropDownList ID="ddlGSTType" runat="server">
                    <asp:ListItem>Select GST Type</asp:ListItem>
                    <asp:ListItem>GST</asp:ListItem>
                    <asp:ListItem>IGST</asp:ListItem>
                    <asp:ListItem>RCM</asp:ListItem>
                    <asp:ListItem>URD</asp:ListItem>
                    <asp:ListItem>EXPORT</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style38">
                <asp:TextBox ID="txtVendorCode" runat="server" Width="219px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style43">
                <asp:Label ID="lblDiscount" runat="server" Text="Fix Discount %"></asp:Label>
            </td>
            <td class="auto-style34">
                <asp:Label ID="lblDefaultTransactionMode" runat="server" Text="Default Transaction Mode"></asp:Label>
            </td>
            <td class="auto-style30">
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
            </td>
            <td class="auto-style37">&nbsp;</td>
            <td class="auto-style40">
                <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
            </td>
            <td class="auto-style38">
                <asp:Label ID="lblPincode" runat="server" Text="Pincode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style42">

                <asp:TextBox ID="txtDiscount" runat="server" Width="219px"></asp:TextBox>

            </td>
            <td class="auto-style33">
                <asp:DropDownList ID="ddlTranMode" runat="server">
                    <asp:ListItem>Select Transaction Mode</asp:ListItem>
                    <asp:ListItem>Cash</asp:ListItem>
                    <asp:ListItem>Credit</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style31" colspan="2">
                <asp:TextBox ID="txtAddress" runat="server" Width="502px"></asp:TextBox>
            </td>
            <td class="auto-style21">
                <asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem>Select State</asp:ListItem>
                    <asp:ListItem>Andhra Pradesh</asp:ListItem>
                    <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                    <asp:ListItem>Assam</asp:ListItem>
                    <asp:ListItem>Bihar</asp:ListItem>
                    <asp:ListItem>Chhattisgarh</asp:ListItem>
                    <asp:ListItem>Goa</asp:ListItem>
                    <asp:ListItem>Gujarat</asp:ListItem>
                    <asp:ListItem>Haryana</asp:ListItem>
                    <asp:ListItem>Himachal Pradesh</asp:ListItem>
                    <asp:ListItem>Jharkhand</asp:ListItem>
                    <asp:ListItem>Karnataka</asp:ListItem>
                    <asp:ListItem>Kerala</asp:ListItem>
                    <asp:ListItem>Madhya Pradesh</asp:ListItem>
                    <asp:ListItem>Maharashtra</asp:ListItem>
                    <asp:ListItem>Manipur</asp:ListItem>
                    <asp:ListItem>Meghalaya</asp:ListItem>
                    <asp:ListItem>Mizoram</asp:ListItem>
                    <asp:ListItem>Nagaland</asp:ListItem>
                    <asp:ListItem>Odisha</asp:ListItem>
                    <asp:ListItem>Punjab</asp:ListItem>
                    <asp:ListItem>Rajasthan</asp:ListItem>
                    <asp:ListItem>Sikkim</asp:ListItem>
                    <asp:ListItem>Tamil Nadu</asp:ListItem>
                    <asp:ListItem>Telangana</asp:ListItem>
                    <asp:ListItem>Tripura</asp:ListItem>
                    <asp:ListItem>Uttar Pradesh</asp:ListItem>
                    <asp:ListItem>Uttarakhand</asp:ListItem>
                    <asp:ListItem>West Bengal</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style39">
                <asp:TextBox ID="txtPincode" runat="server" Width="219px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style43">
                <asp:Label ID="lblEwayPincode" runat="server" Text="Pincode for Eway Bill"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblRoute" runat="server" Text="Route"></asp:Label>
            </td>
            <td class="auto-style35">
                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No."></asp:Label>
            </td>
            <td class="auto-style9">
                <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No."></asp:Label>
            </td>
            <td class="auto-style40">
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
            </td>
            <td class="auto-style38">
                <asp:Label ID="lblNote0" runat="server" Text="Note"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style44">

                <asp:TextBox ID="txtEwayPincode" runat="server" Width="219px"></asp:TextBox>

            </td>
            <td class="auto-style45">

                <asp:TextBox ID="txtRoute" runat="server" Width="219px"></asp:TextBox>

            </td>
            <td class="auto-style46">
                <asp:TextBox ID="txtPhoneNo" runat="server" Width="219px"></asp:TextBox>
            </td>
            <td class="auto-style47">
                <asp:TextBox ID="txtMobileNo" runat="server" Width="219px"></asp:TextBox>
            </td>
            <td class="auto-style48">
                <asp:TextBox ID="txtEmail" runat="server" Width="219px"></asp:TextBox>
            </td>
            <td class="auto-style35" rowspan="2">
                <asp:TextBox ID="txtNote" runat="server" Width="219px" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style49">
                <asp:Label ID="lblGSTINNo" runat="server" Text="GSTIN No."></asp:Label>
            </td>
            <td class="auto-style50">
                <asp:Label ID="lblEGSTINNo" runat="server" Text="E-GSTIN No."></asp:Label>
            </td>
            <td class="auto-style51">
                <asp:Label ID="lblPANNo" runat="server" Text="PAN No."></asp:Label>
            </td>
            <td class="auto-style52">
                <asp:Label ID="lblContactInfo0" runat="server" Text="Contact Info."></asp:Label>
            </td>
            <td class="auto-style52">
                <asp:Label ID="lblCreditAmount" runat="server" Text="Credit Amount"></asp:Label>
            </td>
            <td class="auto-style53"></td>
        </tr>
        <tr>
            <td class="auto-style43">

                <asp:TextBox ID="txtGSTINNo" runat="server" Width="219px"></asp:TextBox>

            </td>
            <td class="auto-style3">

                <asp:TextBox ID="txtEGSTINNo" runat="server" Width="219px"></asp:TextBox>

            </td>
            <td class="auto-style35">
                <asp:TextBox ID="txtPANNo" runat="server" Width="219px"></asp:TextBox>
            </td>
            <td class="auto-style35">
                <asp:TextBox ID="txtContactInfo" runat="server" Width="219px"></asp:TextBox>
            </td>
            <td class="auto-style35">
                <asp:TextBox ID="txtCreditAmount" runat="server" Width="219px"></asp:TextBox>
            </td>
            <td class="auto-style35">                
                <asp:Button ID="btnAddParty" runat="server" Text="Add Party" OnClick="btnAddParty_Click" />         
                <asp:Button ID="btnUpdateParty" runat="server" Text="Update Party" OnClick="btnUpdateParty_Click" /> 
            </td>            
        </tr>     
    </table>
    <asp:GridView ID="gvParty" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" HorizontalAlign="Center"
        AutoGenerateColumns="False" DataKeyNames="partyId" OnRowDataBound="gvParty_RowDataBound"
        OnSelectedIndexChanged="gvParty_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField HeaderText="SR.NO">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
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
                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("partyName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type" SortExpression="partyType">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("partyType") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPartyType" runat="server" Text='<%# Bind("partyType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price List" SortExpression="partyPriceList">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("partyPriceList") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPriceList" runat="server" Text='<%# Bind("partyPriceList") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GST Type" SortExpression="partyGSTType">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("partyGSTType") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblGSTType" runat="server" Text='<%# Bind("partyGSTType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Vendor Code" SortExpression="partyVendorCode">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("partyVendorCode") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblVendorCode" runat="server" Text='<%# Bind("partyVendorCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fix Discount" SortExpression="partyFixDiscount">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("partyFixDiscount") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("partyFixDiscount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Transaction Mode" SortExpression="partyDefaultTranMode">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("partyDefaultTranMode") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDefaultTransactionMode" runat="server" Text='<%# Bind("partyDefaultTranMode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address" SortExpression="partyAddress">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("partyAddress") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("partyAddress") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State" SortExpression="partyState">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("partyState") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("partyState") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pincode" SortExpression="partyPincode">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("partyPincode") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPincode" runat="server" Text='<%# Bind("partyPincode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eway Pincode" SortExpression="partyEwayPincode">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("partyEwayPincode") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblEwayPincode" runat="server" Text='<%# Bind("partyEwayPincode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Route" SortExpression="partyRoute">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("partyRoute") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblRoute" runat="server" Text='<%# Bind("partyRoute") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone No." SortExpression="partyPhone">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("partyPhone") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPhoneNo" runat="server" Text='<%# Bind("partyPhone") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mobile No." SortExpression="partyMobile">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("partyMobile") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("partyMobile") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" SortExpression="partyEmail">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("partyEmail") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("partyEmail") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact Info" SortExpression="partyContact">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("partyContact") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblContactInfo" runat="server" Text='<%# Bind("partyContact") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GSTIN No." SortExpression="partyGSTINNo">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("partyGSTINNo") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblGSTINNo" runat="server" Text='<%# Bind("partyGSTINNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EGSTIN No." SortExpression="partyEGSTINNo">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("partyEGSTINNo") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblEGSTINNo" runat="server" Text='<%# Bind("partyEGSTINNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PAN No." SortExpression="partyPANNNo">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("partyPANNNo") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPANNo" runat="server" Text='<%# Bind("partyPANNNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Note" SortExpression="partyNote">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("partyNote") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblNote" runat="server" Text='<%# Bind("partyNote") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Credit Amount" SortExpression="partyCreditAmount">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("partyCreditAmount") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblCreditAmount" runat="server" Text='<%# Bind("partyCreditAmount") %>'></asp:Label>
                </ItemTemplate>
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
    <asp:SqlDataSource ID="SqlDataParty" runat="server" ConnectionString="<%$ ConnectionStrings:dbGNEnterpriseConStr %>" 
        SelectCommand="SELECT * FROM [tblParty]"></asp:SqlDataSource>
</asp:Content>
