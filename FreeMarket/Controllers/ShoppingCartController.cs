﻿using FreeMarket.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace FreeMarket.Controllers
{
    [RequireHttps]
    public class ShoppingCartController : Controller
    {
        public const string sessionKey = "cart";
        public const string anonymous = "Anonymous";

        private ShoppingCart GetCartFromSession(string userId)
        {
            userId = userId ?? anonymous;

            ShoppingCart tempCart = null;

            if (Session != null)
                tempCart = (ShoppingCart)Session[sessionKey];

            if (tempCart == null)
            {
                Debug.Write(string.Format("\nCreating Cart for user {0} ...", userId));

                if (userId == anonymous)
                {
                    tempCart = new ShoppingCart();
                }
                else
                {
                    tempCart = new ShoppingCart(userId);
                }

                Session[sessionKey] = tempCart;
            }

            return tempCart;
        }

        public ActionResult Cart()
        {
            string userId = User.Identity.GetUserId();

            ShoppingCart cart = GetCartFromSession(userId);
            ShoppingCartViewModel model = new ShoppingCartViewModel();
            model = new ShoppingCartViewModel() { Cart = cart, ReturnUrl = Url.Action("Index", "Product") };

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult CartTotals(ShoppingCart cart)
        {
            return PartialView("_CartTotals", cart);
        }

        public ActionResult CourierSelectionModal(int id, int supplierNumber, int quantity)
        {
            CourierFeeViewModel model = new CourierFeeViewModel();

            string userId = User.Identity.GetUserId();
            string defaultAddressName = "";

            bool anonymousUser = (userId == null);

            if (anonymousUser)
            {

            }
            else
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                defaultAddressName = currentUser.DefaultAddress;

                if (id == 0 || supplierNumber == 0)
                    return RedirectToAction("Index", "Product");

                using (FreeMarketEntities db = new FreeMarketEntities())
                {
                    Product product = db.Products.Find(id);
                    Supplier supplier = db.Suppliers.Find(supplierNumber);

                    if (product == null || supplier == null)
                        return RedirectToAction("Index", "Product");

                    model = new CourierFeeViewModel(id, supplierNumber, quantity, userId, defaultAddressName);
                }
            }

            return PartialView("_CourierSelectionModal", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(CourierFeeViewModel viewModel)
        {
            if (viewModel.ProductNumber == 0 || viewModel.SupplierNumber == 0 || viewModel.SelectedCourierNumber == 0)
            {
                TempData["errorMessage"] = "Error: We could not add the item to the cart.";
                return JavaScript("window.location = window.location.href;");
            }

            string userId = User.Identity.GetUserId();
            ShoppingCart cart = GetCartFromSession(userId);

            FreeMarketObject result;
            result = cart.AddItemFromProduct(viewModel.ProductNumber, viewModel.SupplierNumber, viewModel.SelectedCourierNumber, viewModel.SelectedAddress, viewModel.Quantity, userId);

            if (result.Result == FreeMarketResult.Success)
            {
                // New item added
                if (result.Argument != null)
                {
                    TempData["message"] = string.Format("Success: {0} ({1}) has been added to your cart.", ((Product)(result.Argument)).Description, viewModel.Quantity);
                }
            }
            else
            {
                TempData["errorMessage"] = "Error: We could not add the item to the cart.";
            }

            return JavaScript("window.location = window.location.href;");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(ShoppingCart cart, string returnUrl)
        {
            string userId = User.Identity.GetUserId();
            ShoppingCart sessionCart = GetCartFromSession(userId);
            ShoppingCartViewModel model;

            if (ModelState.IsValid)
            {
                FreeMarketObject resultRemove = new FreeMarketObject();
                FreeMarketObject resultQuantity = new FreeMarketObject();

                // Remove Items

                List<OrderDetail> selectedItems = cart.Body.OrderDetails
                    .Where(c => c.Selected || c.Quantity <= 0)
                    .ToList();

                if (selectedItems.Count > 0)
                {
                    foreach (OrderDetail detail in selectedItems)
                    {
                        resultRemove = sessionCart.RemoveItem(detail.ItemNumber, detail.ProductNumber, detail.SupplierNumber, userId);
                    }
                }

                // Update Quantity

                List<OrderDetail> changedItems = cart.Body.OrderDetails
                    .Where(c => !c.Selected && c.Quantity > 0)
                    .ToList();

                if (changedItems.Count > 0)
                {
                    resultQuantity = sessionCart.UpdateQuantities(changedItems);
                }

                sessionCart.Save();

                TempData["message"] = "Cart has been updated.";

                model = new ShoppingCartViewModel { Cart = sessionCart, ReturnUrl = returnUrl };

                return RedirectToAction("Cart", "ShoppingCart");
            }

            model = new ShoppingCartViewModel { Cart = sessionCart, ReturnUrl = returnUrl };

            return View("Cart", model);
        }

        public ActionResult GetAddress(int AddressNumber)
        {
            string toReturn = "";
            CustomerAddress address;

            if (AddressNumber != 0)
            {
                using (FreeMarketEntities db = new FreeMarketEntities())
                {
                    address = db.CustomerAddresses
                       .Where(c => c.AddressNumber == AddressNumber)
                       .FirstOrDefault();

                    if (address != null)
                        toReturn = address.ToString();
                }
            }

            return Content(toReturn);
        }

        public ActionResult GetDimensions(int productNumber)
        {
            string toReturn = "";
            Product product;

            if (productNumber != 0)
            {
                using (FreeMarketEntities db = new FreeMarketEntities())
                {
                    product = db.Products
                       .Where(c => c.ProductNumber == productNumber)
                       .FirstOrDefault();

                    if (product != null)
                        toReturn = string.Format("{0} {1}", product.Weight, product.Size);
                }
            }

            return Content(toReturn);
        }
    }
}