<%@ Page Title="" Language="C#" MasterPageFile="~/UserSecurityMaster.master" AutoEventWireup="true" CodeFile="pgeUserSecurity.aspx.cs" Inherits="Sugar_pgeUserSecurity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <link href="CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //Total out of range dialog
        function ShowRangeDialog() {
            $(function () {
                $('#dialog').dialog({
                })
            }).dialog("open");
        }
    </script>

     <script type="text/javascript">

        
         /* Add "https://api.ipify.org?format=json" statement
                    this will communicate with the ipify servers in
                    order to retrieve the IP address $.getJSON will
                    load JSON-encoded data from the server using a
                    GET HTTP request */
      
         debugger;
         //$.getJSON("https://icanhazip.com", function (data) {
         $.getJSON("https://ipapi.co/json", function (data) {
        
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
                     window.open('../Sugar/pgeHome.aspx', "_self"); 
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
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfway" runat="server" />
            <asp:HiddenField ID="hdnfconfirm" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
         

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
               <asp:Label ID="lblIPAdd" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="Label1" runat="server" CssClass="lblName" ForeColor="Red"></asp:Label>
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