using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab3_solution
{
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                getUsers();
            }
        }

        protected void getUsers()
        {
            BusinessRules.CUser objUser = new BusinessRules.CUser();

            gvUsers.DataSource = objUser.getUsers();
            gvUsers.DataBind();

            if (HttpContext.Current.User.IsInRole("User"))
            {
                gvUsers.Columns[3].Visible = false;
            }
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BusinessRules.CUser objUser = new BusinessRules.CUser();

            objUser.deleteUser(Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Values["UserID"].ToString()));
            getUsers();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Attributes.Add("onclick", "return confirm('Are you sure?');");
        }

       
    }
}