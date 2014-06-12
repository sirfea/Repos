using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MoviesWebSite.BLL;

namespace MoviesWebSite
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rowId = Request.QueryString["MovieID"];
            int movieId;
            if (!string.IsNullOrEmpty(rowId) &&
                int.TryParse(rowId, out movieId))
            {
                MoviesWebSite.BLL.MyShoppingCart
                    userShoppingCart =
                    new MoviesWebSite.BLL.MyShoppingCart();
                string cartId = userShoppingCart.
                    GetShoppingCartSession();
                userShoppingCart.AddItem(cartId, movieId, 1);
            }
            else
            {
                throw new Exception("Error: Cant load Add To Cart " +
                    "page without movie Id.");
            }
            Response.Redirect("MyShoppingCart.aspx");

        }
    }
}