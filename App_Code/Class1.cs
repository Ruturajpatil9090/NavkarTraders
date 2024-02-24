using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class GridViewTemplate : ITemplate
{
    private DataControlRowType templateType;
    private string columnNameFriendly;
    private string columnNameData;
    private Control control;

    public GridViewTemplate(DataControlRowType type, string colNameFr, string colNameDt, Control con)
    {
        templateType = type;
        columnNameFriendly = colNameFr;
        columnNameData = colNameDt;
        control = con;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        switch (templateType)
        {
            case DataControlRowType.Header:
                {
                    Literal lc = new Literal();
                    lc.Text = columnNameFriendly;
                    container.Controls.Add(lc);
                    break;
                }
            case DataControlRowType.DataRow:
                {
                    Control field = control;
                    field.DataBinding += new EventHandler(this.field_DataBinding);
                    container.Controls.Add(field);
                    break;
                }
        }
    }

    private void field_DataBinding(Object sender, EventArgs e)
    {
        Control c = (Control)sender;
        GridViewRow row = (GridViewRow)c.NamingContainer;
        if (sender.GetType() == typeof(Label))
        {
            (c as Label).Text = DataBinder.Eval(row.DataItem, columnNameData).ToString();
            (c as Label).Font.Size = 7;
            (c as Label).Font.Name = "Arial";
        }
        else if (sender.GetType() == typeof(TextBox))
        {
            (c as TextBox).Text = DataBinder.Eval(row.DataItem, columnNameData).ToString();
            (c as TextBox).Font.Size = 7;
            (c as TextBox).Font.Name = "Arial";
        }
        else if (sender.GetType() == typeof(DropDownList))
        {
            (c as DropDownList).SelectedValue = DataBinder.Eval(row.DataItem, columnNameData).ToString();
            (c as DropDownList).Font.Size = 7;
            (c as DropDownList).Font.Name = "Arial";
        }
        else if (sender.GetType() == typeof(CheckBox))
        {
            (c as CheckBox).Checked = (bool)DataBinder.Eval(row.DataItem, columnNameData);
        }
    }
}