<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormSale.aspx.cs"
    Inherits="GNEnterprise.WebForm7" EnableEventValidation="false" %>

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

        .auto-style10 {
            width: 138px;
        }

        .auto-style12 {
            width: 145px;
        }

        .auto-style17 {
            width: 300px;
        }

        .auto-style18 {
            width: 214px;
        }

        .auto-style19 {
            width: 150px;
        }

        .auto-style21 {
            width: 50px;
            height: 18px;
        }

        .auto-style22 {
            width: 154px;
            height: 18px;
        }

        .auto-style23 {
            width: 99%;
            height: 60px;
        }

        .auto-style24 {
            width: 100%;
            border: 1px solid;
        }

        .auto-style37 {
            border-style: solid;
            border-color: inherit;
            border-width: 1px;
            width: 10px;
            text-align: center;
        }

        .auto-style39 {
            border-style: solid;
            border-color: inherit;
            border-width: 1px;
            width: 10px;
            text-align: center;
            height: 23px;
        }

        .auto-style40 {
            border-style: solid;
            border-color: inherit;
            border-width: 1px;
            width: 10px;
            text-align: center;
            height: 18px;
        }

        .auto-style41 {
            width: 100%;
            height: 60px;
        }

        .auto-style42 {
            width: 31px;
            height: 18px;
        }

        .auto-style43 {
            width: 138px;
            height: 18px;
        }

        .auto-style45 {
            width: 18px;
        }

        .auto-style46 {
            width: 40px;
            height: 18px;
        }

        .auto-style49 {
            width: 50px;
        }

        .auto-style50 {
            width: 10px;
            height: 18px;
        }

        .auto-style51 {
            width: 10px;
        }

        .auto-style55 {
            height: 18px;
        }

        .auto-style57 {
            width: 13px;
            height: 18px;
        }

        .auto-style58 {
            width: 13px;
        }

        .auto-style59 {
            width: 40px;
        }

        .auto-style60 {
            width: 155px;
        }

        .auto-style61 {
            width: 155px;
            height: 18px;
        }

        .auto-style62 {
            width: 74px;
            height: 18px;
        }

        .auto-style63 {
            width: 74px;
        }

        .auto-style64 {
            width: 154px;
        }
        .auto-style65 {
            width: 77px;
        }
        .auto-style67 {
            width: 254px;
        }
        .auto-style68 {
            width: 236px;
        }
    </style>

    <table class="auto-style41" id="tblItem ">
        <tr>
            <td class="auto-style67">
                <asp:Label ID="Label1" runat="server" Text="Previous Sales"></asp:Label>
                <br />

            </td>
            <td class="auto-style65">
                <asp:DropDownList ID="ddlSales" runat="server" OnSelectedIndexChanged="ddlSales_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td class="auto-style17">&nbsp;</td>
            <td class="auto-style19">&nbsp;</td>
            <td class="auto-style12">&nbsp;</td>
            <td class="auto-style18">&nbsp;</td>
            <td class="auto-style18">&nbsp;</td>
            <td class="auto-style18">&nbsp;</td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18">
                <asp:Button ID="btnUpdateBill" runat="server" Text="UPDATE" OnClick="btnUpdateBill_Click" />
            </td>
            <td class="auto-style18">
                <asp:Button ID="btnPDF" runat="server" Text="PRINT" OnClick="btnPrint_Click" />
            </td>
        </tr>
    </table>

    <table class="auto-style41">
        <tr>
            <td class="auto-style68">
                <asp:Label ID="lblSaleID" runat="server" Text="Invoice No"></asp:Label>
                <br />

            </td>
            <td class="auto-style10">
                <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style17">
                <asp:Label ID="lblPartyName" runat="server" Text="Party Name"></asp:Label>
            </td>
            <td class="auto-style19">
                <asp:DropDownList ID="ddlPartyName" runat="server" OnSelectedIndexChanged="Dropdown1_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td class="auto-style12">
                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
            </td>
            <td class="auto-style18">
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtDateUpdate" runat="server"></asp:TextBox>
            </td>
            <td class="auto-style18">
                <asp:TextBox ID="txtGstNo" runat="server" AutoPostBack="False" placeholder="GST Number" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtChallanNo" runat="server" placeholder="Challan No"></asp:TextBox>
            </td>
            <td class="auto-style18">
                <asp:TextBox ID="txtAddress" runat="server" AutoPostBack="True" placeholder="Address" Visible="false"></asp:TextBox>
            </td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18"></td>
            <td class="auto-style18">&nbsp;</td>
            <td class="auto-style18">&nbsp;</td>
        </tr>
    </table>


    <table class="auto-style24" border="1" id="tblProduct">
        <tr>
            <th class="auto-style37">SR.NO</th>
            <th class="auto-style37">Product Name</th>
            <th class="auto-style37">HSN</th>
            <th class="auto-style37">Quantity</th>
            <th class="auto-style37">Unit</th>
            <th class="auto-style37">Unit Price</th>
            <th class="auto-style37">Amount</th>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO1" runat="server" Text="1"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName1" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN1" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity1" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit1" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice1" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO2" runat="server" Text="2"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName2" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN2" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity2" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit2" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice2" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style39">
                <asp:Label ID="lblSRNO3" runat="server" Text="3"></asp:Label>
            </td>
            <td class="auto-style39">
                <asp:DropDownList ID="ddlProductName3" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN3" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style39">
                <asp:TextBox ID="txtQuantity3" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit3" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style39">
                <asp:TextBox ID="txtUnitPrice3" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style39">
                <asp:Label ID="lblTotal3" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO4" runat="server" Text="4"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName4" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN4" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity4" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit4" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice4" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal4" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO5" runat="server" Text="5"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName5" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN5" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity5" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit5" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice5" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal5" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO6" runat="server" Text="6"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName6" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN6" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity6" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit6" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice6" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal6" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO7" runat="server" Text="7"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName7" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN7" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity7" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit7" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice7" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal7" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO8" runat="server" Text="8"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName8" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN8" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity8" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit8" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice8" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal8" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO9" runat="server" Text="9"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName9" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN9" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity9" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit9" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice9" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal9" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO10" runat="server" Text="10"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName10" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN10" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity10" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit10" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice10" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal10" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO11" runat="server" Text="11"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName11" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN11" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity11" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit11" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice11" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal11" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style40">
                <asp:Label ID="lblSRNO12" runat="server" Text="12"></asp:Label>
            </td>
            <td class="auto-style40">
                <asp:DropDownList ID="ddlProductName12" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN12" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style40">
                <asp:TextBox ID="txtQuantity12" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit12" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style40">
                <asp:TextBox ID="txtUnitPrice12" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style40">
                <asp:Label ID="lblTotal12" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO13" runat="server" Text="13"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName13" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN13" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity13" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit13" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice13" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal13" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO14" runat="server" Text="14"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName14" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN14" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity14" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit14" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice14" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal14" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO15" runat="server" Text="15"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName15" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN15" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity15" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit15" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice15" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal15" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO16" runat="server" Text="16"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName16" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN16" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity16" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit16" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice16" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal16" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO17" runat="server" Text="17"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName17" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN17" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity17" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit17" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice17" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal17" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO18" runat="server" Text="18"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName18" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN18" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity18" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit18" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice18" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal18" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO19" runat="server" Text="19"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName19" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN19" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity19" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit19" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice19" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal19" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style37">
                <asp:Label ID="lblSRNO20" runat="server" Text="20"></asp:Label>
            </td>
            <td class="auto-style37">
                <asp:DropDownList ID="ddlProductName20" runat="server" Height="20px" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtHSN20" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtQuantity20" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnit20" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:TextBox ID="txtUnitPrice20" runat="server" Width="177px" AutoPostBack="True" OnTextChanged="CalculateTotal"></asp:TextBox>
            </td>
            <td class="auto-style37">
                <asp:Label ID="lblTotal20" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

    <%--<asp:XmlDataSource ID="XmlDataItem" runat="server" DataFile="~/Data/Item.xml"
        TransformFile="~/Data/Item.xslt"></asp:XmlDataSource>--%><%--    <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999"
        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" DataKeyNames="tempSalesItemId" DataSourceID="SqlDataTemp"
        ForeColor="Black" HorizontalAlign="Center" ShowFooter="True" OnRowDataBound="gvSales_RowDataBound" OnSelectedIndexChanged="gvSales_SelectedIndexChanged1">
        <Columns>
            <asp:TemplateField HeaderText="SR.NO">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="tempSalesItemId" HeaderText="tempSalesItemId" InsertVisible="False" ReadOnly="True" SortExpression="tempSalesItemId" Visible="False" />
            <asp:TemplateField HeaderText="Product" SortExpression="tempSalesItemProductId">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlProductEdit" runat="server">
                    </asp:DropDownList>
                    <%--<asp:SqlDataSource runat="server" ID="SqlDataProduct" ConnectionString='<%$ ConnectionStrings:dbGNEnterpriseConStr %>'
                        DeleteCommand="DELETE FROM [tblProduct] WHERE [productId] = @productId"
                        InsertCommand="INSERT INTO [tblProduct] ([productCompany], [productName], [productUnit], [productStock], [productPurchasePrice], [productMRP], [productSalesPrice], [productDiscount], [productHSN], [productMinimumStock], [productMaximumStock], [subtypeId], [description1], [description2], [description3], [description4], [description5], [productShortCode], [productStockType], [productReorder], [productDiscountRs], [productStoreLocation], [productIGST], [productCGST], [productSGST], [productMasterPacking], [productSaleAper], [productSaleBper], [productSaleCper], [productSaleDper], [productSaleArs], [productSaleBrs], [productSaleCrs], [productSaleDrs], [productNote]) VALUES (@productCompany, @productName, @productUnit, @productStock, @productPurchasePrice, @productMRP, @productSalesPrice, @productDiscount, @productHSN, @productMinimumStock, @productMaximumStock, @subtypeId, @description1, @description2, @description3, @description4, @description5, @productShortCode, @productStockType, @productReorder, @productDiscountRs, @productStoreLocation, @productIGST, @productCGST, @productSGST, @productMasterPacking, @productSaleAper, @productSaleBper, @productSaleCper, @productSaleDper, @productSaleArs, @productSaleBrs, @productSaleCrs, @productSaleDrs, @productNote)"
                        SelectCommand="SELECT * FROM [tblProduct]"
                        UpdateCommand="UPDATE [tblProduct] SET [productCompany] = @productCompany, [productName] = @productName, [productUnit] = @productUnit, [productStock] = @productStock, [productPurchasePrice] = @productPurchasePrice, [productMRP] = @productMRP, [productSalesPrice] = @productSalesPrice, [productDiscount] = @productDiscount, [productHSN] = @productHSN, [productMinimumStock] = @productMinimumStock, [productMaximumStock] = @productMaximumStock, [subtypeId] = @subtypeId, [description1] = @description1, [description2] = @description2, [description3] = @description3, [description4] = @description4, [description5] = @description5, [productShortCode] = @productShortCode, [productStockType] = @productStockType, [productReorder] = @productReorder, [productDiscountRs] = @productDiscountRs, [productStoreLocation] = @productStoreLocation, [productIGST] = @productIGST, [productCGST] = @productCGST, [productSGST] = @productSGST, [productMasterPacking] = @productMasterPacking, [productSaleAper] = @productSaleAper, [productSaleBper] = @productSaleBper, [productSaleCper] = @productSaleCper, [productSaleDper] = @productSaleDper, [productSaleArs] = @productSaleArs, [productSaleBrs] = @productSaleBrs, [productSaleCrs] = @productSaleCrs, [productSaleDrs] = @productSaleDrs, [productNote] = @productNote WHERE [productId] = @productId">
                        <DeleteParameters>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                  <%--  <asp:SqlDataSource runat="server" ID="SqlDataProduct" ConnectionString='<%$ ConnectionStrings:dbGNEnterpriseConStr %>'
                        DeleteCommand="DELETE FROM [tblProduct] WHERE [productId] = @productId"
                        InsertCommand="INSERT INTO [tblProduct] ([productCompany], [productName], [productUnit], [productStock], [productPurchasePrice], [productMRP], [productSalesPrice], [productDiscount], [productHSN], [productMinimumStock], [productMaximumStock], [subtypeId], [description1], [description2], [description3], [description4], [description5], [productShortCode], [productStockType], [productReorder], [productDiscountRs], [productStoreLocation], [productIGST], [productCGST], [productSGST], [productMasterPacking], [productSaleAper], [productSaleBper], [productSaleCper], [productSaleDper], [productSaleArs], [productSaleBrs], [productSaleCrs], [productSaleDrs], [productNote]) VALUES (@productCompany, @productName, @productUnit, @productStock, @productPurchasePrice, @productMRP, @productSalesPrice, @productDiscount, @productHSN, @productMinimumStock, @productMaximumStock, @subtypeId, @description1, @description2, @description3, @description4, @description5, @productShortCode, @productStockType, @productReorder, @productDiscountRs, @productStoreLocation, @productIGST, @productCGST, @productSGST, @productMasterPacking, @productSaleAper, @productSaleBper, @productSaleCper, @productSaleDper, @productSaleArs, @productSaleBrs, @productSaleCrs, @productSaleDrs, @productNote)"
                        SelectCommand="SELECT * FROM [tblProduct]"
                        UpdateCommand="UPDATE [tblProduct] SET [productCompany] = @productCompany, [productName] = @productName, [productUnit] = @productUnit, [productStock] = @productStock, [productPurchasePrice] = @productPurchasePrice, [productMRP] = @productMRP, [productSalesPrice] = @productSalesPrice, [productDiscount] = @productDiscount, [productHSN] = @productHSN, [productMinimumStock] = @productMinimumStock, [productMaximumStock] = @productMaximumStock, [subtypeId] = @subtypeId, [description1] = @description1, [description2] = @description2, [description3] = @description3, [description4] = @description4, [description5] = @description5, [productShortCode] = @productShortCode, [productStockType] = @productStockType, [productReorder] = @productReorder, [productDiscountRs] = @productDiscountRs, [productStoreLocation] = @productStoreLocation, [productIGST] = @productIGST, [productCGST] = @productCGST, [productSGST] = @productSGST, [productMasterPacking] = @productMasterPacking, [productSaleAper] = @productSaleAper, [productSaleBper] = @productSaleBper, [productSaleCper] = @productSaleCper, [productSaleDper] = @productSaleDper, [productSaleArs] = @productSaleArs, [productSaleBrs] = @productSaleBrs, [productSaleCrs] = @productSaleCrs, [productSaleDrs] = @productSaleDrs, [productNote] = @productNote WHERE [productId] = @productId">
                        <DeleteParameters>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlProductAdd" runat="server"
                        DataSourceID="SqlDataProduct" DataTextField="productName" DataValueField="productId">
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDataProduct" ConnectionString='<%$ ConnectionStrings:dbGNEnterpriseConStr %>'
                        DeleteCommand="DELETE FROM [tblProduct] WHERE [productId] = @productId"
                        InsertCommand="INSERT INTO [tblProduct] ([productCompany], [productName], [productUnit], [productStock], [productPurchasePrice], [productMRP], [productSalesPrice], [productDiscount], [productHSN], [productMinimumStock], [productMaximumStock], [subtypeId], [description1], [description2], [description3], [description4], [description5], [productShortCode], [productStockType], [productReorder], [productDiscountRs], [productStoreLocation], [productIGST], [productCGST], [productSGST], [productMasterPacking], [productSaleAper], [productSaleBper], [productSaleCper], [productSaleDper], [productSaleArs], [productSaleBrs], [productSaleCrs], [productSaleDrs], [productNote]) VALUES (@productCompany, @productName, @productUnit, @productStock, @productPurchasePrice, @productMRP, @productSalesPrice, @productDiscount, @productHSN, @productMinimumStock, @productMaximumStock, @subtypeId, @description1, @description2, @description3, @description4, @description5, @productShortCode, @productStockType, @productReorder, @productDiscountRs, @productStoreLocation, @productIGST, @productCGST, @productSGST, @productMasterPacking, @productSaleAper, @productSaleBper, @productSaleCper, @productSaleDper, @productSaleArs, @productSaleBrs, @productSaleCrs, @productSaleDrs, @productNote)"
                        SelectCommand="SELECT * FROM [tblProduct]"
                        UpdateCommand="UPDATE [tblProduct] SET [productCompany] = @productCompany, [productName] = @productName, [productUnit] = @productUnit, [productStock] = @productStock, [productPurchasePrice] = @productPurchasePrice, [productMRP] = @productMRP, [productSalesPrice] = @productSalesPrice, [productDiscount] = @productDiscount, [productHSN] = @productHSN, [productMinimumStock] = @productMinimumStock, [productMaximumStock] = @productMaximumStock, [subtypeId] = @subtypeId, [description1] = @description1, [description2] = @description2, [description3] = @description3, [description4] = @description4, [description5] = @description5, [productShortCode] = @productShortCode, [productStockType] = @productStockType, [productReorder] = @productReorder, [productDiscountRs] = @productDiscountRs, [productStoreLocation] = @productStoreLocation, [productIGST] = @productIGST, [productCGST] = @productCGST, [productSGST] = @productSGST, [productMasterPacking] = @productMasterPacking, [productSaleAper] = @productSaleAper, [productSaleBper] = @productSaleBper, [productSaleCper] = @productSaleCper, [productSaleDper] = @productSaleDper, [productSaleArs] = @productSaleArs, [productSaleBrs] = @productSaleBrs, [productSaleCrs] = @productSaleCrs, [productSaleDrs] = @productSaleDrs, [productNote] = @productNote WHERE [productId] = @productId">
                        <DeleteParameters>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" SortExpression="tempSalesItemQuantity">
                <EditItemTemplate>
                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("tempSalesItemQuantity") %>' ></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="txtQuantityView" runat="server" Text='<%# Bind("tempSalesItemQuantity") %>'></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="footertxtQuantity" runat="server" Text='<%# Bind("tempSalesItemQuantity") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Price" SortExpression="tempSalesItemUnitPrice">
                <EditItemTemplate>
                    <asp:TextBox ID="txtUnitPriceEdit" runat="server" Text='<%# Bind("tempSalesItemUnitPrice") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="txtUnitPriceView" runat="server" Text='<%# Bind("tempSalesItemUnitPrice") %>'></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="footertxtUnitPrice" runat="server" Text='<%# Bind("tempSalesItemUnitPrice") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField Header  SortExpression="tempSalesItemTotal">
                <EditItemTemplate>
                    <asp:Label ID="llbTotalEdit" runat="server" Text="Label"></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="llbTotal" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="footertxtTotal" runat="server" Text='<%# Bind("tempSalesItemTotal") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Action" ShowHeader="False">
                <EditItemTemplate>
                     <asp:ImageButton ImageUrl ="~/Image/update.png" ID="updateIB" runat="server" CommandName ="Update" ToolTip ="Update" Width ="30px" Height="30px" />
                     <asp:ImageButton ImageUrl = "~/Image/cancle.png" ID="cancelIB" runat="server" CommandName ="Cancel" ToolTip ="Cancel" Width ="30px" Height="30px" />
                </EditItemTemplate>
                <ItemTemplate>
                     <asp:ImageButton ImageUrl ="~/Image/edit.png" ID="editIB" runat="server" CommandName ="Edit" ToolTip ="Edit" Width ="30px" Height="30px" />
                     <asp:ImageButton ImageUrl ="~/Image/delete.png" ID="deleteIB" runat="server" CommandName ="Delete" ToolTip ="Delete" Width ="30px" Height="30px"/>
                </ItemTemplate>     
                 <FooterTemplate>
                     <asp:ImageButton ImageUrl ="~/Image/add.png" OnClick ="linkBtnInsert_Click" ID="addIB" runat="server" CommandName ="Add New" ToolTip ="AddNew" Width ="30px" Height="30px" />
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
    <asp:SqlDataSource ID="SqlDataTemp" runat="server" ConnectionString="<%$ ConnectionStrings:dbGNEnterpriseConStr %>"
        DeleteCommand="DELETE FROM [tblTempSalesItem] WHERE [tempSalesItemId] = @tempSalesItemId"
        InsertCommand="INSERT INTO [tblTempSalesItem] ([tempSalesItemQuantity], [tempSalesItemTotal], [tempSalesItemProductId], [tempSalesItemUnitPrice]) VALUES (@tempSalesItemQuantity, @tempSalesItemTotal, @tempSalesItemProductId, @tempSalesItemUnitPrice)"
        SelectCommand="SELECT * FROM [tblTempSalesItem]"
        UpdateCommand="UPDATE [tblTempSalesItem] SET [tempSalesItemQuantity] = @tempSalesItemQuantity, [tempSalesItemTotal] = @tempSalesItemTotal, [tempSalesItemProductId] = @tempSalesItemProductId, [tempSalesItemUnitPrice] = @tempSalesItemUnitPrice WHERE [tempSalesItemId] = @tempSalesItemId">
        <DeleteParameters>
            <asp:Parameter Name="tempSalesItemId" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="tempSalesItemQuantity" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemTotal" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemProductId" Type="Int32" />
            <asp:Parameter Name="tempSalesItemUnitPrice" Type="Decimal" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="tempSalesItemQuantity" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemTotal" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemProductId" Type="Int32" />
            <asp:Parameter Name="tempSalesItemUnitPrice" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>--%><%--<asp:XmlDataSource ID="XmlDataItem" runat="server" DataFile="~/Data/Item.xml"
        TransformFile="~/Data/Item.xslt"></asp:XmlDataSource>--%>
    <%--    <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999"
        BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" DataKeyNames="tempSalesItemId" DataSourceID="SqlDataTemp"
        ForeColor="Black" HorizontalAlign="Center" ShowFooter="True" OnRowDataBound="gvSales_RowDataBound" OnSelectedIndexChanged="gvSales_SelectedIndexChanged1">
        <Columns>
            <asp:TemplateField HeaderText="SR.NO">
                <ItemTemplate>
                    <%# Container.DataItemIndex +1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="tempSalesItemId" HeaderText="tempSalesItemId" InsertVisible="False" ReadOnly="True" SortExpression="tempSalesItemId" Visible="False" />
            <asp:TemplateField HeaderText="Product" SortExpression="tempSalesItemProductId">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlProductEdit" runat="server">
                    </asp:DropDownList>
                    <%--<asp:SqlDataSource runat="server" ID="SqlDataProduct" ConnectionString='<%$ ConnectionStrings:dbGNEnterpriseConStr %>'
                        DeleteCommand="DELETE FROM [tblProduct] WHERE [productId] = @productId"
                        InsertCommand="INSERT INTO [tblProduct] ([productCompany], [productName], [productUnit], [productStock], [productPurchasePrice], [productMRP], [productSalesPrice], [productDiscount], [productHSN], [productMinimumStock], [productMaximumStock], [subtypeId], [description1], [description2], [description3], [description4], [description5], [productShortCode], [productStockType], [productReorder], [productDiscountRs], [productStoreLocation], [productIGST], [productCGST], [productSGST], [productMasterPacking], [productSaleAper], [productSaleBper], [productSaleCper], [productSaleDper], [productSaleArs], [productSaleBrs], [productSaleCrs], [productSaleDrs], [productNote]) VALUES (@productCompany, @productName, @productUnit, @productStock, @productPurchasePrice, @productMRP, @productSalesPrice, @productDiscount, @productHSN, @productMinimumStock, @productMaximumStock, @subtypeId, @description1, @description2, @description3, @description4, @description5, @productShortCode, @productStockType, @productReorder, @productDiscountRs, @productStoreLocation, @productIGST, @productCGST, @productSGST, @productMasterPacking, @productSaleAper, @productSaleBper, @productSaleCper, @productSaleDper, @productSaleArs, @productSaleBrs, @productSaleCrs, @productSaleDrs, @productNote)"
                        SelectCommand="SELECT * FROM [tblProduct]"
                        UpdateCommand="UPDATE [tblProduct] SET [productCompany] = @productCompany, [productName] = @productName, [productUnit] = @productUnit, [productStock] = @productStock, [productPurchasePrice] = @productPurchasePrice, [productMRP] = @productMRP, [productSalesPrice] = @productSalesPrice, [productDiscount] = @productDiscount, [productHSN] = @productHSN, [productMinimumStock] = @productMinimumStock, [productMaximumStock] = @productMaximumStock, [subtypeId] = @subtypeId, [description1] = @description1, [description2] = @description2, [description3] = @description3, [description4] = @description4, [description5] = @description5, [productShortCode] = @productShortCode, [productStockType] = @productStockType, [productReorder] = @productReorder, [productDiscountRs] = @productDiscountRs, [productStoreLocation] = @productStoreLocation, [productIGST] = @productIGST, [productCGST] = @productCGST, [productSGST] = @productSGST, [productMasterPacking] = @productMasterPacking, [productSaleAper] = @productSaleAper, [productSaleBper] = @productSaleBper, [productSaleCper] = @productSaleCper, [productSaleDper] = @productSaleDper, [productSaleArs] = @productSaleArs, [productSaleBrs] = @productSaleBrs, [productSaleCrs] = @productSaleCrs, [productSaleDrs] = @productSaleDrs, [productNote] = @productNote WHERE [productId] = @productId">
                        <DeleteParameters>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                  <%--  <asp:SqlDataSource runat="server" ID="SqlDataProduct" ConnectionString='<%$ ConnectionStrings:dbGNEnterpriseConStr %>'
                        DeleteCommand="DELETE FROM [tblProduct] WHERE [productId] = @productId"
                        InsertCommand="INSERT INTO [tblProduct] ([productCompany], [productName], [productUnit], [productStock], [productPurchasePrice], [productMRP], [productSalesPrice], [productDiscount], [productHSN], [productMinimumStock], [productMaximumStock], [subtypeId], [description1], [description2], [description3], [description4], [description5], [productShortCode], [productStockType], [productReorder], [productDiscountRs], [productStoreLocation], [productIGST], [productCGST], [productSGST], [productMasterPacking], [productSaleAper], [productSaleBper], [productSaleCper], [productSaleDper], [productSaleArs], [productSaleBrs], [productSaleCrs], [productSaleDrs], [productNote]) VALUES (@productCompany, @productName, @productUnit, @productStock, @productPurchasePrice, @productMRP, @productSalesPrice, @productDiscount, @productHSN, @productMinimumStock, @productMaximumStock, @subtypeId, @description1, @description2, @description3, @description4, @description5, @productShortCode, @productStockType, @productReorder, @productDiscountRs, @productStoreLocation, @productIGST, @productCGST, @productSGST, @productMasterPacking, @productSaleAper, @productSaleBper, @productSaleCper, @productSaleDper, @productSaleArs, @productSaleBrs, @productSaleCrs, @productSaleDrs, @productNote)"
                        SelectCommand="SELECT * FROM [tblProduct]"
                        UpdateCommand="UPDATE [tblProduct] SET [productCompany] = @productCompany, [productName] = @productName, [productUnit] = @productUnit, [productStock] = @productStock, [productPurchasePrice] = @productPurchasePrice, [productMRP] = @productMRP, [productSalesPrice] = @productSalesPrice, [productDiscount] = @productDiscount, [productHSN] = @productHSN, [productMinimumStock] = @productMinimumStock, [productMaximumStock] = @productMaximumStock, [subtypeId] = @subtypeId, [description1] = @description1, [description2] = @description2, [description3] = @description3, [description4] = @description4, [description5] = @description5, [productShortCode] = @productShortCode, [productStockType] = @productStockType, [productReorder] = @productReorder, [productDiscountRs] = @productDiscountRs, [productStoreLocation] = @productStoreLocation, [productIGST] = @productIGST, [productCGST] = @productCGST, [productSGST] = @productSGST, [productMasterPacking] = @productMasterPacking, [productSaleAper] = @productSaleAper, [productSaleBper] = @productSaleBper, [productSaleCper] = @productSaleCper, [productSaleDper] = @productSaleDper, [productSaleArs] = @productSaleArs, [productSaleBrs] = @productSaleBrs, [productSaleCrs] = @productSaleCrs, [productSaleDrs] = @productSaleDrs, [productNote] = @productNote WHERE [productId] = @productId">
                        <DeleteParameters>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlProductAdd" runat="server"
                        DataSourceID="SqlDataProduct" DataTextField="productName" DataValueField="productId">
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDataProduct" ConnectionString='<%$ ConnectionStrings:dbGNEnterpriseConStr %>'
                        DeleteCommand="DELETE FROM [tblProduct] WHERE [productId] = @productId"
                        InsertCommand="INSERT INTO [tblProduct] ([productCompany], [productName], [productUnit], [productStock], [productPurchasePrice], [productMRP], [productSalesPrice], [productDiscount], [productHSN], [productMinimumStock], [productMaximumStock], [subtypeId], [description1], [description2], [description3], [description4], [description5], [productShortCode], [productStockType], [productReorder], [productDiscountRs], [productStoreLocation], [productIGST], [productCGST], [productSGST], [productMasterPacking], [productSaleAper], [productSaleBper], [productSaleCper], [productSaleDper], [productSaleArs], [productSaleBrs], [productSaleCrs], [productSaleDrs], [productNote]) VALUES (@productCompany, @productName, @productUnit, @productStock, @productPurchasePrice, @productMRP, @productSalesPrice, @productDiscount, @productHSN, @productMinimumStock, @productMaximumStock, @subtypeId, @description1, @description2, @description3, @description4, @description5, @productShortCode, @productStockType, @productReorder, @productDiscountRs, @productStoreLocation, @productIGST, @productCGST, @productSGST, @productMasterPacking, @productSaleAper, @productSaleBper, @productSaleCper, @productSaleDper, @productSaleArs, @productSaleBrs, @productSaleCrs, @productSaleDrs, @productNote)"
                        SelectCommand="SELECT * FROM [tblProduct]"
                        UpdateCommand="UPDATE [tblProduct] SET [productCompany] = @productCompany, [productName] = @productName, [productUnit] = @productUnit, [productStock] = @productStock, [productPurchasePrice] = @productPurchasePrice, [productMRP] = @productMRP, [productSalesPrice] = @productSalesPrice, [productDiscount] = @productDiscount, [productHSN] = @productHSN, [productMinimumStock] = @productMinimumStock, [productMaximumStock] = @productMaximumStock, [subtypeId] = @subtypeId, [description1] = @description1, [description2] = @description2, [description3] = @description3, [description4] = @description4, [description5] = @description5, [productShortCode] = @productShortCode, [productStockType] = @productStockType, [productReorder] = @productReorder, [productDiscountRs] = @productDiscountRs, [productStoreLocation] = @productStoreLocation, [productIGST] = @productIGST, [productCGST] = @productCGST, [productSGST] = @productSGST, [productMasterPacking] = @productMasterPacking, [productSaleAper] = @productSaleAper, [productSaleBper] = @productSaleBper, [productSaleCper] = @productSaleCper, [productSaleDper] = @productSaleDper, [productSaleArs] = @productSaleArs, [productSaleBrs] = @productSaleBrs, [productSaleCrs] = @productSaleCrs, [productSaleDrs] = @productSaleDrs, [productNote] = @productNote WHERE [productId] = @productId">
                        <DeleteParameters>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="productCompany" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productName" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productUnit" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productPurchasePrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMRP" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSalesPrice" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscount" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productHSN" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMinimumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productMaximumStock" Type="String"></asp:Parameter>
                            <asp:Parameter Name="subtypeId" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="description1" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description2" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description3" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description4" Type="String"></asp:Parameter>
                            <asp:Parameter Name="description5" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productShortCode" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStockType" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productReorder" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productDiscountRs" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productStoreLocation" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productIGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productCGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSGST" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productMasterPacking" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productSaleAper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDper" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleArs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleBrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleCrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productSaleDrs" Type="Decimal"></asp:Parameter>
                            <asp:Parameter Name="productNote" Type="String"></asp:Parameter>
                            <asp:Parameter Name="productId" Type="Int32"></asp:Parameter>
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" SortExpression="tempSalesItemQuantity">
                <EditItemTemplate>
                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("tempSalesItemQuantity") %>' ></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="txtQuantityView" runat="server" Text='<%# Bind("tempSalesItemQuantity") %>'></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="footertxtQuantity" runat="server" Text='<%# Bind("tempSalesItemQuantity") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Price" SortExpression="tempSalesItemUnitPrice">
                <EditItemTemplate>
                    <asp:TextBox ID="txtUnitPriceEdit" runat="server" Text='<%# Bind("tempSalesItemUnitPrice") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="txtUnitPriceView" runat="server" Text='<%# Bind("tempSalesItemUnitPrice") %>'></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="footertxtUnitPrice" runat="server" Text='<%# Bind("tempSalesItemUnitPrice") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField Header  SortExpression="tempSalesItemTotal">
                <EditItemTemplate>
                    <asp:Label ID="llbTotalEdit" runat="server" Text="Label"></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="llbTotal" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="footertxtTotal" runat="server" Text='<%# Bind("tempSalesItemTotal") %>'></asp:TextBox>
                </FooterTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Action" ShowHeader="False">
                <EditItemTemplate>
                     <asp:ImageButton ImageUrl ="~/Image/update.png" ID="updateIB" runat="server" CommandName ="Update" ToolTip ="Update" Width ="30px" Height="30px" />
                     <asp:ImageButton ImageUrl = "~/Image/cancle.png" ID="cancelIB" runat="server" CommandName ="Cancel" ToolTip ="Cancel" Width ="30px" Height="30px" />
                </EditItemTemplate>
                <ItemTemplate>
                     <asp:ImageButton ImageUrl ="~/Image/edit.png" ID="editIB" runat="server" CommandName ="Edit" ToolTip ="Edit" Width ="30px" Height="30px" />
                     <asp:ImageButton ImageUrl ="~/Image/delete.png" ID="deleteIB" runat="server" CommandName ="Delete" ToolTip ="Delete" Width ="30px" Height="30px"/>
                </ItemTemplate>     
                 <FooterTemplate>
                     <asp:ImageButton ImageUrl ="~/Image/add.png" OnClick ="linkBtnInsert_Click" ID="addIB" runat="server" CommandName ="Add New" ToolTip ="AddNew" Width ="30px" Height="30px" />
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
    <asp:SqlDataSource ID="SqlDataTemp" runat="server" ConnectionString="<%$ ConnectionStrings:dbGNEnterpriseConStr %>"
        DeleteCommand="DELETE FROM [tblTempSalesItem] WHERE [tempSalesItemId] = @tempSalesItemId"
        InsertCommand="INSERT INTO [tblTempSalesItem] ([tempSalesItemQuantity], [tempSalesItemTotal], [tempSalesItemProductId], [tempSalesItemUnitPrice]) VALUES (@tempSalesItemQuantity, @tempSalesItemTotal, @tempSalesItemProductId, @tempSalesItemUnitPrice)"
        SelectCommand="SELECT * FROM [tblTempSalesItem]"
        UpdateCommand="UPDATE [tblTempSalesItem] SET [tempSalesItemQuantity] = @tempSalesItemQuantity, [tempSalesItemTotal] = @tempSalesItemTotal, [tempSalesItemProductId] = @tempSalesItemProductId, [tempSalesItemUnitPrice] = @tempSalesItemUnitPrice WHERE [tempSalesItemId] = @tempSalesItemId">
        <DeleteParameters>
            <asp:Parameter Name="tempSalesItemId" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="tempSalesItemQuantity" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemTotal" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemProductId" Type="Int32" />
            <asp:Parameter Name="tempSalesItemUnitPrice" Type="Decimal" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="tempSalesItemQuantity" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemTotal" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemProductId" Type="Int32" />
            <asp:Parameter Name="tempSalesItemUnitPrice" Type="Decimal" />
            <asp:Parameter Name="tempSalesItemId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>--%>    <%--<asp:XmlDataSource ID="XmlDataItem" runat="server" DataFile="~/Data/Item.xml"
        TransformFile="~/Data/Item.xslt"></asp:XmlDataSource>--%>
    <table class="auto-style23">
        <tr>
            <td class="auto-style22">
                <asp:Label ID="lblSR1" runat="server" Text="Payment Mode"></asp:Label>
            </td>
            <td class="auto-style21">
                <asp:TextBox ID="txtPaymentMode" runat="server" Width="177px"></asp:TextBox>
            </td>
            <td class="auto-style21" colspan="3" rowspan="3">
                <asp:TextBox ID="txtDescription1" runat="server" Width="510px" placeholder="Note" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </td>
            <td class="auto-style43">&nbsp;</td>
            <td class="auto-style42"></td>
            <td class="auto-style21"></td>
            <td class="auto-style50"></td>
            <td class="auto-style21"></td>
            <td class="auto-style62"></td>
            <td class="auto-style46"></td>
            <td class="auto-style57"></td>
            <td class="auto-style61">&nbsp;</td>
            <td class="auto-style55">&nbsp;</td>
            <td class="auto-style45">&nbsp;</td>
            <td class="auto-style60">
                <asp:Label ID="lblGrandTotal0" runat="server" Text="Total"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text=" "></asp:Label>
            </td>
            <td class="auto-style45">
                <asp:Label ID="lblTotalValue" runat="server" Text="0"></asp:Label>
            </td>

        </tr>
        <tr>
            <td class="auto-style64">
                <asp:Label ID="lblSR2" runat="server" Text="SRNO 2" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSRNo2" runat="server" Width="177px" Visible="false"></asp:TextBox>
            </td>
            <td class="auto-style21" colspan="3">&nbsp;</td>
            <td>&nbsp;</td>

            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style51">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style63">&nbsp;</td>
            <td class="auto-style59">&nbsp;</td>
            <td class="auto-style58">
                <asp:DropDownList ID="ddlGST" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGST_SelectedIndexChanged">
                    <asp:ListItem Value="0">0% GST</asp:ListItem>
                    <asp:ListItem Value="12">12% GST</asp:ListItem>
                    <asp:ListItem Value="18" Selected="True">18% GST</asp:ListItem>
                    <asp:ListItem Value="28">28% GST</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style60">
                <asp:Label ID="lblSGST" runat="server" Text="SGST"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSGSTPer" runat="server" Text=" "></asp:Label>
            </td>
            <td class="auto-style45">
                <asp:Label ID="lblSGSTValue" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style64">
                <asp:Label ID="Label4" runat="server" Text="SRNO 3" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSRNo3" runat="server" Width="177px" Visible="false"></asp:TextBox>
            </td>
            <td class="auto-style21" colspan="3">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style51">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style63">&nbsp;</td>
            <td class="auto-style59">&nbsp;</td>
            <td class="auto-style58">&nbsp;</td>
            <td class="auto-style60">
                <asp:Label ID="lblCGST" runat="server" Text="CGST"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCGSTPer" runat="server" Text=" "></asp:Label>
            </td>
            <td class="auto-style45">
                <asp:Label ID="lblCGSTValue" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style64">&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtDescription2" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtDescription3" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style51">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style59">&nbsp;</td>
            <td class="auto-style63">&nbsp;</td>
            <td class="auto-style60">&nbsp;</td>
            <td class="auto-style45">&nbsp;</td>
            <td class="auto-style60">&nbsp;</td>
            <td>
                <asp:Label ID="Label2" runat="server" Text=" "></asp:Label>
            </td>
            <td class="auto-style45">&nbsp;</td>
            <td>
                <asp:Label ID="lblSGST0" runat="server" Text="Total GST"></asp:Label>
            </td>
            <td class="auto-style45">&nbsp;</td>
            <td class="auto-style45">
                <asp:Label ID="lblTotalGST" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style64">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style51">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style59">&nbsp;</td>
            <td class="auto-style63">&nbsp;</td>
            <td class="auto-style60">&nbsp;</td>
            <td class="auto-style45">&nbsp;</td>
            <td class="auto-style60">&nbsp;</td>
            <td>
                <asp:Label ID="Label6" runat="server" Text=" "></asp:Label>
            </td>
            <td class="auto-style45">&nbsp;</td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="R/O"></asp:Label>
            </td>
            <td class="auto-style45">&nbsp;</td>
            <td class="auto-style45">

                <asp:Label ID="lblRoundOffValue" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style64">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style49"></td>
            <td class="auto-style49"></td>
            <td class="auto-style49"></td>
            <td class="auto-style49"></td>

            <td class="auto-style49"></td>
            <td class="auto-style49" colspan="3">&nbsp;</td>
            <td class="auto-style63">&nbsp;</td>
            <td class="auto-style60">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style60">&nbsp;</td>
            <td>
                <asp:Label ID="Label9" runat="server" Text=" "></asp:Label>
            </td>
            <td class="auto-style45">&nbsp;</td>
            <td>
                <asp:Label ID="lblGrandTotalValueWithGSTAfterRoundOff" runat="server" Text="Grand Total"></asp:Label>
            </td>
            <td class="auto-style45">&nbsp;</td>
            <td class="auto-style45">
                <asp:Label ID="lblGrandTotalValueWithGSTAfterRoundOffValue" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblGrandTotalValueWithGSTAfterRoundOffInWords" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>

</asp:Content>
