using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArticleProject.Web.ViewModels
{
    public class CategoryViewModel
    {
        [DisplayName("رقم التصنيف")]
        public int? Id { get; set; }
        [Required(ErrorMessage ="This Filed is Required")]
        [DisplayName("اسم التصنيف")]
        public string Name { get; set; }
    }
}
