<%@ Page Title="Multiple SaleBill" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeMultiple_SaleBill.aspx.cs" Inherits="Sugar_Report_pgeMultiple_SaleBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function PSauda(do_no) {
            window.open('../Report/rptDeliveryOrderForGST.aspx?do_no=' + do_no);    //R=Redirected  O=Original

        }
        function SB(dono) {

            window.open('../Report/pgeSaleBill_Print.aspx?doc_no=' + dono);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td></td>
        </tr>
    </table>
    <br />
    <table width="80%">
        <tr>

            <td>From Date:
                <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDt"
                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
                &emsp;&emsp;&emsp;
                     To Date:
                <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                    Height="15px" />
                <ajax1:CalendarExtender ID="CalendarExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
                &nbsp;
            </td>

        </tr>
        <tr>
            <td>

                <asp:Button Text="Select SaleBill" runat="server" Height="24px" Width="100px" OnClick="btnSalePrint_Click" ID="btnSalePrint" />
            </td>
        </tr>

        <tr>
            <td>

                <asp:GridView ID="grdprintSalebill" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                    Width="40%" HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both"
                    CellPadding="5" CellSpacing="5" OnRowDataBound="grdprintSalebill_RowDataBound"
                    Style="table-layout: fixed;">
                    <Columns>
                        <asp:BoundField DataField="doc_no" HeaderText="Sale BillNO" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="billtoname" HeaderText=" SaleParty" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="NETQNTL" HeaderText="quantal" ItemStyle-Width="50px" />
                        <asp:BoundField DataField="salerate" HeaderText="sale_rate" ItemStyle-Width="60px" />
                        <asp:BoundField DataField="saleid" HeaderText="saleid" ItemStyle-Width="60px" />
                        <asp:TemplateField HeaderText="Strength" ItemStyle-Width="20">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkcheck" />
                                <%--<asp:TextBox ID="txtStrength" runat="server" Text='<%# Eval("Strength") %>' Height="20px"
                                    TabIndex="19" onKeyDown="FocusTOT_Days(event,this);" OnTextChanged="txtStrength_TextChanged"
                                    Width="150px" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                    <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                </asp:GridView>


            </td>
            <td></td>


        </tr>
        <tr>
            <td>&emsp;&emsp;&emsp; &emsp;              
                <asp:Button Text=" print SaleBill" runat="server" ID="btnPrintSale_Bill" OnClick="btnPrintSale_Bill_Click" />
            </td>

        </tr>
    </table>

</asp:Content>

