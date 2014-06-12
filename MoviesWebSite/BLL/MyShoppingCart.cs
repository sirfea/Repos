using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Security.Cryptography;

using MoviesWebSite.DAL;
using System.Web.Security;

using System.Diagnostics;

namespace MoviesWebSite.BLL
{

    public struct ShoppingCartUpdate
    {
        // To minimize the dependency of our logic on 
        // user interface we can define a data structure
        // that we can use to pass the shopping Cart items
        // without our method need to directly 
        // access the GridView
        public int MovieId;
        public int PurchaseQuantity;
        public bool RemoveItem;
    }

    public partial class MyShoppingCart:
        MoviesWebSite.MyShoppingCart
    {
        public const string CartId = "MyCartID";

        #region 1 - AddItem

        public void AddItem
            (string cartId, int movieId, int quantity)
        {
            // Using Entity Framework
            using (MoviesSiteDBEntities db =
                new MoviesSiteDBEntities())
            {
                try
                {
                    // This query will check to add 
                    // movie id based on cartid 
                    // in any case we will used FirstOrDefault()
                    // method that will return the first element
                    // of a squence or the default value if the 
                    // sequence contains no elements
                    var myItem = (from c in db.ShoppingCarts
                                  where c.CartID == cartId &&
                                  c.MovieID == movieId
                                  select c).FirstOrDefault();
                    if (myItem == null)
                    {
                        ShoppingCart cartAdd =
                            new ShoppingCart();
                        cartAdd.CartID = cartId;
                        cartAdd.Quantity = quantity;
                        cartAdd.MovieID = movieId;
                        cartAdd.DateCreated = DateTime.Now;
                        cartAdd.DateTime = DateTime.Now;
                        db.ShoppingCarts.Add(cartAdd);
                    }
                    else
                    {
                        myItem.Quantity += quantity;
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new
                        Exception("Error: Unable to " +
                        " add movie to cart" +
                        ex.Message.ToString());
                }
            }
        }

        #endregion

        #region 2 - GetShoppingCart

        public string GetShoppingCartSession()
        {
            if (Session[CartId] == null)
            {
                Session[CartId] =
                    HttpContext.Current.Request.IsAuthenticated ?
                    User.Identity.Name : Guid.NewGuid().ToString();
            }
            return Session[CartId].ToString();
        }



        #endregion

        #region 3 - GetTotal

        public decimal GetTotal(string cartId)
        {
            using (MoviesSiteDBEntities db =
                new MoviesSiteDBEntities())
            {
                decimal cartTotal = 0;
                try
                {
                    var myCart = from c in db.ViewCarts
                                 where c.CartID == cartId
                                 select c;

                    if (myCart.Count() > 0)
                    {
                        cartTotal = myCart.Sum(od =>
                            (decimal)od.Quantity *
                            (decimal)od.MovieCost);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Fail("Error...");

                    throw new Exception
                        ("Error: Unable to calculate total order"
                        + ex.Message.ToString(), ex);
                }
                return cartTotal;

            }

        }




        #endregion

        #region 4 - RemoveItem

        public void RemoveItem(string cartId, int movieId)
        {
            using (MoviesSiteDBEntities db =
                new MoviesSiteDBEntities())
            {
                try
                {
                    var myItem = (from c in db.ShoppingCarts
                                  where c.CartID == cartId
                                  && c.MovieID == movieId
                                  select c).FirstOrDefault();

                    if (myItem != null)
                    {
                        db.ShoppingCarts.Remove(myItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception
                        ("Error: Unable to remove item"
                        + ex.Message.ToString(), ex);
                }
            }

        }


        #endregion

        #region 5 - UpdateItem

        public void UpdateItem
            (string cartId, int movieId, int quantity)
        {
            using (MoviesSiteDBEntities db =
                new MoviesSiteDBEntities())
            {
                try
                {
                    var myItem = (from c in db.ShoppingCarts
                                  where c.CartID == cartId
                                  && c.MovieID == movieId
                                  select c).FirstOrDefault();

                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception
                        ("Error: Unable to Update item"
                        + ex.Message.ToString(), ex);
                }
            }

        }



        #endregion

        #region 6 - Update Shopping Cart DB

        public void UpdateShoppingCartDB(string cartId,
            ShoppingCartUpdate[] cartItemUpdate)
        {
            using (MoviesSiteDBEntities db =
                new MoviesSiteDBEntities())
            {
                try
                {
                    int CartItemCount = cartItemUpdate.Count();
                    var myCart = from c in db.ViewCarts
                                 where c.CartID == cartId
                                 select c;
                    // Iteration over the Grid
                    foreach (var myItem in myCart)
                    {
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (myItem.MovieID == cartItemUpdate[i].MovieId)
                            {
                                if (cartItemUpdate[i].PurchaseQuantity < 1
                                    || cartItemUpdate[i].RemoveItem == true)
                                {
                                    RemoveItem(cartId, myItem.MovieID);
                                }
                                else
                                {
                                    UpdateItem(cartId, myItem.MovieID,
                                        cartItemUpdate[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: Unable to update DB" + ex.Message.ToString());
                    throw;
                }
            }

        }



        #endregion

        #region 7 - Migrate Cart
        // Take old session(anonymous user) and migrate the info 
        // into new session
        public void MigrateCart(string oldCartId, string newCartId)
        {
            using (MoviesSiteDBEntities db =
                new MoviesSiteDBEntities())
            {
                try
                {
                    var myShopCart = from cart in db.ShoppingCarts
                                     where cart.CartID == oldCartId
                                     select cart;

                    foreach (ShoppingCart item in myShopCart)
                    {
                        item.CartID = newCartId;
                    }
                    db.SaveChanges();
                    Session[CartId] = newCartId;
                }
                catch (Exception ex)
                {
                    throw new Exception
                        ("Error: Unable to Migrate Shopping Cart" +
                        ex.Message.ToString(), ex);
                }

            }
        }




        #endregion

        #region 8 - Submit Order

        public bool SubmitOrder(string userName)
        {
            using (MoviesSiteDBEntities db =
                new MoviesSiteDBEntities())
            {
                try
                {
                    // Add new order record
                    Order newOrder = new Order();
                    newOrder.CustomerName = userName;
                    newOrder.OrderDate = DateTime.Now;                    
                    db.Orders.Add(newOrder);
                    db.SaveChanges();

                    // Create a new OrderDetails for each record
                    string cartId = GetShoppingCartSession();
                    var myCart = (from c in db.ViewCarts
                                  where c.CartID == cartId
                                  select c); //.FirstOrDefault();
                    foreach (ViewCart vc in myCart.ToList())
                    {
                        int i = 0;
                        if (i < 1)
                        {
                            OrderDetail od = new OrderDetail();
                            od.OrderID = newOrder.OrderID;
                            od.MovieID = vc.MovieID;
                            od.Quantity = vc.Quantity;
                            od.UnitCost = vc.MovieCost;
                            od.DateTime = DateTime.Now;
                            db.OrderDetails.Add(od);

                            i++;

                        }
                        var myItem = (from c in db.ShoppingCarts
                                      where c.CartID == vc.CartID
                                      && c.MovieID == vc.MovieID
                                      select c).FirstOrDefault();
                        if (myItem != null)
                        {
                            db.ShoppingCarts.Remove(myItem);
                        }
                        db.SaveChanges();
                    }
                }

                catch (Exception ex)
                {
                    throw new Exception
                        ("Error: Unable to Submit order" +
                        ex.Message.ToString(), ex);
                }

            }
            return (true);

        }




        #endregion

    }
}