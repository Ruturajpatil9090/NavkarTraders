<%@ Page Title="Multiple Bill Printing" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeMultipleBillPrinting.aspx.cs" Inherits="Sugar_BussinessRelated_pgeMultipleBillPrinting" %>

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
     <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

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
                <asp:Button Text="Select DO" runat="server" Height="24px" Width="100px" OnClick="btnDOprint_Click" ID="btnDOprint" />

            </td>
        </tr>

        <tr>
            <td>
                <table>

                    <tr>
                        <td>
                            <asp:GridView ID="grddetailHeat" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                                Width="40%" HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both"
                                OnRowCommand="grddetailHeat_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grddetailHeat_RowDataBound"
                                Style="table-layout: fixed;">
                                <Columns>
                                    <asp:BoundField DataField="doc_no" HeaderText="DO No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="voucherbyname" HeaderText="Party" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="quantal" HeaderText="quantal" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="sale_rate" HeaderText="sale_rate" ItemStyle-Width="60px" />

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
                    </tr>

                </table>
                &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
              
            </td>
            <td></td>


        </tr>
        <tr>
            <td>
                <asp:Button Text=" print DO" runat="server" ID="btnprintDO" OnClick="btnprintDO_Click" />
                &emsp;&emsp;&emsp; &emsp;              
               
            </td>

        </tr>
    </table>

</asp:Content>

