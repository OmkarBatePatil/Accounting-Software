<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GNEnterprise.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 47%;
            margin:auto;
            border:5px solid white;
        }
        .auto-style2 {
            height: 47px;
            width: 108px;
        }.body{
             background-color: antiquewhite;
         }
        .auto-style5 {
            height: 62px;
            width: 108px;
        }
        .auto-style8 {
            height: 47px;
            width: 186px;
        }
        .auto-style10 {
            height: 62px;
            width: 186px;
        }
        .auto-style11 {
            height: 52px;
            width: 108px;
        }
        .auto-style12 {
            height: 52px;
            width: 186px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblUsername" runat="server" Text="UserName - "></asp:Label>
                    </td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtUsername" runat="server" Width="213px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">
                        <asp:Label ID="lblPassword" runat="server" Text="Password - "></asp:Label>
                    </td>
                    <td class="auto-style12">
                        <asp:TextBox ID="txtPassword" runat="server" Width="213px" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style10">
                        <asp:Button ID="btnSumbit" runat="server" Text="SUBMIT" Width="80px" OnClick="btnSumbit_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style10">
                        <asp:Label ID="lblErrorMessage" runat="server" Text="Incorrect Password" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            
        </div>
    </form>
</body>
</html>
