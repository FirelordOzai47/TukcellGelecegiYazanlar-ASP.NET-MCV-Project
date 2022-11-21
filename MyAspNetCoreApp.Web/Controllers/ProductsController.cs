using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private IHelper _helper;
        private readonly ProductRepository _productRepository;
        public ProductsController(AppDbContext context, IHelper helper)
        {
            _productRepository = new ProductRepository();
            _context = context;
            _helper = helper;
        }
        public IActionResult Index([FromServices]IHelper helper2)
        {
            var text = "Asp.Net";
            var upperText = _helper.Upper(text);
            var status = _helper.Equals(helper2);

            var products = _context.Products.ToList();

            return View(products);
        }

        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(Product newProduct)
        {
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            //Product newProduct = new Product() { Name = Name, Price = Price, Stock = Stock, Color = Color };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün Başarıyla Eklendi";
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product updateProduct,int productId)
        {
            updateProduct.Id = productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün Başarıyla Güncellendi";
            return RedirectToAction("Index");

        }
    }
}
