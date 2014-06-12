using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// This is for using IOrderedDictionary
using System.Collections.Specialized;

namespace MoviesWebSite
{
    public partial class MyShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MoviesWebSite.BLL.MyShoppingCart userShoppingCart =
                new MoviesWebSite.BLL.MyShoppingCart();

            string cartId =
                userShoppingCart.GetShoppingCartSession();
            decimal cartTotal = 0;
            cartTotal = userShoppingCart.GetTotal(cartId);
            if (cartTotal > 0)
            {
                lblTotal.Text = string.Format("{0:c}",
                userShoppingCart.GetTotal(cartId));
            }
            else
            {
                lblTotalOrder.Text = "";
                lblTotal.Text = "";
                shoppingCartTitle.InnerText =
                    "Shopping Cart is Empty";
                btnUpdate.Visible = false;
                btnChcekout.Visible = false;
            }

        }

        protected void btnUpdate_Click
            (object sender, ImageClickEventArgs e)
        {
            // Connectiong to BLL
            MoviesWebSite.BLL.MyShoppingCart userShoppingCart =
                new BLL.MyShoppingCart();

            // Get the session
            string cartId =
                userShoppingCart.GetShoppingCartSession();

            // Creating an array of data structure
            MoviesWebSite.BLL.ShoppingCartUpdate[] cartUpdate =
                new MoviesWebSite.BLL.ShoppingCartUpdate
                    [grvDetails.Rows.Count];

            // Use IOrderedDictionary interface that will
            // enabling us to create an index based on 
            // key/value pair
            for (int i = 0; i < grvDetails.Rows.Count; i++)
            {
                IOrderedDictionary rowValues = new OrderedDictionary();
                rowValues = GetValues(grvDetails.Rows[i]);
                cartUpdate[i].MovieId =
                    Convert.ToInt32(rowValues["MovieID"]);
                cartUpdate[i].PurchaseQuantity =
                    Convert.ToInt32(rowValues["Quantity"]);

                CheckBox cbRemove = new CheckBox();

                cbRemove = (CheckBox)grvDetails.Rows[i].
                    FindControl("chkRemove");

                cartUpdate[i].RemoveItem = cbRemove.Checked;

            }
            // BLL - build the Update for shopping cart
            // Update shopping Cart
            userShoppingCart.UpdateShoppingCartDB(cartId, cartUpdate);
            grvDetails.DataBind();
            lblTotal.Text = string.Format("{0:c}",
                userShoppingCart.GetTotal(cartId));


        }

        public static IOrderedDictionary
            GetValues(GridViewRow rows)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in rows.Cells)
            {
                if (cell.Visible)
                {
                    // Extract data from cell
                    cell.ContainingField.
                        ExtractValuesFromCell
                        (values, cell, rows.RowState, true);
                }
            }
            return values;
        }        
    }
}