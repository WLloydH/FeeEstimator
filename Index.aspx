<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Index.aspx.vb" Inherits="FeeEstimimator.GovDeptsPlanningIndex" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fee Estimator</title>
    <link rel="shortcut icon" type="image/ico" href="favicon.ico">
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
    <style>
        .cbTextIndented {
            font-family :"Helvetica Neue", Helvetica, Arial, sans-serif;
            font-style: normal;
            font-weight: 400;
            font-color: #333333;

        }
        .cbTextIndented input {
            margin-right: 5px;
        }
    </style>
</head>
<body style="background-color: #e9f6fb">
   <img src="ClearwaterLogo.gif" />
   <h1 align="center" style="color: #3785c1">Fee Estimator</h1>
    <form id="form1" runat="server">
         <p>
            &nbsp;
        </p>
        <p>
            &nbsp;
        </p>
        <table width="100%" cellspacing="0" cellpadding="0">
            <td align="center" width="50%">
                <table width="55%" cellspacing="0" cellpadding="0">
                     <tr>
                        <td align="center" colspan="3">
                            <h3 style="text-align: center;">
                                <asp:Label ID="lblMessage" runat="server" CssClass="Alert" Font-Bold="True" Font-Size="Small" ForeColor="#3785c1"></asp:Label>&nbsp;</h3>
                        </td>
                    </tr>
                     <tr>
                        <td align="left">Type Of Permit</td>
                        <td colspan="2" align="left">
                            <asp:DropDownList ID="ddlPermitType" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                       
                        <td align="left">Type Of Property:</td>
                        <td align="left">
                            <asp:RadioButton ID="Residential" runat="server" GroupName="ConstructionType" />
                            <span style="margin-right: 10px;">Residential</span>
                            <asp:RadioButton ID="Commercial" runat="server" GroupName="ConstructionType" />
                            Commercial
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left">Value Of Job:</td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtValue" runat="server" Width="15em" Height="1.2em"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left">Trades Included:</td>
                        <td align="left" colspan="2">
                            <asp:CheckBox ID="ckbBuilding" CssClass="cbTextIndented" Text="Building" runat="server" />
                        </td>
                     </tr>
                    <tr>
                        <td align="left"></td>
                        <td align="left" colspan="2">
                            <asp:CheckBox ID="ckbElectric" CssClass="cbTextIndented" Text="Electric" runat="server" />
                        </td>
                     </tr>
                    <tr>
                        <td align="left"></td>
                        <td align="left" colspan="2">
                            <asp:CheckBox ID="ckbRoof" CssClass="cbTextIndented" Text="Roof" runat="server" /></td>
                    </tr>
                      <tr>
                        <td align="left"></td>
                        <td align="left" colspan="2">
                            <asp:CheckBox ID="ckbMechanic" CssClass="cbTextIndented" Text="Mechanic" runat="server" /></td>
                    </tr>
                     <tr>
                        <td align="left"></td>
                        <td align="left" colspan="2">
                            <asp:CheckBox ID="ckbPlumbing" CssClass="cbTextIndented" Text="Plumbing" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="left"></td>
                        <td align="left" colspan="2">
                            <asp:CheckBox ID="ckbGas" CssClass="cbTextIndented" Text="Gas" runat="server" /></td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                <table style="margin: 0 auto; width: 100%;">
                    <tr>
                        <td style="height: 10px; text-align: center; width: 100%">
                            <asp:Panel ID="pnlResults" runat="server" BackColor="LightGray" Width="200" Height="165" BorderColor="black" BorderWidth="2">
                                <br />
                                <asp:Label ID="lblEstimate" CssClass="Alert" runat="server" Font-Bold="True" Font-Size="Small" ></asp:Label>
                                <hr />
                                <asp:Label ID="lblPlansFee" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="black"></asp:Label>
                                <br />
                                <asp:Label ID="lblPermitFee" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="black"></asp:Label>
                                <br />
                                <asp:Label ID="lblTrades" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="black"></asp:Label>
                                <br />
                                <asp:Label ID="lblStateFee" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="black"></asp:Label>
                                <hr />
                                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="black"></asp:Label>
                                <br />
                                <br />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="1" style="height: 10px; text-align: center;">
                            <asp:Button ID="txtSubmit" runat="server" Text="Submit" Height="1.7em" Width="6em" />
                            <asp:Button ID="txtReset" runat="server" Text="Reset" Height="1.7em" Width="6em" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table width="75%" cellspacing="0" cellpadding="0">

                    <tr>
                        <td align="center">Click <a href="http://www.myclearwater.com/home/showdocument?id=2261">here </a>to view the schedule of fees, rates and charges.
	               
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblExplainState" CssClass="Alert" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#3785c1"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDisclaimer" CssClass="Alert" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#3785c1"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </table>

    </form>
<br />
<footer class="panel-footer text-center" style="font-size: .75em; font-weight: bolder; background-color: #cfeffd">
    Developed by the City of Clearwater Department of Information Technology
    <br />
    <a href="#">
        <span id="Copyright" class="glyphicon glyphicon-copyright-mark"></span>
    </a>
</footer>
</body>
</html>
<script type="text/javascript" language="javascript" src="//code.jquery.com/jquery-1.12.3.min.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
        var d = new Date();
        $('#Copyright').html('Copyright '.concat(d.getFullYear()).concat(' all rights reserved'));
    });
</script>
