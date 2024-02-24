using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }

    [System.Web.Services.WebMethod] //This webattribute is used to specify that its a webmethod
    //public and static keywords are used here so that the javascript can access this method without any restriction
    public static List<row> GetDynamicRows()
    {
        List<row> lstRows = new List<row>();
        lstRows.Add(new row() { Id = 1, name = "Rishi" });
        lstRows.Add(new row() { Id = 2, name = "Maruti" });
        lstRows.Add(new row() { Id = 3, name = "kesarkar" });
        lstRows.Add(new row() { Id = 4, name = "mhghggg" });
        return lstRows;
    }

    public class row
    {
        public int Id { get; set; }
        public string name { get; set; }

    }
}