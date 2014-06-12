using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;

namespace MoviesWebSite.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If the user is not submmiting his credentials
            // save refferce
            if (!Page.IsPostBack)
            {
                // This will take the current session 
                // of the anonymous user - the user IS NOT
                // logged in - so we want to preserve
                // the user shopping cart information based
                // on the URL
                if (Page.Request.UrlReferrer != null)
                {
                    Session["TempLoginReference"] =
                        Page.Request.UrlReferrer.ToString();
                }
            }
            // RegisterHyperLink.NavigateUrl = 
            // "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

            // User is Logged in so log him out
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/");
            }

        }

        protected void LoginUser_LoggedIn
            (object sender, EventArgs e)
        {
            MoviesWebSite.BLL.MyShoppingCart userShoppingCart =
                new MoviesWebSite.BLL.MyShoppingCart();
            string cartId = userShoppingCart.GetShoppingCartSession();
            userShoppingCart.MigrateCart(cartId, LoginUser.UserName);

            // Take the current session and refresh 
            // the session with new info 
            if (Session["TempLoginReference"] != null)
            {
                Response.Redirect
                    (Session["TempLoginReference"].ToString());
            }
            Session["UserName"] = LoginUser.UserName;


        }
    }
}