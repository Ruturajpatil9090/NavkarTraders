<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeCreateCompany.aspx.cs" Inherits="pgeCreateCompany" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
   

    <script type="text/javascript">

        function Confirm() {
            var confirm_Value = document.createElement("INPUT");
            confirm_Value.type = "hidden";
            confirm_Value.name = "confirm_value";
            if (confirm("Do you want to Delete Record?")==true) 
         // if( prompt("Do you want to Delete Record?", "Cancel")==true)
          {     
                confirm_Value.value = "Yes";
            }
            else {
                confirm_Value.value = "No";
            } 
            document.forms[0].appendChild(confirm_Value.value);
        }



    function OpenPopup(evt) {
   
        var keyCode; if (evt == null) {
            keyCode = window.event.keyCode;
        }

        else {
            keyCode = evt.keyCode;
        }

        // alert(keyCode);=if (keyCode==113)
        var txtValue = document.getElementById("<%=txtCompanyCode.ClientID %>").value
        if (keyCode == 113) {

            window.open("popup.aspx?query=select Company_Code,Company_Name_E as [Company Name] from Company where Company_Name_E like'%" + txtValue + "%'", "List", "scrollbars=no,resizable=yes,width=400,height=280");
            return true;
            $(document).ready();
        }
        else if (keyCode == 9) {
            if (txtValue == "") {
                alert("Enter Company Code");
            }
            else {
                $(document).ready(function () {

                    $.ajax({

                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "pgeCreateCompany.aspx/BindDatatable",
                        data: JSON.stringify({ str: txtValue }),
                        dataType: "json",
                        success: function (data) {
                            
                            document.getElementById("<%=txtCompanyName.ClientID %>").value = data.d[0].Company_Name_E;
                            document.getElementById("<%=txtRegionalName.ClientID %>").value = data.d[0].Company_Name_R;
                            document.getElementById("<%=txtAddress.ClientID %>").value = data.d[0].Address_E;
                            document.getElementById("<%=txtRegionalAddress.ClientID %>").value = data.d[0].Address_R;

                            document.getElementById("<%=txtcityE.ClientID %>").value = data.d[0].City_E;
                            document.getElementById("<%=txtstateE.ClientID %>").value = data.d[0].State_E;
                            document.getElementById("<%=txtpin.ClientID %>").value = data.d[0].PIN;
                            document.getElementById("<%=txtcityR.ClientID %>").value = data.d[0].City_R;

                            document.getElementById("<%=txtstateR.ClientID %>").value = data.d[0].State_R;
                            document.getElementById("<%=txtMob.ClientID %>").value = data.d[0].Mobile_No;
                            document.getElementById("<%=txtPanNo.ClientID %>").value = data.d[0].Pan_No;

                            document.getElementById("<%=lblMsg.ClientID %>").innerHTML = null;
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                });
            }
        }
        


}


    
    function handlepopupresult(result) {
        var txtCompanyCode = document.getElementById("<%=txtCompanyCode.ClientID %>");
        txtCompanyCode.value = result;
        txtCompanyCode.focus();
        
        document.getElementById("<%=txtCompanyName.ClientID %>").value = "";
        document.getElementById("<%=txtRegionalName.ClientID %>").value = "";
        document.getElementById("<%=txtAddress.ClientID %>").value = "";
        document.getElementById("txtRegionalAddress.ClientID %>").value = "";

        document.getElementById("<%=txtcityE.ClientID %>").value = "";
        document.getElementById("<%=txtstateE.ClientID %>").value = "";
        document.getElementById("<%=txtpin.ClientID %>").value = "";
        document.getElementById("<%=txtcityR.ClientID %>").value = "";

        document.getElementById("<%=txtstateR.ClientID %>").value = "";
        document.getElementById("<%=txtMob.ClientID %>").value = "";
        document.getElementById("<%=txtPanNo.ClientID %>").value = "";

        document.getElementById("<%=lblMsg.ClientID %>").innerHTML = null;
       
//        PageMethods.empty();
        



     
    }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:UpdatePanel ID="UpMain" runat="server">
<ContentTemplate>
<asp:Panel ID="Panel1" runat="server" Font-Names="Verdana" 
    Font-Size="Small" Width="100%"  style="text-align:center;">
    <asp:HiddenField ID="hdnf" Value="0" runat="server" />
    <table width="70%" align="center">
    <tr>
    <td style="width:100%;">       
    <fieldset style="text-align:center;">
    <legend style="text-align:center;">
    Company Creation
    </legend>

    <table width="100%">

    <tr>
    <td>    
    <table width="70%" align="left">
    <tr>
    <td colspan="2" align="left">
    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Names="verdana" Font-Italic="true" Font-Size="Small" ForeColor="Red" ></asp:Label>
    </td>
    </tr>
    <tr>
    <td align="left">
    &nbsp;&nbsp;&nbsp;Code:
    </td>
    <td align="left">

    <asp:TextBox ID="txtCompanyCode" runat="server" AutoPostBack="true" CssClass="txt" onkeydown= "OpenPopup(event);"
          ontextchanged="txtCompanyCode_TextChanged"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvtxtCompanyCode" runat="server" SetFocusOnError="true" Text="Required" ErrorMessage="Required" ControlToValidate="txtCompanyCode" CssClass="validator" ValidationGroup="add">
     </asp:RequiredFieldValidator>
    <%--<ajax1:FilteredTextBoxExtender ID="filtertxtCompanyCode" runat="server" FilterType="Numbers" TargetControlID="txtCompanyCode"></ajax1:FilteredTextBoxExtender>--%>
    <%--<ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtCompanyCode"></ajax1:FilteredTextBoxExtender>--%>
    </td>
    </tr>
    <tr>
    <td align="left">
    <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red" ></asp:Label>
    Company Name:
    </td>
    <td align="left">
    <asp:TextBox ID="txtCompanyName"  runat="server" Width="400px" CssClass="txt"></asp:TextBox><%--onkeypress="__doPostBack(this.name,'OnKeyPress');"--%>
    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" Text="Required" ErrorMessage="Required" SetFocusOnError="true" ControlToValidate="txtCompanyName" CssClass="validator" ValidationGroup="add">
    </asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td align="left">
    &nbsp;&nbsp;&nbsp;Regional Name:
    </td>
    <td align="left">
    <asp:TextBox ID="txtRegionalName" runat="server" Width="400px" CssClass="txt" ></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left" valign="top">
    <asp:Label ID="lblstar1" runat="server" Text="*" ForeColor="Red" ></asp:Label>
    Company Address:
    </td>
    <td align="left">
    <asp:TextBox ID="txtAddress" runat="server" Width="230px" TextMode="MultiLine" Height="80px" CssClass="txt"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvtxtAddress" runat="server" SetFocusOnError="true" Text="Required" ErrorMessage="Required" ControlToValidate="txtAddress" CssClass="validator" ValidationGroup="add">
     </asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td align="left" valign="top">
    &nbsp;&nbsp;&nbsp;Regional Address:
    </td>
    <td align="left">
    <asp:TextBox ID="txtRegionalAddress" runat="server" Width="230px" TextMode="MultiLine" Height="80px" CssClass="txt"></asp:TextBox>
    </td>
    </tr>
    </table>
    </td>
    </tr>

    
 <tr>
 <td>
 <table width="90%" align="left">
    <tr>
    <td align="left">
    City:
    </td>
    <td align="left">
    <asp:TextBox ID="txtcityE" runat="server" CssClass="txt"></asp:TextBox>
    </td>
    <td align="left">    
    State:
    </td>
    <td align="left">
    <asp:TextBox ID="txtstateE" runat="server" CssClass="txt"></asp:TextBox>
    </td>
    <td align="left">
    Pin:
    </td>
    <td align="left">
    <asp:TextBox ID="txtpin" runat="server" MaxLength="6" CssClass="txt"></asp:TextBox>
    <ajax1:FilteredTextBoxExtender ID="filterPin" runat="server" TargetControlID="txtpin" FilterType="Numbers"></ajax1:FilteredTextBoxExtender>
    </td>
    </tr>
    <tr>
    <td align="left">R City</td>
    <td align="left"><asp:TextBox ID="txtcityR" runat="server" CssClass="txt"></asp:TextBox></td>
    <td align="left">R State</td>
    <td align="left"><asp:TextBox ID="txtstateR" runat="server" CssClass="txt"></asp:TextBox></td>
    <td></td>
    <td></td>
    </tr>
   <tr>
     <%--<td align="left">
    Financial Year From:
    </td>
    <td align="left">
    <asp:TextBox ID="txtYearFrom" runat="server" CssClass="txt" Width="80px"></asp:TextBox>
    <asp:Image ID="imgCalender1" runat="server"  ImageUrl="~/Images/calender.jpg" Width="20px" Height="15px"/>
    <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtYearFrom" PopupButtonID="imgCalender1" Format="dd-MM-yyyy">
    </ajax1:CalendarExtender>
    </td>
    <td align="left">
    To:
    </td>
    <td align="left">
    <asp:TextBox ID="txtYearTo" runat="server" CssClass="txt" Width="80px"></asp:TextBox>
    <asp:Image ID="imgCalender2" runat="server"  ImageUrl="~/Images/calender.jpg" Width="20px" Height="15px"/>
  <ajax1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtYearTo" PopupButtonID="imgCalender2" Format="dd-MM-yyyy">
    </ajax1:CalendarExtender>
    </td>--%>
    <td align="left">
    Mobile:
    </td>
    <td align="left">
    <asp:TextBox ID="txtMob" runat="server" MaxLength="10" CssClass="txt"></asp:TextBox>
    <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMob">
    </ajax1:FilteredTextBoxExtender>
    </td>
    </tr>
    <tr>
    <td align="left">
    Pan No:
    </td>
    <td>
    <asp:TextBox ID="txtPanNo" runat="server" CssClass="txt"></asp:TextBox>
    </td>
    </tr>
    </table>
 </td>
 </tr>      
<tr>
<td>
<table width="100%" align="center">
 <tr>
 <td align="center">
<asp:Button ID="btnAdd" runat="server"  Text="Add New" CssClass="btnSubmit"
ValidationGroup="save" onclick="btnAdd_Click"/>
 &nbsp;
 <asp:Button ID="btnSave" runat="server"  Text="Save" CssClass="btnSubmit"
  ValidationGroup="add" onclick="btnSave_Click"/>
 &nbsp;
 <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnSubmit"
         ValidationGroup="save" onclick="btnEdit_Click" />
  &nbsp;
 <asp:Button ID="btnDelete" runat="server"  Text="Delete" CssClass="btnSubmit" OnClientClick="Confirm()" 
         ValidationGroup="add" onclick="btnDelete_Click" />
  &nbsp;
 <asp:Button ID="btnCancel" runat="server"  Text="Cancel" CssClass="btnSubmit"
 ValidationGroup="save" onclick="btnCancel_Click"/>
 </td>
 </tr>
 </table>
 </td>
 </tr>
 <tr>
 <td >
 <table width="80%" align="center">
 <tr>
 <td>
 
 <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" 
         CssClass="btnSubmit" onclick="btnFirst_Click"/>
 <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="Previous"  
         CssClass="btnSubmit" onclick="btnPrevious_Click"/>
 <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="Next" 
         CssClass="btnSubmit" onclick="btnNext_Click"/>
 <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="Last" 
         CssClass="btnSubmit" onclick="btnLast_Click"/>
 </td>
 </tr>
 </table>
 </td>
 </tr>

 </table>
 </fieldset>
    </td>
    </tr>
    </table>
    
    </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

