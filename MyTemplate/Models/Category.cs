using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTemplate.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The (0) must be at least {2} and at max{1} characters long.", MinimumLength = 3)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "")]
        public string Slug { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Parent Name")]
        public int? ParentCategoryId { set; get; }

        //danh sach category con
        public List<Category> CategoryChildren { get; set; }

        //category cha
        [ForeignKey("ParentCategoryId")]
        [Display(Name = "Parent Name")]
        public Category ParentCategory { get; set; }
    }
}