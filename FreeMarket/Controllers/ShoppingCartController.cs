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

        public ActionResult GetCourierData(int id, int supplierNumber, int quantity, int addressNumber)
        {
            // Prepare
            CourierFeeViewModel model = new CourierFeeViewModel();
            string userId = User.Identity.GetUserId();
            bool anonymousUser = (userId == null);

            if (anonymousUser)
            {

            }
            else
            {
                // Validate
                using (FreeMarketEntities db = new FreeMarketEntities())
                {
                    Product product = db.Products.Find(id);
                    Supplier supplier = db.Suppliers.Find(supplierNumber);

                    if (product == null || supplier == null)
                        return RedirectToAction("Index", "Product");
                }

                model = new CourierFeeViewModel(id, supplierNumber, quantity, userId, addressNumber);

                SetNoCharge(userId, model);
            }

            return PartialView("_CourierData", model);
        }

        public ActionResult CourierSelectionModal(int id, int supplierNumber, int quantity, int addressNumber = 0)
        {
            // Prepare
            CourierFeeViewModel model = new CourierFeeViewModel();
            string userId = User.Identity.GetUserId();
            string defaultAddressName = "";
            bool anonymousUser = (userId == null);

            if (anonymousUser)
            {

            }
            else
            {
                // Validate
                using (FreeMarketEntities db = new FreeMarketEntities())
                {
                    Product product = db.Products.Find(id);
                    Supplier supplier = db.Suppliers.Find(supplierNumber);

                    if (product == null || supplier == null)
                        return RedirectToAction("Index", "Product");
                }

                ShoppingCart cart = GetCartFromSession(userId);

                if (addressNumber == 0)
                {
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var currentUser = manager.FindById(User.Identity.GetUserId());
                    defaultAddressName = currentUser.DefaultAddress;

                    model = new CourierFeeViewModel(id, supplierNumber, quantity, userId, defaultAddressName);
                }
                else
                {
                    model = new CourierFeeViewModel(id, supplierNumber, quantity, userId, addressNumber);
                }

                SetNoCharge(userId, model);
            }

            return PartialView("_CourierSelectionModal", model);
        }

        public ActionResult CourierSelectionModalFromCart(int id, int supplierNumber, int quantity, string addressString = null)
        {
            // Prepare
            CourierFeeViewModel model = new CourierFeeViewModel();
            string userId = User.Identity.GetUserId();
            string defaultAddressName = "";
            bool anonymousUser = (userId == null);

            if (anonymousUser)
            {

            }
            else
            {
                // Validate
                using (FreeMarketEntities db = new FreeMarketEntities())
                {
                    Product product = db.Products.Find(id);
                    Supplier supplier = db.Suppliers.Find(supplierNumber);

                    if (product == null || supplier == null)
                        return RedirectToAction("Index", "Product");
                }

                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                defaultAddressName = currentUser.DefaultAddress;

                model = new CourierFeeViewModel(id, supplierNumber, quantity, userId, defaultAddressName, addressString);

                SetNoCharge(userId, model);

                // Set a boolean to show that the modal is being accessed from the shopping cart page.
                model.FromCart = true;
            }

            return PartialView("_CourierSelectionModal", model);
        }

        public void SetNoCharge(string userId, CourierFeeViewModel model)
        {
            ShoppingCart cart = GetCartFromSession(userId);

            foreach (OrderDetail temp in cart.Body.OrderDetails)
            {
                foreach (CourierFee info in model.FeeInfo)
                {
                    if (info.CustodianNumber == temp.CustodianNumber && info.CourierNumber == temp.CourierNumber)
                    {
                        info.NoCharge = true;
                    }
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(CourierFeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Validate
                if (viewModel.ProductNumber == 0 || viewModel.SupplierNumber == 0 || viewModel.SelectedCourierNumber == 0)
                {
                    TempData["errorMessage"] = "Error: We could not add the item to the cart.";
                    return JavaScript("window.location = window.location.href;");
                }

                // Prepare
                string userId = User.Identity.GetUserId();
                ShoppingCart cart = GetCartFromSession(userId);

                // If this request comes from the ShoppingCart page then do not display quantity on the modal
                if (viewModel.FromCart)
                    viewModel.Quantity = 0;

                int custodian = viewModel.FeeInfo
                    .Where(c => c.CourierNumber == viewModel.SelectedCourierNumber)
                    .Select(c => c.CustodianNumber)
                    .FirstOrDefault();

                bool noCharge = viewModel.FeeInfo
                    .Where(c => c.CourierNumber == viewModel.SelectedCourierNumber)
                    .FirstOrDefault()
                    .NoCharge;

                FreeMarketObject result;
                result = cart.AddItemFromProduct(viewModel.ProductNumber, viewModel.SupplierNumber, viewModel.SelectedCourierNumber, viewModel.SelectedAddress, viewModel.Quantity, custodian, noCharge, userId);

                if (result.Result == FreeMarketResult.Success)
                {
                    // New item added
                    if (result.Argument != null)
                    {
                        // If the modal was called from the shopping cart page show a different message.
                        if (viewModel.FromCart)
                            TempData["message"] = string.Format("Success: Delivery address updated for {0}.", ((Product)(result.Argument)).Description);
                        else
                            TempData["message"] = string.Format("Success: {0} ({1}) has been added to your cart.", ((Product)(result.Argument)).Description, viewModel.Quantity);
                    }
                }
                else
                    TempData["errorMessage"] = "Error: We could not add the item to the cart.";

                return JavaScript("window.location = window.location.href;");
            }
            // Validation Error
            else
                return PartialView("_CourierSelectionModal", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(ShoppingCart cart, string returnUrl)
        {
            // Prepare
            string userId = User.Identity.GetUserId();
            ShoppingCart sessionCart = GetCartFromSession(userId);
            ShoppingCartViewModel model;

            if (ModelState.IsValid)
            {
                // Prepare
                FreeMarketObject resultRemove = new FreeMarketObject();
                FreeMarketObject resultQuantity = new FreeMarketObject();

                // Remove selected Items
                List<OrderDetail> selectedItems = cart.Body.OrderDetails
                    .Where(c => c.Selected || c.Quantity <= 0)
                    .ToList();

                if (selectedItems.Count > 0)
                {
                    foreach (OrderDetail detail in selectedItems)
                    {
                        // Find an order detail which is not selected, has a courier fee of zero and a matching custodian and courier number
                        // to to the selected item. If an item is found the courier fee of the selected item is moved to the match
                        // so that it no longer has a courier fee of zero. This is to prevent a customer from getting free delivery
                        // by accident.
                        if (sessionCart.Body.OrderDetails
                            .Where(c => !c.Selected && c.CustodianNumber == detail.CustodianNumber
                                && c.CourierNumber == detail.CourierNumber && c.CourierFee == 0)
                            .FirstOrDefault() != null)
                        {
                            OrderDetail temp = sessionCart.Body.OrderDetails
                                .Where(c => c.ProductNumber == detail.ProductNumber && c.SupplierNumber == detail.SupplierNumber)
                                .FirstOrDefault();

                            sessionCart.Body.OrderDetails
                                .Where(c => !c.Selected && c.CustodianNumber == detail.CustodianNumber
                                    && c.CourierNumber == detail.CourierNumber && c.CourierFee == 0)
                                .FirstOrDefault().CourierFee = temp.CourierFee;
                        }

                        resultRemove = sessionCart.RemoveItem(detail.ItemNumber, detail.ProductNumber, detail.SupplierNumber, userId);
                    }
                }

                // Update Quantity
                List<OrderDetail> changedItems = cart.Body.OrderDetails
                    .Where(c => !c.Selected && c.Quantity > 0)
                    .ToList();

                if (changedItems.Count > 0)
                    resultQuantity = sessionCart.UpdateQuantities(changedItems);

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
                        toReturn = address
                            .ToString();
                }
            }

            return Content(toReturn);
        }
    }
}