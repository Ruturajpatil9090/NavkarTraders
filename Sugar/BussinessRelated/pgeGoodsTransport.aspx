<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGoodsTransport.aspx.cs" Inherits="Sugar_BussinessRelated_pgeGoodsTransport" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
             document.getElementById("<%=txtSearchText.ClientID %>").value = "";
             document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
         }

     });
    </script>
    <script type="text/javascript">
        function DO(Action, DO) {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action);
        }
        function Transfer() {
            debugger;
            $("#loader").show();
            var Doc_No = document.getElementById("<%= txtdo_no.ClientID %>").value;
                 var Autoid = document.getElementById("<%= hdnflvid.ClientID %>").value;
                 Do_ID = document.getElementById("<%= hdnfDoid.ClientID %>").value;

                 var Branch_Code = '<%= Session["Branch_Code"] %>';
                 var Company_Code = '<%= Session["Company_Code"] %>';
                 var Year_Code = '<%= Session["year"] %>';

                 var XML = "<ROOT><goodstransport Doc_No='" + Doc_No + "' Do_ID='" + Do_ID + "' Company_Code='" + Company_Code + "' " +
                     "Year_Code='" + Year_Code + "'></goodstransport></ROOT>";
                 var spname = "SP_Goodstransport";
                 var status = "Transfer";
                 ProcessXML(XML, status, spname);
             }

    </script>

    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        function SelectSibling(e) {
            debugger;
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



              pageCount = parseInt(pageCount);
              if (pageCount > 1) {
                  SelectedRowIndex = SelectedRowIndex + 1;
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
    <script type="text/javascript">


        function Cancle() {

            window.open('../BussinessRelated/pgeGoodsTransportUtility.aspx', '_self');
        }
    </script>
    <script type="text/javascript">
        function Validate() {
            debugger;
            $("#loader").show();
            // Validation

            var count = 0;
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                if (gridView == null) {
                    alert('Please Enter Sale Details!');
                    $("#loader").hide();
                    return false;
                }
                var grdrow = gridView.getElementsByTagName("tr");
                var ro = (grdrow.length - 1);
                if (ro == 0) {
                    alert('Please Enter Sale Details!');
                    $("#loader").hide();
                    return false;
                }
                if (ro >= 1) {
                    for (var i = 1; i < grdrow.length; i++) {
                        var action = gridView.rows[i].cells[6].innerHTML;
                        if (gridView.rows[i].cells[6].innerHTML == "D") {
                            count = count + 1;
                        }
                    }
                    if (ro == count) {
                        alert('Minimum One Sale Details is compulsory!');
                        $("#loader").hide();
                        return false;
                    }
                }

                return true;
            }
            function pagevalidation() {
                debugger;
                try {
                    $("#loader").show();
                    var Doc_No = 0, Do_ID = 0;
                    var XML = "<ROOT>";
                    var spname = "SP_Goodstransport";

                    var status = document.getElementById("<%= btnUpdate.ClientID %>").value;
                    if (status == "Update") {
                        Doc_No = document.getElementById("<%= hdnfDodoc.ClientID %>").value;
                        Do_ID = document.getElementById("<%= hdnfDoid.ClientID %>").value;
                    }



                    var Company_Code = '<%= Session["Company_Code"] %>';
                    var Year_Code = '<%= Session["year"] %>';
                    var Branch_Code = '<%= Session["Branch_Code"] %>';
                    var USER = '<%= Session["user"] %>';

                    var d = $("#<%=txtreached_date.ClientID %>").val();
                    var reached_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                    var drp = document.getElementById('<%=drpreached.ClientID %>');
                    var Reached = drp.options[drp.selectedIndex].value;
                    var GT_Remark = $("#<%=txtgtnarration.ClientID %>").val();

                    debugger;
                    var Detail_Insert = ""; Detail_Update = ""; Detail_Delete = "";
                    var Detail_Value = "";

                    var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                    var grdrow = gridView.getElementsByTagName("tr");

                    debugger;

                    Doc_No = "Doc_No='" + Doc_No + "'";


                    var GoodsID = GoodsID;
                    for (var i = 1; i < grdrow.length; i++) {
                        var ID = gridView.rows[i].cells[2].innerHTML;
                        var d = gridView.rows[i].cells[3].innerHTML;
                        var Run_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                        var Station = gridView.rows[i].cells[4].innerHTML;

                        if (gridView.rows[i].cells[6].innerHTML != "D") {
                            var GoodsID = gridView.rows[i].cells[5].innerHTML;
                            XML = XML + "<goodstransport " + Doc_No + " Do_ID='" + Do_ID + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " + " DetailID='" + ID + "' Run_Date='" + Run_Date + "' Station='" + Station + "' GoodsID='" + GoodsID + "' vehicle_reached='" + Reached + "' reached_date='" + reached_date + "' GT_Remark='" + GT_Remark + "'/>"; GT_Remark
                            GoodsID = parseInt(GoodsID) + 1;
                        }
                    }


                    XML = XML + "</ROOT>";
                    ProcessXML(XML, status, spname);
                }
                catch (exx) {
                    $("#loader").hide();
                    alert(exx)
                }
            }

            function ProcessXML(XML, status, spname) {
                debugger;

                $.ajax({
                    type: "POST",
                    url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                    data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response.d);
                        $("#loader").hide();
                    },
                    error: function (response) {
                        alert(response.d);
                        $("#loader").hide();
                    }
                });


                function OnSuccess(response) {
                    debugger;
                    $("#loader").hide();
                    if (status != "Delete") {
                        if (response.d.length > 0) {
                            var word = response.d;
                            var len = word.length;
                            var pos = word.indexOf(",");
                            var id = word.slice(0, pos);
                            var doc = word.slice(pos + 1, len);
                            if (status == "Update") {
                                alert('Sucessfully Updated Record !!! Doc_no=' + doc)
                            }

                            window.open('../BussinessRelated/pgeGoodsTransport.aspx?DO=' + id + '&Action=1', "_self");

                        }
                    }
                    else {
                        var num = parseInt(response.d);

                        if (isNaN(num)) {
                            alert(response.d)

                        }
                        else {
                            window.open('../BussinessRelated/pgeGoodsTransportUtility.aspx', "_self");
                        }
                    }

                }

            }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Goods Transport " Font-Names="verdana" ForeColor="White"
                Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
        <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnflvid" runat="server" />
            <asp:HiddenField ID="hdnfDoid" runat="server" />
            <asp:HiddenField ID="hdnfDodoc" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />

            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" Width="80px"
                            Height="25px" OnClick="btnUpdate_Click" TabIndex="19" />

                        &nbsp;
                        
                         <asp:Button ID="btnCancle" runat="server" Text="cancle" CssClass="btnHelp" Width="80px"
                             Height="25px" TabIndex="20" OnClientClick="Cancle();" />
                    </td>
                </tr>
            </table>

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 80%;" align="left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="right" style="width: 10%;"><asp:LinkButton runat="server" ID="lnkDo" Text="DO No:" ForeColor="Yellow"
                                ToolTip="Click to Go On Delivery Order" OnClick="lnkDo_Click"></asp:LinkButton>
                        </td>
                        <td colspan="4" align="left">
                            <asp:TextBox runat="server" ID="txtdo_no" CssClass="txt" Width="100px" Height="24px"
                                AutoPostBack="true" TabIndex="1"></asp:TextBox>
                            &nbsp; Do Date:
                            <asp:TextBox ID="txtdo_date" runat="Server" CssClass="txt" TabIndex="2" Width="100px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            &nbsp; Lifting Date:
                            <asp:TextBox ID="txtlifting_date" runat="Server" CssClass="txt" TabIndex="3" Width="100px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" style="width: 10%;">Driver No:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtdriver_no" runat="Server" CssClass="txt" TabIndex="4" Width="130px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>

                            &nbsp; Vehicle No:
                            <asp:TextBox ID="txtvehicle_no" runat="Server" CssClass="txt" TabIndex="5" Width="130px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">mill Name:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtmill_name" runat="Server" CssClass="txt" TabIndex="6" Width="250px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Grade:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtgrade" runat="Server" CssClass="txt" TabIndex="7" Width="100px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>

                            &nbsp;    Quantal :
                            <asp:TextBox ID="txtquantal" runat="Server" CssClass="txt" TabIndex="8" Width="100px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" style="width: 10%;">Bill To Name:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtbill_to_name" runat="Server" CssClass="txt" TabIndex="9" Width="250px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Freight:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtfreight" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            &nbsp; Advance:
                            <asp:TextBox ID="txtadvance" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Transport Name:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txttransport_name" runat="Server" CssClass="txt" TabIndex="12" Width="250px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" style="width: 10%;">DO Name:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtDo_name" runat="Server" CssClass="txt" TabIndex="12" Width="250px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td align="right" style="width: 10%;">Sale Bill No:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtsalebill_No" runat="Server" CssClass="txt" TabIndex="12" Width="250px"
                                MaxLength="10" Style="text-align: left;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td align="right" style="width: 10%;">Narration:
                        </td>
                        <td align="left" colspan="4">
                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txtgtnarration" CssClass="txt"
                                    TabIndex="36" Width="280px" Height="50px" />
                            </td>
                           </tr>
                </table>

                <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 3px; border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-bold="true" font-names="verdana" font-size="Medium">Details </h5>
                    </legend>
                </fieldset>
                <table width="80%" align="left">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Date :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdate" runat="Server" CssClass="txt" TabIndex="13" Width="90px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                AutoPostBack="True" OnTextChanged="txtdate_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtdate"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                            &nbsp; Station:
                            <asp:TextBox ID="txtStation" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                Style="text-align: left;" TabIndex="14" Width="150px"></asp:TextBox>
                            &nbsp;
                                <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                    Height="25px" OnClick="btnAdddetails_Click" TabIndex="15" />
                            &nbsp;
                             <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px"
                                 Height="25px" TabIndex="16" />
                        </td>
                    </tr>

                </table>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td align="left" style="margin-top: 100px;" width="60%">
                                    <div style="width: 100%; position: relative;">
                                        <asp:UpdatePanel ID="upGrid" runat="server">
                                            <ContentTemplate>

                                                <asp:Panel ID="pnlgrdDetail" runat="server" BackColor="SeaShell" BorderColor="Maroon"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                                    Height="100px" ScrollBars="Both" Style="margin-left: 30px; float: left;" Width="1200px">
                                                    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" CellPadding="5"
                                                        CellSpacing="5" GridLines="Both" HeaderStyle-BackColor="#397CBB" HeaderStyle-ForeColor="White"
                                                        HeaderStyle-Height="30px" OnRowCommand="grdDetail_RowCommand" OnRowDataBound="grdDetail_RowDataBound"
                                                        Style="table-layout: fixed;" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="lnk" CommandName="EditRecord"
                                                                        Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument="lnk" CommandName="DeleteRecord"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="Black" Height="25px" Wrap="false" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <table style="width: 100%;" align="left" cellpadding="4" cellspacing="4">
                                        <tr>
                                            <td align="right">Reached:
                                            </td>
                                            <td align="left">

                                                <asp:DropDownList ID="drpreached" runat="server" AutoPostBack="true" Width="120px"
                                                    CssClass="ddl" OnSelectedIndexChanged="drpreached_SelectedIndexChanged" TabIndex="17">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp&nbsp&nbsp Reached Date:
                                                    <asp:TextBox ID="txtreached_date" runat="Server" CssClass="txt" TabIndex="18" Width="90px"
                                                        MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                                        AutoPostBack="True" OnTextChanged="txtreached_date_TextChanged"
                                                        Height="24px"></asp:TextBox>
                                                <asp:Image ID="img1" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                                    Width="25px" Height="15px" />
                                                <ajax1:CalendarExtender ID="calenderExtenderreached_date" runat="server" TargetControlID="txtreached_date"
                                                    PopupButtonID="img1" Format="dd/MM/yyyy">
                                                </ajax1:CalendarExtender>

                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                </tr>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="70%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Search Text:
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="1000px" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
                                    <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                    <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                                    <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
