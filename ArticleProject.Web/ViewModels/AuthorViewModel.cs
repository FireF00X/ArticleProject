using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArticleProject.Web.ViewModels
{
    public class AuthorViewModel
    {
        [Required]
        [DisplayName("الرقم التعريفي للناشر")]
        public int Id { get; set; }
        [Required]
        [DisplayName("الرقم التعريفي للمستخدم")]
        public string UserId { get; set; }
        [DisplayName("أسم المستخدم")]
        [Required]
        public string UserName { get; set; }
        [DisplayName("الأسم الكامل")]
        [Required]
        public string FullName { get; set; }
        [DisplayName("الصورة")]
        public string PictureUrl { get; set; }
        [DisplayName("الصورة")]
        public IFormFile PictureFromUser { get; set; }
        [DisplayName("السيرة الذاتية")]
        public string Bio { get; set; }
        [DisplayName("حساب الفيسبوك")]
        public string? FaceBookLink { get; set; }
        [DisplayName("حساب الانسجرام")]
        public string? InstegramLink { get; set; }
        [DisplayName("حساب منصة X")]
        public string? XSiteLink { get; set; }
    }
}
