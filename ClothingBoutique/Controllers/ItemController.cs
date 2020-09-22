using System.Linq;
using ClothingBoutique.Data;
using ClothingBoutique.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingBoutique.Controllers
{
    public class ItemController : Controller
    {   
        private readonly ApplicationDbContext _context;
        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        // FIXME : Cannot render view
        public IActionResult Index()
        {
            return View(_context);
        }
        [Authorize]
        public IActionResult Detail(int itemID)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.price == itemID);
            return View(mItem);
        }
        [Authorize(Roles="manager")]
        // TODO : Missing functionality 
        // public IActionResult Create()
        // {

        // }
        [HttpPost]
        public IActionResult Add(ItemModel nItem)
        {
            _context.items.Add(nItem);
            _context.SaveChanges();
            return RedirectToAction("Detail", new{itemID = nItem.id});
        }
        [Authorize(Roles="manager, employee")]
        // FIXME : employee1 not able to access endpoint
        public IActionResult Edit(int itemID)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == itemID);
            return View(mItem);
        }
        [HttpPost]
        public IActionResult Update(ItemModel uItem)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == uItem.id);

            mItem.itemName = uItem.itemName;
            mItem.itemCategory = uItem.itemName;
            mItem.inStock = uItem.inStock;
            mItem.price = uItem.price;
            mItem.featured = uItem.featured;

            return RedirectToAction("Detail",new{itemID = uItem.id});
        }
        [Authorize(Roles="manager")]
        public IActionResult Confirmation(int itemID)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == itemID);
            return View();
        }
        [HttpDelete]
        public IActionResult Delete(int itemID)
        {
            ItemModel mItem = _context.items.FirstOrDefault(m => m.id == itemID);

            _context.Remove(mItem);

            return Redirect("Index");
        }
    }
}