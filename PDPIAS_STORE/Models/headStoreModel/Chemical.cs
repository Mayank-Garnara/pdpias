using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDPIAS_STORE.Models.headStoreModel
{
    // Maps to your database table name (optional, if class name differs)
    [Table("chemical")]
    public class Chemical
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [Display(Name = "Chemical Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "The CAS Number is required.")]
        [StringLength(50)]
        [Display(Name = "CAS Number")]
        public string cas_number { get; set; }

        // This will likely store the file path or a base64 string
        [Required(ErrorMessage = "The image structure is required.")]
        [StringLength(50)]
        [Display(Name = "Image Structure")]
        public string image_structure { get; set; }

        [Required(ErrorMessage = "The molecular formula is required.")]
        [Display(Name = "Molecular Formula")]
        public string molicular_formula { get; set; }

        [Required(ErrorMessage = "The molecular weight is required.")]
        [StringLength(50)]
        [Display(Name = "Molecular Weight")]
        public string molicular_weight { get; set; }
    }
}