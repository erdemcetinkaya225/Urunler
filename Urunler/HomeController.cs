using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Urunler.Models;
using Urunler.Models.DTO;
using Urunler.Models.ViewModel;
using System.Linq;
namespace Urunler
{
    public class HomeController : Controller
    {
        private readonly UrunlerContext _context;

        public HomeController (UrunlerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {

            try
            {



           
         var productList = from productdb in _context.Products
                               join category in _context.Categories on productdb.Category equals category.Id
                               select new ProductDTO() {
                                   id = productdb.Id,
                                   category = category.Name,
                                   name=productdb.Name,
                                   createdDate = productdb.CreatedDate.ToString()
                               };

                List<ProductDTO> product = new List<ProductDTO>();
                foreach (var item in productList)
                {
                    ProductDTO productDTO = new ProductDTO();
                    productDTO.name = item.name;
                    productDTO.createdDate = item.createdDate;
                    productDTO.category = item.category;
                    productDTO.id = item.id;
                    product.Add(productDTO);
                }
                return Json(new { data= product,StatusCode = 200,status=true,message="İşlem Başarılı"});
            }
            catch(Exception e)
            {
                return Json(new { StatusCode = 404,status=false, message = e.Message });
            }
          
        }
        public JsonResult AddProduct(AddProductViewModel model)
        {

            try
            {
                if (string.IsNullOrEmpty(model.name))
                {

                    return Json(new {status=false,StatusCode=200, message = "Eksik veri girişi yaptınız lütfen kontrol ediniz" });
                }
                Product product = new Product();
                product.Category = model.category;
                product.Name = model.name;
                product.CreatedDate = DateTime.Now;
                _context.Products.Add(product);
                _context.SaveChanges();

                return Json(new { status = true, StatusCode = 200,message="İşlem Başarılı" });
            }
            catch (Exception)
            {

                return Json(new { status = true, StatusCode = 200, message = "Bir Sorun Oluştu" });
            }
           
        }
        public JsonResult AddCategory(AddCategoryViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.name))
                {
                    return Json(new { status = false, StatusCode = 200, message = "Eksik veri girişi yaptınız lütfen kontrol ediniz" });

                }
                var category = _context.Categories.FirstOrDefault(x=>x.Name==model.name);
               if(category!=null)
                {

                    return Json(new { status = false, StatusCode = 200, message = "Bu isimle daha önce kategori kaydı yapılmış" });

                }
                Category categoryies = new Category();
                categoryies.Name = model.name;
                _context.Categories.Add(categoryies);
                _context.SaveChanges();

                return Json(new { status = false, StatusCode = 200, message = "İşlem Başarılı" });

            }
            catch (Exception)
            {

                
            }
            return null;
        }
        public JsonResult CategoryList()
        {


            try
            {
               List <CategoryDTO> categories = new List<CategoryDTO>();
              var CategoryList  = _context.Categories.ToList();
                foreach (var item in CategoryList)
                {
                    CategoryDTO category = new CategoryDTO();
                    category.Id = item.Id;
                    category.Name = item.Name;
                    categories.Add(category);
                }

                return Json(new {data=categories,StatusCode=200,status=true,message="İşlem Başarılı" });
            }
            catch (Exception)
            {

                return Json(new {StatusCode = 404, status = false, message = "Bir Sorun Oluştu" });
            }

           
        }
    }
}
