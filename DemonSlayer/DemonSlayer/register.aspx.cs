using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lesson9
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            BusinessRules.CUser objUser = new BusinessRules.CUser();

            objUser.register(txtUsername.Text, txtPassword.Text, rblRole.SelectedValue);
            Response.Redirect("login.aspx", true);
        }
    }
}