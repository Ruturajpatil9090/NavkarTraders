using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Data;

public partial class pgeSignUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fu1.HasFile)
            {
                string fileName = Path.GetFileName(fu1.PostedFile.FileName);
                fu1.SaveAs(Server.MapPath("~/Images/" + fileName));

                using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                {
                    string retVal = "";
                    obj.flag = 1;
                    obj.tableName = "tblSign";
                    obj.columnNm = "ID,ImageName,ImagePath,Company_Code";
                    obj.values = "'" + 1 + "','" + fileName + "','" + "~/Images/" + fileName + "','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                    DataSet ds = new DataSet();
                    ds = obj.insertAccountMaster(ref retVal);
                    //Response.Redirect("~/Sugar/pgeUploadSignature.aspx", false);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}