<%@ Page Title="freightRegister Report" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeFreightRegister_Report.aspx.cs" Inherits="Report_pgeFreightRegister_Report_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function FreightRegister(accode, FromDt, ToDt) {
            var tn;
            window.open('rptfreightRegister.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }


        function FreightRegisterDetail(accode, FromDt, ToDt) {
            var tn;
            window.open('rptfreightRegisterDetail.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function TransportpaidFreightRegister(accode, FromDt, ToDt) {
            var tn;
            window.open('rptTransportpaidFreightRegister.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function TransportpaidFreightdetailRegister(accode, FromDt, ToDt) {
            var tn;
            window.open('rptTransportpaidFreightdetailRegister.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function TransportAccountRegister(accode, FromDt, ToDt) {
            var tn;
            window.open('rptTransportAcLedgernew.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function TransportwiseDo(accode, FromDt, ToDt) {
            var tn;
            window.open('rptTransportWiseDoDetail.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }


    </script>
    <script type="text/javascript" language="javascript">
        debugger;
        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();
                debugger;
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtAcCode") {
                    document.getElementById("<%=txtAcCode.ClientID %>").focus();
                } 
                document.getElementById("<%=txtSearchText.ClientID %>").value = ""; 
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
    <script type="text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }

        function ValidateDate(txt, keyCode) {
            if (keyCode == 16)
                isShift = false;
            var val = txt.value;

            document.getElementById(lblmesg);
            if (val.length == 10) {
                var splits = val.split("/");
                var dt = new Date(splits[1] + "/" + splits[0] + "/" + splits[2]);

                //Validation for Dates
                if (dt.getDate() == splits[0] && dt.getMonth() + 1 == splits[1]
            && dt.getFullYear() == splits[2]) {
                    lblmesg.style.color = "green";
                    lblmesg.innerHTML = "Valid Date";
                }
                else {
                    lblmesg.style.color = "red";
                    lblmesg.innerHTML = "Invalid Date";
                    return;
                }

                //Range Validation
                if (txt.id.indexOf("txtRange") != -1)
                    RangeValidation(dt);

                //BirthDate Validation
                if (txt.id.indexOf("txtBirthDate") != -1)
                    BirthDateValidation(dt)
            }
            else if (val.length < 10) {
                lblmesg.style.color = "blue";
                lblmesg.innerHTML =
         "Required dd/mm/yy format. Slashes will come up automatically.";
            }
        }

    </script>
    <script type="text/javascript" language="javascript">

        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        function SelectSibling(e) {
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;
            if (KeyCode == 40) {
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            }
            else if (KeyCode == 38) {
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            }
            else if (KeyCode == 13) {
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                var grid = document.getElementById("<%= grdPopup.ClientID %>");
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;


                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
                if (hdnfClosePopupValue == "txtAcCode") {
                    document.getElementById("<%=txtAcCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblAcCodeName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;

                }
            }
}

        function SelectRow(CurrentRow, RowIndex) {
            UpperBound = parseInt('<%= this.grdPopup.Rows.Count %>') - 1;
            LowerBound = 0;

            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)

                if (SelectedRow != null) {
                    SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                    SelectedRow.style.color = SelectedRow.originalForeColor;
                }
            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = '#DCFC5C';
                CurrentRow.style.color = 'Black';
            }
            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ac_name(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnAcCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAcCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAcCode.ClientID %>").val(unit);
                __doPostBack("txtAcCode", "TextChanged");

            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   UTR Report   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:HiddenField ID="hdnfClosePopup" runat="server" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
    <asp:HiddenField ID="hdnfpopup" runat="server" />
    <center>
        <table width="100%" align="left" cellspacing="5">
            <tr>
                <td align="right" style="width: 40%;">
                    <b>AC Code</b>
                </td>
                <td align="left" colspan="5" style="width: 60%;">
                    <asp:TextBox ID="txtAcCode" runat="server" Width="80px" CssClass="txt" AutoPostBack="false"
                        OnTextChanged="txtAcCode_TextChanged" Height="24px" onkeyDown="ac_name(event);"
                        TabIndex="2"></asp:TextBox>
                    <asp:Button ID="btnAcCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnAcCode_Click"
                        Height="24px" Width="20px" />
                    <asp:Label ID="lblAcCodeName" runat="server" CssClass="lblName"></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td align="right" style="width: 40%;">
                    <b>BankWise</b>
                </td>
                <td align="left" colspan="5" style="width: 60%;">
                    <asp:TextBox ID="txtBankCode" runat="server" Width="80px" CssClass="txt" AutoPostBack="True"
                        Height="24px" OnTextChanged="txtBankCode_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnBankCode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                        Width="20px" OnClick="btnBankCode_Click" />
                    <asp:Label ID="lblBankName" runat="server" CssClass="lblName"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td align="right">
                    <b>From:</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                        TabIndex="3" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode);" onkeydown="DateFormat(this,event.keyCode);"> </asp:TextBox>
                    <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                        Width="25px" Height="15px" />
                    <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDate"
                        PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>To:</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                        TabIndex="4" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"> </asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                        Height="15px" />
                    <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                        PopupButtonID="Image1" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnDetailRptNew" runat="server" Text="Freight Balance Report" CssClass="btnHelp"
                        Width="190px" Height="24px" TabIndex="6" OnClick="btnDetailRptNew_Click" />
                </td>
                <td align="right">
                    <asp:Button ID="btnUTRNODOWise" runat="server" Text="FreightDetail Balance Report "
                        CssClass="btnHelp" Width="210px" Height="24px" TabIndex="7" OnClick="btnUTRNODOWise_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btntreansportpaidFreight" runat="server" Text=" Transport Paid Freight Report "
                        CssClass="btnHelp" Width="210px" Height="24px" TabIndex="7" OnClick="btntreansportpaidFreight_Click" />
                </td>
                <td align="right">
                    <asp:Button ID="btntransportpaidfrightdetail" runat="server" Text=" Transport Paid Freight detail Report "
                        CssClass="btnHelp" Width="230px" Height="24px" TabIndex="7" OnClick="btntransportpaidfrightdetail_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnTransportFreightaccount" runat="server" Text=" Transport Freight Account "
                        CssClass="btnHelp" Width="210px" Height="24px" TabIndex="7" OnClick="btnTransportFreightaccount_Click" />
                </td>
                 <td align="right">
                    <asp:Button ID="btnTransportwiseDo" runat="server" Text=" Transport Wise Do Detail "
                        CssClass="btnHelp" Width="210px" Height="24px" TabIndex="7" OnClick="btnTransportwiseDo_Click" />
                </td>
            </tr>
        </table>
        <table>
        </table>
    </center>
        <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
        align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
        Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px;
        min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center;
        left: 10%; top: 10%;">
        <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
            Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
            ToolTip="Close" />
        <table width="95%">
            <tr>
                <td align="center" style="background-color: #F5B540; width: 100%;">
                    <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                        Font-Bold="true" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Search Text:
                    <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                        Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                        CssClass="btnSubmit" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Panel ID="pnlScroll" runat="server" Width="680px" ScrollBars="Both" Direction="LeftToRight" BackColor="#FFFFE4" style="z-index:5000 ;  float:right; max-height:380px; height:380px;">--%>
                    <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                        Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                        <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                            EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White"
                            PageSize="20" AllowPaging="true" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                            Style="table-layout: fixed;" OnPageIndexChanging="grdPopup_PageIndexChanging">
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                            <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                            <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                            <PagerSettings Position="TopAndBottom" />
                        </asp:GridView>
                    </asp:Panel>
                    <%--</asp:Panel>--%>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
