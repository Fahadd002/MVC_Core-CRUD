using Evidence_08_Fahad.Models;
using Evidence_08_Fahad.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evidence_08_Fahad.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductDbContext db;
        private readonly IWebHostEnvironment env;
        public ProductsController(ProductDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Products.Include(x => x.Sales).OrderBy(x => x.ProductId).ToListAsync());
        }
        public IActionResult Create()
        {
            var model = new ProductInputModel();
            model.Sales.Add(new Sale());
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductInputModel model, string act = "")
        {
            if (act == "add")
            {
                model.Sales.Add(new Sale());
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.RawValue = null;
                }

            }
            if (act.StartsWith("remove"))
            {

                int index = int.Parse(act.Substring(act.IndexOf('_') + 1));
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.RawValue = null;
                }
                model.Sales.RemoveAt(index);
            }
            if (act == "insert")
            {
                var product = new Product
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    ExpireDate = model.ExpireDate
                };
                string ext = Path.GetExtension(model.Picture.FileName);
                string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                string savePath = Path.Combine(this.env.WebRootPath, "Pictures", fileName);
                FileStream fs = new FileStream(savePath, FileMode.Create);
                await model.Picture.CopyToAsync(fs);
                fs.Close();
                product.Picture = fileName;
                foreach (var s in model.Sales)
                {
                    product.Sales.Add(s);
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var p = await db.Products.Include(x => x.Sales).FirstOrDefaultAsync(x => x.ProductId == id);
            if (p == null)
            {
                return NotFound();
            }
            var model = new ProductEditMoel
            {
                ProductId = id,
                ProductName = p.ProductName,
                Price = p.Price,
                ExpireDate = p.ExpireDate,
                Sales = p.Sales.ToList(),
            };
            ViewBag.CurrentPic = p.Picture;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditMoel model, string act = "")
        {
            var p = await db.Products.Include(x => x.Sales).FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
            if (p == null) { return NotFound(); }
            if (act == "add")
            {
                model.Sales.Add(new Sale());
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.RawValue = null;
                }

            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.RawValue = null;
                }
                model.Sales.RemoveAt(index);
            }
            if (act == "update")
            {
                p.ProductName = model.ProductName;
                p.Price = model.Price;
                p.ExpireDate = model.ExpireDate;
                if (model.Picture != null)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(this.env.WebRootPath, "Pictures", fileName);
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    await model.Picture.CopyToAsync(fs);
                    fs.Close();
                    p.Picture = fileName;
                }
                db.Sales.RemoveRange(p.Sales.ToList());
                p.Sales.Clear();
                foreach (var s in model.Sales)
                {
                    db.Sales.Add(new Sale { SaleDate = s.SaleDate, ProductId = p.ProductId, Quantity = s.Quantity });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPic = p.Picture;
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = new Product { ProductId = id };
            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            db.SaveChanges();
            return Json(new { success = true, msg = "Data deleted" });
        }
    }
}
