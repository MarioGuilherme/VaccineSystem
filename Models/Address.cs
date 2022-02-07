using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineSystem.Models {
    [Table("addresses")]
    public class Address {
        [Key]
        [Column("id_address")]
        [Display(Name = "Código do Endereço")]
        public int id_address { get; set; }

        [Column("id_person")]
        [Display(Name = "Nome da pessoa")]
        [Required(ErrorMessage = "Selecione uma pessoa!")]
        public int id_person { get; set; }

        [Column("street")]
        [StringLength(60)]
        [Display(Name = "Rua")]
        [Required(ErrorMessage = "O nome da Rua é obrigatório!")]
        public string street { get; set; }

        [Column("complement")]
        [StringLength(60)]
        [Display(Name = "Complemento")]
        public string complement { get; set; }

        [Column("neighborhood")]
        [StringLength(60)]
        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O nome do Bairro é obrigatório!")]
        public string neighborhood { get; set; }

        [Column("city")]
        [StringLength(60)]
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "O nome da Cidade é obrigatório!")]
        public string city { get; set; }

        [Column("state")]
        [StringLength(2)]
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O nome do Estado é obrigatório!")]
        public string state { get; set; }

        public virtual Person Person { get; set; }
    }
}