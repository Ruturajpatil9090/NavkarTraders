<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testpdf.aspx.cs" Inherits="testpdf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button Text="export PDF" runat="server" ID="btnpdf" OnClick="btnPDf_Click" />
    <asp:Panel runat="server" ID="pnlmain">
        <div class="details-page">
            <style media="all" type="text/css">
     
  .pcs-template {
  	font-family: Ubuntu;
    font-size: 9pt;
    color: #333333;
      background:  #ffffff ;
  }

  .pcs-header-content {
    font-size: 9pt;
	color: #333333;
	background-color: #dbc3c3;
  }
  .pcs-template-body {
  	padding: 0 0.400000in 0 0.550000in;
  }
  .pcs-template-footer {
  	height: 0.700000in;
	font-size: 6pt;
	color: #aaaaaa;
	padding: 0 0.400000in 0 0.550000in;
	background-color: #ffffff;
  }
  .pcs-footer-content {
  word-wrap: break-word;
  color: #aaaaaa;
      border-top: 1px solid #e3e3e3;
  }

  .pcs-label {
    color: #817d7d;
  }
  .pcs-entity-title {
    font-size: 28pt;
    color: #000000;
  }
  .pcs-orgname {
    font-size: 10pt;
    color: #333333;
  }
  .pcs-customer-name {
    font-size: 9pt;
    color: #333333;
  }
 .pcs-itemtable-header {
    font-size: 12pt;
    color: #f5efef;
    background-color: #294fd4;
  }
  .pcs-taxtable-header {
    font-size: 12pt;
    color: #000;
    background-color: #f5f4f3;
  }
  .itemBody tr {
    page-break-inside: avoid;
    page-break-after:auto;
  }
  .pcs-item-row {
    font-size: 8pt;
    border-bottom: 1px solid #e3e3e3;
    background-color: #ffffff;
    color: #000000;
  }
  .pcs-item-sku {
    margin-top: 2px;
  	font-size: 10px;
  	color: #444444;
  }
  .pcs-item-desc {
      color: #727272;
      font-size: 8pt;
   }
  .pcs-balance {
    background-color: #f5f4f3;
    font-size: 9pt;
    color: #000000;
  }
  .pcs-totals {
    font-size: 9pt;
    color: #000000;
    background-color: #ffffff;
  }
  .pcs-notes {
    font-size: 8pt;
  }
  .pcs-terms {
    font-size: 8pt;
  }
  .pcs-header-first {
	background-color: #dbc3c3;
	font-size: 9pt;
	color: #333333;
      height: auto;
	}

 .pcs-status {
 	color: ;
	font-size: 15pt;
	border: 3px solid ;
	padding: 3px 8px;
 }

 @page :first {
 	@top-center {
		content: element(header);
	}
    margin-top: 0.700000in;
  }

  .pcs-template-header {
	padding: 0 0.400000in 0 0.550000in;
    height: 0.700000in;
  }
  .pcs-itemtable-description {
    width: 40%
  }

/* Additional styles for RTL compat */

/* Helper Classes */

.inline {
  display: inline-block;
}
.v-top {
  vertical-align: top;
}
.text-align-right {
  text-align: right;
}
.text-align-left {
  text-align: left;
}

/* Helper Classes End */

.item-details-inline {
  display: inline-block;
  margin: 0 10px;
  vertical-align: top;
}

.total-in-words-container {
  width: 100%;
  margin-top: 10px;
}
.total-in-words-label {
  vertical-align: top;
  padding: 0 10px;
}
.total-in-words-value {
  width: 170px;
}
.total-section-label {
  padding: 5px 10px 5px 0;
  vertical-align: middle;
}
.total-section-value {
  width: 120px;
  vertical-align: middle;
    padding: 10px 10px 10px 5px;
}

/* Overrides/Patches for RTL compat */
/* Overrides/Patches End */

     .lineitem-header {
        padding: 5px 10px 5px 5px;
       word-wrap: break-word;
     }
     .lineitem-column {
       padding: 10px 10px 5px 10px;
       word-wrap: break-word;
       vertical-align: top;
     }
     .lineitem-content-right {
        padding: 10px 10px 10px 5px;
     }
     .total-number-section {
        float: left;
     }
     .total-section {
        float: right;
     }
</style>
            <div class="pcs-template">
                <div class="pcs-template-header pcs-header-content" id="header">
                </div>
                <div class="pcs-template-body">
                    <table style="width: 100%; table-layout: fixed;">
                        <tbody>
                            <tr>
                                <td style="vertical-align: top; width: 50%;">
                                    <div>
                                        <img src="https://books.zoho.com/ZFInvoiceLogo.zbfs?logo_id=ffbbf41f470ef181a262b6d698c356cf"
                                            style="width: 240.00px; height: 160.00px;" id="logo_content" alt="ANKUSH" />
                                    </div>
                                    <span class="pcs-orgname"><b>Shreepress</b></span><br/>
                                    <span class="pcs-label"><span style="white-space: pre-wrap; word-wrap: break-word;"
                                        id="tmp_org_address">2448 D Ward,Juna Budhwar Peth Toraskar Chowk Kolhapur 416002
                                        India</span> </span>
                                </td>
                                <td style="width: 50%;" class="text-align-right v-top">
                                    <span class="pcs-entity-title">Invoice</span><br/>
                                    <span id="tmp_entity_number" style="font-size: 10pt;" class="pcs-label"><b># INV-000001</b></span>
                                    <div style="clear: both; margin-top: 20px;">
                                        <span style="font-size: 8pt;"><b>Balance Due</b></span><br/>
                                        <span style="font-size: 12pt;"><b>₹0.00</b></span>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table style="clear: both; width: 100%; margin-top: 30px; table-layout: fixed;">
                        <tbody>
                            <tr>
                                <td style="width: 60%; vertical-align: bottom; word-wrap: break-word;">
                                    <div>
                                        <label style="font-size: 10pt;" class="pcs-label" id="tmp_billing_address_label">
                                            Bill To</label>
                                        <br/>
                                        <span class="pcs-customer-name" id="zb-pdf-customer-detail"><a href="#/contacts/619877000000058056">
                                            smartsoft consultancy </a></span>
                                        <br/>
                                        <span style="white-space: pre-wrap;" id="tmp_billing_address">1212,Shaniwar peth kolhapur
                                            416002 maharashtra India</span>
                                    </div>
                                </td>
                                <td align="right" style="vertical-align: bottom; width: 40%;">
                                    <table style="float: right; width: 100%; table-layout: fixed; word-wrap: break-word;"
                                        border="0" cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td style="padding: 5px 10px 5px 0px; font-size: 10pt;" class="text-align-right">
                                                    <span class="pcs-label">Invoice Date :</span>
                                                </td>
                                                <td class="text-align-right">
                                                    <span id="tmp_entity_date">20/04/2017</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 10px 5px 0px; font-size: 10pt;" class="text-align-right">
                                                    <span class="pcs-label">Terms :</span>
                                                </td>
                                                <td class="text-align-right">
                                                    <span id="tmp_payment_terms">Due on Receipt</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 10px 5px 0px; font-size: 10pt;" class="text-align-right">
                                                    <span class="pcs-label">Due Date :</span>
                                                </td>
                                                <td class="text-align-right">
                                                    <span id="tmp_due_date">20/04/2017</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 10px 5px 0px; font-size: 10pt;" class="text-align-right">
                                                    <span class="pcs-label">P.O.# :</span>
                                                </td>
                                                <td class="text-align-right">
                                                    <span id="tmp_ref_number">SO-00001</span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table style="width: 100%; margin-top: 20px; table-layout: fixed;" class="pcs-itemtable"
                        border="0" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr style="height: 32px;">
                                <td style="padding: 5px 0 5px 5px; text-align: center; word-wrap: break-word; width: 5%;"
                                    class="pcs-itemtable-header">
                                    #
                                </td>
                                <td style="padding: 5px 10px 5px 20px; word-wrap: break-word;" class="pcs-itemtable-header pcs-itemtable-description">
                                    Item &amp; Description
                                </td>
                                <td style="width: 11%;" class="pcs-itemtable-header lineitem-header text-align-right">
                                    Qty
                                </td>
                                <td style="width: 11%;" class="pcs-itemtable-header lineitem-header text-align-right">
                                    Rate
                                </td>
                                <td style="width: 11%;" class="pcs-itemtable-header lineitem-header text-align-right">
                                    Tax %
                                </td>
                                <td style="width: 11%;" class="pcs-itemtable-header lineitem-header text-align-right">
                                    Tax
                                </td>
                                <td style="width: 120px;" class="pcs-itemtable-header lineitem-header text-align-right">
                                    Amount
                                </td>
                            </tr>
                        </thead>
                        <tbody class="itemBody">
                            <tr>
                                <td valign="top" style="padding: 10px 0 10px 5px; text-align: center; word-wrap: break-word;"
                                    class="pcs-item-row">
                                    1
                                </td>
                                <td valign="top" style="padding: 10px 0px 10px 20px;" class="pcs-item-row">
                                    <div>
                                        <div>
                                            <span style="word-wrap: break-word;" id="tmp_item_name">Papers</span><br/>
                                            <span class="pcs-item-sku"></span>
                                            <br/>
                                            <span style="white-space: pre-wrap; word-wrap: break-word;" class="pcs-item-desc"
                                                id="tmp_item_description"></span>
                                        </div>
                                    </div>
                                </td>
                                <td class="pcs-item-row lineitem-column text-align-right">
                                    <span id="tmp_item_qty">1.00</span>
                                    <div class="pcs-item-desc">
                                        quantity</div>
                                </td>
                                <td class="pcs-item-row lineitem-column text-align-right">
                                    <span id="tmp_item_rate">100.00</span>
                                </td>
                                <td class="pcs-item-row lineitem-column lineitem-content-right text-align-right">
                                    <span id="tmp_item_tax_rate">12.50</span>
                                </td>
                                <td class="pcs-item-row lineitem-column lineitem-content-right text-align-right">
                                    <span id="tmp_item_tax_amount">12.50</span>
                                </td>
                                <td class="pcs-item-row lineitem-column lineitem-content-right text-align-right">
                                    <span id="tmp_item_amount">100.00</span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="width: 100%; margin-top: 1px;">
                        <div style="width: 45%; padding: 10px 10px 3px 3px; font-size: 9pt;" class="v-top total-number-section">
                            <div style="white-space: pre-wrap;">
                            </div>
                        </div>
                        <div style="width: 50%;" class="v-top total-section">
                            <table class="pcs-totals" cellspacing="0" border="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td class="total-section-label text-align-right">
                                            Sub Total
                                        </td>
                                        <td id="tmp_subtotal" class="total-section-value text-align-right">
                                            100.00
                                        </td>
                                    </tr>
                                    <tr style="height: 10px;">
                                        <td class="total-section-label text-align-right">
                                            vat (12.5%)
                                        </td>
                                        <td class="total-section-value text-align-right">
                                            12.50
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="total-section-label text-align-right">
                                            <b>Total</b>
                                        </td>
                                        <td id="tmp_total" class="total-section-value text-align-right">
                                            <b>₹112.50</b>
                                        </td>
                                    </tr>
                                    <tr style="height: 10px;">
                                        <td class="total-section-label text-align-right">
                                            Payment Made
                                        </td>
                                        <td style="color: red;" class="total-section-value text-align-right">
                                            (-) 112.50
                                        </td>
                                    </tr>
                                    <tr style="height: 40px;" class="pcs-balance">
                                        <td class="total-section-label text-align-right">
                                            <b>Balance Due</b>
                                        </td>
                                        <td id="tmp_balance_due" class="total-section-value text-align-right">
                                            <b>₹0.00</b>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div style="clear: both; margin-top: 50px; width: 100%;">
                        <label style="font-size: 10pt;" id="tmp_notes_label" class="pcs-label">
                            Notes</label><br/>
                        <p style="margin-top: 7px; white-space: pre-wrap; word-wrap: break-word;" class="pcs-notes">
                            Thanks for your business.</p>
                    </div>
                </div>
                <div class="pcs-template-footer">
                    <div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
