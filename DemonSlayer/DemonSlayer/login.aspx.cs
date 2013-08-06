using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;

namespace lesson9
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //hard-code logins into web.config
            //if (FormsAuthentication.Authenticate(txtUsername.Text, txtPassword.Text))
            //{
            //    FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, true);
            //}
            //else
            //{
            //    lblError.Visible = true;
            //}


            //implement custom role provider
            string role;

            BusinessRules.CUser objUser = new BusinessRules.CUser();
            role = objUser.login(txtUsername.Text, txtPassword.Text);

            if (role == "")
            {
                lblError.Visible = true;
            }
            else
            {

                // Create a new ticket used for authentication, credit Heath Stewart
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                   1, // Ticket version
                   txtUsername.Text, // Username associated with ticket
                   DateTime.Now, // Date/time issued
                   DateTime.Now.AddMinutes(30), // Date/time to expire
                   true, // "true" for a persistent user cookie
                   role, // User-data, in this case the roles
                   FormsAuthentication.FormsCookiePath);// Path cookie valid for

                // Encrypt the cookie using the machine key for secure transport
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash); 

                // Set the cookie's expiration time to the tickets expiration time
                if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                // Add the cookie to the list for outgoing response
                Response.Cookies.Add(cookie);

                // Don't call FormsAuthentication.RedirectFromLoginPage since it could replace the authentication ticket (cookie) we just added
                Response.Redirect("home.aspx", true);
            }

        }
    }
}