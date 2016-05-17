using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Master_Default : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public event ddlSport_Delegate ddlSport_Changed;

    protected void ddlSport_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSport_Changed(sender, e);
    }
}

public delegate void ddlSport_Delegate(object sender, EventArgs e);