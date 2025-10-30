using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDPIAS_STORE.Models.headStoreModel
{
    [Table("main_chemical_storage")]
    public class MainChemicalStorage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int head_id { get; set; }

        [Required]
        public int chemical_id { get; set; }

        [Required]
        public int measurement_unit_id { get; set; }

        [Required]
        [StringLength(10)]
        public string original_quantity { get; set; }

        [Required]
        [StringLength(10)]
        public string current_quantity { get; set; }

        [Required]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Required]
        public DateTime expiry_date { get; set; }

        [Required]
        public DateTime manufacture_date { get; set; }

        [Required]
        [StringLength(50)]
        public string molecular_weight { get; set; }

        [Required]
        [StringLength(50)]
        public string storage_condition { get; set; }

        [Required]
        public byte hazard_level { get; set; }

        [Required]
        [StringLength(150)]
        public string supplier_company_name { get; set; }

        [Required]
        public int chemical_type_id { get; set; }

        [Required]
        public int chemical_state_id { get; set; }
    }
}
