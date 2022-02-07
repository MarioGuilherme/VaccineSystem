using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineSystem.Models {
    [Table("vaccines")]
    public class Vaccine {
        [Key]
        [Column("id_vaccine")]
        [Display(Name = "Código da Vacina")]
        public int id_vaccine { get; set; }

        [Column("id_person")]
        [Display(Name = "Nome da pessoa")]
        [Required(ErrorMessage = "Selecione uma pessoa!")]
        public int id_person { get; set; }

        [Column("vaccine_name")]
        [StringLength(60)]
        [Display(Name = "Nome da Vacina")]
        [Required(ErrorMessage = "O nome da Vacina é obrigatório!")]
        public string vaccine_name { get; set; }

        [Column("vaccine_value")]
        [Display(Name = "Valor da vacina")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "O valor da Vacina é obrigatório!")]
        public decimal vaccine_value { get; set; }

        [Column("vaccine_date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy à\\s\\ hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da vacina")]
        [Required(ErrorMessage = "A data da vacina é obrigatória!")]
        public DateTime vaccine_date { get; set; }

        public virtual Person Person { get; set; }
    }
}