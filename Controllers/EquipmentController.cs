using Microsoft.AspNetCore.Mvc;
using EquipmentInventory.Models;
using EquipmentInventory.ViewModels;

namespace EquipmentInventory.Controllers;

public class EquipmentController : Controller
   {
       // Dữ liệu giả lập
       private static List<Equipment> _equipments = new List<Equipment>
       {
           new Equipment { Id = 1, Name = "ASUS ROG Zephyrus G14 (2024)", Category = "Laptop", Price = 45000000, StockQuantity = 5 },
           new Equipment { Id = 2, Name = "ZOTAC NVIDIA RTX 5070 Ti", Category = "GPU", Price = 22000000, StockQuantity = 10 }
       };

       // 1. Hiển thị danh sách
       public IActionResult Index()
       {
           return View(_equipments);
       }

       // 2. Form Tìm kiếm (GET)
       [HttpGet]
       public IActionResult Search(EquipmentSearchViewModel searchModel)
       {
           var result = _equipments.AsQueryable();

           if (!string.IsNullOrEmpty(searchModel.Keyword))
               result = result.Where(e => e.Name.Contains(searchModel.Keyword, StringComparison.OrdinalIgnoreCase));
               
           if (!string.IsNullOrEmpty(searchModel.Category))
               result = result.Where(e => e.Category == searchModel.Category);

           if (searchModel.MinPrice.HasValue)
               result = result.Where(e => e.Price >= searchModel.MinPrice.Value);

           ViewBag.SearchModel = searchModel;
           return View(result.ToList());
       }

       // 3. Hiển thị Form Thêm mới (GET)
       [HttpGet]
       public IActionResult Create()
       {
           return View();
       }

       // 4. Xử lý Form Thêm mới khi bấm Submit (POST)
       [HttpPost]
       public IActionResult Create(EquipmentCreateViewModel model)
       {
           if (ModelState.IsValid)
           {
               var newId = _equipments.Max(e => e.Id) + 1;
               _equipments.Add(new Equipment
               {
                   Id = newId,
                   Name = model.Name,
                   Category = model.Category,
                   Price = model.Price,
                   StockQuantity = model.StockQuantity
               });

               TempData["SuccessMessage"] = $"Đã thêm thiết bị {model.Name} thành công!";
               return RedirectToAction("Index"); // Thêm xong thì quay về trang danh sách
           }
           return View(model); // Nếu lỗi thì hiện lại form kèm thông báo lỗi
       }
   }

// reviewed