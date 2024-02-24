<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popup.aspx.cs" Inherits="popup" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    

    <script type="text/javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        
      //  var str = null;

        function CloseWindow() {
            window.close();
            window.opener.location.reload();

        }
        window.onload = function () {
            
            UpperBound = parseInt('<%= this.GridView1.Rows.Count %>') - 1;
            LowerBound = 0;
            SelectedRowIndex = 0;
             CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
             CurrentRow.originalForeColor = CurrentRow.style.color;
             CurrentRow.style.backgroundColor = '#DCFC5C';
             CurrentRow.style.color = 'Black';
             
            
        }


        function SelectRow(CurrentRow, RowIndex) {

            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)
            
                return;

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

//        function demo(e) {

//        }



        function SelectSibling(e) {

         
            var keyEnterValue = document.getElementById("<%=hdnKeyCode.ClientID %>").value

            <%var text1= "N";%>
           <%Session["test1"] =text1;%>


            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;

            if (KeyCode == 40)
            {
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
                }
            else if (KeyCode == 38)
            {
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);

                }

            else if (KeyCode == 13)
           {

                var hdvalue=  document.getElementById("<%=  hdnKeyCode.ClientID%>")
                var grid = document.getElementById("<%= GridView1.ClientID %>");
                var hv= grid.rows[SelectedRowIndex+1].cells[0].innerText;
                var hv1=grid.rows[SelectedRowIndex+1].cells[1].innerText;
                hdvalue.value=hv;
               
               var hdrow =  document.getElementById("<%=  hdrow.ClientID%>").value;
               var hdcol =  document.getElementById("<%=  hdcol.ClientID%>").value;
               var r=hdrow.value;

                window.close();
               
               window.opener.handlepopupresult(hdvalue.value,hv1);                                                                          
               // alert(hv)                   
           }
          
            }

           
//          
               // keyEnterValue="Y"
                  // Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$1");
              
           
           
           // return false;

      
    </script>

   
</head>
<body >
    <form id="form1" runat="server">
    <div>
    <asp:HiddenField ID="hdrow" Value="0"  runat="server"/>
    <asp:HiddenField ID="hdcol" Value="0"  runat="server"/>
    <asp:HiddenField ID="hdnKeyCode" Value="0"  runat="server"/>
        <asp:GridView ID="GridView1" runat="server"  onrowcreated="GridView1_RowCreated" onrowdatabound="GridView1_RowDataBound" 
         Font-Names="Verdana" Font-Size="Small" Width="100%"   
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onselectedindexchanging="GridView1_SelectedIndexChanging"   >

        </asp:GridView>
    </div>
    </form>
</body>
</html>

