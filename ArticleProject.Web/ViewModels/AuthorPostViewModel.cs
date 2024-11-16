using System.ComponentModel.DataAnnotations;

public class AuthorPostViewModel
{
    [Display(Name = "الرقم التعريفي")]
    public int Id { get; set; }
    [Display(Name = "معرف المستخدم")]
    public string? UserId { get; set; }
    [Display(Name = "اسم المستخدم")]
    public string? UserName { get; set; }
    [Display(Name = "الاسم الكامل")]
    public string? FullName { get; set; }

    [Display(Name = "صورة المنشور")]
    public IFormFile? PostImageUrl { get; set; }
    [Display(Name = "صورة المنشور")]
    public string? PostImageName { get; set; }
    [Display(Name = "فئة المنشور")]
    public string? PostCategory { get; set; }

    [Required(ErrorMessage = "عنوان المنشور مطلوب")]
    [Display(Name = "عنوان المنشور")]
    public string PostTitle { get; set; }

    [Display(Name = "وصف المنشور")]
    [Required(ErrorMessage = "وصف المنشور مطلوب")]
    public string PostDescription { get; set; }

    [Display(Name = "تاريخ الإنشاء")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int AuthorId { get; set; }
    [Display(Name = "رقم الفئة التعريفي")]
    public int CategoryId { get; set; }
}
