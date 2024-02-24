<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptcheckpendingsalebill.aspx.cs" Inherits="Sugar_Report_rptcheckpendingsalebill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function DoOPen(DO) {
            var Action = 1;
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action, "_blank");
        }
    </script>


    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
        </div>
        <div align="center">
            <table width="1000px" align="center" style="font-size: x-large; font-weight: bold; border-bottom: 1px solid; border-top: 1px solid; border-right: 1px solid; border-left: 1px solid; height: 30px;">
                <tr>
                    <td style="width: 50px;">DO NO
                    </td>
                    <td style="width: 50px;">Doc_Date
                    </td>
                    <td style="width: 50px;">Do_Date
                    </td>
                    <td style="width: 90px;">Quantal
                    </td>
                    <td style="width: 100px;">Sale Bill To</td>

                    <td style="width: 100px;">MillName</td>


                </tr>
            </table>
            <asp:DataList ID="dtlAcList" runat="server">
                <ItemTemplate>
                    <table width="1000px" align="center" style="font-size: large; border-bottom: 1px solid; border-top: 1px solid; border-right: 1px solid; border-left: 1px solid; height: 25px;">
                        <tr>
                            <td style="width: 50px;">
                                <asp:LinkButton runat="server" ID="lbkTenderNo" Text='<%#Eval("DO_NO") %>' OnClick="lbkTenderNo_Click"></asp:LinkButton>

                                <%--<asp:Label ID="lblAcCode" runat="server" Text='<%#Eval("DO_NO") %>'></asp:Label>--%>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblAcName" runat="server" Text='<%#Eval("doc_Date") %>'></asp:Label>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Do_Date") %>'></asp:Label>
                            </td>
                            <td style="width: 90px;" align="center">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("quantal") %>'></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("salebillname") %>'></asp:Label></td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("millname") %>'></asp:Label></td>


                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</body>
</html>
