using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MoviesWebSite.DAL;

namespace MoviesWebSite
{
    public partial class Checkout : System.Web.UI.Page
    {
        decimal cartTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckoutHeader.InnerText = "Your Shopping Cart is Empty";
            lblCartHeader.Text = "";
            btnChceckout.Visible = false;
            
        }

        protected void grvChckout_RowDataBound
            (object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ViewCart myCart = new ViewCart();
                myCart = (ViewCart)e.Row.DataItem;

                cartTotal += myCart.MovieCost * myCart.Quantity;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (cartTotal > 0)
                {
                    CheckoutHeader.InnerText = "Review and Submit Order";
                    lblCartHeader.Text =
                        "Please review your order information below:";
                    btnChceckout.Visible = true;
                    e.Row.Cells[4].Text = "Total: " + cartTotal.ToString();
                }
            }
        }

        protected void btnChceckout_Click
            (object sender, ImageClickEventArgs e)
        {
            MoviesWebSite.BLL.MyShoppingCart userShoppingCart =
                new MoviesWebSite.BLL.MyShoppingCart();

            if (userShoppingCart.SubmitOrder
                (User.Identity.Name) == true)
            {
                CheckoutHeader.InnerText = "Thank You";
                Message.Visible = false;
                btnChceckout.Visible = false;
            }
            else
            {
                CheckoutHeader.InnerText = "Order faild try again";
            }
        }
    }
}