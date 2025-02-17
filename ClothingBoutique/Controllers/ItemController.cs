using System.Linq;
using ClothingBoutique.Data;
using ClothingBoutique.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingBoutique.Controllers
{
    public class ItemController : Controller
    {   
        // ref to db
        private readonly ApplicationDbContext _context;
        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        // list all items
        public IActionResult Index()
        {
            return View(_context);
        }
        // must be logged in to access
        [Authorize]
        // list item detail by id
        public IActionResult Detail(int itemID)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == itemID);
            if(mItem != null)
            {
                return View(mItem);
            } else 
            {
                ViewData["error"] = "Item not Found";
                return View("Error");
            }
        }
        // must be a manager or employee to view the create form
        [Authorize(Roles="manager, employee")] 
        public IActionResult Create()
        {
            return View();
        }
        // add an item to the database and redirect to that items detail page if successful, otherwise re-render form with invalid data
        [HttpPost]
        public IActionResult Add(ItemModel nItem)
        {
            if(ModelState.IsValid)
            {
                _context.items.Add(nItem);
                _context.SaveChanges();
                return RedirectToAction("Detail", new{itemID = nItem.id});
            } else 
            {
                return View("Create", nItem);
            }
        }
        // must be a manager or employee to access the populated edit form
        [Authorize(Roles="manager, employee")]
        public IActionResult Edit(int itemID)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == itemID);
            return View(mItem);
        }
        [HttpPost]
        // update item in db by id
        public IActionResult Update(ItemModel uItem)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == uItem.id);
            if(mItem != null)
            {
                if(ModelState.IsValid)
                {
                    mItem.itemName = uItem.itemName;
                    mItem.itemCategory = uItem.itemCategory;
                    mItem.inStock = uItem.inStock;
                    mItem.price = uItem.price;
                    mItem.featured = uItem.featured;
                    _context.SaveChanges();
                    return RedirectToAction("Detail",new{itemID = uItem.id});
                } else
                {
                    return View("Edit", uItem);
                }
            } else 
            {
                ViewData["error"] = "Item not Found";
                return View("Error");
            }
        }
        // must be a manager to view the delete confirmation page
        [Authorize(Roles="manager")]
        public IActionResult Confirmation(int itemID)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == itemID);
            return View(mItem);
        }
        [HttpGet]
        // remove item from db by id
        public IActionResult Delete(int itemID)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == itemID);

            _context.Remove(mItem);
            _context.SaveChanges();

            return Redirect("Index");
        }
    }
}