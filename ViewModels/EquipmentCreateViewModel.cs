using System.ComponentModel.DataAnnotations;

namespace EquipmentInventory.ViewModels;

public class EquipmentCreateViewModel
{
    [Required(ErrorMessage = "Tên thiết bị không được để trống")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Danh mục không được để trống")]
    public string Category { get; set; }

    [Range(1, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn không được âm")]
    public int StockQuantity { get; set; }
}