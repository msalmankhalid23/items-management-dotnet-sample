using ItemsManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemsManagementWebApp.Controllers
{
    public class ItemController : Controller
    {
        /// <summary>
        /// List to contains the Items
        /// </summary>
        private static List<Item> items = new List<Item>(); 

        public ActionResult Index()
        {
            return View(items);
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Press create button to Add Items.";
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item newItem)
        {
            if (ModelState.IsValid)
            {
                // Process the new item
                newItem.Id = items.Count + 1;
                items.Add(newItem);

                // Return the new item as JSON
                return Json(newItem);
            }

            // For non-AJAX requests, return the regular view
            return View(newItem);
        }

        public ActionResult Edit(int id)
        {
            //var itemToEdit = items.FirstOrDefault(item => item.Id == id);
            //if (itemToEdit == null)
            //{
            //    return HttpNotFound();
            //}
            return View(id);
        }

        [HttpPost]
        public ActionResult Edit(Item updatedItem)
        {
            var existingItem = items.FirstOrDefault(item => item.Id == updatedItem.Id);
            if (existingItem == null)
            {
                return HttpNotFound();
            }

            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult Delete(int id)
        {
            var itemToDelete = items.FirstOrDefault(item => item.Id == id);
            if (itemToDelete == null)
            {
                return HttpNotFound();
            }
            items.Remove(itemToDelete);
            return RedirectToAction("Index");
        }
    }
}