<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeHome.aspx.cs" Inherits="pgeHome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        //Total out of range dialog
        function ShowRangeDialog() {
            $(function () {
                $('#dialog').dialog({
                })
            }).dialog("open");
        }
    </script>
    <style type="text/css">
        .bagroundPopup {
            opacity: 0.6;
            background-color: Black;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://jsonip.appspot.com/?callback=DisplayIP";
            document.getElementsByTagName("head")[0].appendChild(script);
        };
        function DisplayIP(response) {
            document.getElementById("ipaddress").innerHTML = "Your IP Address is " + response.ip;
        }
    </script>
    <script type="text/javascript">
        function Validate() {
            var RB1 = document.getElementById("<%=rblist.ClientID%>");
            var radio = RB1.getElementsByTagName("input");
            var isChecked = false;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    isChecked = true;
                    break;
                }
            }
            if (!isChecked) {
                alert("Please select atleast one option");
            }
            return isChecked;
        }
    </script>
    <script type="text/javascript">
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=AdminGrid.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "checkbox") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= AdminGrid.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            return true;
                        }
                    }
                }
            }
            //document.getElementById('<%=lblValidateCheckbox.ClientID %>').innerText = "Please select atleast one checkbox";
            alert("Please select atleast one Admin!");
            return false;
        }


    </script>
    <script type="text/javascript">
        function dispSummary() {
            var d = new Date().toISOString().slice(0, 10);
            window.open('../Report/rptDispSummarySmall.aspx?fromDT=' + d + '&toDT=' + d + '&Branch_Code=');
        }
        function bwlpa(FromDT, ToDt, Broker_Code) {
            window.open('../Report/rptBrokerWiseLatePayAll.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt + '&Broker_Code=' + Broker_Code);
        }

    </script>
    <script type="text/javascript">
        window.onload = function () {
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://www.telize.com/jsonip?callback=DisplayIP";
            document.getElementsByTagName("head")[0].appendChild(script);
        };
        function DisplayIP(response) {

        }
    </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
    <script type="text/javascript">
        //var checkStatus;

        //var element = new Image();
        //element.__defineGetter__('id', function () {
        //    checkStatus = 'on';
        //});

        //setInterval(function () {
        //    checkStatus = 'off';
        //    console.log(element);
        //    console.clear();
        //    document.querySelector('#status').innerHTML = checkStatus;
        //}, 1000)
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js" type="text/javascript">
    </script>

    <script type="text/javascript">
       
        /* Add "https://api.ipify.org?format=json" statement
                   this will communicate with the ipify servers in
                   order to retrieve the IP address $.getJSON will
                   load JSON-encoded data from the server using a
                   GET HTTP request */
        debugger;
        $.getJSON("https://api.ipify.org?format=json", function (data) {
            debugger;
            // Setting text of element P with id gfg
            $("#<%=lblIPAdd.ClientID %>").val(data.ip);
            ipaddress = data.ip;

            //  $("#gfg").html(data.ip);
            var hdnfconfirm = document.getElementById("<%= hdnfconfirm.ClientID %>").value;
                  var ipaddress;
                  var IPAddress1 = '<%= Session["IPAddress1"] %>';
            var IPAddress2 = '<%= Session["IPAddress2"] %>';
            var confirm = '<%=Session["Confirm"]  %>';
            if (hdnfconfirm == "Y") {
                if (IPAddress1 == ipaddress || IPAddress2 == ipaddress) {
                }
                else {
                    if (confirm == "Y") {

                    }
                    else {
                       // ModalPopupMsg.Show();
                        // document.getElementById('<%=ModalPopupMsg.ClientID %>').style.display = "block";
                        $find("mpm").show();
                    }
                }
            }

        })





    </script>
    <script type="text/javascript">
        function DO() {
            //   window.open('../Sugar/pgeDeliveryOrderForGST.aspx');
            var a = '<%= Session["DOPages"] %>';
            if (a == 1) {
                window.open('../Sugar/BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=0&Action=2');
            }
            else {

                window.open('../Sugar/BussinessRelated/pgeDeliveryOrderMultipleItem.aspx?DO=0&Action=2');
            }
        }
        function TP() {
            window.open('../Sugar/BussinessRelated/PgeTenderHeadUtility.aspx');
        }
        function GL() {
            window.open('../Sugar/Report/pgeGLedgerReport.aspx');
        }
        function MP(FromDT, ToDt, Millcode) {
            window.open('../Sugar/Report/rptMillPaymentForGST.aspx?fromDT=' + FromDT + '&ToDt=' + ToDt + '&Mill_Code=0')
        }
        //function Ac(FromDT, ToDt) {
        //    window.open('../Sugar/Report/rptUtrOnlyBalance.aspx?accode=&utr_no=&AcType=M&FromDT=' + FromDT + '&ToDt=' + ToDt);
        //}
        function DD(FromDT, ToDt, Branch_Code) {
            window.open('../Sugar/Report/rptDoWiseDispatch.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt + '&Branch_Code=' + Branch_Code);
        }
        function report(FromDT, ToDt) {
            window.open('../Sugar/Report/rptPartyWiseSugarStock.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt);
        }
        function dispSummary() {
            var d = new Date().toISOString().slice(0, 10);
            window.open('../Sugar/Report/rptDispSummarySmall.aspx?fromDT=' + d + '&toDT=' + d + '&Branch_Code=');
        }
        function bwlpa(FromDT, ToDt, Broker_Code) {
            window.open('../Sugar/Report/rptBrokerWiseLatePayAll.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt + '&Broker_Code=' + Broker_Code);
        }
        function Account() {
            window.open('../Sugar/Master/pgeAccountUtility.aspx');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfway" runat="server" />
            <asp:HiddenField ID="hdnfconfirm" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <div class="leftPane" style="width: 1300px; height: 500px; margin: 15px 0 0 10px;">
                    <table width="100%" align="left">
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink9" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/BussinessRelated/pgeRegisters.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Register</p>
                                         </span></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink5" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank"
                                    class="button" onclick="DO();"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Delivery Order</p>
                                         </span></asp:HyperLink>


                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink1" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/Transaction/PgeUTRHeadUtility.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Utr Entry</p>
                                         </span></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink2" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/BussinessRelated/PgeTenderHeadUtility.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Tender Purchase</p>
                                         </span></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btndatabasebackup" Text="Database Backup" Style="font-size: large; text-decoration: none; color: White; width: 190px; height: 50px;"
                                    class="button"
                                    OnClick="btndatabasebackup_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink3" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/BussinessRelated/PgeCarporatesaleUtility.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Carporate Sale</p>
                                         </span></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink4" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/Transaction/pgeMultipleReceipt_utility.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Multiple Receipt</p>
                                         </span></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink6" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/Transaction/PgeReceiptPaymentUtility.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Receipt Payment</p>
                                         </span></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink7" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/Report/pgeTrialBalance.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Trial Balance Screen</p>
                                         </span></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink10" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/Report/pgestockbook.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                           Sugar Balance Stock</p>
                                         </span></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a onclick="javascript:dispSummary();" style="font-size: large; text-decoration: none; color: White;"
                                    class="button">
                                    <p style="margin: 20px auto;">
                                        Dispatch Summary
                                    </p>
                                </a>
                            </td>
                            <td>
                                <a href="../Sugar/pgeTransportSMS.aspx" target="_blank" style="font-size: large; text-decoration: none; color: White; width: 170px;"
                                    class="button">
                                    <p style="margin: 20px auto; margin-left: 10px;">
                                        Transport SMS
                                    </p>
                                </a>
                            </td>
                            <td>
                                <a href="../Sugar/Report/rptSugarBalanceStocks.aspx" target="_blank" style="font-size: large; text-decoration: none; color: White; width: 170px;"
                                    class="button">
                                    <p style="margin: 20px auto; margin-left: 10px;">
                                        Stock Book
                                    </p>
                                </a>
                            </td>
                            <td>
                                <a href="../Sugar/Report/rptSugarBalanceStockSummary.aspx" target="_blank" style="font-size: large; text-decoration: none; color: White; width: 170px;"
                                    class="button">
                                    <p style="margin: 20px auto; margin-left: 10px;">
                                        Stock Summary
                                    </p>
                                </a>
                                <asp:Label ID="lblIPAdd" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="Label1" runat="server" CssClass="lblName" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnAll" Text="Daily Report" Style="font-size: large; text-decoration: none; color: White; width: 190px; height: 80px;"
                                    class="button"
                                    OnClick="btnAll_Click" />
                            </td>


                            <td>
                                <a href="../Sugar/Report/pgeGLedgerReport.aspx" target="_blank" style="font-size: large; text-decoration: none; color: White; width: 170px;"
                                    class="button">
                                    <p style="margin: 20px auto; margin-left: 10px;">
                                        Ledger
                                    </p>
                                </a>
                            </td>
                            <td>
                                <a href="../Sugar/BussinessRelated/pgeCarporateRegister.aspx" target="_blank" style="font-size: large; text-decoration: none; color: White; width: 170px;"
                                    class="button">
                                    <p style="margin: 20px auto; margin-left: 10px;">
                                        Carporate Register
                                    </p>
                                </a>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink8" runat="server" Style="font-size: large; color: white; width: 170px;" Target="_blank" class="button" NavigateUrl="~/Sugar/Report/pgeBrokerReport.aspx"><span>
                         <p style="margin-top: 20px; margin-left: 20px;">
                            Broker Report</p>
                                         </span></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table width="100%" align="right">
                        <tr>
                            <td>
                                <a target="_blank" href="https://services.gst.gov.in/services/login">
                                    <img src="../Images/gst.png" height="60px" width="60px" />
                                </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://onlineservices.tin.egov-nsdl.com/etaxnew/tdsnontds.jsp">
                        <img src="../Images/e-payments.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://ewaybillgst.gov.in/login.aspx">
                        <img src="../Images/e way.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.google.com/">
                        <img src="../Images/google.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://accounts.google.com/signin/v2/identifier?passive=1209600&continue=https%3A%2F%2Faccounts.google.com%2Fb%2F0%2FAddMailService&followup=https%3A%2F%2Faccounts.google.com%2Fb%2F0%2FAddMailService&flowName=GlifWebSignIn&flowEntry=ServiceLogin">
                        <img src="../Images/gmail.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.google.com/drive/">
                        <img src="../Images/Google Drive.png" height="60px" width="60px" />
                    </a>

                                &emsp;&emsp;
                    <a target="_blank" href="https://accounts.google.com/signin/v2/identifier?service=analytics&passive=1209600&continue=https%3A%2F%2Fanalytics.google.com%2Fanalytics%2Fweb%2F%23&followup=https%3A%2F%2Fanalytics.google.com%2Fanalytics%2Fweb%2F&flowName=GlifWebSignIn&flowEntry=ServiceLogin">
                        <img src="../Images/Google Analytics.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://outlook.live.com/owa/">
                        <img src="../Images/outlook.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.moneycontrol.com/">
                        <img src="../Images/money control.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.bseindia.com/markets/Commodity/commodity.html">
                        <img src="../Images/bse-commodity.png" height="60px" width="60px" />
                    </a>

                                &emsp;&emsp;
                    <a target="_blank" href="./Transaction/pgeTransactionUtility.aspx">
                        <img src="../Images/Skypeimage.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.investing.com/">
                        <img src="../Images/investing.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://cibnext.icicibank.com/corp/AuthenticationController?FORMSGROUP_ID__=AuthenticationFG&__START_TRAN_FLAG__=Y&FG_BUTTONS__=LOAD&ACTION.LOAD=Y&AuthenticationFG.LOGIN_FLAG=1&BANK_ID=ICI&ITM=nli_corp_primer_login_btn_desk">
                        <img src="../Images/icici.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://netbanking.hdfcbank.com/netbanking/">
                        <img src="../Images/hdfc.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                                <a target="_blank" href="https://corp.onlinesbi.com/corpuser/login.htm">
                                    <img src="../Images/sbi.png" height="60px" width="60px" />
                                </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://corp.idbibank.co.in/corp/BANKAWAY?Action.CorpUser.Init.001=y&AppSignonBankId=IBKL&AppType=corporate">
                        <img src="../Images/idbi.png" height="60px" width="60px" />
                    </a>

                                &emsp;&emsp;
                    <a target="_blank" href="https://corporate.axisbank.co.in/wps/portal/cBanking/axiscbanking/AxisCorporateLogin/!ut/p/a1/04_Sj9CPykssy0xPLMnMz0vMAfGjzOKNAzxMjIwNjLwszI0cDRw9PU3dw3xcjP19TIAKIoEKDHAARwNC-sP1o_AqMTWFKsBjRUFuhEGmo6IiACFl0ps!/dl5/d5/L2dBISEvZ0FBIS9nQSEh/?_ga=2.11015732.1610748224.1570105808-2061852097.1570105808">
                        <img src="../Images/axis.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.facebook.com/">
                        <img src="../Images/facebook.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.linkedin.com/">
                        <img src="../Images/linked in.png" height="60px" width="60px" />
                    </a>

                                &emsp;&emsp;
                    <a target="_blank" href="https://twitter.com/">
                        <img src="../Images/twitter.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.youtube.com/">
                        <img src="../Images/youtube.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.aawaz.com/">
                        <img src="../Images/aawaz.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                                <a target="_blank" href="https://www.makemytrip.com/">
                                    <img src="../Images/mmt.png" height="60px" width="60px" />
                                </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.booking.com/index.en-gb.html?label=gen173rf-1BCAEoggI46AdIM1gDaGyIAQGYAQm4ARfIAQzYAQHoAQGIAgGiAg5jaGluaW1hbmRpLmNvbagCA7gCqMKK_AXAAgHSAiQ2NjUyZDk5NS0wMmNlLTRmYTEtYThhYS00N2JjMzUyNDQ0NmHYAgXgAgE;sid=3c6547307fe97f11e6e7b8ebbcb9ee27;keep_landing=1&sb_price_type=total&">
                        <img src="../Images/booking.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.redbus.in/">
                        <img src="../Images/redbus.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://www.irctc.co.in/nget/train-search">
                        <img src="../Images/IRCTC.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://hangouts.google.com/">
                        <img src="../Images/Google hangout.png" height="60px" width="60px" />
                    </a>
                                &emsp;&emsp;
                    <a target="_blank" href="https://web.whatsapp.com/">
                        <img src="../Images/Whatsapp.png" height="60px" width="60px" />
                    </a>

                            </td>
                        </tr>
                    </table>
                </div>

                <div id="dialog" style="display: none;">
                    <table cellspacing="10">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label runat="server" ID="lblMsg" Text="This Computer Dont have Security Certificate to Use This Site"
                                    Font-Size="Large" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Label runat="server" ID="lblGenerate" Text="You Want To Generate Certificate On this Computer?"
                                    Font-Size="Large" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button runat="server" ID="btnGenerate" Text="Generate" CssClass="button2" OnClick="btnGenerate_Click" />
                            </td>
                            <td align="left">
                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="button2" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

            </asp:Panel>
            <ajax1:ModalPopupExtender ID="ModalPopupMsg" PopupControlID="dialog" BackgroundCssClass="bagroundPopup"
                TargetControlID="btn" runat="server" BehaviorID="mpm">
            </ajax1:ModalPopupExtender>
            <asp:Button ID="btn" runat="server" Style="display: none;" />
            <asp:Button ID="btn2" runat="server" Style="display: none;" />
            <asp:Button ID="btn3" runat="server" Style="display: none;" />
            <asp:Button ID="btn4" runat="server" Style="display: none;" />
            <div class="otp-popup" id="divOtp" style="display: none;">
                <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnlOTP">
                            <table>
                                <tr>
                                    <td align="center">
                                        <h3 style="color: White;">User Verification</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblSendOtp" runat="server" Text="One Time Password" Font-Bold="true"
                                            Font-Italic="true" ForeColor="BurlyWood"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <p style="font-size: large; outline-color: Red; color: White;">
                                            Please Select Way To recieve OTP...
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rblist" OnSelectedIndexChanged="rblist_SelectedIndexChanged"
                                            ForeColor="#FFFFCC">
                                            <asp:ListItem Text="SMS" Value="S"></asp:ListItem>
                                            <asp:ListItem Text="Email" Value="E"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button runat="server" ID="btnSend" Text="SEND" CssClass="button2" OnClientClick="return Validate();"
                                            OnClick="btnSend_Click" Visible="false" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button runat="server" ID="Button2" Text="OK" CssClass="button2" OnClientClick="return Validate();"
                                            OnClick="btnOk_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnCancelOtp" Text="CANCEL"
                                            CssClass="button2" OnClick="btnCancelOtp_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <ajax1:ModalPopupExtender ID="ModalPopupOTP" PopupControlID="divOtp" BackgroundCssClass="bagroundPopup"
                TargetControlID="btn2" runat="server">
            </ajax1:ModalPopupExtender>
            <div id="otpVerification" class="otp-verification">
                <asp:UpdatePanel runat="server" ID="upl4">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td colspan="2" align="center">
                                    <h3 style="color: Olive; font-weight: bold;">OTP Verification</h3>
                                    <asp:Label runat="server" ID="lblWrongOtp" ForeColor="Red"></asp:Label>
                                    <asp:Label runat="server" ID="lblResendOtp" ForeColor="BlueViolet"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Please Enter One Time Password(OTP) You have Recieved:
                                </td>
                                <td align="left">
                                    <asp:TextBox runat="server" ID="txtOtpVerification" CssClass="textbox" Style="focus: true;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button runat="server" ID="btnConfirm" CssClass="button2" Text="VERIFY" OnClick="btnConfirm_Click" />
                                </td>
                                <td align="left">
                                    <asp:LinkButton runat="server" Text="Resend OTP" OnClick="resendlnk_Click" ID="resendlnk"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <ajax1:ModalPopupExtender ID="ModalPopupVerification" PopupControlID="otpVerification"
                BackgroundCssClass="bagroundPopup" TargetControlID="btn3" runat="server">
            </ajax1:ModalPopupExtender>
            <div id="user" style="width: 600px; height: 300px; background-color: #FFFFFF; display: none;">
                <div id="userhead" style="background-color: Red; height: 30px; text-align: center; top: 3px; border: 1px solid white;">
                    <h3 style="color: White; margin-top: 3px;">Access Denied!</h3>
                    <div id="usercontent">
                        <br />
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <p style="font-weight: bold; background-color: Black; color: White;">
                                        Your Dont Have Access To Generate Certificate...If You Want To Generate Please Contact
                                Your Admin..Below Is The List Of Site Admin..Please Select Only One Admin Who will
                                Give you The OTP Sent On His Mobile...
                                    </p>
                                </td>
                            </tr>
                        </table>
                        <div id="gridview">
                            <asp:GridView runat="server" ID="AdminGrid" PageSize="5" AutoGenerateColumns="false"
                                Width="100%" HeaderStyle-BackColor="ButtonShadow" HeaderStyle-ForeColor="White"
                                AlternatingRowStyle-BackColor="BlueViolet" RowStyle-Height="25px" RowStyle-ForeColor="Black"
                                AlternatingRowStyle-ForeColor="White" PagerStyle-BackColor="Black" PagerStyle-ForeColor="White">
                                <Columns>
                                    <asp:BoundField DataField="User_Name" HeaderText="Admin Name" ItemStyle-Width="300px" />
                                    <asp:BoundField DataField="EmailId" HeaderText="EmailId" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="mobile" HeaderText="Mobile" ItemStyle-Width="100px" />
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="grdCB" onclick="CheckBoxCheck(this);" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <asp:Label runat="server" ID="lblValidateCheckbox" Text="" ForeColor="Red"></asp:Label>
                        <table width="100%" align="center">
                            <tr>
                                <td align="right">
                                    <asp:Button runat="server" ID="btnSendOtptoAdmin" Text="SEND" CssClass="button2"
                                        OnClick="btnSendOtptoAdmin_Click" OnClientClick="javascript:validateCheckBoxes()" />
                                </td>
                                <td style="width: 10%;"></td>
                                <td align="left">
                                    <asp:Button runat="server" ID="Button1" Text="Cancel" CssClass="button2" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <ajax1:ModalPopupExtender ID="usermodalpopup" PopupControlID="user" BackgroundCssClass="bagroundPopup"
                TargetControlID="btn4" runat="server">
            </ajax1:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
